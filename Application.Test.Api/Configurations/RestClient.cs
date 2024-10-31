namespace Application.Api.Configuration;

using Common.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

public static class RestClientUtil
{
    private static RestClient client;
    public static RestClient Client => client;

    static readonly AppConfigAPI AppConfig = ConfigurationManager.AppConfigApi;

    private static string baseUrl = AppConfig.Url.Base;
    private static string TokenUrl = AppConfig.Url.Token;
    private static string clientID = AppConfig.EnvironmentVariables.ClientID;
    private static string clientSecret = AppConfig.EnvironmentVariables.ClientSecret;
    private static string _refreshToken = AppConfig.EnvironmentVariables.RefreshToken;
    private static string _accessToken;
    private static string user_id;
    public static string BaseUrl => baseUrl;
    public static string AccessToken => _accessToken;
    public static string User_id => user_id;
    public static void InitializeClient()
    {
        RefreshToken();
        client = new RestClient(BaseUrl + "/v1");
        GetUser();
    }
    public static RestResponse ExecuteRequest(string addURL, Method method, Dictionary<string, string>? headers = null, params string[] body)
    {
        var request = new RestRequest(addURL, method);
        request.AddHeader("Authorization", "Bearer " + _accessToken);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        return client.Execute(request); 
    }

    private static void RefreshToken()
    {
        var _client = new RestClient(TokenUrl);
        var request = new RestRequest("/token",Method.Post);

        var credentials = $"{clientID}:{clientSecret}";
        var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Authorization", "Basic " + base64Credentials);
        request.AddParameter("grant_type", "refresh_token");
        request.AddParameter("refresh_token", _refreshToken);

        var response = _client.Execute(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
            _accessToken = tokenResponse.access_token;
        }
        else
        {
            throw new Exception($"Error fetching token: {response.Content}");
        }
    }
    
    public static string GetUser()
    {
        var response = ExecuteRequest("/me", Method.Get);
        var user = JsonConvert.DeserializeObject<UserProfile>(response.Content);
        user_id = user.id;
        return user_id;
    }
}
