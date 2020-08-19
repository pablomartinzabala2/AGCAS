using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Concesionaria.Clases;
using Concesionaria.Clases;
namespace Concesionaria
{
    public partial class FrmIngresoCheque : Form
    {
        cFunciones fun;
        public FrmIngresoCheque()
        {
            InitializeComponent();
        }

        private void FrmIngresoCheque_Load(object sender, EventArgs e)
        {
            fun = new cFunciones();
            fun.LlenarCombo(cmbBanco, "Banco", "Nombre", "CodBanco");
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            fun.SoloEnteroConPunto(sender, e);
        }

        public void Mensaje(string msj)
        {
            MessageBox.Show(msj, "Sistema");
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text == "")
            {
                Mensaje("Ingresar fecha");
                return;
            }

            if (txtVencimiento.Text == "")
            {
                Mensaje("Ingresar fecha de vencimiento");
                return;
            }

            if (txtImporte.Text == "")
            {
                Mensaje("Debe ingresar un importe");
                return;
            }

            if (txtNumeroCheque.Text == "")
            {
                Mensaje("Debe ingresar un número de cheque");
                return;
            }

            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            DateTime Vencimiento = Convert.ToDateTime(txtVencimiento.Text);
            Double Importe = Convert.ToDouble(txtImporte.Text);
            string NumeroCheque = txtNumeroCheque.Text;
            string Apellido = txtApellido.Text;
            string Nombre = txtNombre.Text;
            string Patente = txtPatente.Text; 
            Int32? CodBanco = null;
            if (cmbBanco.SelectedIndex >0)
                CodBanco  = Convert.ToInt32 (cmbBanco.SelectedValue);
            string Telefono = txtTelefono.Text;
            string Descripcion = "INGRESO DE CHEQUE " + NumeroCheque;
            Descripcion = Descripcion + ", PATENTE: " + txtPatente.Text.ToUpper();
            Descripcion = Descripcion + ". CLIENTE: " + txtApellido.Text.ToUpper();
            cChequeCobrar cheque = new cChequeCobrar();
            cheque.Insertar(Fecha, Vencimiento, Importe,
                CodBanco, Apellido , Nombre, Patente, Telefono,NumeroCheque);
            Mensaje("Datos grabados correctamente");
            cMovimiento mov = new cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado,
                -1 * Importe, 0, 0, 0, 0, Fecha, Descripcion);
            txtNumeroCheque.Text = "";
            txtImporte.Text = "";
            txtVencimiento.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtPatente.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmListadoChequeCobrar frm = new FrmListadoChequeCobrar();
            frm.Show();
        }
    }
}
