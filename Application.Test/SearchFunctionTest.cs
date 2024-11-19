using Application.Business.Pages;
using Application.Test.TestCaseDataExamples;

namespace Application.Test;

[TestFixture]
public class SearchFunctionTest : TestFixtureBase
{

    [TestCaseSource(nameof(SearchCases.ValidSongName))]
    [Description("Search for a song")]
    [Category("Functional Test")]
    [Property("Priority", "P1")]
    public void SearchModule_ValidSongSearch(string songName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the song title in the search bar.
        var expectedResult = songName;
        home.EnterSearch(songName);

        var actualResult = home.SongTitle;

        actualResult.Should().Be(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.ValidArtistName))]
    [Description("Search for an artist")]
    [Category("Functional Test")]
    [Property("Priority", "P1")]
    public void SearchModule_ValidArtistSearch(string artistName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the artist name in the search bar.
        var expectedResult = artistName;
        home.EnterSearch(artistName);

        var actualResult = home.ArtistName;

        actualResult.Should().Be(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.ValidAlbumName))]
    [Description("Search for an album")]
    [Category("Functional Test")]
    [Property("Priority", "P1")]
    public void SearchModule_ValidAlbumSearch(string albumName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the album title in the search bar.
        var expectedResult = albumName;
        home.EnterSearch(albumName);

        var actualResult = home.AlbumName;

        actualResult.Should().Be(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.InvalidSearch))]
    [Description("Handle invalid search")]
    [Category("Negative Test")]
    [Property("Priority", "P1")]
    public void SearchModule_InvalidSearch(string input)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter an invalid search term in the search bar.
        var expectedResult = input;
        home.EnterSearch(input);

        var actualResults = new[] { home.SongTitle, home.ArtistName, home.AlbumName };

        actualResults.Should().NotContain(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.ValidSongName))]
    [Description("Filter search results ('Songs' category)")]
    [Category("Functional Test")]
    [Property("Priority", "P2")]
    public void SearchModule_ValidSongSearch_FilteredSearch(string songName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the song title in the search bar.
        home.EnterSearch(songName);

        //3. Apply "Songs" filter option.
        var expectedResult = songName;
        home.PressFilterButton(1);

        var actualResult = home.FilteredSongName;

        actualResult.Should().Be(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.ValidArtistName))]
    [Description("Filter search results ('Artist' category)")]
    [Category("Functional Test")]
    [Property("Priority", "P2")]
    public void SearchModule_ValidArtistSearch_FilteredSearch(string artistName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the song title in the search bar.
        home.EnterSearch(artistName);

        //3. Apply "Artist" filter option.
        var expectedResult = artistName;
        home.PressFilterButton(2);

        var actualResult = home.GridFilteredNames;

        actualResult.Should().Be(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.ValidAlbumName))]
    [Description("Filter search results ('Album' category)")]
    [Category("Functional Test")]
    [Property("Priority", "P2")]
    public void SearchModule_ValidAlbumSearch_FilteredSearch(string albumName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the song title in the search bar.
        home.EnterSearch(albumName);

        //3. Apply "Album" filter option.
        var expectedResult = albumName;
        home.PressFilterButton(4);

        var actualResult = home.GridFilteredNames;

        actualResult.Should().Be(expectedResult);
    }
    [TestCaseSource(nameof(SearchCases.ValidPlaylistName))]
    [Description("Filter search results ('Playlist' category)")]
    [Category("Functional Test")]
    [Property("Priority", "P2")]
    public void SearchModule_ValidPlaylistSearch_FilteredSearch(string playlistName)
    {
        //1. Launch the app
        var home = Home.OpenHomePage();
        home.IsItHomePage.Should().BeTrue();

        //2. Enter the song title in the search bar.
        home.EnterSearch(playlistName);

        //3. Apply "Playlist" filter option.
        var expectedResult = playlistName;
        home.PressFilterButton(3);

        var actualResult = home.GridFilteredNames;

        actualResult.Should().Be(expectedResult);
    }
}