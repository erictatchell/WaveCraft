using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Wave3931
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private DFT DFT;
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DFT = new DFT(); // Create a new instance of the DFT form
            DFT.Owner = this; // Set the owner if needed
            DFT.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Txt files for testing hahaha(*.txt)|*.txt|MS-WAVE Files (*.wav)|*.wav|All Files (*.*)|*.*"; // Filter for text files, but you can change it to match your file type

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // User selected a file
                    string selectedFilePath = openFileDialog.FileName;
                    toolStripStatusLabel1.Text = "Selected File: " + System.IO.Path.GetFileName(selectedFilePath);

                    // Now you can open and read the file as needed
                    // For example, you can display the content in a TextBox:
                    //string fileContent = System.IO.File.ReadAllText(selectedFilePath);
                    //textBox1.Text = fileContent; // Assuming you have a TextBox named textBox1
                }
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            // Get the name of the selected file from toolStripStatusLabel1
            string selectedFileName = toolStripStatusLabel1.Text.Replace("Selected File: ", "");

            // Use the selectedFileName as needed
            MessageBox.Show("Selected File Name: " + selectedFileName);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
