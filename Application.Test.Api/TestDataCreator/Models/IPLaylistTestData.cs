namespace Application.Api.TestDataCreator.Models;

public interface IPLaylistTestData
{
    string CreatePlaylist(string? playListName = null);
    void DeletePlaylist(string playlistId);
    void AddSongs(string playlistId, List<string> trackIDs);
}

