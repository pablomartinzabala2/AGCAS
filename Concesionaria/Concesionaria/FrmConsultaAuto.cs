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
    public partial class FrmConsultaAuto : Form
    {
        public FrmConsultaAuto()
        {
            InitializeComponent();
            Buscar("");
        }

        private void Buscar(string Patente)
        {
            Clases.cAuto objAuto = new Clases.cAuto();
            DataTable trdo = objAuto.GetAutos(txtPatente.Text);
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 100;
            Grilla.Columns[2].Width = 440;
            Grilla.Columns[3].Width = 150;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar(txtPatente.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string CODIGO = Grilla.CurrentRow.Cells[0].Value.ToString();
            FrmAbmCliente frm = new FrmAbmCliente();
            Principal.CodigoPrincipalAbm = CODIGO;
            frm.Focus();
            this.Close();
        }

        private void FrmConsultaAuto_Load(object sender, EventArgs e)
        {

        }
    }
}
