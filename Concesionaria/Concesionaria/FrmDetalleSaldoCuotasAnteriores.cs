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
    public partial class FrmDetalleSaldoCuotasAnteriores : Form
    {
        public FrmDetalleSaldoCuotasAnteriores()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void FrmDetalleSaldoCuotasAnteriores_Load(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            Clases.cSaldoCuotasAnteriores saldo = new Clases.cSaldoCuotasAnteriores();
            DataTable trdo = saldo.GetSaldoCobranza (Convert.ToInt32 (Principal.CodigoPrincipalAbm)); 
            trdo = fun.TablaaMiles (trdo,"Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
        }
    }
}
