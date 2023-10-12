﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace Wave3931
{
    public partial class WaveAnalyzerForm : Form
    {
        wave_file_header header = new wave_file_header();
        private double[] audioData;
        private readonly String file;

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

        public WaveAnalyzerForm(String file)
        {
            InitializeComponent();
            this.file = file;
            header.initialize(22050);
            double[] freqs = readingWave(file);
            plotFreqWaveChart(freqs);
            selectToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            RightChannelChart.ContextMenuStrip = cm;
        }
        private void CopySelected(object sender, EventArgs e)
        {
            double start = RightChannelChart.ChartAreas[0].CursorX.SelectionStart;
            double end = RightChannelChart.ChartAreas[0].CursorX.SelectionEnd;
            int startIndex = (int)(start);
            int endIndex = (int)(end);

            if (startIndex < 0) { startIndex = 0; }
            if (endIndex >= audioData.Length) { endIndex = audioData.Length - 1; }
            if (startIndex >= endIndex) { return; }

            int selectedDataLength = endIndex - startIndex + 1;
            double[] selectedAudioData = new double[selectedDataLength];
            Array.Copy(audioData, startIndex, selectedAudioData, 0, selectedDataLength);
            byte[] byteData = new byte[selectedDataLength * sizeof(double)];
            Buffer.BlockCopy(selectedAudioData, 0, byteData, 0, byteData.Length);
            string base64Data = Convert.ToBase64String(byteData);
            Clipboard.SetText(base64Data);
        }
        private void CutSelected(object sender, EventArgs e)
        {
            double start = RightChannelChart.ChartAreas[0].CursorX.SelectionStart;
            double end = RightChannelChart.ChartAreas[0].CursorX.SelectionEnd;
            int startIndex = (int)(start);
            int endIndex = (int)(end);

            if (startIndex < 0) { startIndex = 0; }
            if (endIndex >= audioData.Length) { endIndex = audioData.Length - 1; }
            if (startIndex >= endIndex) { return; }

            int selectedDataLength = endIndex - startIndex + 1;

            // Copy the selected data to the clipboard
            double[] selectedAudioData = new double[selectedDataLength];
            Array.Copy(audioData, startIndex, selectedAudioData, 0, selectedDataLength);
            byte[] byteData = new byte[selectedDataLength * sizeof(double)];
            Buffer.BlockCopy(selectedAudioData, 0, byteData, 0, byteData.Length);
            string base64Data = Convert.ToBase64String(byteData);
            Clipboard.SetText(base64Data);

            // Remove the selected data from audioData
            List<double> newDataList = new List<double>(audioData);
            newDataList.RemoveRange(startIndex, selectedDataLength);
            audioData = newDataList.ToArray();

            // Clear the chart and update it with the modified data
            RightChannelChart.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length; i++)
            {
                RightChannelChart.Series[0].Points.AddXY(i, audioData[i]);
            }

            // Update other chart properties as needed
        }



        private void PasteSelected(object sender, EventArgs e)
        {
            double pos = RightChannelChart.ChartAreas[0].CursorX.SelectionStart;
            string base64Data = Clipboard.GetText();
            byte[] byteData = Convert.FromBase64String(base64Data);
            int pasteIndex = (int)(pos);
            if (pasteIndex < 0)
            {
                pasteIndex = 0;
            }
            else if (pasteIndex >= audioData.Length)
            {
                pasteIndex = audioData.Length;
            }
            int copiedDataLength = byteData.Length / sizeof(double);

            // Expand the audioData array to accommodate the pasted data
            List<double> newDataList = new List<double>(audioData);
            newDataList.InsertRange(pasteIndex, new double[copiedDataLength]);
            audioData = newDataList.ToArray();

            // Copy the pasted data into audioData
            double[] copiedAudioData = new double[copiedDataLength];
            Buffer.BlockCopy(byteData, 0, copiedAudioData, 0, byteData.Length);
            for (int i = 0; i < copiedDataLength; i++)
            {
                audioData[pasteIndex + i] = copiedAudioData[i];
            }

            // Clear the chart and update it with the modified data
            RightChannelChart.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length; i++)
            {
                RightChannelChart.Series[0].Points.AddXY(i, audioData[i]);
            }
        }




        public WaveAnalyzerForm()
        {
            InitializeComponent();
            header.initialize(22050);
            selectToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
            /*double[] freqs = readingWave();
            plotFreqWaveChart(freqs);*/
        }

        public double[] readingWave(String file)
        {
            List<double> outputList = new List<double>();
            BinaryReader reader = new BinaryReader(File.OpenRead(file));
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
            DFT = new DFT(outputList.ToArray());
            toolStripStatusLabel1.Text = "File Path: " + file;
            audioData = outputList.ToArray();
            return outputList.ToArray();
        }

        private DFT DFT;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void plotFreqWaveChart(double[] audioData)
        {
            RightChannelChart.Series[0].Points.Clear();
            for (int m = 0; m < audioData.Length; m++)
            {
                RightChannelChart.Series[0].Points.AddXY(m, audioData[m]);
            }
            RightChannelChart.ChartAreas[0].AxisX.Minimum = 0;

        }



        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {
            RightChannelChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            RightChannelChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            if (zoomToolStripMenuItem.Checked)
            {
                RightChannelChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            } else
            {
                RightChannelChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            }
        }


        private void TopMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnRecord_Click(object sender, EventArgs e)
        {

        }

        private void btnDFT_Click(object sender, EventArgs e)
        {
            DFT.Owner = this; // Set the owner if needed
            DFT.Show();
        }

        private void OptionsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnSamples_Click(object sender, EventArgs e)
        {

        }

        private void btnLength_Click(object sender, EventArgs e)
        {

        }

        private void btnThreading_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            WaveAnalyzerForm waveAnalyzerForm = new WaveAnalyzerForm();
            waveAnalyzerForm.Show();
        }

        // IGNORE
        private void btnOpenFile_Click(object sender, EventArgs e)
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

        private void btnSaveFile_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void LeftChannelChart_Click(object sender, EventArgs e)
        {

        }

        private void selectionOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = true;
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RightChannelChart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
        }
    }
}
