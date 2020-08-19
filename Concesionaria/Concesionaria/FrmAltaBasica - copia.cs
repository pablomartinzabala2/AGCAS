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
    public partial class FrmAltaBasica : Form
    {
        public FrmAltaBasica()
        {
            InitializeComponent();
        }

        private void FrmAltaBasica_Load(object sender, EventArgs e)
        {
            txtTabla.Text = Principal.NombreTablaSecundario;
            txtCampoId.Text = Principal.CampoIdSecundario;
            txtCampoNombre.Text = Principal.CampoNombreSecundario;
            this.Text = "Formulario de alta";
        }

        public void Grabar()
        {
            string Id = txtCampoId.Text;
            string Nombre = txtCampoNombre.Text;
            string Tabla = txtTabla.Text;
            string sql = "insert into " + Tabla;
            sql = sql + "(" + Nombre;
            sql = sql + ")";
            sql = sql + "Values (" + "'" + txtNombre.Text.ToUpper () + "'";
            sql = sql + ")";
            Clases.cDb.ExecutarNonQuery(sql);
            sql = "select max(" + Id.ToString() + ")";
            sql = sql + " as " + Id.ToString();
            sql = sql + " from " + Tabla;
            DataTable trdo = Clases.cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                txtId.Text = trdo.Rows[0][Id].ToString();
            Principal.CampoIdSecundarioGenerado = txtId.Text;
            //FrmAutos frm = new FrmAutos();
            //frm.Focus();
            this.Close();
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundarioGenerado = "";
            this.Close();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                   // foreach (Control g in c.Controls)
                  //  {
                        if (c is TextBox)
                            ((TextBox)c).CharacterCasing = CharacterCasing.Upper;
                 //   }
                    //Empleamos un casteo              
            }                     
        }
    }
}
