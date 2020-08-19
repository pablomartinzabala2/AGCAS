using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace Concesionaria.Clases
{
    public class cGarantia
    {
        public void Insertar(Int32 CodVenta, string Descripcion, DateTime Fecha, double Importe)
        {
            string sql = "Insert into Garantia(";
            sql = sql + "CodVenta,Descripcion,Fecha,Importe)";
            sql = sql + " values (" + CodVenta.ToString();
            sql = sql + "," + "'" + Descripcion.ToString() + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetGarantias(Int32 CodVenta)
        {
            string sql = "select CodGarantia,Fecha,Descripcion,Importe";
            sql = sql + " from Garantia ";
            sql = sql + " where CodVenta =" + CodVenta.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarGarantias(Int32 CodGarantia)
        {
            string sql = "delete from garantia where CodGarantia =" + CodGarantia.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
