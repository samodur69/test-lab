namespace Application.Model.PageElements.Library;
using Common.DriverWrapper;

public class LibraryHolderControl(IElement root) : BaseElementControl(root)
{
    private const string LikedTracksCSS = "div[aria-describedby='onClickHintspotify:collection:tracks'][role='button']";
    public static LikedTracksButtonControl LikedTracksButton => new(Driver.FindElementByCss(LikedTracksCSS));
}