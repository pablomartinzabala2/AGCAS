using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public  class cCobroInteres
    {
        public void RegistrarCobro(Int32 CodPrestamo, DateTime Fecha, Double Importe)
        {
            string sql = "Insert into CobroIntereses(";
            sql = sql + "CodPrestamo,Fecha,Importe)";
            sql = sql + " values (" + CodPrestamo.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetInteresesPagadosxCodPrestamo(Int32 CodPrestamo)
        {
            string sql = "select CodCobro, Fecha,Importe ";
            sql = sql + " from CobroIntereses ";
            sql = sql + " where CodPrestamo=" + CodPrestamo.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarCobro(Int32 CodPago)
        {
            string sql = "delete from CobroIntereses ";
            sql = sql + " where CodCobro =" + CodPago.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetPagosInteresxFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            string sql = "select p.Nombre,pa.Fecha,pa.Importe";
            sql = sql + " from PrestamoCobrar p,CobroIntereses pa";
            sql = sql + " where p.CodPrestamo = pa.CodPrestamo";
            sql = sql + " and pa.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and pa.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            return cDb.ExecuteDataTable(sql);
        }

        public double GetResumenCobroInteresesxFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            double Importe = 0;
            string sql = "select sum(Importe) as Total from CobroIntereses ";
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Total"].ToString() != "")
                {
                    Importe = Convert.ToDouble(trdo.Rows[0]["Total"].ToString());
                }
            return Importe;
        }
    }
}
