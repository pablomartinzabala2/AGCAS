using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cEntidad
    {
        public string GetNombrexCodigo(Int32 CodEntidad)
        {
            string nombre = "";
            string sql = "select nombre from entidad where CodEntidad =" + CodEntidad.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                nombre = trdo.Rows[0]["Nombre"].ToString(); 
            }
            return nombre;
        }
    }
}
