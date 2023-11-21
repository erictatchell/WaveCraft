using System;
using System.Drawing;
using System.Numerics;
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

        public DFT(double[] s, double sampleRate, int threads, string windowType)
        {
            InitializeComponent();

            this.windowType = windowType;
            this.Text = "DFT - " + windowType + " Windowing";

            int N = s.Length;
            Complex[] dftResult = threadedDFTFunc(s, N, threads);

            double[] frequencies = new double[N];
            for (int i = 0; i < N; i++)
            {
                frequencies[i] = i * sampleRate / N;
            }

            for (int i = 0; i < N; i++)
            {
                chart1.Series[0].Points.AddXY(frequencies[i], dftResult[i].Magnitude);
            }

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = sampleRate;

            chart1.ChartAreas[0].AxisY.Title = "Amplitude";
            chart1.ChartAreas[0].AxisX.Title = "Frequency (Hz)";
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
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

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
