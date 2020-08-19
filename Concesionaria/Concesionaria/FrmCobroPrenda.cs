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
    public partial class FrmCobroPrenda : Form
    {
        public FrmCobroPrenda()
        {
            InitializeComponent();
           // txtFecha.Text = DateTime.Now.ToShortDateString(); 
        }

        private void txtPatente_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox)
                            ((TextBox)g).CharacterCasing = CharacterCasing.Upper;
                    }
                    //Empleamos un casteo

                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //string Patente = txtPatente.Text;
           // GetPrendaxCod(Patente);
        }

        public void GetPrendaxCod(Int32  CodPrenda)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            Clases.cPrenda prenda = new Clases.cPrenda();
            DataTable trdo = prenda.GetPrendaxCodigo(CodPrenda);
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (trdo.Rows.Count > 0)
            {
                txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtCodCliente.Text = trdo.Rows[0]["CodCliente"].ToString();
                if (trdo.Rows[0]["CodPrenda"].ToString() != "")
                {
                    CargarPrenda(Convert.ToInt32(trdo.Rows[0]["CodPrenda"].ToString()));
                }
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString ();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString();
                if (txtImporte.Text !="")
                {
                    txtImporte.Text = fun.ParteEntera (txtImporte.Text);
                    txtImporte.Text = fun.FormatoEnteroMiles (txtImporte.Text);
                }
                txtCodVenta.Text = trdo.Rows[0]["CodVenta"].ToString();
                txtDiferencia.Text = trdo.Rows[0]["Diferencia"].ToString();
                if (trdo.Rows[0]["ImportePagado"].ToString() != "")
                {
                    txtImporteaPagar.Text = trdo.Rows[0]["ImportePagado"].ToString();
                    txtImporteaPagar.Text = fun.ParteEntera(txtImporteaPagar.Text);
                    txtImporteaPagar.Text = fun.FormatoEnteroMiles(txtImporteaPagar.Text);
                }
                if (txtDiferencia.Text != "")
                {
                    txtDiferencia.Text = fun.ParteEntera(txtDiferencia.Text);
                    txtDiferencia.Text = fun.FormatoEnteroMiles(txtDiferencia.Text);
                }
                if (trdo.Rows[0]["FechaPago"].ToString() != "")
                {
                    DateTime fec = Convert.ToDateTime(trdo.Rows[0]["FechaPago"].ToString());
                    txtFecha.Text = fec.ToShortDateString();
                    btnGrabar.Enabled = false;
                    btnAnular.Enabled = true;
                    txtImporteaPagar.Enabled = false;
                }
                else
                {
                    btnGrabar.Enabled = true;
                    btnAnular.Enabled = false;
                    txtImporteaPagar.Enabled = true;
                }
            }
            else
            {
                txtDescripcion.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                Grilla.DataSource = null;
            }
        }

        private void CargarPrenda(Int32 CodPrenda)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cPrenda prenda = new Clases.cPrenda();
            DataTable trdo = prenda.GetPrendaxCodigo(CodPrenda);
            Grilla.DataSource = fun.TablaaMiles(trdo, "Importe");
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[2].Width = 120;
            Grilla.Columns[3].Visible = false;
            Grilla.Columns[4].Visible = false;
            Grilla.Columns[5].Visible = false;
            Grilla.Columns[6].Width  = 108;
            Grilla.Columns[7].Visible = false;
            Grilla.Columns[8].Width  = 480;
            Grilla.Columns[8].HeaderText = "Entidad";
            Grilla.Columns[6].HeaderText = "Fecha Pago";
            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Grilla.Rows.Count < 2)
            {
                MessageBox.Show("Debe Seleccionar una prenda para continuar ", Clases.cMensaje.Mensaje()); 
                return;
            }

            if (txtFecha.Text =="")
            {
                MessageBox.Show("Debe ingresar una fecha para continuar.", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta.", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtTotalCheque.Text == "")
            {
                MessageBox.Show("El importe total del cheque debe ser superior a cero.", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtTotalCheque.Text == "0")
            {
                MessageBox.Show("El importe total del cheque debe ser superior a cero.", Clases.cMensaje.Mensaje());
                return;
            }

            double Importe = fun.ToDouble(txtImporte.Text);
            double ImportePagar = fun.ToDouble(txtTotalCheque.Text);
            double dif =ImportePagar- Importe;
            if (dif != 0)
            {
                var result = MessageBox.Show("El importe a pagar es distinto al importe,Desea continuar?", "Información",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            
            
            
         string Descripcion = "COBRO DE PRENDA:  PATENTE " + txtPatente.Text;
            Descripcion = Descripcion + ", CLIENTE " + txtNombre.Text ;
            string CodPrenda = Principal.CodigoPrincipalAbm; 
         //Double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString());
         Int32 CodVenta = Convert.ToInt32(txtCodVenta.Text);
         DateTime Fecha = Convert.ToDateTime(txtFecha.Text);   
            Clases.cPrenda prenda = new Clases.cPrenda();
            prenda.RegistrarPagoPrenda(Convert.ToInt32(CodPrenda), Convert.ToDateTime(txtFecha.Text), ImportePagar, dif);
         Clases.cMovimiento mov = new Clases.cMovimiento();
        //mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, Importe, 0, ((-1) * Importe), 0, 0, Fecha, Descripcion);
         
            for (int i = 0; i < GrillaCheques.Rows.Count - 1; i++)
         {
             string sImporteCheque = GrillaCheques.Rows[i].Cells[1].Value.ToString();
             string sqlCheque = "insert into Cheque(CodVenta,NroCheque,Importe,Fecha,FechaVencimiento,CodCliente,CodBanco,CodPrenda)";
             sqlCheque = sqlCheque + "values (" + CodVenta.ToString();
             sqlCheque = sqlCheque + ","+ "'" + GrillaCheques.Rows[i].Cells[0].Value.ToString() + "'";
             sqlCheque = sqlCheque + "," + fun.ToDouble(sImporteCheque);
             sqlCheque = sqlCheque + "," + "'" + txtFecha.Text + "'";
             sqlCheque = sqlCheque + "," + "'" + GrillaCheques.Rows[i].Cells[2].Value.ToString() + "'";
             sqlCheque = sqlCheque + "," + txtCodCliente.Text;
             sqlCheque = sqlCheque + "," + CmbBanco.SelectedValue;
             sqlCheque = sqlCheque + "," + CodPrenda.ToString();
             sqlCheque = sqlCheque + ")";
             Clases.cDb.ExecutarNonQuery(sqlCheque);
         }
         if (dif > 0)
         {
             string TEXTO = "GANANCIA POR EXEDENTE DE PRENDA, PATENTE " + txtPatente.Text;
             //mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, dif , 0, 0, 0, 0, Fecha, TEXTO);
         }
         if (dif < 0)
         {
             string TEXTO = "PERDIDA POR FALTANTE DE PRENDA, PATENTE " + txtPatente.Text;
            // mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, dif, 0, 0, 0, 0, Fecha, TEXTO);
         }
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            GetPrendaxCod(Convert.ToInt32(CodPrenda));
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            /*
            if (Grilla.Rows.Count < 2)
            {
                MessageBox.Show("Debe Seleccionar una prenda para continuar ", Clases.cMensaje.Mensaje());
                return;
            }
            
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una fila para continuar.", Clases.cMensaje.Mensaje());
                return;
            }

            if (Grilla.CurrentRow.Cells[6].Value.ToString() == "")
            {
                MessageBox.Show("La prenda no se puede anular debido a que no ha sido cobrada.", Clases.cMensaje.Mensaje());
                return;
            }
            */
            string Descripcion = "ANULACION DE COBRO DE PRENDA:  Patente " + txtPatente.Text;
            Descripcion = Descripcion + ", Cliente " + txtNombre.Text;
            string CodPrenda = Principal.CodigoPrincipalAbm; 
            Double Importe = fun.ToDouble(txtImporte.Text);
            Int32 CodVenta = Convert.ToInt32(txtCodVenta.Text);
            Clases.cPrenda prenda = new Clases.cPrenda();
            prenda.AnularPagoPrenda(Convert.ToInt32(CodPrenda));
            Clases.cMovimiento mov = new Clases.cMovimiento();
            if (txtDiferencia.Text != "0")
            {
                double Diferencia = fun.ToDouble(txtDiferencia.Text);
                string TEXTO = "ANULACION DIFERENCIA PRENDA, PATENTE " + txtPatente.Text;
               // mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, (-1) * Diferencia, 0, 0, 0, 0, DateTime.Now, TEXTO); 
            }
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Clases.cCheque cheque = new Clases.cCheque();
            cheque.BorrarChequexCodPrenda(Convert.ToInt32 (CodPrenda));
            Importe = fun.ToDouble(txtTotalCheque.Text);
             mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, (-1) * Importe, 0, 0, 0, 0, Fecha, Descripcion); 
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            GetPrendaxCod(Convert.ToInt32(CodPrenda));
        }

        private void FrmCobroPrenda_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                fun.LlenarCombo(CmbBanco, "Banco", "Nombre", "CodBanco");
                Int32 CodPrenda =Convert.ToInt32 (Principal.CodigoPrincipalAbm);
                GetPrendaxCod(CodPrenda);
                BuscarCheques(CodPrenda);
            }
        }

        private void txtImporteaPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
        }

        private void txtImporteaPagar_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
             fun.SoloEnteroConPunto(sender, e);
        }

        private void txtImporteaPagar_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtImporteaPagar.Text != "")
                txtImporteaPagar.Text = fun.FormatoEnteroMiles(txtImporteaPagar.Text);
        }

        private void BtnAgregarCheque_Click(object sender, EventArgs e)
        {
            if (txtCheque.Text == "")
            {
                MessageBox.Show("Debe ingresr un número de cheque", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtImporteCheque.Text == "")
            {
                MessageBox.Show("Debe ingresr un importe de cheque", Clases.cMensaje.Mensaje());
                return;
            }

            if (CmbBanco.SelectedIndex < 1)
            {
                MessageBox.Show("Debe seleccionar un banco para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaVencimiento.Text) == false)
            {
                MessageBox.Show("Debe ingresr una fecha de vencimiento para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            DataTable tbCheques = new DataTable();
            tbCheques.Columns.Add("NroCheque");
            tbCheques.Columns.Add("Importe");
            tbCheques.Columns.Add("FechaVencimiento");
            tbCheques.Columns.Add("CodBanco");
            tbCheques.Columns.Add("Banco");
            int i = 0;
            for (i = 0; i < GrillaCheques.Rows.Count - 1; i++)
            {
                string Cheque = GrillaCheques.Rows[i].Cells[0].Value.ToString();
                string Importe = GrillaCheques.Rows[i].Cells[1].Value.ToString();
                string FechaVencimiento = GrillaCheques.Rows[i].Cells[2].Value.ToString();
                string CodBanco = GrillaCheques.Rows[i].Cells[3].Value.ToString();
                string sBanco = GrillaCheques.Rows[i].Cells[4].Value.ToString();

                DataRow r = tbCheques.NewRow();
                r[0] = Cheque;
                r[1] = Importe;
                r[2] = FechaVencimiento;
                r[3] = CodBanco;
                r[4] = sBanco;
                tbCheques.Rows.Add(r);
            }
            Clases.cBanco objBanco = new Clases.cBanco();
            string banco = objBanco.GetBancoxCodigo(Convert.ToInt32(CmbBanco.SelectedValue));
            DataRow r1 = tbCheques.NewRow();
            r1[0] = txtCheque.Text;
            r1[1] = txtImporteCheque.Text;
            r1[2] = txtFechaVencimiento.Text;
            r1[3] = CmbBanco.SelectedValue;
            r1[4] = banco;
            tbCheques.Rows.Add(r1);
            GrillaCheques.DataSource = tbCheques;
            GrillaCheques.Columns[0].HeaderText = "Cheque";
            GrillaCheques.Columns[2].HeaderText = "Vencimiento";
            GrillaCheques.Columns[3].Visible = false;
            GrillaCheques.Columns[4].Width = 310;
            GrillaCheques.Columns[1].Width = 100;
            txtImporteCheque.Text = "";
            txtCheque.Text = "";
            txtFechaVencimiento.Text = "";
            double TotalCheques = 0;
            for (i = 0; i < tbCheques.Rows.Count; i++)
            {
                TotalCheques = TotalCheques + fun.ToDouble(tbCheques.Rows[i][1].ToString());
            }
            txtTotalCheque.Text = TotalCheques.ToString();
            //Clases.cFunciones fun = new Clases.cFunciones();
            txtTotalCheque.Text = fun.ParteEntera(txtTotalCheque.Text);
            txtTotalCheque.Text = fun.FormatoEnteroMiles(txtTotalCheque.Text);
            CalcularDiferencias();
        }

        private void txtImporteCheque_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtImporteCheque.Text != "")
            {
                txtImporteCheque.Text = fun.FormatoEnteroMiles(txtImporteCheque.Text);
            }
        }

        private void CalcularDiferencias()
        {
            Double Importe = 0;
            Double Cheque = 0;
            Double Dif = 0;
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtImporte.Text != "")
                Importe = fun.ToDouble(txtImporte.Text);
            if (txtTotalCheque.Text != "")
                Cheque = fun.ToDouble(txtTotalCheque.Text);
            Dif = Cheque - Importe;
            txtDiferencia.Text = Dif.ToString();
            txtDiferencia.Text = fun.FormatoEnteroMiles(txtDiferencia.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GrillaCheques.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una cheque para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            DataTable tbCheques = new DataTable();
            tbCheques.Columns.Add("NroCheque");
            tbCheques.Columns.Add("Importe");
            tbCheques.Columns.Add("FechaVencimiento");
            tbCheques.Columns.Add("CodBanco");
            tbCheques.Columns.Add("Banco");
            int i = 0;
            for (i = 0; i < GrillaCheques.Rows.Count - 1; i++)
            {
                string Cheque = GrillaCheques.Rows[i].Cells[0].Value.ToString();
                string Importe = GrillaCheques.Rows[i].Cells[1].Value.ToString();
                string FechaVencimiento = GrillaCheques.Rows[i].Cells[2].Value.ToString();
                string CodBanco = GrillaCheques.Rows[i].Cells[3].Value.ToString();
                string sBanco = GrillaCheques.Rows[i].Cells[4].Value.ToString();
                DataRow r = tbCheques.NewRow();
                r[0] = Cheque;
                r[1] = Importe;
                r[2] = FechaVencimiento;
                r[3] = CodBanco;
                tbCheques.Rows.Add(r);
            }

            string ChequeaBorrar = GrillaCheques.CurrentRow.Cells[0].Value.ToString();

            for (i = 0; i < tbCheques.Rows.Count; i++)
            {
                if (tbCheques.Rows[i]["NroCheque"].ToString() == ChequeaBorrar)
                {
                    tbCheques.Rows[i].Delete();
                    tbCheques.AcceptChanges();
                    i = tbCheques.Rows.Count;
                }
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            GrillaCheques.DataSource = tbCheques;
            double TotalCheques = 0;
            for (i = 0; i < tbCheques.Rows.Count; i++)
            {
                TotalCheques = TotalCheques + fun.ToDouble(tbCheques.Rows[i][1].ToString());
            }
            txtTotalCheque.Text = TotalCheques.ToString();
            CalcularDiferencias();
        }

        private void BuscarCheques(Int32 CodPrenda)
        {
            Clases.cCheque cheque = new Clases.cCheque();
            DataTable trdo = cheque.GetChequexCodPrenda(CodPrenda);
            Clases.cFunciones fun = new Clases.cFunciones();
            trdo = fun.TablaaMiles(trdo, "Importe");
            GrillaCheques.DataSource = trdo;
            GrillaCheques.Columns[0].HeaderText = "Cheque";
            GrillaCheques.Columns[2].HeaderText = "Vencimiento";
            GrillaCheques.Columns[3].Visible = false;
            GrillaCheques.Columns[4].Width = 310;
            GrillaCheques.Columns[1].Width = 100;
            txtImporteCheque.Text = "";
            txtCheque.Text = "";
            txtFechaVencimiento.Text = "";
            double TotalCheques = 0;
            
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                TotalCheques = TotalCheques + Convert.ToDouble(trdo.Rows[i][1].ToString());
            }
            txtTotalCheque.Text = TotalCheques.ToString();
            //Clases.cFunciones fun = new Clases.cFunciones();
            txtTotalCheque.Text = fun.ParteEntera(txtTotalCheque.Text);
            txtTotalCheque.Text = fun.FormatoEnteroMiles(txtTotalCheque.Text);
            CalcularDiferencias();
        }
    }
}
