using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prikkerticker;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddScoped<YearNotificationScraper>();
            })
            .Build();

        host.Run();
    }
}
