namespace Application.Test;

public class SampleTest
{
    [Test]
    public void FirstTest()
    {
        1.Should().BePositive("this is life");
    }
}
