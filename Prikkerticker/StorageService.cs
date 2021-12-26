using Azure.Data.Tables;
using Prikkerticker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prikkerticker;

public class StorageService
{
    private readonly TableClient _tableClient;

    private const string _yearsKey = "Years";

    public StorageService(TableClient tableClient)
    {
        tableClient.CreateIfNotExists();
        _tableClient = tableClient;
    }

    public IEnumerable<YearNotification> GetLatestYearNotification() => throw new NotImplementedException();

    public async Task StoreYearNotificationAsync(YearNotification? yearNotification)
    {
        if (yearNotification is null)
            return;

        TableEntity tableEntity = new();
        tableEntity.PartitionKey = yearNotification.UnixMilliseconds.ToString();
        tableEntity.RowKey = yearNotification.Tweet.Id;
        tableEntity[_yearsKey] = yearNotification.CommaSeparatedYears;

        await _tableClient.UpsertEntityAsync(tableEntity);
    }

    private YearNotification MapTableEntityToYearNotification(TableEntity entity) => throw new NotImplementedException();
}
