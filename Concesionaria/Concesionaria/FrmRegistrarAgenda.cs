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
    public partial class FrmRegistrarAgenda : Form
    {
        public FrmRegistrarAgenda()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            cAgenda agenda = new cAgenda();
            
            if (txtApellido.Text == "")
            {
                Mensaje("Debe ingresar un apellido");
                return;
            }
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }
            if (TXTtELEFONO.Text == "")
            {
                Mensaje("Debe ingresar un teléfono para continuar");
                return;
            }
            if (cmbMarca.SelectedIndex < 1)
            {
                Mensaje("Debe seleccionar una marca para continuar");
                return;
            }
            if (txtModelo.Text =="")
            {
                Mensaje ("Debe ingresar un modelo");
                return;
            }
            string Nombre = txtNombre.Text;
            string Apellido = txtApellido.Text;
            DateTime Fecha = Convert.ToDateTime(txtFechaDesde.Text);
            Double Importe = fun.ToDouble(txtImporte.Text);
            string Descripcion = txtDescripcion.Text;
            Int32? CodMarca = null ;
            Double Precio =0;
            int Vendedor = 0;
            int Comprador = 0;
            int CodOpcion = 0;
            string Opcion = "";
            string Telefono = "";
            Int32? CodVendedor = null;
            if (cmbVendedor.SelectedIndex > 0)
                CodVendedor = Convert.ToInt32(cmbVendedor.SelectedValue);
            if (RadioComprador.Checked == true)
            {
                CodOpcion = 1;
               Opcion   = "Comprador";
            }
            else
            {
                CodOpcion = 2;
                Opcion = "Vendedor";
            }
                
            if (cmbMarca.SelectedIndex >0)
                CodMarca = Convert.ToInt32 (cmbMarca.SelectedValue);
            string Modelo = txtModelo.Text ;
            string Patente = txtPatente.Text;
            Telefono = TXTtELEFONO.Text;
            if (txtCodAgenda.Text == "")
            {
                agenda.GrabarAgenda(Nombre, Apellido, CodMarca, Modelo, Importe, Descripcion, CodOpcion, Opcion, Fecha, Patente, CodVendedor,Telefono);
                
            }
            else
            {
                Int32 CodAgenda = Convert.ToInt32 (txtCodAgenda.Text);
                agenda.ModificarAgenda(CodAgenda , Nombre, Apellido, CodMarca, Modelo, Importe, Descripcion, CodOpcion, Opcion, Fecha, Patente, CodVendedor,Telefono);
            }
            Mensaje("Datos grabados correctamente");
            Limpiar();
        }

        private void Mensaje(string Msj)
        {
            MessageBox.Show(Msj, Clases.cMensaje.Mensaje());
        }

        private void FrmRegistrarAgenda_Load(object sender, EventArgs e)
        {
            CargarVendedor();
            
                
            DateTime Fecha = DateTime.Now;
            txtFechaDesde.Text = Fecha.ToShortDateString();
            cFunciones fun = new cFunciones();
            fun.LlenarCombo(cmbMarca, "Marca", "Nombre", "CodMarca");
            if (Principal.CodigoPrincipalAbm != null)
            {
                txtCodAgenda.Text = Principal.CodigoPrincipalAbm;
                GetAgendaxCodigo(Convert.ToInt32(txtCodAgenda.Text));
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            cFunciones fun = new cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void Limpiar()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtPatente.Text = "";
            txtModelo.Text = "";
            cmbMarca.SelectedIndex = 0;
            txtImporte.Text = "";
            TXTtELEFONO.Text = "";
            txtDescripcion.Text = "";
        }

        private void CargarVendedor()
        {
            Clases.cVendedor ven = new Clases.cVendedor();
            DataTable tvend = ven.GetVendedores();
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarComboDatatable(cmbVendedor, tvend, "Apellido", "CodVendedor");
        }

        private void GetAgendaxCodigo(Int32 CodAgenda)
        {
            cFunciones fun = new cFunciones();
            cAgenda agenda = new cAgenda();
            DataTable trdo = agenda.GetAgendaxCodigo(CodAgenda);
            if (trdo.Rows.Count > 0)
            {
                txtCodAgenda.Text = trdo.Rows[0]["CodAgenda"].ToString();
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtModelo.Text = trdo.Rows[0]["Modelo"].ToString();
                txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                if (trdo.Rows[0]["CodMarca"].ToString()!="")
                {
                    Int32 CodMarca =Convert.ToInt32 (trdo.Rows[0]["CodMarca"].ToString());
                    cmbMarca.SelectedValue = CodMarca.ToString();
                }

                if (trdo.Rows[0]["Fecha"].ToString() != "")
                {
                    DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["Fecha"].ToString());
                    txtFechaDesde.Text = Fecha.ToShortDateString();
                }

                TXTtELEFONO.Text = trdo.Rows[0]["Telefono"].ToString();
                if (trdo.Rows[0]["Precio"].ToString() != "")
                {
                    Double Precio = Convert.ToDouble(trdo.Rows[0]["Precio"].ToString());
                    txtImporte.Text = Precio.ToString();
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);

                }
                if (trdo.Rows[0]["CodOpcion"].ToString() != "")
                {
                    Int32 CodOpcion = Convert.ToInt32(trdo.Rows[0]["CodOpcion"].ToString());
                    if (CodOpcion == 1)
                    {
                        RadioComprador.Checked = true;
                        RadioVendedor.Checked = false;
                    }

                    if (CodOpcion == 2)
                    {
                        RadioVendedor.Checked = true;
                        RadioComprador.Checked = false;
                    }

                    if (trdo.Rows[0]["CodVendedor"].ToString() != "")
                    {
                        cmbVendedor.SelectedValue = trdo.Rows[0]["CodVendedor"].ToString();
                    }
                        

                }
            }
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones ();
            if (txtImporte.Text != "")
                txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
        }

        private void btnAlarma_Click(object sender, EventArgs e)
        {
            string Patente = txtPatente.Text;
            string Nombre = txtNombre.Text + " " + txtApellido.Text;
            string Union = Patente + "&" + Nombre;
            Principal.Comodin = Union;
            Principal.CodigoPrincipalAbm = null;
            FrmRegistrarAlarma form = new FrmRegistrarAlarma();
            form.ShowDialog();
        }

    }
}
