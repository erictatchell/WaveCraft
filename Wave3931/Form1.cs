using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using NAudio.Wave;
using NAudio.Dsp;

namespace Wave3931
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }   

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            WaveAnalyzerForm waveAnalyzerForm = new WaveAnalyzerForm(); 
            waveAnalyzerForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Optional: Set some properties for the OpenFileDialog
            openFileDialog.Title = "Select a file";
            openFileDialog.Filter = "All Files (*.*)|*.*"; // Filter to show all files by default

            // Display the file dialog and check if the user pressed OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                // Do something with the selected file path, for example:
                MessageBox.Show("Selected file: " + selectedFilePath);
            }
        }
    }
}
