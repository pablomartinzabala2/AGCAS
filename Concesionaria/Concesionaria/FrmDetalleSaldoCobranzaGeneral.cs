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
    public partial class FrmDetalleSaldoCobranzaGeneral : Form
    {
        public FrmDetalleSaldoCobranzaGeneral()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmDetalleSaldoCobranzaGeneral_Load(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Int32 CodCobranza = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            Clases.cSaldoCobranzaGeneral saldo = new Clases.cSaldoCobranzaGeneral();
            DataTable trdo = saldo.GetSaldoCobranza(CodCobranza);
            trdo = fun.TablaaMiles(trdo, "Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[2].Width = 100;
            Grilla.Columns[3].Width = 160;
        }
    }
}
