using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cSaldoCobranzaGeneral
    {
        public void InsertarSaldoCob(Int32 CodCobranza, DateTime Fecha, double Importe)
        {
            string sql = "Insert into saldocobranzaGeneral(";
            sql = sql + "CodCobranza,Fecha,Importe)";
            sql = sql + "values (" + CodCobranza.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetSaldoCobranza(Int32 CodCobranza)
        {
            string sql = "select * from SaldoCobranzaGeneral";
            sql = sql + " where CodCobranza =" + CodCobranza.ToString();
            return cDb.ExecuteDataTable(sql);
        }
    }
}
