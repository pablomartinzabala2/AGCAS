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
    public partial class FrmAbmMarca : Form
    {
        public FrmAbmMarca()
        {
            InitializeComponent();
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
            txtCodMarca.Text = ""; 
            Grupo.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Grupo.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
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
            Clases.cMarca objMarca = new Clases.cMarca();
            if (objMarca.PuedeBorrar(Convert.ToInt32(txtCodMarca.Text)))
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                fun.EliminarGenerico("Marca", "CodMarca", txtCodMarca.Text);
                MessageBox.Show("La marca se ha eliminado de la base", Clases.cMensaje.Mensaje());
                fun.LimpiarGenerico(this);
                txtCodMarca.Text = "";
                Botonera(1);
            }
            else
            {
                MessageBox.Show("La marca no se puede eliminar, se perderían datos historicos.", Clases.cMensaje.Mensaje());
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Validar() == true)
            {
                //se usa por las dudas ingreso ya exista el deni
                //y no grabe repetido el documento


                if (txtCodMarca.Text == "")
                    fun.GuardarNuevoGenerico(this, "Marca");
                else
                    fun.ModificarGenerico(this, "Marca", "CodMarca", txtCodMarca.Text);
                MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
                Botonera(1);
                fun.LimpiarGenerico(this);
                txtCodMarca.Text = "";
            }
        }

        private Boolean Validar()
        {
            if (txt_Nombre.Text == "")
            {
                MessageBox.Show("Debe ingresar una marca para continuar", Clases.cMensaje.Mensaje());
                return false;
            }
            return true;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
           // FrmConsultaMarca form = new FrmConsultaMarca();
          //  form.FormClosing += new FormClosingEventHandler(form_FormClosing);
          //  form.ShowDialog();
            Principal.OpcionesdeBusqueda = "Nombre";
            Principal.TablaPrincipal = "Marca";
            Principal.OpcionesColumnasGrilla = "CodMarca;Nombre";
            Principal.ColumnasVisibles = "0;1";
            Principal.ColumnasAncho = "0;580";
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
                    txtCodMarca.Text = Principal.CodigoPrincipalAbm.ToString();

                    if (Principal.CodigoPrincipalAbm != "")
                        fun.CargarControles(this, "Marca", "CodMarca", txtCodMarca.Text);
                    Grupo.Enabled = false;
                    return;
                }

            }


            
        }

        private void FrmAbmMarca_Load(object sender, EventArgs e)
        {
            Botonera(1);
            Grupo.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Botonera(1);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodMarca.Text = "";
            Grupo.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_Nombre_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox)
                            ((TextBox)g).CharacterCasing = CharacterCasing.Upper;
                    }
                    //Empleamos un casteo

                }
            }
        }
    }
}
