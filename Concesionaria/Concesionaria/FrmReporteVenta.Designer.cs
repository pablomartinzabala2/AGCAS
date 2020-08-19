namespace Concesionaria
{
    partial class FrmReporteVenta
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReporteAutoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.COPIACONCESIONARIADataSet = new Concesionaria.COPIACONCESIONARIADataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReporteAutoTableAdapter = new Concesionaria.COPIACONCESIONARIADataSetTableAdapters.ReporteAutoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteAutoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.COPIACONCESIONARIADataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // ReporteAutoBindingSource
            // 
            this.ReporteAutoBindingSource.DataMember = "ReporteAuto";
            this.ReporteAutoBindingSource.DataSource = this.COPIACONCESIONARIADataSet;
            // 
            // COPIACONCESIONARIADataSet
            // 
            this.COPIACONCESIONARIADataSet.DataSetName = "COPIACONCESIONARIADataSet";
            this.COPIACONCESIONARIADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReporteAutoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Concesionaria.ReporteVentas.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(772, 583);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReporteAutoTableAdapter
            // 
            this.ReporteAutoTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReporteVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 607);
            this.Controls.Add(this.reportViewer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReporteVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de venta";
            this.Load += new System.EventHandler(this.FrmReporteVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReporteAutoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.COPIACONCESIONARIADataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReporteAutoBindingSource;
        private COPIACONCESIONARIADataSet COPIACONCESIONARIADataSet;
        private COPIACONCESIONARIADataSetTableAdapters.ReporteAutoTableAdapter ReporteAutoTableAdapter;
    }
}