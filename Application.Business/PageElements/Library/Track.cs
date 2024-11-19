using Application.Model.PageElements.Library;

namespace Application.Business.PageElements.Library;

public class Track(TrackControl track) : Button(track)
{
    public string Title => track.Title;
    public bool IsPlaying() => track.IsPlaying();
    public void Play() => Click();
}