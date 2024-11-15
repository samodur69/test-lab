namespace Application.Test;

using Business.Pages;

[TestFixture]
public class SearchFunctionTest : TestFixtureBase
{
    private static IEnumerable<TestCaseData> ValidSongName() 
    {
        yield return new TestCaseData("Mockingbird");
        yield return new TestCaseData("Shape of You");
        yield return new TestCaseData("Blinding Lights");
    }
    private static IEnumerable<TestCaseData> ValidArtistName()
    {
        yield return new TestCaseData("Eminem");
        yield return new TestCaseData("Ed Sheeran");
        yield return new TestCaseData("Bohemian Rhapsody");
    }
    private static IEnumerable<TestCaseData> ValidAlbumName()
    {
        yield return new TestCaseData("Blurryface");
        yield return new TestCaseData("Thriller");
        yield return new TestCaseData("25");
    }
    private static IEnumerable<TestCaseData> ValidPlaylistName()
    {
        yield return new TestCaseData("Eminem Top 10");
        yield return new TestCaseData("Twenty One Pilots Mix");
        yield return new TestCaseData("Billie Eilish Mix");
    }
    private static IEnumerable<TestCaseData> InvalidSearch()
    {
        yield return new TestCaseData("Dooo!!))((");
        yield return new TestCaseData("#$^%%#");
        yield return new TestCaseData("ФАЫППУЙЧС");
    }
    [TestCaseSource(nameof(ValidSongName))]
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
    [TestCaseSource(nameof(ValidArtistName))]
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
    [TestCaseSource(nameof(ValidAlbumName))]
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
    [TestCaseSource(nameof(InvalidSearch))]
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
    [TestCaseSource(nameof(ValidSongName))]
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
    [TestCaseSource(nameof(ValidArtistName))]
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
    [TestCaseSource(nameof(ValidAlbumName))]
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
    [TestCaseSource(nameof(ValidPlaylistName))]
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