namespace Application.Model.PageElements.Library;
using Common.DriverWrapper;

public class LikedTracksButtonControl(IElement root) : ButtonControl(root)
{
    private const string LikedTracksCSS = "div[data-testid='track-list']";
    public new LikedTracksHolderControl Click()
    {
        base.Click();
        return new(Driver.FindElementByCss(LikedTracksCSS));
    }
}