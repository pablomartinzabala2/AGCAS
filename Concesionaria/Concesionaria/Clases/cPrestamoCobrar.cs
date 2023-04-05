using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cPrestamoCobrar
    {
        public Int32  InsertarPrestamo(string Nombre, string Telefono, string Dirección, DateTime Fecha, double Importe,
            double PorcentajeInteres, DateTime FechaVencimiento, Double ImporteaPagar)
        {
            string sql = "insert into PrestamoCobrar(Nombre,Direccion,Telefono";
            sql = sql + ",Fecha,Importe,PorcentajeInteres,FechaVencimiento,ImporteaPagar)";
            sql = sql + " values (" + "'" + Nombre + "'";
            sql = sql + "," + "'" + Dirección + "'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + PorcentajeInteres.ToString().Replace(",", ".");
            sql = sql + "," + "'" + FechaVencimiento.ToShortDateString() + "'";
            sql = sql + "," + ImporteaPagar.ToString().Replace(",", ".");
            sql = sql + ")";
            return cDb.EjecutarEscalar(sql);
        }

        public DataTable GetPrestamosxFecha(DateTime FechaDesde, DateTime FechaHasta, string Nombre, int SoloImpago)
        {
            string sql = "select CodPrestamo, Nombre,Direccion as Dirección,Telefono as Teléfono,FechaVencimiento,Fecha,Importe,ImporteaPagar,FechaPago";
            sql = sql + " from PrestamoCobrar ";
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Nombre != "")
                sql = sql + " and Nombre like " + "'" + "%" + Nombre + "%" + "'";
            if (SoloImpago == 1)
                sql = sql + " and FechaPago is null";
            sql = sql + " order by CodPrestamo desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPrestamoxCodigo(Int32 CodPrestamo)
        {
            string sql = "select * from PrestamoCobrar";
            sql = sql + " where CodPrestamo=" + CodPrestamo.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void ModificarPorcentajePrestamo(Int32 CodPrestamo, Double PorcentajeInteres,
         Double ImportePagar, DateTime FechaVencimiento, double Importe)
        {
            string sql = "Update PrestamoCobrar ";
            sql = sql + " set ImporteaPagar =" + ImportePagar.ToString().Replace(",", ".");
            sql = sql + ",PorcentajeInteres=" + PorcentajeInteres.ToString().Replace(",", ".");
            sql = sql + ",FechaVencimiento=" + "'" + FechaVencimiento.ToShortDateString() + "'";
            sql = sql + ",Importe =" + Importe.ToString().Replace(",", ".");
            sql = sql + " where CodPrestamo =" + CodPrestamo.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
