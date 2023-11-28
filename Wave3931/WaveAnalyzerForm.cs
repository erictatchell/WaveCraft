using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Wave3931.Externals;

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
        double[] leftChannel;
        double[] rightChannel;
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
        public int detectSR()
        {
            if (file != null)
            {
                switch (header.SampleRate)
                {
                    case 8000:
                        SetSampleRate(8000);
                        return 0;
                    case 11025:
                        SetSampleRate(11025);
                        return 1;
                    case 22050:
                        SetSampleRate(22050);
                        return 2;
                    case 44100:
                        SetSampleRate(44100);
                        return 3;
                }
            }
            
            return 1;
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
            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            LEFT_CHANNEL_CHART.ContextMenuStrip = cm;
            RIGHT_CHANNEL_CHART.ContextMenuStrip = cm;

            header.initialize(11025);
            double[] freqs = readingWave(file);
            sampleRate.SelectedIndex = 0;
            originalSampleRateIndex = sampleRate.SelectedIndex;
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
            LEFT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel;
            RIGHT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel;
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
            originalSampleRateIndex = sampleRate.SelectedIndex;
            originalDFTThreadIndex = DFTThread.SelectedIndex;
            originalWindowingIndex = windowing.SelectedIndex;
            SELECT = true;
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
            RIGHT_CHANNEL_CHART.ContextMenuStrip = cm;
            LEFT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel;
            RIGHT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel;
        }

        private void CopySelected(object sender, EventArgs e)
        {
            double start = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            double end = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;

            if (start == -1 || end == -1)
            {
                start = RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
                end = RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
            }

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
            double[] selectedAudioData = new double[selectedDataLength];
            Array.Copy(audioData, startIndex, selectedAudioData, 0, selectedDataLength);

            byte[] byteData = new byte[selectedDataLength * sizeof(double)];
            Buffer.BlockCopy(selectedAudioData, 0, byteData, 0, byteData.Length);
            string base64Data = Convert.ToBase64String(byteData);
            Clipboard.SetText(base64Data);

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
            Chart selected = LEFT_CHANNEL_CHART;
            if (pasteIndex == -1)
            {
                pasteIndex = (int)RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
                selected = RIGHT_CHANNEL_CHART;
            }

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

            selected.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length; i++)
            {
                selected.Series[0].Points.AddXY(i, audioData[i]);
            }

            byte[] byteArray = audioData.Select(sample =>
            {
                double scaledValue = 0;
                if (file == null)
                {
                    scaledValue = (sample + 1) / 2.0;
                }
                else scaledValue = sample;
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
            SetChannels(header.NumChannels);
            SetSampleRate(header.SampleRate);
            Marshal.FreeHGlobal(pSaveBuffer);
            return outputList.ToArray();
        }

        private void DemuxStereoData(double[] stereoData, out double[] leftChannel, out double[] rightChannel)
        {
            int numSamples = stereoData.Length / 2;
            leftChannel = new double[numSamples];
            rightChannel = new double[numSamples];

            for (int i = 0; i < numSamples; i++)
            {
                leftChannel[i] = stereoData[2 * i];
                rightChannel[i] = stereoData[2 * i + 1];
            }
        }

        private double[] MuxStereoData(double[] leftChannel, double[] rightChannel)
        {
            int numSamples = leftChannel.Length;
            double[] stereoData = new double[2 * numSamples];

            for (int i = 0; i < numSamples; i++)
            {
                stereoData[2 * i] = leftChannel[i];
                stereoData[2 * i + 1] = rightChannel[i];
            }

            return stereoData;
        }

        private DFT DFT;
        public void plotFreqWaveChart(double[] audioData)
        {

            LEFT_CHANNEL_CHART.Series[0].Points.Clear();
            RIGHT_CHANNEL_CHART.Series[0].Points.Clear();

            if (header.NumChannels == 1 || header.NumChannels == 0)
            {
                // Mono audio data
                for (int m = 0; m < audioData.Length; m++)
                {
                    LEFT_CHANNEL_CHART.Series[0].Points.AddXY(m, audioData[m]);
                }

                LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.Minimum = 0;
                leftLabel.Visible = true;
            }
            else if (header.NumChannels == 2)
            {
                // Stereo audio data
                int numSamples = audioData.Length / 2;

                leftChannel = new double[numSamples];
                rightChannel = new double[numSamples];
                    
                for (int m = 0; m < numSamples; m++)
                {
                    leftChannel[m] = audioData[2 * m];
                    rightChannel[m] = audioData[2 * m + 1];

                    LEFT_CHANNEL_CHART.Series[0].Points.AddXY(m, leftChannel[m]);
                    RIGHT_CHANNEL_CHART.Series[0].Points.AddXY(m, rightChannel[m]);
                }

                LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.Minimum = 0;
                RIGHT_CHANNEL_CHART.ChartAreas[0].AxisX.Minimum = 0;

                leftLabel.Visible = true;
                rightLabel.Visible = true;
            }

            if (file == null)
            {
                toolStripStatusLabel3.Text = string.Format("{0:F2}s  -  {1} Hz  -  Channels: {2}", (double)audioData.Length / GetSampleRate(), GetSampleRate(), header.NumChannels != 0 ? header.NumChannels : 1);
            }
            else
            {
                toolStripStatusLabel3.Text = string.Format("{0}: {1:F2}s  -  {2} Hz  -  Channels: {3}", fileName, (double)audioData.Length / GetSampleRate(), GetSampleRate(), header.NumChannels);
            }

            LEFT_CHANNEL_CHART.Visible = true;
            RIGHT_CHANNEL_CHART.Visible = true;
        }



        private void chart2_Click(object sender, EventArgs e)
        {
            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart = -1;
            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd = -1;
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

        private void BenchmarkDFT(double[] data, int numThreads)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1Thread = new Stopwatch();

            if (numThreads == 1)
            {
                stopwatch.Start();
                Task.Run(() =>
                {
                    stopwatch1Thread.Start();
                    DFT = new DFT(data, GetSampleRate(), numThreads, WINDOW_TYPE, this);
                    DFT.Owner = this;
                    Invoke(new Action(() =>
                    {
                        DFT.Show();
                        stopwatch.Stop();
                        stopwatch1Thread.Stop();
                        DFT.UpdateBenchmarkLabel(stopwatch.Elapsed, stopwatch1Thread.Elapsed, numThreads);
                    }));
                });
            }
            else
            {
                stopwatch.Start();
                Task.Run(() =>
                {
                    DFT = new DFT(data, GetSampleRate(), numThreads, WINDOW_TYPE, this);
                    DFT.Owner = this;
                    Invoke(new Action(() =>
                    {
                        DFT.Show();
                        stopwatch.Stop();
                    }));
                });

                Task.Run(() =>
                {
                    stopwatch1Thread.Start();
                    DFT oneThreadDFT = new DFT(data, GetSampleRate(), 1, WINDOW_TYPE, this);
                    Thread.Sleep(20); // allow UI to update
                    Invoke(new Action(() =>
                    {
                        stopwatch1Thread.Stop();
                        DFT.UpdateBenchmarkLabel(stopwatch.Elapsed, stopwatch1Thread.Elapsed, numThreads);
                    }));
                });
            }
        }




        private void btnDFT_Click(object sender, EventArgs e)
        {
            int start = (int)LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            int end = (int)LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;

            if (start >= end)
            {
                BenchmarkDFT(audioData, NUM_THREADS);
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

            BenchmarkDFT(selected, NUM_THREADS);
        }




        private void OptionsPanel_Paint(object sender, PaintEventArgs e)
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

                            header.ByteRate = (uint)(header.SampleRate * header.NumChannels * (header.BitsPerSample / 8));
                            header.BlockAlign = (ushort)(header.NumChannels * (header.BitsPerSample / 8));

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
                                // Assuming audio samples range from -0.5 to 0.5
                                byte byteValue = (byte)((sample + 0.5) * 255);
                                return byteValue;
                            }).ToArray();

                            for (int i = 0; i < byteArray.Length; i++)
                            {
                                writer.Write(byteArray[i]);
                            }
                        }
                    }
                    else
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
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart = -1;
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd = -1;

            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserEnabled = true;
            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            if (!SELECT)
            {
                RIGHT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            }
            else
            {
                RIGHT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            }
        }
        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            Chart chart = (Chart)sender;
            Point cursorPos = e.Location;
            double cursorX = chart.ChartAreas[0].AxisX.PixelPositionToValue(cursorPos.X);

            double zoomFactor = (e.Delta > 0) ? 1.1 : 0.9;

            double xMin = chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double xMax = chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;

            double newXMin = cursorX + (xMin - cursorX) / zoomFactor;
            double newXMax = cursorX + (xMax - cursorX) / zoomFactor;
            chart.ChartAreas[0].AxisX.ScaleView.Zoom(newXMin, newXMax);
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            if (newXMax - newXMin < 500)
            {
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            }
            else
            {
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
        }






        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SELECT = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SELECT = true;
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
    }
}
