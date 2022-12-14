using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Blazor.Hubs;
using RaceRetreat.Contracts;
using System.Diagnostics;

namespace RaceRetreat.Blazor.Runners;

public class GameRunner
{
    private readonly GameHub _gameHub;
    private readonly LevelsHelper _levelsHelper;
    private readonly ILogger<GameRunner> _logger;

    private CurrentMap _currentMap = CurrentMap.Map0;
    private MapRunner? _activeMapRunner = null;
    private MapState? _lastMapState = null;

    public MapState? CurrentMapState => _lastMapState;

    public GameRunner(
        GameHub gameHub,
        LevelsHelper levelsHelper,
        ILogger<GameRunner> logger)
    {
        _gameHub = gameHub;
        _levelsHelper = levelsHelper;
        _logger = logger;
    }

    public async Task Tick()
    {
        var sw = Stopwatch.StartNew();

        // If no map is active, just wait for a new map to be set.
        if (_activeMapRunner == null)
        {
            _logger.LogInformation("No map is active! Waiting for 1000ms...");

            await Task.Delay(1000);
            return;
        }

        // Make the active map tick.
        _lastMapState = _activeMapRunner.Tick();

        // Send the new gamestate to the SignalR hub.
        await _gameHub.SendGameState(new GameState
        {
            MapName = _lastMapState.MapName,
            Rounds = _lastMapState.Rounds,
            CurrentRound = _lastMapState.CurrentRound
        });

        _logger.LogInformation($"Running map '{_lastMapState.MapName}', round {_lastMapState.CurrentRound}/{_lastMapState.Rounds}");

        // Calculate remaining time to sleep.
        var delay = _lastMapState.TimePerRound - (int)sw.ElapsedMilliseconds;
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
        _activeMapRunner = new MapRunner(map.Map, map.MapName);
    }
}