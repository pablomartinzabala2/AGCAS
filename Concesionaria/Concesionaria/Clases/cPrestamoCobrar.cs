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
    }
}
