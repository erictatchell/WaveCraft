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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveAnalyzerForm));
            this.BottomStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.BottomStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.BottomStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.BottomStatusStrip.Location = new System.Drawing.Point(0, 576);
            this.BottomStatusStrip.Name = "BottomStatusStrip";
            this.BottomStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 17, 0);
            this.BottomStatusStrip.Size = new System.Drawing.Size(1262, 25);
            this.BottomStatusStrip.TabIndex = 9;
            this.BottomStatusStrip.Text = "statusStrip1";
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
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 129);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 34);
            this.button3.TabIndex = 4;
            this.button3.Text = "DFT";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 91);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 34);
            this.button4.TabIndex = 5;
            this.button4.Text = "Record";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 15);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 34);
            this.button2.TabIndex = 0;
            this.button2.Text = "Play";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(18, 53);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(92, 34);
            this.button5.TabIndex = 3;
            this.button5.Text = "Stop";
            this.button5.UseVisualStyleBackColor = true;
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
            this.OptionsPanel.BackColor = System.Drawing.Color.Black;
            this.OptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OptionsPanel.Controls.Add(this.button10);
            this.OptionsPanel.Controls.Add(this.button9);
            this.OptionsPanel.Controls.Add(this.button8);
            this.OptionsPanel.Controls.Add(this.button7);
            this.OptionsPanel.Controls.Add(this.button6);
            this.OptionsPanel.Controls.Add(this.button3);
            this.OptionsPanel.Controls.Add(this.button4);
            this.OptionsPanel.Controls.Add(this.button2);
            this.OptionsPanel.Controls.Add(this.button5);
            this.OptionsPanel.Cursor = System.Windows.Forms.Cursors.No;
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
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 28);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(18, 167);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(92, 34);
            this.button6.TabIndex = 6;
            this.button6.Text = "Samples";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(18, 205);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(92, 34);
            this.button7.TabIndex = 7;
            this.button7.Text = "Length";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(18, 243);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(92, 34);
            this.button8.TabIndex = 8;
            this.button8.Text = "Threading";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(17, 281);
            this.button9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(92, 34);
            this.button9.TabIndex = 9;
            this.button9.Text = "Filter";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(18, 319);
            this.button10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(92, 34);
            this.button10.TabIndex = 10;
            this.button10.Text = "Clear";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // LeftChannelChart
            // 
            this.LeftChannelChart.BackColor = System.Drawing.Color.DarkGray;
            chartArea2.BackColor = System.Drawing.Color.Black;
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
            this.LeftChannelChart.Size = new System.Drawing.Size(1136, 269);
            this.LeftChannelChart.TabIndex = 0;
            this.LeftChannelChart.Text = "chart1";
            // 
            // RightChannelChart
            // 
            this.RightChannelChart.BackColor = System.Drawing.Color.DarkGray;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.RightChannelChart.ChartAreas.Add(chartArea1);
            this.RightChannelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightChannelChart.Location = new System.Drawing.Point(0, 0);
            this.RightChannelChart.Margin = new System.Windows.Forms.Padding(4);
            this.RightChannelChart.Name = "RightChannelChart";
            this.RightChannelChart.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.RightChannelChart.Series.Add(series1);
            this.RightChannelChart.Size = new System.Drawing.Size(1136, 273);
            this.RightChannelChart.TabIndex = 1;
            this.RightChannelChart.Text = "chart2";
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
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer2.SplitterDistance = 273;
            this.splitContainer2.TabIndex = 10;
            // 
            // WaveAnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Wave3931.Properties.Resources.vaporWaveBG;
            this.ClientSize = new System.Drawing.Size(1262, 601);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BottomStatusStrip);
            this.Controls.Add(this.TopMenuStrip);
            this.Font = new System.Drawing.Font("Constantia", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaveAnalyzerForm";
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
        private System.Windows.Forms.Button button2;
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