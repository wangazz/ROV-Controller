using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROV_Controller_v0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Going Forward";
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) textBox1.Text = "Going Forwards";
            if (e.KeyCode == Keys.A) textBox1.Text = "Going Left";
            if (e.KeyCode == Keys.S) textBox1.Text = "Going Backwards";
            if (e.KeyCode == Keys.D) textBox1.Text = "Going Right";
            if (e.KeyCode == Keys.Z) textBox1.Text = "Swivel Left";
            if (e.KeyCode == Keys.X) textBox1.Text = "Swivel Right";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            textBox1.Text = "";
        }
    }
}
