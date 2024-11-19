using Common.DriverWrapper;

namespace Application.Model.PageElements.Login;

public class LoginErrorBannerControl(IElement root) : InputControl(root)
{
    private const string SvgErrorCss = "svg[aria-label='Error:']";
    private const string SpanMessageCss = "span";
    public bool IsError() => Root.FindElementsByCss(SvgErrorCss).Any() && Root.FindElementsByCss(SpanMessageCss).Any();
}