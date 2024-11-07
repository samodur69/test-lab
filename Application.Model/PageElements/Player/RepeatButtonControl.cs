using Common.DriverWrapper;

namespace Application.Model.PageElements.Player;

public class RepeatButtonControl(IElement root) : ButtonControl(root)
{
    public enum TriState
    {
        UNCHECKED,
        REPEAT_PLAYLIST,
        REPEAT_ONE
    }
    public TriState GetState() => Root.GetAttribute("aria-checked").ToLower() switch {
        "false" => TriState.UNCHECKED,
        "true" => TriState.REPEAT_PLAYLIST,
        _ => TriState.REPEAT_ONE
    };
}