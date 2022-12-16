using RaceRetreat.Domain;

namespace RaceRetreat.Contracts;

public class GameState
{
    public string MapName { get; set; }
    public int Rounds { get; set; }
    public int CurrentRound { get; set; }

    public RaceMap Map { get; set; }
    public List<ActionLog> ActionLog { get; set; }
}