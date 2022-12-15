using RaceRetreat.Domain;

namespace RaceRetreat.Contracts;

public record MapState
{
    public string MapName { get; init; }
    public int Rounds { get; init; }
    public int TimePerRound { get; init; }
    public int CurrentRound { get; init; }

    public List<Player> Players { get; init; }
    public List<Play> Plays { get; init; }

    
    public RaceMap Map { get; init; }
}