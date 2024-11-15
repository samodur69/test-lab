using Common.DriverWrapper;

namespace Application.Model.PageElements;

public class BaseElementControl(IElement root) : ModelControlBase(root)
{
    public virtual string Text
    { 
        get { return Root.Text; } 
        set { throw new NotImplementedException($"{nameof(BaseElementControl)} setter for Text is not implemented!"); } 
    }

    public void WaitValid(int timeout) => WaitUntilValid(timeout);
    public void WaitInvalid(int timeout) => WaitUntilNotValid(timeout);
}