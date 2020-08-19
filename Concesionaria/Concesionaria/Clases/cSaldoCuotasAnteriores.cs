using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cSaldoCuotasAnteriores
    {
        public void InsertarSaldoCob(Int32 CodGrupo, Int32 Cuota, DateTime Fecha, double Importe)
        {
            string sql = "Insert into SaldoCuotasAnteriores(";
            sql = sql + "CodGrupo,Cuota,Fecha,Importe)";
            sql = sql + "values (" + CodGrupo.ToString();
            sql = sql + "," + Cuota.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetSaldoCobranza(Int32 CodGrupo)
        {
            string sql = "select * from SaldoCuotasAnteriores";
            sql = sql + " where CodGrupo =" + CodGrupo.ToString();
            return cDb.ExecuteDataTable(sql);
        }
    }
}
