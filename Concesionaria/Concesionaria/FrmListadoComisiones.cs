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
    public partial class FrmListadoComisiones : Form
    {
        public FrmListadoComisiones()
        {
            InitializeComponent();
        }

        private void FrmListadoComisiones_Load(object sender, EventArgs e)
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

            Clases.cComisionVendedor comision = new Clases.cComisionVendedor();
            DataTable trdo = comision.GetComisionesxFecha(FechaDesde, FechaHasta, Impagos, txtApellido.Text, txtPatente.Text);
            trdo = fun.TablaaMiles(trdo, "Importe");
            txtTotal.Text = fun.TotalizarColumna(trdo, "Importe").ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[1].Width = 230;
            Grilla.Columns[2].Width = 240;
            Grilla.Columns[5].HeaderText = "Fecha Pago";
            Grilla.Columns[5].Width = 150;
            Grilla.Columns[6].Visible = false;
            Grilla.Columns[8].Width = 223;
            Grilla.Columns[8].HeaderText = "Descripción"; 
        }

        private void btnCobroCheque_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("De.cmebe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }

                string CodComision = Grilla.CurrentRow.Cells[0].Value.ToString();
                FrmPagoComision form = new FrmPagoComision();
                Principal.CodigoPrincipalAbm = CodComision;
                form.ShowDialog();
        }
    }
}

