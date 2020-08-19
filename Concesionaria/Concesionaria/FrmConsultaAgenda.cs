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
    public partial class FrmConsultaAgenda : Form
    {
        public FrmConsultaAgenda()
        {
            InitializeComponent();
        }

        private void FrmConsultaAgenda_Load(object sender, EventArgs e)
        {
            DateTime Fecha = DateTime.Now;
            txtFechaHasta.Text = Fecha.ToShortDateString();
            Fecha = Fecha.AddMonths(-1);
            txtFechaDesde.Text = Fecha.ToShortDateString();
            CargarOpciones();
            cFunciones fun = new cFunciones();
            fun.LlenarCombo(cmbMarca, "Marca", "Nombre", "CodMarca");
        }

        private void Mensaje(string Msj)
        {
            MessageBox.Show(Msj, Clases.cMensaje.Mensaje());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buscar();   
        }

        private void Buscar()
        {
            cFunciones fun = new cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                Mensaje("La fecha desde es incorrecta");
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                Mensaje("La fecha dehasta es incorrecta");
                return;
            }

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            cAgenda agenda = new cAgenda();
            string Patente = "";
            string Descripcion = txtDescripcion.Text;
            Double PrecioDesde = 0;
            Double PrecioHasta = 0;
            int IncluyePrecio = 0;
            Int32? CodOpcion = null;
            Int32? CodMarca = null;
            string Telefono = "";
            string Modelo = "";
            
            if (cmbMarca.SelectedIndex > 0)
                CodMarca = Convert.ToInt32(cmbMarca.SelectedValue);
            Modelo = txtModelo.Text;
            Telefono = txtTelefono.Text;
            if (cmbOpciones.SelectedIndex > 0)
                CodOpcion = Convert.ToInt32(cmbOpciones.SelectedValue);

            if (txtPrecioDesde.Text != "" && txtPrecioHasta.Text != "")
            {
                IncluyePrecio = 1;
                PrecioDesde = fun.ToDouble(txtPrecioDesde.Text);
                PrecioHasta = fun.ToDouble(txtPrecioHasta.Text);
            }
            string Cliente = "";
            if (txtNombreCliente.Text != "")
                Cliente = txtNombreCliente.Text;

            DataTable trdo = agenda.GetAgenda(FechaDesde, FechaHasta, IncluyePrecio, PrecioDesde, PrecioHasta, CodOpcion, Patente, Descripcion, Cliente, Telefono, CodMarca, Modelo);
            trdo = fun.TablaaMiles(trdo, "Precio");
            Grilla.DataSource = trdo;
           // Grilla.Columns[5].Width = 170;
            Grilla.Columns[1].Width = 150;
            Grilla.Columns[2].Width = 130;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[7].Visible = false;
        }

        private void txtPrecioDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            cFunciones fun = new cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtPrecioHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            cFunciones fun = new cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtPrecioDesde_Leave(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (txtPrecioDesde.Text !="")
                txtPrecioDesde.Text = fun.FormatoEnteroMiles(txtPrecioDesde.Text);
        }

        private void txtPrecioHasta_Leave(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (txtPrecioHasta.Text !="")
                txtPrecioHasta.Text = fun.FormatoEnteroMiles(txtPrecioHasta.Text);
        }

        private void CargarOpciones()
        {
            cFunciones fun = new cFunciones();
            DataTable tb = fun.CrearTabla("CodOpcion;Opcion");
            tb = fun.AgregarFilas(tb, "1;Comprador");
            tb = fun.AgregarFilas(tb, "2;Vendedor");
            fun.LlenarComboDatatable(cmbOpciones, tb, "Opcion", "CodOpcion");
        }

        private void btnAgregarAuto_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmRegistrarAgenda frm = new FrmRegistrarAgenda();
            frm.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro");
                return;
            }

            string msj = "Confirma eliminar el Cliente ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            Int32 CodAgenda = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);
            cAgenda agenda = new cAgenda();
            agenda.BorrarAgenda(CodAgenda);
            Buscar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro");
                return;
            }
            Int32 CodAgenda = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);
            Principal.CodigoPrincipalAbm = CodAgenda.ToString();
            FrmRegistrarAgenda frm = new FrmRegistrarAgenda();
            frm.ShowDialog();
        }

        private void Grilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        
    }
}
