namespace Application.Test.TestCaseDataExamples;

public static class SearchCases
{
    public static IEnumerable<TestCaseData> ValidSongName() 
    {
        yield return new TestCaseData("Mockingbird");
        yield return new TestCaseData("Shape of You");
        yield return new TestCaseData("Blinding Lights");
    }

    public static IEnumerable<TestCaseData> ValidArtistName()
    {
        yield return new TestCaseData("Eminem");
        yield return new TestCaseData("Ed Sheeran");
        yield return new TestCaseData("Bohemian Rhapsody");
    }

    public static IEnumerable<TestCaseData> ValidAlbumName()
    {
        yield return new TestCaseData("Blurryface");
        yield return new TestCaseData("Thriller");
        yield return new TestCaseData("25");
    }

    public static IEnumerable<TestCaseData> ValidPlaylistName()
    {
        yield return new TestCaseData("Eminem Top 10");
        yield return new TestCaseData("Twenty One Pilots Mix");
        yield return new TestCaseData("Billie Eilish Mix");
    }

    public static IEnumerable<TestCaseData> InvalidSearch()
    {
        yield return new TestCaseData("Dooo!!))((");
        yield return new TestCaseData("#$^%%#");
        yield return new TestCaseData("ФАЫППУЙЧС");
    }
}