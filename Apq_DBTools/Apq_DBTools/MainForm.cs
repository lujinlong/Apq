using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace Apq_DBTools
{
	public partial class MainForm : Apq.Windows.Forms.ImeForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			#region 菜单
			tsmiFile.Text = Apq.GlobalObject.UILang["文件(&F)"];
			tsmiExit.Text = Apq.GlobalObject.UILang["退出(&X)"];

			tsmiTool.Text = Apq.GlobalObject.UILang["工具(&T)"];
			tsmiOption.Text = Apq.GlobalObject.UILang["选项(&O)"];
			tsmiUILang.Text = Apq.GlobalObject.UILang["语言(&L)"];
			tsmiSqlGen.Text = Apq.GlobalObject.UILang["脚本生成(&G)"];
			tsmiFSRename.Text = Apq.GlobalObject.UILang["批量重命名(&H)"];

			tsmiWindow.Text = Apq.GlobalObject.UILang["窗口(&W)"];
			tsmiCloseAll.Text = Apq.GlobalObject.UILang["全部关闭(&L)"];

			tsmiHelp.Text = Apq.GlobalObject.UILang["帮助(&H)"];
			tsmiSN.Text = Apq.GlobalObject.UILang["注册信息(&R)"];
			tsmiAbout.Text = Apq.GlobalObject.UILang["关于(&A)"];
			#endregion
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			string cfgText = GlobalObject.XmlConfigChain[this.GetType(), "Text"];
			if (cfgText != null)
			{
				Text = cfgText;
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		#region 文件
		//退出
		private void tsmiExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region 工具
		private void tsmiOption_Click(object sender, EventArgs e)
		{
			Apq_DBTools.Forms.MainOption win = new Apq_DBTools.Forms.MainOption();
			win.ShowDialog(this);
		}

		private void tsmiUILang_Click(object sender, EventArgs e)
		{
			DockContent dc = (DockContent)Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Apq.Windows.Forms.DockForms.UILangCfg));
			dc.Show(dockPanel1);
		}

		private void tsmiSqlGen_Click(object sender, EventArgs e)
		{
            SqlGen win = new SqlGen();
			win.Show(dockPanel1);
		}

		private void tsmiFSRename_Click(object sender, EventArgs e)
		{
			FSRename win = new FSRename();
			win.Show(dockPanel1);
		}
		#endregion

		#region 窗口
		private void tsmiCloseAll_Click(object sender, EventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}

		private void tsmiNewApp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.ExecutablePath);
		}
		#endregion

		#region 帮助
		private void tsmiSN_Click(object sender, EventArgs e)
		{
			Form f = new Apq.Reg.Client.Win.RegForm();
			f.ShowDialog(this);
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			GlobalObject.AboutBox.Asm = GlobalObject.TheAssembly;
			GlobalObject.AboutBox.ShowDialog(this);
		}
		#endregion
	}
}