/**
    Author: Eric Tatchell & Brendan Doyle
    Date: October & November 2023
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

/***********************************************************************************************************************************************************
 *
 * File: DFT.cs
 *
 * Purpose: This file defines the DFT class, which is responsible for performing Discrete Fourier Transform (DFT) analysis on a given signal.
 *          By default, the DFT class uses a rectangular windowing function, but the user can choose between a rectangular, triangular, or Hamming window.
 *          Once the DFT is performed, the user can apply a lowpass or highpass filter to the signal.
 ***********************************************************************************************************************************************************/


namespace Wave3931
{
    /**
    * Class DFT, inheriting from Form. This class represents a form for performing Discrete Fourier Transform (DFT) on audio data,
    * with functionalities for lowpass and highpass filtering.
    */
    public partial class DFT : Form
    {
        private static int threads;

        // Mutex object for thread sync
        private readonly object lockObject = new object();

        // Thread-specific amplitude array for DFT calculation
        private double[] threadAmplitude;

        // Type of windowing function used for DFT
        private string windowType;

        // Result arrays for DFT calculations, real and full complex
        private double[] dftRes;
        private Complex[] cmplx_dftRes;

        // Reference to the main WaveCraft form, mainly for plotting and getting data
        private WaveAnalyzerForm main = null;

        /**
         * Retrieves the benchmark label.
         */
        public Label GetBenchmarkLabel()
        {
            return benchmark;
        }

        /**
         * Updates the benchmark label with the given performance metrics.
         * 
         * @Param timeSpan The time span to display.
         * @Param timeSpan2 The time span to compare to.
         * @Param numThreads The number of threads used.
         */
        public void UpdateBenchmarkLabel(TimeSpan timeSpan, TimeSpan timeSpan2, int numThreads)
        {
            double ratio = (double)timeSpan2.TotalMilliseconds / (double)timeSpan.TotalMilliseconds;
            if (numThreads == 1)
            {
                benchmark.Text = $"Benchmark (1 thread): {timeSpan.TotalMilliseconds} ms";
                label1.Text = "";
                label2.Text = "";
            }
            else if (ratio <= 1)
            {
                benchmark.Text = $"Benchmark: {numThreads} threads is {ratio:f2}x slower than 1 thread";
                label1.Text = $"1 thread: {timeSpan2.TotalMilliseconds} ms";
                label2.Text = $"{numThreads} threads: {timeSpan.TotalMilliseconds} ms";
            } else
            {
                benchmark.Text = $"Benchmark: {numThreads} threads is {ratio:f2}x faster than 1 thread";
                label1.Text = $"1 thread: {timeSpan2.TotalMilliseconds} ms";
                label2.Text = $"{numThreads} threads: {timeSpan.TotalMilliseconds} ms";
            }
        }


        /**
        * Constructor for the DFT class.
        * 
        * @Param s The input signal.
        * @Param sampleRate The sample rate of the input signal.
        * @Param threads The number of threads to use for parallel processing.
        * @Param windowType The type of windowing function to apply.
        * @Param calling The calling form.
        */
        public DFT(double[] s, double sampleRate, int threads, string windowType, Form calling)
        {
            InitializeComponent();

            DFT.threads = threads;
            label2.Text = $"{threads} threads: ... ";
            this.windowType = windowType;
            this.Text = "DFT - " + windowType + " Windowing";

            int N = s.Length;
            Complex[] dftResult = threadedDFTFunc(s, N, threads);
            cmplx_dftRes = new Complex[N];
            cmplx_dftRes = dftResult;
            double[] frequencies = new double[N];
            for (int i = 0; i < N; i++)
            {
                frequencies[i] = i * sampleRate / N;
            }
            double threshold = 10000;
            Plot(dftResult, threshold, N);

            chart1.ChartAreas[0].AxisX.Minimum = 0;

            chart1.ChartAreas[0].AxisY.Title = "Amplitude";
            chart1.ChartAreas[0].AxisX.Title = "Frequency (Hz)";
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            main = calling as WaveAnalyzerForm;
        }

        /**
         * Plots the DFT result on the chart.
         * 
         * @Param dftResult The result of the DFT.
         * @Param threshold The threshold for plotting.
         * @Param N The size of the DFT result.
         */
        public void Plot(Complex[] dftResult, double threshold, int N)
        {
            double max = 0;
            for (int i = 0; i < N; i++)
            {
                if (dftResult[i].Magnitude < threshold)
                {
                    chart1.Series[0].Points.AddXY(i, dftResult[i].Magnitude);
                    if (i > 0 && dftResult[i].Magnitude > max)
                    {
                        max = dftResult[i].Magnitude;
                    }
                }
            }
        }

        /**
         * Plots the DFT result on the chart.
         * 
         * @Param dftResult The result of the DFT.
         * @Param threshold The threshold for plotting.
         * @Param N The size of the DFT result.
         */
        public void Plot(double[] dftResult, double threshold, int N)
        {
            for (int i = 0; i < N; i++)
            {
                chart1.Series[0].Points.AddXY(i, dftResult[i]);
            }
        }

        /**
         * Performs DFT on a signal.
         * 
         * @Param s The input signal.
         * @Param n The size of the input signal.
         * @Param threadNum The current thread number.
         * @Param maxThreads The maximum number of threads to use.
         * 
         * @Return The complex result of the DFT.
         */
        public Complex[] dft(double[] s, int n, int threadNum, int maxThreads)
        {
            Complex[] cmplx = new Complex[n];
            double[] window = GenerateWindow(n);

            int pointsPerThread = n / maxThreads;
            int startIndex = threadNum * pointsPerThread;
            int endIndex = (threadNum == maxThreads - 1) ? n : (threadNum + 1) * pointsPerThread;

            for (int f = startIndex; f < endIndex; f++)
            {
                double re = 0;
                double im = 0;

                for (int t = 0; t < n - 1; t++)
                {
                    double windowedSample = s[t] * window[t];
                    re += windowedSample * Math.Cos(2 * Math.PI * t * f / n);
                    im -= windowedSample * Math.Sin(2 * Math.PI * t * f / n);
                }

                cmplx[f] = new Complex(re, im);

            }
            dftRes = new double[n];
            for (int i = 0; i < cmplx.Length; i++)
            {
                dftRes[i] = cmplx[i].Real;
            }

            return cmplx;
        }

        /**
         * Performs windoing on a frequency array based on the specified window type.
         * 
         * @Param n The length of the array.
         * 
         * @Return The windowed array
         */
        private double[] GenerateWindow(int n)
        {
            double[] window = new double[n];

            switch (windowType)
            {
                case "Rectangle":
                    // Rectangle
                    for (int i = 0; i < n; i++)
                    {
                        window[i] = 1.0;
                    }
                    break;

                case "Triangle":
                    // Triangle window
                    for (int i = 0; i < n; i++)
                    {
                        window[i] = 1.0 - Math.Abs((i - (n - 1) / 2.0) / ((n - 1) / 2.0));
                    }
                    break;
                case "Hamming":
                    for (int i = 0; i < n; i++)
                    {
                        // Hamming window
                        window[i] = (0.54 - 0.46 * Math.Cos(2 * Math.PI * i / (n - 1)));
                    }
                    break;

                default:
                    // rectangle
                    for (int i = 0; i < n; i++)
                    {
                        window[i] = 1.0;
                    }
                    break;
            }

            return window;
        }

        /**
         * Performs DFT on a signal using multi-threading.
         * 
         * @Param s The input signal.
         * @Param n The length of the signal.
         * @Param threadNum The current thread number.
         * @Param maxThreads The maximum number of threads to use.
         * 
         * @Return DFT result
         */
        public Complex[] threadedDFTFunc(double[] s, int n, int threadNum)
        {
            Thread[] tArray = new Thread[threadNum];
            threadAmplitude = new double[n];
            Complex[] cmplxResult = new Complex[n];

            for (int i = 0; i < threadNum; i++)
            {
                int threadIndex = i;
                tArray[i] = new Thread(() =>
                {
                    Complex[] partialResult = dft(s, n, threadIndex, threadNum);

                    lock (lockObject)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            cmplxResult[j] += partialResult[j];
                        }
                    }
                });

                tArray[i].Start();
            }

            foreach (Thread th in tArray)
                th.Join();

            return cmplxResult;
        }

        /**
         * Creates a lowpass filter for DFT analysis.
         * 
         * @Param N The size of the filter.
         * 
         * @Return the filter in Complex
         */
        private Complex[] creationOfLowpassFilter(int N)
        {
            Complex[] outComplex = new Complex[N];
            double start = chart1.ChartAreas[0].CursorX.SelectionStart;
            for (int i = 0; i < (N / 2); i++)
            {
                if (N % 2 != 0)
                {
                    outComplex[N / 2] = new Complex(0, 0);
                }
                if (i < start)
                {
                    outComplex[i] = new Complex(1, 1);
                    outComplex[N - i - 1] = new Complex(1, 1);
                }
                else
                {
                    outComplex[i] = new Complex(0, 0);
                    outComplex[N - i - 1] = new Complex(0, 0);
                }
            }
            return outComplex;  
        }

        /**
         * Performs convolution on a signal with given weights and overwrites the original.
         * 
         * @Param convolutionData The weights obtained with inverseDFT(filter).
         * @Param audioData The original signal data.
         * 
         * @Return The convolved signal.
         */
        private double[] convolve(double[] convolutionData, double[] audioData)
        {
            int N = audioData.Length, WN = convolutionData.Length;
            double[] newSignal = new double[N];
            for (int n = 0; n < N; n++)
            {
                double temp = 0;
                for (int wn = 0; wn < WN; wn++)
                {
                    //Externals.convolveInASM(N, WN, convolutionData, audioData, newSignal);
                    if ((n + wn) < (N - 1))
                        temp += convolutionData[wn] * audioData[n + wn];
                    else
                        temp += 0;
                }
                newSignal[n] = temp;
            }
            this.main.setAudioData(newSignal);
            return newSignal;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        } 

        /** 
         * inverseDFT
         * Inverse DFT function that was demonstrated in class. Normalize each s(t) by N.
         * Sets an instance double[]'s values to the real portions
         * 
         * Params: 
         * Complex[] A is the filter
         * 
         * Returns:
         * double[] The real portions of the inverseDFT. These are weights we can use for convolution
         */
        public double[] inverseDFT(Complex[] A)
        {
            int n = A.Length;
            Complex[] s = new Complex[n];

            for (int t = 0; t < n; t++)
            {
                double re = 0;
                double im = 0;

                for (int f = 0; f < n; f++)
                {
                    double angle = 2 * Math.PI * t * f / n;
                    re += A[f].Real * Math.Cos(angle) + A[f].Imaginary * Math.Sin(angle);
                    im += A[f].Imaginary * Math.Cos(angle) - A[f].Real * Math.Sin(angle);
                }

                s[t] = new Complex(re, im) / n;
            }
            // save the real portion in an array
            double[] real = new double[n];
            for (int i =0; i < n; i ++)
            {
                real[i] = s[i].Real;
            }
            return real;
        }

        /** 
         * btnLowpass_Click is used by the lowpass button control. It follows these steps:
         * 1. Design a lowpass filter based on the user selected cutoff frequency
         * 2. Perform inverse DFT on the filter to obtain weights
         * 3. Get the original signal from WaveAnalyser
         * 4. Perform convolution on the signal with the filter
         * 5. Lines 269 through 275: Return the signal data to a byte array
         * 6. Update the pSaveBuffer which in turn filters the audio
         * 7. Display the changes
         * 
         * @Param sender The object that triggered the event.
         * @Param e The event arguments.
         */
        private void btnLowpass_Click(object sender, EventArgs e)
        {
            Complex[] filter = creationOfLowpassFilter(dftRes.Length);
            double[] filtered = inverseDFT(filter);
            double[] audioData = this.main.getAudioData();
            audioData = convolve(filtered, audioData);

            byte[] byteArray = audioData.Select(sample =>
            {
                // Scale the sample from [-1, 1] to [0, 255]
                double scaledValue = (sample + 1) * 127.5;

                // Ensure the value is within the valid byte range [0, 255]
                scaledValue = Math.Max(0, Math.Min(255, scaledValue));

                // Convert to byte
                byte byteValue = (byte)scaledValue;

                return byteValue;
            }).ToArray();

            IntPtr pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);
            Externals.UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);
            this.main.plotFreqWaveChart(audioData);
            Plot(audioData, 1, cmplx_dftRes.Length);
            WaveAnalyzerForm.EDITED = true;
        }


        /**
         * Creates a highpass filter for DFT analysis.
         * 
         * @Param N The size of the filter.
         * 
         * @Return the filter in Complex
         */
        private Complex[] creationOfHighpassFilter(int N)
        {
            Complex[] outComplex = new Complex[N];
            double start = chart1.ChartAreas[0].CursorX.SelectionStart;
            for (int i = 0; i < (N / 2); i++)
            {
                if (N % 2 != 0)
                {
                    outComplex[N / 2] = new Complex(0, 0);
                }
                if (i >= start)
                {
                    outComplex[i] = new Complex(1, 1);
                    outComplex[N - i - 1] = new Complex(1, 1);
                }
                else
                {
                    outComplex[i] = new Complex(0, 0);
                    outComplex[N - i - 1] = new Complex(0, 0);
                }
            }
            return outComplex;

        }

        /** 
         * btnHighpass_Click is used by the highpass button control. It follows these steps:
         * 1. Design a highpass filter based on the user selected cutoff frequency
         * 2. Perform inverse DFT on the filter to obtain weights
         * 3. Get the original signal from WaveAnalyser
         * 4. Perform convolution on the signal with the filter
         * 6. Update the pSaveBuffer which in turn filters the audio
         * 7. Plot the changes
         * 
         * @param sender The object that triggered the event.
         * @param e The event arguments.
        */
        private void btnHighpass_Click(object sender, EventArgs e)
        {
            Complex[] filter = creationOfHighpassFilter(dftRes.Length);
            double[] filtered = inverseDFT(filter);
            double[] audioData = this.main.getAudioData();
            audioData = convolve(filtered, audioData);

            byte[] byteArray = audioData.Select(sample =>
            {
                byte byteValue = (byte)(sample + 1);

                return byteValue;
            }).ToArray();

            IntPtr pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);
            Externals.UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);
            this.main.plotFreqWaveChart(audioData);
            Plot(cmplx_dftRes, 1, cmplx_dftRes.Length);
            WaveAnalyzerForm.EDITED = true;
        }

        /**
        * Event handler for the Load event of DFT. Triggered when the DFT form is loaded.
        * 
        * @Param sender The source of the event.
        * @Param e Contains the event data.
        */
        private void DFT_Load(object sender, EventArgs e)
        {

        }
    }
}
