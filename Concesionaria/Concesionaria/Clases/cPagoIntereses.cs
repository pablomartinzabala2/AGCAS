using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public  class cPagoIntereses
    {
        public void RegistrarPago(Int32 CodPrestamo,DateTime Fecha,Double Importe)
        {
            string sql = "Insert into PagosIntereses(";
            sql = sql + "CodPrestamo,Fecha,Importe)";
            sql = sql + " values (" + CodPrestamo.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString () +"'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetInteresesPagadosxCodPrestamo(Int32 CodPrestamo)
        {
            string sql = "select CodPago, Fecha,Importe ";
            sql = sql + " from PagosIntereses ";
            sql = sql + " where CodPrestamo=" + CodPrestamo.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarPago(Int32 CodPago)
        {
            string sql = "delete from PagosIntereses";
            sql = sql + " where CodPago =" + CodPago.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetPagosInteresxFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            string sql = "select p.Nombre,pa.Fecha,pa.Importe";
            sql = sql + " from Prestamo p,PagosIntereses pa";
            sql = sql + " where p.CodPrestamo = pa.CodPrestamo";
            sql = sql + " and pa.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and pa.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            return cDb.ExecuteDataTable(sql);
        }

        public double GetResumenPagosInteresesxFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            double Importe = 0;
            string sql = "select sum(Importe) as Total from PagosIntereses ";
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
