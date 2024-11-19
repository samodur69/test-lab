using Application.Model.PageElements;

namespace Application.Business.PageElements;

public class Input(InputControl model) : BaseElement(model)
{
    public void Clear() => model.Clear();
    public override string Text {
        get => model.Text;
        set => model.Text = value;
    }
}