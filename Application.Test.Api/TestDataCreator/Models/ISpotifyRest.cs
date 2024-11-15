using Application.Api.Configurations;
using System.Net;

namespace Application.Api.TestDataCreator.Models;

public interface ISpotifyRest
{
    SpotifyPlaylist CreatePlaylist(PlaylistInfo request);
    HttpStatusCode DeletePlaylist(string playlistId);
    HttpStatusCode AddSongs(string playlistId, List<string> trackIDs);
}
