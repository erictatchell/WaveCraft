using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Wave3931
{
    public partial class Form1 : Form
    {
        wave_file_header header = new wave_file_header();
        public Form1()
        {
            InitializeComponent();
            header.initialize((uint)22050);

        }

        public double[] readingWave(String fileName)
        {
            List<double> outputList = new List<double>();
            BinaryReader reader = new BinaryReader(File.OpenRead(fileName));
            header.clear();
            header.ChunkID = reader.ReadInt32();
            header.ChunkSize = reader.ReadInt32();
            header.Format = reader.ReadInt32();
            header.SubChunk1ID = reader.ReadInt32();
            header.SubChunk1Size = reader.ReadInt32();
            header.AudioFormat = reader.ReadUInt16();
            header.NumChannels = reader.ReadUInt16();
            header.SampleRate = reader.ReadUInt32();
            header.ByteRate = reader.ReadUInt32();
            header.BlockAlign = reader.ReadUInt16();
            header.BitsPerSample = reader.ReadUInt16();
            header.SubChunk2ID = reader.ReadInt32();
            header.SubChunk2Size = reader.ReadInt32();

            int bytesPerSample = header.BitsPerSample / 8;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (bytesPerSample == 1) // 8-bit audio
                {
                    byte sample = reader.ReadByte();
                    outputList.Add(sample / 128.0);
                }
                else if (bytesPerSample == 4) // 32-bit audio
                {
                    float sample = reader.ReadSingle();
                    outputList.Add((double)sample);
                }
                else
                {
                    short sample = reader.ReadInt16(); // 16 bit audio
                    outputList.Add(sample);
                }
            }
            return outputList.ToArray();
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
                    double[] freqs = readingWave(selectedFilePath);
                    plotFreqWaveChart(freqs);
                }
            }
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

        
        public void plotFreqWaveChart(double[] audioData)
        {
            chart1.Series[0].Points.Clear();
            for (int m = 0; m < audioData.Length; m++)
            {
                chart1.Series[0].Points.AddXY(m, audioData[m]);
            }
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
