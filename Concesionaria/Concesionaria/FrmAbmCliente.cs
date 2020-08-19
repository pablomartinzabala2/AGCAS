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
    public partial class FrmAbmCliente : Form
    {
        public FrmAbmCliente()
        {
            InitializeComponent();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmb_CodTipoDoc, "TipoDocumento", "Nombre", "CodTipoDoc");
            if (cmb_CodTipoDoc.Items.Count > 0)
                cmb_CodTipoDoc.SelectedIndex = 1;
            cmb_CodTipoDoc.Enabled = false;
            fun.LlenarCombo(cmb_CodBarrio, "Barrio", "Nombre", "CodBarrio");
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Validar() == true)
            {
                fun.ModificarGenerico(this, "Cliente", "CodCliente", "1");
                //  if (txtCodCLiente.Text =="")
                //      fun.GuardarNuevoGenerico(this, "Cliente");
            }


        }

        private Boolean Validar()
        {
            if (txt_NroDocumento.Text == "")
            {
                MessageBox.Show("Debe ingresar un número de documento para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txt_Apellido.Text == "")
            {
                MessageBox.Show("Debe ingresar un apellido para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txt_Nombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre para continuar", Clases.cMensaje.Mensaje());
                return false;
            }
            return true;
        }

        private void FrmAbmCliente_Load(object sender, EventArgs e)
        {
            Botonera(1);
            Grupo.Enabled = true;
        }

        private void Botonera(int Jugada)
        {
            switch (Jugada)
            {
                //estado inicial
                case 1:
                    btnNuevo.Enabled = true;
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnAceptar.Enabled = false;
                    btnCancelar.Enabled = false;

                    break;
                case 2:
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = true;
                    btnAceptar.Enabled = true;
                    btnCancelar.Enabled = true;

                    break;
                case 3:
                    //viene del buscador
                    btnNuevo.Enabled = true;
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnAceptar.Enabled = false;
                    btnCancelar.Enabled = false;


                    break;
            }


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodCLiente.Text = "";
            Grupo.Enabled = true;
            if (cmb_CodTipoDoc.Items.Count   > 0)
                cmb_CodTipoDoc.SelectedIndex = 1;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
          //  FrmConsultaCLiente form = new FrmConsultaCLiente();
          //  form.FormClosing += new FormClosingEventHandler(form_FormClosing);
          //  form.ShowDialog();
            //codigo generico
            Principal.OpcionesdeBusqueda = "Nombre;Apellido";
            Principal.TablaPrincipal = "Cliente";
            Principal.OpcionesColumnasGrilla = "CodCliente;Nombre;Apellido";
            Principal.ColumnasVisibles = "0;1;1";
            Principal.ColumnasAncho = "0;290;290";
            FrmBuscadorGenerico form = new FrmBuscadorGenerico();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            //CargarJugador(Convert.ToInt32(PRINCIPAL.CDOGIO_JUGADOR));
            if (Principal.CodigoPrincipalAbm != null)
            {
                if (Principal.CodigoPrincipalAbm != "")
                {
                    Botonera(3);
                    txtCodCLiente.Text = Principal.CodigoPrincipalAbm.ToString();
                    
                    if (Principal.CodigoPrincipalAbm != "")
                        fun.CargarControles(this, "Cliente", "CodCliente", txtCodCLiente.Text);
                    Grupo.Enabled = false;
                    return;
                }
                
            }
            
            
            if (Principal.CampoIdSecundarioGenerado != "")
            {
                
                switch (Principal.NombreTablaSecundario)
                {
                    case "Barrio":
                        fun.LlenarCombo(cmb_CodBarrio, "Barrio", "Nombre", "CodBarrio");
                        cmb_CodBarrio.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Grupo.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Botonera(1);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodCLiente.Text = "";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Validar() == true)
            {
                //se usa por las dudas ingreso ya exista el deni
                //y no grabe repetido el documento
                UbicaCliente();
                  if (txtCodCLiente.Text =="")
                      fun.GuardarNuevoGenerico(this, "Cliente");
                  else
                      fun.ModificarGenerico(this, "Cliente", "CodCliente", txtCodCLiente.Text);
                  MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
                  Botonera(1);
                  fun.LimpiarGenerico(this);
                  txtCodCLiente.Text = "";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msj = "Confirma eliminar el Cliente ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            Clases.cCliente clie = new Clases.cCliente();
            if (clie.PuedeBorrar(Convert.ToInt32(txtCodCLiente.Text)))
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                fun.EliminarGenerico("Cliente", "CodCliente", txtCodCLiente.Text);
                MessageBox.Show("El cliente se ha eliminado de la base", Clases.cMensaje.Mensaje());
                fun.LimpiarGenerico(this);
                txtCodCLiente.Text = ""; 
                Botonera(1);
            }
            else
            {
                MessageBox.Show("El cliente no se puede eliminar, se perderían datos historicos.", Clases.cMensaje.Mensaje());
            }
        }

        private void txt_NroDocumento_TextChanged(object sender, EventArgs e)
        { 
            /*
            Int32 CodTipoDoc = 0;
            if (cmbDocumento.SelectedIndex > 0)
                CodTipoDoc = Convert.ToInt32(cmbDocumento.SelectedValue);
            string nroDocumento = txt_NroDocumento.Text;
            Clases.cCliente cliente = new Clases.cCliente();
            DataTable trdo = cliente.GetClientesxNroDoc(CodTipoDoc, nroDocumento);
            if (trdo.Rows.Count > 0)
            {
                txt_Nombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txt_Apellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtM_Telefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txtM_Celular.Text = trdo.Rows[0]["Celular"].ToString();
                txt_Calle.Text = trdo.Rows[0]["Calle"].ToString();
                txt_Numero.Text = trdo.Rows[0]["Numero"].ToString();
                if (trdo.Rows[0]["CodBarrio"].ToString() != "")
                    cmb_CodBarrio.SelectedValue = trdo.Rows[0]["CodBarrio"].ToString();
                txtCodCLiente.Text = trdo.Rows[0]["CodCliente"].ToString();
            }
            else
            {
                txt_Nombre.Text = "";
                txt_Apellido.Text = "";
                txtM_Telefono.Text = "";
                txtM_Celular.Text = "";
                txtCodCLiente.Text = "";
                cmb_CodBarrio.SelectedIndex = 0;
                txt_Calle.Text = "";
                txt_Numero.Text = "";
            }
              */  

                
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevoBarrio_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodBarrio";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "Barrio";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void UbicaCliente()
        {
            
            Int32 CodTipoDoc = 0;
            if (cmb_CodTipoDoc.SelectedIndex > 0)
                CodTipoDoc = Convert.ToInt32(cmb_CodTipoDoc.SelectedValue);
            string nroDocumento = txt_NroDocumento.Text;
            Clases.cCliente cliente = new Clases.cCliente();
            DataTable trdo = cliente.GetClientesxNroDoc(CodTipoDoc, nroDocumento);
            if (trdo.Rows.Count > 0)
            {
                txtCodCLiente.Text = trdo.Rows[0]["CodCliente"].ToString();
            }
            
              
        }

        private void BarraBotones_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        
    }
}