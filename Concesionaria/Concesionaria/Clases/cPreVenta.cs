using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cPreVenta
    {
        public DataTable GetPreVentasxFecha(DateTime FechaDesde, DateTime FechaHasta, string Patente,string Apellido)
        {
            string sql = "";
            sql = "select Distinct v.CodPreVenta as CodVenta,a.Patente,a.Descripcion,sa.DescripcionAutoPartePago, c.Apellido,c.Nombre,";
            sql = sql + "v.Fecha,v.ImporteVenta,ImporteEfectivo,v.ImporteAutoPartePago,v.ImporteCredito,v.ImportePrenda";
            sql = sql + ", ( ";
            sql = sql + "  0) as Cheque";
            sql = sql + ", v.ImporteCobranza";
            sql = sql + "," + "(isnull(v.ImporteVenta,0)";
            sql = sql + " - (select isnull(sum(ssa.ImporteCompra),0) from StockAuto ssa where ssa.CodStock = v.CodStock)";
            sql = sql + " - (select isnull(sum(Importe),0) from Costo cst where cst.CodStock = v.CodStock)";
            sql = sql + " -(select isnull(sum(Importe),0) from gasto gst where gst.CodStock = v.CodStock)";
            //sql = sql + " - (select isnull(sum(Importe),0) from GastosRecepcionxAuto gra where gra.CodStock = v.CodStock)";
           // sql = sql + " - (select isnull(sum(Importe),0) from ComisionVendedor co where co.CodVenta = v.CodVenta )";
          //  sql = sql + " - (select isnull(sum(Importe),0) from Garantia gar where gar.CodVenta = v.CodVenta )";
            //sql = sql + " + (select isnull(sum(Importe),0) from DiferenciaTransferencia dif where dif.CodVenta = v.CodVenta )";
            //sql = sql + " + (select isnull(sum(Diferencia),0) from Prenda Pren where Pren.CodVenta = v.CodVenta )";
            //sql = sql + " - (select isnull(sum(Importe),0) from Impuesto Imp where Imp.CodVenta = v.CodVenta )";
            sql = sql + " ) as Ganancia";
            sql = sql + ",v.CodCliente";
            
            sql = sql + " from PreVenta v,cliente c,auto a,stockauto sa";
            sql = sql + " where v.CodCliente = c.CodCliente";
            sql = sql + " and v.CodAutoVendido=a.CodAuto";
            sql = sql + " and a.CodAuto = sa.CodAuto";
            sql = sql + " and v.CodStock = sa.CodStock";
            sql = sql + " and v.Fecha >=" + "'" + FechaDesde + "'";
            sql = sql + " and v.Fecha<=" + "'" + FechaHasta + "'";
            if (Patente != "")
                sql = sql + " and a.Patente like" + "'%" + Patente + "%'";
            sql = sql + " and v.FechaEjecucion is null";
            if (Apellido != null)
                sql = sql + " and c.Apellido like " + "'" + Apellido +"'";
            sql = sql + " order by v.CodPreVenta Desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPreVentaxCodigo(Int32 CodPreVenta)
        {
            string sql = " select *";
            sql = sql + " from PreVenta p,Cliente c, auto a";
            sql = sql + " where p.CodCliente = c.CodCliente";
            sql = sql + " and p.CodAutoVendido = a.CodAuto";
            sql = sql + " and p.CodPreVenta =" + CodPreVenta.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPreVentasxFechaEjecucion(DateTime FechaDesde, DateTime FechaHasta, string Patente)
        {
            string sql = "";
            sql = "select Distinct v.CodPreVenta as CodVenta,a.Patente,a.Descripcion,sa.DescripcionAutoPartePago, c.Apellido,c.Nombre,";
            sql = sql + "v.Fecha,v.ImporteVenta,ImporteEfectivo,v.ImporteAutoPartePago,v.ImporteCredito,v.ImportePrenda";
            sql = sql + ", ( ";
            sql = sql + "  0) as Cheque";
            sql = sql + ", v.ImporteCobranza";
            sql = sql + "," + "(isnull(v.ImporteVenta,0)";
            sql = sql + " - (select isnull(sum(ssa.ImporteCompra),0) from StockAuto ssa where ssa.CodStock = v.CodStock)";
            sql = sql + " - (select isnull(sum(Importe),0) from Costo cst where cst.CodStock = v.CodStock)";
            sql = sql + " -(select isnull(sum(Importe),0) from gasto gst where gst.CodStock = v.CodStock)";
            //sql = sql + " - (select isnull(sum(Importe),0) from GastosRecepcionxAuto gra where gra.CodStock = v.CodStock)";
            // sql = sql + " - (select isnull(sum(Importe),0) from ComisionVendedor co where co.CodVenta = v.CodVenta )";
            //  sql = sql + " - (select isnull(sum(Importe),0) from Garantia gar where gar.CodVenta = v.CodVenta )";
            //sql = sql + " + (select isnull(sum(Importe),0) from DiferenciaTransferencia dif where dif.CodVenta = v.CodVenta )";
            //sql = sql + " + (select isnull(sum(Diferencia),0) from Prenda Pren where Pren.CodVenta = v.CodVenta )";
            //sql = sql + " - (select isnull(sum(Importe),0) from Impuesto Imp where Imp.CodVenta = v.CodVenta )";
            sql = sql + " ) as Ganancia";
            sql = sql + ",v.FechaEjecucion";
            sql = sql + " from PreVenta v,cliente c,auto a,stockauto sa";
            sql = sql + " where v.CodCliente = c.CodCliente";
            sql = sql + " and v.CodAutoVendido=a.CodAuto";
            sql = sql + " and a.CodAuto = sa.CodAuto";
            sql = sql + " and v.CodStock = sa.CodStock";
            sql = sql + " and v.Fecha >=" + "'" + FechaDesde + "'";
            sql = sql + " and v.Fecha<=" + "'" + FechaHasta + "'";
            if (Patente != "")
                sql = sql + " and a.Patente like" + "'%" + Patente + "%'";
            sql = sql + " and v.FechaAnulo is null";
            sql = sql + " order by v.CodPreVenta Desc";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
