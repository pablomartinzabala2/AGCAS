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
    public partial class FrmConsultaMarca : Form
    {
        public FrmConsultaMarca()
        {
            InitializeComponent();
            Clases.cMarca marca = new Clases.cMarca();
            DataTable trdo = marca.GetMarcas();
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 350;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string CODIGO = Grilla.CurrentRow.Cells[0].Value.ToString();
            FrmAbmCliente frm = new FrmAbmCliente();
            Principal.CodigoPrincipalAbm = CODIGO;
            frm.Focus();
            this.Close();
        }
    }
}
