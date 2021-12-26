using Prikkerticker.Models.Twitter;
using System;
using System.Collections.Generic;

namespace Prikkerticker.Models;

public class YearNotification
{
    public YearNotification(IEnumerable<int> years, Tweet tweet)
    {
        Years = years;
        Tweet = tweet;
    }

    public IEnumerable<int> Years { get; init; }
    public Tweet Tweet { get; init; }
    public bool Sent { get; set; }
    public long UnixMilliseconds => new DateTimeOffset(Tweet.CreatedAt).ToUnixTimeMilliseconds();
    public string CommaSeparatedYears => string.Join(',', Years);

}
