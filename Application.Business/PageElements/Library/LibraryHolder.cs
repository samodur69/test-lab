using Application.Model.PageElements.Library;

namespace Application.Business.PageElements.Library;

public class LibraryHolder(LibraryHolderControl holder) : BaseElement(holder)
{
    public LikedTracksButton FavoriteTracksButton => new(LibraryHolderControl.LikedTracksButton);
}