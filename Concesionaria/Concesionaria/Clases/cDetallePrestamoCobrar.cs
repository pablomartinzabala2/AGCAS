using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cDetallePrestamoCobrar
    {
        public void InsertarDetallePrestamo(Int32 CodPrestamo, double Importe,
            string Descripcion, DateTime Fecha)
        {
            string sql = "Insert into DetallePrestamoCobrar(CodPrestamo,Importe,Descripcion,Fecha)";
            sql = sql + " values (" + CodPrestamo.ToString();
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Descripcion + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }
    }
}
