namespace Application.Business.PageElements.Player;
using Application.Model.PageElements.Player;

public class PlayerVolume(PlayerVolumeControl control) : BaseElement(control)
{
    public Button MuteButton => new(control.MuteButton);
    public int CurrentPos() => control.Position();
    public int MaxPos() => control.MaxPosition();
    public void MoveSliderStart() => control.MoveSliderStart();
    public void MoveSliderMiddle() => control.MoveSliderMiddle();
    public void MoveSliderEnd() => control.MoveSliderEnd();
    public void MoveSlider(int percentage, uint step = 0) => control.MoveSlider(percentage, step);
}