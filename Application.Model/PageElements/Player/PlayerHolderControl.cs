namespace Application.Model.PageElements.Player;
using Common.DriverWrapper;
using Common.Utils.Waiter;

public class PlayerHolderControl(IElement root) : BaseElementControl(root)
{
    private const string NowPlayingWidgetCSS = "div[data-testid='now-playing-widget']";
    private const string MainPlayerControlsCSS = "div[data-testid='player-controls']";
    private const string MainPlayerVolumeControlsCSS = "div[data-testid='volume-bar']";
    public static PlayerNowPlayingControl NowPlayingControl => new(Driver.FindElementByCss(NowPlayingWidgetCSS));
    public static PlayerMainControl MainControl => new(Driver.FindElementByCss(MainPlayerControlsCSS));
    public static PlayerVolumeControl VolumeControl => new(Driver.FindElementByCss(MainPlayerVolumeControlsCSS));
    public bool WaitForPlayerToBeReady() => Waiter.WaitUntil(() => MainControl.IsEnabled && MainControl.IsVisible);
}