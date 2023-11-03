using NAudio.Dsp;
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

        double[] dft(int N, double[] s)
        {
            double[] A = new double[N];
            for (int f = 0; f < N; f++)
            {
                for (int t = 0; t < N; t++)
                {
                    double re = Math.Cos(2 * Math.PI * t * f / N);
                    double im = Math.Sin(2 * Math.PI * t * f / N);
                    Complex reMinusim = new Complex(re, im);
                    A[f] += s[t] * (re - im);
                }
                A[f] /= N; // Normalize by dividing by N
            }
            return A;
        }

        public DFT(double[] s)
        {
            InitializeComponent();

            int N = s.Length / 2;
            double[] dftResult = dft(N, s);

            // Add data points to the chart
            for (int i = 0; i < N; i++)
            {
                chart1.Series[0].Points.AddXY(i, dftResult[i]);
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
