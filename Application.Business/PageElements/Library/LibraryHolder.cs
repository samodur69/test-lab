namespace Application.Business.PageElements.Library;
using Application.Model.PageElements.Library;

public class LibraryHolder(LibraryHolderControl holder) : BaseElement(holder)
{
    public LikedTracksButton FavoriteTracksButton => new(LibraryHolderControl.LikedTracksButton);
}