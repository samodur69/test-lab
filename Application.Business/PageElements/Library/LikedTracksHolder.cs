namespace Application.Business.PageElements.Library;

using Application.Business.PageElements.Player;
using Application.Model.PageElements.Library;

public class LikedTracksHolder(LikedTracksHolderControl holder) : BaseElement(holder)
{
    public Track FirstTrack => new(holder.GetFirstTrack());
    public List<Track> GetTrackList() => holder.GetTrackList().Select(elem => new Track(elem))
                 .ToList();
}