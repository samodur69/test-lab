using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Common.Configuration;

public static class ConfigurationManager
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
        string GetValue(string arg) => Config[arg] ?? "";

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
                Password : GetValue(GetValue("EnvironmentVars:Password"))
            );
        }

        var userName = GetValue("Credentials:Username");
        var email = GetValue("Credentials:Email");
        var password = GetValue("Credentials:Password");

        var maximize = bool.Parse(GetValue("Browser:Maximize"));

        return new(
            Url : new(
                Base: GetValue("Url:Base"), 
                Login: GetValue("Url:Login"), 
                Signup: GetValue("Url:Signup")
            ),
            
            Credentials : new(
                Username : userName, 
                Email : email,
                Password : password
            ),

            EnvironmentVariables : environmentVariables,
            
            BrowserOptions : new(
                Browser : GetValue("Browser:Type"), 
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
