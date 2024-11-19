using Common.DriverWrapper;

namespace Application.Model.PageElements.Library;

public class LikedTracksButtonControl(IElement root) : ButtonControl(root)
{
    private const string LikedTracksCSS = "div[data-testid='track-list']";
    public new LikedTracksHolderControl Click()
    {
        base.Click();
        return new LikedTracksHolderControl(Driver.FindElementByCss(LikedTracksCSS));
    }
}