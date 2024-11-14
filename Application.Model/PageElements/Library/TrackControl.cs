namespace Application.Model.PageElements.Library;
using Common.DriverWrapper;
using Common.Utils.RgbNormalizer;

public class TrackControl(IElement root, int index) : ButtonControl(root)
{
    public readonly int Index = index;
    private const string TrackNameCSS = "a[data-testid='internal-track-link']";
    private const string TrackPlayButtonCSS = "button";
    public const string PlayingColorGreen = "rgba(30, 215, 96, 1)";
    public string Title => Text;
    /// <summary>
    ///     The title/name of the song
    /// </summary>
    public override string Text
    {
        get => Root.FindElementByCss(TrackNameCSS).Text;
        set { throw new NotImplementedException($"{nameof(BaseElementControl)} setter for Text should not be used!"); } 
    }
    public string TitleColor() => Root.FindElementByCss(TrackNameCSS).GetCssValue("color");
    public bool IsPlaying() => 
        RgbNormalizer.Normalize(Root.FindElementByCss(TrackNameCSS).GetCssValue("color")).Equals(RgbNormalizer.Normalize(PlayingColorGreen), StringComparison.OrdinalIgnoreCase);
    public override void Click()
    { 
        var elem = Root.FindElementByCss(TrackPlayButtonCSS);

        //Double clicking on the empty space resets the timeline and begins to play the track
        Driver.DoubleClick(elem.Position.X + elem.Size.Width + 10, elem.Position.Y);
    }
}