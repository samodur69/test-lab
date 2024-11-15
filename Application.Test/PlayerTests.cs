namespace Application.Test;

using Business.Pages;
using Common.Utils.Waiter;

[TestFixture]
//[Parallelizable(ParallelScope.All)]
public class PlayerTests : TestFixtureBase
{
    private static readonly string LoginEmail = AppConfig.EnvironmentVariables.Email;
    private static readonly string LoginPassword = AppConfig.EnvironmentVariables.Password;
    private const int SecondsToWait = 4;
    private const int SkipBackwardWait = 5000;

    [Test]
    [Description("Verify that a user can play a track")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void Player_Should_Play_Track_When_AnyTrack_IsSelected()
    {
        var mainPage = MainPage();

        var library = mainPage.Library;
        var firstTrack = library.FavoriteTracksButton.Click().FirstTrack;

        var player = mainPage.Player;
        player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");

        firstTrack.Play();

        player.MainControl.WaitToStartPlaying().Should().BeTrue("The timeline should advance a bit");

        var nowPlaying = mainPage.Player.NowPlayingControl;
        nowPlaying.IsNowPlaying(firstTrack).Should().BeTrue("Now Playing control should have the name of our first track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");
    }

    [Test]
    [Description("Verify that a user can pause a currently playing track")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void Player_Should_Play_Track_Then_Pause_Track_When_AnyTrack_IsSelected()
    {
        var mainPage = MainPage();

        var library = mainPage.Library;
        var firstTrack = library.FavoriteTracksButton.Click().FirstTrack;

        var player = mainPage.Player;
        player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");

        firstTrack.Play();

        player.MainControl.WaitToStartPlaying().Should().BeTrue("The timeline should advance a bit");

        var nowPlaying = mainPage.Player.NowPlayingControl;
        nowPlaying.IsNowPlaying(firstTrack).Should().BeTrue("Now Playing control should have the name of our first track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");

        player.MainControl.PlayButton.Click();
        player.MainControl.IsPositionChange(SecondsToWait).Should().BeFalse("The timeline should NOT move if the track has stopped playing");
    }

    [Test]
    [Description("Verify that a user can resume a paused track")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void Player_Should_Play_Track_Then_Pause_And_Resume_Track_When_AnyTrack_IsSelected()
    {
        var mainPage = MainPage();

        var library = mainPage.Library;
        var firstTrack = library.FavoriteTracksButton.Click().FirstTrack;

        var player = mainPage.Player;
        player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        
        firstTrack.Play();
        
        player.MainControl.WaitToStartPlaying().Should().BeTrue("The timeline should advance a bit");

        var nowPlaying = mainPage.Player.NowPlayingControl;
        nowPlaying.IsNowPlaying(firstTrack).Should().BeTrue("Now Playing control should have the name of our first track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");

        player.MainControl.PlayButton.Click();
        player.MainControl.IsPositionChange(SecondsToWait).Should().BeFalse("The timeline should NOT move if the track has been stopped");

        player.MainControl.PlayButton.Click();
        player.MainControl.IsPositionIncreased(SecondsToWait).Should().BeTrue("The timeline should've moved if the track is really playing");
    }

    [Test]
    [Description("Verify that a user can skip to the next track")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Skip_ToNextTrack_When_AnyTrack_IsPlaying()
    {
        var mainPage = MainPage();

        var library = mainPage.Library;
        var trackList = library.FavoriteTracksButton.Click().GetTrackList();

        trackList.Count.Should().BeGreaterThanOrEqualTo(3, "We need at least 3 tracks to properly test skip forward/backward");

        var player = mainPage.Player;
        player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");

        player.MainControl.RepeatButton.SetRepeatPlaylist();

        trackList[0].Play();

        player.MainControl.WaitToStartPlaying().Should().BeTrue("The timeline should advance a bit");

        var nowPlaying = mainPage.Player.NowPlayingControl;
        nowPlaying.IsNowPlaying(trackList[0]).Should().BeTrue("Now Playing control should have the name of our first track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");

        player.MainControl.RepeatButton.SetRepeatPlaylist();

        player.MainControl.SkipForwardButton.Click();
        nowPlaying.IsNowPlaying(trackList[1]).Should().BeTrue("Now Playing control should have the name of our second(next) track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");
    }

    [Test]
    [Description("Verify that a user can skip to the previous track")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Skip_ToPreviousTrack_When_AnyTrack_IsPlaying()
    {
        var mainPage = MainPage();

        var library = mainPage.Library;
        var trackList = library.FavoriteTracksButton.Click().GetTrackList();

        trackList.Count.Should().BeGreaterThanOrEqualTo(3, "We need at least 3 tracks to properly test skip forward/backward");

        var player = mainPage.Player;
        player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");

        player.MainControl.RepeatButton.SetRepeatPlaylist();

        trackList[2].Play();

        player.MainControl.WaitToStartPlaying().Should().BeTrue("The timeline should advance a bit");

        var nowPlaying = mainPage.Player.NowPlayingControl;
        nowPlaying.IsNowPlaying(trackList[2]).Should().BeTrue("Now Playing control should have the name of our first track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");
        
        player.MainControl.SkipBackwardButton.Click();

        //Because skip backward doesn't always skip to the previous track...
        Waiter.WaitUntil(() => { 
            if(nowPlaying.IsNowPlayingNoWait(trackList[1]))
                return true;
            player.MainControl.SkipBackwardButton.Click();
            return false;
        }, SkipBackwardWait);

        nowPlaying.IsNowPlaying(trackList[1]).Should().BeTrue("Now Playing control should have the name of our previous track");
        player.MainControl.IsAtPosition(SecondsToWait).Should().BeTrue("The timeline should've moved if the track has really been playing");
    }

    [Test]
    [Description("Verify that a user can increase the volume")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void Player_Should_Increase_Volume_When_VolumeSlider_IsUsed()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();

        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");

        var volume = mainPage.Player.VolumeControl;

        volume.MoveSlider(70);
        volume.CurrentPos().Should().BeGreaterThanOrEqualTo( 65, "The slider should be greater than the value, but it might be a bit off" );
    }

    [Test]
    [Description("Verify that a user can decrease the volume")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void Player_Should_Decrease_Volume_When_VolumeSlider_IsUsed()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();

        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var volume = mainPage.Player.VolumeControl;

        volume.MoveSlider(20);
        volume.CurrentPos().Should().BeLessThanOrEqualTo( 25, "The slider should be less than the value, but it might be a bit off"  );
    }

    [Test]
    [Description("Verify that a user can enable the shuffle mode")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Enable_ShuffleMode_If_ShuffleModeIsDisabled_When_ShuffleButtonIsClicked()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();

        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var mainControl = mainPage.Player.MainControl;

        mainControl.ShuffleButton.Toggle();
        mainControl.ShuffleButton.IsShuffle().Should().BeTrue("The shuffle button should be enabled");
    }

    [Test]
    [Description("Verify that a user can disable the shuffle mode")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Disable_ShuffleMode_If_ShuffleModeIsEnabled_When_ShuffleButtonIsClicked()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();

        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var mainControl = mainPage.Player.MainControl;

        mainControl.ShuffleButton.Toggle();
        mainControl.ShuffleButton.IsShuffle().Should().BeTrue("The shuffle button should be in the ON position");

        mainControl.ShuffleButton.Toggle();
        mainControl.ShuffleButton.IsShuffle().Should().BeFalse("The shuffle button should be in the OFF position");
    }

    [Test]
    [Description("Verify that repeat all mode can be activated")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Enable_RepeatAllMode_If_RepeatAllModeIsNotEnabled_When_RepeatButtonIsClicked()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();
        
        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var mainControl = mainPage.Player.MainControl;

        mainControl.RepeatButton.SetRepeatPlaylist();
        mainControl.RepeatButton.IsRepeatPlaylist().Should().BeTrue("Repeat playlist button should be in the ON position");
    }

    [Test]
    [Description("Verify that repeat mode can be deactivated")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Disable_RepeatAllMode_If_RepeatAllModeIsEnabled_When_RepeatButtonIsClicked()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();
        
        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var mainControl = mainPage.Player.MainControl;

        mainControl.RepeatButton.SetRepeatPlaylist();
        mainControl.RepeatButton.IsRepeatPlaylist().Should().BeTrue("Repeat playlist button should be in the ON position");

        mainControl.RepeatButton.SetUnchecked();
        mainControl.RepeatButton.IsRepeatPlaylist().Should().BeFalse("Repeat playlist button should be in the OFF position");
        mainControl.RepeatButton.IsRepeatOne().Should().BeFalse("Repeat one track button should be in the OFF position");
    }

    [Test]
    [Description("Verify that a user can mute")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Mute_Volume_If_VolumeIsNotMuted_When_MuteButtonIsClicked()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();

        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var volume = mainPage.Player.VolumeControl;

        volume.MoveSlider(50);

        volume.MuteButton.Click();
        volume.CurrentPos().Should().BeLessThanOrEqualTo( 1, "The mute button affects the volume slider, so it should be 0" );
    }

    [Test]
    [Description("Verify that a user can unmute")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void Player_Should_Unmute_Volume_If_VolumeIsMuted_When_MuteButtonIsClicked()
    {
        var mainPage = MainPage();
        mainPage.Library.FavoriteTracksButton.Click();

        mainPage.Player.WaitForPlayerToBeReady().Should().BeTrue("The music player needs time to load");
        var volume = mainPage.Player.VolumeControl;

        volume.MoveSlider(50);

        volume.MuteButton.Click();
        volume.CurrentPos().Should().BeLessThanOrEqualTo( 1, "The mute button affects the volume slider, so it should be 0" );

        volume.MuteButton.Click();
        volume.CurrentPos().Should().BeGreaterThanOrEqualTo( 45, "The mute button affects the volume slider, so it should be back to the original value" );
    }

    private static Home MainPage()
    {
        var homePage = new Home();
        homePage.Open();

        var loginPage = homePage.PressLogin();

        loginPage.IsLoginTitleVisible().Should().BeTrue();

        loginPage.IsLoginWithGoogleVisible().Should().BeTrue();
        loginPage.IsLoginWithFaceBookVisible().Should().BeTrue();
        loginPage.IsLoginWithAppleVisible().Should().BeTrue();

        loginPage.EnterUsername(LoginEmail);
        loginPage.EnterPassword(LoginPassword);
        loginPage.PressLogin();
        
        homePage.IsUserProfileButtonVisible().Should().BeTrue("This element is visible when the login flow was successful");

        homePage.CloseCookies();

        return homePage;
    }
}