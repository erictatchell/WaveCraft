namespace Wave3931
{
    partial class DFT
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DFT));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnLowpass = new System.Windows.Forms.Button();
            this.btnHighpass = new System.Windows.Forms.Button();
            this.benchmark = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Black;
            chartArea1.BackColor = System.Drawing.Color.Crimson;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.HorizontalCenter;
            chartArea1.BackSecondaryColor = System.Drawing.Color.PaleVioletRed;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Default;
            this.chart1.DataSource = this.chart1.Images;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart1.Location = new System.Drawing.Point(0, 102);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series1.BackImageTransparentColor = System.Drawing.Color.White;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.White;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Name = "1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(656, 296);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // btnLowpass
            // 
            this.btnLowpass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(14)))), ((int)(((byte)(79)))));
            this.btnLowpass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLowpass.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnLowpass.FlatAppearance.BorderSize = 0;
            this.btnLowpass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLowpass.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLowpass.ForeColor = System.Drawing.Color.White;
            this.btnLowpass.Location = new System.Drawing.Point(204, 80);
            this.btnLowpass.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnLowpass.Name = "btnLowpass";
            this.btnLowpass.Size = new System.Drawing.Size(137, 27);
            this.btnLowpass.TabIndex = 6;
            this.btnLowpass.Text = "LOWPASS FILTER";
            this.btnLowpass.UseVisualStyleBackColor = false;
            this.btnLowpass.Click += new System.EventHandler(this.btnLowpass_Click);
            // 
            // btnHighpass
            // 
            this.btnHighpass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(14)))), ((int)(((byte)(79)))));
            this.btnHighpass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHighpass.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(79)))));
            this.btnHighpass.FlatAppearance.BorderSize = 0;
            this.btnHighpass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHighpass.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighpass.ForeColor = System.Drawing.Color.White;
            this.btnHighpass.Location = new System.Drawing.Point(347, 80);
            this.btnHighpass.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnHighpass.Name = "btnHighpass";
            this.btnHighpass.Size = new System.Drawing.Size(147, 27);
            this.btnHighpass.TabIndex = 7;
            this.btnHighpass.Text = "HIGHPASS FILTER";
            this.btnHighpass.UseVisualStyleBackColor = false;
            this.btnHighpass.Click += new System.EventHandler(this.btnHighpass_Click);
            // 
            // benchmark
            // 
            this.benchmark.AutoSize = true;
            this.benchmark.ForeColor = System.Drawing.SystemColors.Window;
            this.benchmark.Location = new System.Drawing.Point(12, 9);
            this.benchmark.Name = "benchmark";
            this.benchmark.Size = new System.Drawing.Size(143, 13);
            this.benchmark.TabIndex = 8;
            this.benchmark.Text = "Benchmarking, please wait...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "1 thread: ...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "threads:\r\n";
            // 
            // DFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(656, 398);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.benchmark);
            this.Controls.Add(this.btnHighpass);
            this.Controls.Add(this.btnLowpass);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DFT";
            this.Text = "DFT";
            this.Load += new System.EventHandler(this.DFT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnLowpass;
        private System.Windows.Forms.Button btnHighpass;
        private System.Windows.Forms.Label benchmark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}