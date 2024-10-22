using Common.DriverWrapper;

namespace Application.Model.PageElements;

public class InputControl(IElement root) : BaseElementControl(root)
{
    public virtual void Clear() 
    { 
        Root.ClearViaKeys(); 
        WaitForDomProperty("value", ""); 
    }
    public override string Text {
        get { return Root.Text; }
        set 
        { 
            Root.SendText(value); 
            WaitForDomProperty("value", value); 
        }
    }
    public virtual void Submit() => Root.SendEnterKey();
}