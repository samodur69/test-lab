namespace Application.Business.PageElements.Player;
using Application.Model.PageElements.Player;

public class PlayerHolder(PlayerHolderControl control) : BaseElement(control)
{
    public PlayerNowPlaying NowPlayingControl => new(PlayerHolderControl.NowPlayingControl);
    public PlayerMain MainControl => new(PlayerHolderControl.MainControl);
    public PlayerVolume VolumeControl => new(PlayerHolderControl.VolumeControl);
    public bool WaitForPlayerToBeReady() => control.WaitForPlayerToBeReady();
}