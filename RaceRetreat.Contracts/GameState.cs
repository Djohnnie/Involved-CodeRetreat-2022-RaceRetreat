using RaceRetreat.Domain;

namespace RaceRetreat.Contracts;

public class GameState
{
    public string MapName { get; set; }
    public string PlayerName { get; set; }
    public int Rounds { get; set; }
    public int CurrentRound { get; set; }

    public RaceMap Map { get; set; }
}