using RaceRetreat.Domain;
using System.Text.Json;

namespace RaceRetreat.Editor;

public partial class EditorControl : UserControl
{
    private RaceMap _map;
    private Point? _cursorLocation;

    public TileKind ActiveEditTile { get; set; }
    public OverlayKind ActiveEditOverlay { get; set; }

    public int MapWidth
    {
        get { return _map.Width; }
        set
        {
            _map.Width = value;
            Invalidate();
        }
    }

    public int MapHeight
    {
        get { return _map.Height; }
        set
        {
            _map.Height = value;
            Invalidate();
        }
    }

    public int Rounds
    {
        get { return _map.Rounds; }
        set
        {
            _map.Rounds = value;
        }
    }

    public int TimePerRound
    {
        get { return _map.TimePerRound; }
        set
        {
            _map.TimePerRound = value;
        }
    }

    public int OilPerPlayer
    {
        get { return _map.OilPerPlayer; }
        set
        {
            _map.OilPerPlayer = value;
        }
    }

    public int RocksPerPlayer
    {
        get { return _map.RocksPerPlayer; }
        set
        {
            _map.RocksPerPlayer = value;
        }
    }

    public EditorControl()
    {
        InitializeComponent();
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.DoubleBuffer |
            ControlStyles.ResizeRedraw, true);

        _map = new RaceMap(16, 10);
    }

    private void EditorControl_Paint(object sender, PaintEventArgs e)
    {
        var tileWidth = ClientRectangle.Width / _map.Width;
        var tileHeight = ClientRectangle.Height / _map.Height;

        e.Graphics.Clear(Color.White);

        foreach (var mazeTile in _map.Tiles)
        {
            if (mazeTile.IsUsed)
            {
                var tileBitmap = Bitmaps.FromTileKind(mazeTile.Kind);
                var overlayBitmap = Bitmaps.FromOverlayKind(mazeTile.Overlay);
                var backgroundBitmap = Bitmaps.FromTileKind(TileKind.R1_00);

                var xOffset = mazeTile.X * tileWidth;
                var yOffset = mazeTile.Y * tileHeight;
                var bounds = new Rectangle(xOffset, yOffset, tileWidth, tileHeight);

                if (_cursorLocation.HasValue)
                {
                    if (bounds.Contains(_cursorLocation.Value))
                    {
                        tileBitmap = Bitmaps.FromTileKind(ActiveEditTile);
                        overlayBitmap = Bitmaps.FromOverlayKind(ActiveEditOverlay);
                    }
                }

                e.Graphics.DrawImage(backgroundBitmap, bounds);
                e.Graphics.DrawImage(tileBitmap, bounds);
                e.Graphics.DrawImage(overlayBitmap, bounds);

                using var brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString($"({mazeTile.X},{mazeTile.Y})", SystemFonts.DefaultFont, brush, bounds, format);
            }
        }
    }

    private void EditorControl_SizeChanged(object sender, EventArgs e)
    {
        Invalidate();
    }

    private void EditorControl_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var mazeTile = GetRaceTileFromLocation(e.Location);

            if (mazeTile != null)
            {
                mazeTile.Kind = ActiveEditTile;
                mazeTile.Overlay = ActiveEditOverlay;
            }
        }

        var oldLocation = _cursorLocation;
        _cursorLocation = e.Location;

        var tileWidth = ClientRectangle.Width / _map.Width;
        var tileHeight = ClientRectangle.Height / _map.Height;

        for (var x = 0; x < _map.Width; x++)
        {
            for (var y = 0; y < _map.Height; y++)
            {
                var xOffset = x * tileWidth;
                var yOffset = y * tileHeight;
                var bounds = new Rectangle(xOffset, yOffset, tileWidth, tileHeight);

                if (oldLocation.HasValue && bounds.Contains(oldLocation.Value))
                {
                    Invalidate(bounds);
                }

                if (bounds.Contains(e.Location))
                {
                    Invalidate(bounds);
                }
            }
        }
    }

    private void EditorControl_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var mazeTile = GetRaceTileFromLocation(e.Location);

            if (mazeTile != null)
            {
                mazeTile.Kind = ActiveEditTile;
                mazeTile.Overlay = ActiveEditOverlay;
            }
        }
    }

    private RaceTile GetRaceTileFromLocation(Point location)
    {
        foreach (var tile in _map.Tiles)
        {
            var bounds = CalculateBounds(tile.X, tile.Y);

            if (bounds.Contains(location))
            {
                return tile;
            }
        }

        return null;
    }

    private Rectangle GetBoundsAtLocation(Point location)
    {
        var tileWidth = ClientRectangle.Width / _map.Width;
        var tileHeight = ClientRectangle.Height / _map.Height;

        for (var x = 0; x < _map.Width; x++)
        {
            for (var y = 0; y < _map.Height; y++)
            {
                var xOffset = x * tileWidth;
                var yOffset = y * tileHeight;
                return new Rectangle(xOffset, yOffset, tileWidth, tileHeight);
            }
        }

        return ClientRectangle;
    }

    private Rectangle CalculateBounds(int x, int y)
    {
        var tileWidth = ClientRectangle.Width / _map.Width;
        var tileHeight = ClientRectangle.Height / _map.Height;

        var xOffset = x * tileWidth;
        var yOffset = y * tileHeight;
        var bounds = new Rectangle(xOffset, yOffset, tileWidth, tileHeight);

        return bounds;
    }

    internal void CreateNew()
    {
        _map = new RaceMap(16, 10);
        Rounds = 0;
        TimePerRound = 0;
        OilPerPlayer = 0;
        RocksPerPlayer = 0;
        Invalidate();
    }

    internal string SaveCurrent()
    {
        return JsonSerializer.Serialize(_map);
    }

    internal void OpenExisting(string data)
    {
        _map = JsonSerializer.Deserialize<RaceMap>(data);
        Invalidate();
    }
}