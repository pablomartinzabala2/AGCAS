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
    public partial class FrmAbmAuto : Form
    {
        public FrmAbmAuto()
        {
            InitializeComponent();
        }

        private void FrmAbmAuto_Load(object sender, EventArgs e)
        {
            Botonera(1);
            Grupo.Enabled = false;  
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmb_CodMarca, "Marca", "Nombre", "CodMarca");
            fun.LlenarCombo(cmb_CodCiudad, "Ciudad", "Nombre", "CodCiudad");
            fun.LlenarCombo(cmb_CodTipoCombustible, "TipoCombustible", "Nombre", "Codigo");
            cmb_CodCiudad.SelectedValue = 1;

        }

        private void InicializarComponentes()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmb_CodMarca, "Marca", "Nombre", "CodMarca");
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
            if (txt_Patente.Text == "")
            {
                MessageBox.Show("Debe ingresar una patente para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txt_Kilometros.Text == "")
                txt_Kilometros.Text = "0";

            if (txt_Descripcion.Text == "")
            {
                MessageBox.Show("Debe ingresar una descripción para continuar", Clases.cMensaje.Mensaje());
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
            txtCodAuto.Text = "";
            Grupo.Enabled = true;
            
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            FrmConsultaCLiente form = new FrmConsultaCLiente();
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
                    txtCodAuto.Text = Principal.CodigoPrincipalAbm.ToString();

                    if (Principal.CodigoPrincipalAbm != "")
                        fun.CargarControles(this, "Auto", "CodAuto", txtCodAuto.Text);
                    Grupo.Enabled = false;
                    return;
                }

            }


            //if (Principal.CampoIdSecundarioGenerado != "")
            //{

            //    switch (Principal.NombreTablaSecundario)
            //    {
            //        case "Barrio":
            //            fun.LlenarCombo(cmb_CodBarrio, "Barrio", "Nombre", "CodBarrio");
            //            cmb_CodBarrio.SelectedValue = Principal.CampoIdSecundarioGenerado;
            //            break;
            //    }
            //}
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
            txtCodAuto.Text = "";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Validar() == true)
            {
                //se usa por las dudas ingreso ya exista el deni
                //y no grabe repetido el documento
               // UbicaCliente();
                if (txtCodAuto.Text  == "")
                    fun.GuardarNuevoGenerico(this, "Auto");
                else
                    fun.ModificarGenerico(this, "Cliente", "CodAuto", txtCodAuto.Text);
                MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
                Botonera(1);
                fun.LimpiarGenerico(this);
                txtCodAuto.Text = "";
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
            Clases.cAuto cAuto = new Clases.cAuto();
            if (cAuto.PuedeBorrar(Convert.ToInt32(txtCodAuto.Text)))
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                fun.EliminarGenerico("Cliente", "CodAuto", txtCodAuto.Text);
                MessageBox.Show("El vehículo se ha eliminado de la base", Clases.cMensaje.Mensaje());
                fun.LimpiarGenerico(this);
                txtCodAuto.Text = "";
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

       

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            Botonera(2);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodAuto.Text = "";
            Grupo.Enabled = true;
            if (cmb_CodMarca.Items.Count > 0)
                cmb_CodMarca.SelectedIndex = 1;
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            Botonera(2);
            Grupo.Enabled = true;
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            string msj = "Confirma eliminar la marca ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            Clases.cAuto cauto = new Clases.cAuto();
            if (cauto.PuedeBorrar(Convert.ToInt32(txtCodAuto.Text)))
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                fun.EliminarGenerico("Auto", "CodAuto", txtCodAuto.Text);
                MessageBox.Show("El vehículo se ha eliminado de la base", Clases.cMensaje.Mensaje());
                fun.LimpiarGenerico(this);
                txtCodAuto.Text = "";
                Botonera(1);
            }
            else
            {
                MessageBox.Show("El vehículo no se puede eliminar, se perderían datos historicos.", Clases.cMensaje.Mensaje());
            }
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Validar() == true)
            {
                //se usa por las dudas ingreso ya exista el deni
                //y no grabe repetido el documento
                UbicaAuto(); 

                if (txtCodAuto.Text == "")
                    fun.GuardarNuevoGenerico(this, "Auto");
                else
                    fun.ModificarGenerico(this, "Auto", "CodAuto", txtCodAuto.Text);
                MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
                Botonera(1);
                fun.LimpiarGenerico(this);
                txtCodAuto.Text = "";
            }
        }

        private void btnAbrir_Click_1(object sender, EventArgs e)
        {
            FrmConsultaAuto form = new FrmConsultaAuto();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void UbicaAuto()
        {
            string Patente = txt_Patente.Text;
            Clases.cAuto auto = new Clases.cAuto();
            DataTable trdo = auto.GetAutoxPatente(Patente);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodAuto"].ToString() != "")
                    txtCodAuto.Text = trdo.Rows[0]["CodAuto"].ToString();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Botonera(1);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodAuto.Text = "";
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
