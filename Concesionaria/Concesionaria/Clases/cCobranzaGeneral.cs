using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cCobranzaGeneral
    {
        public void InsertarCobranza(DateTime Fecha, string Descripcion, double Importe,
            string Nombre,string Telefono,string Direccion,string Patente)
        {
            string sql = "Insert into CobranzaGeneral(Fecha,Importe,Descripcion,Saldo,Cliente,Telefono,Direccion,Patente)";
            sql = sql + "values(" + "'" + Fecha.ToShortDateString () +"'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Descripcion + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Nombre +"'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + Direccion + "'";
            sql = sql + "," + "'" + Patente + "'";
            sql = sql + ")";
             cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetCobranzasGeneralesxFecha(DateTime FechaDesde,DateTime FechaHasta,int SoloImpago,string Concepto,string Cliente)
        {
            string sql = "select CodCobranza,Descripcion,Importe,Fecha,ImportePagado,FechaPago,Saldo,Cliente";
            sql = sql + " from CobranzaGeneral ";
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString () + "'" ;
            sql = sql + " and Fecha <=" + "'" + FechaHasta + "'";
            if (Concepto != "")
                sql = sql + " and Patente like" + "'%" + Concepto + "%'" ;
            if (SoloImpago == 1)
                sql = sql + " and Saldo >0";
            if (Cliente != "")
                sql = sql + " and Cliente like " + "'%" + Cliente  +"%'";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCobranzaxCodigo(Int32 CodCobranza)
        {
            string sql = "select *";
            sql = sql + " from CobranzaGeneral";
            sql = sql + " where CodCobranza=" + CodCobranza.ToString () ;
            return cDb.ExecuteDataTable(sql);
        }

        public void RegistrarCobro(Int32 CodCobranza, DateTime Fecha, double Importe)
        {
            string sql = "update CobranzaGeneral";
            sql = sql + " set ImportePagado =" + Importe.ToString ().Replace (",",".");
            sql = sql + ", Saldo = Saldo - " + Importe.ToString().Replace(",", ".");
            sql = sql + ", FechaPago =" + "'" + Fecha.ToShortDateString ()  +"'";
            sql = sql + " Where CodCobranza=" + CodCobranza.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public double GetTotalCobranza()
        {
            double Importe =0;
            string sql = "select sum(Saldo) as Importe from CobranzaGeneral";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public void AnularCobranza(Int32 CodCobranza)
        {
            string sql = "Update CobranzaGeneral";
            sql = sql + " set Saldo = Importe";
            sql = sql + ", ImportePagado =null";
            sql = sql + ",FechaPago = null";
            sql = sql + " where CodCobranza =" + CodCobranza.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public void PagarSaldo(Int32 CodCobranza, double Importe)
        {
            string sql = "Update CobranzaGeneral ";
            sql = sql + "Set Saldo = Saldo -" + Importe.ToString().Replace(",", ".") ;
            sql = sql + ", ImportePagado = ImportePagado +" + Importe.ToString ().Replace (",",".");
            sql = sql + " where CodCobranza =" + CodCobranza.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetDedudaCobranzaGeneral(string Apellido,string Patente)
        {
            int b = 0;
            string sql = "select * from CobranzaGeneral";
            if (Apellido != "")
            {
                sql = sql + " where Cliente like " + "'%" + Apellido + "%'";
                sql = sql + " and Saldo >0";
                b = 1;
            }
            if (Patente != "")
            {
                if (b == 0)
                {
                    sql = sql + " where Patente like "  +"'%" + Patente + "%'";
                    sql = sql + " and Saldo >0";
                }
                else
                {
                    sql = sql + " and Patente like "  +"'%" + Patente + "%'";
                }
            }
            
            return cDb.ExecuteDataTable(sql);
        }
    }
}
