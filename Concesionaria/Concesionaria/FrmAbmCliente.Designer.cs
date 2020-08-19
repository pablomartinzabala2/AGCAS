namespace Concesionaria
{
    partial class FrmAbmCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbmCliente));
            this.Grupo = new System.Windows.Forms.GroupBox();
            this.txtM_Celular = new System.Windows.Forms.MaskedTextBox();
            this.txtM_Telefono = new System.Windows.Forms.MaskedTextBox();
            this.txtCodCLiente = new System.Windows.Forms.TextBox();
            this.btnNuevoBarrio = new System.Windows.Forms.Button();
            this.cmb_CodBarrio = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Numero = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Calle = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Nombre = new System.Windows.Forms.TextBox();
            this.txt_Apellido = new System.Windows.Forms.TextBox();
            this.cmb_CodTipoDoc = new System.Windows.Forms.ComboBox();
            this.txt_NroDocumento = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BarraBotones = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.Grupo.SuspendLayout();
            this.BarraBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grupo
            // 
            this.Grupo.Controls.Add(this.txtM_Celular);
            this.Grupo.Controls.Add(this.txtM_Telefono);
            this.Grupo.Controls.Add(this.txtCodCLiente);
            this.Grupo.Controls.Add(this.btnNuevoBarrio);
            this.Grupo.Controls.Add(this.cmb_CodBarrio);
            this.Grupo.Controls.Add(this.label14);
            this.Grupo.Controls.Add(this.txt_Numero);
            this.Grupo.Controls.Add(this.label13);
            this.Grupo.Controls.Add(this.txt_Calle);
            this.Grupo.Controls.Add(this.label12);
            this.Grupo.Controls.Add(this.label11);
            this.Grupo.Controls.Add(this.label10);
            this.Grupo.Controls.Add(this.txt_Nombre);
            this.Grupo.Controls.Add(this.txt_Apellido);
            this.Grupo.Controls.Add(this.cmb_CodTipoDoc);
            this.Grupo.Controls.Add(this.txt_NroDocumento);
            this.Grupo.Controls.Add(this.label9);
            this.Grupo.Controls.Add(this.label8);
            this.Grupo.Controls.Add(this.label7);
            this.Grupo.Controls.Add(this.label6);
            this.Grupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grupo.Location = new System.Drawing.Point(12, 42);
            this.Grupo.Name = "Grupo";
            this.Grupo.Size = new System.Drawing.Size(741, 179);
            this.Grupo.TabIndex = 4;
            this.Grupo.TabStop = false;
            this.Grupo.Text = "Información del Cliente";
            // 
            // txtM_Celular
            // 
            this.txtM_Celular.Location = new System.Drawing.Point(470, 87);
            this.txtM_Celular.Mask = "000-000-000000";
            this.txtM_Celular.Name = "txtM_Celular";
            this.txtM_Celular.Size = new System.Drawing.Size(249, 23);
            this.txtM_Celular.TabIndex = 30;
            // 
            // txtM_Telefono
            // 
            this.txtM_Telefono.Location = new System.Drawing.Point(121, 84);
            this.txtM_Telefono.Mask = "(999)0000-0000";
            this.txtM_Telefono.Name = "txtM_Telefono";
            this.txtM_Telefono.Size = new System.Drawing.Size(246, 23);
            this.txtM_Telefono.TabIndex = 22;
            // 
            // txtCodCLiente
            // 
            this.txtCodCLiente.Location = new System.Drawing.Point(468, 147);
            this.txtCodCLiente.Name = "txtCodCLiente";
            this.txtCodCLiente.Size = new System.Drawing.Size(50, 23);
            this.txtCodCLiente.TabIndex = 29;
            // 
            // btnNuevoBarrio
            // 
            this.btnNuevoBarrio.Image = global::Concesionaria.Properties.Resources.page_add;
            this.btnNuevoBarrio.Location = new System.Drawing.Point(399, 141);
            this.btnNuevoBarrio.Name = "btnNuevoBarrio";
            this.btnNuevoBarrio.Size = new System.Drawing.Size(40, 28);
            this.btnNuevoBarrio.TabIndex = 25;
            this.btnNuevoBarrio.UseVisualStyleBackColor = true;
            this.btnNuevoBarrio.Click += new System.EventHandler(this.btnNuevoBarrio_Click);
            // 
            // cmb_CodBarrio
            // 
            this.cmb_CodBarrio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CodBarrio.FormattingEnabled = true;
            this.cmb_CodBarrio.Location = new System.Drawing.Point(121, 138);
            this.cmb_CodBarrio.Name = "cmb_CodBarrio";
            this.cmb_CodBarrio.Size = new System.Drawing.Size(246, 24);
            this.cmb_CodBarrio.TabIndex = 19;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 141);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 17);
            this.label14.TabIndex = 23;
            this.label14.Text = "Barrio";
            // 
            // txt_Numero
            // 
            this.txt_Numero.Location = new System.Drawing.Point(470, 118);
            this.txt_Numero.Name = "txt_Numero";
            this.txt_Numero.Size = new System.Drawing.Size(246, 23);
            this.txt_Numero.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(396, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 17);
            this.label13.TabIndex = 21;
            this.label13.Text = "Altura";
            // 
            // txt_Calle
            // 
            this.txt_Calle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Calle.Location = new System.Drawing.Point(121, 112);
            this.txt_Calle.Name = "txt_Calle";
            this.txt_Calle.Size = new System.Drawing.Size(246, 23);
            this.txt_Calle.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 17);
            this.label12.TabIndex = 19;
            this.label12.Text = "Calle";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(392, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "Celular";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "Teléfono";
            // 
            // txt_Nombre
            // 
            this.txt_Nombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Nombre.Location = new System.Drawing.Point(121, 55);
            this.txt_Nombre.Name = "txt_Nombre";
            this.txt_Nombre.Size = new System.Drawing.Size(246, 23);
            this.txt_Nombre.TabIndex = 13;
            // 
            // txt_Apellido
            // 
            this.txt_Apellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Apellido.Location = new System.Drawing.Point(468, 58);
            this.txt_Apellido.Name = "txt_Apellido";
            this.txt_Apellido.Size = new System.Drawing.Size(246, 23);
            this.txt_Apellido.TabIndex = 14;
            // 
            // cmb_CodTipoDoc
            // 
            this.cmb_CodTipoDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CodTipoDoc.FormattingEnabled = true;
            this.cmb_CodTipoDoc.Location = new System.Drawing.Point(121, 25);
            this.cmb_CodTipoDoc.Name = "cmb_CodTipoDoc";
            this.cmb_CodTipoDoc.Size = new System.Drawing.Size(246, 24);
            this.cmb_CodTipoDoc.TabIndex = 12;
            // 
            // txt_NroDocumento
            // 
            this.txt_NroDocumento.Location = new System.Drawing.Point(468, 29);
            this.txt_NroDocumento.Name = "txt_NroDocumento";
            this.txt_NroDocumento.Size = new System.Drawing.Size(246, 23);
            this.txt_NroDocumento.TabIndex = 12;
            this.txt_NroDocumento.TextChanged += new System.EventHandler(this.txt_NroDocumento_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(396, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Apellido";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Nombre";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Número";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Documento";
            // 
            // BarraBotones
            // 
            this.BarraBotones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraBotones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnEliminar,
            this.btnAceptar,
            this.btnCancelar,
            this.btnAbrir,
            this.btnImprimir,
            this.btnSalir});
            this.BarraBotones.Location = new System.Drawing.Point(0, 0);
            this.BarraBotones.Name = "BarraBotones";
            this.BarraBotones.ShowItemToolTips = false;
            this.BarraBotones.Size = new System.Drawing.Size(764, 39);
            this.BarraBotones.TabIndex = 9;
            this.BarraBotones.Text = "toolStrip1";
            this.BarraBotones.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.BarraBotones_ItemClicked);
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(36, 36);
            this.btnNuevo.Text = "toolStripButton1";
            this.btnNuevo.ToolTipText = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(36, 36);
            this.btnEditar.Text = "toolStripButton2";
            this.btnEditar.ToolTipText = "Modificar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(36, 36);
            this.btnEliminar.Text = "toolStripButton3";
            this.btnEliminar.ToolTipText = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(36, 36);
            this.btnAceptar.Text = "Grabar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(36, 36);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(36, 36);
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(36, 36);
            this.btnImprimir.Text = "toolStripButton1";
            this.btnImprimir.ToolTipText = "Imprimir";
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(36, 36);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FrmAbmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(764, 235);
            this.Controls.Add(this.BarraBotones);
            this.Controls.Add(this.Grupo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAbmCliente";
            this.Load += new System.EventHandler(this.FrmAbmCliente_Load);
            this.Grupo.ResumeLayout(false);
            this.Grupo.PerformLayout();
            this.BarraBotones.ResumeLayout(false);
            this.BarraBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Grupo;
        private System.Windows.Forms.MaskedTextBox txtM_Celular;
        private System.Windows.Forms.MaskedTextBox txtM_Telefono;
        private System.Windows.Forms.TextBox txtCodCLiente;
        private System.Windows.Forms.Button btnNuevoBarrio;
        private System.Windows.Forms.ComboBox cmb_CodBarrio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Numero;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Calle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Nombre;
        private System.Windows.Forms.TextBox txt_Apellido;
        private System.Windows.Forms.ComboBox cmb_CodTipoDoc;
        private System.Windows.Forms.TextBox txt_NroDocumento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip BarraBotones;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnSalir;
    }
}