namespace RaceRetreat.Domain;

public class Player
{
    public int Index { get; set; }
    public string PlayerName { get; set; }
    public int OilTicksRemaining { get; set; }
    public bool Attacked { get; set; }
}