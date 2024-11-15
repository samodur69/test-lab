namespace Application.Model.PageElements.Player;
using Common.DriverWrapper;

public class PlayerMainControl(IElement root) : BaseSliderControl(root, ProgressBarCSS, 25)
{
    private const string PlayPauseButtonCSS = "button[data-testid='control-button-playpause']";
    private const string PlaybackPositionCSS = "div[data-testid='playback-position']";
    private const string ProgressBarCSS = "div[data-testid='progress-bar']";
    private const string SkipBackwardButtonCSS = "button[data-testid='control-button-skip-back']";
    private const string SkipForwardButtonCSS = "button[data-testid='control-button-skip-forward']";
    private const string PlaybackDurationCSS = "div[data-testid='playback-duration']";
    private const string RepeatButtonCSS = "button[data-testid='control-button-repeat']";
    private const string ShuffleButtonCSS = "button[data-testid='control-button-shuffle']";

    public ButtonControl SkipBackwardButton => new(Driver.FindElementByCss(SkipBackwardButtonCSS));
    public ButtonControl SkipForwardButton => new(Driver.FindElementByCss(SkipForwardButtonCSS));
    public ButtonControl PlayButton => new(Driver.FindElementByCss(PlayPauseButtonCSS));
    public RepeatButtonControl RepeatButton => new(Driver.FindElementByCss(RepeatButtonCSS));
    public ShuffleButtonControl ShuffleButton => new(Driver.FindElementByCss(ShuffleButtonCSS));
    public override int Position() => ProgressToInt(Root.FindElementByCss(ProgressBarCSS).GetAttribute("style"));
    public override int MaxPosition() => 100;

    public int PlaybackPosition() => StringToSeconds(Driver.FindElementByCss(PlaybackPositionCSS).Text);
    public int PlaybackMaxPosition() => StringToSeconds(Driver.FindElementByCss(PlaybackDurationCSS).Text);
}