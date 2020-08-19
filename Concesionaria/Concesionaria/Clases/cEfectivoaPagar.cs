using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using System.Data.SqlClient;
namespace Concesionaria.Clases
{
    public class cEfectivoaPagar
    {
        public void Insertar(SqlConnection con, SqlTransaction Transaccion,DateTime Fecha,double Importe,Int32 CodCompra,Int32? CodCliente,Int32 CodAuto)
        {
            string sql = "insert into EfectivosaPagar(Fecha,Importe,Saldo,CodCompra,CodCliente,CodAuto,ImportePagado)";
            sql = sql + "values(" + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + CodCompra.ToString();
            if (CodCliente == null)
                sql = sql + ",null";
            else
                sql = sql + "," + CodCliente.ToString();
            sql = sql + "," + CodAuto.ToString();
            sql = sql + ",0";
            sql = sql + ")";
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = Transaccion;
            comand.CommandText = sql;
            comand.ExecuteNonQuery();
        }

        public DataTable GetEfectivosaPagarxFecha(DateTime FechaDesde, DateTime FechaHasta,string Patente,int SoloImpago)
        {
            string sql = "select e.CodRegistro,e.Fecha,e.Importe,e.FechaPago,e.Saldo,";
            sql = sql + "(select c.Apellido from Cliente c where c.CodCliente = e.CodCliente) as Apellido";
            sql = sql + ",(select a.Patente from auto a where a.CodAuto = e.CodAuto) as Patente";
            sql = sql + ",(select a.Descripcion from auto a where a.CodAuto = e.CodAuto) as Descripcion";
            sql = sql + " from EfectivosaPagar e,auto au";
            sql = sql + " where e.CodAuto = au.CodAuto";
            sql = sql + " and e.Fecha >=" + "'" + FechaDesde.ToShortDateString () + "'" ;
            sql = sql + " and e.Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Patente != "")
                sql = sql + " and au.Patente like " + "'%" + Patente + "%'";
            if (SoloImpago == 1)
                sql = sql + " and e.Saldo >0";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetEfectivosaPagarxCodigo(Int32 CodRegistro)
        {
            string sql = " select e.*";
            sql = sql + ",c.Apellido,c.Nombre,a.Patente,a.Descripcion";
            sql = sql + " from EfectivosaPagar e,auto a,cliente c";
            sql = sql + " where e.CodCliente = c.CodCliente";
            sql = sql + " and e.CodAuto = a.CodAuto";
            sql = sql + " and e.CodRegistro=" + CodRegistro.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void ActualizarPago(Int32 CodRegistro, DateTime Fecha,double Importe)
        {
            string sql = "Update EfectivosaPagar";
            sql = sql + " set Saldo = Saldo -" + Importe.ToString ().Replace (",",".");
            sql = sql + ",ImportePagado = ImportePagado + " + Importe.ToString ().Replace (",",".");
            sql = sql + ",FechaPago=" + "'" + Fecha.ToShortDateString () +"'";
            sql = sql + " where CodRegistro=" + CodRegistro.ToString ();
            cDb.ExecutarNonQuery (sql);
        }

        public Double TotalSaldo()
        {
            double Importe = 0;
            string sql = "select isnull(sum(Saldo),0) as Importe from EfectivosaPagar";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString ());
            }
            return Importe;
        }

        public void Anular(Int32 CodRegistro)
        {
            string sql = "update efectivosapagar";
            sql = sql + " set ImportePagado =0";
            sql = sql + ",Saldo = Importe";
            sql = sql + ",FechaPago =null";
            sql = sql + " where CodRegistro=" + CodRegistro.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetEfectivoPagarxCodCompra(Int32 CodCompra)
        {
            string sql = "select * from EfectivosaPagar where CodCompra=" + CodCompra.ToString();
            return cDb.ExecuteDataTable(sql);
        }
    }
}

