using Common.DriverWrapper;
using Common.Utils.ExceptionWrapper;
using Common.Utils.Waiter;

namespace Application.Model.PageElements.Login;

public class LoginPasswordInputControl(IElement root) : InputControl(root)
{
    private const string PasswordErrorCSS = "div[id='password-error']";
    private const string SvgErrorCSS = "svg[aria-label='Error:']";
    private const string SpanMessageCSS = "span";
    public bool IsValidData() => !ElementAttributeEqual( "aria-invalid", "true", true, DEFAULT_TIMEOUT );
    public bool IsValidatorError() 
    { 
        IElement? passwordErrorElem = null;

        Waiter.WaitUntil
        (
            () => { 
                var elems = Driver.FindElementsByCss(PasswordErrorCSS);
                if(elems.Count() == 1) passwordErrorElem = elems.First();
                return passwordErrorElem != null; }
            , DEFAULT_TIMEOUT
        );
        
        return passwordErrorElem != null && passwordErrorElem.FindElementsByCss(SvgErrorCSS).Any() && passwordErrorElem!.FindElementsByCss(SpanMessageCSS).Any();
    }
}