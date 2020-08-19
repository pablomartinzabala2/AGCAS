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
    public partial class FrmAlertas : Form
    {
        public FrmAlertas()
        {
            InitializeComponent();
        }

        private void FrmAlertas_Load(object sender, EventArgs e)
        {
            Clases.cAlarma alarma = new Clases.cAlarma();
            DataTable trdo = alarma.GetAlarmasxFecha(DateTime.Now);
            Grilla.DataSource = trdo;
            Grilla.Columns[1].Width = 200;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].Width = 100;
            Grilla.Columns[3].Width = 250;
            Grilla.Columns[4].Width = 90; 
        }
    }
}
