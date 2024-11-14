namespace Application.Api.Configuration;

using Application.Api.Configurations;
using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

public static class RestClientUtil
{
    static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    private static readonly ThreadLocal<Common.Logger.ILogger> Logger = new();
    private static Common.Logger.ILogger Report {get => Logger.Value!; }
    private static RestClient client = new();
    public static RestClient Client => client;
    private static string baseUrl = AppConfig.Url.API_Base;
    private static string _accessToken = "";
    private static string user_id = "";
    public static string BaseUrl => baseUrl;
    public static string AccessToken => _accessToken;
    public static string User_id => user_id;
    public static void ConfigureLogger(Common.Logger.ILogger logger) => Logger.Value = logger;
    public static void InitializeClient()
    {
        RefreshToken();
        client = new RestClient(BaseUrl + "/v1");
        GetUser();
    }

    private static RestResponse ExecuteRequest(string addURL, Method method, Dictionary<string, string>? headers = null, Dictionary<string, List<string>>? queryParams = null, object? body = null)
    {
        Report.LOG_INFO($"RestClientUtil.ExecuteRequest(addURL: {addURL}, method: {method})");

        var request = new RestRequest(addURL, method);
        request.AddHeader("Authorization", "Bearer " + _accessToken);
        if (headers != null)
        {
            request.AddHeaders(headers);
        }
        if (body != null)
        {
            request.AddJsonBody(body);
        }
        if (queryParams != null)
        {
            foreach (var param in queryParams)
            {
                foreach (var value in param.Value)
                {
                    request.AddQueryParameter(param.Key, value);
                }
            }
        }

        var res = client.Execute(request);

        Report.LOG_INFO($"RestClientUtil.ExecuteRequest Result: {res.StatusCode}");

        return res; 
    }

    public static RestResponse PostRequest(string addURL, Dictionary<string, List<string>>? queryParams = null, object? body = null)
    {
        var headers = new Dictionary<string, string>() { {"Content-Type", "application/json"} };

        return ExecuteRequest(addURL, Method.Post, headers, queryParams, body);
    }

    public static RestResponse DeleteRequest(string addURL)
    {
        return ExecuteRequest(addURL, Method.Delete);
    }

    public static RestResponse GetRequest(string addURL)
    {
        return ExecuteRequest(addURL, Method.Get);
    }

    private static void RefreshToken()
    {
        var _client = new RestClient(AppConfig.Url.API_Token);
        var request = new RestRequest("/token",Method.Post);

        var credentials = $"{AppConfig.EnvironmentVariables.API_ClientID}:{AppConfig.EnvironmentVariables.API_ClientSecret}";
        var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Authorization", "Basic " + base64Credentials);
        request.AddParameter("grant_type", "refresh_token");
        request.AddParameter("refresh_token", AppConfig.EnvironmentVariables.API_RefreshToken);

        var response = _client.Execute(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content!);
            _accessToken = tokenResponse!.Access_token;

            Report.LOG_INFO($"RestClientUtil.RefreshToken() {(_accessToken.Length == 0 ? "FAIL" : "OK")}");
        }
        else
        {
            throw new Exception($"Error fetching token: {response.Content}");
        }
    }
    
    public static string GetUser()
    {
        var response = GetRequest("/me");
        var user = JsonConvert.DeserializeObject<UserProfile>(response.Content!);
        user_id = user!.Id;
        return user_id;
    }
}
