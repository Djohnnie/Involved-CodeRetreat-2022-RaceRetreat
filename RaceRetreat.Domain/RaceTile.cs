namespace RaceRetreat.Domain;

public class RaceTile
{
    public int X { get; set; }
    public int Y { get; set; }
    public TileKind Kind { get; set; }
    public OverlayKind Overlay { get; set; }
    public bool IsUsed { get; set; }

    public RaceTile()
    {
        
    }

    public RaceTile(int x, int y, TileKind kind)
    {
        X = x;
        Y = y;
        Kind = kind;
        IsUsed = true;
    }

}