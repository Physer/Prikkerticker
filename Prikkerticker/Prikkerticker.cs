using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Prikkerticker;

public class Prikkerticker
{
    private readonly ILogger _logger;
    private readonly BoosterYearProcessor _yearNotificationScraper;
    private readonly StorageService _storageService;

    public Prikkerticker(ILoggerFactory loggerFactory,
        BoosterYearProcessor yearNotificationScraper, 
        StorageService storageService)
    {
        _logger = loggerFactory.CreateLogger<Prikkerticker>();
        _yearNotificationScraper = yearNotificationScraper;
        _storageService = storageService;
    }

    [Function(nameof(Prikkerticker))]
    public async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        var boosterYearResponse = await _yearNotificationScraper.ProcessBoosterYearAsync();
        if (boosterYearResponse?.Success == false)
        {
            _logger.LogWarning($"Booster tweet unsuccesfully scraped!");
            return;
        }

        await _storageService!.StoreYearNotificationAsync(boosterYearResponse!.YearNotification);
    }
}
