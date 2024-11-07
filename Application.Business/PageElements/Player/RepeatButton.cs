namespace Application.Business.PageElements.Player;
using Application.Model.PageElements.Player;
using Common.Utils.Waiter;

public class RepeatButton(RepeatButtonControl control) : Button(control)
{
    public void SetUnchecked()
    {
        Waiter.WaitUntil(() => { 
            if(control.GetState() == RepeatButtonControl.TriState.UNCHECKED)
                return true;

            control.Click();
            return false;
        });
    }
    public void SetRepeatPlaylist()
    {
        Waiter.WaitUntil(() => { 
            if(control.GetState() == RepeatButtonControl.TriState.REPEAT_PLAYLIST)
                return true;

            control.Click();
            return false;
        });
    }
    public void SetRepeatOne()
    {
        Waiter.WaitUntil(() => { 
            if(control.GetState() == RepeatButtonControl.TriState.REPEAT_ONE)
                return true;

            control.Click();
            return false;
        });
    }
    public bool IsUnchecked() => control.GetState() == RepeatButtonControl.TriState.UNCHECKED;
    public bool IsRepeatPlaylist() => control.GetState() == RepeatButtonControl.TriState.REPEAT_PLAYLIST;
    public bool IsRepeatOne() => control.GetState() == RepeatButtonControl.TriState.REPEAT_ONE;
}