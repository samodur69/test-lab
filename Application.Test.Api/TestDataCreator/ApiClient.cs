
using Application.Api.Configuration;
using Application.Api.Configurations;
using Application.Api.TestDataCreator.Models;
using System.Net;
using System.Text.Json;

namespace Application.Api.TestDataCreator;

public class ApiClient : ISpotifyRest
{
    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public SpotifyPlaylist CreatePlaylist(PlaylistInfo request)
    {
        var response = RestClientUtil.PostRequest($"/users/{RestClientUtil.User_id}/playlists", null, request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        return JsonSerializer.Deserialize<SpotifyPlaylist>(response.Content,options);
    }

    public HttpStatusCode DeletePlaylist(string playlistId)
    {
        var response = RestClientUtil.DeleteRequest($"/playlists/{playlistId}/followers");
        return response.StatusCode;
    }

    public HttpStatusCode AddSongs(string playlistId, List<string> trackIDs)
    {
        Dictionary<string, List<string>> queryParams = new Dictionary<string, List<string>>() { { "uris", new List<string>(trackIDs) } };
        var response = RestClientUtil.PostRequest($"/playlists/{playlistId}/tracks", queryParams);
        return response.StatusCode;
    }
}

