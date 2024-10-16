using Common.DriverWrapper;

namespace Application.Model;

public class ModelControlBase(IElement Root) : ModelBase
{
    public bool IsVisible{ get; } = Root.Displayed;
    public bool IsEnabled{ get; } = Root.Enabled;
    public bool IsSelected{ get; } = Root.Selected;
}
