namespace Application.Business.PageElements.Library;
using Application.Model.PageElements.Library;

public class Track(TrackControl track) : Button(track)
{
    public string Title => track.Title;
    public string TitleColor() => track.TitleColor();
    public bool IsPlaying() => track.IsPlaying();
    public void Play() => Click();
}