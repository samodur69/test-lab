using Common.DriverWrapper;

namespace Application.Model.PageElements.Player;

public class ShuffleButtonControl(IElement root) : ButtonControl(root)
{
    public bool IsShuffle() => Root.GetAttribute("aria-checked").Equals("true", StringComparison.OrdinalIgnoreCase);
}