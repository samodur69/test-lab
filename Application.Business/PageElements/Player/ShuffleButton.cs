namespace Application.Business.PageElements.Player;
using Application.Model.PageElements.Player;
using Common.Utils.Waiter;

public class ShuffleButton(ShuffleButtonControl control) : Button(control)
{
    public void Toggle() => Click();
    public bool IsShuffle() => control.IsShuffle();
    public override void Click()
    {
        var oldValue = IsShuffle();

        Waiter.WaitUntil(() => { 
            if(oldValue != IsShuffle())
                return true;
            base.Click();
            return false;
        });
    }
}