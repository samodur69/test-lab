using Common.DriverWrapper;
using Common.Utils.ExceptionWrapper;
using Common.Utils.Waiter;
using OpenQA.Selenium.Internal;

namespace Application.Model.PageElements.Login;

public class LoginUsernameInputControl(IElement root) : InputControl(root)
{
    private const string UsernameErrorCSS = "div[id='username-error']";
    private const string SvgErrorCSS = "svg[aria-label='Error:']";
    private const string SpanMessageCSS = "span";
    public bool IsValidData() => !ElementAttributeEqual( "aria-invalid", "true", true, DEFAULT_TIMEOUT );
    public bool IsValidatorError() 
    { 
        IElement? usernameErrorElem = null;

        Waiter.WaitUntil
        (
            () => 
            { 
                ExceptionWrapper.Test(() => usernameErrorElem = Root.FindParent().FindElementByCss(UsernameErrorCSS) );
                return usernameErrorElem != null;
            }
            , DEFAULT_TIMEOUT
        );

        return usernameErrorElem != null && usernameErrorElem.FindElementsByCss(SvgErrorCSS).Any() && usernameErrorElem.FindElementsByCss(SpanMessageCSS).Any();
    }
}