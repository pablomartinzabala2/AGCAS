using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cPunitorioCuotasAnteriores
    {
        public void GrabarPunitorio(
           Int32 CodGrupo, Int32 Cuota, Double Importe, DateTime Fecha)
        {
            string sql = "Insert into PunitorioCuotasAnteriores(CodGrupo,Cuota,Importe,Fecha)";
            sql = sql + "Values(" + CodGrupo.ToString();
            sql = sql + "," + Cuota.ToString();
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public Double GetImportePunitorio(Int32 CodGrupo, Int32 Cuota)
        {
            Double Importe = 0;
            string sql = "select importe from PunitorioCuotasAnteriores";
            sql = sql + " where CodGrupo=" + CodGrupo.ToString();
            sql = sql + " and Cuota=" + Cuota;
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;

        }

        public Double GetImportexFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            Double Importe = 0;
            string sql = "select sum(Importe) as Importe ";
            sql = sql + " from PunitorioCuotasAnteriores";
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
