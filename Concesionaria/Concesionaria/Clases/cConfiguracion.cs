using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concesionaria.Clases
{
    public static class cConfiguracion
    {
        public static System.Drawing.Color ColorTextBox()
        {
            return System.Drawing.SystemColors.Control;
        }

        public static void BorrarTablas()
        {
            string sql ="delete from resumencuentas";
            cDb.ExecutarNonQuery (sql);
             sql ="delete from movimiento";
            cDb.ExecutarNonQuery (sql);
            sql = "delete from auto";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from stockauto";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from cuotas";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from cheque";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from cobranza";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from Gasto";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from Costo";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from GastosRecepcionxAuto";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from Venta";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from Prestamo";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from PagosIntereses";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from comisionvendedor";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from gastospagar";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from ChequesPagar";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from cuotasanteriores";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from CobranzaGeneral";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from Prenda";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from EfectivosaPagar";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from DiferenciaTransferencia";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from GastosPagar";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from PreVenta";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from PunitorioCuotas";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from gastosnegocio";
            cDb.ExecutarNonQuery(sql);
            //gastosnegocio
            sql = "delete from PunitorioCuotasAnteriores";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from PunitorioCobranza";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from PunitorioCuotas";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from ventaxtarjeta";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from chequecobrar";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from prestamo ";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from DetallePrestamo ";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from PagosIntereses ";
            cDb.ExecutarNonQuery(sql);

            sql = "delete from PrestamoCobrar ";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from DetallePrestamoCobrar ";
            cDb.ExecutarNonQuery(sql);
            sql = "delete from CobroIntereses ";
            cDb.ExecutarNonQuery(sql);
            //chequecobrar

        }
    }
}
