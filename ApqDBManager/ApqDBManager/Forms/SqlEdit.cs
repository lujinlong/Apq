using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;
using ApqDBManager.Forms;

namespace ApqDBManager
{
	/// <summary>
	/// 可作为 后台多线程示例
	/// </summary>
	public partial class SqlEdit : Apq.Windows.Forms.DockForm
	{
		public SqlEdit()
		{
			InitializeComponent();
		}

		public DBIs DBIs = new DBIs();
		public SqlOut SqlOut = new SqlOut();
		public SqlEditDoc SqlEditDoc = new SqlEditDoc();
		public ErrList ErrList = new ErrList();

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			#region 工具栏
			acOpen.Text = Apq.GlobalObject.UILang["打开(&O)"];
			acSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			acSaveAs.Text = Apq.GlobalObject.UILang["另存为(&A)"];

			acDBI.Text = Apq.GlobalObject.UILang["数据库列表(&L)"];
			acOut.Text = Apq.GlobalObject.UILang["输出(&O)"];
			acErrList.Text = Apq.GlobalObject.UILang["错误列表(&E)"];
			#endregion

			this.acOpen.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Open.png");
			this.acSave.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Save.png");
		}

		private void SqlEdit_Load(object sender, EventArgs e)
		{
			SqlEditDoc.Show(dockPanel1);
			//SqlOut.Show(dockPanel1);
		}

		private void SqlEdit_FormClosing(object sender, FormClosingEventArgs e)
		{
			ErrList.Close();
			SqlOut.Close();
			DBIs.Close();
			SqlEditDoc.Close();
		}

		private void SqlEdit_Activated(object sender, EventArgs e)
		{
		}

		private void SqlEdit_Deactivate(object sender, EventArgs e)
		{

		}

		private void acDBI_Execute(object sender, EventArgs e)
		{
			acDBI.Checked = !acDBI.Checked;
			if (acDBI.Checked)
			{
				DBIs.Show(dockPanel1);
			}
			else
			{
				DBIs.Hide();
			}
		}

		private void acOut_Execute(object sender, EventArgs e)
		{
			acOut.Checked = !acOut.Checked;
			if (acOut.Checked)
			{
				SqlOut.Show(dockPanel1);
			}
			else
			{
				SqlOut.Hide();
			}
		}

		private void acErrList_Execute(object sender, EventArgs e)
		{
			acErrList.Checked = !acErrList.Checked;
			if (acErrList.Checked)
			{
				ErrList.Show(dockPanel1);
			}
			else
			{
				ErrList.Hide();
			}
		}
	}
}