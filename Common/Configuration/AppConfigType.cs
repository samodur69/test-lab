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
	string Signup,
    string API_Base,
    string API_Token
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
    bool Maximize,
    bool Headless,
    bool DisableSandbox,
    bool DisableGPU,
    bool DisableSharedMemory,
    bool EnableWindowSize,
    int WindowSizeX,
    int WindowSizeY,
    string DebuggerAddress,
    int DebuggerPort,
    int RemoteDebuggingPort
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
    string Password = "",
    string API_ClientID = "",
    string API_ClientSecret = "",
    string API_RefreshToken = ""
);