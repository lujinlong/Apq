using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqDBManager.Forms
{
	public partial class DBCEdit : Form
	{
		public DBCEdit()
		{
			InitializeComponent();
		}

		private DataRow _drCs = null;

		/// <summary>
		/// 存储Combox的可选项
		/// </summary>
		private DataSet _ds = new DataSet();

		private void DBCEdit_Load(object sender, EventArgs e)
		{
			// 加载Combox可选项
			if (!_ds.Tables.Contains("DBNames"))
			{
				_ds.Tables.Add(GlobalObject.XmlConfigChain.GetTableConfig("ApqDBManager.Forms.DBCEdit", "DBNames") ?? new DataTable("DBNames"));
				_ds.Tables.Add(GlobalObject.XmlConfigChain.GetTableConfig("ApqDBManager.Forms.DBCEdit", "ServerNames") ?? new DataTable("ServerNames"));
				_ds.Tables.Add(GlobalObject.XmlConfigChain.GetTableConfig("ApqDBManager.Forms.DBCEdit", "UserIds") ?? new DataTable("UserIds"));
				DataTable dt = _ds.Tables["DBNames"];
				if (!dt.Columns.Contains("DBName")) dt.Columns.Add("DBName");
				dt = _ds.Tables["ServerNames"];
				if (!dt.Columns.Contains("ServerName")) dt.Columns.Add("ServerName");
				dt = _ds.Tables["UserIds"];
				if (!dt.Columns.Contains("UserId")) dt.Columns.Add("UserId");

				// 绑定到Combox
				cbDBName.DataSource = _ds.Tables["DBNames"];
				cbDBName.DisplayMember = "DBName";
				cbServerName.DataSource = _ds.Tables["ServerNames"];
				cbServerName.DisplayMember = "ServerName";
				cbUserId.DataSource = _ds.Tables["UserIds"];
				cbUserId.DisplayMember = "UserId";

				// 初始化为未选定
				cbDBName.SelectedIndex = -1;
				cbServerName.SelectedIndex = -1;
				cbUserId.SelectedIndex = -1;
			}
		}

		#region 保存用户输入的新值并设置SelectedIndex为正确值
		private void cbDBName_DropDown(object sender, EventArgs e)
		{
			if (cbDBName.Text.Length <= 0)
			{
				cbDBName.SelectedIndex = -1;
				return;
			}

			string str = cbDBName.Text;
			string strSqlON = Apq.Data.SqlClient.Common.ConvertToSqlON(str);
			DataRow[] drs = _ds.Tables["DBNames"].Select(string.Format("DBName = {0}", strSqlON));
			if (drs == null || drs.Length == 0)
			{
				DataRow dr = _ds.Tables["DBNames"].Rows.Add(str);
				cbDBName.SelectedIndex = _ds.Tables["DBNames"].Rows.IndexOf(dr);
			}

			GlobalObject.XmlUserConfig.SetTableConfig("ApqDBManager.Forms.DBCEdit", "DBNames", _ds.Tables["DBNames"]);
		}

		private void cbServerName_DropDown(object sender, EventArgs e)
		{
			if (cbServerName.Text.Length <= 0)
			{
				cbServerName.SelectedIndex = -1;
				return;
			}

			string str = cbServerName.Text;
			string strSqlON = Apq.Data.SqlClient.Common.ConvertToSqlON(str);
			DataRow[] drs = _ds.Tables["ServerNames"].Select(string.Format("ServerName = {0}", strSqlON));
			if (drs == null || drs.Length == 0)
			{
				DataRow dr = _ds.Tables["ServerNames"].Rows.Add(str);
				cbServerName.SelectedIndex = _ds.Tables["ServerNames"].Rows.IndexOf(dr);
			}

			GlobalObject.XmlUserConfig.SetTableConfig("ApqDBManager.Forms.DBCEdit", "ServerNames", _ds.Tables["ServerNames"]);
		}

		private void cbUserId_DropDown(object sender, EventArgs e)
		{
			if (cbUserId.Text.Length <= 0)
			{
				cbUserId.SelectedIndex = -1;
				return;
			}

			string str = cbUserId.Text;
			string strSqlON = Apq.Data.SqlClient.Common.ConvertToSqlON(str);
			DataRow[] drs = _ds.Tables["UserIds"].Select(string.Format("UserId = {0}", strSqlON));
			if (drs == null || drs.Length == 0)
			{
				DataRow dr = _ds.Tables["UserIds"].Rows.Add(str);
				cbUserId.SelectedIndex = _ds.Tables["UserIds"].Rows.IndexOf(dr);
			}

			GlobalObject.XmlUserConfig.SetTableConfig("ApqDBManager.Forms.DBCEdit", "UserIds", _ds.Tables["UserIds"]);
		}
		#endregion

		#region 按钮
		private void btnConfirm_Click(object sender, EventArgs e)
		{
			_drCs["name"] = beName.Text;
			_drCs["DBName"] = cbDBName.Text;
			_drCs["ServerName"] = cbServerName.Text;
			_drCs["UserId"] = cbUserId.Text;
			_drCs["Pwd"] = txtPwd.EditValue;
			_drCs["Option"] = txtOption.EditValue;

			DialogResult = DialogResult.OK;
			this.Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Hide();
		}

		private void beName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			beName.EditValue = cbDBName.Text;
		}
		#endregion

		/// <summary>
		/// 查看
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="ReadOnly">是否只读</param>
		public void ViewOne(DataRow dr, bool ReadOnly)
		{
			_drCs = dr;

			// 清空原值
			beName.EditValue = string.Empty;
			cbDBName.SelectedIndex = -1;
			cbServerName.SelectedIndex = -1;
			cbUserId.SelectedIndex = -1;
			txtPwd.EditValue = string.Empty;
			txtOption.EditValue = string.Empty;

			// 设置只读
			beName.Properties.ReadOnly = ReadOnly;
			cbDBName.Enabled = !ReadOnly;
			cbServerName.Enabled = !ReadOnly;
			cbUserId.Enabled = !ReadOnly;
			txtPwd.Properties.ReadOnly = ReadOnly;
			txtOption.Properties.ReadOnly = ReadOnly;

			if (dr == null)
			{
				return;
			}

			// 显示新值
			beName.Text = Apq.Convert.ChangeType<string>(dr["name"]);
			cbDBName.Text = Apq.Convert.ChangeType<string>(dr["DBName"]);
			cbServerName.Text = Apq.Convert.ChangeType<string>(dr["ServerName"]);
			cbUserId.Text = Apq.Convert.ChangeType<string>(dr["UserId"]);
			txtPwd.EditValue = Apq.Convert.ChangeType<string>(dr["Pwd"]);
			txtOption.EditValue = Apq.Convert.ChangeType<string>(dr["Option"]);
		}

	}
}
