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
    public partial class FrmListadoEfectivosaPagar : Form
    {
        public FrmListadoEfectivosaPagar()
        {
            InitializeComponent();
        }

        private void FrmListadoEfectivosaPagar_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
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

            int Impagos = 0;
            if (chkImpagos.Checked == true)
                Impagos = 1;

            Clases.cPrenda prenda = new Clases.cPrenda();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            Clases.cEfectivoaPagar obj = new Clases.cEfectivoaPagar();
            DataTable trdo = obj.GetEfectivosaPagarxFecha(FechaDesde, FechaHasta, txtPatente.Text.Trim(),Impagos);
            trdo = fun.TablaaMiles(trdo, "Saldo");
            trdo = fun.TablaaMiles(trdo, "Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = true; 
            Grilla.Columns[5].Width = 150;
            Grilla.Columns[6].Width = 150;
            Grilla.Columns[7].Width = 240;
            txtTotal.Text = fun.TotalizarColumna(trdo, "Saldo").ToString();
            if (txtTotal.Text != "")
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);

        }

        private void btnCobroPrenda_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }
            string CodRegistro = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoPrincipalAbm = CodRegistro;
            FrmRegistarEfectivosaPagar form = new FrmRegistarEfectivosaPagar();
            form.ShowDialog();
        }
    }
}
