using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Prikkerticker;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices((context, services) =>
            {
                var azureTablesConnectionString = context.Configuration.GetConnectionString(Constants.ConnectionStringKeys.PrikkerTickerAzureTableApi);
                if (string.IsNullOrWhiteSpace(azureTablesConnectionString))
                    throw new ArgumentException("Invalid Azure Tables connection string");

                // Infrastructural services
                services.AddHttpClient();
                services.AddSingleton(new TableClient(azureTablesConnectionString, Constants.AzureTableApi.TableName));

                // Application services
                services.AddScoped<BoosterYearProcessor>();
            })
            .Build();

        host.Run();
    }
}
