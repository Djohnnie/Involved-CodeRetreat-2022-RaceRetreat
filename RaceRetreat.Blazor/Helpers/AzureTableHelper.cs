using Azure;
using Azure.Data.Tables;

namespace RaceRetreat.Blazor.Helpers;

public class AzureTableHelper
{
    private readonly TableServiceClient _tableServiceClient;
    private readonly TableClient _tableClient;

    public AzureTableHelper()
    {
        _tableServiceClient = new TableServiceClient("DefaultEndpointsProtocol=https;AccountName=raceretreatstorage;AccountKey=mnb4zGr8LOp8966gIixyVJ96NGxdg8zsYrTsJCldw7nNzMbEDx02jngXPe3V2YCOqSurAPcxFLPA+AStu2ajfA==;EndpointSuffix=core.windows.net");
        _tableClient = _tableServiceClient.GetTableClient("players");
        _ = _tableClient.CreateIfNotExistsAsync();
    }

    public async Task AddPlayer(string playerName)
    {
        var playerEntity = new PlayerEntity
        {
            PartitionKey = "players",
            RowKey = playerName
        };

        await _tableClient.AddEntityAsync(playerEntity);
    }

    public async IAsyncEnumerable<PlayerEntity> GetPlayers()
    {
        await foreach (var page in _tableClient.QueryAsync<PlayerEntity>().AsPages())
        {
            foreach (var player in page.Values)
            {
                yield return player;
            }
        }
    }
}

public class PlayerEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}