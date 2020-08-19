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
    public partial class FrmListadoPrestamo : Form
    {
        public FrmListadoPrestamo()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
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
            Clases.cPrestamo prestamo = new Clases.cPrestamo();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable tb = prestamo.GetPrestamosxFecha(FechaDesde, FechaHasta, Nombre,soloImpago);
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

        private void FrmListadoPrestamo_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
        }

        private void btnCobroPrenda_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Int32 CodPrestamo = Convert.ToInt32 (Grilla.CurrentRow.Cells[0].Value);
            Principal.CodigoPrincipalAbm = CodPrestamo.ToString();
            FrmPagoInteres form = new FrmPagoInteres ();
            form.ShowDialog ();
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFechaDevolucion.Text) == false)
            {
                MessageBox.Show("La fecha de devolución es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (Grilla.CurrentRow.Cells[8].Value.ToString() != "")
            {
                MessageBox.Show("Ya se ha registrado el pago del préstamo", Clases.cMensaje.Mensaje());
                return;
            }


            Int32 CodPrestamo = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);  
            DateTime FechaPago = Convert.ToDateTime(txtFechaDevolucion.Text);
            string Nombre = Grilla.CurrentRow.Cells[1].Value.ToString();
            string Descripcion = "PAGO PRESTAMO " + Nombre.ToString();
            double Importe = fun.ToDouble (Grilla.CurrentRow.Cells[6].Value.ToString());
            Clases.cPrestamo prestamo = new Clases.cPrestamo();
            prestamo.RegistrarDevolucion(CodPrestamo, FechaPago);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, 0, 0, FechaPago, Descripcion);
            MessageBox.Show("Datos registrados correctametne", Clases.cMensaje.Mensaje());
            CargarGrilla();
        }
    }
}
