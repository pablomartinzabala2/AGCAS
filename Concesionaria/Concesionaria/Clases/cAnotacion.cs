using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cAnotacion
    {
        public void Insertar(DateTime Fecha,string Descripcion,double? ImporteIngreso,double? ImporteEgreso)
        {
            string sql = "Insert into Anotacion";
            sql = sql + "(Fecha,Descripcion,ImporteIngreso,ImporteEgreso)";
            sql = sql + "Values(" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + "'" + Descripcion  +"'";
            if (ImporteIngreso != null)
            {
                sql = sql + "," + ImporteIngreso.ToString().Replace(",", ".");
                sql = sql + ",null)";
            }
            else{
                sql = sql + ",null";
                sql = sql + "," + ImporteEgreso.ToString().Replace(",", ".");
                sql = sql + ")";
            }
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetAnotacionesxFecha(DateTime FechaDesde, DateTime FechaHasta,string Descripcion)
        {
            string sql = "select * from Anotacion";
            sql = sql + " where Fecha>=" + "'" + FechaDesde  + "'" ;
            sql = sql + " and Fecha <="  +"'" + FechaHasta + "'";
            if (Descripcion !="")
                sql = sql + " and Descripcion like" + "'%" + Descripcion  + "%'" ;
            sql = sql + " order by Fecha Desc";
            return cDb.ExecuteDataTable(sql);
        }

        public void Borrar(Int32 CodAnotacion)
        {
            string sql = "delete from Anotacion where CodAnotacion =" + CodAnotacion.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
