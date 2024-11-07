using Application.Api.Configuration;
using RestAssured.Request.Builders;

namespace Application.Api;

[TestFixture]
public abstract class TestFixtureBase
{
    protected RequestSpecification? requestSpecification;
    protected readonly string baseUri = RestClientUtil.BaseUrl;
    protected string accessToken;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        RestClientUtil.InitializeClient();
        accessToken= RestClientUtil.AccessToken;
        requestSpecification = new RequestSpecBuilder()
            .WithBaseUri(baseUri)
            .WithBasePath("v1")
            .WithOAuth2(accessToken)
            .WithContentType("application/json")
            .Build();
    }
}
