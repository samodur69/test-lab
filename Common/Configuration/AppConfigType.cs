namespace Common.Configuration;

public record AppConfig
(
	Url Url,
    Credentials Credentials,
    EnvironmentVariables EnvironmentVariables,
    BrowserOptions BrowserOptions,
    DriverOptions DriverOptions,
    LoggerOptions LoggerOptions
);

public record Url
(
    string Base,
	string Login,
	string Signup
);

public record DriverOptions
(
    string Type,
    int WaitTimeout,
    int PollingRate,
    string ScreenshotDir
);

public record BrowserOptions
(
    string Browser,
    bool Maximize
);

public record LoggerOptions
(
    string Type,
    string FileName
);

public record Credentials
(
    string Username,
    string Email,
    string Password
);

public record EnvironmentVariables
(
    string Username = "",
    string Email = "",
    string Password = ""
);