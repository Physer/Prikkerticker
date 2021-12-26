namespace Prikkerticker;

public static class Constants
{
    public static class ConfigurationKeys
    {
        public const string TwitterApiBaseUrl = nameof(TwitterApiBaseUrl);
        public const string TwitterApiKey = nameof(TwitterApiKey);
        public const string TwitterApiSecret = nameof(TwitterApiSecret);
        public const string TwitterBearerToken = nameof(TwitterBearerToken);
        public const string TwitterUsernameToScrape = nameof(TwitterUsernameToScrape);
    }

    public static class ConnectionStringKeys
    {
        public const string PrikkerTickerAzureTableApi = nameof(PrikkerTickerAzureTableApi);
    }

    public static class AzureTableApi
    {
        public const string TableName = "yearnotifications";
    }
}
