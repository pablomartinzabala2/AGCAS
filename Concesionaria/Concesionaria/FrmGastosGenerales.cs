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
    public partial class FrmGastosGenerales : Form
    {
        public FrmGastosGenerales()
        {
            InitializeComponent();
        }

        private void FrmGastosGenerales_Load(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();  
            txtFecha.Text = DateTime.Now.ToShortDateString();
            fun.LlenarCombo(cmbConcepto, "Entidad", "Nombre", "CodEntidad");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodEntidad";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "Entidad";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Principal.CampoIdSecundarioGenerado != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                switch (Principal.NombreTablaSecundario)
                {
                    case "Entidad":
                        fun.LlenarCombo(cmbConcepto, "Entidad", "Nombre", "CodEntidad");
                        cmbConcepto.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;
                }
            }

        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, Clases.cMensaje.Mensaje()); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                Mensaje("La fecha es incorrecta");
                return;
            }

            if (txtEfectivo.Text == "")
            {
                Mensaje("Debe ingresar un importe");
                return;
            }

            if (cmbConcepto.SelectedIndex == 0)
            {
                if (txtDescripcion.Text == "")
                {
                    Mensaje("Debe ingresar una descripción");
                    return;
                }
            }

            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            double Importe = fun.ToDouble(txtEfectivo.Text);
            string Descripcion = txtDescripcion.Text;
            Int32? CodEntidad = null;
            string NombreEntidad = "";
            if (cmbConcepto.SelectedIndex > 0)
            {
                CodEntidad = Convert.ToInt32(cmbConcepto.SelectedValue);
                Clases.cEntidad ent = new Clases.cEntidad();
                NombreEntidad = ent.GetNombrexCodigo(Convert.ToInt32 ( CodEntidad));
            }

            if (NombreEntidad != "")
            {
                Descripcion = NombreEntidad + "," + Descripcion;
            }
            Clases.cGastosNegocio gasto = new Clases.cGastosNegocio();
            gasto.GrabarGastos(Fecha, CodEntidad, Descripcion, Importe);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, 0, 0, Fecha, Descripcion);
            Mensaje("Datos grabados correctamente");
            txtEfectivo.Text = "";
            txtDescripcion.Text = "";
            txtFecha.Text = "";
            if (cmbConcepto.Items.Count >1) 
                cmbConcepto.SelectedIndex = 0;
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtEfectivo.Text == "")
            {
                txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
            }
        }

        
    }
}
