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

        public Complex[] dft(int N, double[] s)
        {
            Complex[] A = new Complex[N];

            for (int f = 0; f < N; f++)
            {
                A[f] = new Complex(0, 0);

                for (int t = 0; t < N; t++)
                {
                    double angle = 2 * Math.PI * t * f / N;

                    A[f] = Complex.Add(A[f], new Complex(s[t] * Math.Cos(angle), -s[t] * Math.Sin(angle)));
                }
            }
            return A;
        }




        public DFT(double[] s, double sampleRate)
        {
            InitializeComponent();

            int N = s.Length;
            Complex[] dftResult = dft(N, s);
            double[] frequencies = new double[N];
            for (int i = 0; i < N; i++)
            {
                frequencies[i] = i * sampleRate / N;
            }

            for (int i = 0; i < N; i++)
            {
                chart1.Series[0].Points.AddXY((int)frequencies[i], dftResult[i].Magnitude);
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

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
