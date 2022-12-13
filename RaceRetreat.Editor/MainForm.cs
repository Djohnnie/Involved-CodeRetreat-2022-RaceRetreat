using RaceRetreat.Domain;
using System.Drawing.Design;

namespace RaceRetreat.Editor;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        InitializeToolStrip();
    }

    private void InitializeToolStrip()
    {
        foreach (var tileKind in Enum.GetValues<TileKind>())
        {
            var title = $"{tileKind}";

            var button = new ToolStripButton(title, Bitmaps.FromTileKind(tileKind))
            {
                ToolTipText = title,
                Tag = tileKind
            };
            button.Click += TileButton_Click;

            if (title.StartsWith("R1"))
            {
                roads1DropDownButton.DropDownItems.Add(button);
            }

            if (title.StartsWith("R2"))
            {
                roads2DropDownButton.DropDownItems.Add(button);
            }

            if (title.StartsWith("R3"))
            {
                roads3DropDownButton.DropDownItems.Add(button);
            }
        }

        foreach (var overlayKind in Enum.GetValues<OverlayKind>())
        {
            var title = $"{overlayKind}";

            var button = new ToolStripButton(title, Bitmaps.FromOverlayKind(overlayKind))
            {
                ToolTipText = title,
                Tag = overlayKind
            };
            button.Click += OverlayButton_Click;

            overlayDropDownButton.DropDownItems.Add(button);
        }
    }

    private void TileButton_Click(object? sender, EventArgs e)
    {
        var button = sender as ToolStripButton;
        if (button != null)
        {
            mapEditor.ActiveEditTile = (TileKind)button.Tag;
        }
    }

    private void OverlayButton_Click(object? sender, EventArgs e)
    {
        var button = sender as ToolStripButton;
        if (button != null)
        {
            mapEditor.ActiveEditOverlay = (OverlayKind)button.Tag;
        }
    }

    private void newToolStripButton_Click(object sender, EventArgs e)
    {
        mapEditor.CreateNew();
        mapWidthToolStripTextBox.Text = $"{mapEditor.MapWidth}";
        mapHeightToolStripTextBox.Text = $"{mapEditor.MapHeight}";
        roundsToolStripTextBox.Text = $"{mapEditor.Rounds}";
        timeToolStripTextBox.Text = $"{mapEditor.TimePerRound}";
        rockToolStripTextBox.Text = $"{mapEditor.OilPerPlayer}";
        oilToolStripTextBox.Text = $"{mapEditor.RocksPerPlayer}";
    }

    private async void openToolStripButton_Click(object sender, EventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "RaceRetreat Map files (*.racejson)|*.racejson";
        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            string data = await File.ReadAllTextAsync(openFileDialog.FileName);
            mapEditor.OpenExisting(data);
            mapWidthToolStripTextBox.Text = $"{mapEditor.MapWidth}";
            mapHeightToolStripTextBox.Text = $"{mapEditor.MapHeight}";
            roundsToolStripTextBox.Text = $"{mapEditor.Rounds}";
            timeToolStripTextBox.Text = $"{mapEditor.TimePerRound}";
            rockToolStripTextBox.Text = $"{mapEditor.OilPerPlayer}";
            oilToolStripTextBox.Text = $"{mapEditor.RocksPerPlayer}";
        }
    }

    private async void saveToolStripButton_Click(object sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "RaceRetreat Map files (*.racejson)|*.racejson";
        if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            string data = mapEditor.SaveCurrent();
            await File.WriteAllTextAsync(saveFileDialog.FileName, data);
        }
    }

    private void mapWidthToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            mapEditor.MapWidth = Convert.ToInt32(mapWidthToolStripTextBox.Text);
        }
        catch { }
    }

    private void mapHeightToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            mapEditor.MapHeight = Convert.ToInt32(mapHeightToolStripTextBox.Text);
        }
        catch { }
    }

    private void roundsToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            mapEditor.Rounds = Convert.ToInt32(roundsToolStripTextBox.Text);
        }
        catch { }
    }

    private void timeToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            mapEditor.TimePerRound = Convert.ToInt32(timeToolStripTextBox.Text);
        }
        catch { }
    }

    private void oilToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            mapEditor.OilPerPlayer = Convert.ToInt32(oilToolStripTextBox.Text);
        }
        catch { }
    }

    private void rockToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            mapEditor.RocksPerPlayer = Convert.ToInt32(rockToolStripTextBox.Text);
        }
        catch { }
    }
}