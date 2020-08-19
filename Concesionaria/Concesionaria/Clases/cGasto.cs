using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public  class cGasto
    {
        public string GetGastoxCodigo(Int32 CodGasto)
        {
            string sql = "select Nombre from CategoriaGasto ";
            sql = sql + " where CodCategoriaGasto =" + CodGasto.ToString();
            string Nombre = "";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                Nombre = trdo.Rows[0]["Nombre"].ToString();
            return Nombre;
        }

        public void BorrarGastoxCodStock(Int32 CodStock)
        {
            string sql = "";
            sql = "delete from Gasto where CodStock =" + CodStock.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public void InsertarGasto(Int32 CodStock,Int32 CodCategoriaGasto, double Importe,DateTime Fecha)
        {
            string sql = "";
            sql = "Insert into Gasto(CodStock,CodCategoriaGasto,Importe,Fecha)";
            sql = sql + "values (" + CodStock.ToString();
            sql = sql + "," + CodCategoriaGasto.ToString();
            sql = sql + "," + Importe.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetGastosxCodStock(Int32 CodStock)
        {
            string sql = " select g.CodCategoriaGasto,cg.Nombre,g.Importe,g.Fecha";
            sql = sql + " from Gasto g,CategoriaGasto cg";
            sql = sql + " where g.CodCategoriaGasto = cg.CodCategoriaGasto ";
            sql = sql + " and g.CodStock =" + CodStock.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public string GetNombreGastoTransferenciaxCodigo(Int32 Codigo)
        {
            string sql = "select descripcion from CategoriaGastoTransferencia ";
            string Nombre = "";
            sql = sql + " where Codigo =" + Codigo.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                Nombre = trdo.Rows[0]["Descripcion"].ToString();  
            }
            return Nombre;
        }

        public string GetNombreGastoRecepcionxCodigo(Int32 Codigo)
        {
            string sql = "select descripcion from CategoriaGastoRecepcion ";
            string Nombre = "";
            sql = sql + " where Codigo =" + Codigo.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                Nombre = trdo.Rows[0]["Descripcion"].ToString();
            }
            return Nombre;
        }

        public void GrabarGastosRecepcionxCodStock(Int32 CodStock,Int32 CodGasto,Double Importe,DateTime Fecha)
        {
            string sql = "Insert into GastosRecepcionxAuto";
            sql = sql + "(CodStock,CodGasto,Importe,Fecha)";
            sql = sql + "Values(" + CodStock.ToString();
            sql = sql + "," + CodGasto.ToString();
            sql = sql + "," + Importe.ToString();
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public void BorrarGastoxCategoria(Int32 CodStock, Int32 CodCategoriaGasto)
        {
            string sql = "delete from Gasto where CodStock =" + CodStock.ToString();
            sql = sql + " and CodCategoriaGasto =" + CodCategoriaGasto.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetGastosRecepcionxCodStock(Int32 CodStock)
        {
            string sql = " select g.CodGasto,cg.Descripcion,g.Fecha,g.Importe";
            sql = sql + " from GastosRecepcionxAuto g,CategoriaGastoRecepcion cg";
            sql = sql + " where g.CodGasto = cg.Codigo ";
            sql = sql + " and g.CodStock =" + CodStock.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarGastosRecepcionxCodStock(Int32 CodStock)
        {
            string sql = "delete from GastosRecepcionxAuto  where CodStock=" + CodStock.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetGastosRecepcionxCodStock2(Int32 CodStock)
        {
            string sql ="select g.CodGasto as Codigo,cr.Descripcion,";
            sql = sql + "'Recepcion' as Tìpo,g.Importe,g.Fecha";
            sql = sql + " from GastosRecepcionxAuto g,CategoriaGastoRecepcion cr";
            sql = sql + " where g.CodGasto = cr.Codigo";
            sql = sql + " and g.CodStock =" + CodStock.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorarGastoRecepcion2(Int32 CodStock, Int32 CodGasto)
        {
            string sql = "delete from GastosRecepcionxAuto ";
            sql = sql + " where CodStock = " + CodStock.ToString();
            sql = sql + " and CodGasto =" + CodGasto.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
