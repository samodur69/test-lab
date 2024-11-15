namespace Application.Business;

using Application.Model;

public class BusinessControlBase(ModelControlBase model) : BusinessBase(model)
{
    public bool IsVisible{ get; } = model.IsVisible;
    public bool IsEnabled{ get; } = model.IsEnabled;
    public bool IsSelected{ get; } = model.IsSelected;
    public bool IsValid{ get; } = model.IsValid();
}