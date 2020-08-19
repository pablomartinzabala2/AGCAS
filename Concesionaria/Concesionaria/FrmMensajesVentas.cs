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
    public partial class FrmMensajesVentas : Form
    {
        public FrmMensajesVentas()
        {
            InitializeComponent();
        }

        private void FrmMensajesVentas_Load(object sender, EventArgs e)
        {
            Clases.cMensajesVentas msj = new Clases.cMensajesVentas();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtCodigo.Text = Principal.CodigoPrincipalAbm;
            DataTable trdo = msj.GetMensajesxCodVenta(Convert.ToInt32(txtCodigo.Text));
            Grilla.DataSource = trdo;
            Grilla.Columns[1].Width = 400;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtMensaje.Text == "")
            {
                MessageBox.Show("Debe ingresar un mensaje para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cMensajesVentas msj = new Clases.cMensajesVentas();
            Clases.cMensajeCuotasAnteriores msjCUotasAnt = new Clases.cMensajeCuotasAnteriores();
            string Mensaje = txtMensaje.Text;
            Int32 CodRegistro = Convert.ToInt32(txtCodigo.Text);
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            msj.InsertarMensaje(Mensaje, Fecha, CodRegistro);

            MessageBox.Show("Datos Grabados Correctamente", Clases.cMensaje.Mensaje());
            DataTable trdo = msj.GetMensajesxCodVenta(CodRegistro);
            Grilla.DataSource = trdo;
            txtMensaje.Text = "";
        }
    }
}
