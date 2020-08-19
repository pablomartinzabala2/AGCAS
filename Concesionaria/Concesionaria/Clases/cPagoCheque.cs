using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public class cPagoCheque
    {
        public void InsertarPagoCheque(Int32 CodCheque,double Importe,DateTime Fecha)
        {
            string sql = "insert into PagosCheques(CodCheque,Importe,Fecha)";
            sql = sql + "values(" + CodCheque.ToString () ;
            sql = sql + "," + Importe.ToString().Replace(",", ".");
            sql = sql + "," + "'" + Fecha.ToShortDateString ()  + "'";
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
            //actualizo el saldo  del cheque
            sql = "update ChequesPagar set Saldo = Saldo - " + Importe.ToString ().Replace (",",".");
            sql = sql + ", FechaPago =" + "'" + Fecha.ToShortDateString () + "'";
            sql = sql + " where CodCheque =" + CodCheque.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetPagosCheques(Int32 CodCheque)
        {
            string sql = "select * from PagosCheques";
            sql = sql + " where CodCheque=" + CodCheque.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void AnularPagoCheque(Int32 CodPago, Int32 CodCheque, double Importe)
        {
            string sql = "delete from pagoscheques where CodPago=" + CodPago.ToString();
            cDb.ExecutarNonQuery(sql);
            sql = " update chequespagar set Saldo = Saldo +" + Importe.ToString ().Replace (",",".");
            sql = sql + " where CodCheque =" + CodCheque.ToString ();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
