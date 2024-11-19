using Application.Business.PageElements.Library;
using Application.Model.PageElements.Player;
using Common.Utils.ExceptionWrapper;
using Common.Utils.Waiter;

namespace Application.Business.PageElements.Player;

public class PlayerNowPlaying(PlayerNowPlayingControl control) : BaseElement(control)
{
    public BaseElement TrackName() => new(PlayerNowPlayingControl.TrackName());
    public BaseElement AuthorName() => new(PlayerNowPlayingControl.AuthorName());

    public bool IsNowPlaying(Track track)
    {
        var isEqual = false;
        return Waiter.WaitUntil(
            () => !ExceptionWrapper.Test(() => isEqual = TrackName().Text.Equals(track.Title, StringComparison.OrdinalIgnoreCase))
                .IsError() && isEqual && track.IsPlaying()
        );
    }

    public bool IsNowPlayingNoWait(Track track)
    {
        var isEqual = false;
        return !ExceptionWrapper.Test(() => isEqual = TrackName().Text.Equals(track.Title, StringComparison.OrdinalIgnoreCase))
            .IsError() && isEqual && track.IsPlaying();
    }
}