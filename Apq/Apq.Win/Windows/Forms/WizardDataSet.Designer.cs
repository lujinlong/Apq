namespace Apq.Windows.Forms
{
	partial class WizardDataSet
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this._ds = new System.Data.DataSet();
			((System.ComponentModel.ISupportInitialize)(this._ds)).BeginInit();
			this.SuspendLayout();
			// 
			// btnBack
			// 
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnFinish
			// 
			this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
			// 
			// btnNext
			// 
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// _ds
			// 
			this._ds.DataSetName = "NewDataSet";
			// 
			// WizardDataSet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(600, 423);
			this.Name = "WizardDataSet";
			this.Load += new System.EventHandler(this.WizardDataSet_Load);
			((System.ComponentModel.ISupportInitialize)(this._ds)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Data.DataSet _ds;
	}
}
