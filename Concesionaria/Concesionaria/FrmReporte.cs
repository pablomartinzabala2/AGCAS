using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Concesionaria
{
    public partial class FrmReporte : Form
    {
        public FrmReporte()
        {
            InitializeComponent();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'CONCESIONARIADataSet.Reporte' table. You can move, or remove it, as needed.
            
            this.ReporteTableAdapter.Fill(this.CONCESIONARIADataSet.Reporte);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void GrabarDatos()
        {
            string NombreCliente = "JUANA PEREZ";
            string DniCliente = "27111555";
            string DireccionCliente = "Avenida General Paz 239 Ciudad de Córdoba"; 
            string Texto1 = "Entre la firma <n>CASTAÑO AUTOMOTORES</n> con domicilio";
            Texto1 = Texto1 + " en Av Colon 5735 de la ciudad de Córdoba";
            Texto1 = Texto1 + " adelante VENDEDOR Y La Sr/a NombreCliente";
            Texto1 = Texto1 + " DNI DniCliente Con domicilio en CALLE DireccionCliente";
            Texto1 = Texto1 + " en adelante COMPRADOR convienen en celebrar el siguiente boleto";
            Texto1 = Texto1 + " de compra venta, sujeto a las siguientes Cláusulas ";
            Texto1 = Texto1.Replace("NombreCliente", NombreCliente);
            Texto1 = Texto1.Replace("DniCliente", DniCliente);
            Texto1 = Texto1.Replace("DireccionCliente", DireccionCliente);

            string sql = "update Reporte set PARTE1 =" + "'" + Texto1 + "'" ;
            Clases.cDb.ExecutarNonQuery(sql);

        }
    }
}
