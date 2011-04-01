namespace ApqDBManager.Controls.MainOption
{
	partial class XmlServers
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
			this.beXmlServers = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.ofbXmlServers = new System.Windows.Forms.OpenFileDialog();
			this.lbcRDBTypes = new DevExpress.XtraEditors.ListBoxControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtRDBType = new DevExpress.XtraEditors.TextEdit();
			this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.btnUp = new DevExpress.XtraEditors.SimpleButton();
			this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
			this.btnDown = new DevExpress.XtraEditors.SimpleButton();
			this.btnDel = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.beXmlServers.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lbcRDBTypes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRDBType.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// beXmlServers
			// 
			this.beXmlServers.Enabled = false;
			this.beXmlServers.Location = new System.Drawing.Point(105, 22);
			this.beXmlServers.Name = "beXmlServers";
			this.beXmlServers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.beXmlServers.Size = new System.Drawing.Size(349, 21);
			this.beXmlServers.TabIndex = 8;
			this.beXmlServers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beXmlServers_ButtonClick);
			this.beXmlServers.EditValueChanged += new System.EventHandler(this.beXmlServers_EditValueChanged);
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(15, 25);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(84, 14);
			this.labelControl1.TabIndex = 7;
			this.labelControl1.Text = "服务器列表文件";
			// 
			// ofbXmlServers
			// 
			this.ofbXmlServers.DefaultExt = "xml";
			this.ofbXmlServers.FileName = "XmlServers.xml";
			this.ofbXmlServers.Filter = "Xml文件(*.xml)|*.xml|所有文件(*.*)|*.*";
			this.ofbXmlServers.RestoreDirectory = true;
			// 
			// lbcRDBTypes
			// 
			this.lbcRDBTypes.Location = new System.Drawing.Point(105, 81);
			this.lbcRDBTypes.Name = "lbcRDBTypes";
			this.lbcRDBTypes.Size = new System.Drawing.Size(268, 217);
			this.lbcRDBTypes.TabIndex = 9;
			this.lbcRDBTypes.SelectedIndexChanged += new System.EventHandler(this.lbcRDBTypes_SelectedIndexChanged);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(15, 57);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(84, 14);
			this.labelControl2.TabIndex = 10;
			this.labelControl2.Text = "服务器类型设置";
			// 
			// txtRDBType
			// 
			this.txtRDBType.Location = new System.Drawing.Point(105, 54);
			this.txtRDBType.Name = "txtRDBType";
			this.txtRDBType.Size = new System.Drawing.Size(268, 21);
			this.txtRDBType.TabIndex = 11;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(379, 52);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 12;
			this.btnAdd.Text = "添加";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(379, 113);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(75, 23);
			this.btnUp.TabIndex = 13;
			this.btnUp.Text = "上移";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(379, 227);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(75, 23);
			this.btnUpdate.TabIndex = 14;
			this.btnUpdate.Text = "修改";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(379, 142);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(75, 23);
			this.btnDown.TabIndex = 15;
			this.btnDown.Text = "下移";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnDel
			// 
			this.btnDel.Location = new System.Drawing.Point(379, 256);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(75, 23);
			this.btnDel.TabIndex = 16;
			this.btnDel.Text = "删除";
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// XmlServers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnDel);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.txtRDBType);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.lbcRDBTypes);
			this.Controls.Add(this.beXmlServers);
			this.Controls.Add(this.labelControl1);
			this.Name = "XmlServers";
			this.Size = new System.Drawing.Size(480, 380);
			((System.ComponentModel.ISupportInitialize)(this.beXmlServers.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lbcRDBTypes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRDBType.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit beXmlServers;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private System.Windows.Forms.OpenFileDialog ofbXmlServers;
		private DevExpress.XtraEditors.ListBoxControl lbcRDBTypes;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit txtRDBType;
		private DevExpress.XtraEditors.SimpleButton btnAdd;
		private DevExpress.XtraEditors.SimpleButton btnUp;
		private DevExpress.XtraEditors.SimpleButton btnUpdate;
		private DevExpress.XtraEditors.SimpleButton btnDown;
		private DevExpress.XtraEditors.SimpleButton btnDel;

	}
}
