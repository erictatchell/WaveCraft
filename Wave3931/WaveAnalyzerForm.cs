using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Wave3931.Externals;
using Timer = System.Windows.Forms.Timer;

namespace Wave3931
{
    public partial class WaveAnalyzerForm : Form
    {
        wave_file_header header = new wave_file_header();
        private double[] audioData;
        private String file;
        private String fileName;
        private string WINDOW_TYPE;
        private bool SELECT;

        private int originalSampleRateIndex;
        private int originalDFTThreadIndex;
        private int originalChannelIndex;
        private int originalWindowingIndex;

        private int NUM_THREADS;
        public double[] getAudioData()
        {
            return audioData;
        }
        public void setAudioData(double[] ad)
        {
            audioData = ad;
        }
        public void DFT_UpdatePSaveBuffer(IntPtr psb, int dwdl)
        {
            UpdatePSaveBuffer(psb, dwdl);
        }
        public int detectSR()
        {
            switch (header.SampleRate)
            {
                case 11025:
                    SetSampleRate(11025);
                    return 0;
                case 22050:
                    SetSampleRate(22050);
                    return 1;
                case 44100:
                    SetSampleRate(44100);
                    return 2;
            }
            return 0;
        }

        public int detectChannels()
        {
            switch (header.NumChannels)
            {
                case 1:
                    SetChannels(1);
                    return 0;
                case 2:
                    SetChannels(2);
                    return 1;
            }
            return 0;
        }

        public WaveAnalyzerForm(String filePath, String fileName)
        {
            InitializeComponent();
            this.file = filePath;
            this.fileName = fileName;
            this.Text = "WaveCraft - " + fileName;
            Box(this.Handle);
            leftLabel.Visible = false;
            rightLabel.Visible = false;
            DFTThread.SelectedIndex = 0;
            windowing.SelectedIndex = 0;
            originalDFTThreadIndex = DFTThread.SelectedIndex;
            originalWindowingIndex = windowing.SelectedIndex;

            SELECT = true;
            selectRadio.Checked = true;
            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            LEFT_CHANNEL_CHART.ContextMenuStrip = cm;

            header.initialize(11025);
            header.changeSR(11025);
            double[] freqs = readingWave(file);
            sampleRate.SelectedIndex = detectSR();
            originalSampleRateIndex = sampleRate.SelectedIndex;
            channelsBox.SelectedIndex = detectChannels();
            originalChannelIndex = channelsBox.SelectedIndex;
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

            plotFreqWaveChart(freqs);
        }
        public WaveAnalyzerForm()
        {
            InitializeComponent();
            Box(this.Handle);

            leftLabel.Visible = false;
            rightLabel.Visible = false;

            sampleRate.SelectedIndex = 0;
            DFTThread.SelectedIndex = 0;
            windowing.SelectedIndex = 0;
            channelsBox.SelectedIndex = 0;
            originalSampleRateIndex = sampleRate.SelectedIndex;
            originalDFTThreadIndex = DFTThread.SelectedIndex;
            originalWindowingIndex = windowing.SelectedIndex;
            originalChannelIndex = channelsBox.SelectedIndex;
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
            if (startIndex >= endIndex) {
                int temp = startIndex;
                startIndex = endIndex;
                endIndex = temp;
            }

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
            if (startIndex >= endIndex)
            {
                int temp = startIndex;
                startIndex = endIndex;
                endIndex = temp;
            }

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
        }



        private void PasteSelected(object sender, EventArgs e)
        {
            string base64Data = Clipboard.GetText();
            byte[] byteData = Convert.FromBase64String(base64Data);
            int pasteIndex = (int)LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;

            if (pasteIndex < 0)
            {
                pasteIndex = 0;
            }
            else if (pasteIndex > audioData.Length)
            {
                pasteIndex = audioData.Length;
            }

            int copiedDataLength = byteData.Length / sizeof(double);
            List<double> newDataList = new List<double>(audioData);
            newDataList.InsertRange(pasteIndex, new double[copiedDataLength]);
            audioData = newDataList.ToArray();
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

            byte[] byteArray = audioData.Select(sample =>
            {
                double scaledValue = 0;
                // Scale the value to the range [0, 1]
                if (file == null)
                {
                    scaledValue = (sample + 1) / 2.0;
                }
                else scaledValue = sample;
                

                // Convert the scaled value to an 8-bit representation
                byte byteValue = (byte)(scaledValue * 255);

                return byteValue;
            }).ToArray();

            IntPtr pSaveBuffer;
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
            int counter = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte sample = reader.ReadByte();
                outputList.Add(sample);
                counter++;
            }
            audioData = outputList.ToArray();
            byte[] byteArray = new byte[counter];

            for (int i = 0; i < audioData.Length; i++)
            {
                double sample = audioData[i];
                byte byteValue = (byte)(sample);

                byteArray[i] = byteValue;
            }
            IntPtr pSaveBuffer = IntPtr.Zero;
            pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);

            UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);
            SetWaveformData(header.Format, 1, header.SampleRate, header.ByteRate, header.BlockAlign, header.BitsPerSample, 0);
            SetWaveHdr(pSaveBuffer, byteArray.Length, 0, 0, 0x00000004 | 0x00000008, 0, 0);
            Marshal.FreeHGlobal(pSaveBuffer);
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
            if (file == null)
            {
                toolStripStatusLabel3.Text = string.Format("{0:F2}s at {1} Hz", (double)audioData.Length / GetSampleRate(), GetSampleRate());
            }
            else
            {
                toolStripStatusLabel3.Text = string.Format("{0}: {1:F2}s at {2} Hz ", fileName, (double)audioData.Length / GetSampleRate(), GetSampleRate());
            }
            LEFT_CHANNEL_CHART.Visible = true;
            RIGHT_CHANNEL_CHART.Visible = true;
            double[] rightData = new double[audioData.Length];
            for (int m = 0; m < audioData.Length; m++)
            {
                rightData[m] = 0;
                RIGHT_CHANNEL_CHART.Series[0].Points.AddXY(m, rightData[m]);
            }
            leftLabel.Visible = true;

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
                DFT = new DFT(audioData, GetSampleRate(), NUM_THREADS, WINDOW_TYPE, this);
                DFT.Owner = this;
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

            DFT = new DFT(selected, GetSampleRate(), NUM_THREADS, WINDOW_TYPE, this);
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
                    if (file == null)
                    {
                        header.initialize((uint)GetSampleRate());
                        header.changeSR((uint)GetSampleRate());
                        // Create a BinaryWriter to write the WAV file
                        using (BinaryWriter writer = new BinaryWriter(File.Create(saveFilePath)))
                        {
                            // Write the WAV header
                            // Assuming you have these values available
                            ushort wFormatTag = GET_wFormatTag();
                            ushort nChannels = GET_nChannels();
                            uint nSamplesPerSec = GET_nSamplesPerSec();
                            ushort wBitsPerSample = GET_wBitsPerSample();

                            // Manually set the missing fields
                            header.ChunkID = 0x46464952; // "RIFF" in ASCII
                            header.SubChunk1ID = 0x20746D66; // "fmt " in ASCII
                            header.SubChunk1Size = 16; // Size of the fmt chunk (16 for PCM)
                            header.AudioFormat = 1; // PCM format
                            header.NumChannels = nChannels;
                            header.SampleRate = nSamplesPerSec;
                            header.BitsPerSample = wBitsPerSample;
                            header.SubChunk2ID = 0x61746164; // "data" in ASCII

                            // Calculate other fields
                            header.ByteRate = (uint)(header.SampleRate * header.NumChannels * (header.BitsPerSample / 8));
                            header.BlockAlign = (ushort)(header.NumChannels * (header.BitsPerSample / 8));

                            // Now you can write the header to the file
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

                            // Convert audioData to byteArray and write to file
                            byte[] byteArray = audioData.Select(sample =>
                            {
                                byte byteValue = (byte)((sample + 1) * 127.5);

                                return byteValue;
                            }).ToArray();

                            for (int i = 0; i < byteArray.Length; i++)
                            {
                                writer.Write(byteArray[i]);
                            }
                        }
                    } else
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Create(saveFilePath)))
                        {
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
                            byte[] byteArray = audioData.Select(sample =>
                            {
                                byte byteValue = (byte)(sample);

                                return byteValue;
                            }).ToArray();

                            for (int i = 0; i < byteArray.Length; i++)
                            {
                                writer.Write(byteArray[i]);
                            }
                        }
                    }
                    this.Text = "WaveCraft - " + Path.GetFileName(saveFilePath);
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

        private void panel2_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void sampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            label1.ForeColor = comboBox.SelectedIndex != originalSampleRateIndex
                ? Color.FromArgb(250, 44, 109)
                : Color.White;

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    SetSampleRate(11025);
                    header.changeSR(11025);
                    break;
                case 1:
                    SetSampleRate(22050);
                    header.changeSR(22050);
                    break;
                case 2:
                    SetSampleRate(44100);
                    header.changeSR(44100);
                    break;
            }
        }

        private void DFTThread_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            label2.ForeColor = comboBox.SelectedIndex != originalDFTThreadIndex
                ? Color.FromArgb(250, 44, 109)
                : Color.White;

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    NUM_THREADS = 1;
                    break;
                case 1:
                    NUM_THREADS = 2;
                    break;
                case 2:
                    NUM_THREADS = 4;
                    break;
                case 3:
                    NUM_THREADS = 15;
                    break;
                case 4:
                    NUM_THREADS = 50;
                    break;
            }
        }

        private void windowing_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            label3.ForeColor = comboBox.SelectedIndex != originalWindowingIndex
                ? Color.FromArgb(250, 44, 109)
                : Color.White;

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    WINDOW_TYPE = "Rectangle";
                    break;
                case 1:
                    WINDOW_TYPE = "Triangle";
                    break;
                case 2:
                    WINDOW_TYPE = "Hamming";
                    break;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void channelsBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
