using System.Diagnostics;
using Common.Configuration;

namespace Common.Utils.Waiter;

public static class Waiter
{
    private const int SafeGuardTimeOut = 60000;
    private static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;

    public static bool WaitUntil(Func<bool> condition) => WaitUntil(condition, AppConfig.DriverOptions.WaitTimeout, AppConfig.DriverOptions.PollingRate);
    public static bool WaitUntil(Func<bool> condition, int timeout) => WaitUntil(condition, timeout, AppConfig.DriverOptions.PollingRate);
    public static bool WaitUntil(Func<bool> condition, int timeout, int pollingRate)
    {
        var duration = TimeSpan.FromMilliseconds(timeout);
        var durationSafeguard = TimeSpan.FromMilliseconds(SafeGuardTimeOut);
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        while (stopwatch.Elapsed < duration && stopwatch.Elapsed < durationSafeguard)
        {
            if(condition()) return true;
            
            Thread.Sleep(pollingRate);
        }

        return false;
    }
}