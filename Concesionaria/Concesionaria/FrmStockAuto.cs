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
    public partial class FrmStockAuto : Form
    {
        public FrmStockAuto()
        {
            InitializeComponent();
        }

        private void FrmStockAuto_Load(object sender, EventArgs e)
        {
            PintarFormulario();
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmbMarca, "Marca", "Nombre", "CodMarca");
            Buscar();
            //BuscarAutosdeStock("",null);
        }

        private void BuscarAutosdeStock(string Patente,Int32? CodMarca)
        {
            double Total = 0;
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cStockAuto stock = new Clases.cStockAuto();
            DataTable trdo = stock.GetStockDetalladosVigente(Patente,CodMarca);
            trdo = fun.TablaaMiles(trdo, "Costo");
            Total = fun.TotalizarColumna(trdo, "Costo");
            txtTotalVehiculos.Text = trdo.Rows.Count.ToString();
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].Width = 195;
            Grilla.Columns[3].Width = 280;
            Grilla.Columns[4].Width = 90;
            Grilla.Columns[5].Width = 200;
            Grilla.Columns[4].HeaderText = "Fecha";
            Grilla.Columns[5].HeaderText = "Ex Titular";
            Grilla.Columns[7].HeaderText = "Concesión"; 
            txtMontoTotal.Text = Total.ToString();
            if (txtMontoTotal.Text != "")
            {
                txtMontoTotal.Text = fun.SepararDecimales(txtMontoTotal.Text);
                txtMontoTotal.Text = fun.FormatoEnteroMiles(txtMontoTotal.Text);
            }
            

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            string Patente = txtPatente.Text;
            Int32? CodMarca = null;
            if (cmbMarca.SelectedIndex > 0)
                CodMarca = Convert.ToInt32(cmbMarca.SelectedValue);
            Clases.cStockAuto stock = new Clases.cStockAuto();
            DataTable trdo = stock.GetStockDetalladosVigente(Patente, CodMarca);
            txtTotalVehiculos.Text = trdo.Rows.Count.ToString();
            trdo = fun.TablaaMiles(trdo, "Costo");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[7].HeaderText = "Concesión"; 
            //Grilla.Columns[7].Visible = false; 
            Grilla.Columns[2].Width = 170;
            Grilla.Columns[3].Width = 200;
            Grilla.Columns[4].Width = 100;
            Grilla.Columns[5].Width = 200;
            //double Total = fun.TotalizarColumna(trdo, "Costo");
            double Total = fun.TotalizarColumnaCondicion(trdo, "Costo", "Concesion", "0");
            double TotalConcesion = fun.TotalizarColumnaCondicion(trdo, "Costo", "Concesion", "1");
            txtMontoTotal.Text = Total.ToString();
            txtConcesion.Text = TotalConcesion.ToString();
            if (txtMontoTotal.Text != "")
            {
                txtMontoTotal.Text = fun.SepararDecimales(txtMontoTotal.Text);
                txtMontoTotal.Text = fun.FormatoEnteroMiles(txtMontoTotal.Text);
            }
            if (txtConcesion.Text != "")
            {
                txtConcesion.Text = fun.SepararDecimales(txtConcesion.Text);
                txtConcesion.Text = fun.FormatoEnteroMiles(txtConcesion.Text);
            }
        }

        private void PintarFormulario()
        {
            foreach (Control c in this.Controls)
            {
                string name = c.Name;
                if (c is TextBox)
                    c.BackColor = Color.LightGray;
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox || g is MaskedTextBox)
                            g.BackColor = Clases.cConfiguracion.ColorTextBox();
                        //g.BackColor = System.Drawing.SystemColors.Control;   
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show ("Debe seleccionar un registro para continuar",Clases.cMensaje.Mensaje ());
                return;
            }
            string CodStock = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoPrincipalAbm = CodStock.ToString();
            FrmDetalleAuto childForm = new FrmDetalleAuto();
            childForm.Text = "Detalle del vehículo";
            childForm.ShowDialog();
        }

        private void btnBajaStock_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un elemento para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            string msj = "Confirma quitar el auto del stock ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }

            Int32 CodStock = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);
            Int32 Concesion = Convert.ToInt32(Grilla.CurrentRow.Cells[7].Value);
            if (Concesion == 1)
            {
                Clases.cStockAuto stock = new Clases.cStockAuto();
                stock.InsertarBajaStock(CodStock, DateTime.Now);
                MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
                Buscar();
            }
            else
            {
                MessageBox.Show("El auto no esta en concesión", Clases.cMensaje.Mensaje()); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 CodStock = 0;
            string Descripcion = "";
            string Marca = "";
            string Patente = "";
            string Precio = "100.00";
            string Modelo = "";
            string Kilometros = "";
            string Combustible = "";
            string NumeroInterno = "";
            string Ubicacion ="";
            string sql = "";
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cDb.ExecutarNonQuery("delete from ReporteAuto");
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                CodStock = Convert.ToInt32(Grilla.Rows[i].Cells[0].Value.ToString());
                Modelo = GetModeloxCodStock (CodStock);
                Precio = GetPrecioxCodStock(CodStock);
                Kilometros = GetKilometrosxCodStock(CodStock);
                Combustible = GetCombustiblexCodStock(CodStock);
                if (Precio != "")
                {
                    Precio = fun.SepararDecimales(Precio);
                    Precio = fun.FormatoEnteroMiles(Precio);
                }
                    
                Patente = Grilla.Rows[i].Cells[1].Value.ToString();
                NumeroInterno = GetNumeroInternoxPatente(Patente);
                //Ubicacion = GetUbicacion (Patente);
                
                Descripcion = Grilla.Rows[i].Cells[3].Value.ToString();
                Marca = Grilla.Rows[i].Cells[2].Value.ToString();
                sql = "Insert into ReporteAuto(Extra1,Descripcion,Marca,Modelo,Precio,Kilometros,Combustible,Extra2)";
                sql = sql + "values(" + "'" + Patente + "'";
                sql = sql + "," + "'" + Descripcion  +"'";
                sql = sql + "," + "'" + Marca +"'";
                sql = sql + "," + "'" + Modelo +"'";
                sql = sql + "," + "'" + Precio +"'";
                sql = sql + "," + "'" + Kilometros + "'";
                sql = sql + "," + "'" + Combustible + "'";
                sql = sql + "," + "'" + NumeroInterno + "'";
                sql = sql + ")";
                Clases.cDb.ExecutarNonQuery(sql);
            }
            FrmReporteListaPrecio form = new FrmReporteListaPrecio();
            form.Show();
        }

        private string GetNumeroInternoxPatente(string Patente)
        {
            Clases.cAuto auto = new Clases.cAuto();
            DataTable trdo = auto.GetAutoxPatente(Patente);
            string NumeroInterno = "";
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodAuto"].ToString() != "")
                {
                    NumeroInterno = trdo.Rows[0]["NumeroInterno"].ToString(); 
                }
            }
            return NumeroInterno;
        }

         private string GetUbicacion(string Patente)
        {
            Clases.cAuto auto = new Clases.cAuto();
            DataTable trdo = auto.GetAutoxPatente(Patente);
            string Ubicacion = "";
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodAuto"].ToString() != "")
                {
                    Ubicacion = trdo.Rows[0]["Ubicacion"].ToString(); 
                }
            }
            return Ubicacion;
        }
        private string GetModeloxCodStock(Int32 CodStock)
        {
            Clases.cStockAuto obj = new Clases.cStockAuto();
            DataTable trdo = obj.GetStockxCodigo(CodStock);
            string Modelo = "";
            if (trdo.Rows.Count > 0)
            {
                Modelo = trdo.Rows[0]["Anio"].ToString(); 
            }
            return Modelo;
        }

        private string GetPrecioxCodStock(Int32 CodStock)
        {
            Clases.cStockAuto obj = new Clases.cStockAuto();
            DataTable trdo = obj.GetStockxCodigo(CodStock);
            string Precio = "";
            if (trdo.Rows.Count > 0)
            {
                Precio = trdo.Rows[0]["PrecioVenta"].ToString();
            }
            return Precio;
        }

        private string GetKilometrosxCodStock(Int32 CodStock)
        {
            Clases.cStockAuto obj = new Clases.cStockAuto();
            DataTable trdo = obj.GetStockxCodigo(CodStock);
            string Kilometros = "";
            if (trdo.Rows.Count > 0)
            {
                Kilometros = trdo.Rows[0]["Kilometros"].ToString();
            }
            return Kilometros;
        }

        private string GetCombustiblexCodStock(Int32 CodStock)
        {
            Clases.cStockAuto obj = new Clases.cStockAuto();
            DataTable trdo = obj.GetStockxCodigo(CodStock);
            string Combustible = "";
            if (trdo.Rows.Count > 0)
            {
                Combustible = trdo.Rows[0]["Combustible"].ToString();
            }
            return Combustible;
        }
    }
}
