namespace Application.Business.PageElements.Player;
using Application.Model.PageElements.Player;
using Common.Utils.ExceptionWrapper;
using Common.Utils.Waiter;
using Microsoft.VisualBasic;

public class PlayerMain(PlayerMainControl control) : BaseElement(control)
{
    public Button SkipBackwardButton => new(control.SkipBackwardButton);
    public Button SkipForwardButton => new(control.SkipForwardButton);
    public Button PlayButton => new(control.PlayButton);
    public RepeatButton RepeatButton => new(control.RepeatButton);
    public ShuffleButton ShuffleButton => new(control.ShuffleButton);
    public void ResetPlayback()
    {
        ExceptionWrapper.Test(() => {
        if(Position() >= 1 || PlaybackPosition() >= 1)
            MoveSliderStart();
        });
    }
    public int Position() => control.Position();
    public int MaxPosition() => control.MaxPosition();
    public int PlaybackPosition() => control.PlaybackPosition();
    public int PlaybackMaxPosition() => control.PlaybackMaxPosition();
    public void MoveSliderStart() => control.MoveSliderStart();
    public void MoveSliderMiddle() => control.MoveSliderMiddle();
    public void MoveSliderEnd() => control.MoveSliderEnd();
    public void MoveSlider(int percentage, uint step = 0) => control.MoveSlider(percentage, step);
    public bool WaitToStartPlaying() => Waiter.WaitUntil( () => Position() <= 1) && Waiter.WaitUntil( () => Position() > 1);
    public bool IsAtPosition(int seconds) => Waiter.WaitUntil( () => PlaybackPosition() >= seconds);
    public bool IsAtPositionPercentage(int percent) => Waiter.WaitUntil( () => Position() >= percent);
    public bool IsPositionChange(int seconds)
    {
        var oldPos = PlaybackPosition();
        Waiter.WaitUntil( () => false, seconds * 1000);

        return PlaybackPosition() != oldPos;
    }

    public bool IsPositionIncreased(int seconds)
    {
        var oldPos = PlaybackPosition();
        Waiter.WaitUntil( () => false, seconds * 1000);

        return PlaybackPosition() > oldPos;
    }
}