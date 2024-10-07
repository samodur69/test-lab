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
        var loggerType = Config["Logging:Type"];
        var loggerFileName = Config["Logging:FileName"];

        var useEnvVar = bool.Parse(Config["UseEnvironmentVars"]);
        var maximize = bool.Parse(Config["BrowserOptions:Maximize"]);

        var userName = useEnvVar ? Config["EnvironmentVars:Username"] : Config["Credentials:Username"];
        var password = useEnvVar ? Config["EnvironmentVars:Password"] : Config["Credentials:Password"];

        var browserList = ((Func<List<string>>)(() => {
            var list = new List<string>();

            foreach (var browser in Config.GetSection("Browsers").GetChildren()) {
                if(browser.Value == null) continue;
                if(!bool.Parse(browser.Value)) continue;

                list.Add(browser.Key);
            }
            return list;
        }))();

        return new(
            AppName    : Config["Application"],
            AppVersion : Config["Version"],
            BaseUrl    : Config["Url:Base"],

            Username : userName,
            Password : password,

            Maximize : maximize,
            Browsers : browserList,

            LoggerType : loggerType,
            LoggerFileName : loggerFileName
        );
    }
}
