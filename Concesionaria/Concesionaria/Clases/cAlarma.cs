using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cAlarma
    {
        public void GrabarAlarma(string Descripcion, DateTime Fecha,string Cliente,string Patente)
        {
            string sql = "Insert into Alarma(Nombre,Fecha,Cliente,Patente)";
            sql = sql + "Values(" + "'" + Descripcion + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + "," + "'" + Cliente + "'";
            sql = sql + "," + "'" + Patente + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery (sql);
        }

        public DataTable GetAlarmasxFecha(DateTime Fecha)
        {
            string sql = "select * from Alarma";
            sql = sql + " where Fecha =" + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + " order by CodAlarma desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetAlertasxRangoFecha(DateTime FechaDesde, DateTime FechaHasta,string Texto,string Patente,string Cliente)
        {
            string sql = "select * from Alarma";
            sql = sql + " where Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (Texto != "")
                sql = sql + " and Nombre like " + "'%" + Texto + "%'";
            if (Patente != "")
                sql = sql + " and Patente like " + "'%" + Patente + "%'";
            if (Cliente != "")
                sql = sql + " and Cliente like " + "'%" + Cliente  + "%'";
            sql = sql + " order by CodAlarma desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetAlarmaxCodigo(Int32 CodAlarma)
        {
            string sql = "select * from Alarma";
            sql = sql + " where CodAlarma =" + CodAlarma.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void ModificarAlarma(Int32 CodAlarma, string Descripcion, DateTime Fecha, string Cliente, string Patente)
        {
            string sql = " update Alarma set Nombre=" + "'" + Descripcion + "'";
            sql = sql + ",Fecha=" + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + ",Cliente=" + "'" + Cliente + "'";
            sql = sql + ",Patente=" + "'" + Patente + "'";
            sql = sql + " where CodAlarma=" + CodAlarma.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public void EliminarAlarma(Int32 CodAlarma)
        {
            string sql = "delete from Alarma where CodAlarma=" + CodAlarma.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
