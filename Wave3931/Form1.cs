using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using NAudio.Wave;
using NAudio.Dsp;

namespace Wave3931
{
    public partial class Form1 : Form
    {
        private double[] audioData;
        struct WaveFileHeader
        {
            public int RIFF;
            public int FileSizeMinus4;
            public int WAVE;
            public int Fmt_;
            public int FmtSize;
            public short FormatTag;
            public short Channels;
            public int SamplesPerSec;
            public int AvgBytesPerSec;
            public short BlockAlign;
            public short BitsPerSample;
            public int Data;
            public int DataSize;
        }

        private WaveFileHeader ReadWaveHeader(string filePath)
        {
            WaveFileHeader header = new WaveFileHeader();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                header.RIFF = reader.ReadInt32();
                header.FileSizeMinus4 = reader.ReadInt32();
                header.WAVE = reader.ReadInt32();
                header.Fmt_ = reader.ReadInt32();
                header.FmtSize = reader.ReadInt32();
                header.FormatTag = reader.ReadInt16();
                header.Channels = reader.ReadInt16();
                header.SamplesPerSec = reader.ReadInt32();
                header.AvgBytesPerSec = reader.ReadInt32();
                header.BlockAlign = reader.ReadInt16();
                header.BitsPerSample = reader.ReadInt16();
                header.Data = reader.ReadInt32();
                header.DataSize = reader.ReadInt32();
            }
            return header;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    toolStripStatusLabel1.Text = "Selected File: " + System.IO.Path.GetFileName(selectedFilePath);
                }
            }
        }
        double[] generic_fsg(int N, double f)
        {
            double[] s = new double[N];

            for (int t = 0; t < N; t++)
            {
                s[t] = (Math.Cos((2 * Math.PI) * ((double)t / N) * f));
            }

            return s;
        }
        public Form1()
        {
            InitializeComponent();
            
        }

        private DFT DFT;
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DFT = new DFT(); // Create a new instance of the DFT form
            DFT.Owner = this; // Set the owner if needed
            DFT.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        public void plotFreqWaveChart(double[] freq)
        {
            chart1.Series[0].Points.Clear();
            for (int m = 0; m < freq.Length; m++)
            { chart1.Series[0].Points.AddXY(m, freq[m]); }
            chart1.ChartAreas[0].AxisX.Minimum = 0;
        }

        

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            // Get the name of the selected file from toolStripStatusLabel1
            string selectedFileName = toolStripStatusLabel1.Text.Replace("Selected File: ", "");

            // Use the selectedFileName as needed
            MessageBox.Show("Selected File Name: " + selectedFileName);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
