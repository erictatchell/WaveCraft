using System;
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
    }
}
