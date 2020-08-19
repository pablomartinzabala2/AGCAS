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
    public partial class FrmBuscadorGenerico : Form
    {
        public FrmBuscadorGenerico()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int Filtro = 0;
            if (Combo.SelectedIndex > 0)
            {
                if (txtBuscar.Text == "")
                {
                    MessageBox.Show ("Debe ingresar un texto a buscar",Clases.cMensaje.Mensaje ()); 
                    return;
                }
                Filtro = 1;
            }

            string sql = "select ";
            string lista = Principal.OpcionesColumnasGrilla;
            string[] vec = lista.Split(';');
            for (int i = 0; i < vec.Count(); i++)
            {
                if (i == 0)
                    sql = sql + vec[i].ToString();
                else
                    sql = sql + "," + vec[i].ToString(); 
            }

            sql = sql + " From " + Principal.TablaPrincipal;
            if (Filtro == 1)
            {
                sql = sql + " where " + GetCampoaBuscar();
                sql = sql + " like " + "'%" + txtBuscar.Text + "%'";
            }
            DataTable trdo = Clases.cDb.ExecuteDataTable(sql);
            Grilla.DataSource  = trdo;
            FormatoColumnas();
        }

        private string GetCampoaBuscar()
        {
            int indice =Convert.ToInt32 ( Combo.SelectedValue.ToString());
            string lista = Principal.OpcionesdeBusqueda;
            string[] vec = lista.Split(';');
            return vec[indice - 1].ToString();
        }

        private void CargarCombo()
        {
            string lista = Principal.OpcionesdeBusqueda;
            string[] vec = lista.Split(';');
            DataTable trdo = new DataTable();
            trdo.Columns.Add("Indice");
            trdo.Columns.Add("Texto");

            DataRow r = trdo.NewRow();
            r[0] = 0;
            r[1] = "---Seleccionar---";
            trdo.Rows.Add(r);
            int Indice = 0;
            string Texto = "";
            for (int i = 0; i < vec.Count(); i++)
            {
                Indice = i + 1;
                Texto = vec[i].ToString();
                DataRow r2 = trdo.NewRow();
                r2[0] = Indice.ToString();
                r2[1] = Texto;
                trdo.Rows.Add(r2);
            }
            Combo.DataSource = trdo;
            Combo.ValueMember = "Indice";
            Combo.DisplayMember = "Texto";
            Combo.SelectedIndex = 0;
        }

        private void FrmBuscadorGenerico_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void FormatoColumnas()
        {
            string Lista = Principal.ColumnasVisibles;
            string[] vec = Lista.Split(';');
            for (int i = 0; i < vec.Count(); i++)
            {
                if (vec[i] == "0")
                    Grilla.Columns[i].Visible = false;
                else
                    Grilla.Columns[i].Visible = true;
            }
            Lista = Principal.ColumnasAncho;
            vec = Lista.Split(';');
            for (int i = 0; i < vec.Count(); i++)
            {
                Grilla.Columns[i].Width =Convert.ToInt32 (vec[i].ToString());  
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un elemento para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            string CODIGO = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoPrincipalAbm = CODIGO;
            string Tabla = Principal.TablaPrincipal;
            switch (Tabla)
            {
                case "Cliente":
                    FrmAbmCliente frm = new FrmAbmCliente();
                    frm.Focus();
                    break;
            }
            
            this.Close();
        }
    }
}
