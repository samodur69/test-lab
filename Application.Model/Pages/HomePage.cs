namespace Application.Model.Pages;

using Application.Model.PageElements;
using Application.Model.PageElements.Login;
using Common.Configuration;

public class HomePage : ModelBase
{
    //Main Page
    private const string navigationBarCSS = "div[data-testid='global-nav-bar']";
    private const string yourLibraryCSS = "div[id='Desktop_LeftSidebar_Id']";
    private const string homeTabCSS = "div[class='main-view-container']";

    //Navigation Bar
    private const string LoginBtnCSS = "button[data-testid='login-button']";
    private const string UserProfileBtnCSS = "button[data-testid='user-widget-link']";
    private const string SearchInputCSS = "input[data-testid='search-input']";

    //Search Result
    private const string TopResultCardCSS = "div[data-testid='top-result-card']";
    private const string ArtistSectionCSS = "section[data-testid='artists-search-entity-shelf']";
    private const string AlbumSectionCSS = "section[data-testid='albums-search-entity-shelf']";
    private const string songTitleCSS = "div[data-encore-id='text']";
    private const string getItemFromSectionCSS = "p[data-encore-id='cardTitle']";

    //Search Filters
    private const string FilterButtonsCSS = "div[role='list']";
    private const string TrackFilterButtonCSS = "a[href$='tracks']";
    private const string ArtistsFilterButtonCSS = "a[href$='artists']";
    private const string PlaylistsFilterButtonCSS = "a[href$='playlists']";
    private const string AlbumsFilterButtonCSS = "a[href$='albums']";
    private const string SongsFilterResultCSS = "div[data-testid='tracklist-row']";
    private const string SongsFilterResultTextCSS = "div[data-encore-id='text']";
    private const string GridFilterResultTextCSS = "p[data-encore-id='cardTitle']";
    public HomePage()
    {
        Url = AppConfig.Url.Base;
    }

    //Main Page
    public static ModelControlBase NavigationBar => new(Driver.FindElementByCss(navigationBarCSS));
    public static ModelControlBase YourLibrary => new(Driver.FindElementByCss(yourLibraryCSS));
    public static ModelControlBase HomeTab => new(Driver.FindElementByCss(homeTabCSS));

    //Navigation Bar
    public static LoginButtonControl LoginButton => new(NavigationBar.element.FindElementByCss(LoginBtnCSS));
    public static InputControl SearchField => new(NavigationBar.element.FindElementByCss(SearchInputCSS));
    public static ButtonControl UserProfileButton => new(Driver.FindElementByCss(UserProfileBtnCSS));


    //Search Result
    public static ModelControlBase TopResult => new(Driver.FindElementByCss(TopResultCardCSS));
    public static ModelControlBase ArtistShelf => new(Driver.FindElementByCss(ArtistSectionCSS));
    public static ModelControlBase AlbumShelf => new(Driver.FindElementByCss(AlbumSectionCSS));
    public static string SongTitle => TopResult.element.FindElementByCss(songTitleCSS).Text;
    public static string Artist => ArtistShelf.element.FindElementByCss(getItemFromSectionCSS).Text;
    public static string Album => AlbumShelf.element.FindElementByCss(getItemFromSectionCSS).Text;

    //Search Filters
    public static ModelControlBase FilterButtons => new(Driver.FindElementByCss(FilterButtonsCSS));
    public static ButtonControl SongFilterButton => new(FilterButtons.element.FindElementByCss(TrackFilterButtonCSS));
    public static ButtonControl ArtistFilterButton => new(FilterButtons.element.FindElementByCss(ArtistsFilterButtonCSS));
    public static ButtonControl PlaylistFilterButton => new(FilterButtons.element.FindElementByCss(PlaylistsFilterButtonCSS));
    public static ButtonControl AlbumFilterButton => new(FilterButtons.element.FindElementByCss(AlbumsFilterButtonCSS));
    public static ModelControlBase SongFilterResult => new(Driver.FindElementByCss(SongsFilterResultCSS));
    public static string SongFilterResultText => SongFilterResult.element.FindElementByCss(SongsFilterResultTextCSS).Text;
    public static string GridFilterResultText => Driver.FindElementByCss(GridFilterResultTextCSS).Text;
}
