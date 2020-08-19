using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cMensajeCuotasAnteriores
    {
        public void InsertarMensaje(string Mensaje, DateTime Fecha, Int32 CodGrupo)
        {
            string sql = "Insert into MensajesCuotasAnteriores";
            sql = sql + "(Mensaje,Fecha,CodGrupo)";
            sql = sql + "values(" + "'" + Mensaje + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + CodGrupo.ToString();
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetMensajesxCodVenta(Int32 CodGrupo)
        {
            string sql = "select Fecha,Mensaje from MensajesCuotasAnteriores";
            sql = sql + " where CodGrupo =" + CodGrupo.ToString();
            sql = sql + " order by Fecha Desc";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
