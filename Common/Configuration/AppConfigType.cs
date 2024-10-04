namespace Common.Configuration;

public record AppConfig
(
	string AppName,
    string AppVersion,
	string BaseUrl,
    string Username,
    string Password,
    bool Maximize,
    List<string> Browsers
);