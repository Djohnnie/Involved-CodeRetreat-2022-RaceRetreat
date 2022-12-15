namespace RaceRetreat.Domain;

public class RaceTile
{
    public int X { get; set; }
    public int Y { get; set; }
    public TileKind Kind { get; set; }
    public OverlayKind Overlay { get; set; }
    public bool IsUsed { get; set; }

    public List<Player> Players { get; set; }

    public bool IsStart => Kind == TileKind.R1_11 || Kind == TileKind.R1_12 || Kind == TileKind.R1_13 || Kind == TileKind.R1_14 || Kind == TileKind.R1_15 || Kind == TileKind.R1_16 || Kind == TileKind.R1_17 || Kind == TileKind.R1_18;

    public bool IsEnd => Kind == TileKind.R1_21 || Kind == TileKind.R1_23 || Kind == TileKind.R1_23 || Kind ==
        TileKind.R1_24 || Kind ==
        TileKind.R1_25 || Kind == TileKind.R1_26 || Kind == TileKind.R1_27 || Kind == TileKind.R1_28;

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