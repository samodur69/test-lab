namespace Application.Business.PageElements;

using Application.Model.PageElements;

public class Button(ButtonControl model) : BaseElement(model)
{
    public virtual void Click() => model.Click();
}