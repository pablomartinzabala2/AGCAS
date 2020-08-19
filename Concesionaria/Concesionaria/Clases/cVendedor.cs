using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
     public  class cVendedor
    {
         public DataTable GetVendedores()
         {
             string sql = "Select CodVendedor, (Apellido + ' ' + Nombre) as Apellido";
             sql = sql + " from Vendedor ";
             sql = sql + " order by Apellido ";
             return cDb.ExecuteDataTable(sql);
         }

         public void GrabarVendedor(string Ape, string Nom)
         {
             string sql = "insert into Vendedor(Nombre,Apellido)";
             sql = sql + " values (" + "'" + Nom + "'";
             sql = sql + "," + "'" + Ape + "'";
             sql = sql + ")";
             cDb.ExecutarNonQuery(sql);
         }

         public Int32 GetMaxCodVendedor()
         {
             Int32 CodVendedor = 0;
             string sql = "select max(CodVendedor) as CodVendedor ";
             sql = sql + " from Vendedor";
             DataTable trdo = cDb.ExecuteDataTable (sql);
             if (trdo.Rows.Count > 0)
                 CodVendedor = Convert.ToInt32(trdo.Rows[0]["CodVendedor"].ToString());
             return CodVendedor;
         }

         public DataTable GetVendedorxCodigo(Int32 CodVendedor)
         {
             string sql = "select * from Vendedor";
             sql = sql + " where CodVendedor =" + CodVendedor.ToString();
             return cDb.ExecuteDataTable(sql);
         }
    }
}
