using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public  class cDiferenciaTransferencia
    {
        public void Insertar(Int32? CodVenta,double Importe,Int32 CodGasto)
        {
            string sql = "insert into DiferenciaTransferencia(CodVenta,Importe,CodGasto)";
            if (CodVenta !=null )
                sql = sql + " values (" + CodVenta.ToString();
            else
                sql = sql + " values (null";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + CodGasto.ToString();
            sql = sql + ")";
             cDb.ExecutarNonQuery(sql);
        }

        public void Borrar(Int32 CodGasto)
        {
            string sql = "delete from DiferenciaTransferencia";
            sql = sql + " where CodGasto=" + CodGasto.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public double GetImporteDiferenciaxCodGasto(Int32 CodGasto)
        {
            double Importe = 0;
            string sql = "select sum(importe) as importe from DiferenciaTransferencia where CodGasto=" + CodGasto.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            }
            return Importe;
        }

        public void ActualizarCodVenta(Int32 CodVenta,Int32 CodGasto)
        {
            string sql = " update DiferenciaTransferencia ";
            sql = sql + " set CodVenta=" + CodVenta.ToString();
            sql = sql + " where CodGasto=" + CodGasto;
            cDb.ExecutarNonQuery(sql);
        }
    }
}
