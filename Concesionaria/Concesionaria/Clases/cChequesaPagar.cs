using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public  class cChequesaPagar
    {
        public double GetTotalChequesaPagar()
        {
            double Total =0;
            string sql = "select sum(Saldo) as Importe";
            sql = sql + " from ChequesPagar";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Total = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            }
            return Total;
        }

        public DataTable GetChequesPagar(DateTime FechaDesde, DateTime FechaHasta, int Impago,string Patente)
        {
            string sql = "select c.CodCheque, c.NroCheque,";
            sql = sql + "(select cli.Apellido from Cliente cli where cli.CodCliente =c.CodCliente) as Apellido";
            sql = sql + ",c.Fecha,c.Importe,c.Saldo, c.FechaPago,";
            sql = sql + "(select a.Patente from auto a where a.CodAuto = c.CodAuto) as Patente";
            sql = sql + ",(select a.Descripcion from auto a where a.CodAuto = c.CodAuto) as Descripcion";
            sql = sql + ",FechaVencimiento";
            sql = sql + " from ChequesPagar c, auto au";
            sql = sql + " where c.CodAuto = au.CodAuto";
            sql = sql + " and c.Fecha>=" + "'" + FechaDesde.ToShortDateString () + "'" ;
            sql = sql + " and c.Fecha<=" + "'" + FechaHasta.ToShortDateString () + "'";
            if (Impago ==1)
                sql = sql + " and FechaPago is null ";
            if (Patente != "")
                sql = sql + " and au.Patente like " + "'%" + Patente + "%'";
            sql = sql + " order by CodCheque desc";
                            
            return cDb.ExecuteDataTable (sql);
        }

        public DataTable GetChequesPagarxCodigo(Int32 CodCheque)
        {
            string sql = "select c.*,cli.Apellido,cli.Nombre";
            sql = sql + ",(select a.Patente from auto a where a.CodAuto=c.CodAuto) as Patente";
            sql = sql + " from chequespagar c,cliente cli ";
            sql = sql + " where c.CodCliente = cli.CodCliente";
            sql = sql + " and CodCheque=" + CodCheque.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void PagarCheque(Int32 CodCheque, DateTime Fecha)
        {
            string sql = "Update ChequesPagar ";
            sql = sql + " set FechaPago=" + "'" + Fecha.ToShortDateString () + "'" ;
            sql = sql + " where CodCheque =" + CodCheque.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularPagarCheque(Int32 CodCheque)
        {
            string sql = "Update ChequesPagar ";
            sql = sql + " set FechaPago=null";
            sql = sql + " where CodCheque =" + CodCheque.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetChequesxCodCompra(Int32 CodCompra)
        {
            string sql = "select c.NroCheque,c.Fecha, c.Importe,";
            sql = sql + "c.FechaPago,b.Nombre as Banco";
            sql = sql + " From ChequesPagar c,Banco b";
            sql = sql + " where c.CodBanco = b.CodBanco";
            sql = sql + " and c.CodCompra=" + CodCompra.ToString ();
            return cDb.ExecuteDataTable(sql);
        }
    }
}
