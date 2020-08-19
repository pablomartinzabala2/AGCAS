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
    public partial class FrmListadoPreVenta : Form
    {
        public FrmListadoPreVenta()
        {
            InitializeComponent();
        }

        private void FrmListadoPreVenta_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                MessageBox.Show("Fecha desde incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                MessageBox.Show("Fecha hasta incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (Convert.ToDateTime(txtFechaDesde.Text) > Convert.ToDateTime(txtFechaHasta.Text))
            {
                MessageBox.Show("La fecha desde debe ser inferior a la fecha hasta", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cVenta objVenta = new Clases.cVenta();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            
            Clases.cPreVenta objPreVenta = new Clases.cPreVenta();
            DataTable trdo = objPreVenta.GetPreVentasxFechaEjecucion(FechaDesde, FechaHasta, txtPatente.Text.Trim());
            //le agre[g
            
            Int32 Cant = trdo.Rows.Count;
            
            trdo = fun.TablaaMiles(trdo, "ImporteVenta");
            trdo = fun.TablaaMiles(trdo, "ImporteEfectivo");
            trdo = fun.TablaaMiles(trdo, "ImporteAutoPartePago");
            trdo = fun.TablaaMiles(trdo, "ImporteCredito");
            trdo = fun.TablaaMiles(trdo, "ImportePrenda");
            trdo = fun.TablaaMiles(trdo, "Cheque");
            trdo = fun.TablaaMiles(trdo, "ImporteCobranza");
            trdo = fun.TablaaMiles(trdo, "Ganancia");
            
            
            Grilla.DataSource = trdo;

            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].HeaderText = "Descripción";
            Grilla.Columns[7].HeaderText = "Total";
            Grilla.Columns[8].HeaderText = "Efectivo";
            Grilla.Columns[9].HeaderText = "Vehículo";
            Grilla.Columns[10].HeaderText = "Documentos";
            Grilla.Columns[11].HeaderText = "Prenda";
            Grilla.Columns[13].HeaderText = "Cobranza";

            Grilla.Columns[1].Width = 105;
            Grilla.Columns[2].Width = 160;
            Grilla.Columns[4].Width = 160;
            Grilla.Columns[5].Visible = false;
            Grilla.Columns[3].HeaderText = "Parte Pago";
            Grilla.Columns[7].Width = 80;
            Grilla.Columns[8].Width = 80;
            Grilla.Columns[9].Width = 80;
            Grilla.Columns[10].Width = 80;
            Grilla.Columns[11].Width = 80;
            Grilla.Columns[12].Width = 80;
            Grilla.Columns[13].Width = 80;
            Grilla.Columns[15].HeaderText = "Ejecución"; 
            Grilla.Columns[3].Visible = false;
            Grilla.Columns[9].Visible = false;
            Grilla.Columns[10].Visible = false;
            Grilla.Columns[11].Visible = false;
            Grilla.Columns[12].Visible = false;
            Grilla.Columns[13].Visible = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            string Ejecucion = Grilla.CurrentRow.Cells[15].Value.ToString();
            if (Ejecucion != "")
            {
                MessageBox.Show("La seña ya ha sido ejecutada", Clases.cMensaje.Mensaje());
                return;
            }
            string Codigo = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoSenia = Codigo;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Principal.CodigoSenia = null;
            this.Close();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            string Ejecucion = Grilla.CurrentRow.Cells[15].Value.ToString();
            if (Ejecucion != "")
            {
                MessageBox.Show("La seña ya ha sido ejecutada", Clases.cMensaje.Mensaje());
                return;
            }
            string Fecha = DateTime.Now.ToShortDateString();
            Int32  CodPreVenta =Convert.ToInt32 ( Grilla.CurrentRow.Cells[0].Value.ToString());
            string Patente = Grilla.CurrentRow.Cells[1].Value.ToString();
            double  ImporteSenia = GetImporteSenia(CodPreVenta);
            Int32 CodStock = GetCodStock(CodPreVenta);
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
                 //vuelvo el stock el auto
                 string sql = "update StockAuto  set FechaBaja = null";
                     sql = sql + " where CodStock=" + CodStock.ToString ();
                     Comand.CommandText = sql;
                     Comand.ExecuteNonQuery();
                 //grabos las fechas
                 string sqlFechas ="update preventa";
                 sqlFechas = sqlFechas + " set FechaEjecucion=" + "'" + Fecha + "'";
                 sqlFechas = sqlFechas + ",FechaAnulo =" + "'" + Fecha + "'";
                 sqlFechas = sqlFechas + " where CodPreVenta=" + CodPreVenta.ToString ();
                 SqlCommand comandFechas = new SqlCommand();
                comandFechas.Connection = con;
                comandFechas.Transaction = Transaccion;
                comandFechas.CommandText = sqlFechas ;
                comandFechas.ExecuteNonQuery();

                 //grabo movimeinto
                 SqlCommand comandMovimientoAuto = new SqlCommand();
                comandMovimientoAuto.Connection = con;
                comandMovimientoAuto.Transaction = Transaccion;
                comandMovimientoAuto.CommandText = GetSqlMovimientosSeña(ImporteSenia ,Patente);
                comandMovimientoAuto.ExecuteNonQuery();
                Transaccion.Commit();

                con.Close();

                MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            }
             catch (Exception ex)
             {
                 Transaccion.Rollback();
                 MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
             }
        }

        public double GetImporteSenia(Int32 CodPreVenta)
        {
            double Importe = 0;
            Clases.cPreVenta pre = new Clases.cPreVenta();
            DataTable trdo = pre.GetPreVentaxCodigo(CodPreVenta); 
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["PrecioSenia"].ToString() != "")
                {
                    Importe = Convert.ToDouble(trdo.Rows[0]["PrecioSenia"].ToString());
                }
            return Importe;
        }

        public Int32  GetCodStock(Int32 CodPreVenta)
        {
           
            Int32  CodStock = 0;
            Clases.cPreVenta pre = new Clases.cPreVenta();
            DataTable trdo = pre.GetPreVentaxCodigo(CodPreVenta);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["CodStock"].ToString() != "")
                {
                    CodStock = Convert.ToInt32(trdo.Rows[0]["CodStock"].ToString());
                }
            return CodStock ;
        }

        private string GetSqlMovimientosSeña(double Importe,string Patente)
        {
            string sql = "";
            string Fecha = DateTime.Now.ToShortDateString();
           // Int32 CodAutoVendido = Convert.ToInt32(txtCodAuto.Text);
            Importe = -1 * Importe;
            double ImporteAuto = 0;
            double ImporteDocumento = 0;
            double ImporteEfectivo = Importe;
            double ImportePrenda = 0;
            double ImporteCobranza = 0;
            double ImporteBanco = 0;

            Clases.cFunciones fun = new Clases.cFunciones();
            
            string Descripcion = "ANULACIÓN DE SEÑA DE AUTO " + Patente ;

            //Principal.CodUsuarioLogueado 
            sql = "insert into Movimiento(Fecha,CodUsuario";
            sql = sql + ",ImporteEfectivo,ImporteDocumento,ImportePrenda,ImporteAuto,CodVenta,ImporteCobranza,ImporteBanco,Descripcion)";
            sql = sql + "values(" + "'" + Fecha + "'";
            sql = sql + "," + Principal.CodUsuarioLogueado.ToString();
            sql = sql + "," + ImporteEfectivo.ToString();
            sql = sql + "," + ImporteDocumento.ToString();
            sql = sql + "," + ImportePrenda.ToString();
            sql = sql + "," + ImporteAuto.ToString();
            sql = sql + ",NULL";
            sql = sql + "," + ImporteCobranza.ToString();
            sql = sql + "," + ImporteBanco.ToString();
            sql = sql + "," + "'" + Descripcion + "'";
            sql = sql + ")";
            return sql;
        }
    }
}
