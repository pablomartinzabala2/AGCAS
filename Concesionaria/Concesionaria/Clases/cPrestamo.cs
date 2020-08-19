using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public  class cPrestamo
    {
        public void InsertarPrestamo(string Nombre, string Telefono, string Dirección, DateTime Fecha, double Importe, 
            double PorcentajeInteres,DateTime FechaVencimiento, Double ImporteaPagar)
        {
            string sql = "insert into Prestamo(Nombre,Direccion,Telefono";
            sql = sql + ",Fecha,Importe,PorcentajeInteres,FechaVencimiento,ImporteaPagar)";
            sql = sql + " values (" + "'" + Nombre + "'";
            sql = sql + "," + "'" + Dirección + "'";
            sql = sql + "," + "'" + Telefono  + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + PorcentajeInteres.ToString().Replace(",", ".");
            sql = sql + "," + "'" + FechaVencimiento.ToShortDateString() + "'";
            sql = sql + "," + ImporteaPagar.ToString().Replace(",", ".");
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetPrestamosxFecha(DateTime FechaDesde, DateTime FechaHasta,string Nombre, int SoloImpago)
        {
            string sql = "select CodPrestamo, Nombre,Direccion as Dirección,Telefono as Teléfono,FechaVencimiento,Fecha,Importe,ImporteaPagar,FechaPago";
            sql = sql + " from Prestamo ";
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Nombre != "")
                sql = sql + " and Nombre like " + "'" + "%" + Nombre +"%" + "'";
            if (SoloImpago == 1)
                sql = sql + " and FechaPago is null";
            sql = sql + " order by CodPrestamo desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPrestamoxCodigo(Int32 CodPrestamo)
        {
            string sql = "select * from Prestamo";
            sql = sql + " where CodPrestamo=" + CodPrestamo.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public double GetTotalDeudaPrestamo()
        {
            double Importe = 0;
            string sql = "select sum(Importe) as Importe";
            sql = sql + " from Prestamo where FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            }
            return Importe;
        }

        public void RegistrarDevolucion(Int32 CodPrestamo,DateTime Fecha)
        {
            string sql = "update Prestamo set FechaPago =" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + " where CodPrestamo =" + CodPrestamo.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public Int32 GetMaxPrestamo()
        {
            Int32 CodPrestamo = 0;
            string sql = "select max(CodPrestamo) as CodPrestamo from Prestamo";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                CodPrestamo = Convert.ToInt32(trdo.Rows[0]["CodPrestamo"].ToString()); 
            }
            return CodPrestamo;
        }

        public void ModificarPorcentajePrestamo(Int32 CodPrestamo, Double PorcentajeInteres,
            Double ImportePagar, DateTime FechaVencimiento,double Importe)
        {
            string sql = "Update Prestamo ";
            sql = sql + " set ImporteaPagar =" + ImportePagar.ToString ().Replace (",",".");
            sql = sql + ",PorcentajeInteres=" + PorcentajeInteres.ToString().Replace(",", ".");
            sql = sql + ",FechaVencimiento=" + "'" + FechaVencimiento.ToShortDateString() + "'";
            sql = sql + ",Importe =" + Importe.ToString().Replace(",", ".");
            sql = sql + " where CodPrestamo =" + CodPrestamo.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
