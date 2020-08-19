using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public static class cGenId
    {
        public static Int32 GetId(string Tabla,string CampoId)
        {
            Int32 Id =0;
            string sql ="select max(" + CampoId + ") as Codigo";
            sql = sql + " from " + Tabla.ToString ();
            DataTable trdo = cDb.ExecuteDataTable (sql);
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["Codigo"].ToString ()!="") 
                    Id = Convert.ToInt32 (trdo.Rows[0]["Codigo"].ToString ());
            Id = Id +1;
            return Id;

        }
    }
}
