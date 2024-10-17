namespace Common.Utils.Waiter;

using System.Diagnostics;
using Common.Configuration;

public class Waiter
{
    private static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;

    public static bool WaitUntil(Func<bool> condition) => WaitUntil(condition, AppConfig.DriverOptions.WaitTimeout, AppConfig.DriverOptions.PollingRate);
    public static bool WaitUntil(Func<bool> condition, int timeout) => WaitUntil(condition, timeout, AppConfig.DriverOptions.PollingRate);
    public static bool WaitUntil(Func<bool> condition, int timeout, int pollingRate)
    {
        var duration = TimeSpan.FromMilliseconds(timeout);
        var stopwatch = new Stopwatch();
        
        stopwatch.Start();

        while (stopwatch.Elapsed < duration)
        {
            if(condition()) return true;
            
            Thread.Sleep(pollingRate);
        }

        return false;
    }
}