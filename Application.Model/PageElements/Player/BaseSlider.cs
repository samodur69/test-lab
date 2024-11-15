namespace Application.Model.PageElements.Player;
using Common.DriverWrapper;
using Common.Utils.Waiter;

public abstract class BaseSliderControl(IElement root, string mainSliderCSS, int defaultStep) : BaseElementControl(root)
{
    private const int MaxTimeout = 30000;
    private const string ProgressBarTransform = "--progress-bar-transform: ";
    private readonly string MainSliderCSS = mainSliderCSS;
    private readonly int DefaultStep = defaultStep;
    public virtual int Position() { throw new NotImplementedException(); }
    public virtual int MaxPosition() { throw new NotImplementedException(); }

    public virtual void MoveSlider(int percentage, uint step = 0)
    {
        if(step == 0) step = (uint)DefaultStep;

        var progressBar = Root.FindElementByCss(MainSliderCSS);
        var pos = 0; 

        //First set the slider into neutral, aka 50%...
        MoveSliderMiddle();

        //Continuously move it left/right until it is more or less the value we want...
        Waiter.WaitUntil( () => { 
            if(percentage < 50) pos -= (int)step;
            else if(percentage > 50) pos += (int)step;

            if(Position() is var curPos && (percentage < 50 && curPos > percentage) || (percentage > 50 && curPos < percentage))
            {
                Driver.DragAndDropToOffset(progressBar, pos, 0);
                return false;
            }
            return true;
        }, MaxTimeout, 1 );
    }

    public virtual void MoveSliderStart()
    {
        var progressBar = Root.FindElementByCss(MainSliderCSS);

        Waiter.WaitUntil( () => { 
            Driver.DragAndDropToOffset(progressBar, -1 * progressBar.Size.Width / 2, 0);
            if(Position() is <= 1)
                return true;
            return false;
        }, MaxTimeout, 1);
    }

    public virtual void MoveSliderMiddle()
    {
        var progressBar = Root.FindElementByCss(MainSliderCSS);

        Waiter.WaitUntil( () => { 
            Driver.DragAndDropToOffset(progressBar, 0, 0);

            if (Position() is >= 49 and <= 51)
                return true;

            if(Position() is > 50 or < 50)
                return false;
            return true;
        }, MaxTimeout, 1);
    }

    public virtual void MoveSliderEnd()
    {
        var progressBar = Root.FindElementByCss(MainSliderCSS);

        Waiter.WaitUntil( () => { 
            Driver.DragAndDropToOffset(progressBar, progressBar.Size.Width / 2, 0);
            if(Position() is >= 99)
                return true;
            return false;
        }, MaxTimeout, 1);
    }

    protected virtual int ProgressToInt(string str)
    {
        string[] parts = str.Split(';');
        string result = parts[0].Replace("%", "")
                             .Replace(ProgressBarTransform, "");

        return (int)double.Parse(result);
    }

    protected virtual int StringToSeconds(string time)
    {
        string[] parts = time.Split(':');

        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);

        return (minutes * 60) + seconds;
    }
}