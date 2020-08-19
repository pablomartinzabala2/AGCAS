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
    public partial class FrmListadoGastos : Form
    {
        public FrmListadoGastos()
        {
            InitializeComponent();
        }

        private void FrmListadoGastos_Load(object sender, EventArgs e)
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

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            int Impagos = 0;
            if (chkImpagos.Checked == true)
                Impagos = 1;
            //Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cGastosPagar gasto = new Clases.cGastosPagar();
            DataTable trdo = gasto.GetGastosPagarxFecha(FechaDesde, FechaHasta, txtPatente.Text, Impagos);
            trdo = fun.TablaaMiles(trdo, "Importe");
            txtTotal.Text = fun.TotalizarColumna(trdo, "Importe").ToString();
            Grilla.DataSource = trdo;
            Grilla.Columns[5].HeaderText = "Fecha Pago";
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 160;
            Grilla.Columns[2].Width = 355;
            Grilla.Columns[4].Width = 120;
            Grilla.Columns[5].Width = 110;
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
        }

        private void btnCobroCheque_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje()); 
                return;
            }

            Int32 CodGasto = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString ());
            Principal.CodigoPrincipalAbm = CodGasto.ToString();
            FrmRegistrarPagoGastos frm = new FrmRegistrarPagoGastos();
            frm.ShowDialog();
        }
    }
}
