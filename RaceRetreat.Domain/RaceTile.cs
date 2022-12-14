namespace RaceRetreat.Domain;

public class RaceTile
{
    public int X { get; set; }
    public int Y { get; set; }
    public TileKind Kind { get; set; }
    public OverlayKind Overlay { get; set; }
    public bool IsUsed { get; set; }

    public List<Player> Players { get; set; }

    public bool IsStart => Kind == TileKind.R1_03 | Kind == TileKind.R2_03 || Kind == TileKind.R3_03;

    public bool IsDrivable => Kind != TileKind.R1_00 && Kind != TileKind.R2_00 && Kind != TileKind.R3_00;

    public bool HasOil =>
        Overlay == OverlayKind.O_11 || Overlay == OverlayKind.O_12 || Overlay == OverlayKind.O_13 ||
        Overlay == OverlayKind.O_14;

    public bool HasRock => Overlay == OverlayKind.O_21 || Overlay == OverlayKind.O_22 || Overlay == OverlayKind.O_23;

    public RaceTile()
    {
        Players = new List<Player>();
    }

    public RaceTile(int x, int y, TileKind kind)
    {
        X = x;
        Y = y;
        Kind = kind;
        IsUsed = true;
        Players = new List<Player>();
    }

}