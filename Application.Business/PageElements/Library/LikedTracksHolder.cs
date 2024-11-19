using Application.Model.PageElements.Library;

namespace Application.Business.PageElements.Library;

public class LikedTracksHolder(LikedTracksHolderControl holder) : BaseElement(holder)
{
    public Track FirstTrack => new(holder.GetFirstTrack());
    public List<Track> GetTrackList() => holder.GetTrackList().Select(elem => new Track(elem))
                 .ToList();
}