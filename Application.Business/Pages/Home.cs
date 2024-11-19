using Application.Business.PageElements.Library;
using Application.Business.PageElements.Login;
using Application.Business.PageElements.Player;
using Application.Model.Pages;
using Common.Utils.ExceptionWrapper;
using Common.Utils.Waiter;

namespace Application.Business.Pages;

public class Home() : BusinessBase(new HomePage())
{
    private LoginButton LoginButton => new(HomePage.LoginButton);

    public Login PressLogin()
    {
        LoginButton.Click();
        return new Login();
    }

    public static Home OpenHomePage()
    {
        var homePage = new Home();
        homePage.Open();
        return homePage;
    }

    public void PressFilterButton(int index)
    {
        Console.WriteLine(HomePage.FilterButtons.element.FindParent().GetType());
        switch (index)
        {
            case 1:
                HomePage.SongFilterButton.Click();
                break;
            case 2:
                HomePage.ArtistFilterButton.Click();
                break;
            case 3:
                HomePage.PlaylistFilterButton.Click();
                break;
            case 4:
                HomePage.AlbumFilterButton.Click();
                break;
        }

    }
    public void EnterSearch(string input) => HomePage.SearchField.Text = input;
    public bool IsItHomePage => (IsNavigationBarVisible && IsYourLibraryVisible && IsHomeTabVisible);
    private bool IsNavigationBarVisible => HomePage.NavigationBar.IsVisible;
    private bool IsYourLibraryVisible => HomePage.YourLibrary.IsVisible;
    private bool IsHomeTabVisible => HomePage.HomeTab.IsVisible;
    public string SongTitle => HomePage.SongTitle;
    public string ArtistName => HomePage.Artist;
    public string AlbumName => HomePage.Album;
    public string FilteredSongName => HomePage.SongFilterResultText;
    public string GridFilteredNames => HomePage.GridFilterResultText;
    public bool IsUserProfileButtonVisible() => HomePage.UserProfileButton.IsVisible;
    public void CloseCookies() => Waiter.WaitUntil(() => !ExceptionWrapper.Test(()=>HomePage.CloseCookiesButton.Click()).IsError());
    public LibraryHolder Library => new(HomePage.LibraryHolderControl);
    public PlayerHolder Player => new(HomePage.PlayerControl);
}