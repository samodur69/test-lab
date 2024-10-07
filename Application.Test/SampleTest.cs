using Application.Driver;
using OpenQA.Selenium.BiDi.Communication;
namespace Application.Test;
public class SampleTest : TestFixtureBase
{
    
    [Test]
    public void FirstTest()
    {
        1.Should().BePositive("this is life");
    }

    [Test]
    public void SecondTest()
    {
        Console.WriteLine($"Hello! I'm {AppConfig.AppName} v{AppConfig.AppVersion}. My homepage is {AppConfig.BaseUrl}. We are testing these browsers:");
        foreach(var item in AppConfig.Browsers) Console.WriteLine($"{item}");
        Console.WriteLine($"Maximized: {AppConfig.Maximize}");

        Logger.LOG($"Hello! I'm {AppConfig.AppName} v{AppConfig.AppVersion}. My homepage is {AppConfig.BaseUrl}.");

        (2+2).Should().Be(4);
    }
    [Test]
    public void DriverTest()
    {
        var webDriver = new WebDriver();
        webDriver.CreateDriver(new WebDriverSettings(AppConfig.Browsers[0], AppConfig.Maximize));
        webDriver.GoToURL(AppConfig.BaseUrl);
        string? result = webDriver.GetURL();
        (result).Should().Be(AppConfig.BaseUrl);
        webDriver.QuitDriver();
    }
}
