namespace Application.Bdd;

using Application.Business.PageElements.Library;
using Application.Business.PageElements.Player;

using Business.Pages;

using FluentAssertions;
using TechTalk.SpecFlow;

[Binding]
public class PlayerSteps(ScenarioContext context) : BaseStep(context)
{
    private const int SecondsToWait = 4;
    private readonly Home _homePage = new();
    private LikedTracksHolder? likedTracksHolder;
    private Track? firstTrack;
    private PlayerHolder? player;
    private PlayerNowPlaying? nowPlaying;

    [Then("I have opened my library")]
    public void ThenIHaveOpenedMyLibrary()
    {
        var library = _homePage.Library;
        likedTracksHolder = library.FavoriteTracksButton.Click();
    }

    [Then("I select the first track")]
    public void ThenISelectTheFirstTrack()
    {
        firstTrack = likedTracksHolder!.FirstTrack;
    }

    [When("I wait for the player to be ready")]
    public void WhenIWaitForThePlayerToBeReady()
    {
        player = _homePage.Player;
        player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
    }

    [When("I play the track")]
    public void WhenIPlayTheTrack()
    {
        firstTrack!.Play();
    }

    [Then("the player should start playing the track")]
    public void ThenThePlayerShouldStartPlayingTheTrack()
    {
        player!.MainControl.WaitToStartPlaying().Should().BeTrue("The timeline should advance a bit");
    }

    [Then("the \"Now Playing\" control should display the selected track")]
    public void ThenNowPlayingControlShouldDisplayTheSelectedTrack()
    {
        nowPlaying = _homePage.Player.NowPlayingControl;
        nowPlaying.IsNowPlaying(firstTrack!).Should().BeTrue("Now Playing control should have the name of our first track");
    }

    [Then("the timeline should move forward after a few seconds")]
    public void ThenTheTimelineShouldMoveForwardAfterAFewSeconds()
    {
        player!.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");
    }
}