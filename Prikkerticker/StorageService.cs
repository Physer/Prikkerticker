using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using System;

namespace Prikkerticker;

public class StorageService
{
    private readonly TableClient _tableClient;

    public StorageService(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AzureTableStorageConnectionString");
        var tableName = "years";
        _tableClient = new TableClient(connectionString, tableName);
        _tableClient.CreateIfNotExists();
    }

    public string GetLatestStoredYear() => throw new NotImplementedException();

    public void StoreYearNotification(YearNotification yearNotification) => throw new NotImplementedException();
}
