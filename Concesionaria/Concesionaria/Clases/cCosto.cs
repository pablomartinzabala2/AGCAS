using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Concesionaria.Clases
{
    public class cCosto
    {
        public void InsertarCosto(Int32 CodAuto,string Patente,Double? Importe,string Fecha,string Descripcion, Int32? CodStock )
        {
            string sql = "";
            sql = "Insert into Costo(CodAuto,Patente,";
            sql = sql + "Importe,Fecha,Descripcion,CodStock)";
            sql = sql + "values(" + CodAuto.ToString ();
            sql = sql + "," + "'" + Patente  +"'";
            if (Importe ==null)
                sql = sql + ",null";
            else
                sql = sql + "," + Importe.ToString ();
            sql = sql + "," + "'" + Fecha  + "'";
            sql = sql + "," + "'" + Descripcion  + "'";
            if (CodStock == null)
                sql = sql + ",null";
            else
                sql = sql + "," + CodStock.ToString(); 
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetCostoxPatente(string Patente)
        {
            string sql = "select CodCosto,Patente,Descripcion,Fecha,Importe from Costo";
            sql = sql + " where FechaBaja is null ";
            sql = sql + " and Patente =" + "'" + Patente + "'";
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarCosto(Int32 CodCosto)
        {
            string sql = "";
            sql = "delete from costo where CodCosto =" + CodCosto.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetCostoxCodigoStock(Int32 CodStock)
        {
            string sql = "select CodCosto,Patente,Descripcion,Fecha,Importe from Costo";
            sql = sql + " where FechaBaja is null ";
            sql = sql + " and CodStock =" + CodStock.ToString() ;
            return cDb.ExecuteDataTable(sql);
        }
    }
}
