namespace RaceRetreat.DemoClient.Domain;

internal class GameState
{
    public string MapName { get; set; }
    public int Rounds { get; set; }
    public int CurrentRound { get; set; }

    public RaceMap Map { get; set; }
    public List<ActionLog> ActionLog { get; set; }
}