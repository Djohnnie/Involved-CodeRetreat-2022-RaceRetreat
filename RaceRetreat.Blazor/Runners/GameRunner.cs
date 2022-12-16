using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Runners;

public class GameRunner
{
    private readonly LevelsHelper _levelsHelper;
    private readonly PlaysHelper _playsHelper;
    private readonly ILogger<GameRunner> _logger;
    private readonly ActionLogHelper _actionLogHelper;
    private readonly ConfigurationHelper _configurationHelper;
    private MapRunner? _activeMapRunner = null;
    private MapState? _lastMapState = null;
    private List<IRaceAction> PlayerActions = new();

    public MapState? CurrentMapState => _lastMapState;


    public GameRunner(
        LevelsHelper levelsHelper,
        PlaysHelper playsHelper,
        ILogger<GameRunner> logger,
        ActionLogHelper actionLogHelper,
        ConfigurationHelper configurationHelper)
    {
        _levelsHelper = levelsHelper;
        _playsHelper = playsHelper;
        _logger = logger;
        _actionLogHelper = actionLogHelper;
        _configurationHelper = configurationHelper;
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
        _lastMapState = await _activeMapRunner.Tick(tickActions);

        // Clear PlayerActions
        PlayerActions.Clear();

        _logger.LogInformation($"Running map '{_lastMapState.MapName}', round {_lastMapState.CurrentRound}/{_lastMapState.Rounds}");

        return _lastMapState;
    }

    public async Task SetActiveMap(string mapName)
    {
        List<Player> players = await _playsHelper.GetPlayers();
        var map = await _levelsHelper.GetMapByName(mapName);
        _activeMapRunner = new MapRunner(map.Map, map.MapName, players, _actionLogHelper, _configurationHelper);

        _actionLogHelper.Log($"Active map set to '{mapName}'");

        await _activeMapRunner.SetupMap();
    }

    public Task AddAction(IRaceAction action)
    {
        PlayerActions.Add(action);

        return Task.CompletedTask;
    }

    private async Task<List<IRaceAction>> CalculateActionsToProcess()
    {
        var newList = new List<IRaceAction>();
        var playerActionsDict = new Dictionary<string, List<IRaceAction>>();

        foreach (var playerAction in PlayerActions)
        {
            if (!playerActionsDict.ContainsKey(playerAction.PlayerName))
                playerActionsDict.Add(playerAction.PlayerName, new List<IRaceAction>());

            playerActionsDict[playerAction.PlayerName].Add(playerAction);
        }

        foreach (var playerActions in playerActionsDict)
        {
            var playerActionValues = playerActions.Value;
            var isOiled = _activeMapRunner.IsPlayerOiled(playerActions.Key);

            if (!isOiled && playerActionValues.Count > 2)
            {
                _actionLogHelper.Log($"{playerActions.Key} has sent too many actions! Only the last two will be processed.");
            }

            if (isOiled && playerActionValues.Count > 1)
            {
                _actionLogHelper.Log($"{playerActions.Key} has sent too many actions! Only the last will be processed because {playerActions.Key} is oiled.");
            }

            newList.AddRange(isOiled ? playerActionValues.TakeLast(1) : playerActionValues.TakeLast(2));
        }

        return newList;
    }

}