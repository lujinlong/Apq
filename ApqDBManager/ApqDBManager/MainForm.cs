using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace ApqDBManager
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
			tsmiFile.Text = Apq.GlobalObject.UILang["主菜单(&M)"];
			acNew.Text = Apq.GlobalObject.UILang["新建(&N)"];
			tsmiOpen.Text = Apq.GlobalObject.UILang["打开(&O)"];
			tsmiSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			tsmiSaveAs.Text = Apq.GlobalObject.UILang["另存为(&A)"];
			acExit.Text = Apq.GlobalObject.UILang["退出(&X)"];

			tsmiTool.Text = Apq.GlobalObject.UILang["工具(&T)"];
			acOption.Text = Apq.GlobalObject.UILang["选项(&O)"];
			acUILang.Text = Apq.GlobalObject.UILang["语言(&L)"];

			tsmiWindow.Text = Apq.GlobalObject.UILang["窗口(&W)"];
			acCloseAll.Text = Apq.GlobalObject.UILang["全部关闭(&L)"];

			tsmiHelp.Text = Apq.GlobalObject.UILang["帮助(&H)"];
			acSN.Text = Apq.GlobalObject.UILang["注册信息(&R)"];
			acAbout.Text = Apq.GlobalObject.UILang["关于(&A)"];
			#endregion

			this.acNew.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\New.png");
			this.tsmiOpen.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Open.png");
			this.tsmiSave.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Save.png");
			this.tsmiSaveAs.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\SaveAs.png");
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			#region 读取标题
			string cfgText = GlobalObject.XmlConfigChain[this.GetType(), "Text"];
			if (cfgText != null)
			{
				Text = cfgText;
			}
			#endregion

			acNew.DoExecute();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		#region 文件
		//新建
		private void acNew_Execute(object sender, EventArgs e)
		{
			SqlEdit childForm = new SqlEdit();
			childForm.Show(dockPanel1);
		}
		//退出
		private void acExit_Execute(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region 工具
		public void acOption_Execute(object sender, EventArgs e)
		{
			ApqDBManager.Forms.MainOption win = new ApqDBManager.Forms.MainOption();
			win.ShowDialog(this);
		}

		private void acUILang_Execute(object sender, EventArgs e)
		{
			DockContent dc = (DockContent)Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Apq.Windows.Forms.DockForms.UILangCfg));
			dc.Show(dockPanel1);
		}
		#endregion

		#region 窗口
		private void acCloseAll_Execute(object sender, EventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}

		private void acNewApp_Execute(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.ExecutablePath);
		}
		#endregion

		#region 帮助
		private void acSN_Execute(object sender, EventArgs e)
		{
			Form f = new Apq.Reg.Client.Win.RegForm();
			f.ShowDialog(this);
		}

		private void acAbout_Execute(object sender, EventArgs e)
		{
			GlobalObject.AboutBox.Asm = GlobalObject.TheAssembly;
			GlobalObject.AboutBox.ShowDialog(this);
		}
		#endregion

	}
}