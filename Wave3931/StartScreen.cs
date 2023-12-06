using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/***********************************************************************************************************************************************************
 *
 * File: StartScreen.cs
 *
 * Purpose: Starting screen of the application. This is the first screen that the user sees when the application is launched.
 *          It contains both the "Open File" and "Create New File" options.
 *
 ***********************************************************************************************************************************************************/


namespace Wave3931
{
    /* 
     * Class StartScreen, inheriting from Form. This class represents the starting screen of the application.
     */
    public partial class StartScreen : Form
    {
        /* 
        * Constructor for StartScreen. Calls the InitializeComponent method to set up the form components.
        */
        public StartScreen()
        {
            InitializeComponent();
        }

        /* 
        * Event handler for the comboBox1 selected index change event. Triggered when the selected index of comboBox1 is changed.
        * 
        * @param sender: The object that triggered the event.
        * @param e: The event arguments.
        */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /* 
        * Event handler for the Load event of StartScreen. Triggered when the StartScreen form is loaded.
        * 
        * @param sender: The object that triggered the event.
        * @param e: The event arguments.
        */
        private void StartScreen_Load(object sender, EventArgs e)
        {

        }

        /* 
        * Event handler for button1 click event. Triggered when button1 is clicked.
        */
        private void button1_Click(object sender, EventArgs e)
        {
            // using statement to ensure that the OpenFileDialog is disposed of properly
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    //toolStripStatusLabel1.Text = "Selected File: " + System.IO.Path.GetFileName(selectedFilePath);
                }
            }
        }
    }
}
