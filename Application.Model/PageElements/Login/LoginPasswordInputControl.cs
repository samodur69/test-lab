using Common.DriverWrapper;
using Common.Utils.Waiter;

namespace Application.Model.PageElements.Login;

public class LoginPasswordInputControl(IElement root) : InputControl(root)
{
    private const string PasswordErrorCss = "div[id='password-error']";
    private const string SvgErrorCss = "svg[aria-label='Error:']";
    private const string SpanMessageCss = "span";
    public bool IsValidData() => !ElementAttributeEqual( "aria-invalid", "true", true, DEFAULT_TIMEOUT );
    public bool IsValidatorError() 
    { 
        IElement? passwordErrorElem = null;

        Waiter.WaitUntil
        (
            () => { 
                var elems = Driver.FindElementsByCss(PasswordErrorCss);
                if(elems.Count() == 1) passwordErrorElem = elems.First();
                return passwordErrorElem != null; }
            , DEFAULT_TIMEOUT
        );
        
        return passwordErrorElem != null && passwordErrorElem.FindElementsByCss(SvgErrorCss).Any() && passwordErrorElem!.FindElementsByCss(SpanMessageCss).Any();
    }
}