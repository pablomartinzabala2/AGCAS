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
    public partial class FrmPrestamoCobrar : Form
    {
        public FrmPrestamoCobrar()
        {
            InitializeComponent();
        }

        private void FrmPrestamoCobrar_Load(object sender, EventArgs e)
        {
            DateTime Fecha = DateTime.Now;
            dpFechaVto.Value = Fecha;
            Fecha = Fecha.AddMonths(1);
            dpFechaVto.Value = Fecha;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtTelefono.Text == "")
            {
                MessageBox.Show("Debe ingresar un teléfono para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtImporte.Text == "")
            {
                MessageBox.Show("Debe ingresar un Importe para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(dpFecha.Value.ToShortDateString()) == false)
            {
                MessageBox.Show("Debe ingresar una fecha válida para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            dpFechaVto
            if (fun.ValidarFecha(dpFechaVto.Value.ToShortDateString()) == false)
            {
                MessageBox.Show("Debe ingresar una fecha de vencimiento válidad para continuar", Clases.cMensaje.Mensaje());
                return;
            }


            if (txtPorcentaje.Text == "")
            {
                MessageBox.Show("Debe ingresar un Porcentaje para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            // Clases.cFunciones fun = new Clases.cFunciones();
            string Nombre = txtNombre.Text;
            string Telefono = txtTelefono.Text;
            string Dirección = txtDireccion.Text;
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            double Importe = fun.ToDouble(txtImporte.Text);
            double PorcentajeInteres = Convert.ToDouble(txtPorcentaje.Text);
            DateTime FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text);
            double ImporteaPagar = fun.ToDouble(txtMontoApagar.Text);

            cPrestamoCobrar prestamo = new Clases.cPrestamoCobrar();

            prestamo.InsertarPrestamo(Nombre, Telefono, Dirección, Fecha, Importe, PorcentajeInteres, FechaVencimiento, ImporteaPagar);
            Int32 CodPrestamo = prestamo.GetMaxPrestamo();
            string Descripcion = "INGRESO PRESTAMO " + txtNombre.Text.ToUpper();
            string DescripcionDetalle = "INGRESO PRESTAMO " + Importe.ToString().Replace(",", ".");
            Clases.cDetallePrestamo detalle = new Clases.cDetallePrestamo();
            detalle.InsertarDetallePrestamo(CodPrestamo, Importe, DescripcionDetalle, Fecha);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, (-1)*Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtMontoApagar.Text = "";
            txtImporte.Text = "";
            txtPorcentaje.Text = "";
        }
    }
}
