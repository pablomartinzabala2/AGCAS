using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cAgenda
    {
        public void GrabarAgenda(string Nombre, string Apellido, Int32? CodMarca,
            string Modelo, Double? Precio, string Descripcion, int CodOpcion, string Opcion, DateTime Fecha, string Patente, Int32? CodVendedor,string Telefono)
        {
            string sql = "insert into Agenda(";
            sql = sql + "Nombre,Apellido,CodMarca,Modelo,Precio,Descripcion,CodOpcion,Opcion,Fecha,Patente,CodVendedor,Telefono)";
            sql = sql + "Values(" + "'" + Nombre + "'";
            sql = sql + "," + "'" + Apellido + "'";
            if (CodMarca != null)
                sql = sql + "," + CodMarca.ToString();
            else
                sql = sql + ",null";
            sql = sql + "," + "'" + Modelo + "'";
            if (Precio != null)
                sql = sql + "," + Precio.ToString().Replace(",", ".");
            else
                sql = sql + ",null";
            sql = sql + "," + "'" + Descripcion + "'";
            sql = sql + "," + CodOpcion.ToString();
            sql = sql + "," + "'" + Opcion + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + "'" + Patente + "'";
            if (CodVendedor != null)
                sql = sql + "," + CodVendedor.ToString();
            else
                sql = sql + ",null";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetAgenda(DateTime FechaDesde, DateTime FechaHasta,int IncluyePrecio,
            Double PrecioDesde,Double PrecioHasta,Int32? CodOpcion, string Patente,string Descripcion,string Cliente,string Telefono, Int32? CodMarca,string Modelo)
        {
            string sql = "";
            sql = "select a.CodAgenda,a.Apellido";
            sql = sql + ",(Select (v.Apellido + ' ' + v.Nombre) as Vendedor From Vendedor v where v.CodVendedor = a.CodVendedor) as Vendedor";
            sql = sql + ",(select m.Nombre from Marca m where m.CodMarca=a.CodMarca) as Marca";
            sql = sql + ",a.Modelo,a.Telefono, a.Precio,a.CodOpcion,a.Opcion";
            sql = sql + " from Agenda a";
            sql = sql + " where a.Fecha<=" + "'" + FechaHasta.ToShortDateString () + "'";
            sql = sql + " and a.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            if (IncluyePrecio == 1)
            {
                   sql = sql + " and a.Precio >=" + PrecioDesde.ToString().Replace(",", ".");
                   sql = sql + " and a.Precio <=" + PrecioHasta.ToString().Replace(",", ".");
            }
            if (CodOpcion != null)
                sql = sql + " and a.CodOpcion=" + CodOpcion.ToString();
            if (Patente != "")
                sql = sql + " and a.Patente like " + "'%" + Patente + "%'";
            if (Descripcion  != "")
                sql = sql + " and a.Descripcion like " + "'%" + Descripcion + "%'";
            if (Cliente  != "")
                sql = sql + " and a.Apellido like " + "'%" + Cliente + "%'";
            if (Telefono  != "")
                sql = sql + " and a.Telefono like " + "'%" + Telefono + "%'";
            if (CodMarca != null)
                sql = sql + " and a.CodMarca=" + CodMarca.ToString();
            if (Modelo  != "")
                sql = sql + " and a.Modelo like " + "'%" + Modelo + "%'";
            sql = sql + " order by CodAgenda Desc";
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarAgenda(Int32 CodAgenda)
        {
            string sql = "delete from agenda where CodAgenda=" + CodAgenda.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetAgendaxCodigo(Int32 CodAgenda)
        {
            string sql = "select * from Agenda ";
            sql = sql + " where CodAgenda=" + CodAgenda.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void ModificarAgenda(Int32 CodAgenda, string Nombre, string Apellido, Int32? CodMarca,
            string Modelo, Double? Precio, string Descripcion, int CodOpcion, string Opcion, DateTime Fecha, string Patente,Int32? CodVendedor,string Telefono)
        {
            string sql = "Update Agenda set";
            sql = sql + " Nombre=" + "'" + Nombre + "'";
            sql = sql + ",Apellido=" + "'" + Apellido + "'";
            if (CodMarca != null)
                sql = sql + ",CodMarca=" + CodMarca.ToString();
            else
                sql = sql + ",CodMarca=null";
            sql = sql + ",Modelo=" + "'" + Modelo + "'";
            sql = sql + ",Precio =" + Precio.ToString().Replace(",", ".");
            sql = sql + ",Descripcion=" + "'" + Descripcion + "'";
            sql = sql + ",CodOpcion=" + CodOpcion.ToString();
            sql = sql + ",Opcion=" + "'" + Opcion + "'";
            sql = sql + ",Patente=" + "'" + Patente + "'";
            if (CodVendedor != null)
                sql = sql + ",CodVendedor=" + CodVendedor.ToString();
            else
                sql = sql + ",CodVendedor = null";
            sql = sql + ",Telefono=" + "'" + Telefono + "'";
            sql = sql + " where CodAgenda=" + CodAgenda.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
