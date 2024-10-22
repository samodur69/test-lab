namespace Application.Business.PageElements;

using Application.Model.PageElements;

public class Input(InputControl model) : BaseElement(model)
{
    public virtual void Clear() => model.Clear();
    public override string Text {
        get => model.Text;
        set => model.Text = value;
    }
    public virtual void Submit() => model.Submit();
}