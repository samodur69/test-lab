using Application.Api.Configuration;
using static RestAssured.Dsl;
using System.Net;
using Application.Api.Configurations;
using Newtonsoft.Json;
using Common.Utils.Waiter;
using System.Xml.Linq;
using System.Reflection;
namespace Application.Api;

[TestFixture]
public class APITests : TestFixtureBase
{
    private static IEnumerable<TestCaseData> InvalidPlaylistIDS()
    {
        yield return new TestCaseData("12vfsd WROND ID fervc234c12");
    }
    private static IEnumerable<TestCaseData> ValidPlaylistDetails()
    {
        yield return new TestCaseData("Name for Updated Playlist", "description for Updated playlist", false);
    }
    private static IEnumerable<TestCaseData> ValidReorderingData()
    {
        yield return new TestCaseData(1,5,2);
    }
    private static IEnumerable<TestCaseData> InvalidPlaylistAndTrackIDs()
    {
        yield return new TestCaseData("wrong","wrong");
    }
    private static IEnumerable<TestCaseData> ValidNewPlaylistName()
    {
        yield return new TestCaseData("New Playlist name with no data");
    }
    private static IEnumerable<TestCaseData> ValidNewPlaylistDetails()
    {
        yield return new TestCaseData("New Playlist name with data", "description for new playlist", false);
    }

    [Test]
    [Description("Get Playlist With valid ID")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void GetPlaylist_ValidID()
    {
        var response = (SpotifyPlaylist)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(SpotifyPlaylist));

        //Validation
        response.Id.Should().Be(_basePlaylistID);
    }

    [Test, TestCaseSource(nameof(InvalidPlaylistIDS))]
    [Description("Get Playlist With Invalid ID")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void GetPlaylist_InvalidID(string id)
    {
        Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{id}")
            .Then()
            .StatusCode(HttpStatusCode.BadRequest);
    }

    [Test, TestCaseSource(nameof(ValidPlaylistDetails))]
    [Description("Change Playlist Details")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void ChangePlaylistDetails(string nameNew, string descriptionNew,bool visibility)
    {
        var requestBody = new PlaylistInfo(nameNew, descriptionNew, visibility);

        Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Put($"/playlists/{_basePlaylistID}")    
            .Then()
            .StatusCode(HttpStatusCode.OK).Extract().Response();

        //Validation
        SpotifyPlaylist response = null;

        Waiter.WaitUntil(() => {
            response = (SpotifyPlaylist)Given()
                .Spec(requestSpecification)
                .When()
                .Get($"/playlists/{_basePlaylistID}")
                .Then()
                .StatusCode(HttpStatusCode.OK)
                .DeserializeTo(typeof(SpotifyPlaylist));
            return response.Name == nameNew;
        },30000, 5000);

        response = (SpotifyPlaylist)Given()
                .Spec(requestSpecification)
                .When()
                .Get($"/playlists/{_basePlaylistID}")
                .Then()
                .StatusCode(HttpStatusCode.OK)
                .DeserializeTo(typeof(SpotifyPlaylist));

        response.Name.Should().Be(nameNew);
        response.Description.Should().Be(descriptionNew);
    }

    [Test]
    [Description("Get Playlist Items Info")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void GetPlaylistItems_ValidID()
    {
        var response = (PlaylistItems)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(PlaylistItems));

        //Validation
        response.Href.Should().Contain(_basePlaylistID);
    }

    [Test, TestCaseSource(nameof(ValidReorderingData))]
    [Description("Reordering the position of playlist items")]
    [Category("Smoke")]
    [Property("Priority", "P2")]
    public void UpdatePlaylistItems_Reorder(int start,int position,int range)
    {
        //Set up
        var beforeResponse = (PlaylistItems)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(PlaylistItems));

        var requestBody = new ReorderRequest(start, position, range);
        //Act
        Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Put($"/playlists/{_basePlaylistID}/tracks")
            .StatusCode(HttpStatusCode.OK);

        //Validation
        var afterResponse = (PlaylistItems)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(PlaylistItems));

        for (int i = 0; i < range; i++)
        {
            beforeResponse.Items[start+i].Track.Id.Should().Be(afterResponse.Items[position-range+i].Track.Id);
        }
    }

    [Test]
    [Description("Add Items To Playlist")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void AddItemsToPlaylist_ValidTrackID()
    {
        var requestBody = new AddTracksRequest(new[] { _trackID }, 0);
        Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Post($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.Created);

        //Validation
        var response = (PlaylistItems)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(PlaylistItems));
        _trackID.Should().Contain(response.Items[0].Track.Id);
    }

    [Test, TestCaseSource(nameof(InvalidPlaylistAndTrackIDs))]
    [Description("Add Items To Playlist using Wrong Track ID or Playlist ID")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void AddItemsToPlaylist_InvalidTrackID(string id,string trackIDs)
    {
        var requestBody = new AddTracksRequest(new[] { trackIDs }, 0);
        var response = Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Post($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .Extract()
            .Response();

        //Validation
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.NotFound);
    }

    [Test]
    [Description("Remove Playlist Items")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void RemovePlaylistItems_ValidID()
    {
        AddItemsToPlaylist_ValidTrackID();

        var requestBody = new RemoveRequest(new[] { new RemoveTrackRequest(_trackID) });

        Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Delete($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK);

        //Validation
        var response = (PlaylistItems)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/tracks")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(PlaylistItems));

        foreach (var item in response.Items)
        {
            _trackID.Should().NotContain(item.Track.Id);
        }
    }

    [Test, TestCaseSource(nameof(InvalidPlaylistAndTrackIDs))]
    [Description("Remove Playlist Items using Wrong IDS")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void RemovePlaylistItems_InvalidID(string playlistID, string trackID)
    {
        var requestBody = new RemoveRequest(new[] { new RemoveTrackRequest(_trackID) });
        var response =  Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Delete($"/playlists/{playlistID}/tracks")
            .Then()
            .Extract()
            .Response();
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.NotFound);
    }

    [Test, TestCaseSource(nameof(ValidNewPlaylistName))]
    [Description("Create blank Playlist")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void CreatePlaylist_Blank(string nameForPlaylist)
    {
        var requestBody = new PlaylistInfo(nameForPlaylist, string.Empty, false);
        var response = (SpotifyPlaylist)Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Post($"/users/{RestClientUtil.User_id}/playlists")
            .Then()
            .StatusCode(HttpStatusCode.Created)
            .DeserializeTo(typeof(SpotifyPlaylist));

        //Validation
        response.Name.Should().Be(nameForPlaylist);

        //Clean Up
        Given()
            .Spec(requestSpecification)
            .When()
            .Delete($"/playlists/{response.Id}/followers")
            .Then()
            .StatusCode(HttpStatusCode.OK);
    }

    [Test, TestCaseSource(nameof(ValidNewPlaylistDetails))]
    [Description("Create Playlist with Custom name and description")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void CreatePlaylist_WithData(string nameForPlaylist, string descriptionForPlaylist, bool visibility)
    {
        var requestBody = new PlaylistInfo(nameForPlaylist, descriptionForPlaylist, visibility);
        var response = (SpotifyPlaylist)Given()
            .Spec(requestSpecification)
            .Body(requestBody)
            .When()
            .Post($"/users/{RestClientUtil.User_id}/playlists")
            .Then()
            .StatusCode(HttpStatusCode.Created)
            .DeserializeTo(typeof(SpotifyPlaylist));

        //Validation
        response.Name.Should().Be(nameForPlaylist);
        response.Description.Should().Be(descriptionForPlaylist);

        //Clean Up
        Given()
            .Spec(requestSpecification)
            .When()
            .Delete($"/playlists/{response.Id}/followers")
            .Then()
            .StatusCode(HttpStatusCode.OK);
    }

    [Test]
    [Description("Get Cover Image of Playlist")]
    [Category("Smoke")]
    [Property("Priority", "P3")]
    public void GetCoverImage()
    {
        var response = (List<Image>)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/images")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(List<Image>));
        
        foreach(var image in response)
        {
            image.Url.Should().Contain("http");
        }
    }

    [Test]
    [Description("Add custom Cover Image for Playlist")]
    [Category("Smoke")]
    [Property("Priority", "P3")]
    public void AddCustomCoverImage()
    {
        string base64Encoded = Convert.ToBase64String(System.IO.File.ReadAllBytes("Configurations/Images/imagecover.jpg"));

        Given()
            .Spec(requestSpecification)
            .ContentType("image/jpeg")
            .Body(base64Encoded)
            .When()
            .Put($"/playlists/{_basePlaylistID}/images")
            .Then()
            .StatusCode(HttpStatusCode.Accepted);

        //Validation
        var actualResult = (List<Image>)Given()
            .Spec(requestSpecification)
            .When()
            .Get($"/playlists/{_basePlaylistID}/images")
            .Then()
            .StatusCode(HttpStatusCode.OK)
            .DeserializeTo(typeof(List<Image>));
        actualResult.Should().NotBeNull();
    }
}
