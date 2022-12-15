namespace RaceRetreat.Domain;

public record Play
{
    public string PlayerName { get; set; }
    public int Index { get; set; }

    public List<Step>? Steps { get; set; }
}