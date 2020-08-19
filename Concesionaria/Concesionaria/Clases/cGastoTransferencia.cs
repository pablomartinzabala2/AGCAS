using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
     public class cGastoTransferencia
    {
         public DataTable GetGastoTransferenciaxCodVenta(Int32 CodVenta)
         {
             string sql = " select *";
             sql = sql + " from GastosTransferencia g,CategoriaGastoTransferencia cg";
             sql = sql + " where g.CodGastoTranasferencia = cg.Codigo";
             sql = sql + " and g.CodVenta =" + CodVenta.ToString();
             return cDb.ExecuteDataTable(sql);
         }
    }
}
