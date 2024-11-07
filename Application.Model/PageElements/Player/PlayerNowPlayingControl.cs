namespace Application.Model.PageElements.Player;
using Common.DriverWrapper;

public class PlayerNowPlayingControl(IElement root) : BaseElementControl(root)
{
    private const string SongNameCSS = "div[data-testid='context-item-info-title']";
    private const string AuthorNameCSS = "div[data-testid='context-item-info-subtitles']";
    
    public static BaseElementControl TrackName() => new(Driver.FindElementByCss(SongNameCSS));
    public static BaseElementControl AuthorName() => new(Driver.FindElementByCss(AuthorNameCSS));
}