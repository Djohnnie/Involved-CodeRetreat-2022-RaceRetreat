using RaceRetreat.Contracts;
using RaceRetreat.Domain;
using System.Text.Json;

namespace RaceRetreat.Blazor.Helpers;

public class LevelsHelper
{
    public Task<GetLevelByNameResponse> GetMapByName(string mapName)
    {
        var mapResourceName = BuildMapResourceName(mapName);
        var mapJson = EmbeddedResourceHelper.GetMapByResourceName(mapResourceName);
        var map = JsonSerializer.Deserialize<RaceMap>(mapJson);
        map.Tiles.RemoveAll(x => !x.IsUsed);

        return Task.FromResult(new GetLevelByNameResponse
        {
            MapName = mapName,
            Map = map
        });
    }

    private string BuildMapResourceName(string mapName)
    {
        return $"RaceRetreat.Blazor.Maps.{mapName}.racejson";
    }
}