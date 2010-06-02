namespace Apq.Windows.Controls.DataSetDTS
{
	partial class FromDataSet
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.dsTableName = new System.Data.DataSet();
			this.dataTable1 = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.dsTableName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(40, 54);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(52, 14);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "请选择表:";
			// 
			// dsTableName
			// 
			this.dsTableName.DataSetName = "NewDataSet";
			this.dsTableName.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
			// 
			// dataTable1
			// 
			this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
			this.dataTable1.TableName = "Table1";
			// 
			// dataColumn1
			// 
			this.dataColumn1.Caption = "选择";
			this.dataColumn1.ColumnName = "Checked";
			// 
			// dataColumn2
			// 
			this.dataColumn2.Caption = "表名";
			this.dataColumn2.ColumnName = "Name";
			// 
			// listBoxControl1
			// 
			this.listBoxControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxControl1.DataSource = this.dsTableName;
			this.listBoxControl1.DisplayMember = "Table1.Name";
			this.listBoxControl1.Location = new System.Drawing.Point(66, 95);
			this.listBoxControl1.Name = "listBoxControl1";
			this.listBoxControl1.Size = new System.Drawing.Size(458, 197);
			this.listBoxControl1.TabIndex = 1;
			this.listBoxControl1.ValueMember = "Table1.Name";
			// 
			// DataSetOut
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listBoxControl1);
			this.Controls.Add(this.labelControl1);
			this.Name = "DataSetOut";
			this.Size = new System.Drawing.Size(580, 360);
			this.Load += new System.EventHandler(this.DataSet_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsTableName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private System.Data.DataSet dsTableName;
		private System.Data.DataTable dataTable1;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
	}
}
