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
    public partial class FrmGastos : Form
    {
        public FrmGastos()
        {
            InitializeComponent();
            InicializarComponentes(); 
        }

        private void txtPatente_TextChanged(object sender, EventArgs e)
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
            string Patente = txtPatente.Text;
            int b = 0;
            if (Patente.Length > 5)
            {
                Clases.cAuto auto = new Clases.cAuto();
                DataTable trdo = auto.GetAutoxPatente(Patente);
                if (trdo.Rows.Count > 0)
                {
                    b = 1;
                    txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                    txtAnio.Text = trdo.Rows[0]["Anio"].ToString();
                    txtKms.Text = trdo.Rows[0]["Kilometros"].ToString();
                    txtCodAuto.Text = trdo.Rows[0]["CodAuto"].ToString();
                    txtImporte.Text = trdo.Rows[0]["Importe"].ToString();

                    if (trdo.Rows[0]["CodCiudad"].ToString() != "")
                    {
                        cmbCiudad.SelectedValue = trdo.Rows[0]["CodCiudad"].ToString();
                    }

                    if (trdo.Rows[0]["CodMarca"].ToString() != "")
                    {
                        cmbMarca.SelectedValue = trdo.Rows[0]["CodMarca"].ToString();
                    }

                    Clases.cStockAuto stock = new Clases.cStockAuto();
                    DataTable trdo2 = stock.GetStockAutosVigentes(Convert.ToInt32(txtCodAuto.Text));
                    if (trdo2.Rows.Count > 0)
                    {
                        txtCodStock.Text = trdo2.Rows[0]["CodStock"].ToString();
                        //cargo los gastos x cod stock
                    }
                    CargarGrilla();

                }
            }
            if (b == 0)
                LimpiarAuto();
        }

        private void CargarGrilla()
        {
            Clases.cGasto gasto = new Clases.cGasto();
            DataTable tresul = gasto.GetGastosxCodStock(Convert.ToInt32(txtCodStock.Text));
            Clases.cFunciones fun = new Clases.cFunciones();
            tresul = fun.TablaaMiles(tresul, "Importe");
            Grilla.DataSource = tresul;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 480; 
        }

        private void LimpiarAuto()
        {
            txtCodAuto.Text = "";
            cmbMarca.SelectedIndex = 0;
            txtDescripcion.Text = "";
            txtAnio.Text = "";
            txtKms.Text = "";
            txtImporte.Text = "";
            txtCodStock.Text = "";
            cmbCiudad.SelectedIndex = 0;
            Grilla.DataSource = null;
            //CalcularTotalGeneral();
        }

        private void InicializarComponentes()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmbMarca, "Marca", "Nombre", "CodMarca");
            fun.LlenarCombo(cmbCiudad, "Ciudad", "Nombre", "CodCiudad");
            cmbCiudad.SelectedValue = 1;
            fun.LlenarCombo(CmbCategoriaGasto, "CategoriaGasto", "Nombre", "CodCategoriaGasto");
           // string FechaCorta = DateTime.Now.ToShortDateString();
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtFecha.Text == "")
            {
                MessageBox.Show("Debe ingresar una fecha para continuar.", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta.", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtCodStock.Text == "")
            {
                MessageBox.Show("Debe ingresar un auto para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Int32 CodCategoriaGasto =Convert.ToInt32 (CmbCategoriaGasto.SelectedValue.ToString());
            Clases.cGasto gasto = new Clases.cGasto();
            string Nombre = gasto.GetGastoxCodigo(Convert.ToInt32(CodCategoriaGasto));
            string sImporte = txtImporteGasto.Text;
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);

            //Clases.cFunciones fun = new Clases.cFunciones();
            double Importe = fun.ToDouble(sImporte);
            gasto.InsertarGasto(Convert.ToInt32(txtCodStock.Text), CodCategoriaGasto, Importe, DateTime.Now);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimiento(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, Importe, 0, Fecha);
            CargarGrilla();
        }

        private void txtImporteGasto_Leave(object sender, EventArgs e)
        {
            if (txtImporteGasto.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtImporteGasto.Text = fun.FormatoEnteroMiles(txtImporteGasto.Text);
            }
            
        }

        private void btnAgregarCategoriaGasto_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodCategoriaGasto";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "CategoriaGasto";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
            GrabarGastos();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Principal.CampoIdSecundarioGenerado != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                switch (Principal.NombreTablaSecundario)
                {
                    case "CategoriaGasto":
                        fun.LlenarCombo(CmbCategoriaGasto, "CategoriaGasto", "Nombre", "CodCategoriaGasto");
                        CmbCategoriaGasto.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;                  
                }
            }

        }

        private void GrabarGastos()
        {
            
            Clases.cFunciones fun = new Clases.cFunciones();
            Int32 CodCategoriaGasto = 0;
            double Importe = 0;
            Int32 CodStock = Convert.ToInt32(txtCodStock.Text);
            Clases.cGasto gasto = new Clases.cGasto();
            Clases.cMovimiento mov = new Clases.cMovimiento();
            gasto.BorrarGastoxCodStock(CodStock);
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                CodCategoriaGasto = Convert.ToInt32(Grilla.Rows[i].Cells[0].Value.ToString());
                Importe = fun.ToDouble(Grilla.Rows[i].Cells[2].Value.ToString());
                CodCategoriaGasto = Convert.ToInt32(Grilla.Rows[i].Cells[0].Value.ToString());
                gasto.InsertarGasto(CodStock, CodCategoriaGasto, Importe,DateTime.Now);
                mov.RegistrarMovimiento(-1, Principal.CodUsuarioLogueado, (-1) * Importe, 0, 0, Importe, 0, Fecha);
            }
        }

        private void btnEliminarGasto_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones ();
            Int32 CodCategoriaGasto = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);
            double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString ());
            Clases.cGasto gasto = new Clases.cGasto();
            gasto.BorrarGastoxCategoria(Convert.ToInt32(txtCodStock.Text), CodCategoriaGasto);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            mov.RegistrarMovimiento(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, -1 * Importe, 0, Fecha);
            CargarGrilla();
        }

        private void FrmGastos_Load(object sender, EventArgs e)
        {

        }
    }
}
