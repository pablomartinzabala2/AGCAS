using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cContador
    {
        public Int32 GetCantidad ()
        {
            int Cantidad = 0;
            string sql = "select Numero from Contador";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count >0)
            {
                if (trdo.Rows[0]["Numero"].ToString ()!="")
                {
                    Cantidad = Convert.ToInt32(trdo.Rows[0]["Numero"].ToString());
                }
            }
            return Cantidad;
        }

        public void Actualizar()
        {
            string sql = "Update Contador ";
            sql = sql + " set Numero = Numero - 1";
            cDb.ExecutarNonQuery(sql);
        }
    }
}
