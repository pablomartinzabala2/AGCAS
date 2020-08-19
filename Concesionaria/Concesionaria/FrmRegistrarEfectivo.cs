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
    public partial class FrmRegistrarEfectivo : Form
    {
        public FrmRegistrarEfectivo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtEfectivo.Text == "")
            {
                MessageBox.Show("Ingresar efectivo para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtFecha.Text =="" )
            {
                MessageBox.Show("Ingresar una fecha para  continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            string Descripcion = txtDescripcion.Text;
            Double Importe = fun.ToDouble(txtEfectivo.Text);
           // Clases.cFunciones fun = new Clases.cFunciones();
            if (cmbTipo.SelectedIndex == 1)
                Importe = (-1) * Importe; 
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion (0, Principal.CodUsuarioLogueado, Importe  , 0, 0, 0, 0,Fecha ,Descripcion );
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            txtEfectivo.Text = "";
            txtDescripcion.Text = "";
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            if (txtEfectivo.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
            }
            
        }

        private void FrmRegistrarEfectivo_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            cmbTipo.Items.Add("Ingreso");
            cmbTipo.Items.Add("Egreso");
            
            cmbTipo.SelectedIndex = 0;
        }
    }
}
