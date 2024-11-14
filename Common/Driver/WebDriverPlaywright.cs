namespace Common.Driver;

using Common.Driver.Configuration;
using Common.Enums;

using Microsoft.Playwright;

public static class PlaywrightManager
{
    private sealed record ContextHolder(IPlaywright? Driver = null, IBrowser? Browser = null, IPage? Page = null);
    public record Context(IPlaywright Driver, IBrowser Browser, IPage Page);
    private static readonly ThreadLocal<ContextHolder> _context = new();
    private static bool _installDepsOnce = true;

    public static Context Create(WebDriverConfig settings)
    {
        if (_context.Value != null && _context.Value.Driver != null) return new(_context.Value!.Driver!, _context.Value!.Browser!, _context.Value!.Page!);

        if(_installDepsOnce){
            var exitCode = Microsoft.Playwright.Program.Main(new[] {"install", "--with-deps"});
            if (exitCode != 0)
                throw new InvalidOperationException($"Playwright installation exited with code {exitCode}");
            _installDepsOnce = false;
        }
        
        _context.Value = settings.DriverType switch
        {
            WebDriverTypes.CHROME => CreateChromium(settings.DriverSettings, "chrome"),
            WebDriverTypes.EDGE => CreateChromium(settings.DriverSettings, "msedge"),
            WebDriverTypes.FIREFOX => CreateFirefox(settings.DriverSettings),
            _ => throw new ArgumentException($"SeleniumDriverManager - browser type: {settings.DriverType} is not supported!")
        };

        _context.Value!.Page!.SetDefaultTimeout(settings.WaitTimeout);
        _context.Value!.Page!.SetDefaultNavigationTimeout(settings.WaitTimeout);

        return new(_context.Value!.Driver!, _context.Value!.Browser!, _context.Value!.Page!);
    }
    public static void GoToUrl(string url) => _context.Value?.Page?.GotoAsync(url).Wait();
    public static string? GetUrl() => _context.Value?.Page?.Url;
    public static void Close()
    {
        _context.Value?.Browser?.CloseAsync().Wait();
        _context.Value?.Driver?.Dispose();
        _context.Value = new ContextHolder();
    }

    private static ContextHolder CreateChromium(WebBrowserSettings settings, string channel)
    {
        var playwright = Playwright.CreateAsync().Result;

        var options = new BrowserTypeLaunchOptions();
        var arg = new List<string>{};

        if (settings.Maximized)
            arg.Add("--start-maximized");
        if(settings.Headless){
            options.Headless = true;
            arg.Add("--headless");
        } else  options.Headless = false;
        if(settings.DisableSandbox){
            options.ChromiumSandbox = false;
            arg.Add("--no-sandbox");
        }
        if(settings.DisableGPU)
            arg.Add("--disable-gpu");
        if(settings.DisableSharedMemory)
            arg.Add("--disable-dev-shm-usage");
        if(settings.EnableWindowSize)
            arg.Add($"--window-size={settings.WindowSizeX},{settings.WindowSizeY}");
        if(settings.DebuggerAddress.Length > 0 && settings.RemoteDebuggingPort > 0)
        {
            arg.Add($"--remote-debugging-port={settings.RemoteDebuggingPort}");
            arg.Add($"--remote-debugging-address={settings.DebuggerAddress}");
        }
        arg.Add("--enable-media-stream");
        arg.Add("--disable-sync");
        arg.Add("--enable-logging");
        arg.Add("--log-level=0");
        arg.Add("--test-type=webdriver");

        options.Channel = channel;
        options.Args = arg;

        //These arguments are passed by playwright. We need to disable some of them to be able to play media.
        options.IgnoreDefaultArgs = 
        [
            "--hide-scrollbars",
            "--blink-settings=primaryHoverType=2,availableHoverTypes=2,primaryPointerType=4,availablePointerTypes=4",
            "--mute-audio", 
            "--disable-media",
            "--disable-features=TranslateUI",
            "--disable-field-trial-config",
            "--disable-background-timer-throttling",
            "--disable-back-forward-cache",
            "--disable-breakpad",
            "--disable-component-extensions-with-background-pages",
            "--disable-component-update",
            $"{(!settings.DisableSharedMemory ? "--disable-dev-shm-usage" : "")}",
            "--disable-extensions",
            "--disable-renderer-backgrounding",
            "--force-color-profile=srgb",
            "--metrics-recording-only",
            "--disable-ipc-flooding-protection",
            "--export-tagged-pdf",
            "--unsafely-disable-devtools-self-xss-warnings",
            $"{(!settings.DisableSandbox ? "--no-sandbox" : "")}",
            "--disable-features=ImprovedCookieControls,LazyFrameLoading,GlobalMediaControls,DestroyProfileOnBrowserClose,MediaRouter,DialMediaRouteProvider,AcceptCHFrame,AutoExpandDetailsElement,CertificateTransparencyComponentUpdater,AvoidUnnecessaryBeforeUnloadCheckSync,Translate,HttpsUpgrades,PaintHolding,ThirdPartyStoragePartitioning,LensOverlay,PlzDedicatedWorker",
        ];

        var browser = playwright.Chromium.LaunchAsync(options).Result; 
        var context = browser.NewContextAsync(new BrowserNewContextOptions{ ViewportSize = ViewportSize.NoViewport }).Result;
        var page = context.NewPageAsync().Result;

        return new(playwright, browser, page);
    }

    private static ContextHolder CreateFirefox(WebBrowserSettings settings)
    {
        var playwright = Playwright.CreateAsync().Result;

        var options = new BrowserTypeLaunchOptions();
        var arg = new List<string>{};

        if (settings.Maximized)
            arg.Add("-kiosk");
        if(settings.Headless){
            options.Headless = true;
            arg.Add("-headless");
        } else options.Headless = false;
        if(settings.EnableWindowSize){
            arg.Add($"-width={settings.WindowSizeX}");
            arg.Add($"-height={settings.WindowSizeY}");
        }

        var browser = playwright.Firefox.LaunchAsync(options).Result; 
        var context = browser.NewContextAsync(new BrowserNewContextOptions{  ViewportSize = new ViewportSize() { Width = settings.WindowSizeX, Height = settings.WindowSizeY } }).Result;

        var page = context.NewPageAsync().Result;
        page.SetViewportSizeAsync(settings.WindowSizeX, settings.WindowSizeY).Wait();

        return new(playwright, browser, page);
    }
}
