using Application.Api.Configuration;
using RestSharp;
using static RestAssured.Dsl;
using System.Net;
using System.Text.Json.Nodes;
using RestAssured.Request.Builders;
namespace Application.Api;

[TestFixture]
public class APITests : TestFixtureBase
{
    [Test]
    public void CreatePlaylistAndAddSong()
    {
        string nameForPlaylist = "NAME for playlist";
        string trackID = "11dFghVXANMlKmJXsNCbNl";

        // Step 1: Create Playlist
        SpotifyPlaylist playlistResponse = (SpotifyPlaylist)Given()
            .Spec(requestSpecification)
            .Body(new { name = nameForPlaylist })
            .When()
            .Post($"/users/{RestClientUtil.User_id}/playlists")
            .Then()
            .StatusCode(HttpStatusCode.Created)
            .DeserializeTo(typeof(SpotifyPlaylist));
        string playlistID = playlistResponse.Id;
        playlistID.Should().NotBeNullOrEmpty();

        // Step 2: Add Song to Playlist
        var addSongResponse = Given()
            .Spec(requestSpecification)
            .Body(new { uris = new[] { $"spotify:track:{trackID}" }, position = 0 })
            .When()
            .Post($"/playlists/{playlistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.Created);

        // Validation
        ShowResponse Result = (ShowResponse)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{playlistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(ShowResponse));
        Result.Items[0].Track.Id.Should().Be(trackID);
    }
}
