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
	public partial class SqlEdit : Apq.Windows.Forms.DockForm
	{
		public SqlEdit()
		{
			InitializeComponent();

			DBIs.SqlEdit = this;
			SqlOut.SqlEdit = this;
			SqlEditDoc.SqlEdit = this;
			ErrList.SqlEdit = this;
		}

		public DBIs DBIs = new DBIs();
		public SqlOut SqlOut = new SqlOut();
		public SqlEditDoc SqlEditDoc = new SqlEditDoc();
		public ErrList ErrList = new ErrList();

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			#region 工具栏
			tsbOpen.Text = Apq.GlobalObject.UILang["打开(&O)"];
			tsbSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			tsbSaveAs.Text = Apq.GlobalObject.UILang["另存为(&A)"];

			tsddbView.Text = Apq.GlobalObject.UILang["视图(&V)"];
			tsmiDBI.Text = Apq.GlobalObject.UILang["数据库列表(&L)"];
			tsmiOut.Text = Apq.GlobalObject.UILang["输出(&O)"];
			tsmiErrList.Text = Apq.GlobalObject.UILang["错误列表(&E)"];
			#endregion

			this.tsbOpen.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Open.png");
			this.tsbSave.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Save.png");
		}

		private void SqlEdit_Load(object sender, EventArgs e)
		{
			SqlEditDoc.Show(dockPanel1);
			DBIs.Show(dockPanel1);
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

		private void tsmiDBI_CheckedChanged(object sender, EventArgs e)
		{
			if (tsmiDBI.Checked)
			{
				DBIs.Show(dockPanel1);
			}
			else
			{
				DBIs.Hide();
			}
		}

		private void tsmiOut_CheckedChanged(object sender, EventArgs e)
		{
			if (tsmiOut.Checked)
			{
				SqlOut.Show(dockPanel1);
			}
			else
			{
				SqlOut.Hide();
			}
		}

		private void tsmiErrList_CheckedChanged(object sender, EventArgs e)
		{
			if (tsmiErrList.Checked)
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