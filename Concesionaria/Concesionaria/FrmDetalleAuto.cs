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
    public partial class FrmDetalleAuto : Form
    {
        public FrmDetalleAuto()
        {
            InitializeComponent();
            if (Principal.CodigoPrincipalAbm != "")
            {
                CargarAuto(Convert.ToInt32(Principal.CodigoPrincipalAbm));
                CargarCostoxstock(Convert.ToInt32(Principal.CodigoPrincipalAbm));
                CargarGastosGeneralesxCodStoxk(Convert.ToInt32(Principal.CodigoPrincipalAbm));
                CargarCheques(Convert.ToInt32(Principal.CodigoPrincipalAbm));
                GetTelefonoCliente(Convert.ToInt32(Principal.CodigoPrincipalAbm));
                // GetEfectivoPagar(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            }
        }

        private void CargarAuto(Int32 CodStock)
        {
            Clases.cStockAuto stock = new Clases.cStockAuto();
            DataTable trdoAuto = stock.GetStockxCodigo(CodStock);
            if (trdoAuto.Rows.Count > 0)
            {
                if (trdoAuto.Rows[0]["FechaAlta"].ToString()!="")
                {
                    DateTime FechaIngreso = Convert.ToDateTime(trdoAuto.Rows[0]["FechaAlta"].ToString());
                    txtFechaIngreso.Text = FechaIngreso.ToShortDateString();
                }
                txtPatente.Text = trdoAuto.Rows[0]["Patente"].ToString();
                txtDescripcion.Text = trdoAuto.Rows[0]["Descripcion"].ToString();
                txtkms.Text = trdoAuto.Rows[0]["Kilometros"].ToString();
                txtanio.Text = trdoAuto.Rows[0]["Anio"].ToString();
                txtCiudad.Text = trdoAuto.Rows[0]["Motor"].ToString();
                txtChasis.Text = trdoAuto.Rows[0]["Chasis"].ToString();
                txtMotor.Text = trdoAuto.Rows[0]["Motor"].ToString();
                txtCiudad.Text = trdoAuto.Rows[0]["Ciudad"].ToString();
                txtImporte.Text = trdoAuto.Rows[0]["ImporteCompra"].ToString();
                txtPrecioVenta.Text = trdoAuto.Rows[0]["PrecioVenta"].ToString();
                if (txtImporte.Text != "")
                {
                    txtImporte.Text = txtImporte.Text.Replace(",", ".");
                    string[] vec = txtImporte.Text.Split('.');
                    Clases.cFunciones fun = new Clases.cFunciones();
                    txtImporte.Text = fun.FormatoEnteroMiles(vec[0]);
                }

                if (txtPrecioVenta.Text != "")
                {
                    txtPrecioVenta.Text = txtPrecioVenta.Text.Replace(",", ".");
                    string[] vec = txtPrecioVenta.Text.Split('.');
                    Clases.cFunciones fun = new Clases.cFunciones();
                    txtPrecioVenta.Text = fun.FormatoEnteroMiles(vec[0]);
                }
                txtExTitular.Text = trdoAuto.Rows[0]["ApeNom"].ToString();
                txtAutoPartePago.Text = trdoAuto.Rows[0]["DescripcionAutoPartePago"].ToString();
            }

        }

        public void CargarCostoxstock(Int32 CodStock)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cCosto costo = new Clases.cCosto();
            DataTable trdo = costo.GetCostoxCodigoStock(CodStock);
            //agrego el boton
            Grilla.DataSource = fun.TablaaMiles(trdo, "Importe");
            // Grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            // Grilla.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[2].Width = 400;
            Grilla.Columns[3].Width = 150;
            Grilla.Columns[4].Width = 100;
            Grilla.Columns[3].Width = 100;
            Grilla.Columns[2].HeaderText = "Descripción"; 
            CalcularTotalGeneral();
            Grilla.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            
            // Estilo();

        }

        public void CalcularTotalGeneral()
        {
            int i = 0;
            double Total = 0;
            for (i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                if (Grilla.Rows[i].Cells[4].Value != "")
                {
                    Total = Total + Convert.ToDouble(Grilla.Rows[i].Cells[4].Value);
                }
            }
            txtTotal.Text = Total.ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = txtTotal.Text.Replace(",", ".");
                string[] vec = txtTotal.Text.Split('.');
                Clases.cFunciones fun = new Clases.cFunciones();
                txtTotal.Text = fun.FormatoEnteroMiles(vec[0]);
            }
        }

        private void CargarGastosGeneralesxCodStoxk(Int32 CodStock)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cGastosPagar gastos = new Clases.cGastosPagar();
            DataTable trdo = gastos.GetGastosPagarxCodStock(CodStock);
            trdo = fun.TablaaMiles(trdo, "Importe");
            
            GrillaGastosRecepcion.DataSource = trdo;
            GrillaGastosRecepcion.Columns[0].Width = 260;
            GrillaGastosRecepcion.Columns[3].Width = 120;
            GrillaGastosRecepcion.Columns[3].HeaderText = "Fecha Pago"; 
        }

        private void FrmDetalleAuto_Load(object sender, EventArgs e)
        {

        }

        private void CargarCheques(Int32 CodStock)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cCompra compra = new Clases.cCompra();
            Int32 CodCompra = compra.GetCodCompraxCodStock(CodStock);
            Clases.cChequesaPagar cheque = new Clases.cChequesaPagar();
            DataTable trdo = cheque.GetChequesxCodCompra(CodCompra);
            trdo = fun.TablaaMiles(trdo, "Importe");
            GrillaCheques.DataSource  = trdo;
            GrillaCheques.Columns[3].HeaderText = "Fecha Pago";
            GrillaCheques.Columns[3].Width = 120;
            GrillaCheques.Columns[4].Width = 170;
            DataTable tComp = compra.GetCompraxCodigo(CodCompra);
            GetEfectivoPagar(CodCompra);
            if (tComp.Rows.Count > 0)
            {
                if (tComp.Rows[0]["ImporteEfectivo"].ToString() != "")
                {
                    txtEfectivo.Text = tComp.Rows[0]["ImporteEfectivo"].ToString();
                    txtEfectivo.Text = fun.SepararDecimales(txtEfectivo.Text);
                    txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
                }

                if (tComp.Rows[0]["ImporteAutoPartePago"].ToString() != "")
                {
                    txtImporteAutoPartePago.Text = tComp.Rows[0]["ImporteAutoPartePago"].ToString();
                    txtImporteAutoPartePago.Text = fun.SepararDecimales(txtImporteAutoPartePago.Text);
                    txtImporteAutoPartePago.Text = fun.FormatoEnteroMiles(txtImporteAutoPartePago.Text);
                }

                if (tComp.Rows[0]["CodStockSalida"].ToString() != "")
                {
                    Clases.cStockAuto stock = new Clases.cStockAuto();
                    DataTable tauto = stock.GetStockxCodigo(Convert.ToInt32(tComp.Rows[0]["CodStockSalida"].ToString()));
                    if (tauto.Rows.Count > 0)
                    {
                        txtPatente2.Text = tauto.Rows[0]["Patente"].ToString();
                        txtDescripcion2.Text = tauto.Rows[0]["Descripcion"].ToString();
                    }
                }
                //GetStockxCodigo
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtPrecioVenta_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
           txtPrecioVenta.Text= fun.FormatoEnteroMiles(txtPrecioVenta.Text);
        }

        private void btnGrabarPrecio_Click(object sender, EventArgs e)
        {
            if (txtPrecioVenta.Text == "")
            {
                MessageBox.Show("Debe ingresar un precio para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            double Importe = fun.ToDouble(txtPrecioVenta.Text);
            Int32 CodStock = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            Clases.cStockAuto stock = new Clases.cStockAuto();
            stock.ActualizarPrecioVenta(CodStock, Importe);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
        }

        private void GetEfectivoPagar(Int32 CodCompra)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cEfectivoaPagar eft = new Clases.cEfectivoaPagar();
            DataTable trdo = eft.GetEfectivoPagarxCodCompra(CodCompra);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                {
                    txtEfectivoPagar.Text = trdo.Rows[0]["Importe"].ToString();
                    txtEfectivoPagar.Text = fun.SepararDecimales(txtEfectivoPagar.Text);
                    txtEfectivoPagar.Text = fun.FormatoEnteroMiles(txtEfectivoPagar.Text);
                }
            }
        }

        private void GetTelefonoCliente(Int32 CodStock)
        {
            Clases.cStockAuto stock = new Clases.cStockAuto();
            DataTable trdo = stock.GetStockxCodigo(CodStock);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodCliente"].ToString() != "")
                {
                    Int32 CodCliente = Convert.ToInt32(trdo.Rows[0]["CodCliente"].ToString());
                    Clases.cCliente cli = new Clases.cCliente();
                    DataTable tbCliente = cli.GetClientesxCodigo(CodCliente);
                    if (tbCliente.Rows.Count > 0)
                    {
                        string telefono = tbCliente.Rows[0]["Telefono"].ToString();
                        string NroDoc = tbCliente.Rows[0]["NroDocumento"].ToString();
                        txtTelefono.Text = telefono;
                        string Nombre = tbCliente.Rows[0]["Nombre"].ToString();
                        string Apellido = tbCliente.Rows[0]["Apellido"].ToString();
                        string NomApe = Nombre + " " + Apellido;
                        txtCliente.Text = NomApe;
                        string Celular = tbCliente.Rows[0]["Celular"].ToString();
                        txtCelular.Text = Celular;
                        txtNroDoc.Text = NroDoc;
                    }
                }
            }
        }
    }
}
