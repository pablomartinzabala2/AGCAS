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
    public partial class FrmCosto : Form
    {
        public FrmCosto()
        {
            InitializeComponent();
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
            Clases.cFunciones fun = new Clases.cFunciones();
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
                    if (txtImporte.Text != "")
                        txtImporte.Text = fun.SepararDecimales(txtImporte.Text);
                    if (trdo.Rows[0]["CodCiudad"].ToString() != "")
                    {
                        cmbCiudad.SelectedValue = trdo.Rows[0]["CodCiudad"].ToString();
                    }

                    if (trdo.Rows[0]["CodMarca"].ToString() != "")
                    {
                        cmbMarca.SelectedValue = trdo.Rows[0]["CodMarca"].ToString();
                    }

                    if (trdo.Rows[0]["Propio"].ToString() == "1")
                    {
                        radioPropio.Checked = true;
                        radioConcesion.Checked = false;
                    }

                    if (trdo.Rows[0]["Concesion"].ToString() == "1")
                    {
                        radioPropio.Checked = false;
                        radioConcesion.Checked = true;
                    }
                    Clases.cStockAuto stock = new Clases.cStockAuto();
                    DataTable trdo2 = stock.GetStockAutosVigentes(Convert.ToInt32(txtCodAuto.Text));
                    if (trdo2.Rows.Count > 0)
                    {
                        txtCodStock.Text = trdo2.Rows[0]["CodStock"].ToString();
                        CargarCostoxstock(Convert.ToInt32 (txtCodStock.Text));
                    }
                    
                }
            }
            if (b == 0)
                LimpiarAuto();
        }

        private void LimpiarAuto()
        {
            txtCodAuto.Text = "";
            cmbMarca.SelectedIndex = 0;
            txtDescripcion.Text = "";
            txtAnio.Text = "";
            txtKms.Text = "";
            txtImporte.Text = "";
            CargarCostoxstock(-1);
            CalcularTotalGeneral();
            GrillaGastosRecepcion.DataSource = null;
        }

        private void InicializarComponentes()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmbMarca, "Marca", "Nombre", "CodMarca");
            fun.LlenarCombo(cmbCiudad, "Ciudad", "Nombre", "CodCiudad");
            cmbCiudad.SelectedValue = 1;
            string FechaCorta = DateTime.Now.ToShortDateString();
            txtFecha.Text = FechaCorta;
            fun.LlenarCombo(CmbGastoRecepcion, "CategoriaGastoRecepcion", "Descripcion", "Codigo");
        }

        private void FrmCosto_Load(object sender, EventArgs e)
        {
            InicializarComponentes();
        }

        public void CargarCostoxstock(Int32 CodStock)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cCosto costo = new Clases.cCosto();
            DataTable trdo = costo.GetCostoxCodigoStock(CodStock);
            //agrego el boton
            Grilla.DataSource =fun.TablaaMiles (trdo,"Importe");
           // Grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            
           // Grilla.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[2].Width = 440;
            Grilla.Columns[3].Width = 90;
            Grilla.Columns[4].Width = 150;
            Grilla.Columns[2].HeaderText = "Descripción"; 
            CalcularTotalGeneral();
            Grilla.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
           
            //cargo los gastos
            Clases.cGasto gasto = new Clases.cGasto();
            if (txtCodStock.Text != "")
            {
                DataTable tgasto = gasto.GetGastosRecepcionxCodStock2(Convert.ToInt32(txtCodStock.Text));
                GrillaGastosRecepcion.DataSource = tgasto;
                GrillaGastosRecepcion.Columns[0].Visible = false;
                GrillaGastosRecepcion.Columns[2].Visible = false;

                txtImporteGastoRecepcion.Text = "";
                GrillaGastosRecepcion.Columns[1].Width = 450;
                Grilla.Columns[2].HeaderText = "Descripción";
                CalcularTotalGeneral();
                Grilla.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            
        }

        private void btnAgregarCosto_Click(object sender, EventArgs e)
        {
            if (txtCodStock.Text == "")
            {
                MessageBox.Show("Debe ingresar un vehículo para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtCodAuto.Text == "")
            {
                MessageBox.Show("Debe ingresar un auto válido", Clases.cMensaje.Mensaje()); 
                return;
            }
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


            Double Importe = 0;
            if (txtCosto.Text != "")
                Importe = Convert.ToDouble(txtCosto.Text);
            Int32 CodAuto = Convert.ToInt32(txtCodAuto.Text);
            string Descripcion = txtDescripcionCosto.Text + ", PATENTE " + txtPatente.Text;
            string Fecha = txtFecha.Text;
            string Patente = txtPatente.Text;
            Int32? CodStock = -1;
            if (txtCodStock.Text != "")
                CodStock = Convert.ToInt32(txtCodStock.Text);

            Clases.cCosto costo = new Clases.cCosto();
            costo.InsertarCosto(CodAuto, Patente, Importe, Fecha, Descripcion.ToUpper () , CodStock);
            CargarCostoxstock(Convert.ToInt32 (CodStock));
            DateTime FechaCosto = Convert.ToDateTime(txtFecha.Text);

            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, (-1) * (Importe), 0, 0, Importe, 0, FechaCosto,Descripcion.ToUpper ());
            txtCosto.Text = "";
            txtDescripcionCosto.Text = "";
            txtCodCosto.Text = "";
            // Clases.cGrilla.FormatoColumnasMiles(Grilla, "Importe");
        }

        public void CalcularTotalGeneral()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            int i = 0;
            double Total = 0;
            double TotalTotal = 0;
            TotalTotal = fun.ToDouble(txtImporte.Text);
            for (i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                if (Grilla.Rows[i].Cells[4].Value != "")
                {
                    Total = Total + Convert.ToDouble(Grilla.Rows[i].Cells[4].Value);
                }               
            }
            TotalTotal = TotalTotal + Total;
            txtTotal.Text = Total.ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            txtTotalGeneral.Text = TotalTotal.ToString();
            txtTotalGeneral.Text = fun.FormatoEnteroMiles(txtTotalGeneral.Text);
        }

        public void Estilo()
        {
            Grilla.Columns[2].HeaderCell.Style.BackColor = Color.Red;
            Grilla.Columns[2].HeaderCell.Style.ForeColor  = Color.White;
            Grilla.Columns[3].HeaderCell.Style.BackColor = Color.MediumSlateBlue;
            Grilla.Columns[3].HeaderCell.Style.ForeColor = Color.White;
            Grilla.Columns[4].HeaderCell.Style.BackColor = Color.MediumSlateBlue;
            Grilla.Columns[4].HeaderCell.Style.ForeColor = Color.White;
            Grilla.EnableHeadersVisualStyles = false; 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un costo ", Clases.cMensaje.Mensaje());
                return;
            }
            txtCodCosto.Text = Grilla.CurrentRow.Cells[0].Value.ToString();   
            if (txtCodCosto.Text == "")
            {
                
            }
            var resul = MessageBox.Show("Confirma eliminar el costo", Clases.cMensaje.Mensaje(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Double Importe = 0;
            string sImporte ="";
            if (resul == DialogResult.Yes)
            {
                //busco el importe a borrar
                
                 sImporte = "";
                 for (int i = 0; i < Grilla.Rows.Count - 1; i++)
                 {
                     if (Grilla.Rows[i].Cells[0].Value.ToString() == txtCodCosto.Text)
                     {
                         sImporte = Grilla.Rows[i].Cells[4].Value.ToString();
                     }
                 }
                    
            }
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (sImporte != "")
                Importe = fun.ToDouble(sImporte);
                Clases.cCosto costo = new Clases.cCosto();
                Int32 CodCosto = Convert.ToInt32(txtCodCosto.Text);
                costo.BorrarCosto(CodCosto);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                Clases.cMovimiento mov = new Clases.cMovimiento();
                mov.RegistrarMovimiento(-1, Principal.CodUsuarioLogueado, (Importe), 0, 0,(-1)* Importe, 0,Fecha);
                CargarCostoxstock(Convert.ToInt32(txtCodStock.Text));
            }
        
        private void Grilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string resultado = Grilla.SelectedRows.Item(0).Cells(0).Value;
            string Codigo = Grilla.CurrentRow.Cells[0].Value.ToString();
            if (Codigo != "")
            {
                txtDescripcionCosto.Text = Grilla.CurrentRow.Cells[2].Value.ToString();
                txtFecha.Text = Grilla.CurrentRow.Cells[3].Value.ToString();
                txtCosto.Text = Grilla.CurrentRow.Cells[4].Value.ToString();
                txtCodCosto.Text = Grilla.CurrentRow.Cells[0].Value.ToString();
            }
            else
            {
                txtDescripcionCosto.Text = "";
                txtFecha.Text = "";
                txtCosto.Text = "";
                txtCodCosto.Text = ""; 
            }
        }

        private void txtCosto_Leave(object sender, EventArgs e)
        {
            if (txtCosto.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtCosto.Text = fun.FormatoEnteroMiles(txtCosto.Text); 
            }
        }

        private void btnAgregarGastodeRecepcion_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtCodStock.Text == "")
            {
                MessageBox.Show("Debe ingresar un vehículo para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            if (CmbGastoRecepcion.SelectedIndex < 1)
            {
                MessageBox.Show("Debe seleccionar una categoría de gasto de recepción ", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtImporteGastoRecepcion.Text == "")
            {
                MessageBox.Show("Debe ingresar un importe de gasto de recepción ", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cGasto gasto = new Clases.cGasto();
            string Descripcion = gasto.GetNombreGastoRecepcionxCodigo(Convert.ToInt32(CmbGastoRecepcion.SelectedValue));
            AgregarGastoRecepcion(CmbGastoRecepcion.SelectedValue.ToString(), Descripcion, txtImporteGastoRecepcion.Text, "Recepcion");
            GrabarGastosdeRecepcion(Convert.ToInt32(txtCodStock.Text));
        }

        private void AgregarGastoRecepcion(string Codigo, string Descripcion, string Importe, string Tipo)
        {
            /*  for (int i = 0; i < GrillaGastosRecepcion.Rows.Count - 1; i++)
              {
                  if (GrillaGastosRecepcion.Rows[i].Cells[0].Value.ToString() == Codigo.ToString() && GrillaGastos.Rows[i].Cells[2].Value.ToString() == Tipo)
                  {
                      MessageBox.Show("Ya se ha ingresado el gasto", Clases.cMensaje.Mensaje());
                      return;
                  }
              }
             */
            DataTable tListado = new DataTable();
            tListado.Columns.Add("Codigo");
            tListado.Columns.Add("Descripcion");
            tListado.Columns.Add("Tipo");
            tListado.Columns.Add("Importe");
            tListado.Columns.Add("Fecha");
            for (int i = 0; i < GrillaGastosRecepcion.Rows.Count - 1; i++)
            {
                string sCodigo = GrillaGastosRecepcion.Rows[i].Cells[0].Value.ToString();
                string sDescripcion = GrillaGastosRecepcion.Rows[i].Cells[1].Value.ToString();
                string sTipo = GrillaGastosRecepcion.Rows[i].Cells[2].Value.ToString();
                string sImporte = GrillaGastosRecepcion.Rows[i].Cells[3].Value.ToString();
                string sFecha = GrillaGastosRecepcion.Rows[i].Cells[4].Value.ToString();
                DataRow r;
                r = tListado.NewRow();
                r[0] = sCodigo;
                r[1] = sDescripcion;
                r[2] = sTipo;
                r[3] = sImporte;
                r[4] = sFecha;
                tListado.Rows.Add(r);
            }
            DataRow r1;
            r1 = tListado.NewRow();
            r1[0] = Codigo;
            r1[1] = Descripcion;
            r1[2] = Tipo;
            r1[3] = Importe;
            r1[4] = txtFecha.Text;
            tListado.Rows.Add(r1);
            GrillaGastosRecepcion.DataSource = tListado;
            Clases.cFunciones fun = new Clases.cFunciones();
            GrillaGastosRecepcion.Columns[0].Visible = false;
            GrillaGastosRecepcion.Columns[2].Visible = false;

            txtImporteGastoRecepcion.Text = "";
            GrillaGastosRecepcion.Columns[1].Width = 450;
            GrillaGastosRecepcion.Columns[1].HeaderText = "Descripción";

            
        }

        private void txtImporteGastoRecepcion_Leave(object sender, EventArgs e)
        {
            if (txtImporteGastoRecepcion.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtImporteGastoRecepcion.Text = fun.FormatoEnteroMiles(txtImporteGastoRecepcion.Text);
            }
        }

        private void GrabarGastosdeRecepcion(Int32 CodStock)
        {
            string CodGastoRecepcion = "";
            Double Importe = 0;
            Clases.cGasto gasto = new Clases.cGasto();
            gasto.BorrarGastosRecepcionxCodStock(CodStock);
            for (int k = 0; k < GrillaGastosRecepcion.Rows.Count - 1; k++)
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                CodGastoRecepcion = GrillaGastosRecepcion.Rows[k].Cells[0].Value.ToString();
                Importe = fun.ToDouble(GrillaGastosRecepcion.Rows[k].Cells[3].Value.ToString());
                if (CodGastoRecepcion != "")
                {
                   
                    gasto.GrabarGastosRecepcionxCodStock(CodStock, Convert.ToInt32(CodGastoRecepcion), Importe,Convert.ToDateTime(txtFecha.Text));

                }
            }
        }

        private void btnEliminarGastoRecepcion_Click(object sender, EventArgs e)
        {
            if (GrillaGastosRecepcion.Rows.Count < 2)
                return;
            if (GrillaGastosRecepcion.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un gasto de recepción");
                return;
            }
            string Codigo = GrillaGastosRecepcion.CurrentRow.Cells[0].Value.ToString();
            string Tipo = GrillaGastosRecepcion.CurrentRow.Cells[2].Value.ToString();
            if (Codigo != "")
            {
                Clases.cGasto gasto = new Clases.cGasto();
                gasto.BorarGastoRecepcion2(Convert.ToInt32 (txtCodStock.Text),Convert.ToInt32 (Codigo));
            }
            Clases.cGasto gasto2 = new Clases.cGasto();
            DataTable tgasto = gasto2.GetGastosRecepcionxCodStock2(Convert.ToInt32(txtCodStock.Text));
            GrillaGastosRecepcion.DataSource = tgasto;
        }
    }
}
