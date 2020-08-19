using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cTarjeta
    {
        public DataTable GetTarjetaxCodVenta(Int32 CodVenta)
        {
            string sql = "select vt.CodTarjeta,t.Nombre,vt.Importe";
            sql = sql + " from ventaxtarjeta vt,Tarjeta t";
            sql = sql + " where vt.CodTarjeta=t.CodTarjeta";
            sql = sql + " and vt.CodVenta=" + CodVenta.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public Double GetSaldoTarjeta()
        {
            Double Importe = 0;
            string sql = "select isnull(sum(Saldo),0) as Importe";
            sql = sql + " from ventaxtarjeta ";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public DataTable GetVentaxTarjeta(DateTime FechaDesde,DateTime FechaHasta,
            Int32? CodTarjeta,string Patente)
        {
            string sql = "select vt.CodVenta,vt.CodTarjeta";
            sql = sql + ",au.Patente,au.Descripcion,vt.Importe,t.Nombre,vt.Saldo,vt.FechaCobro";
            sql = sql + " from venta v,ventaxtarjeta vt,auto au,tarjeta t";
            sql = sql + " where v.CodVenta= vt.CodVenta";
            sql = sql + " and v.CodAutoVendido= au.CodAuto";
            sql = sql + " and vt.CodTarjeta=t.CodTarjeta";
            sql = sql + " and v.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and v.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (CodTarjeta != null)
            {
                sql = sql + " and vt.CodTarjeta =" + CodTarjeta.ToString();
            }
            if (Patente != "")
            {
                sql = sql + " and Patente like " + "'%" + Patente + "%'";
            }
            return cDb.ExecuteDataTable(sql);
        }

        public void RegistrarCobro(Int32 CodVenta, Int32 CodTarjeta,DateTime Fecha)
        {
            string sql = "Update ventaxtarjeta";
            sql = sql + " set Saldo =0";
            sql = sql + ", FechaCobro=" + "'" + Fecha.ToShortDateString() + "'"; 
            sql = sql + " where CodVenta=" + CodVenta.ToString ();
            sql = sql + " and CodTarjeta =" + CodTarjeta.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularCobro(Int32 CodVenta, Int32 CodTarjeta)
        {
            string sql = "Update ventaxtarjeta";
            sql = sql + " set Saldo =Importe";
            sql = sql + ", FechaCobro=null";
            sql = sql + " where CodVenta=" + CodVenta.ToString();
            sql = sql + " and CodTarjeta =" + CodTarjeta.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        
    }
}
