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
            Int32 CodPrestamo = 0;
            string Nombre = txtNombre.Text;
            string Telefono = txtTelefono.Text;
            string Dirección = txtDireccion.Text;
            DateTime Fecha = Convert.ToDateTime(dpFecha.Value);
            double Importe = fun.ToDouble(txtImporte.Text);
            double PorcentajeInteres = Convert.ToDouble(txtPorcentaje.Text);
            DateTime FechaVencimiento = Convert.ToDateTime(dpFechaVto.Value);
            double ImporteaPagar = fun.ToDouble(txtMontoApagar.Text);

            cPrestamoCobrar prestamo = new Clases.cPrestamoCobrar();

            CodPrestamo = prestamo.InsertarPrestamo(Nombre, Telefono, Dirección, Fecha, Importe, PorcentajeInteres, FechaVencimiento, ImporteaPagar);
           // Int32 CodPrestamo = prestamo.GetMaxPrestamo();
            string Descripcion = "INGRESO PRESTAMO " + txtNombre.Text.ToUpper();
            string DescripcionDetalle = "INGRESO PRESTAMO " + Importe.ToString().Replace(",", ".");
            cDetallePrestamoCobrar detalle = new Clases.cDetallePrestamoCobrar();
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

        private void txtPorcentaje_Leave(object sender, EventArgs e)
        {
            CalcularPorcentaje();
        }

        private void CalcularPorcentaje()
        {
            if (txtPorcentaje.Text != "0" && txtImporte.Text != "0")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                double Por = 0;
                double Monto = 0;
                double aPagar = 0;
                if (txtPorcentaje.Text != "")
                    Por = Convert.ToDouble(txtPorcentaje.Text);

                if (txtImporte.Text != "")
                    Monto = fun.ToDouble(txtImporte.Text);
                aPagar = (Monto * Por) / 100;
                txtMontoApagar.Text = aPagar.ToString();
                if (txtMontoApagar.Text != "")
                {
                    decimal m = Convert.ToDecimal(aPagar);
                    txtMontoApagar.Text = decimal.Round(m, 0).ToString();
                    txtMontoApagar.Text = fun.FormatoEnteroMiles(txtMontoApagar.Text);
                }
            }

        }
    }
}
