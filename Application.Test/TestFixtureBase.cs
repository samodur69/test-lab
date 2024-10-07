namespace Application.Test;
using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;

[TestFixture]
public abstract class TestFixtureBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected static readonly ILogger Logger = LoggerFactory.Create(new LoggerConfig(AppConfig.LoggerType, AppConfig.LoggerFileName));
    [OneTimeSetUp]
    public void OnTestFixtureSetUp()
    {
    }
}
