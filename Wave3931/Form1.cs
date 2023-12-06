/**
    Author: Eric Tatchell & Brendan Doyle
    Date: October & November 2023
 */
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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/***********************************************************************************************************************************************************
 *
 * File: Forms1.cs
 *
 * Purpose: This file defines Form1, the main entry form for the Wave3931 application, facilitating the creation of new wave analyzer 
 * forms and the opening of existing wave files through a user interface.
 *
 ***********************************************************************************************************************************************************/

namespace Wave3931
{
    /* 
     * Class Form1 is responsible for the starting screen of the application.
     */
    public partial class Form1 : Form
    {
        /* 
        * Constructor for Form1. Calls the InitializeComponent method to set up the form components.
        */
        public Form1()
        {
            InitializeComponent();
        }

        /* 
        * Event handler for the New File button click event.
        * Creates and displays a new instance of WaveAnalyzerForm.
        * 
        * @param sender The source of the event.
        * @param e Contains the event data.
        */
        private void btnNewFile_Click(object sender, EventArgs e)
        {
            WaveCraft waveAnalyzerForm = new WaveCraft();
            waveAnalyzerForm.Show();
        }

        /* 
        * Event handler for the Load event of Form1. 
        * This method is called when Form1 is loaded.
        * 
        * @param sender The source of the event.
        * @param e Contains the event data.
        */
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /* 
        * Event handler for the Open File button click event. Opens a dialog to select and open a wave file.
        * 
        * @param sender The source of the event.
        * @param e Contains the event data.
        */
        private void btnOpenFile_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    toolStripStatusLabel1.Text = "Selected File: " + System.IO.Path.GetFileName(selectedFilePath);
                    // Create and show a new instance of WaveAnalyzerForm with the selected file
                    WaveCraft waveAnalyzerForm = new WaveCraft(selectedFilePath, Path.GetFileName(selectedFilePath));
                    waveAnalyzerForm.Show();
                }
            }
        }
    }
}