using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Concesionaria
{
    public partial class FrmAnularVenta : Form
    {
        public FrmAnularVenta()
        {
            InitializeComponent();
        }

        private void FrmAnularVenta_Load(object sender, EventArgs e)
        {
            GetVentas();
        }

        private void GetVentas()
        {
            Clases.cVenta venta = new Clases.cVenta();
            DataTable trdo = venta.GetVentas();
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 200;
            Grilla.Columns[2].Width = 200;
            Grilla.Columns[5].Width = 300; 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar ", Clases.cMensaje.Mensaje()); 
                return;
            }

            string msj = "Confirma anular la venta ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }

            string Patente = Grilla.CurrentRow.Cells[4].Value.ToString();   
             string CodVenta = Grilla.CurrentRow.Cells[0].Value.ToString();
            
            //
             Int32 CodAutoPartePago1 = 0;
             Int32 CodAutoPartePago2 = 0;
             double ImportePagadoCobranza = 0;
             double ImportePagadoCuotas = 0;
             double ImportePagadoPrenda = 0;
             double ImportePagadoCheque = 0;

             Clases.cVenta objVenta2 = new Clases.cVenta();
             Clases.cCobranza cobranza = new Clases.cCobranza();
             Clases.cPrenda prenda = new Clases.cPrenda(); 
            ImportePagadoCobranza = cobranza.GetImportePagado(Convert.ToInt32(CodVenta));
             Clases.cCuota cuota = new Clases.cCuota();
             Clases.cCheque cheque = new Clases.cCheque();
            ImportePagadoCuotas = cuota.ImportePagado(Convert.ToInt32(CodVenta));
            ImportePagadoPrenda = prenda.ImportePagado(Convert.ToInt32(CodVenta));
            ImportePagadoCheque = cheque.ImportePagado(Convert.ToInt32(CodVenta));
            DataTable tresult2 = objVenta2.GetAutosPartePago(Convert.ToInt32(CodVenta));
             for (int z = 0; z < tresult2.Rows.Count; z++)
             {
                 if (z == 0)
                 {
                     if (tresult2.Rows[0]["CodAuto"].ToString() != "")
                     {
                         CodAutoPartePago1 = Convert.ToInt32(tresult2.Rows[0]["CodAuto"].ToString());
                     }
                 }

                 if (z == 1)
                 {
                     if (tresult2.Rows[0]["CodAuto"].ToString() != "")
                     {
                         CodAutoPartePago2 = Convert.ToInt32(tresult2.Rows[0]["CodAuto"].ToString());
                     }
                 }
             }

             double ImporteAutoPartePago = 0;
             double ImporteCredito = 0;
             double ImporteEfectivo = 0;
             double ImportePrenda = 0;
             double ImporteCobranza = 0;
             double ImporteBanco = 0;
             double CodAutoVendido = -1;
             double CodAutoPartePago = -1;
             double ImporteAutoNegativo = 0;
            double ImporteSenia =0;
            
             Clases.cVenta objVenta = new Clases.cVenta();
             
            if (CodVenta != "")
             {
                Clases.cMovimiento objMov = new Clases.cMovimiento ();
                ImporteAutoNegativo = objMov.GetImporteAutoNegativoxCodVenta(Convert.ToInt32(CodVenta));
                ImporteAutoNegativo = -1 * ImporteAutoNegativo;
                
                 DataTable trdo = objVenta.GetVentaxCodigo(Convert.ToInt32(CodVenta));
                 if (trdo.Rows.Count > 0)
                 {
                     ImporteCredito = Convert.ToDouble(trdo.Rows[0]["ImporteCredito"].ToString());
                     ImporteEfectivo = Convert.ToDouble(trdo.Rows[0]["ImporteEfectivo"].ToString());
                     ImportePrenda = Convert.ToDouble(trdo.Rows[0]["ImportePrenda"].ToString());
                     ImporteCobranza = Convert.ToDouble(trdo.Rows[0]["ImporteCobranza"].ToString());
                     if (trdo.Rows[0]["PrecioSenia"].ToString()!="")
                        ImporteSenia = Convert.ToDouble(trdo.Rows[0]["PrecioSenia"].ToString());
                     ImporteEfectivo = ImporteEfectivo + ImporteSenia ;
                     if (trdo.Rows[0]["CodAutoVendido"].ToString() != "")
                     {
                         CodAutoVendido = Convert.ToInt32(trdo.Rows[0]["CodAutoVendido"].ToString());
                     }

                     if (trdo.Rows[0]["CodAutoPartePago"].ToString() != "")
                     {
                         CodAutoPartePago = Convert.ToInt32(trdo.Rows[0]["CodAutoPartePago"].ToString());
                     }

                     if (trdo.Rows[0]["ImporteAutoPartePago"].ToString() != "")
                     {
                         ImporteAutoPartePago = Convert.ToDouble(trdo.Rows[0]["ImporteAutoPartePago"].ToString());
                     }

                     if (trdo.Rows[0]["ImporteBanco"].ToString() != "")
                     {
                         ImporteBanco = Convert.ToDouble(trdo.Rows[0]["ImporteBanco"].ToString());
                     }
                 }

                //importe total del credito en documentos usado mas abajo
                 double ImporteTotalDocumento = 0;
                 Clases.cCuota objCuotas = new Clases.cCuota();
                 ImporteTotalDocumento = objCuotas.GetSaldoDeudaCuotas(Convert.ToInt32(CodVenta)); 
                 SqlConnection con = new SqlConnection();
                 con.ConnectionString = Clases.cConexion.Cadenacon();
                 con.Open();
                 SqlTransaction Transaccion;
                 Transaccion = con.BeginTransaction();
                 SqlCommand Comand = new SqlCommand();
                 Comand.Connection = con;
                 Comand.Transaction = Transaccion;
                 try
                 {
                     //vuelvo el auto al stock
                     //string sql = "insert into StockAuto(CodAuto,FechaAlta,CodUsuario)";
                     //sql = sql + " values(" + CodAutoVendido.ToString();
                     //sql = sql + "," + "'" + DateTime.Now.ToShortDateString() + "'";
                     //sql = sql + "," + Principal.CodUsuarioLogueado;
                     //sql = sql + ")";
                     string sql = "update StockAuto  set FechaBaja = null";
                     sql = sql + " where CodStock=";
                     sql = sql + " (select max(CodStock) from StockAuto sa ";
                     sql = sql + " where sa.CodAuto =" + CodAutoVendido.ToString();
                     sql = sql + ")";
                     Comand.CommandText = sql;
                     Comand.ExecuteNonQuery();
                     //si pago con un auto le doy de baja
                     if (CodAutoPartePago > 0)
                     {
                         SqlCommand Comand2 = new SqlCommand();
                         Comand2.Connection = con;
                         Comand2.Transaction = Transaccion;
                         string sqlStock = "update StockAuto";
                         sqlStock = sqlStock + " set FechaBaja =" + "'" + DateTime.Now.ToShortDateString() + "'";
                         sqlStock = sqlStock + " where CodAuto =" + CodAutoPartePago;
                         Comand2.CommandText = sqlStock;
                         Comand2.ExecuteNonQuery();
                     }
                     //borro la venta
                     string sql3 = "delete from venta where CodVenta=" + CodVenta.ToString();
                     SqlCommand Comand3 = new SqlCommand();
                     Comand3.Connection = con;
                     Comand3.Transaction = Transaccion;
                     Comand3.CommandText = sql3;
                     Comand3.ExecuteNonQuery();

                     //borro las cuotas

                     string sql4 = "delete from cuotas where CodVenta=" + CodVenta.ToString();
                     SqlCommand Comand4 = new SqlCommand();
                     Comand4.Connection = con;
                     Comand4.Transaction = Transaccion;
                     Comand4.CommandText = sql4;
                     Comand4.ExecuteNonQuery();

                     //borro la prenda

                     string sqlPrenda = "delete from Prenda where CodVenta=" + CodVenta.ToString();
                     SqlCommand ComandPrenda = new SqlCommand();
                     ComandPrenda.Connection = con;
                     ComandPrenda.Transaction = Transaccion;
                     ComandPrenda.CommandText = sqlPrenda;
                     ComandPrenda.ExecuteNonQuery();

                     //borro los cheques

                     string sqlCheque = "delete from Cheque where CodVenta=" + CodVenta.ToString();
                     SqlCommand ComandCheque = new SqlCommand();
                     ComandCheque.Connection = con;
                     ComandCheque.Transaction = Transaccion;
                     ComandCheque.CommandText = sqlCheque;
                     ComandCheque.ExecuteNonQuery();

                     //si hubo un saldo de cobranza tb lo anulo
                     //ya que significa que habia pagado una cobranza
                     //y debo volver a sacar el efectivo cobrado
                     
                     //borro las cobranzas

                     string sqlCobranza = "delete from Cobranza where CodVenta=" + CodVenta.ToString();
                     SqlCommand ComandCobranza = new SqlCommand();
                     ComandCobranza.Connection = con;
                     ComandCobranza.Transaction = Transaccion;
                     ComandCobranza.CommandText = sqlCobranza;
                     ComandCobranza.ExecuteNonQuery();

                     

                     //borro las comisiones
                     if (ImportePagadoCobranza > 0)
                     {
                         //vuelvo el efectivo atraz
                         ImporteEfectivo = ImporteEfectivo + ImportePagadoCobranza;
                         //
                     }

                     if (ImportePagadoCuotas >0)
                         ImporteEfectivo = ImporteEfectivo + ImportePagadoCuotas ;

                     if (ImportePagadoPrenda > 0)
                         ImporteEfectivo = ImporteEfectivo + ImportePagadoPrenda;
                     
                     if (ImportePagadoCheque >0)
                         ImporteEfectivo = ImporteEfectivo + ImportePagadoCheque;
                     
                     string sqlComision = "delete from ComisionVendedor where CodVenta=" + CodVenta.ToString();
                     SqlCommand ComandComision = new SqlCommand();
                     ComandComision.Connection = con;
                     ComandComision.Transaction = Transaccion;
                     ComandComision.CommandText = sqlComision;
                     ComandComision.ExecuteNonQuery();

                     //Inserto el movimiento con los valores opuesto
                     ImporteCredito = ImporteCredito * (-1);
                     ImporteTotalDocumento = ImporteTotalDocumento * (-1);
                     ImporteEfectivo = ImporteEfectivo * (-1);
                     ImportePrenda = ImportePrenda * (-1);
                     ImporteCobranza = ImporteCobranza * (-1);
                     ImporteBanco = ImporteBanco * (-1);
                     ImporteAutoPartePago = (-1) * ImporteAutoPartePago;


                     string Descrip = "ANULACION VENTA " + Patente.ToString();
                     string sql5 = "Insert into Movimiento(ImporteDocumento,ImporteEfectivo";
                     sql5 = sql5 + ",ImportePrenda,ImporteCobranza,CodUsuario,Fecha,ImporteAuto,ImporteBanco,Descripcion)";
                     sql5 = sql5 + "Values(" + ImporteTotalDocumento.ToString().Replace(",", ".");
                     sql5 = sql5 + "," + ImporteEfectivo.ToString ().Replace (",",".");
                     sql5 = sql5 + "," + ImportePrenda.ToString().Replace(",", ".");
                     sql5 = sql5 + "," + ImporteCobranza.ToString().Replace(",", ".");
                     sql5 = sql5 + "," + Principal.CodUsuarioLogueado.ToString();
                     sql5 = sql5 + "," + "'" + DateTime.Now.ToShortDateString() + "'";
                     sql5 = sql5 + "," + ImporteAutoPartePago.ToString().Replace(",", ".");
                     sql5 = sql5 + "," + ImporteBanco.ToString().Replace(",", ".");
                     sql5 = sql5 + "," + "'" + Descrip + "'";
                     sql5 = sql5 + ")";
                     //finalmente inserto el movimiento opuesto
                     //para que vuelva el valor de la cuenta vehiculo
                     SqlCommand Comand5 = new SqlCommand();
                     Comand5.Connection = con;
                     Comand5.Transaction = Transaccion;
                     Comand5.CommandText = sql5;
                     Comand5.ExecuteNonQuery();

                     string sql5b = "Insert into Movimiento(ImporteDocumento,ImporteEfectivo";
                     sql5b = sql5b + ",ImportePrenda,ImporteCobranza,CodUsuario,Fecha,ImporteAuto,ImporteBanco)";
                     sql5b = sql5b + "Values(" + ImporteTotalDocumento.ToString().Replace(",", ".");
                     sql5b = sql5b + ",0";
                     sql5b = sql5b + ",0";
                     sql5b = sql5b + ",0"; 
                     sql5b = sql5b + "," + Principal.CodUsuarioLogueado.ToString();
                     sql5b = sql5b + "," + "'" + DateTime.Now.ToShortDateString() + "'";
                     sql5b = sql5b + "," + ImporteAutoNegativo.ToString().Replace(",", "."); 
                     sql5b = sql5b + ",0";
                     sql5b = sql5b + ")";
                     //finalmente inserto el movimiento opuesto del auto
                     //para que vuelva el valor de la cuenta vehiculo
                     SqlCommand Comand5b = new SqlCommand();
                     Comand5b.Connection = con;
                     Comand5b.Transaction = Transaccion;
                     Comand5b.CommandText = sql5b;
                     Comand5b.ExecuteNonQuery();

                     string sql6 = "delete from VentasxAuto where CodVenta =" + CodVenta.ToString();
                     SqlCommand Comand6 = new SqlCommand();
                     Comand6.Connection = con;
                     Comand6.Transaction = Transaccion;
                     Comand6.CommandText = sql6;
                     Comand6.ExecuteNonQuery();

                     string sql7 = "delete from GastosPagar where CodVenta =" + CodVenta.ToString();
                     SqlCommand Comand7 = new SqlCommand();
                     Comand7.Connection = con;
                     Comand7.Transaction = Transaccion;
                     Comand7.CommandText = sql7;
                     Comand7.ExecuteNonQuery();

                     // doy de baja los autos del stock que ingresaron
                     // como parte de pago
                     if (CodAutoPartePago1 > 0)
                     {
                         string sql8 = "update StockAuto set FechaBaja=" + "'" + DateTime.Now.ToShortDateString () + "'";
                         sql8 = sql8 + " where CodAuto=" + CodAutoPartePago1.ToString ();
                         SqlCommand Comand8 = new SqlCommand();
                         Comand8.Connection = con;
                         Comand8.Transaction = Transaccion;
                         Comand8.CommandText = sql8;
                         Comand8.ExecuteNonQuery();
                     }

                     Transaccion.Commit();
                     con.Close();
                     GetVentas();
                     MessageBox.Show("Venta anulada correctamente", Clases.cMensaje.Mensaje()); 
                 }
                 catch (Exception ex)
                 {
                     Transaccion.Rollback();
                     MessageBox.Show("Hubo un error en el proceso de anulación de venta", Clases.cMensaje.Mensaje());
                 }
             }
        }
    }
}
