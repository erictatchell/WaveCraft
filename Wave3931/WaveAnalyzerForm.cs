using System;
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
using static Wave3931.WavePlayer;
using System.Threading;
using System.Diagnostics;
using MathNet.Numerics.Optimization;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace Wave3931
{
    public partial class WaveAnalyzerForm : Form
    {
        wave_file_header header = new wave_file_header();
        private double[] audioData;
        private readonly String file;
        private string WINDOW_TYPE;
        private bool SELECT;

        private int NUM_THREADS;
        public double[] getAudioData()
        {
            return audioData;
        }

        public WaveAnalyzerForm(String file)
        {
            InitializeComponent();
            this.file = file;
            Box(this.Handle);

            header.initialize(11025);
            double[] freqs = readingWave(file);
            plotFreqWaveChart(freqs);
            byte[] byteArray = new byte[audioData.Length];

            for (int i = 0; i < audioData.Length; i++)
            {
                double sample = audioData[i];
                byte byteValue = (byte)((sample + 1) * 127.5);

                byteArray[i] = byteValue;
            }
            IntPtr pSaveBuffer = IntPtr.Zero;
            pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);

            UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);

            Marshal.FreeHGlobal(pSaveBuffer);

            titleLeft.Visible = false;
            titleRight.Visible = false;
            hzToolStripMenuItem.Checked = true;
            threads_1.Checked = true;
            NUM_THREADS = 1;
            rectangleToolStripMenuItem.Checked = true;
            WINDOW_TYPE = "Rectangle";
            SELECT = true;
            selectRadio.Checked = true;
            btnPlay.Enabled = false;
            btnRecord.Enabled = false;
            btnDFT.Enabled = false;
            btnClear.Enabled = false;
            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            LEFT_CHANNEL_CHART.ContextMenuStrip = cm;
        }
        public WaveAnalyzerForm()
        {
            InitializeComponent();
            Box(this.Handle);

            titleLeft.Visible = false;
            titleRight.Visible = false;

            hzToolStripMenuItem.Checked = true;
            threads_1.Checked = true;
            NUM_THREADS = 1;
            rectangleToolStripMenuItem.Checked = true;
            WINDOW_TYPE = "Rectangle";
            SELECT = true;
            selectRadio.Checked = true;
            btnPlay.Enabled = false;
            btnRecord.Enabled = false;
            btnDFT.Enabled = false;
            btnClear.Enabled = false;

            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            LEFT_CHANNEL_CHART.ContextMenuStrip = cm;

        }
        private void CopySelected(object sender, EventArgs e)
        {
            double start = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            double end = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
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
            double start = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            double end = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
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
            LEFT_CHANNEL_CHART.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length; i++)
            {
                LEFT_CHANNEL_CHART.Series[0].Points.AddXY(i, audioData[i]);
            }

            // Update other chart properties as needed
        }



        private void PasteSelected(object sender, EventArgs e)
        {
            double pos = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            string base64Data = Clipboard.GetText();
            byte[] byteData = Convert.FromBase64String(base64Data);
            int pasteIndex = (int)(pos);

            if (pasteIndex < 0)
            {
                pasteIndex = 0;
            }
            else if (pasteIndex > audioData.Length)
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

            LEFT_CHANNEL_CHART.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length; i++)
            {
                LEFT_CHANNEL_CHART.Series[0].Points.AddXY(i, audioData[i]);
            }

            byte[] byteArray = new byte[audioData.Length];

            for (int i = 0; i < audioData.Length; i++)
            {
                double sample = audioData[i];
                byte byteValue = (byte)((sample + 1) * 127.5);

                byteArray[i] = byteValue;
            }
            IntPtr pSaveBuffer = IntPtr.Zero;
            pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);

            UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);

            Marshal.FreeHGlobal(pSaveBuffer);

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

            LEFT_CHANNEL_CHART.Series[0].Points.Clear();
            for (int m = 0; m < audioData.Length; m++)
            {
                LEFT_CHANNEL_CHART.Series[0].Points.AddXY(m, audioData[m]);
            }
            LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.Minimum = 0;
            toolStripStatusLabel1.Text = string.Format("{0:F2}s at {1} Hz", (double)audioData.Length / GetSampleRate(), GetSampleRate());
            LEFT_CHANNEL_CHART.Visible = true;
            RIGHT_CHANNEL_CHART.Visible = true;
            double[] rightData = new double[audioData.Length];
            for (int m = 0; m < audioData.Length; m++)
            {
                rightData[m] = 0;
                RIGHT_CHANNEL_CHART.Series[0].Points.AddXY(m, rightData[m]);
            }
            titleLeft.Visible = true;

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
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserEnabled = true;
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            if (!SELECT)
            {
                LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            } else
            {
                LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            }
        }


        private void TopMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Waveform Audio Recorder");

            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, 0x0111, 1002, 0);
            }
        }




        private async void btnStop_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Waveform Audio Recorder");
            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, 0x0111, 1001, 0);
            }
            double[] data = await WaitForDataAsync();
            audioData = data;
            plotFreqWaveChart(data);
            btnPlay.Enabled = true;
            btnRecord.Enabled = false;
            btnStopRecord.Enabled = true;
            btnDFT.Enabled = true;
            btnClear.Enabled = true;

            btnPlay.BackColor = Color.FromArgb(250, 14, 79);
            btnPlay.ForeColor = Color.White;

            btnStopRecord.BackColor = Color.FromArgb(250, 14, 79);
            btnStopRecord.ForeColor = Color.White;

            btnRecord.BackColor = Color.White;
            btnRecord.ForeColor = Color.Black;

            btnDFT.BackColor = Color.FromArgb(250, 14, 79);
            btnDFT.ForeColor = Color.White;

            btnClear.BackColor = Color.FromArgb(250, 14, 79);
            btnClear.ForeColor = Color.White;
        }

        private Task<double[]> WaitForDataAsync()
        {
            return Task.Run(() =>
            {
                int maxAttempts = 10;

                IntPtr pb;
                int dl;

                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    dl = GetDataLength();

                    if (dl > 0)
                    {
                        // normal
                        pb = GetPBuffer();
                        double[] data = new double[dl];
                        for (int i = 0; i < dl; i++)
                        {
                            byte sample = Marshal.ReadByte(pb, i);
                            data[i] = (((double)sample / 127.5) - 1);
                        }
                        return data;
                    }
                    Thread.Sleep(1);
                }
                // worst case
                return new double[0];
            });
        }




        private void btnRecord_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Waveform Audio Recorder");

            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, 0x0111, 1000, 0);
            }
            btnPlay.Enabled = false;
            btnRecord.Enabled = true;
            btnStopRecord.Enabled = false;
            btnClear.Enabled = false;
            btnDFT.Enabled = false; 
            
            btnRecord.BackColor = Color.FromArgb(250, 14, 79);
            btnRecord.ForeColor = Color.White;

            btnStopRecord.BackColor = Color.White;
            btnStopRecord.ForeColor = Color.Black;

            btnPlay.BackColor = Color.White;
            btnPlay.ForeColor = Color.Black;

            btnDFT.BackColor = Color.White;
            btnDFT.ForeColor = Color.Black;

            btnClear.BackColor = Color.White;
            btnClear.ForeColor = Color.Black;
        }

        private void btnDFT_Click(object sender, EventArgs e)
        {
            int start = (int)LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            int end = (int)LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
            if (start >= end)
            {
                DFT = new DFT(audioData, GetSampleRate(), NUM_THREADS, WINDOW_TYPE);
                DFT.Owner = this; // Set the owner if needed
                DFT.Show();
                return;
            }
            

            if (start < 0) start = 0;
            if (end >= audioData.Length) end = audioData.Length - 1;
            if (start >= end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            double[] selected = new double[end - start + 1];
            int j = 0;
            for (int i = start; i < end; i++)
            {
                selected[j] = audioData[i];
                j++;
            }

            DFT = new DFT(selected, GetSampleRate(), NUM_THREADS, WINDOW_TYPE);
            DFT.Owner = this; // Set the owner if needed
            DFT.Show();
        }


        private void OptionsPanel_Paint(object sender, PaintEventArgs e)
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
            audioData = new double[0];
            plotFreqWaveChart(audioData);
            btnPlay.Enabled = false;
            btnRecord.Enabled = false;
            btnDFT.Enabled = false;
            btnClear.Enabled = false;
            btnRecord.BackColor = Color.White;
            btnRecord.ForeColor = Color.Black;
            btnPlay.BackColor = Color.White;
            btnPlay.ForeColor = Color.Black;
            btnDFT.BackColor = Color.White;
            btnDFT.ForeColor = Color.Black;
            btnClear.BackColor = Color.White;
            btnClear.ForeColor = Color.Black;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            WaveAnalyzerForm waveAnalyzerForm = new WaveAnalyzerForm();
            waveAnalyzerForm.Show();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveFilePath = saveFileDialog.FileName;

                    // Create a BinaryWriter to write the WAV file
                    using (BinaryWriter writer = new BinaryWriter(File.Create(saveFilePath)))
                    {
                        // Write the WAV header
                        writer.Write(header.ChunkID);
                        writer.Write(header.ChunkSize);
                        writer.Write(header.Format);
                        writer.Write(header.SubChunk1ID);
                        writer.Write(header.SubChunk1Size);
                        writer.Write(header.AudioFormat);
                        writer.Write(header.NumChannels);
                        writer.Write(header.SampleRate);
                        writer.Write(header.ByteRate);
                        writer.Write(header.BlockAlign);
                        writer.Write(header.BitsPerSample);
                        writer.Write(header.SubChunk2ID);
                        writer.Write(header.SubChunk2Size);

                        // Write the audio data
                        for (int i = 0; i < audioData.Length; i++)
                        {
                            if (header.BitsPerSample == 8)
                            {
                                // 8-bit audio (convert to bytes)
                                byte sample = (byte)(audioData[i] * 128.0);
                                writer.Write(sample);
                            }
                            else if (header.BitsPerSample == 16)
                            {
                                // 16-bit audio (short)
                                short sample = (short)audioData[i];
                                writer.Write(sample);
                            }
                            else if (header.BitsPerSample == 32)
                            {
                                // 32-bit audio (float)
                                float sample = (float)audioData[i];
                                writer.Write(sample);
                            }
                            // Add support for other bit depths if needed
                        }
                    }
                }
            }
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

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.ZoomReset();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        // 11025
        void hzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sampleRateToolStripMenuItem.Text = "Sample Rate: 11025 Hz";
            sampleRateToolStripMenuItem.ForeColor = Color.Black;
            hzToolStripMenuItem1.Checked = false;
            hzToolStripMenuItem2.Checked = false;
            hzToolStripMenuItem.Checked = true;
            SetSampleRate(11025);
        }

        // 22050
        private void hzToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sampleRateToolStripMenuItem.Text = "Sample Rate: 22050 Hz";
            sampleRateToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            hzToolStripMenuItem.Checked = false;
            hzToolStripMenuItem2.Checked = false;
            hzToolStripMenuItem1.Checked = true;
            SetSampleRate(22050);
        }

        // 44100
        private void hzToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            sampleRateToolStripMenuItem.Text = "Sample Rate: 44100 Hz";
            sampleRateToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            hzToolStripMenuItem.Checked = false;
            hzToolStripMenuItem1.Checked = false;
            hzToolStripMenuItem2.Checked = true;
            SetSampleRate(44100);
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            NUM_THREADS = 1;
            dFTThreadsToolStripMenuItem.Text = "DFT Threads: 1";
            dFTThreadsToolStripMenuItem.ForeColor = Color.Black;
            threads_2.Checked = false;
            threads_4.Checked = false;
            threads_15.Checked = false;
            threads_50.Checked = false;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            NUM_THREADS = 50;
            dFTThreadsToolStripMenuItem.Text = "DFT Threads: 50";
            dFTThreadsToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            threads_1.Checked = false;
            threads_2.Checked = false;
            threads_4.Checked = false;
            threads_15.Checked = false;
        }

        private void threads_2_Click(object sender, EventArgs e)
        {
            NUM_THREADS = 2;
            dFTThreadsToolStripMenuItem.Text = "DFT Threads: 2";
            dFTThreadsToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            threads_1.Checked = false;
            threads_4.Checked = false;
            threads_15.Checked = false;
            threads_50.Checked = false;
        }

        private void threads_4_Click(object sender, EventArgs e)
        {
            NUM_THREADS = 4;
            dFTThreadsToolStripMenuItem.Text = "DFT Threads: 4";
            dFTThreadsToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            threads_1.Checked = false;
            threads_2.Checked = false;
            threads_15.Checked = false;
            threads_50.Checked = false;

        }

        private void threads_15_Click(object sender, EventArgs e)
        {
            NUM_THREADS = 15;
            dFTThreadsToolStripMenuItem.Text = "DFT Threads: 15";
            dFTThreadsToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            threads_1.Checked = false;
            threads_2.Checked = false;
            threads_4.Checked = false;
            threads_50.Checked = false;
        }

        private void dFTThreadsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            windowingToolStripMenuItem.Text = "Windowing: Rectangle";
            WINDOW_TYPE = "Rectangle";
            windowingToolStripMenuItem.ForeColor = Color.Black;
            triangleToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            windowingToolStripMenuItem.Text = "Windowing: Triangle";
            WINDOW_TYPE = "Triangle";
            windowingToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            rectangleToolStripMenuItem.Checked = false;
            hammingToolStripMenuItem.Checked = false;
        }

        private void hammingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            windowingToolStripMenuItem.Text = "Windowing: Hamming";
            WINDOW_TYPE = "Hamming";
            windowingToolStripMenuItem.ForeColor = Color.FromArgb(220, 7, 60);
            rectangleToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = false;
        }

        private void sampleRateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void windowingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SELECT = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SELECT = true;
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
