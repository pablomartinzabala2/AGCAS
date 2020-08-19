using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    class cMensajeEfectivoPagar
    {
        public void InsertarMensaje(string Mensaje, DateTime Fecha, Int32 CodRegistro)
        {
            string sql = "Insert into MensajesEfectivosPagar";
            sql = sql + "(Descripcion,Fecha,CodRegistro)";
            sql = sql + "values(" + "'" + Mensaje + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + CodRegistro.ToString();
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetMensajesxCodVenta(Int32 CodRegistro)
        {
            string sql = "select Fecha,Descripcion as Descripción from MensajesEfectivosPagar";
            sql = sql + " where CodRegistro =" + CodRegistro.ToString();
            sql = sql + " order by Fecha Desc";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
