using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Prikkerticker;

public class YearNotificationScraper
{
    private readonly HttpClient _twitterClient;
    private readonly string _twitterUsername;

    public YearNotificationScraper(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        var twitterBaseUrl = configuration[Constants.ConfigurationKeys.TwitterApiBaseUrl];
        var twitterClient = httpClientFactory.CreateClient();
        twitterClient.BaseAddress = new Uri(twitterBaseUrl);
        twitterClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration[Constants.ConfigurationKeys.TwitterBearerToken]);

        _twitterClient = twitterClient;
        _twitterUsername = configuration[Constants.ConfigurationKeys.TwitterUsernameToScrape];
    }

    public async Task ScrapeBoosterYearAsync()
    {
        var response = await _twitterClient.GetStringAsync($"tweets/search/recent?query=-is:reply from:{_twitterUsername}&tweet.fields=created_at");
    }
}
