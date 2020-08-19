using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Concesionaria
{
    public partial class FrmCopia : Form
    {
        public FrmCopia()
        {
            InitializeComponent();
        }

        private void btnCopia_Click(object sender, EventArgs e)
        {
            string cad = Clases.cConexion.Cadenacon();
            try
            {
                string ConnectionString = cad;
                SqlConnection cnn = new SqlConnection(ConnectionString);


                SqlCommand cmd = new SqlCommand("backupdb", cnn);
                cmd.CommandType = CommandType.StoredProcedure;




                cnn.Open();


                cmd.ExecuteNonQuery();


                MessageBox.Show("El backup fue realizado exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
    }
}
