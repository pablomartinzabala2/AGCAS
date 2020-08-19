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
    public partial class FrmRegistrarCobranzaGeneral : Form
    {
        public FrmRegistrarCobranzaGeneral()
        {
            InitializeComponent();
        }

        private void FrmRegistrarCobranzaGeneral_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (txtEfectivo.Text == "")
            {
                Mensaje("Debe ingresar un monto");
                return;
            }

            if (txtDescripcion.Text == "")
            {
                Mensaje("Debe ingresar una Descripción");
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }

            DateTime Fecha = Convert.ToDateTime (txtFecha.Text);
            string Descripcion = txtDescripcion.Text ;
            double Importe = fun.ToDouble(txtEfectivo.Text);;
            string Nombre = txtNombre.Text;
            string Telefono = txtTelefono.Text;
            string Patente = txtPatente.Text;
            string Direccion = txtDireccion.Text;
            cCobranzaGeneral cob = new cCobranzaGeneral();
            cob.InsertarCobranza(Fecha, Descripcion, Importe, Nombre, Telefono, Direccion, Patente);
            Mensaje("Datos grabados correctamente");
            txtDescripcion.Text = "";
            txtEfectivo.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";

        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (txtEfectivo.Text != "")
                txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
        }
    }
}
