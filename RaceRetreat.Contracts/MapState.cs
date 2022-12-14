namespace RaceRetreat.Contracts;

public class MapState
{
    public string MapName { get; set; }
    public int Rounds { get; set; }
    public int TimePerRound { get; set; }
    public int CurrentRound { get; set; }
}