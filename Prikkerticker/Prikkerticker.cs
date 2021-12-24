using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Prikkerticker;

public class Prikkerticker
{
    private readonly ILogger _logger;
    private readonly YearNotificationScraper _yearNotificationScraper;

    public Prikkerticker(ILoggerFactory loggerFactory, 
        YearNotificationScraper yearNotificationScraper)
    {
        _logger = loggerFactory.CreateLogger<Prikkerticker>();
        _yearNotificationScraper = yearNotificationScraper;
    }

    [Function(nameof(Prikkerticker))]
    public async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow:yyyy-MM-dd HH:mm} (UTC)");
        await _yearNotificationScraper.ScrapeBoosterYearAsync();
    }
}
