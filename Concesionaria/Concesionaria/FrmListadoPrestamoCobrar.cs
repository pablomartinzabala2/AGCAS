using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Concesionaria.Clases;
namespace Concesionaria
{
    public partial class FrmListadoPrestamoCobrar : Form
    {
        public FrmListadoPrestamoCobrar()
        {
            InitializeComponent();
        }

        private void FrmListadoPrestamoCobrar_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
            CargarGrilla();
        }

        private void CargarGrilla()
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

            int soloImpago = 0;
            if (chkImpagos.Checked)
                soloImpago = 1;
            string Nombre = txtNombre.Text;
            Clases.cPrestamoCobrar prestamo = new Clases.cPrestamoCobrar();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable tb = prestamo.GetPrestamosxFecha(FechaDesde, FechaHasta, Nombre, soloImpago);
            tb = fun.TablaaMiles(tb, "Importe");
            tb = fun.TablaaMiles(tb, "ImporteaPagar");
            txtTotal.Text = fun.TotalizarColumna(tb, "Importe").ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            Grilla.DataSource = tb;
            Grilla.Columns[1].Width = 290;
            Grilla.Columns[2].Width = 170;
            Grilla.Columns[5].HeaderText = "Fecha";
            Grilla.Columns[5].Width = 100;
            Grilla.Columns[7].HeaderText = "Importe a pagar";
            Grilla.Columns[7].Width = 140;
            Grilla.Columns[8].HeaderText = "Fecha Pago";
            Grilla.Columns[8].Width = 130;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[4].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}
