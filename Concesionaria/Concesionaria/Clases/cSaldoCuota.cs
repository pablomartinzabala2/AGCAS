using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cSaldoCuota
    {
        public void InsertarSaldoCob(Int32 CodVenta,Int32 Cuota, DateTime Fecha, double Importe)
        {
            string sql = "Insert into saldoCuotas(";
            sql = sql + "CodVenta,Cuota,Fecha,Importe)";
            sql = sql + "values (" + CodVenta.ToString();
            sql = sql + "," + Cuota.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetSaldoCobranza(Int32 CodVenta)
        {
            string sql = "select * from SaldoCuotas";
            sql = sql + " where CodVenta =" + CodVenta.ToString();
            return cDb.ExecuteDataTable(sql);
        }
    }
}
