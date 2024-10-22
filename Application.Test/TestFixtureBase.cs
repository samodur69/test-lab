namespace Application.Test;

using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;
using Business.Pages;

[TestFixture]
public abstract class TestFixtureBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected static readonly ILogger Logger = LoggerFactory.Create(
        new LoggerConfig(AppConfig.LoggerOptions.Type, AppConfig.LoggerOptions.FileName)
    );

    [TearDown]
    public void OnTearDown() => Home.CloseBrowser();

    [OneTimeTearDown]
    public void OnTestFixtureTearDown() => throw new NotImplementedException("Not Implemented Yet!");
}
