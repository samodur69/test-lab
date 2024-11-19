using Common.DriverWrapper;

namespace Application.Model.PageElements.Library;

public class LibraryHolderControl(IElement root) : BaseElementControl(root)
{
    private const string LikedTracksCss = "div[aria-describedby='onClickHintspotify:collection:tracks'][role='button']";
    public static LikedTracksButtonControl LikedTracksButton => new(Driver.FindElementByCss(LikedTracksCss));
}