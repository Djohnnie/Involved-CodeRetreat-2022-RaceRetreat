namespace RaceRetreat.Domain;

public class RaceMap
{
    public List<RaceTile> Tiles { get; set; } = new List<RaceTile>();

    public RaceTile this[int x, int y]
    {
        get
        {
            return Tiles.SingleOrDefault(t => t.X == x && t.Y == y);
        }
    }

    public int Width
    {
        get
        {
            return Tiles.Where(t => t.IsUsed).Max(t => t.X) + 1;
        }
        set
        {
            Rebuild(value, Height);
        }
    }

    public int Height
    {
        get
        {
            return Tiles.Where(t => t.IsUsed).Max(t => t.Y) + 1;
        }
        set
        {
            Rebuild(Width, value);
        }
    }

    public int Rounds { get; set; }
    public int TimePerRound { get; set; }
    public int OilPerPlayer { get; set; }
    public int RocksPerPlayer { get; set; }

    public RaceMap() { }

    public RaceMap(int width, int height)
    {
        Rebuild(width, height);
    }

    private void Rebuild(int width, int height)
    {
        Tiles.ForEach(t => t.IsUsed = false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var existingTile = Tiles.SingleOrDefault(t => t.X == x && t.Y == y);

                if (existingTile == null)
                {
                    Tiles.Add(new RaceTile(x, y, TileKind.R1_00));
                }
                else
                {
                    existingTile.IsUsed = true;
                }
            }
        }
    }
}