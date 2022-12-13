namespace RaceRetreat.Editor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapEditor = new RaceRetreat.Editor.EditorControl();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mapWidthToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.mapHeightToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.roads1DropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.roads2DropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.roads3DropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.overlayDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.roundsToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.timeToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.oilToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.rockToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapEditor
            // 
            this.mapEditor.ActiveEditOverlay = RaceRetreat.Domain.OverlayKind.O_00;
            this.mapEditor.ActiveEditTile = RaceRetreat.Domain.TileKind.R1_00;
            this.mapEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapEditor.Location = new System.Drawing.Point(0, 94);
            this.mapEditor.MapHeight = 10;
            this.mapEditor.MapWidth = 16;
            this.mapEditor.Name = "mapEditor";
            this.mapEditor.Padding = new System.Windows.Forms.Padding(10);
            this.mapEditor.Size = new System.Drawing.Size(2483, 1044);
            this.mapEditor.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.mapWidthToolStripTextBox,
            this.toolStripLabel2,
            this.mapHeightToolStripTextBox,
            this.toolStripSeparator2,
            this.roads1DropDownButton,
            this.roads2DropDownButton,
            this.roads3DropDownButton,
            this.overlayDropDownButton,
            this.toolStripSeparator3,
            this.toolStripLabel3,
            this.roundsToolStripTextBox,
            this.toolStripLabel4,
            this.timeToolStripTextBox,
            this.toolStripLabel5,
            this.oilToolStripTextBox,
            this.toolStripLabel6,
            this.rockToolStripTextBox});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(10);
            this.toolStrip.Size = new System.Drawing.Size(2483, 94);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::RaceRetreat.Editor.Properties.Resources._new;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Margin = new System.Windows.Forms.Padding(10, 2, 10, 4);
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.newToolStripButton.Text = "Start new map";
            this.newToolStripButton.ToolTipText = "Start new map";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::RaceRetreat.Editor.Properties.Resources._open;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Margin = new System.Windows.Forms.Padding(10, 2, 10, 4);
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.openToolStripButton.Text = "Open existing map";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::RaceRetreat.Editor.Properties.Resources._save;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Margin = new System.Windows.Forms.Padding(10, 2, 10, 4);
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(68, 68);
            this.saveToolStripButton.Text = "Save current map";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 74);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(83, 68);
            this.toolStripLabel1.Text = "Width:";
            // 
            // mapWidthToolStripTextBox
            // 
            this.mapWidthToolStripTextBox.Name = "mapWidthToolStripTextBox";
            this.mapWidthToolStripTextBox.Size = new System.Drawing.Size(100, 74);
            this.mapWidthToolStripTextBox.Text = "16";
            this.mapWidthToolStripTextBox.TextChanged += new System.EventHandler(this.mapWidthToolStripTextBox_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(98, 68);
            this.toolStripLabel2.Text = "Height: ";
            this.toolStripLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mapHeightToolStripTextBox
            // 
            this.mapHeightToolStripTextBox.Name = "mapHeightToolStripTextBox";
            this.mapHeightToolStripTextBox.Size = new System.Drawing.Size(100, 74);
            this.mapHeightToolStripTextBox.Text = "10";
            this.mapHeightToolStripTextBox.TextChanged += new System.EventHandler(this.mapHeightToolStripTextBox_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 74);
            // 
            // roads1DropDownButton
            // 
            this.roads1DropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.roads1DropDownButton.Image = global::RaceRetreat.Editor.Properties.Resources.r1_03;
            this.roads1DropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.roads1DropDownButton.Margin = new System.Windows.Forms.Padding(10, 2, 10, 4);
            this.roads1DropDownButton.Name = "roads1DropDownButton";
            this.roads1DropDownButton.Size = new System.Drawing.Size(86, 68);
            this.roads1DropDownButton.Text = "Asphalt roads";
            // 
            // roads2DropDownButton
            // 
            this.roads2DropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.roads2DropDownButton.Image = global::RaceRetreat.Editor.Properties.Resources.r2_03;
            this.roads2DropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.roads2DropDownButton.Margin = new System.Windows.Forms.Padding(10, 2, 10, 4);
            this.roads2DropDownButton.Name = "roads2DropDownButton";
            this.roads2DropDownButton.Size = new System.Drawing.Size(86, 68);
            this.roads2DropDownButton.Text = "Dirt Roads";
            this.roads2DropDownButton.ToolTipText = "Dirt roads";
            // 
            // roads3DropDownButton
            // 
            this.roads3DropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.roads3DropDownButton.Image = global::RaceRetreat.Editor.Properties.Resources.r3_03;
            this.roads3DropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.roads3DropDownButton.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.roads3DropDownButton.Name = "roads3DropDownButton";
            this.roads3DropDownButton.Size = new System.Drawing.Size(86, 74);
            this.roads3DropDownButton.Text = "Sand roads";
            // 
            // overlayDropDownButton
            // 
            this.overlayDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.overlayDropDownButton.Image = global::RaceRetreat.Editor.Properties.Resources.o_01;
            this.overlayDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.overlayDropDownButton.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.overlayDropDownButton.Name = "overlayDropDownButton";
            this.overlayDropDownButton.Size = new System.Drawing.Size(86, 74);
            this.overlayDropDownButton.Text = "toolStripDropDownButton1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 74);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(98, 68);
            this.toolStripLabel3.Text = "Rounds:";
            // 
            // roundsToolStripTextBox
            // 
            this.roundsToolStripTextBox.Name = "roundsToolStripTextBox";
            this.roundsToolStripTextBox.Size = new System.Drawing.Size(100, 74);
            this.roundsToolStripTextBox.Text = "16";
            this.roundsToolStripTextBox.TextChanged += new System.EventHandler(this.roundsToolStripTextBox_TextChanged);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(185, 68);
            this.toolStripLabel4.Text = "Time per round:";
            // 
            // timeToolStripTextBox
            // 
            this.timeToolStripTextBox.Name = "timeToolStripTextBox";
            this.timeToolStripTextBox.Size = new System.Drawing.Size(100, 74);
            this.timeToolStripTextBox.Text = "16";
            this.timeToolStripTextBox.TextChanged += new System.EventHandler(this.timeToolStripTextBox_TextChanged);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(206, 68);
            this.toolStripLabel5.Text = "Oil /game /player:";
            // 
            // oilToolStripTextBox
            // 
            this.oilToolStripTextBox.Name = "oilToolStripTextBox";
            this.oilToolStripTextBox.Size = new System.Drawing.Size(100, 74);
            this.oilToolStripTextBox.Text = "16";
            this.oilToolStripTextBox.TextChanged += new System.EventHandler(this.oilToolStripTextBox_TextChanged);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(226, 68);
            this.toolStripLabel6.Text = "Rock /game /player:";
            // 
            // rockToolStripTextBox
            // 
            this.rockToolStripTextBox.Name = "rockToolStripTextBox";
            this.rockToolStripTextBox.Size = new System.Drawing.Size(100, 74);
            this.rockToolStripTextBox.Text = "16";
            this.rockToolStripTextBox.TextChanged += new System.EventHandler(this.rockToolStripTextBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2483, 1138);
            this.Controls.Add(this.mapEditor);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Involved RaceRetreat Map Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EditorControl mapEditor;
        private ToolStrip toolStrip;
        private ToolStripButton newToolStripButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton roads1DropDownButton;
        private ToolStripDropDownButton roads2DropDownButton;
        private ToolStripDropDownButton roads3DropDownButton;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox mapWidthToolStripTextBox;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox mapHeightToolStripTextBox;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton overlayDropDownButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox roundsToolStripTextBox;
        private ToolStripLabel toolStripLabel4;
        private ToolStripTextBox timeToolStripTextBox;
        private ToolStripLabel toolStripLabel5;
        private ToolStripTextBox oilToolStripTextBox;
        private ToolStripLabel toolStripLabel6;
        private ToolStripTextBox rockToolStripTextBox;
    }
}