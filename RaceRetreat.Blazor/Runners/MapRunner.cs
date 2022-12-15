using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;
using RaceRetreat.Domain.Actions;

namespace RaceRetreat.Blazor.Runners;

public class MapRunner
{
    private readonly RaceMap _map;
    private readonly string _mapName;
    private readonly List<Player> _players;
    private readonly ConfigurationHelper _configurationHelper;
    private List<Play> _plays;

    private int _currentRound = 0;

    public MapRunner(RaceMap map, string mapName, List<Player> players, ConfigurationHelper configurationHelper)
    {
        _map = map;
        _mapName = mapName;
        _players = players;
        _configurationHelper = configurationHelper;
    }

    public async Task SetupMap()
    {
        var startTile = _map.Tiles.FirstOrDefault(x => x.IsStart);

        if (startTile == null)
        {
            throw new ArgumentNullException($"No Start Tile!");
        }

        _map.Tiles.ForEach(x => x.Players.Clear());

        var configuration = await _configurationHelper.Refresh();

        _players.ForEach(x => x.Points = configuration.DefaultPoints);
        startTile.Players = _players.ToList();

        _plays = new List<Play>();

        foreach (var player in _players)
        {
            player.OilRemaining = _map.OilPerPlayer;
            player.RocksRemaining = _map.RocksPerPlayer;
            _plays.Add(new Play
            {
                PlayerName = player.PlayerName,
                Index = player.Index,
                Steps = new List<Step>
                {
                    new Step
                    {
                        X = startTile.X,
                        Y = startTile.Y,
                    }
                }
            });
        }
    }

    public async Task<MapState> Tick(List<IRaceAction> tickActions)
    {
        _currentRound++;

        if (_currentRound > _map.Rounds)
        {
            await SetupMap();

            _currentRound = 0;
        }
        else
        {
            var configuration = await _configurationHelper.Refresh();
            HandleActions(tickActions, configuration);

            foreach (var tile in _map.Tiles)
            {
                foreach (var player in tile.Players)
                {
                    _plays.Single(x => x.Index == player.Index).Steps.Add(new Step
                    {
                        X = tile.X,
                        Y = tile.Y,
                    });
                }
            }
        }

        return new MapState
        {
            MapName = _mapName,
            Rounds = _map.Rounds,
            TimePerRound = _map.TimePerRound,
            CurrentRound = _currentRound,
            Players = _players,
            Plays = _plays,
            Map = _map
        };
    }

    public bool IsPlayerOiled(string playerName)
    {
        var player = _map.FetchPlayer(playerName);
        return player.OilTicksRemaining > 0;
    }

    /// <summary>
    /// First attack options because attack disables the other player completely
    /// Then move/mine actions in the order given to allow a player to mine and move through the tile in one tick
    /// Last place actions, these only cound in the next tick
    /// </summary>
    /// <param name="tickActions"></param>
    private void HandleActions(List<IRaceAction> tickActions, Configuration configuration)
    {
        //firstAttackActions
        var attackActions = tickActions.Where(x => x.GetType() == typeof(AttackPlayerAction)).ToList();
        attackActions.ForEach(x => x.ExecuteAction(_map, configuration));

        //ThenMoveMineActions
        var moveMineActions = tickActions
            .Where(x => x.GetType() == typeof(MoveAction) || x.GetType() == typeof(MineRockAction)).ToList();
        moveMineActions.ForEach(x => x.ExecuteAction(_map, configuration));

        //ThenOilRockActions
        var oilRockActions = tickActions
            .Where(x => x.GetType() == typeof(PlaceOilAction) || x.GetType() == typeof(PlaceRockAction)).ToList();
        oilRockActions.ForEach(x => x.ExecuteAction(_map, configuration));
    }
}