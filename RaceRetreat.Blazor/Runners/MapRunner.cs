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

    public MapState Tick()
    {
        _currentRound++;

        if (_currentRound > _map.Rounds)
        {
            // TODO: RESET
            _currentRound = 1;
        }

        return new MapState
        {
            MapName = _mapName,
            Rounds = _map.Rounds,
            TimePerRound = _map.TimePerRound,
            CurrentRound = _currentRound
        };
    }
}