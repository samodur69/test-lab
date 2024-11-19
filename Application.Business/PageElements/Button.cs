using Application.Model.PageElements;

namespace Application.Business.PageElements;

public class Button(ButtonControl model) : BaseElement(model)
{
    public virtual void Click() => model.Click();
}