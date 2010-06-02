using DevExpress.XtraGrid.Views.Grid;
namespace ApqDBManager.Forms
{
	partial class ErrList
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colRSrvID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.col__ServerName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.cols = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.DataMember = "ErrList";
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(992, 266);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRSrvID,
            this.col__ServerName,
            this.cols});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.IndicatorWidth = 50;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.gridView1.OptionsBehavior.Editable = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// colRSrvID
			// 
			this.colRSrvID.Caption = "服务器编号";
			this.colRSrvID.FieldName = "RSrvID";
			this.colRSrvID.Name = "colRSrvID";
			this.colRSrvID.Visible = true;
			this.colRSrvID.VisibleIndex = 0;
			// 
			// col__ServerName
			// 
			this.col__ServerName.Caption = "服务器";
			this.col__ServerName.FieldName = "__ServerName";
			this.col__ServerName.Name = "col__ServerName";
			this.col__ServerName.Visible = true;
			this.col__ServerName.VisibleIndex = 1;
			// 
			// cols
			// 
			this.cols.Caption = "错误信息";
			this.cols.FieldName = "s";
			this.cols.Name = "cols";
			this.cols.Visible = true;
			this.cols.VisibleIndex = 2;
			// 
			// ErrList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 266);
			this.Controls.Add(this.gridControl1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Name = "ErrList";
			this.TabText = "错误列表";
			this.Text = "错误列表";
			this.Load += new System.EventHandler(this.ErrList_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn colRSrvID;
		private DevExpress.XtraGrid.Columns.GridColumn col__ServerName;
		private DevExpress.XtraGrid.Columns.GridColumn cols;
	}
}