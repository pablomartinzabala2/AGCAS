using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cChequeCobrar
    {
        public void Insertar(DateTime Fecha,DateTime Vencimiento,
            Double Importe,Int32? CodBanco,string Apellido,
            string Nombre,string Patente,string Telefono,string NumeroCheque)
        {
            string sql = "insert into chequecobrar(";
            sql = sql + "Fecha,Vencimiento,Importe,CodBanco,";
            sql = sql + "Apellido,Nombre,Patente,Telefono,NumeroCheque)";
            sql = sql + " Values(" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + "'" + Vencimiento.ToShortDateString() + "'";
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + CodBanco.ToString();
            sql = sql + "," + "'" + Apellido + "'";
            sql = sql + "," + "'" + Nombre + "'";
            sql = sql + "," + "'" + Patente + "'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + NumeroCheque  + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);

        }

        public Double GetTotalChequesaCobrar()
        {
            Double Importe = 0;
            string sql = "select sum(Importe) as Importe";
            sql = sql + " from chequecobrar ";
            sql = sql + " where FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public DataTable GetChequesxFecha(DateTime FechaDesde, DateTime FechaHasta, int SoloImpago)
        {
            string sql = "select c.CodCheque,c.Fecha,c.NumeroCheque,";
            sql = sql + "(select b.Nombre from Banco b where b.CodBanco = c.CodBanco) as Banco";
            sql = sql + ",c.Importe,c.FechaPago";
            sql = sql + ",C.Apellido,c.Nombre,c.Telefono,c.EntregadoA";
            sql = sql + " from chequecobrar c";
            sql = sql + " where c.Fecha>=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and c.Fecha<=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (SoloImpago == 1)
                sql = sql + " and c.FechaPago is null";
            return cDb.ExecuteDataTable(sql);
        }

        public void CobroCheque(DateTime FechaPago,Int32 CodCheque)
        {
            string sql = "update chequecobrar";
            sql = sql + " set FechaPago=" + "'" + FechaPago.ToShortDateString () + "'";
            sql = sql + " where CodCheque=" + CodCheque.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularCheque(Int32 CodCheque)
        {
            string sql = "update chequecobrar";
            sql = sql + " set FechaPago=null";
            sql = sql + " where CodCheque=" + CodCheque.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetChequexCodigo(Int32 CodCheque)
        {
            string sql = "select * from chequecobrar ";
            sql = sql + " where CodCheque=" + CodCheque.ToString ();
            return cDb.ExecuteDataTable (sql);
        }

        public void GrabarEntregado(Int32 CodChque, string Nombre)
        {
            string sql = "Update chequecobrar ";
            sql = sql + " set EntregadoA=" + "'" + Nombre + "'";
            sql = sql + " where CodCheque=" + CodChque.ToString();
            cDb.ExecutarNonQuery(sql);

        }
    }
}
