namespace Application.Api.Configuration;
public record TokenResponse(
    string access_token,
    string token_type,
    int expires_in
);
public record UserProfile(
    string id,
    string href,
    string country,
    string display_name
);
public record SpotifyPlaylist(
    string Id,
    string Uri
);
public record ShowResponse(
    int Total,
    List<ShowItem> Items
);
public record ShowItem(
    string AddedAt,
    Track Track
);
public record Track(
    string Href,
    string Id,
    string Name,
    string Uri
);