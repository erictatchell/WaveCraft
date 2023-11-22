using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Wave3931
{
    public partial class DFT : Form
    {
        private readonly object lockObject = new object();
        private double[] threadAmplitude;
        private string windowType;
        private double[] dftRes;
        private Complex[] cmplx_dftRes;
        private WaveAnalyzerForm main = null;

        public DFT(double[] s, double sampleRate, int threads, string windowType, Form calling)
        {
            InitializeComponent();

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

        private double[] GenerateWindow(int n)
        {
            double[] window = new double[n];

            switch (windowType)
            {
                case "Rectangle":
                    // Rectangle window (no windowing)
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

                // Add more cases for other window types if needed
                case "Hamming":
                    for (int i = 0; i < n; i++)
                    {
                        // Hamming window
                        window[i] = (0.54 - 0.46 * Math.Cos(2 * Math.PI * i / (n - 1)));
                    }
                    break;

                default:
                    // Default to Rectangle window
                    for (int i = 0; i < n; i++)
                    {
                        window[i] = 1.0;
                    }
                    break;
            }

            return window;
        }


        public Complex[] threadedDFTFunc(double[] s, int n, int threadNum)
        {
            Thread[] tArray = new Thread[threadNum];
            threadAmplitude = new double[n];
            Complex[] cmplxResult = new Complex[n];

            for (int i = 0; i < threadNum; i++)
            {
                int threadIndex = i; // Capture the variable to avoid closure issues
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

        private Complex[] creationOfLowpassFilter(int N)
        {
            Complex[] outComplex = new Complex[N];
            double start = chart1.ChartAreas[0].CursorX.SelectionStart;

            // create a complex numbers for the selected size, otherwise it is complex zero
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

        private void convolve(double[] convolutionData, double[] orgSignal)
        {
            int N = orgSignal.Length, WN = convolutionData.Length;
            double[] newSignal = new double[N];
            for (int n = 0; n < N; n++)
            {
                double temp = 0;
                for (int wn = 0; wn < WN; wn++)
                {
                    if ((n + wn) < (N - 1)) // if we are less than the frequency data size
                        temp += convolutionData[wn] * orgSignal[n + wn];
                    else
                        temp += 0;
                }
                newSignal[n] = temp;
            }
            this.main.setAudioData(newSignal);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
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
            double[] real = new double[n];
            for (int i =0; i < n; i ++)
            {
                real[i] = s[i].Real;
            }
            return real;
        }

        private void btnLowpass_Click(object sender, EventArgs e)
        {
            Complex[] filter = creationOfLowpassFilter(dftRes.Length);
            double[] filtered = inverseDFT(filter);
            double[] audioData = this.main.getAudioData();

            // Convolve the filtered signal with the audio data
            convolve(filtered, audioData);

            // Adjust the scale and offset to fit the values within 0 to 255 for 8-bit PCM
            byte[] byteArray = audioData.Select(sample =>
            {
                // Scale the value to the range [0, 1]
                double scaledValue = (sample + 1) / 2.0;

                // Convert the scaled value to an 8-bit representation
                byte byteValue = (byte)(scaledValue * 255);

                return byteValue;
            }).ToArray();

            IntPtr pSaveBuffer;
            pSaveBuffer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, pSaveBuffer, byteArray.Length);
            WavePlayer.UpdatePSaveBuffer(pSaveBuffer, byteArray.Length);
            // Marshal.FreeHGlobal(pSaveBuffer); // Don't free the buffer if it's being used externally
            this.main.plotFreqWaveChart(audioData);
        }




    }
}
