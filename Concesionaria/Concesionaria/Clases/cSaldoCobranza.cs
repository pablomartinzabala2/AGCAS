using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
namespace Concesionaria.Clases
{
    public  class cSaldoCobranza
    {
        public void InsertarSaldoCob(Int32 CodCobranza, DateTime Fecha, double Importe)
        {
            string sql = "Insert into saldocobranza(";
            sql = sql + "CodCobranza,Fecha,Importe)";
            sql = sql + "values (" + CodCobranza.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetSaldoCobranza(Int32 CodCobranza)
        {
            string sql = "select * from SaldoCobranza";
            sql = sql + " where CodCobranza =" + CodCobranza.ToString();
            return cDb.ExecuteDataTable(sql);
        }
    }
}
