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
    public partial class FrmListadoChequesaPagar : Form
    {
        public FrmListadoChequesaPagar()
        {
            InitializeComponent();
        }

        private void FrmListadoChequesaPagar_Load(object sender, EventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            txtFechaHasta.Text = fechahoy.ToShortDateString();
            fechahoy = fechahoy.AddMonths(-1);
            txtFechaDesde.Text = fechahoy.ToShortDateString();

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

            int Impago = 0;
            if (chkImpagos.Checked == true)
                Impago = 1;

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            int Impagos = 0;
            if (chkImpagos.Checked == true)
                Impagos = 1;
            Clases.cChequesaPagar cheque = new Clases.cChequesaPagar();
            DataTable trdo = cheque.GetChequesPagar(FechaDesde, FechaHasta, Impago,txtPatente.Text);
            txtTotal.Text = fun.TotalizarColumna(trdo, "Importe").ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            trdo = fun.TablaaMiles(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "Saldo");
            Grilla.DataSource = trdo;
            Grilla.Columns[1].HeaderText = "Nro de cheque";
            Grilla.Columns[6].HeaderText = "Fecha Pago";
            Grilla.Columns[9].HeaderText = "Fecha Vto";
            Grilla.Columns[5].Width = 130;
            Grilla.Columns[1].Width = 140;
            Grilla.Columns[2].Width = 150;
            Grilla.Columns[3].Width = 100;
            Grilla.Columns[6].Width = 110;
            Grilla.Columns[7].Width = 180;
            Grilla.Columns[7].HeaderText = "Descripción";
            Grilla.Columns[0].Visible = false;
        }

        private void btnCobroCheque_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }
            Int32 CodCheque = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);
            Principal.CodigoPrincipalAbm = CodCheque.ToString();
            FrmRegistrarPagoCheque frm = new FrmRegistrarPagoCheque();
            frm.ShowDialog();

        }
    }
}
