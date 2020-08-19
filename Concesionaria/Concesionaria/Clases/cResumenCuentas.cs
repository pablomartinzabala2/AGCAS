using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cResumenCuentas
    {
        public DataTable GetResumenCuentas()
        {
            //obtengos los importes sin procesar
            string sql = "select sum(ImporteEfectivo) as ImporteEfectivo, sum(ImportePrenda) as ImportePrenda";
            sql = sql + ", sum(ImporteAuto) as ImporteAuto, sum(ImporteCobranza) as ImporteCobranza, sum(ImporteBanco) as ImporteBanco";
            sql = sql + " from Movimiento";
            sql = sql + " where Procesado is null";
            DataTable tMov = cDb.ExecuteDataTable(sql);
            //obtengo los resumenes
            string sql2 = "select * from ResumenCuentas";
            DataTable tResumen = cDb.ExecuteDataTable(sql2);
            double ImporteEfectivo = 0;
            double ImportePrenda = 0;
            double ImporteAuto = 0;
            double ImporteCobranza = 0;
            double ImporteBanco = 0;
            //obtengo los importe en efectivo de los no procesados
            for (int i = 0; i < tMov.Rows.Count; i++)
            {
                if (tMov.Rows[i]["ImporteEfectivo"].ToString() != "")
                    ImporteEfectivo = ImporteEfectivo + Convert.ToDouble(tMov.Rows[i]["ImporteEfectivo"].ToString());

                if (tMov.Rows[i]["ImportePrenda"].ToString() != "")
                    ImportePrenda = ImportePrenda + Convert.ToDouble(tMov.Rows[i]["ImportePrenda"].ToString());

                if (tMov.Rows[i]["ImporteAuto"].ToString() != "")
                    ImporteAuto = ImporteAuto + Convert.ToDouble(tMov.Rows[i]["ImporteAuto"].ToString());
                
                if (tMov.Rows[i]["ImporteCobranza"].ToString() != "")
                    ImporteCobranza = ImporteCobranza + Convert.ToDouble(tMov.Rows[i]["ImporteCobranza"].ToString());

                if (tMov.Rows[i]["ImporteBanco"].ToString() != "")
                    ImporteBanco = ImporteBanco + Convert.ToDouble(tMov.Rows[i]["ImporteBanco"].ToString());


            }

            //sumo los importes a los resumenes
            if (tResumen.Rows.Count > 0)
            {
                ImporteEfectivo = ImporteEfectivo + Convert.ToDouble(tResumen.Rows[0]["ImporteEfectivo"]);
                ImportePrenda = ImportePrenda + Convert.ToDouble(tResumen.Rows[0]["ImportePrenda"]);
                ImporteAuto = ImporteAuto + Convert.ToDouble(tResumen.Rows[0]["ImporteAuto"]);
                ImporteCobranza = ImporteCobranza + Convert.ToDouble(tResumen.Rows[0]["ImporteCobranza"]);
                ImporteBanco = ImporteBanco + Convert.ToDouble(tResumen.Rows[0]["ImporteBanco"]);
                }

            //actualizo los resumenes nuevos
            string sql3 = "delete from ResumenCuentas";
            cDb.ExecutarNonQuery(sql3);

            string sql4 = "Insert into ResumenCuentas(ImporteEfectivo,ImportePrenda,ImporteAuto,ImporteCobranza,ImporteBanco)";
            sql4 = sql4 + " values (" + ImporteEfectivo.ToString().Replace(",", ".");
            sql4 = sql4 + "," + ImportePrenda.ToString().Replace(",", ".");
            sql4 = sql4 + "," + ImporteAuto.ToString().Replace(",", ".");
            sql4 = sql4 + "," + ImporteCobranza.ToString().Replace(",", ".");
            sql4 = sql4 + "," + ImporteBanco.ToString().Replace(",", ".");
            sql4 = sql4 + ")";
            cDb.ExecutarNonQuery(sql4);
            //actualizo los movimientos
            string sql5 = "Update Movimiento set Procesado =1";
            cDb.ExecutarNonQuery(sql5);
            sql = "select * from ResumenCuentas";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
