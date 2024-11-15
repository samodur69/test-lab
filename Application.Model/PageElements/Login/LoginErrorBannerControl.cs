using Common.DriverWrapper;

namespace Application.Model.PageElements.Login;

public class LoginErrorBannerControl(IElement root) : InputControl(root)
{
    private const string SvgErrorCSS = "svg[aria-label='Error:']";
    private const string SpanMessageCSS = "span";
    public bool IsError() => Root.FindElementsByCss(SvgErrorCSS).Any() && Root.FindElementsByCss(SpanMessageCSS).Any();
}