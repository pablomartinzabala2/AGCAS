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
    public partial class FrmBorrarTablas : Form
    {
        public FrmBorrarTablas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToUpper() != "PABLO")
            {
                MessageBox.Show("Ingresar clave");
                return;
            }
            Clases.cConfiguracion.BorrarTablas();
            MessageBox.Show("datos borrados", Clases.cMensaje.Mensaje()); 
        }
    }
}
