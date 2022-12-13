using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Blazor.Hubs;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;
using System.Diagnostics;

namespace RaceRetreat.Blazor.Runners;

public class GameRunner
{
    private readonly GameHub _gameHub;
    private readonly LevelsHelper _levelsHelper;

    private CurrentMap _currentMap = CurrentMap.Map0;
    private RaceMap? _activeMap = null;

    public GameRunner(
        GameHub gameHub,
        LevelsHelper levelsHelper)
    {
        _gameHub = gameHub;
        _levelsHelper = levelsHelper;
    }

    public async Task Tick()
    {
        var sw = Stopwatch.StartNew();

        if (_activeMap == null)
        {
            await Task.Delay(1000);
            return;
        }

        await _gameHub.SendGameState(new GameState
        {
            MapName = _currentMap.ToString(),
            Round = (int)sw.ElapsedMilliseconds
        });

        var delay = _activeMap.TimePerRound - (int)sw.ElapsedMilliseconds;
        await Task.Delay(delay < 0 ? 0 : delay);
    }

    public async Task SetActiveMap(string mapName)
    {
        switch (mapName)
        {
            case "map-1":
                _currentMap = CurrentMap.Map1;
                break;
            case "map-2":
                _currentMap = CurrentMap.Map2;
                break;
            case "map-3":
                _currentMap = CurrentMap.Map3;
                break;
            case "map-4":
                _currentMap = CurrentMap.Map4;
                break;
            case "map-5":
                _currentMap = CurrentMap.Map5;
                break;
            default:
                _currentMap = CurrentMap.Map0;
                break;
        }

        var map = await _levelsHelper.GetMapByName(mapName);
        _activeMap = map.Map;
    }
}