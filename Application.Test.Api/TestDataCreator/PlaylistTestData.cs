using System.Net;
using System.Text;
using Application.Api.Configurations;
using Application.Api.TestDataCreator.Models;

namespace Application.Api.TestDataCreator;

public class PlaylistTestData : IPLaylistTestData
{
    private readonly ApiClient apiSpotify = new ApiClient();
    public string CreatePlaylist(string? playListName = null)
    {
        return apiSpotify.CreatePlaylist(new PlaylistInfo(playListName ?? GenerateRandomString(20), GenerateRandomString(100), false)).Id;
    }

    public void DeletePlaylist(string playlistId)
    {
        var result = apiSpotify.DeletePlaylist(playlistId);
        result.Should().Be(HttpStatusCode.OK);
    }

    public void AddSongs(string playlistId, List<string> trackIDs)
    {
        var result = apiSpotify.AddSongs(playlistId, trackIDs);
        result.Should().Be(HttpStatusCode.Created);
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        return result.ToString();
    }
}
