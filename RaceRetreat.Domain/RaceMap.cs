namespace RaceRetreat.Domain;

public class RaceMap : List<RaceTile>
{
    public RaceTile this[int x, int y]
    {
        get
        {
            return this.SingleOrDefault(t => t.X == x && t.Y == y);
        }
    }

    public int Width
    {
        get
        {
            return this.Where(t => t.IsUsed).Max(t => t.X) + 1;
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
            return this.Where(t => t.IsUsed).Max(t => t.Y) + 1;
        }
        set
        {
            Rebuild(Width, value);
        }
    }

    public RaceMap() { }

    public RaceMap(int width, int height)
    {
        Rebuild(width, height);
    }

    private void Rebuild(int width, int height)
    {
        ForEach(t => t.IsUsed = false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var existingTile = this.SingleOrDefault(t => t.X == x && t.Y == y);

                if (existingTile == null)
                {
                    Add(new RaceTile(x, y, TileKind.R1_00));
                }
                else
                {
                    existingTile.IsUsed = true;
                }
            }
        }
    }
}