namespace RaceRetreat.DemoClient.Domain;

internal class RaceMap
{
    public List<RaceTile> Tiles { get; set; }

    public RaceTile this[int x, int y]
    {
        get
        {
            return Tiles.SingleOrDefault(t => t.X == x && t.Y == y);
        }
    }

    public int Width { get; set; }
    public int Height { get; set; }
    public int Rounds { get; set; }
    public int TimePerRound { get; set; }
    public int OilPerPlayer { get; set; }
    public int RocksPerPlayer { get; set; }
}