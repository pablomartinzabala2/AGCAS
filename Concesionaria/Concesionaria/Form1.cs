using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Concesionaria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            txt.Text = txt.Text.Replace(".", "");
            Int32 x = Convert.ToInt32(txt.Text);
            txt.Text = x.ToString("N0");
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Int32 x = Convert.ToInt32(txt.Text);
            txt.Text = x.ToString("N0");
        }
    }
}
