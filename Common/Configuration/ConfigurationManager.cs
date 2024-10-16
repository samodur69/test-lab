using Microsoft.Extensions.Configuration;

namespace Common.Configuration;

public class ConfigurationManager
{
    public static readonly AppConfig AppConfig;
    private static readonly string configFolder = "Configurations";
    private static readonly string configName = "test.uat.json";

    static ConfigurationManager()
    {
        AppConfig = ParseConfig(new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(Path.Combine(configFolder, configName), optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build());
    }

    private static AppConfig ParseConfig(IConfigurationRoot Config)
    {
        var driverType = Config["Driver:Type"];
        var driverTimeout = int.Parse(Config["Driver:WaitTimeout"]);
        var driverPollingRate = int.Parse(Config["Driver:PollingRate"]);
        var screenshotDir = Config["Driver:ScreenshotPath"];

        var loggerType = Config["Logging:Type"];
        var loggerFileName = Config["Logging:FileName"];

        var useEnvVar = bool.Parse(Config["EnvironmentVars:Enable"]);
        var userName = useEnvVar ? Config["EnvironmentVars:Username"] : Config["Credentials:Username"];
        var password = useEnvVar ? Config["EnvironmentVars:Password"] : Config["Credentials:Password"];

        var maximize = bool.Parse(Config["Browser:Maximize"]);

        return new(
            Url : new(
                Base: Config["Url:Base"], 
                Login: Config["Url:Login"], 
                Signup: Config["Url:Signup"]
            ),
            
            Credentials : new(
                Username : userName, 
                Password : password
            ),
            
            BrowserOptions : new(
                Browser : Config["Browser:Type"], 
                Maximize : maximize
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
