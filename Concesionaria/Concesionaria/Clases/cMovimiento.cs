using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient ;

namespace Concesionaria.Clases
{
    public  class cMovimiento
    {
        public void RegistrarMovimiento(Int32 CodVenta, Int32 CodUsuario, Double ImporteEfectivo,
            Double ImporteDocumento, Double ImportePrenda, Double ImporteAuto, Double ImporteBanco,DateTime Fecha)
        {
            string sql = "insert into Movimiento(";
            sql = sql + "CodVenta,CodUsuario,ImporteEfectivo";
            sql = sql + ",ImporteDocumento,ImportePrenda,ImporteAuto,ImporteBanco,Fecha)";
            if (CodVenta > 0)
                sql = sql + "Values(" + CodVenta.ToString();
            else
                sql = sql + "Values(NULL";
            sql = sql + "," + CodUsuario.ToString();
            sql = sql + "," + ImporteEfectivo.ToString().Replace(",", "."); ;
            sql = sql + "," + ImporteDocumento.ToString().Replace(",", ".");
            sql = sql + "," + ImportePrenda.ToString().Replace(",", ".");
            sql = sql + "," + ImporteAuto.ToString().Replace(",", ".");
            sql = sql + "," + ImporteBanco.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public void RegistrarMovimientoDescripcion(Int32 CodVenta, Int32 CodUsuario, Double ImporteEfectivo,
            Double ImporteDocumento, Double ImportePrenda, Double ImporteAuto, Double ImporteBanco,DateTime Fecha,string Descripcion)
        {
            string sql = "insert into Movimiento(";
            sql = sql + "CodVenta,CodUsuario,ImporteEfectivo";
            sql = sql + ",ImporteDocumento,ImportePrenda,ImporteAuto,ImporteBanco,Fecha,Descripcion)";
            if (CodVenta > 0)
                sql = sql + "Values(" + CodVenta.ToString();
            else
                sql = sql + "Values(NULL";
            sql = sql + "," + CodUsuario.ToString();
            sql = sql + "," + ImporteEfectivo.ToString().Replace(",", "."); ;
            sql = sql + "," + ImporteDocumento.ToString().Replace(",", ".");
            sql = sql + "," + ImportePrenda.ToString().Replace(",", ".");
            sql = sql + "," + ImporteAuto.ToString().Replace(",", ".");
            sql = sql + "," + ImporteBanco.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + "'" + Descripcion + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public double GetImporteAutoNegativoxCodVenta(Int32 CodVenta)
        {
            double Total =0;
            //se usa para obtener la diferencia negativa
            //para volver el importe del auto en parte de pago
            string sql = "select ImporteAuto from Movimiento ";
            sql = sql + " where CodVenta =" + CodVenta.ToString();
            sql = sql + "  and ImporteAuto <0 ";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["ImporteAuto"].ToString() != "")
                    Total = Convert.ToDouble(trdo.Rows[0]["ImporteAuto"].ToString());
            return  Total;
        }

        public DataTable GetMovimientoxFecha(DateTime FechaDesde, DateTime FechaHasta, string Concepto)
        {
            string sql = "select m.Fecha,m.Descripcion,m.ImporteEfectivo";
                sql = sql + " from Movimiento m";
            sql = sql + " where m.Fecha >=" + "'" + FechaDesde.ToShortDateString () + "'" ;
            sql = sql + " and m.Fecha <=" + "'" +  FechaHasta.ToShortDateString () + "'";
            if (Concepto !="")
                sql = sql + " and Descripcion like " + "'" + "%" + Concepto +"%" + "'";
            sql = sql + " and ImporteEfectivo <>0 ";
            sql = sql + " order by m.CodMovimiento desc";
            return cDb.ExecuteDataTable (sql);
        }

        public void RegistrarMovimientoDescripcionTransaccion(SqlConnection con,SqlTransaction Transaccion, Int32 CodVenta, Int32 CodUsuario, Double ImporteEfectivo,
           Double ImporteDocumento, Double ImportePrenda, Double ImporteAuto, Double ImporteBanco, DateTime Fecha, string Descripcion,Int32 CodCompra)
        {
            string sql = "insert into Movimiento(";
            sql = sql + "CodVenta,CodUsuario,ImporteEfectivo";
            sql = sql + ",ImporteDocumento,ImportePrenda,ImporteAuto,ImporteBanco,Fecha,Descripcion,CodCompra)";
            if (CodVenta > 0)
                sql = sql + "Values(" + CodVenta.ToString();
            else
                sql = sql + "Values(NULL";
            sql = sql + "," + CodUsuario.ToString();
            sql = sql + "," + ImporteEfectivo.ToString().Replace(",", "."); ;
            sql = sql + "," + ImporteDocumento.ToString().Replace(",", ".");
            sql = sql + "," + ImportePrenda.ToString().Replace(",", ".");
            sql = sql + "," + ImporteAuto.ToString().Replace(",", ".");
            sql = sql + "," + ImporteBanco.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + "'" + Descripcion + "'";
            if (CodCompra > 0)
                sql = sql + "," + CodCompra.ToString();
            else
                sql = sql + ",NULL";
            sql = sql + ")";

            SqlCommand Comand = new SqlCommand();
            Comand.Connection = con;
            Comand.Transaction = Transaccion;
            Comand.CommandText = sql;
            Comand.ExecuteNonQuery();
            
        }
    
    }
}
