using System.Net;
using Application.Api.Configurations;
using Application.Api.TestDataCreator.Models;
using NHamcrest.Core;

namespace Application.Api.TestDataCreator;

public class TracksTestData : ITracksTestData
{
    private readonly string[] tracksList =
    {
        "spotify:track:7qiZfU4dY1lWllzX7mPBI3", //Shape of You
        "spotify:track:3z8h0TU7ReDPLIbEnYhWZb", //Bohemian Rhapsody
        "spotify:track:7J1uxwnxfQLu4APicE5Rnj", //Billie Jean
        "spotify:track:0pqnGHJpmpxLKifKRmU6WP", //Believer
        "spotify:track:3AhXZa8sUQht0UEdBJgpGc", //Like a Rolling Stone
        "spotify:track:4CeeEOM32jQcH3eN9Q2dGj", //Smells Like Teen Spirit
        "spotify:track:561jH07mF1jHuk7KlaeF0s", //Mockingbird
        "spotify:track:2LlQb7Uoj1kKyGhlkBf9aC", //Thriller
        "spotify:track:65YsalQBmQCzIPaay72CzQ", //Diamonds
        "spotify:track:54ipXppHLA8U4yqpOFTUhr", //Bones
    };

    public string[] GetRandomTracks(int numberOfTracks)
    {
        Random random = new Random();
        return tracksList
            .OrderBy(x => random.Next())
            .Take(numberOfTracks)
            .ToArray();
    }
}
