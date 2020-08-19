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
    public partial class FrmDocumentosAnteriores : Form
    {
        public FrmDocumentosAnteriores()
        {
            InitializeComponent();
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmb_CodMarca, "Marca", "Nombre", "CodMarca");
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
            int b = 0;
            string Patente = txtPatente.Text;
            if (Patente.Length > 5)
            {
                Clases.cAuto auto = new Clases.cAuto();
                DataTable trdo = auto.GetAutoxPatente(Patente);
                if (trdo.Rows.Count > 0)
                {
                    b = 1;
                    txtCodAuto.Text = trdo.Rows[0]["CodAuto"].ToString();
                    txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                    if (ExisteStock (Convert.ToInt32 (txtCodAuto.Text))==true)
                    {
                        MessageBox.Show("El auto ya existe en el stock", Clases.cMensaje.Mensaje());
                        b = 0;
                    }
                }
            }
            if (b == 0)
            {
                txtCodAuto.Text = "";
                txtDescripcion.Text = "";
                txtCodAuto.Text = "";
            }
        }

        private Boolean ExisteStock(Int32 CodAuto)
        {
            Boolean Existe = false ;
            Clases.cStockAuto stock = new Clases.cStockAuto();
            DataTable trdo = stock.GetStockAutosVigentes(CodAuto);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodAuto"].ToString() != "")
                    Existe = true;
            }
            return Existe;
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ",")
                e.Handled = true;
        }

        private void txtCapital_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ",")
                e.Handled = true;
        }

        private void txtInteres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCapital_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtCapital.Text != "")
            {
                txtCapital.Text = fun.FormatoEnteroMiles(txtCapital.Text);
            }
        }

        private void btnCalcularCuotas_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }
            int n = 2;
            int x = 4;
            double r = Math.Pow(x, n);
            double Capital = 0;
            double Interes = 0;
            double Cuotas = 0;
            double ValorCuota = 0;

            if (txtCapital.Text == "")
            {
                MessageBox.Show("Debe ingresar un capital", Clases.cMensaje.Mensaje());
                return;
            }
            if (txtCapital.Text == "")
            {
                MessageBox.Show("Debe ingresar un capital", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtInteres.Text == "")
            {
                MessageBox.Show("Debe ingresar un interés", Clases.cMensaje.Mensaje());
                return;
            }

            if (Convert.ToDouble(txtInteres.Text) > 99)
            {
                MessageBox.Show("Debe ingresar un interés menor a 100", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtCuotas.Text == "")
            {
                MessageBox.Show("Debe ingresar una cantidad de cuotas para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtCapital.Text != "")
                Capital = fun.ToDouble(txtCapital.Text);
            if (txtCuotas.Text != "")
                Cuotas = fun.ToDouble(txtCuotas.Text);
            if (txtInteres.Text != "")
            {
                Interes = Convert.ToDouble(txtInteres.Text);

            }


            double d1 = Cuotas * Interes;
            d1 = d1 / 100;
            double d2 = Capital * d1;
            ValorCuota = (Capital + d2) / Cuotas;
            Int32 ValorCuotaEntero = Convert.ToInt32(ValorCuota);
            Int32 ValorCuotaSinInteres = Convert.ToInt32(Capital / Cuotas);
            DateTime FechaVencimiento = Convert.ToDateTime(txtFecha.Text);
            DataTable tcuotas = new DataTable();
            tcuotas.Columns.Add("Cuota");
            tcuotas.Columns.Add("Importe");
            tcuotas.Columns.Add("FechaVencimiento");
            tcuotas.Columns.Add("CuotasSinInteres");
            int i = 0;
            FechaVencimiento = FechaVencimiento.AddMonths(1);
            DataRow row;
            Int32 AcumuladorCuotasSinInteres = 0;
            for (i = 0; i < Convert.ToInt32(Cuotas); i++)
            {
                row = tcuotas.NewRow();
                row["Cuota"] = (i + 1).ToString();
                row["Importe"] = ValorCuotaEntero.ToString();
                row["FechaVencimiento"] = FechaVencimiento.ToShortDateString();

                AcumuladorCuotasSinInteres = AcumuladorCuotasSinInteres + ValorCuotaSinInteres;
                FechaVencimiento = FechaVencimiento.AddMonths(1);
                if (i == Cuotas - 1)
                {
                    //es la ultima cuota y le agrego la diferencia.
                    Int32 Dif = AcumuladorCuotasSinInteres - Convert.ToInt32(Capital);
                    ValorCuotaSinInteres = ValorCuotaSinInteres - Dif;
                }
                row["CuotasSinInteres"] = ValorCuotaSinInteres.ToString();
                tcuotas.Rows.Add(row);
            }
            GrillaCuotas.DataSource = tcuotas;
            GrillaCuotas.Columns[2].HeaderText = "Vencimiento";
            GrillaCuotas.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            GrillaCuotas.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight;
            GrillaCuotas.Columns[0].Width = 50;
            GrillaCuotas.Columns[1].Width = 150;
            GrillaCuotas.Columns[2].Width = 150;
        }

        private void btnBorrarCuotas_Click(object sender, EventArgs e)
        {
            GrillaCuotas.DataSource = null;
            txtCapital.Text = "";
            txtCuotas.Text = "";
            txtInteres.Text = "";
        }

        private void GrabarCuotas()
        {
            string Cuota = "";
            string ImporteCuota = "";
            string FechaVecimiento = "";
            string ImporteSinInteres = "";
            string sqlInsertCuota = "";
            string Nombre = txtNombre.Text;
            string Apellido = txtApellido.Text;
            string Telefono = txtTelefono.Text;
            string Direccion = txtDireccion.Text;
            Int32 CodMarca = Convert.ToInt32(cmb_CodMarca.SelectedValue);
            string Anio = txtAnio.Text;
            Clases.cCuotasAnteriores cuotaAnt = new Clases.cCuotasAnteriores();
            Int32 CodGrupo = cuotaAnt.GetMaxCodGrupo();
            for (int i = 0; i < GrillaCuotas.Rows.Count - 1; i++)
            {
                Cuota = GrillaCuotas.Rows[i].Cells[0].Value.ToString();
                ImporteCuota = GrillaCuotas.Rows[i].Cells[1].Value.ToString();
                FechaVecimiento = GrillaCuotas.Rows[i].Cells[2].Value.ToString();
                ImporteSinInteres = GrillaCuotas.Rows[i].Cells[3].Value.ToString();
                sqlInsertCuota = "Insert into CuotasAnteriores(CodGrupo,Cuota,Importe,FechaVencimiento,Saldo,ImporteSinInteres,Nombre,Apellido,Telefono,Patente,Descripcion,CodMarca,Anio,Direccion)";
                sqlInsertCuota = sqlInsertCuota + " values (";
                sqlInsertCuota = sqlInsertCuota + CodGrupo.ToString();
                sqlInsertCuota = sqlInsertCuota + "," + Cuota;
                sqlInsertCuota = sqlInsertCuota + "," + ImporteCuota;
                sqlInsertCuota = sqlInsertCuota + "," + "'" + FechaVecimiento + "'";
                sqlInsertCuota = sqlInsertCuota + "," + ImporteCuota;
                sqlInsertCuota = sqlInsertCuota + "," + ImporteSinInteres;
                sqlInsertCuota = sqlInsertCuota + "," + "'" + Nombre + "'";
                sqlInsertCuota = sqlInsertCuota + "," + "'" + Apellido + "'";
                sqlInsertCuota = sqlInsertCuota + "," + "'" + Telefono + "'";
                sqlInsertCuota = sqlInsertCuota + "," + "'" + txtPatente.Text  + "'";
                sqlInsertCuota = sqlInsertCuota + "," + "'" + txtDescripcion.Text + "'";
                sqlInsertCuota = sqlInsertCuota + "," + CodMarca.ToString();
                sqlInsertCuota = sqlInsertCuota + "," + "'" + Anio + "'";
                sqlInsertCuota = sqlInsertCuota + "," + "'" + Direccion  + "'";
                sqlInsertCuota = sqlInsertCuota + ")";   
                Clases.cDb.ExecutarNonQuery(sqlInsertCuota);
            }
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            Limpiar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Validar()==true)
                GrabarCuotas();
        }

        private Boolean Validar()
        {
            Boolean op = true;
            if (txtPatente.Text == "")
            {
                MessageBox.Show("Debe ingresar una patente para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Debe ingresar una descripción para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (cmb_CodMarca.SelectedIndex < 1)
            {
                MessageBox.Show("Debe seleccionar una marca para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre para continuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe ingresar un apellido para continuar", Clases.cMensaje.Mensaje());
                return false;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha (txtFecha.Text)==false)
            {
                MessageBox.Show("La fecha ingresada es incontinuar", Clases.cMensaje.Mensaje());
                return false;
            }

            if (GrillaCuotas.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar un plan de cuotas", Clases.cMensaje.Mensaje());
                return false;
            }

            return op;
        }

        private void Limpiar()
        {
            txtCodAuto.Text = "";
            txtPatente.Text = "";
            txtDescripcion.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
            txtCapital.Text = "";
            txtFecha.Text = "";
            txtInteres.Text = "";
            txtCuotas.Text = "";
            GrillaCuotas.DataSource = null;
            txtDireccion.Text = "";
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            e.Handled = fun.SoloNumerosEnteros(e);
        }
    }
}
