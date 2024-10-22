namespace Application.Api;

[TestFixture]
public class TestFixtureBase
{
    [OneTimeSetUp]
    public void OnTestFixtureSetUp() => throw new NotImplementedException("Not Implemented Yet!");

    [Test]
    public void Test() => (1+1).Should().Be(2);
}
