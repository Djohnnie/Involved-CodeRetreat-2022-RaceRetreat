using RaceRetreat.Contracts;
using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Runners;

public class MapRunner
{
    private RaceMap _map;
    private string _mapName;

    private int _currentRound = 0;

    public MapRunner(RaceMap map, string mapName)
    {
        _map = map;
        _mapName = mapName;
    }

    public MapState Tick(List<IRaceAction> tickActions)
    {
        _currentRound++;

        if (_currentRound > _map.Rounds)
        {
            // TODO: RESET
            _currentRound = 1;
        }
        else
        {
            HandleActions(tickActions);
        }

        return new MapState
        {
            MapName = _mapName,
            Rounds = _map.Rounds,
            TimePerRound = _map.TimePerRound,
            CurrentRound = _currentRound
        };
    }

    /// <summary>
    /// First attack options because attack disables the other player completely
    /// Then move/mine actions in the order given to allow a player to mine and move through the tile in one tick
    /// Last place actions, these only cound in the next tick
    /// </summary>
    /// <param name="tickActions"></param>
    private void HandleActions(List<IRaceAction> tickActions)
    {
        //firstAttackActions
        var attackActions = tickActions.Where(x => x.GetType() == typeof(AttackPlayerAction)).ToList();
        attackActions.ForEach(x => x.ExecuteAction(_map));

        //ThenMoveMineActions
        var moveMineActions = tickActions
            .Where(x => x.GetType() == typeof(MoveAction) || x.GetType() == typeof(MineRockAction)).ToList();
        moveMineActions.ForEach(x => x.ExecuteAction(_map));

        //ThenOilRockActions
        var oilRockActions = tickActions
            .Where(x => x.GetType() == typeof(PlaceOilAction) || x.GetType() == typeof(PlaceRockAction)).ToList();
        oilRockActions.ForEach(x => x.ExecuteAction(_map));
    }
}