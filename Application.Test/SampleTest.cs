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

        (2+2).Should().Be(4);
    }
}
