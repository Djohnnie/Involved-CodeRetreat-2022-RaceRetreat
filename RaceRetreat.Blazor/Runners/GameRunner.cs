using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Runners;

public class GameRunner
{
    private readonly LevelsHelper _levelsHelper;
    private readonly PlaysHelper _playsHelper;
    private readonly ILogger<GameRunner> _logger;

    private CurrentMap _currentMap = CurrentMap.Map0;
    private MapRunner? _activeMapRunner = null;
    private MapState? _lastMapState = null;
    private List<IRaceAction> PlayerActions { get; set; }

    public MapState? CurrentMapState => _lastMapState;


    public GameRunner(
        LevelsHelper levelsHelper,
        PlaysHelper playsHelper,
        ILogger<GameRunner> logger)
    {
        _levelsHelper = levelsHelper;
        _playsHelper = playsHelper;
        _logger = logger;
        PlayerActions = new List<IRaceAction>();
    }

    public async Task<MapState?> Tick()
    {

        // If no map is active, just wait for a new map to be set.
        if (_activeMapRunner == null)
        {
            _logger.LogInformation("No map is active! Waiting for 1000ms...");

            await Task.Delay(1000);
            return null;
        }

        var tickActions = await CalculateActionsToProcess();
        // Make the active map tick.
        _lastMapState =  _activeMapRunner.Tick(tickActions);

        //Clear PlayerActions
        PlayerActions.Clear();

        _logger.LogInformation($"Running map '{_lastMapState.MapName}', round {_lastMapState.CurrentRound}/{_lastMapState.Rounds}");

        return _lastMapState;
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

        List<Player> players = await _playsHelper.GetPlayers();
        var map = await _levelsHelper.GetMapByName(mapName);
        _activeMapRunner = new MapRunner(map.Map, map.MapName, players);
        _activeMapRunner.SetupMap();
    }

    public async Task AddAction(IRaceAction action)
    {
        PlayerActions.Add(action);

        //Just so resharper will shut up (=
        await Task.CompletedTask;
    }

    private async Task<List<IRaceAction>> CalculateActionsToProcess()
    {
        var newList = new List<IRaceAction>();
        var playerActionsDict = new Dictionary<string, List<IRaceAction>>();

        foreach (var playerAction in PlayerActions)
        {
            if(!playerActionsDict.ContainsKey(playerAction.PlayerName))
                playerActionsDict.Add(playerAction.PlayerName, new List<IRaceAction>());

            playerActionsDict[playerAction.PlayerName].Add(playerAction);
        }

        foreach (var playerActions in playerActionsDict)
        {
            var playerActionValues = playerActions.Value;
            var isOiled = _activeMapRunner.IsPlayerOiled(playerActions.Key);

            newList.AddRange(isOiled ? playerActionValues.TakeLast(1) : playerActionValues.TakeLast(2));
        }

        return newList;
    }

}