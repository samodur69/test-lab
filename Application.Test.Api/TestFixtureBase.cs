using Application.Api.Configuration;
using Application.Api.TestDataCreator;
using RestAssured.Request.Builders;

using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;

namespace Application.Api;

[TestFixture]
public abstract class TestFixtureBase
{
    static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected RequestSpecification requestSpecification = new RequestSpecBuilder().Build();
    protected readonly string baseUri = RestClientUtil.BaseUrl;
    protected string accessToken = "";
    protected string _basePlaylistID = "";
    protected string _trackID = "";

    private TestDataSpotifyBase? testDataCreator;

    [SetUp]
    public void OnSetUp()
    {
        RestClientUtil.ConfigureLogger(LoggerFactory.Create(
        new LoggerConfig(AppConfig.LoggerOptions.Type, AppConfig.LoggerOptions.FileName), 
            ReportPortal.Shared.Context.Current
        ));

        RestClientUtil.InitializeClient();
        accessToken= RestClientUtil.AccessToken;
        requestSpecification = new RequestSpecBuilder()
            .WithBaseUri(baseUri)
            .WithBasePath("v1")
            .WithOAuth2(accessToken)
            .WithContentType("application/json")
            .Build();

        int maxCapacity = 6;

        testDataCreator = new TestDataSpotifyBase();
        _basePlaylistID = testDataCreator.Playlists.CreatePlaylist();
        var randomTracks = testDataCreator.Tracks.GetRandomTracks(maxCapacity);
        testDataCreator.Playlists.AddSongs(_basePlaylistID, randomTracks.Take(maxCapacity - 1).ToList());
        _trackID = randomTracks[maxCapacity-1];
    }

    [TearDown]
    public void OnTearDown()
    {
        testDataCreator!.Playlists.DeletePlaylist(_basePlaylistID!);
    }
}