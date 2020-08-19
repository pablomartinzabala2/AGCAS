using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Concesionaria.Clases
{
    public  class cGastosPagar
    {
        public double GetGastosaPagar()
        {
            double Total = 0;
            string sql = "select sum(Importe) as Importe";
            sql = sql + " from GastosPagar g";
            sql = sql + " where g.FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString ()!="") 
                Total = Convert.ToDouble (trdo.Rows[0]["Importe"].ToString ());
            return Total;
        }

        public DataTable GetGastosPagarxFecha(DateTime FechaDesde, DateTime FechaHasta,string Patente,int SoloImpago)
        {
            string sql = "select g.CodGasto,a.Patente,g.Descripcion, g.Fecha,g.Importe,g.FechaPago";
            sql = sql + " from GastosPagar g,auto a,StockAuto sa";
            sql = sql + " where g.CodStock = sa.CodStock ";
            sql = sql + " and sa.CodAuto=a.CodAuto";
           // sql = sql + " and sa.FechaBaja is null";
            sql = sql + " and g.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and g.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Patente != "")
                sql = sql + " and a.Patente like" + "'%" + Patente  + "%'" ;
            if (SoloImpago == 1)
                sql = sql + " and g.FechaPago is null";
            sql = sql + " order by g.CodGasto Desc";
            
            return cDb.ExecuteDataTable(sql);
        }

        public void InsertarGastosPagar(Int32 CodAuto,string Descripcion,DateTime Fecha,double Importe,Int32? CodVenta,Int32 CodStock)
        {
            string sqlGastosPagar = ""; 
            sqlGastosPagar = "Insert into GastosPagar(CodAuto,Descripcion";
            sqlGastosPagar = sqlGastosPagar + ",Fecha,Importe,CodVenta,CodStock)";
            sqlGastosPagar = sqlGastosPagar + "values (" + CodAuto.ToString ();
            sqlGastosPagar = sqlGastosPagar + "," + "'" + Descripcion + "'";
            sqlGastosPagar = sqlGastosPagar + "," + "'" + Fecha.ToShortDateString () + "'";
            sqlGastosPagar = sqlGastosPagar + "," + Importe.ToString ().Replace (",",".");
            if (CodVenta !=null) 
                sqlGastosPagar = sqlGastosPagar + "," + CodVenta.ToString();
            else
                sqlGastosPagar = sqlGastosPagar +",null";
            sqlGastosPagar = sqlGastosPagar + "," + CodStock.ToString();
            sqlGastosPagar = sqlGastosPagar + ")";
            cDb.ExecutarNonQuery (sqlGastosPagar);
        }

        public void ActualizarCodStockGastosPagar(Int32 CodVenta)
        {
            string sql = "Update GastosPagar g";
            sql = sql + " set g.CodStock =";
            sql = sql + "(select max(CodStock) as CodStock from StockAuto sa";
            sql = sql + " where sa.CodAuto=g.CodAuto)";
            sql = sql + " where g.CodVenta =" + CodVenta.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetGastosPagarxCodStock(Int32 CodStock)
        { 
            string sql = "Select CodGasto,Descripcion,Fecha,Importe,FechaPago";
            sql = sql + " from GastosPagar ";
            sql = sql + " where CodStock=" + CodStock.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetGastosPagarxCodGasto(Int32 CodGasto)
        {
            string sql = "select g.*,a.Patente from GastosPagar g,auto a ";
            sql = sql + " where g.CodAuto = a.CodAuto";
            sql = sql + " and CodGasto=" + CodGasto.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void ActualizarPago(Int32 CodGasto, DateTime? FechaPago)
        {
            string sql = "update GastosPagar set ";
            if (FechaPago == null)
            {
                sql = sql + " FechaPago=null";
            }
            else
            {
                sql = sql + " FechaPago=" + "'" + FechaPago +"'";
                
            }
            sql = sql + " where CodGasto=" + CodGasto.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public void InsertarGastosPagarTransaccion(SqlConnection  con,SqlTransaction Transaccion, Int32 CodAuto, string Descripcion, DateTime Fecha, double Importe, Int32? CodVenta, Int32 CodStock,Int32? CodCompra)
        {
            string sqlGastosPagar = "";
            sqlGastosPagar = "Insert into GastosPagar(CodAuto,Descripcion";
            sqlGastosPagar = sqlGastosPagar + ",Fecha,Importe,CodVenta,CodStock,CodCompra)";
            sqlGastosPagar = sqlGastosPagar + "values (" + CodAuto.ToString();
            sqlGastosPagar = sqlGastosPagar + "," + "'" + Descripcion + "'";
            sqlGastosPagar = sqlGastosPagar + "," + "'" + Fecha.ToShortDateString() + "'";
            sqlGastosPagar = sqlGastosPagar + "," + Importe.ToString().Replace(",", ".");
            if (CodVenta != null)
                sqlGastosPagar = sqlGastosPagar + "," + CodVenta.ToString();
            else
                sqlGastosPagar = sqlGastosPagar + ",null";
            sqlGastosPagar = sqlGastosPagar + "," + CodStock.ToString();
            if (CodCompra != null)
                sqlGastosPagar = sqlGastosPagar + "," + CodCompra.ToString();
            else
                sqlGastosPagar = sqlGastosPagar + ",null";

            sqlGastosPagar = sqlGastosPagar + ")";
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = Transaccion;
            comand.CommandText = sqlGastosPagar;
            comand.ExecuteNonQuery();
            
        }

        public DataTable GetGastosPagarxCodVenta(Int32 CodVenta)
        {
            string sql = "select *";
            sql = sql + " from GastosPagar ";
            sql = sql + " where CodVenta=" + CodVenta.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void ActualizarCodVenta(SqlConnection con, SqlTransaction Transaccion, Int32 CodVenta, Int32 CodGasto)
        {
            string sql = " update GastosPagar set CodVenta=" + CodVenta.ToString();
            sql = sql + " where CodGasto=" + CodGasto.ToString();
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = Transaccion;
            comand.CommandText = sql;
            comand.ExecuteNonQuery();
        }
    }
}
