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
    public partial class FrmRegistrarAnotacion : Form
    {
        Clases.cFunciones fun;
        public FrmRegistrarAnotacion()
        {
            InitializeComponent();
            fun = new Clases.cFunciones();
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmRegistrarAnotacion_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            cmbTipo.Items.Add("Ingreso");
            cmbTipo.Items.Add("Egreso");
            cmbTipo.SelectedIndex = 0;
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, Clases.cMensaje.Mensaje());
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }
            if (txtEfectivo.Text == "")
            {
                Mensaje("Debe ingresar un importe");
                return;
            }

            if (txtDescripcion.Text == "")
            {
                Mensaje("Debe ingresar una descripción");
                return;
            }

            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            double Importe = fun.ToDouble(txtEfectivo.Text);
            string Descripcion = txtDescripcion.Text;
            double? ImporteIngreso = null;
            double? ImporteEgreso = null;
            if (cmbTipo.SelectedIndex == 0)
                ImporteIngreso = Importe;
            else
                ImporteEgreso = Importe;

            Clases.cAnotacion anotacion = new Clases.cAnotacion();
            anotacion.Insertar(Fecha, Descripcion, ImporteIngreso, ImporteEgreso);
            MessageBox.Show("Datos grabados correctamente");
            txtDescripcion.Text = "";
            txtEfectivo.Text = "";

        }

    }
}
