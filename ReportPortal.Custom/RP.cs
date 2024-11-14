using System.Text;
using Common.Configuration;
using ReportPortal.Shared.Extensibility;
using ReportPortal.Shared.Extensibility.ReportEvents;
using ReportPortal.Shared.Extensibility.ReportEvents.EventArgs;
using ReportPortal.Shared.Reporter;

namespace ReportPortal.Custom;

public class ReportPortalEventsObserver : IReportEventsObserver
{
    private static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    private const string Redacted = "[REDACTED]";
    static private readonly List<string> Secrets = 
    [
        AppConfig.EnvironmentVariables.Email,
        AppConfig.EnvironmentVariables.Username,
        AppConfig.EnvironmentVariables.Password,
        AppConfig.EnvironmentVariables.API_ClientID,
        AppConfig.EnvironmentVariables.API_ClientSecret,
        AppConfig.EnvironmentVariables.API_RefreshToken
    ];
    
    public void Initialize(IReportEventsSource reportEventsSource)
    {
        reportEventsSource.OnBeforeLogsSending += ReportEventsSource_OnBeforeLogsSending;
        reportEventsSource.OnBeforeTestStarting += ReportEventsSource_OnBeforeTestStarting;
    }

    static private void ReportEventsSource_OnBeforeTestStarting(ITestReporter testReporter, BeforeTestStartingEventArgs args)
    {
        foreach(var secret in Secrets)
        {
            StringBuilder name = new(args.StartTestItemRequest.Name);
            name.Replace(secret, Redacted);
            args.StartTestItemRequest.Name = name.ToString();
        }
    }

    static private void ReportEventsSource_OnBeforeLogsSending(ILogsReporter logsReporter, BeforeLogsSendingEventArgs args)
    {
        args.CreateLogItemRequests
        .ToList()
        .ForEach(req =>
            {
                StringBuilder text = new(req.Text);
                foreach(var secret in Secrets)
                    text.Replace(secret, Redacted);
                req.Text = text.ToString();
            }
        );
    }
}