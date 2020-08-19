using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cBanco
    {
        public string GetBancoxCodigo(Int32 CodBanco)
        {
            string Banco="";
            string sql = "select nombre from banco";
            sql = sql + " where CodBanco =" + CodBanco.ToString ();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                Banco = trdo.Rows[0]["Nombre"].ToString(); 
            }
            return Banco;
        }
    }
}
