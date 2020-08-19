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
    public partial class FrmListadoCheques : Form
    {
        public FrmListadoCheques()
        {
            InitializeComponent();
        }

        private void FrmListadoCheques_Load(object sender, EventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            txtFechaHasta.Text = fechahoy.ToShortDateString();
            fechahoy = fechahoy.AddMonths(-1);
            txtFechaDesde.Text = fechahoy.ToShortDateString();
            CargarBancos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                MessageBox.Show("Fecha desde incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                MessageBox.Show("Fecha hasta incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (Convert.ToDateTime(txtFechaDesde.Text) > Convert.ToDateTime(txtFechaHasta.Text))
            {
                MessageBox.Show("La fecha desde debe ser inferior a la fecha hasta", Clases.cMensaje.Mensaje());
                return;
            }

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            int Impagos = 0;
            Int32? CodBanco = null;
            string NroCheque = "";
            if (cmbBanco.SelectedIndex > 0)
                CodBanco = Convert.ToInt32(cmbBanco.SelectedValue);
            NroCheque = txtNumeroCheque.Text;
            if (chkImpagos.Checked == true)
                Impagos = 1;
            Clases.cCheque cheque = new Clases.cCheque();
            DataTable trdo = cheque.GetChequesxFecha(FechaDesde, FechaHasta,Impagos,CodBanco ,NroCheque );
            trdo = fun.TablaaMiles(trdo, "Importe");
            txtTotal.Text = fun.TotalizarColumna(trdo, "Importe").ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            Grilla.DataSource = trdo;
            Grilla.Columns[1].Width = 100;
            Grilla.Columns[1].HeaderText = "Fec. Cobro";
            Grilla.Columns[2].Width = 100;
            Grilla.Columns[2].HeaderText = "Fecha Vto.";
            Grilla.Columns[3].Width = 140;
            Grilla.Columns[3].HeaderText = "Nro. Cheque";
            Grilla.Columns[5].Width = 220;
            Grilla.Columns[6].Visible = false; 
             
 
        }

        private void btnCobroCheque_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje()); 
                return;
            }
            string CodAuto = Grilla.CurrentRow.Cells[6].Value.ToString();
            Principal.CodigoPrincipalAbm = CodAuto;
            FrmCobroCheque frm = new FrmCobroCheque();
            frm.ShowDialog();
            
        }

        private void CargarBancos()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmbBanco, "Banco", "Nombre", "CodBanco");
        }
    }
}
