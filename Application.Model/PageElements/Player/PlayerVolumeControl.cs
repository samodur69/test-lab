namespace Application.Model.PageElements.Player;
using Common.DriverWrapper;
using Common.Utils.Waiter;

public class PlayerVolumeControl(IElement root) : BaseSliderControl(root, VolumeSliderCSS, 2)
{
    private const string MuteButtonCSS = "button[data-testid='volume-bar-toggle-mute-button']";
    private const string VolumeSliderCSS = "div[data-testid='progress-bar']";
    public ButtonControl MuteButton => new(Driver.FindElementByCss(MuteButtonCSS));
    public override int Position() => ProgressToInt(Root.FindElementByCss(VolumeSliderCSS).GetAttribute("style"));
    public override int MaxPosition() => 100;
}