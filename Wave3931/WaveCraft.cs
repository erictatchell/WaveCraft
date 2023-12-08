/**
    Author: Eric Tatchell & Brendan Doyle
    Date: October & November 2023
 */
using System;
using System.Collections;
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

/***********************************************************************************************************************************************************
 *
 * File: WaveCraft.cs
 *
 * Purpose: This file defines the WaveCraft class, which is the main form of the WaveCraft app. It is responsible for the user interface and
 *          the creation of new wave analyzer forms and the opening of existing wave files through a user interface. It also contains the methods for
 *          cutting, copying, and pasting audio data, as well as the methods for playing back audio data and performing a Discrete Fourier Transform (DFT).
 * 
 * Authors: Eric Tatchell & Brendan Doyle
 * 
 * ***********************************************************************************************************************************************************/


namespace Wave3931
{
    public partial class WaveCraft : Form
    {
        // Class members declaration with descriptions

        // Instance for reading files and saving
        private wave_file_header header = new wave_file_header();

        // Instance checking which chart is selected, mainly for stereo
        private Chart selected;

        // Instance array for signal data/samples
        private double[] audioData;

        // Instance string for just the file name
        private string fileName;

        // Instance string for the full file path
        private string filePath;

        // Instance string checking the window selection
        private string WINDOW_TYPE;

        // Instance array for left channel audioData
        private double[] leftChannel;

        // Instance array for right channel audioData
        private double[] rightChannel;

        // Instance int tracking the original index for sample rate selection
        private int originalSampleRateIndex;

        // Instance int tracking the original index for bps selection
        private int originalBitsPerSampleIndex;

        // Instance int tracking the original index for thread selection
        private int originalDFTThreadIndex;

        // Instance int tracking the original index for windowing selection
        private int originalWindowingIndex;

        // Instance int tracking the user selected # of threads
        private int NUM_THREADS;

        // instance tracking bits per sample
        public static ushort BPS;

        // Instance boolean checking if the current file is edited
        public static bool EDITED;

        // Instance boolean checking if the current file is saved
        public static bool SAVED;

        // Insance for the start time used for benchmarking
        private DateTime startTime;

        // Instance for the vertical line animation
        private VerticalLineAnnotation verticalLine;

        // Instance for the animation duration
        private double animationDuration;

        // Instance to track sample rate while pasting
        private int sample_rate;

        /**
         * Method to get the sample rate of the audio file.
         * 
         * @return The sample rate of the audio file.
         */
        public double[] getAudioData()
        {
            return audioData;
        }

        /**
        * Method to get the sample rate of the audio file.
        *
        * @return The sample rate of the audio file.
        */
        public void setAudioData(double[] ad)
        {
            audioData = ad;
        }

        /**
         * Method to get the sample rate of the audio file, 4 available.
         *
         * @return The sample rate of the audio file.
         */
        public int detectSR()
        {
            if (fileName != null) // only for opened files
            {
                switch (header.SampleRate)
                {
                    case 8000:
                        SetSampleRate(8000);
                        return 0;
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
            }
            
            return 1; // default is 11025
        }

        /**
        * Method to get the bits per sample of the audio file, 2 choices available.
        *         *
        * @return The bits per sample of the audio file.
        */
        public int detectBPS()
        {
            if (fileName != null) // only opened files
            {
                switch (header.BitsPerSample)
                {
                    case 8:
                        SetBitsPerSample(8);
                        return 0;
                    case 16:
                        SetBitsPerSample(16);
                        return 1;
                }
            }

            return 0; // default is 8
        }

        /**
         * Initializes components and sets up the form with default values for the sample rate, bits per sample, and number of threads.
         *
         * @param filePath The full file path of the wave file.
         * @param fileName The name of the wave file.
         */
        public WaveCraft(String filePath, String fileName)
        {
            InitializeComponent();
            initPlayLine(); // creates the playback line
            this.fileName = filePath; // initialize instances
            this.filePath = fileName; // initialize instances
            this.Text = "WaveCraft - " + fileName; // changes the title bar

            Box(this.Handle); // creates an invisible DialogBox implementing dll.c's DlgProc, param: this window

            leftLabel.Visible = false; // "L" disabled until user's file selection is determined
            rightLabel.Visible = false; // "R" disabled until user's file selection is determined
            DFTThread.SelectedIndex = 0; // default threads is 1
            windowing.SelectedIndex = 0; // default windowing is Rectangle
            originalDFTThreadIndex = DFTThread.SelectedIndex; // save original
            originalWindowingIndex = windowing.SelectedIndex; // save original

            EDITED = false; // nothing has been done, user can exit safely

            // right click context menu on a chart for editing options
            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            LEFT_CHANNEL_CHART.ContextMenuStrip = cm;
            RIGHT_CHANNEL_CHART.ContextMenuStrip = cm;

            header.initialize(11025, 1, 8); // initialize wave_file_header with 11025hz, 1 channel, 8 bits per sample by default
            double[] signal = readingWave(this.fileName); // read wav file's wave
            sampleRate.SelectedIndex = detectSR(); // set UI and dll.c's sample rate
            comboBox1.SelectedIndex = detectBPS(); // set UI and dll.c's bits
            originalSampleRateIndex = sampleRate.SelectedIndex; // save original
            originalBitsPerSampleIndex = comboBox1.SelectedIndex; // save original
            
            // initial available buttons
            btnPlay.Enabled = true;
            btnRecord.Enabled = false;
            btnStopRecord.Enabled = true;
            btnDFT.Enabled = true;
            btnClear.Enabled = true;

            // set colours for UI sugar
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

            plotFreqWaveChart(signal); // finally, plot the wave
            LEFT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel; // enable zooming
            RIGHT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel; // enable zooming
        }
        
        /**
         * Initializes components and sets up the form with default values for the sample rate, bits per sample, and number of threads.
         */
        public WaveCraft()
        {
            InitializeComponent();
            initPlayLine(); // creates the playback line
            leftLabel.Visible = false; // "L" disabled until user's file selection is determined
            rightLabel.Visible = false; // "R" disabled until user's file selection is determined

            Box(this.Handle); // creates an invisible DialogBox implementing dll.c's DlgProc, param: this window

            sampleRate.SelectedIndex = 0; // default sample rate is 11025
            comboBox1.SelectedIndex = 1; // default bits per sample is 16
            DFTThread.SelectedIndex = 0; // default threads is 1
            windowing.SelectedIndex = 0; // default windowing tech is Rectangle
            originalSampleRateIndex = sampleRate.SelectedIndex; // save original
            originalBitsPerSampleIndex = comboBox1.SelectedIndex; // save original
            originalDFTThreadIndex = DFTThread.SelectedIndex; // save original
            originalWindowingIndex = windowing.SelectedIndex; // save original

            EDITED = false; // no changes made yet

            // initial available buttons
            btnPlay.Enabled = false;
            btnRecord.Enabled = false;
            btnDFT.Enabled = false;
            btnClear.Enabled = false;

            // right click context menu on a chart for editing options
            ContextMenuStrip cm = new ContextMenuStrip();
            ToolStripMenuItem copy = new ToolStripMenuItem("Copy", null, CopySelected);
            ToolStripMenuItem cut = new ToolStripMenuItem("Cut", null, CutSelected);
            ToolStripMenuItem paste = new ToolStripMenuItem("Paste", null, PasteSelected);
            cm.Items.Add(copy);
            cm.Items.Add(cut);
            cm.Items.Add(paste);
            LEFT_CHANNEL_CHART.ContextMenuStrip = cm;
            RIGHT_CHANNEL_CHART.ContextMenuStrip = cm;


            LEFT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel; // enable zooming
            RIGHT_CHANNEL_CHART.MouseWheel += Chart_MouseWheel; // enable zooming

            SAVED = false; // user cannot exit safely
        }

        /**
         * Method that handles the click and drag event of the mouse cursor on the chart for selecting an area to be copied.
         */
        private void CopySelected(object sender, EventArgs e)
        {
            double start = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            double end = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
            if (start == -1 || end == -1)
            {
                start = RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
                end = RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
            }

            int startIndex = (int)(start); // int typecast
            int endIndex = (int)(end); // int typecast

            // pretty much every edge case
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
            // take the area of audioData the user selected, put in selectedAudioData
            Array.Copy(audioData, startIndex, selectedAudioData, 0, selectedDataLength);

            byte[] byteData;

            if (BPS == 8) // 8 bit audio
            {
                byteData = selectedAudioData.Select(sample =>
                {
                    // it just works!
                    double scaledValue = (sample + 1) * 127.5;
                    scaledValue = Math.Max(0, Math.Min(255, scaledValue));
                    return (byte)scaledValue;
                }).ToArray();
            }
            else if (BPS == 16) // 16 bit audio
            {
                short[] shortData = selectedAudioData.Select(sample =>
                {
                    // it just works!
                    short scaledValue = (short)(sample * 32767.0);
                    return scaledValue;
                }).ToArray();

                byteData = new byte[selectedDataLength * sizeof(short)];
                Buffer.BlockCopy(shortData, 0, byteData, 0, byteData.Length);
            }
            else
            {
                return;
            }
            this.sample_rate = GetSampleRate();
            
            // save data to clipboard
            string base64Data = Convert.ToBase64String(byteData);
            Clipboard.SetText(base64Data);
        }

        /**
         * Method that handles the click and drag event of the mouse cursor on the chart for selecting an area to be cut.
         */
        private void CutSelected(object sender, EventArgs e)
        {
            double start = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            double end = LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
            selected = LEFT_CHANNEL_CHART;
            if (start == -1 || end == -1)
            {
                selected = RIGHT_CHANNEL_CHART;
                start = RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
                end = RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd;
            }
            int startIndex = (int)(start);
            int endIndex = (int)(end);

            // pretty much every edge case
            if (startIndex < 0) { startIndex = 0; }
            if (endIndex >= audioData.Length) { endIndex = audioData.Length - 1; }
            if (startIndex >= endIndex)
            {
                int temp = startIndex;
                startIndex = endIndex;
                endIndex = temp;
            }

            // get the selected portion of audioData
            int selectedDataLength = endIndex - startIndex + 1;
            double[] selectedAudioData = new double[selectedDataLength];
            Array.Copy(audioData, startIndex, selectedAudioData, 0, selectedDataLength);

            byte[] byteData;

            // check bit depth
            if (BPS == 8)
            {
                byteData = selectedAudioData.Select(sample =>
                {
                    double scaledValue = (sample + 1) * 127.5;
                    scaledValue = Math.Max(0, Math.Min(255, scaledValue));
                    return (byte)scaledValue;
                }).ToArray();
            }
            else if (BPS == 16)
            {
                short[] shortData = selectedAudioData.Select(sample =>
                {
                    short scaledValue = (short)(sample * 32767.0);
                    return scaledValue;
                }).ToArray();

                byteData = new byte[selectedDataLength * sizeof(short)];
                Buffer.BlockCopy(shortData, 0, byteData, 0, byteData.Length);
            }
            else
            {
                return;
            }

            string base64Data = Convert.ToBase64String(byteData);
            Clipboard.SetText(base64Data);

            List<double> newDataList = new List<double>(audioData);
            newDataList.RemoveRange(startIndex, selectedDataLength);
            audioData = newDataList.ToArray();

            byte[] byteArray = new byte[audioData.Length];

            IntPtr pSaveBuffer = IntPtr.Zero;
            if (BPS == 8)
            {
                byteArray = audioData.Select(sample =>
                {
                    double scaledValue = (sample + 1) * 127.5;
                    scaledValue = Math.Max(0, Math.Min(255, scaledValue));
                    byte byteValue = (byte)scaledValue;
                    return byteValue;
                }).ToArray();

                pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
                Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);
            }
            else if (BPS == 16)
            {
                short[] shortArray = audioData.Select(sample =>
                {
                    short scaledValue = (short)(sample * 32767.0);
                    return scaledValue;
                }).ToArray();

                pSaveBuffer = Marshal.AllocHGlobal(shortArray.Length * sizeof(short));
                Marshal.Copy(shortArray, 0, pSaveBuffer, shortArray.Length);
            }
            UpdatePSaveBuffer(pSaveBuffer, BPS == 8 ? byteArray.Length : audioData.Length * sizeof(short));

            // update the points
            selected.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length / (BPS / 8); i++)
            {
                selected.Series[0].Points.AddXY(i, audioData[i]);
            }
            this.sample_rate = GetSampleRate();

        }


        /**
        * Method that handles the click and drag event of the mouse cursor on the chart for selecting an area to be pasted.
        * 
        * 
        * -------------------- NOTE ----------------------------------------- *
        * Pasting 8 bit audio does NOT WORK AT THE MOMENT, only 16 bit works  *
        * ------------------------------------------------------------------- *
        */
        private void PasteSelected(object sender, EventArgs e)
        {
            string base64Data = Clipboard.GetText();
            byte[] byteData = Convert.FromBase64String(base64Data);
            int pasteIndex = (int)LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart;
            selected = LEFT_CHANNEL_CHART;
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

            int copiedDataLength;

            short[] shortData = null;
            if (BPS == 8)
            {
                copiedDataLength = byteData.Length;
            }
            else if (BPS == 16)
            {
                copiedDataLength = byteData.Length / sizeof(short);
                shortData = new short[copiedDataLength];
                Buffer.BlockCopy(byteData, 0, shortData, 0, byteData.Length);
            }
            else
            {
                // Handle other bit depths if needed
                return;
            }

            List<double> newDataList = new List<double>(audioData);

            if (BPS == 8)
            {
                newDataList.InsertRange(pasteIndex, byteData.Select(b => (double)b - 127.5).ToArray());
            }
            else if (BPS == 16)
            {
                newDataList.InsertRange(pasteIndex, shortData.Select(s => (double)s / 32767.0).ToArray());
            }

            audioData = newDataList.ToArray();

            selected.Series[0].Points.Clear();
            for (int i = 0; i < audioData.Length / (BPS / 8); i++)
            {
                selected.Series[0].Points.AddXY(i, audioData[i]);
            }

            IntPtr pSaveBuffer;
            byte[] byteArray = null;
            if (BPS == 8)
            {
                byteArray = audioData.Select(sample =>
                {
                    double scaledValue = (sample + 1) * 127.5;
                    scaledValue = Math.Max(0, Math.Min(255, scaledValue));
                    byte byteValue = (byte)scaledValue;
                    return byteValue;
                }).ToArray();

                pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
                Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);
            }
            else if (BPS == 16)
            {
                short[] shortArray = audioData.Select(sample =>
                {
                    short scaledValue = (short)(sample * 32767.0);
                    return scaledValue;
                }).ToArray();

                pSaveBuffer = Marshal.AllocHGlobal(shortArray.Length * sizeof(short));
                Marshal.Copy(shortArray, 0, pSaveBuffer, shortArray.Length);
            }
            else
            {
                return;
            }

            UpdatePSaveBuffer(pSaveBuffer, BPS == 8 ? byteArray.Length : audioData.Length * sizeof(short));

            Marshal.FreeHGlobal(pSaveBuffer);


            EDITED = true;
            SAVED = false;
        }

        /**
         * Method that handles reading the wave file and updating the chart with the audio data.
         *
         * @param sender The source of the event.
         * @param e Contains the event data.
         * 
         * @return The audio data.
         */
        public double[] readingWave(String file)
        {

            List<double> outputList = new List<double>();
            List<byte> byteList = new List<byte>();
            BinaryReader reader = new BinaryReader(File.OpenRead(file));
            header.clear();
            header.ChunkID = reader.ReadInt32();
            header.ChunkSize = reader.ReadInt32();
            header.Format = reader.ReadInt32();
            header.SubChunk1ID = reader.ReadInt32();
            header.SubChunk1Size = reader.ReadInt32();
            header.NumChannels = reader.ReadUInt16();
            header.AudioFormat = reader.ReadUInt16();
            header.SampleRate = reader.ReadUInt32();
            header.ByteRate = reader.ReadUInt32();
            header.BlockAlign = reader.ReadUInt16();
            header.BitsPerSample = reader.ReadUInt16();
            header.SubChunk2ID = reader.ReadInt32();
            header.SubChunk2Size = reader.ReadInt32();
            BPS = header.BitsPerSample;
            int counter = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (header.BitsPerSample == 8)
                {
                    byte sample = reader.ReadByte();
                    outputList.Add((sample / 127.5) - 1);
                    byteList.Add(sample);
                }
                else if (header.BitsPerSample == 16)
                {
                    short sample = reader.ReadInt16();
                    outputList.Add(sample / 32767.0);
                    byteList.AddRange(BitConverter.GetBytes(sample));
                }
                counter++;
            }


            audioData = outputList.ToArray();
            byte[] byteArray = byteList.ToArray();
            IntPtr pSaveBuffer = IntPtr.Zero;
            pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);

            UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);
            SetChannels(header.NumChannels);
            SetSampleRate(header.SampleRate);
            Marshal.FreeHGlobal(pSaveBuffer);
            return audioData;
        }

        /**
         * Method that handles demuxing the stereo audio data for both left and right channels.
         * 
         * @param stereoData The stereo audio data.
         * @param leftChannel The left channel audio data.
         * @param rightChannel The right channel audio data.
         */
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

        /**
         * Method for Muxing the left and right channel audio data into stereo audio data.
         * 
         * @param leftChannel The left channel audio data.
         * @param rightChannel The right channel audio data.
         */
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

        // Instance for Descrete Fourier Transform
        private DFT DFT;

        /**
         * Method for plotting the frequency and waveform charts.
         * 
         * @param audioData The audio data.
         */
        public void plotFreqWaveChart(double[] audioData)
        {
            int length = BPS == 8 ? audioData.Length : audioData.Length / 2;
            LEFT_CHANNEL_CHART.Series[0].Points.Clear();
            RIGHT_CHANNEL_CHART.Series[0].Points.Clear();

            if (header.NumChannels == 1 || header.NumChannels == 0)
            {
                // Mono audio data
                for (int m = 0; m < length; m++)
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

            if (fileName == null)
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

        /**
         * Method for clicking on the frequency chart.
         */
        private void chart2_Click(object sender, EventArgs e)
        {
            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart = -1;
            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd = -1;
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserEnabled = true;
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
        }

        /**
         * method for clicking on the menu strip located in the top of the form.
         */
        private void TopMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /**
         * Method for creating a vertical line annotation for the wave form line. (the lines you see you going up and down)
         */
        private void initPlayLine()
        {
            verticalLine = new VerticalLineAnnotation();
            verticalLine.AxisX = LEFT_CHANNEL_CHART.ChartAreas[0].AxisX;
            verticalLine.AllowMoving = false;
            verticalLine.IsInfinitive = true;
            verticalLine.ClipToChartArea = LEFT_CHANNEL_CHART.ChartAreas[0].Name;
            verticalLine.LineColor = System.Drawing.Color.Gold;
            verticalLine.LineWidth = 2;
            verticalLine.X = 0;
            LEFT_CHANNEL_CHART.Annotations.Add(verticalLine);
        }

        /**
         * Method for play button and triggers playback line.
         */
        private async void btnPlay_ClickAsync(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Waveform Audio Recorder");

            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, 0x0111, 1002, 0);
            }
            animationDuration = BPS == 8 ? (double)audioData.Length / GetSampleRate() : ((double)audioData.Length / 2) / GetSampleRate();

            startTime = DateTime.Now;
            await AnimateVerticalLine();
        }

        /**
         * Async method for animating the playback line.
         */
        private async Task AnimateVerticalLine()
        {
            while (true)
            {
                double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;

                if (elapsedSeconds >= animationDuration)
                {
                    break;
                }

                double position = (elapsedSeconds / animationDuration) * LEFT_CHANNEL_CHART.ChartAreas[0].AxisX.Maximum;
                verticalLine.X = position;
                LEFT_CHANNEL_CHART.Invalidate();
                await Task.Delay(15);

            }
        }


        /**
         * Async method for stopping the playback.
         */
        private async void btnStop_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Waveform Audio Recorder");
            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, 0x0111, 1001, 0);
            }
            double[] data = await WaitForDataAsync(BPS);
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

            EDITED = true;
            SAVED = false;
        }

        /**
         * Asynchronously waits for audio data to be available and processes it based on bits per sample (BPS).
         * 
         * @param BPS: The bits per sample to determine how the audio data should be processed.
         * 
         * @return: Returns a Task containing the processed audio data as an array of doubles.
         */
        private Task<double[]> WaitForDataAsync(int BPS)
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
                        pb = GetPBuffer();
                        double[] data = new double[dl];

                        if (BPS == 8)
                        {
                            for (int i = 0; i < dl; i++)
                            {
                                byte sample = Marshal.ReadByte(pb, i);
                                data[i] = (double)(sample / 127.5) - 1;
                            }
                        }
                        else if (BPS == 16)
                        {
                            for (int i = 0; i < dl / 2; i++)
                            {
                                short sample = Marshal.ReadInt16(pb, i * 2);
                                data[i] = (double)sample / 32767.0;
                            }
                        }


                        return data;
                    }
                    Thread.Sleep(1);
                }

                return new double[0];
            });
        }

        /**
         * Method for the record button to start recording audio.
         */
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

        /**
         * Method for benchmarking the Discrete Fourier Transform (DFT), changes based on the amount of threads selected by user.
         * 
         * @param data: The audio data to be processed.
         * @param numThreads: The number of threads to use for processing.
         */
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

        /**
         * Event handler for the DFT button, executes DFT on selected or entire audio data. 
         */
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

        /**
         * Event handler for painting the options panel.
         */
        private void OptionsPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        /**
         * Event handler for the clear button, clears the audio data from the form.
         */
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
            EDITED = false;
            SAVED = false;
        }

        /**
         * Method for clicking the new file button, opens a new wave file.
         */
        private void btnNew_Click(object sender, EventArgs e)
        {
            WaveCraft waveAnalyzerForm = new WaveCraft();
            waveAnalyzerForm.Show();
        }

        /**
        * Event handler for the save file button, saves the current wave file.         
        */
        private void btnSaveFile_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveFilePath = saveFileDialog.FileName;
                    if (fileName == null)
                    {
                        header.initialize((uint)GetSampleRate(), 1, BPS);
                        header.changeSR((uint)GetSampleRate());
                        using (BinaryWriter writer = new BinaryWriter(File.Create(saveFilePath)))
                        {
                            ushort wFormatTag = GET_wFormatTag();
                            ushort nChannels = GET_nChannels();
                            uint nSamplesPerSec = GET_nSamplesPerSec();
                            ushort wBitsPerSample = GET_wBitsPerSample();

                            header.ChunkID = wave_file_header.mmioStringToFOURCC("RIFF", 0);
                            header.SubChunk1ID = wave_file_header.mmioStringToFOURCC("fmt ", 0);
                            header.SubChunk1Size = 16;/*wBitsPerSample == 8 ? 8 : 16;*/
                            header.Format = wave_file_header.mmioStringToFOURCC("WAVE", 0);
                            header.NumChannels = nChannels;
                            header.SampleRate = nSamplesPerSec;
                            header.BitsPerSample = wBitsPerSample;
                            header.SubChunk2ID = wave_file_header.mmioStringToFOURCC("data", 0);

                            header.ByteRate = (uint)(header.SampleRate * header.NumChannels * header.BitsPerSample / 8);
                            header.BlockAlign = (ushort)(header.NumChannels * header.BitsPerSample / 8);

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

                            if (header.BitsPerSample == 8)
                            {
                                byte[] byteArray = audioData.Select(sample =>
                                {
                                    // Assuming audio samples range from -0.5 to 0.5
                                    byte byteValue = (byte)((sample + 1) * 127.5);
                                    return byteValue;
                                }).ToArray();
                                for (int i = 0; i < audioData.Length; i++)
                                {
                                    writer.Write(byteArray[i]);
                                }
                            } else
                            {
                                for (int i = 0; i < audioData.Length; i++)
                                {
                                    short sampleValue = (short)((audioData[i] * 32767) + 1);
                                    writer.Write(sampleValue);
                                }
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
                            //byte[] byteArray = CompressMRLE(audioData);

                            for (int i = 0; i < byteArray.Length; i++)
                            {
                                writer.Write(byteArray[i]);
                            }
                        }
                    }
                    this.Text = "WaveCraft - " + Path.GetFileName(saveFilePath);
                    this.fileName = saveFilePath;
                    SAVED = true;
                }
            }
        }

        /**
        * Compresses the input array using Modified Run-Length Encoding (MRLE).
        * 
        * @param inputArray The array of double values to be compressed.
        * @return An array of bytes representing the compressed data.
        */
        private byte[] CompressMRLE(double[] inputArray)
        {
            List<byte> compressedList = new List<byte>();

            int count = 1;
            for (int i = 1; i < inputArray.Length; i++)
            {
                if (inputArray[i] == inputArray[i - 1])
                {
                    count++;
                }
                else
                {
                    compressedList.Add((byte)count);
                    compressedList.Add((byte)inputArray[i - 1]);
                    count = 1;
                }
            }

            // Add the last run
            compressedList.Add((byte)count);
            compressedList.Add((byte)inputArray[inputArray.Length - 1]);

            return compressedList.ToArray();
        }

        /**
         * Event handler for clicking the exit button. Exits the application.
         */
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        /**
         * Event handler for clicking on the Left Channel Chart.
         * Resets selection and enables cursor functionality.
         */
        private void LeftChannelChart_Click(object sender, EventArgs e)
        {
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionStart = -1;
            LEFT_CHANNEL_CHART.ChartAreas[0].CursorX.SelectionEnd = -1;

            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserEnabled = true;
            RIGHT_CHANNEL_CHART.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            RIGHT_CHANNEL_CHART.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
        }

        /**
         * Event handler for mouse wheel action on charts.
         * Implements zooming functionality based on the cursor's position.
         */
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

        /**
         * Panel for the options menu.
         */
        private void panel2_Paint_2(object sender, PaintEventArgs e)
        {

        }

        /**
         * Event handler for changing the sample rate selection.
         * Updates the UI and sets the new sample rate in the header.
         */
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

        /**
         * Event handler for changing the DFT thread count selection.
         * Updates the UI and sets the new thread count.
         */
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

        /**
         * Event handler for changing the windowing selection.
         * Updates the UI and sets the new windowing type.
         */
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

        /**
         * Event handler for the 'Open' menu item click.
         * Opens a file dialog to select and open a new wave file.
         */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EDITED && !SAVED) // already saved or opened another file
            {
                if (audioData != null)
                {
                    DialogResult result = MessageBox.Show("Do you want to save before opening a new file?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        btnSaveFile_Click(sender, e);
                    }
                    else if (result == DialogResult.No)
                    {
                        openFile();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        openFile();
                    }
                }
            }
            else 
            {
                openFile();
            }
            
        }

        /**
         * Helper method to open a wave file using a file dialog.
         */
        private void openFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    WaveCraft waveAnalyzerForm = new WaveCraft(selectedFilePath, Path.GetFileName(selectedFilePath));
                    this.Close();
                    waveAnalyzerForm.Show();
                }
            }
        }

        /**
         * Event handler for changing the bits per sample selection.
         * Updates the UI and sets the new bits per sample.
         */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    BPS = 8;
                    SetBitsPerSample(BPS);
                    break;
                case 1:
                    BPS = 16;
                    SetBitsPerSample(BPS);
                    break;
            }
        }
    }
}
