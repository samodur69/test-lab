namespace Application.Test;
using Common.Configuration;


[TestFixture]
public abstract class TestFixtureBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    [OneTimeSetUp]
    public void OnTestFixtureSetUp()
    {
    }
}
