using Azure;
using Azure.Data.Tables;

namespace RaceRetreat.Blazor.Helpers;

public class AzureTableHelper
{
    private readonly TableServiceClient _tableServiceClient;
    private readonly TableClient _playerTableClient;
    private readonly TableClient _configurationTableClient;

    public AzureTableHelper()
    {
        _tableServiceClient = new TableServiceClient("DefaultEndpointsProtocol=https;AccountName=raceretreatstorage;AccountKey=mnb4zGr8LOp8966gIixyVJ96NGxdg8zsYrTsJCldw7nNzMbEDx02jngXPe3V2YCOqSurAPcxFLPA+AStu2ajfA==;EndpointSuffix=core.windows.net");
        _playerTableClient = _tableServiceClient.GetTableClient("players");
        _ = _playerTableClient.CreateIfNotExistsAsync();
        _configurationTableClient = _tableServiceClient.GetTableClient("configuration");
        _ = _configurationTableClient.CreateIfNotExistsAsync();
    }

    public async Task AddPlayer(string playerName)
    {
        var playerEntity = new PlayerEntity
        {
            PartitionKey = "players",
            RowKey = playerName
        };

        await _playerTableClient.AddEntityAsync(playerEntity);
    }

    public async IAsyncEnumerable<PlayerEntity> GetPlayers()
    {
        await foreach (var page in _playerTableClient.QueryAsync<PlayerEntity>().AsPages())
        {
            foreach (var player in page.Values)
            {
                yield return player;
            }
        }
    }

    public async Task<int> GetConfiguration(string key)
    {
        var result = await _configurationTableClient.GetEntityAsync<ConfigurationEntity>("configuration", key);
        return result.Value?.Value ?? 0;
    }
}

public class PlayerEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}

public class ConfigurationEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string Value { get; set; }
}