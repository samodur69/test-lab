using Application.Api.Configuration;
using Application.Api.TestDataCreator;
using RestAssured.Request.Builders;

namespace Application.Api;

[TestFixture]
public abstract class TestFixtureBase
{
    protected RequestSpecification requestSpecification;
    protected readonly string baseUri = RestClientUtil.BaseUrl;
    protected string accessToken;

    protected string _basePlaylistID;
    protected string _trackID;

    private TestDataSpotifyBase testDataCreator;

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

        int maxCapacity = 6;

        testDataCreator = new TestDataSpotifyBase();
        _basePlaylistID = testDataCreator.Playlists.CreatePlaylist();
        var randomTracks = testDataCreator.Tracks.GetRandomTracks(maxCapacity);
        testDataCreator.Playlists.AddSongs(_basePlaylistID, randomTracks.Take(maxCapacity - 1).ToList());
        _trackID = randomTracks[maxCapacity-1];
    }
    [OneTimeTearDown]
    public void CleanUp()
    {
        testDataCreator.Playlists.DeletePlaylist(_basePlaylistID);
    }
}
