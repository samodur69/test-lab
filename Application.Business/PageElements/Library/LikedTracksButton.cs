using Application.Model.PageElements.Library;

namespace Application.Business.PageElements.Library;

public class LikedTracksButton(LikedTracksButtonControl button) : Button(button)
{
    public new LikedTracksHolder Click() => new(button.Click());
}