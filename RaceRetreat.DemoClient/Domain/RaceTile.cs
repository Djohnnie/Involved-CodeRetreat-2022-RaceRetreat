namespace RaceRetreat.DemoClient.Domain;

internal class RaceTile
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsStart { get; set; }
    public bool IsEnd { get; set; }
    public bool IsDrivable { get; set; }
    public bool HasOil { get; set; }
    public bool HasRock { get; set; }
    public List<Player> Players { get; set; }
}