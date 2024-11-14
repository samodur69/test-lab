using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Common.Configuration;

public static class ConfigurationManager
{
    private static readonly string ConfigFolder = "Configurations";
    private static readonly string ConfigName = "test.uat.json";
    
    public static readonly AppConfig AppConfig = ParseConfig(new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile(Path.Combine(ConfigFolder, ConfigName), optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build());

    private static AppConfig ParseConfig(IConfigurationRoot config)
    {
        string GetValue(string arg) => config[arg] ?? "";

        var driverType = GetValue("Driver:Type");
        var driverTimeout = int.Parse(GetValue("Driver:WaitTimeout"));
        var driverPollingRate = int.Parse(GetValue("Driver:PollingRate"));
        var screenshotDir = GetValue("Driver:ScreenshotPath");

        var loggerType = GetValue("Logging:Type");
        var loggerFileName = GetValue("Logging:FileName");

        var useEnvVar = bool.Parse(GetValue("EnvironmentVars:Enable"));
        var environmentVariables = new EnvironmentVariables();

        if(useEnvVar){
            environmentVariables = new EnvironmentVariables(
                Username : GetValue(GetValue("EnvironmentVars:Username")),
                Email : GetValue(GetValue("EnvironmentVars:Email")),
                Password : GetValue(GetValue("EnvironmentVars:Password")),
                API_ClientID : GetValue(GetValue("EnvironmentVars:API_ClientID")),
                API_ClientSecret : GetValue(GetValue("EnvironmentVars:API_ClientSecret")),
                API_RefreshToken : GetValue(GetValue("EnvironmentVars:API_RefreshToken"))
            );
        }

        var userName = GetValue("Credentials:Username");
        var email = GetValue("Credentials:Email");
        var password = GetValue("Credentials:Password");

        var maximize = bool.Parse(GetValue("Browser:Maximize"));
        var headless = bool.Parse(GetValue("Browser:Headless"));
        var disableSandbox = bool.Parse(GetValue("Browser:DisableSandbox"));
        var disableGpu = bool.Parse(GetValue("Browser:DisableGPU"));
        var disableSharedMemory = bool.Parse(GetValue("Browser:DisableSharedMemory"));
        var enableWindowSize = bool.Parse(GetValue("Browser:EnableWindowSize"));
        var windowSizeX = int.Parse(GetValue("Browser:WindowSizeX"));
        var windowSizeY = int.Parse(GetValue("Browser:WindowSizeY"));
        var debuggerAddress = GetValue("Browser:DebuggerAddress");
        var debuggerPort = int.Parse(GetValue("Browser:DebuggerPort"));
        var remoteDebuggingPort = int.Parse(GetValue("Browser:RemoteDebuggingPort"));

        return new(
            Url : new(
                Base: GetValue("Url:Base"), 
                Login: GetValue("Url:Login"), 
                Signup: GetValue("Url:Signup"),
                API_Base: GetValue("Url:API_Base"),
                API_Token: GetValue("Url:API_Token")
            ),
            
            Credentials : new(
                Username : userName, 
                Email : email,
                Password : password
            ),

            EnvironmentVariables : environmentVariables,
            
            BrowserOptions : new(
                Browser : GetValue("Browser:Type"), 
                Maximize : maximize,
                Headless : headless,
                DisableSandbox : disableSandbox,
                DisableGPU : disableGpu,
                DisableSharedMemory : disableSharedMemory,
                EnableWindowSize : enableWindowSize,
                WindowSizeX : windowSizeX,
                WindowSizeY : windowSizeY,
                DebuggerAddress : debuggerAddress,
                DebuggerPort: debuggerPort,
                RemoteDebuggingPort: remoteDebuggingPort
            ),

            DriverOptions : new(
                Type : driverType,
                WaitTimeout : driverTimeout,
                PollingRate : driverPollingRate,
                ScreenshotDir : screenshotDir
            ),

            LoggerOptions : new(
                Type : loggerType, 
                FileName : loggerFileName
            )
        );
    }
}
