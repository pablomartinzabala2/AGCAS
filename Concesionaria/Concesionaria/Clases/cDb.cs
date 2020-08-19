using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Concesionaria.Clases
{
    public static  class cDb
    {
        public static void ExecutarNonQuery(string sql)
        {
            SqlHelper.ExecuteNonQuery(cConexion.Cadenacon(), CommandType.Text, sql);
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            return SqlHelper.ExecuteDataset(cConexion.Cadenacon(), CommandType.Text, sql).Tables[0];  
        }

        public static string ExecuteScalar(string sql, string Campo)
        {
            string Dato = "";
            DataTable trdo = SqlHelper.ExecuteDataset(cConexion.Cadenacon(), CommandType.Text, sql).Tables[0];
            if (trdo.Rows.Count > 0)
            {
                Dato = trdo.Rows[0][Campo].ToString();
            }
            return Dato;
        }  
    }
}
