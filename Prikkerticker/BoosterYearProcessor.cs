using Microsoft.Extensions.Configuration;
using Prikkerticker.Models;
using Prikkerticker.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prikkerticker;

public class BoosterYearProcessor
{
    private readonly HttpClient _twitterClient;
    private readonly string _twitterUsername;

    public BoosterYearProcessor(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        var twitterBaseUrl = configuration[Constants.ConfigurationKeys.TwitterApiBaseUrl];
        var twitterClient = httpClientFactory.CreateClient();
        twitterClient.BaseAddress = new Uri(twitterBaseUrl);
        twitterClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration[Constants.ConfigurationKeys.TwitterBearerToken]);

        _twitterClient = twitterClient;
        _twitterUsername = configuration[Constants.ConfigurationKeys.TwitterUsernameToScrape];
    }

    public async Task<YearNotificationResponse> ProcessBoosterYearAsync()
    {
        var response = await _twitterClient.GetStringAsync($"tweets/search/recent?query=-is:reply from:{_twitterUsername}&tweet.fields=created_at");
        if (string.IsNullOrWhiteSpace(response))
            return new();

        var twitterData = JsonSerializer.Deserialize<TwitterData>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (twitterData is null || twitterData?.Data?.Any() == false)
            return new();

        var boosterYearNotification = GetLatestBoosterTweet(twitterData?.Data);
        if (boosterYearNotification is null)
            return new();

        return new(boosterYearNotification);
    }

    private YearNotification? GetLatestBoosterTweet(IEnumerable<Tweet>? tweets)
    {
        if (tweets is null)
            return null;

        foreach (var tweet in tweets)
        {
            if (string.IsNullOrWhiteSpace(tweet?.Text))
                continue;

            if (!tweet.Text.Contains("oppepprik"))
                continue;

            var boosterTweetsMatches = Regex.Matches(tweet.Text, "\\d{4}").ToList();
            List<int> years = new();
            foreach (var boosterTweetMatch in boosterTweetsMatches)
            {
                if (boosterTweetMatch?.Success == false)
                    continue;

                if(int.TryParse(boosterTweetMatch!.Value, out var year))
                    years.Add(year);
            }
            return new(years, tweet);
        }
        return null;
    }
}
