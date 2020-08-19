using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cComisionVendedor
    {
        public double GetComisionesPendientes()
        {
            double Importe = 0;
            string sql = "select sum(Importe) as Importe from comisionvendedor";
            sql = sql + " where FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            }
            return Importe;
        }

        public DataTable GetComisionesxFecha(DateTime FechaDesde, DateTime FechaHasta, int Impago,string Apellido,string Patente)
        {
            string sql = "select c.CodComision, v.Apellido,v.Nombre,c.Fecha,c.Importe,c.FechaPago,v.CodVendedor";
            sql = sql + ", (select a.Patente from venta v,auto a where a.CodAuto =v.CodAutoVendido and v.CodVenta = c.CodVenta) as Patente";
           // sql = sql + ",a.Patente";
            sql = sql + ",au.Descripcion";
            sql = sql + " from ComisionVendedor c,Vendedor v";
            sql = sql + " ,Venta Ven,Auto au";
            sql = sql + " where c.CodVendedor = v.CodVendedor";
            sql = sql + " and c.CodVenta = ven.CodVenta";
            sql = sql + " and ven.CodAutoVendido = au.CodAuto";
            
            if (Impago == 1)
                sql = sql + " and c.FechaPago is null";
            if (Apellido != "")
                sql = sql + " and v.Nombre like " + "'%" + Apellido + "%'";
            if (Patente != "")
               sql = sql + " and au.Patente like " + "'%" + Patente + "%'" ;
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetComisionesxCodVendedor(Int32 CodVendedor)
        {
            string sql = "select c.Fecha,c.Importe,c.FechaPago,";
            sql = sql + "(select a.Descripcion from venta v,auto a where v.CodAutoVendido = a.CodAuto and v.CodVenta = c.CodVenta) as Descripcion";
            sql = sql + ",c.CodComision";
            sql = sql + ", (select a.Patente from venta v,auto a where a.CodAuto =v.CodAutoVendido and v.CodVenta = c.CodVenta) as Patente";
            sql = sql + " from ComisionVendedor c";
            sql = sql + " where CodVendedor=" + CodVendedor.ToString();
            sql = sql + " order by Fecha Desc ";
            return cDb.ExecuteDataTable(sql);
        }

        public void PagoComision(DateTime Fecha, Int32 CodComision)
        {
            string sql = "update ComisionVendedor set FechaPago =" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + " where CodComision=" + CodComision.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularPagoComision(Int32 CodComision)
        {
            string sql = "update ComisionVendedor set FechaPago =null";
            sql = sql + " where CodComision=" + CodComision.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetComisionesxCodigo(Int32 CodComision)
        {
            string sql = "select c.Fecha,c.Importe,c.FechaPago,";
            sql = sql + "(select a.Descripcion from venta v,auto a where v.CodAutoVendido = a.CodAuto and v.CodVenta = c.CodVenta) as Descripcion";
            sql = sql + ",c.CodComision";
            sql = sql + ", (select a.Patente from venta v,auto a where a.CodAuto =v.CodAutoVendido and v.CodVenta = c.CodVenta) as Patente";
            sql = sql + ",(select ven.Nombre from Vendedor ven where Ven.CodVendedor = c.CodVendedor) as Nombre";
            sql = sql + ",(select ven.Apellido from Vendedor ven where Ven.CodVendedor = c.CodVendedor) as Apellido";
            sql = sql + " from ComisionVendedor c";
            sql = sql + " where CodComision=" + CodComision.ToString();
            sql = sql + " order by c.Fecha Desc ";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
