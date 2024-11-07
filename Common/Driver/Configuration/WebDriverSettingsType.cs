namespace Common.Driver.Configuration;

public record WebBrowserSettings
(
    bool Maximized = true,
    bool Headless = false,
    bool DisableSandbox = false,
    bool DisableGPU = false,
    bool DisableSharedMemory = false,
    bool EnableWindowSize = false,
    int WindowSizeX = 1920,
    int WindowSizeY = 1080,
    string DebuggerAddress = "",
    int DebuggerPort = -1,
    int RemoteDebuggingPort = -1
);