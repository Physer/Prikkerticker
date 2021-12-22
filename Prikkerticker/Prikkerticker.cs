using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Prikkerticker;

public class Prikkerticker
{
    private readonly ILogger _logger;

    public Prikkerticker(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Prikkerticker>();
    }

    [Function(nameof(Prikkerticker))]
    public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow:yyyy-MM-dd HH:mm} (UTC)");
        _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
    }
}
