using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cGastosNegocio
    {
        public void GrabarGastos(DateTime Fecha, Int32? CodEntidad, string Descripcion, double Importe)
        {
            string sql = "Insert into GastosNegocio(Fecha,CodEntidad,Descripcion,Importe)";
            sql = sql + "values(" + "'" + Fecha.ToShortDateString () + "'" ;
            if (CodEntidad == null)
                sql = sql + ",null";
            else
                sql = sql + "," + CodEntidad.ToString();
            sql = sql + "," + "'" + Descripcion + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetGastosNegocioxFecha(DateTime FechaDesde, DateTime FechaHasta,string Descripcion)
        {
            string sql = "select g.CodGasto, e.Nombre as Concepto,g.Descripcion,g.Fecha,g.Importe";
            sql = sql + " from GastosNegocio g,entidad e";
            sql = sql + " where g.CodEntidad = e.CodEntidad";
            sql = sql + " and g.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and g.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Descripcion != "")
                sql = sql + " and Descripcion like" + "'%" + Descripcion + "%'" ;
            sql = sql + " order by g.CodGasto Desc";
            return cDb.ExecuteDataTable(sql);
        }

        public double GetGastosNegocioxFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            double Importe = 0;
            string sql = "select sum(Importe) as Total from gastosnegocio ";  
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString () + "'";
            sql = sql + " and Fecha <="  +"'" + FechaHasta.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["Total"].ToString() != "")
                {
                    Importe = Convert.ToDouble(trdo.Rows[0]["Total"].ToString());
                }
            return Importe;
        }
    }
}
