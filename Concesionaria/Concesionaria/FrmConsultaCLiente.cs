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
    public partial class FrmConsultaCLiente : Form
    {
        public FrmConsultaCLiente()
        {
            InitializeComponent();
            BuscarCliente("", "", "");
        }

        private void BuscarCliente(string NroDocumento, string Ape, string Nom)
        {
            Clases.cCliente clie = new Clases.cCliente();
            DataTable trdo = clie.BuscarCliente(NroDocumento, Ape, Nom);
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].Width = 240;
            Grilla.Columns[3].Width = 240;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {   
             string CODIGO = Grilla.CurrentRow.Cells[0].Value.ToString();
            FrmAbmCliente frm = new FrmAbmCliente();
            Principal.CodigoPrincipalAbm = CODIGO ; 
            frm.Focus();
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCliente(txtnroDocumento.Text, txtApe.Text, txtNom.Text);
        }
    }
}
