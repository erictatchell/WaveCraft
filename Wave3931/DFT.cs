using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Wave3931
{
    public partial class DFT : Form
    {
        double[] generic_fsg(int N, double[] f)
        {
            double[] s = new double[N];

            for (int t = 0; t < N; t++)
            {
                s[t] = (Math.Cos((2 * Math.PI) * ((double)t / N) * f[t]));
            }

            return s;
        }

        public class ComplexNumber
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }

            public ComplexNumber(double real, double imaginary)
            {
                Real = real;
                Imaginary = imaginary;
            }
        }

        public ComplexNumber[] dft(int N, double[] s)
        {
            ComplexNumber[] A = new ComplexNumber[N];
            for (int f = 0; f < N; f++)
            {
                A[f] = new ComplexNumber(0, 0); // Initialize to (0, 0)
                for (int t = 0; t < N; t++)
                {
                    double re = Math.Cos(2 * Math.PI * t * f / N);
                    double im = Math.Sin(2 * Math.PI * t * f / N);

                    A[f].Real += s[t] * (re - im);
                    A[f].Imaginary += s[t] * (re + im);
                }
                A[f].Real /= N; // Normalize by dividing by N
                A[f].Imaginary /= N; // Normalize by dividing by N
            }
            return A;
        }


        public DFT(double[] s, double sampleRate)
        {
            InitializeComponent();

            int N = s.Length;
            ComplexNumber[] dftResult = dft(N, s);

            // Calculate the frequencies for each bin
            double[] frequencies = new double[N];
            for (int i = 0; i < N; i++)
            {
                frequencies[i] = i * sampleRate / N;
            }

            // Add data points to the chart with frequencies on the y-axis and amplitudes on the x-axis
            for (int i = 0; i < N; i++) // N / 2 to consider only the positive frequencies
            {
                double amplitude = Math.Sqrt(dftResult[i].Real * dftResult[i].Real + dftResult[i].Imaginary * dftResult[i].Imaginary);
                chart1.Series[0].Points.AddXY((int)frequencies[i], amplitude);
            }
            chart1.ChartAreas[0].AxisY.Title = "Amplitude";
            chart1.ChartAreas[0].AxisX.Title = "Frequency (Hz)";
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White; 
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
        }




        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
