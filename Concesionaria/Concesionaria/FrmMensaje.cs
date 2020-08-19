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
    public partial class FrmMensaje : Form
    {
        public FrmMensaje()
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
            Clases.cMensajesCuotas msjCUotas = new Clases.cMensajesCuotas();
            Clases.cMensajeCuotasAnteriores msjCUotasAnt = new Clases.cMensajeCuotasAnteriores();
            if (Principal.NombreTablaSecundario == "MensajesCuotas")
            {
                string Mensaje = txtMensaje.Text;
                Int32 CodVenta = Convert.ToInt32 (txtCodigo.Text);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                msjCUotas.InsertarMensaje(Mensaje, Fecha, CodVenta);
                MessageBox.Show("Datos Grabados Correctamente", Clases.cMensaje.Mensaje());
                DataTable trdo = msjCUotas.GetMensajesxCodVenta(CodVenta);
                Grilla.DataSource = trdo;
                txtMensaje.Text = "";
            }

            if (Principal.NombreTablaSecundario == "MensajesCuotasAnteriores")
            {
                string Mensaje = txtMensaje.Text;
                Int32 CodGrupo = Convert.ToInt32(txtCodigo.Text);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                msjCUotasAnt.InsertarMensaje(Mensaje, Fecha, CodGrupo);
                MessageBox.Show("Datos Grabados Correctamente", Clases.cMensaje.Mensaje());
                DataTable trdo = msjCUotasAnt.GetMensajesxCodVenta(CodGrupo);
                Grilla.DataSource = trdo;
                txtMensaje.Text = "";
            }

            if (Principal.NombreTablaSecundario == "MensajesCobranza")
            {
                Clases.cMensajeCobranza msjCob = new Clases.cMensajeCobranza();
                string Mensaje = txtMensaje.Text;
                Int32 CodGrupo = Convert.ToInt32(txtCodigo.Text);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                msjCob.InsertarMensaje(Mensaje, Fecha, CodGrupo);
                MessageBox.Show("Datos Grabados Correctamente", Clases.cMensaje.Mensaje());
                DataTable trdo = msjCob.GetMensajesxCodCobranza(CodGrupo);
                Grilla.DataSource = trdo;
                txtMensaje.Text = "";
            }

            if (Principal.NombreTablaSecundario == "MensajesPrestamos")
            {
                Clases.cMensajesPrestamo msjCob = new Clases.cMensajesPrestamo();
                string Mensaje = txtMensaje.Text;
                Int32 CodPrestamo = Convert.ToInt32(txtCodigo.Text);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                msjCob.InsertarMensaje(Mensaje, Fecha, CodPrestamo);
                MessageBox.Show("Datos Grabados Correctamente", Clases.cMensaje.Mensaje());
                DataTable trdo = msjCob.GetMensajesxCodPrestamo(CodPrestamo);
                Grilla.DataSource = trdo;
                txtMensaje.Text = "";
            }
        }

        private void FrmMensaje_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtCodigo.Text = Principal.CampoNombreSecundario;
            if (Principal.NombreTablaSecundario == "MensajesCuotas")
            {
                Int32 CodVenta = Convert.ToInt32(txtCodigo.Text);
                Clases.cMensajesCuotas msjCUotas = new Clases.cMensajesCuotas();
                DataTable trdo = msjCUotas.GetMensajesxCodVenta(CodVenta);
                Grilla.DataSource = trdo;
                Grilla.Columns[1].Width = 400; 
            }

            if (Principal.NombreTablaSecundario == "MensajesCuotasAnteriores")
            {
                Int32 CodGrupo = Convert.ToInt32(txtCodigo.Text);
                Clases.cMensajeCuotasAnteriores msjCUotasAnt = new Clases.cMensajeCuotasAnteriores(); 
                 DataTable trdo = msjCUotasAnt.GetMensajesxCodVenta(CodGrupo);
                Grilla.DataSource = trdo;
                Grilla.Columns[1].Width = 400;
            }

            if (Principal.NombreTablaSecundario == "MensajesCobranza")
            {
                Int32 CodGrupo = Convert.ToInt32(txtCodigo.Text);
                Clases.cMensajeCobranza msjCob = new Clases.cMensajeCobranza();
                DataTable trdo = msjCob.GetMensajesxCodCobranza(CodGrupo);
                Grilla.DataSource = trdo;
                Grilla.Columns[1].Width = 400;
            }

            if (Principal.NombreTablaSecundario == "MensajesPrestamos")
            {
                Int32 CodPrestamo = Convert.ToInt32(txtCodigo.Text);
                Clases.cMensajesPrestamo msj = new Clases.cMensajesPrestamo();
                DataTable trdo = msj.GetMensajesxCodPrestamo(CodPrestamo);
                Grilla.DataSource = trdo;
                Grilla.Columns[1].Width = 400;
            }

           
        }

        private void Grilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Grilla.CurrentRow ==null)
            {
                return;
            }
            string msj = Grilla.CurrentRow.Cells[1].Value.ToString();
            txtMensaje.Text = msj;
        }
    }
}
