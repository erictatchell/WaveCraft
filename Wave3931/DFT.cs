﻿using System;
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
        double[] generic_fsg(int N, double f)
        {
            double[] s = new double[N];

            for (int t = 0; t < N; t++)
            {
                s[t] = (Math.Cos((2 * Math.PI) * ((double)t / N) * f));
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
                    A[f] += s[t] * Math.Cos(2 * Math.PI * t * f / N);
                }
            }
            return A;
        }
        public DFT()
        {
            InitializeComponent();

            int N = 100;
            double[] samples = generic_fsg(N, 5);
            double[] freqs = dft(N, samples);

            for (int i = 0; i < N; i++)
            {
                chart1.Series["1"].Points.AddXY(i, freqs[i]);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}