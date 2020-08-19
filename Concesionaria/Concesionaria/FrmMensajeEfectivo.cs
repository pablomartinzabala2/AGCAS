using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace Concesionaria
{
    public partial class FrmMensajeEfectivo : Form
    {
        public FrmMensajeEfectivo()
        {
            InitializeComponent();
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
            Clases.cMensajeEfectivoPagar  msj = new Clases.cMensajeEfectivoPagar();
            Clases.cMensajeCuotasAnteriores msjCUotasAnt = new Clases.cMensajeCuotasAnteriores();
            string Mensaje = txtMensaje.Text;
            Int32 CodRegistro = Convert.ToInt32 (txtCodigo.Text);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                msj.InsertarMensaje(Mensaje, Fecha, CodRegistro);    
            
                MessageBox.Show("Datos Grabados Correctamente", Clases.cMensaje.Mensaje());
                DataTable trdo = msj.GetMensajesxCodVenta(CodRegistro);
                Grilla.DataSource = trdo;
                txtMensaje.Text = "";
        }

        private void FrmMensajeEfectivo_Load(object sender, EventArgs e)
        {
            Clases.cMensajeEfectivoPagar msj = new Clases.cMensajeEfectivoPagar();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtCodigo.Text = Principal.CodigoPrincipalAbm;
            DataTable trdo = msj.GetMensajesxCodVenta(Convert.ToInt32(txtCodigo.Text));
            Grilla.DataSource = trdo;
            Grilla.Columns[1].Width = 400; 
        }
    }
}
