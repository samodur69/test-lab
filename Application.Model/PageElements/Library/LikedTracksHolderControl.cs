namespace Application.Model.PageElements.Library;
using Common.DriverWrapper;

public class LikedTracksHolderControl(IElement root) : BaseElementControl(root)
{
    private const string ButtonCSS = "div[data-testid='tracklist-row']";
    public TrackControl GetFirstTrack() => new(Root.FindElementByCss(ButtonCSS), int.Parse(Root.FindElementByCss(ButtonCSS).FindParent().GetAttribute("aria-rowindex")));
    public IEnumerable<TrackControl> GetTrackList() =>
    [.. Root.FindElementByCss(ButtonCSS)
    .FindParent().FindParent()
    .FindElementsByCss(ButtonCSS)
    .Select(elem => { var index = int.Parse(elem.FindParent().GetAttribute("aria-rowindex")); return new TrackControl(elem, index); } )
    .OrderBy(elem => elem.Index)];
}