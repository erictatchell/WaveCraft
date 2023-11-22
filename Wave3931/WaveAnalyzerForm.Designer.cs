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
            System.Windows.Forms.DataVisualization.Charting.VerticalLineAnnotation verticalLineAnnotation1 = new System.Windows.Forms.DataVisualization.Charting.VerticalLineAnnotation();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveAnalyzerForm));
            this.btnDFT = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.zoomRadio = new System.Windows.Forms.RadioButton();
            this.selectRadio = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.BottomStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Info = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.titleLeft = new System.Windows.Forms.TextBox();
            this.LEFT_CHANNEL_CHART = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.titleRight = new System.Windows.Forms.TextBox();
            this.RIGHT_CHANNEL_CHART = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dFTThreadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threads_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.threads_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.threads_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.threads_15 = new System.Windows.Forms.ToolStripMenuItem();
            this.threads_50 = new System.Windows.Forms.ToolStripMenuItem();
            this.windowingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hammingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.panel2.SuspendLayout();
            this.OptionsPanel.SuspendLayout();
            this.BottomStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LEFT_CHANNEL_CHART)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RIGHT_CHANNEL_CHART)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.TopMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDFT
            // 
            this.btnDFT.BackColor = System.Drawing.Color.White;
            this.btnDFT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDFT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnDFT.FlatAppearance.BorderSize = 2;
            this.btnDFT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDFT.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDFT.Location = new System.Drawing.Point(3, 93);
            this.btnDFT.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnDFT.Name = "btnDFT";
            this.btnDFT.Size = new System.Drawing.Size(96, 27);
            this.btnDFT.TabIndex = 4;
            this.btnDFT.Text = "DFT";
            this.btnDFT.UseVisualStyleBackColor = false;
            this.btnDFT.Click += new System.EventHandler(this.btnDFT_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(14)))), ((int)(((byte)(79)))));
            this.btnStopRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopRecord.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnStopRecord.FlatAppearance.BorderSize = 2;
            this.btnStopRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopRecord.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopRecord.ForeColor = System.Drawing.Color.White;
            this.btnStopRecord.Location = new System.Drawing.Point(3, 63);
            this.btnStopRecord.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(96, 27);
            this.btnStopRecord.TabIndex = 5;
            this.btnStopRecord.Text = "RECORD";
            this.btnStopRecord.UseVisualStyleBackColor = false;
            this.btnStopRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleDescription = "Play currently open sound file";
            this.btnPlay.AccessibleName = "Play Button";
            this.btnPlay.BackColor = System.Drawing.Color.White;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnPlay.FlatAppearance.BorderSize = 2;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(3, 1);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(96, 27);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "PLAY";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.White;
            this.btnRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecord.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnRecord.FlatAppearance.BorderSize = 2;
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.Location = new System.Drawing.Point(3, 32);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(96, 27);
            this.btnRecord.TabIndex = 3;
            this.btnRecord.Text = "STOP";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.OptionsPanel);
            this.panel2.Controls.Add(this.BottomStatusStrip);
            this.panel2.Controls.Add(this.splitContainer2);
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1304, 568);
            this.panel2.TabIndex = 11;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint_2);
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.AccessibleDescription = "Right options panel ";
            this.OptionsPanel.AccessibleName = "Options Panel";
            this.OptionsPanel.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonMenu;
            this.OptionsPanel.BackColor = System.Drawing.Color.Black;
            this.OptionsPanel.Controls.Add(this.zoomRadio);
            this.OptionsPanel.Controls.Add(this.selectRadio);
            this.OptionsPanel.Controls.Add(this.btnClear);
            this.OptionsPanel.Controls.Add(this.btnDFT);
            this.OptionsPanel.Controls.Add(this.btnStopRecord);
            this.OptionsPanel.Controls.Add(this.btnPlay);
            this.OptionsPanel.Controls.Add(this.btnRecord);
            this.OptionsPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.OptionsPanel.Location = new System.Drawing.Point(1203, 0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(101, 546);
            this.OptionsPanel.TabIndex = 8;
            this.OptionsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OptionsPanel_Paint);
            // 
            // zoomRadio
            // 
            this.zoomRadio.AutoSize = true;
            this.zoomRadio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomRadio.ForeColor = System.Drawing.SystemColors.Control;
            this.zoomRadio.Location = new System.Drawing.Point(15, 160);
            this.zoomRadio.Name = "zoomRadio";
            this.zoomRadio.Size = new System.Drawing.Size(53, 17);
            this.zoomRadio.TabIndex = 12;
            this.zoomRadio.TabStop = true;
            this.zoomRadio.Text = "Zoom";
            this.zoomRadio.UseVisualStyleBackColor = true;
            this.zoomRadio.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // selectRadio
            // 
            this.selectRadio.AutoSize = true;
            this.selectRadio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectRadio.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectRadio.ForeColor = System.Drawing.SystemColors.Control;
            this.selectRadio.Location = new System.Drawing.Point(15, 138);
            this.selectRadio.Name = "selectRadio";
            this.selectRadio.Size = new System.Drawing.Size(65, 15);
            this.selectRadio.TabIndex = 11;
            this.selectRadio.TabStop = true;
            this.selectRadio.Text = "Select";
            this.selectRadio.UseVisualStyleBackColor = true;
            this.selectRadio.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnClear.FlatAppearance.BorderSize = 2;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(2, 519);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 27);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BottomStatusStrip
            // 
            this.BottomStatusStrip.AccessibleDescription = "Bottom window status strip";
            this.BottomStatusStrip.AccessibleName = "StatusStrip";
            this.BottomStatusStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
            this.BottomStatusStrip.AllowItemReorder = true;
            this.BottomStatusStrip.AllowMerge = false;
            this.BottomStatusStrip.BackColor = System.Drawing.Color.Black;
            this.BottomStatusStrip.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BottomStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.BottomStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.Info,
            this.toolStripStatusLabel1});
            this.BottomStatusStrip.Location = new System.Drawing.Point(0, 546);
            this.BottomStatusStrip.Name = "BottomStatusStrip";
            this.BottomStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.BottomStatusStrip.Size = new System.Drawing.Size(1304, 22);
            this.BottomStatusStrip.TabIndex = 9;
            this.BottomStatusStrip.Text = "bottomStatusStrip";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // Info
            // 
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(0, 17);
            this.Info.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(460, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click_1);
            // 
            // splitContainer2
            // 
            this.splitContainer2.AccessibleDescription = "Split container with 2 charts.";
            this.splitContainer2.AccessibleName = "Split Container";
            this.splitContainer2.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.splitContainer2.BackColor = System.Drawing.Color.Black;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 19);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.titleLeft);
            this.splitContainer2.Panel1.Controls.Add(this.LEFT_CHANNEL_CHART);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.titleRight);
            this.splitContainer2.Panel2.Controls.Add(this.RIGHT_CHANNEL_CHART);
            this.splitContainer2.Size = new System.Drawing.Size(1200, 527);
            this.splitContainer2.SplitterDistance = 239;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 10;
            // 
            // titleLeft
            // 
            this.titleLeft.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.titleLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titleLeft.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLeft.ForeColor = System.Drawing.Color.Gold;
            this.titleLeft.Location = new System.Drawing.Point(32, 93);
            this.titleLeft.Name = "titleLeft";
            this.titleLeft.ReadOnly = true;
            this.titleLeft.Size = new System.Drawing.Size(14, 19);
            this.titleLeft.TabIndex = 2;
            this.titleLeft.Text = "L";
            this.titleLeft.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // LEFT_CHANNEL_CHART
            // 
            this.LEFT_CHANNEL_CHART.AccessibleDescription = "Left Channel";
            this.LEFT_CHANNEL_CHART.AccessibleName = "Left Channel";
            this.LEFT_CHANNEL_CHART.AccessibleRole = System.Windows.Forms.AccessibleRole.Chart;
            verticalLineAnnotation1.AllowMoving = true;
            verticalLineAnnotation1.LineColor = System.Drawing.Color.Gold;
            verticalLineAnnotation1.Name = "Line";
            this.LEFT_CHANNEL_CHART.Annotations.Add(verticalLineAnnotation1);
            this.LEFT_CHANNEL_CHART.BackColor = System.Drawing.Color.Black;
            this.LEFT_CHANNEL_CHART.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LEFT_CHANNEL_CHART.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.LEFT_CHANNEL_CHART.BorderlineColor = System.Drawing.Color.Black;
            this.LEFT_CHANNEL_CHART.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.LEFT_CHANNEL_CHART.BorderlineWidth = 0;
            this.LEFT_CHANNEL_CHART.BorderSkin.BackColor = System.Drawing.Color.Black;
            this.LEFT_CHANNEL_CHART.BorderSkin.PageColor = System.Drawing.Color.Black;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.Black;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(6)))), ((int)(((byte)(83)))));
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Near;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Gold;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Gold;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.BackImage = "C:\\Users\\etatc\\source\\repos\\WaveAnalyser\\Wave3931\\Resources\\vaporWaveBG.jpg";
            chartArea1.CursorX.LineColor = System.Drawing.Color.Black;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.LightGreen;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Black;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "LeftChannelChartArea";
            this.LEFT_CHANNEL_CHART.ChartAreas.Add(chartArea1);
            this.LEFT_CHANNEL_CHART.Cursor = System.Windows.Forms.Cursors.Cross;
            this.LEFT_CHANNEL_CHART.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LEFT_CHANNEL_CHART.Location = new System.Drawing.Point(0, 0);
            this.LEFT_CHANNEL_CHART.Name = "LEFT_CHANNEL_CHART";
            series1.BackImage = "C:\\Users\\brend\\OneDrive - BCIT\\Pictures\\Saved Pictures\\vaporWaveBG.jpg";
            series1.ChartArea = "LeftChannelChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.WhiteSmoke;
            series1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Name = "Series1";
            this.LEFT_CHANNEL_CHART.Series.Add(series1);
            this.LEFT_CHANNEL_CHART.Size = new System.Drawing.Size(1200, 239);
            this.LEFT_CHANNEL_CHART.TabIndex = 1;
            this.LEFT_CHANNEL_CHART.Text = "Left Channel";
            this.LEFT_CHANNEL_CHART.Click += new System.EventHandler(this.chart2_Click);
            // 
            // titleRight
            // 
            this.titleRight.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.titleRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titleRight.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleRight.ForeColor = System.Drawing.Color.Gold;
            this.titleRight.Location = new System.Drawing.Point(32, 107);
            this.titleRight.Name = "titleRight";
            this.titleRight.ReadOnly = true;
            this.titleRight.Size = new System.Drawing.Size(14, 19);
            this.titleRight.TabIndex = 3;
            this.titleRight.Text = "R";
            // 
            // RIGHT_CHANNEL_CHART
            // 
            this.RIGHT_CHANNEL_CHART.AccessibleDescription = "Right Channel";
            this.RIGHT_CHANNEL_CHART.AccessibleName = "Right Channel";
            this.RIGHT_CHANNEL_CHART.AccessibleRole = System.Windows.Forms.AccessibleRole.Chart;
            this.RIGHT_CHANNEL_CHART.BackColor = System.Drawing.Color.Black;
            this.RIGHT_CHANNEL_CHART.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RIGHT_CHANNEL_CHART.BackSecondaryColor = System.Drawing.Color.Red;
            this.RIGHT_CHANNEL_CHART.BorderlineColor = System.Drawing.Color.Black;
            this.RIGHT_CHANNEL_CHART.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.RIGHT_CHANNEL_CHART.BorderlineWidth = 3;
            this.RIGHT_CHANNEL_CHART.BorderSkin.BackColor = System.Drawing.Color.Black;
            this.RIGHT_CHANNEL_CHART.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.RIGHT_CHANNEL_CHART.BorderSkin.PageColor = System.Drawing.Color.Black;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.Gold;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.BackImage = "C:\\Users\\etatc\\source\\repos\\WaveAnalyser\\Wave3931\\Resources\\vaporWaveBG.jpg";
            chartArea2.IsSameFontSizeForAllAxes = true;
            chartArea2.Name = "ChartArea1";
            this.RIGHT_CHANNEL_CHART.ChartAreas.Add(chartArea2);
            this.RIGHT_CHANNEL_CHART.Cursor = System.Windows.Forms.Cursors.Cross;
            this.RIGHT_CHANNEL_CHART.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RIGHT_CHANNEL_CHART.Location = new System.Drawing.Point(0, 0);
            this.RIGHT_CHANNEL_CHART.Name = "RIGHT_CHANNEL_CHART";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.White;
            series2.Name = "Series1";
            this.RIGHT_CHANNEL_CHART.Series.Add(series2);
            this.RIGHT_CHANNEL_CHART.Size = new System.Drawing.Size(1200, 287);
            this.RIGHT_CHANNEL_CHART.TabIndex = 0;
            this.RIGHT_CHANNEL_CHART.Text = "Right Channel";
            this.RIGHT_CHANNEL_CHART.Click += new System.EventHandler(this.LeftChannelChart_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-23, -67);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(120, 80);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 9;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileMenu,
            this.saveFileMenu});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFileMenu
            // 
            this.newFileMenu.Name = "newFileMenu";
            this.newFileMenu.Size = new System.Drawing.Size(98, 22);
            this.newFileMenu.Text = "New";
            this.newFileMenu.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // saveFileMenu
            // 
            this.saveFileMenu.Name = "saveFileMenu";
            this.saveFileMenu.Size = new System.Drawing.Size(98, 22);
            this.saveFileMenu.Text = "Save";
            this.saveFileMenu.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // sampleRateToolStripMenuItem
            // 
            this.sampleRateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hzToolStripMenuItem,
            this.hzToolStripMenuItem1,
            this.hzToolStripMenuItem2});
            this.sampleRateToolStripMenuItem.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sampleRateToolStripMenuItem.Name = "sampleRateToolStripMenuItem";
            this.sampleRateToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.sampleRateToolStripMenuItem.Text = "Sample Rate: 11025 Hz";
            this.sampleRateToolStripMenuItem.Click += new System.EventHandler(this.sampleRateToolStripMenuItem_Click);
            // 
            // hzToolStripMenuItem
            // 
            this.hzToolStripMenuItem.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hzToolStripMenuItem.Name = "hzToolStripMenuItem";
            this.hzToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.hzToolStripMenuItem.Text = "11025 Hz";
            this.hzToolStripMenuItem.Click += new System.EventHandler(this.hzToolStripMenuItem_Click);
            // 
            // hzToolStripMenuItem1
            // 
            this.hzToolStripMenuItem1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hzToolStripMenuItem1.Name = "hzToolStripMenuItem1";
            this.hzToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.hzToolStripMenuItem1.Text = "22050 Hz";
            this.hzToolStripMenuItem1.Click += new System.EventHandler(this.hzToolStripMenuItem1_Click);
            // 
            // hzToolStripMenuItem2
            // 
            this.hzToolStripMenuItem2.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hzToolStripMenuItem2.Name = "hzToolStripMenuItem2";
            this.hzToolStripMenuItem2.Size = new System.Drawing.Size(126, 22);
            this.hzToolStripMenuItem2.Text = "44100 Hz";
            this.hzToolStripMenuItem2.Click += new System.EventHandler(this.hzToolStripMenuItem2_Click);
            // 
            // dFTThreadsToolStripMenuItem
            // 
            this.dFTThreadsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threads_1,
            this.threads_2,
            this.threads_4,
            this.threads_15,
            this.threads_50});
            this.dFTThreadsToolStripMenuItem.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dFTThreadsToolStripMenuItem.Name = "dFTThreadsToolStripMenuItem";
            this.dFTThreadsToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.dFTThreadsToolStripMenuItem.Text = "DFT Threads: 1";
            this.dFTThreadsToolStripMenuItem.Click += new System.EventHandler(this.dFTThreadsToolStripMenuItem_Click);
            // 
            // threads_1
            // 
            this.threads_1.CheckOnClick = true;
            this.threads_1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threads_1.Name = "threads_1";
            this.threads_1.Size = new System.Drawing.Size(84, 22);
            this.threads_1.Text = "1";
            this.threads_1.Click += new System.EventHandler(this.toolStripMenuItem2_Click_1);
            // 
            // threads_2
            // 
            this.threads_2.CheckOnClick = true;
            this.threads_2.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threads_2.Name = "threads_2";
            this.threads_2.Size = new System.Drawing.Size(84, 22);
            this.threads_2.Text = "2";
            this.threads_2.Click += new System.EventHandler(this.threads_2_Click);
            // 
            // threads_4
            // 
            this.threads_4.CheckOnClick = true;
            this.threads_4.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threads_4.Name = "threads_4";
            this.threads_4.Size = new System.Drawing.Size(84, 22);
            this.threads_4.Text = "4";
            this.threads_4.Click += new System.EventHandler(this.threads_4_Click);
            // 
            // threads_15
            // 
            this.threads_15.CheckOnClick = true;
            this.threads_15.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threads_15.Name = "threads_15";
            this.threads_15.Size = new System.Drawing.Size(84, 22);
            this.threads_15.Text = "15";
            this.threads_15.Click += new System.EventHandler(this.threads_15_Click);
            // 
            // threads_50
            // 
            this.threads_50.CheckOnClick = true;
            this.threads_50.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threads_50.Name = "threads_50";
            this.threads_50.Size = new System.Drawing.Size(84, 22);
            this.threads_50.Text = "50";
            this.threads_50.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // windowingToolStripMenuItem
            // 
            this.windowingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rectangleToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.hammingToolStripMenuItem});
            this.windowingToolStripMenuItem.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowingToolStripMenuItem.Name = "windowingToolStripMenuItem";
            this.windowingToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.windowingToolStripMenuItem.Text = "Windowing: Rectangle";
            this.windowingToolStripMenuItem.Click += new System.EventHandler(this.windowingToolStripMenuItem_Click);
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.CheckOnClick = true;
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.rectangleToolStripMenuItem_Click);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.CheckOnClick = true;
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.triangleToolStripMenuItem_Click);
            // 
            // hammingToolStripMenuItem
            // 
            this.hammingToolStripMenuItem.CheckOnClick = true;
            this.hammingToolStripMenuItem.Name = "hammingToolStripMenuItem";
            this.hammingToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.hammingToolStripMenuItem.Text = "Hamming";
            this.hammingToolStripMenuItem.Click += new System.EventHandler(this.hammingToolStripMenuItem_Click);
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
            this.sampleRateToolStripMenuItem,
            this.dFTThreadsToolStripMenuItem,
            this.windowingToolStripMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.TopMenuStrip.Size = new System.Drawing.Size(1304, 24);
            this.TopMenuStrip.TabIndex = 10;
            this.TopMenuStrip.Text = "TopMenuStrip";
            this.TopMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TopMenuStrip_ItemClicked);
            // 
            // WaveAnalyzerForm
            // 
            this.AccessibleDescription = "Charts to analyze sound waves";
            this.AccessibleName = "Wave Analyzer";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1304, 592);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TopMenuStrip);
            this.Font = new System.Drawing.Font("Constantia", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaveAnalyzerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wave Analyzer";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.BottomStatusStrip.ResumeLayout(false);
            this.BottomStatusStrip.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LEFT_CHANNEL_CHART)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RIGHT_CHANNEL_CHART)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDFT;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart RIGHT_CHANNEL_CHART;
        private System.Windows.Forms.DataVisualization.Charting.Chart LEFT_CHANNEL_CHART;
        private System.Windows.Forms.StatusStrip BottomStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileMenu;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenu;
        private System.Windows.Forms.ToolStripMenuItem sampleRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem dFTThreadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threads_1;
        private System.Windows.Forms.ToolStripMenuItem threads_2;
        private System.Windows.Forms.ToolStripMenuItem threads_4;
        private System.Windows.Forms.ToolStripMenuItem threads_15;
        private System.Windows.Forms.ToolStripMenuItem threads_50;
        private System.Windows.Forms.ToolStripMenuItem windowingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hammingToolStripMenuItem;
        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.TextBox titleLeft;
        private System.Windows.Forms.TextBox titleRight;
        private System.Windows.Forms.RadioButton zoomRadio;
        private System.Windows.Forms.RadioButton selectRadio;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel Info;
    }
}