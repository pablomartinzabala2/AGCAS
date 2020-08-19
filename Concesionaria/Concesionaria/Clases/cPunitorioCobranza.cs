using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Concesionaria.Clases
{
    public class cPunitorioCobranza
    {
        public void GrabarPunitorio(
           Int32 CodVenta, Int32 CodCobranza, Double Importe,DateTime Fecha)
        {
            string sql = "Insert into PunitorioCobranza(CodVenta,CodCobranza,Importe,Fecha)";
            sql = sql + "Values(" + CodVenta.ToString();
            sql = sql + "," + CodCobranza.ToString();
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public Double GetImportePunitorio(Int32 CodCobranza)
        {
            Double Importe = 0;
            string sql = "select importe from PunitorioCobranza";
            sql = sql + " where CodCobranza=" + CodCobranza.ToString ();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;

        }

        public void BorrarPunitorio(Int32 CodCobranza)
        {
            string sql = "Delete from PunitorioCobranza";
            sql = sql + " where CodCobranza=" + CodCobranza.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public Double GetImportexFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            Double Importe = 0;
            string sql = "select sum(Importe) as Importe ";
            sql = sql + " from PunitorioCobranza";
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }
    }
}
