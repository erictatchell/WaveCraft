namespace Wave3931
{
    partial class WaveAnalyzerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveAnalyzerForm));
            this.BottomStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.LeftChannelChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.RightChannelChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.BottomStatusStrip.SuspendLayout();
            this.TopMenuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.OptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftChannelChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightChannelChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomStatusStrip
            // 
            this.BottomStatusStrip.AccessibleDescription = "Bottom window status strip";
            this.BottomStatusStrip.AccessibleName = "StatusStrip";
            this.BottomStatusStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
            this.BottomStatusStrip.AllowItemReorder = true;
            this.BottomStatusStrip.AllowMerge = false;
            this.BottomStatusStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BottomStatusStrip.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BottomStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.BottomStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.BottomStatusStrip.Location = new System.Drawing.Point(0, 576);
            this.BottomStatusStrip.Name = "BottomStatusStrip";
            this.BottomStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 17, 0);
            this.BottomStatusStrip.Size = new System.Drawing.Size(1262, 25);
            this.BottomStatusStrip.TabIndex = 9;
            this.BottomStatusStrip.Text = "bottomStatusStrip";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(96, 20);
            this.toolStripStatusLabel1.Text = "Selected File:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.AccessibleDescription = "Menu bar at the top of the wave analyzer window";
            this.TopMenuStrip.AccessibleName = "Top menu bar";
            this.TopMenuStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.TopMenuStrip.AllowMerge = false;
            this.TopMenuStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TopMenuStrip.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.TopMenuStrip.Size = new System.Drawing.Size(1262, 30);
            this.TopMenuStrip.TabIndex = 10;
            this.TopMenuStrip.Text = "TopMenuStrip";
            this.TopMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TopMenuStrip_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 2;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(3, 116);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 34);
            this.button3.TabIndex = 4;
            this.button3.Text = "DFT";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(3, 78);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 34);
            this.button4.TabIndex = 5;
            this.button4.Text = "Record";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleDescription = "Play currently open sound file";
            this.btnPlay.AccessibleName = "Play Button";
            this.btnPlay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.FlatAppearance.BorderSize = 2;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlay.Location = new System.Drawing.Point(3, 2);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(120, 34);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 2;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(3, 40);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 34);
            this.button5.TabIndex = 3;
            this.button5.Text = "Stop";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoSize = true;
            this.panel2.BackgroundImage = global::Wave3931.Properties.Resources.StartScreen;
            this.panel2.Controls.Add(this.splitContainer2);
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Controls.Add(this.OptionsPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1262, 546);
            this.panel2.TabIndex = 11;
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.AccessibleDescription = "Right options panel ";
            this.OptionsPanel.AccessibleName = "Options Panel";
            this.OptionsPanel.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonMenu;
            this.OptionsPanel.BackColor = System.Drawing.Color.Black;
            this.OptionsPanel.Controls.Add(this.button10);
            this.OptionsPanel.Controls.Add(this.button9);
            this.OptionsPanel.Controls.Add(this.button8);
            this.OptionsPanel.Controls.Add(this.button7);
            this.OptionsPanel.Controls.Add(this.button6);
            this.OptionsPanel.Controls.Add(this.button3);
            this.OptionsPanel.Controls.Add(this.button4);
            this.OptionsPanel.Controls.Add(this.btnPlay);
            this.OptionsPanel.Controls.Add(this.button5);
            this.OptionsPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.OptionsPanel.Location = new System.Drawing.Point(1136, 0);
            this.OptionsPanel.Margin = new System.Windows.Forms.Padding(4);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(126, 546);
            this.OptionsPanel.TabIndex = 8;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem1.Text = "New";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 28);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderSize = 2;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(3, 214);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 34);
            this.button6.TabIndex = 6;
            this.button6.Text = "Samples";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.FlatAppearance.BorderSize = 2;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Location = new System.Drawing.Point(3, 252);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(120, 34);
            this.button7.TabIndex = 7;
            this.button7.Text = "Length";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.FlatAppearance.BorderSize = 2;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Location = new System.Drawing.Point(3, 367);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(120, 34);
            this.button8.TabIndex = 8;
            this.button8.Text = "Threading";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.FlatAppearance.BorderSize = 2;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Location = new System.Drawing.Point(3, 405);
            this.button9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(120, 38);
            this.button9.TabIndex = 9;
            this.button9.Text = "Filter";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.FlatAppearance.BorderSize = 2;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Location = new System.Drawing.Point(3, 516);
            this.button10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(120, 34);
            this.button10.TabIndex = 10;
            this.button10.Text = "Clear";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // LeftChannelChart
            // 
            this.LeftChannelChart.AccessibleDescription = "Right Channel";
            this.LeftChannelChart.AccessibleName = "Right Channel";
            this.LeftChannelChart.AccessibleRole = System.Windows.Forms.AccessibleRole.Chart;
            this.LeftChannelChart.BackColor = System.Drawing.Color.Black;
            this.LeftChannelChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LeftChannelChart.BackSecondaryColor = System.Drawing.Color.Red;
            this.LeftChannelChart.BorderlineColor = System.Drawing.Color.Black;
            this.LeftChannelChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.LeftChannelChart.BorderlineWidth = 3;
            this.LeftChannelChart.BorderSkin.BackColor = System.Drawing.Color.Black;
            this.LeftChannelChart.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.LeftChannelChart.BorderSkin.PageColor = System.Drawing.Color.Black;
            this.LeftChannelChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.BackImage = "C:\\Users\\brend\\OneDrive - BCIT\\Pictures\\Saved Pictures\\vaporWaveBG.jpg";
            chartArea2.IsSameFontSizeForAllAxes = true;
            chartArea2.Name = "ChartArea1";
            this.LeftChannelChart.ChartAreas.Add(chartArea2);
            this.LeftChannelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftChannelChart.Location = new System.Drawing.Point(0, 0);
            this.LeftChannelChart.Margin = new System.Windows.Forms.Padding(4);
            this.LeftChannelChart.Name = "LeftChannelChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            this.LeftChannelChart.Series.Add(series2);
            this.LeftChannelChart.Size = new System.Drawing.Size(1136, 284);
            this.LeftChannelChart.TabIndex = 0;
            this.LeftChannelChart.Text = "Right Channel";
            // 
            // RightChannelChart
            // 
            this.RightChannelChart.AccessibleDescription = "Left Channel";
            this.RightChannelChart.AccessibleName = "Left Channel";
            this.RightChannelChart.AccessibleRole = System.Windows.Forms.AccessibleRole.Chart;
            this.RightChannelChart.BackColor = System.Drawing.Color.Black;
            this.RightChannelChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RightChannelChart.BackSecondaryColor = System.Drawing.Color.WhiteSmoke;
            this.RightChannelChart.BorderlineColor = System.Drawing.Color.Black;
            this.RightChannelChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.RightChannelChart.BorderlineWidth = 3;
            this.RightChannelChart.BorderSkin.BackColor = System.Drawing.Color.Black;
            this.RightChannelChart.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.RightChannelChart.BorderSkin.PageColor = System.Drawing.Color.Black;
            this.RightChannelChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.BackImage = "C:\\Users\\brend\\OneDrive - BCIT\\Pictures\\Saved Pictures\\vaporWaveBG.jpg";
            chartArea1.CursorX.LineColor = System.Drawing.Color.Black;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.LightGreen;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Black;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "LeftChannelChartArea";
            this.RightChannelChart.ChartAreas.Add(chartArea1);
            this.RightChannelChart.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.RightChannelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend1.Enabled = false;
            legend1.Font = new System.Drawing.Font("Constantia", 8F);
            legend1.ForeColor = System.Drawing.Color.WhiteSmoke;
            legend1.HeaderSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.Line;
            legend1.IsEquallySpacedItems = true;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend1.MaximumAutoSize = 20F;
            legend1.Name = "Legend1";
            legend1.Title = "L";
            this.RightChannelChart.Legends.Add(legend1);
            this.RightChannelChart.Location = new System.Drawing.Point(0, 0);
            this.RightChannelChart.Margin = new System.Windows.Forms.Padding(4);
            this.RightChannelChart.Name = "RightChannelChart";
            series1.BackImage = "C:\\Users\\brend\\OneDrive - BCIT\\Pictures\\Saved Pictures\\vaporWaveBG.jpg";
            series1.ChartArea = "LeftChannelChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.WhiteSmoke;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.RightChannelChart.Series.Add(series1);
            this.RightChannelChart.Size = new System.Drawing.Size(1136, 261);
            this.RightChannelChart.TabIndex = 1;
            this.RightChannelChart.Text = "Left Channel";
            this.RightChannelChart.Click += new System.EventHandler(this.chart2_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-28, -83);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(150, 100);
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.AccessibleDescription = "Split container with 2 charts.";
            this.splitContainer2.AccessibleName = "Split Container";
            this.splitContainer2.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.RightChannelChart);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.LeftChannelChart);
            this.splitContainer2.Size = new System.Drawing.Size(1136, 546);
            this.splitContainer2.SplitterDistance = 261;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 10;
            // 
            // WaveAnalyzerForm
            // 
            this.AccessibleDescription = "Charts to analyze sound waves";
            this.AccessibleName = "Wave Analyzer";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1262, 601);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BottomStatusStrip);
            this.Controls.Add(this.TopMenuStrip);
            this.Font = new System.Drawing.Font("Constantia", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaveAnalyzerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wave Analyzer";
            this.BottomStatusStrip.ResumeLayout(false);
            this.BottomStatusStrip.PerformLayout();
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.OptionsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftChannelChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightChannelChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip BottomStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart LeftChannelChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart RightChannelChart;
    }
}