using Common.DriverWrapper;

namespace Application.Model;

public class ModelControlBase(IElement Root) : ModelBase
{
    public bool IsVisible{ get; } = Root.Displayed;
    public bool IsEnabled{ get; } = Root.Enabled;
    public bool IsSelected{ get; } = Root.Selected;
    public string GetAttribute(string attributeName) => Root.GetAttribute(attributeName);
    public string GetCssValue(string propertyName) => Root.GetAttribute(propertyName);
    public string GetDomAttribute(string attributeName) => Root.GetAttribute(attributeName);
    public string GetDomProperty(string propertyName) => Root.GetAttribute(propertyName);
}
