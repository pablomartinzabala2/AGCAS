using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace Concesionaria
{
    public partial class FrmDetalleSaldosEfectivosaPagar : Form
    {
        public FrmDetalleSaldosEfectivosaPagar()
        {
            InitializeComponent();
        }

        private void FrmDetalleSaldosEfectivosaPagar_Load(object sender, EventArgs e)
        {
            Int32 CodRegistro = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            Clases.cSaldoEfectivoPagar saldo = new Clases.cSaldoEfectivoPagar();
            DataTable trdo = saldo.GetSaldos(CodRegistro);
            Clases.cFunciones fun = new Clases.cFunciones();
            trdo = fun.TablaaMiles(trdo, "Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false; 
        }
    }
}
