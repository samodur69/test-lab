using Common.DriverWrapper;

namespace Application.Model.PageElements;

public class InputControl(IElement Root) : ModelControlBase(Root)
{
    public void Clear() => Root.ClearViaKeys();
    public string Text {
        get { return Root.Text; }
        set { Root.SendText(value); }
    }
    public void Submit() => Root.SendEnterKey();
}