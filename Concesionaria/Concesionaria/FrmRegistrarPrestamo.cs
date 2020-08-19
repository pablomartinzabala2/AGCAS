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
    public partial class FrmRegistrarPrestamo : Form
    {
        public FrmRegistrarPrestamo()
        {
            InitializeComponent();
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

            if (fun.ValidarFecha (txtFecha.Text)==false )
            {
                MessageBox.Show ("Debe ingresar una fecha válida para continuar", Clases.cMensaje.Mensaje ());
                return ;
            }
             
            if (fun.ValidarFecha (txtFechaVencimiento.Text)==false )
            {
                MessageBox.Show ("Debe ingresar una fecha de vencimiento válidad para continuar", Clases.cMensaje.Mensaje ());
                return ;
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

            Clases.cPrestamo prestamo = new Clases.cPrestamo();
            prestamo.InsertarPrestamo(Nombre, Telefono, Dirección, Fecha, Importe, PorcentajeInteres, FechaVencimiento, ImporteaPagar);
            Int32 CodPrestamo = prestamo.GetMaxPrestamo();
            string Descripcion = "INGRESO PRESTAMO " + txtNombre.Text.ToUpper();
            string DescripcionDetalle = "INGRESO PRESTAMO " + Importe.ToString().Replace(",", ".");
            Clases.cDetallePrestamo detalle = new Clases.cDetallePrestamo();
            detalle.InsertarDetallePrestamo(CodPrestamo, Importe, DescripcionDetalle, Fecha);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtMontoApagar.Text = "";
            txtImporte.Text = "";
            txtFecha.Text = "";
            txtFechaVencimiento.Text = "";
            txtPorcentaje.Text = ""; 
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ",")
                e.Handled = true;
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            if (txtImporte.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                CalcularPorcentaje();
            }
        }

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox)
                            ((TextBox)g).CharacterCasing = CharacterCasing.Upper;
                    }
                    //Empleamos un casteo

                }
            }
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox)
                            ((TextBox)g).CharacterCasing = CharacterCasing.Upper;
                    }
                    //Empleamos un casteo

                }
            }
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox)
                            ((TextBox)g).CharacterCasing = CharacterCasing.Upper;
                    }
                    //Empleamos un casteo

                }
            }
        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
