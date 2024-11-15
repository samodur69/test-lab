using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V127.DOM;

namespace Application.Api.Configurations;

public record TokenResponse(
    string Access_token,
    string Token_type,
    int Expires_in
);

public record UserProfile(
    string Id,
    string Href,
    string Country,
    string Display_name
);

public record SpotifyPlaylist(
    string Description,
    string Name,
    string Id
);

public record PlaylistItems(
    string Href,
    int Total,
    List<Items> Items
);

public record Items(
    string AddedAt,
    Track Track
);
public record Track(
    string Href,
    string Id,
    string Name,
    string Uri
);

public record Image(
    string Url,
    string Height,
    string width
);

public record PlaylistInfo(
    string name,
    string description,
    bool @public
);
public record ReorderRequest(
    int range_start,
    int insert_before,
    int range_length
);
public record AddTracksRequest(
    string[] uris,
    int position
);
public record RemoveTrackRequest(string uri);
public record RemoveRequest(RemoveTrackRequest[] tracks);