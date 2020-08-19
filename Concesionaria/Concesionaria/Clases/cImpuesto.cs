using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Concesionaria.Clases
{
    public  class cImpuesto
    {
        public void Insertar(Int32 CodVenta, string Descripcion, DateTime Fecha, double Importe)
        {
            string sql = "Insert into Impuesto(";
            sql = sql + "CodVenta,Descripcion,Fecha,Importe)";
            sql = sql + " values (" + CodVenta.ToString();
            sql = sql + "," + "'" + Descripcion.ToString() + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetImpuestos(Int32 CodVenta)
        {
            string sql = "select CodImpuesto,Fecha,Descripcion,Importe";
            sql = sql + " from Impuesto ";
            sql = sql + " where CodVenta =" + CodVenta.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarImpuesto(Int32 CodImpuesto)
        {
            string sql = "delete from Impuesto where CodImpuesto =" + CodImpuesto.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
