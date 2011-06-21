using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace ApqDBCManager
{
	public partial class MainForm : Apq.Windows.Forms.DockForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			#region 菜单
			tsmiFile.Text = Apq.GlobalObject.UILang["主菜单(&M)"];
			tsmiNew.Text = Apq.GlobalObject.UILang["新文档(&N)"];
			tsmiDBS.Text = Apq.GlobalObject.UILang["服务器列表(&S)"];
			tsmiDBI.Text = Apq.GlobalObject.UILang["数据库列表(&I)"];
			tsmiExit.Text = Apq.GlobalObject.UILang["退出(&X)"];

			tsmiTool.Text = Apq.GlobalObject.UILang["工具(&T)"];
			tsmiOption.Text = Apq.GlobalObject.UILang["选项(&O)"];
			tsmiUILang.Text = Apq.GlobalObject.UILang["语言(&L)"];
			tsmiRSAKey.Text = Apq.GlobalObject.UILang["RSA密钥对(&R)"];
			tsmiDES.Text = Apq.GlobalObject.UILang["DES加解密(&D)"];
			tsmiCryptCS.Text = Apq.GlobalObject.UILang["DB连接加解密(&C)"];
			tsmiRandom.Text = Apq.GlobalObject.UILang["随机串生成器(&R)"];

			tsmiWindow.Text = Apq.GlobalObject.UILang["窗口(&W)"];
			tsmiCloseAll.Text = Apq.GlobalObject.UILang["全部关闭(&L)"];

			tsmiHelp.Text = Apq.GlobalObject.UILang["帮助(&H)"];
			tsmiAbout.Text = Apq.GlobalObject.UILang["关于(&A)"];
			#endregion
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

			#region 添加图标
			//Icon = new Icon(Application.StartupPath + @"\Res\ico\sign125.ico");

			this.tsmiNew.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\New.png");
			#endregion

			tsmiNew_Click(tsmiNew, null);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		#region 主菜单
		//新窗口
		private void tsmiNew_Click(object sender, EventArgs e)
		{
			Forms.DBC childForm = new Forms.DBC();
			childForm.Show(dockPanel1);
		}

		private void tsmiDBS_Click(object sender, EventArgs e)
		{
			DockContent dc = (DockContent)Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Forms.DBS));
			dc.Show(dockPanel1);
		}

		private void tsmiDBI_Click(object sender, EventArgs e)
		{
			DockContent dc = (DockContent)Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Forms.DBI));
			dc.Show(dockPanel1);
		}

		//退出
		private void tsmiExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region 工具
		public void tsmiOption_Click(object sender, EventArgs e)
		{
			ApqDBCManager.Forms.MainOption win = new ApqDBCManager.Forms.MainOption();
			win.ShowDialog(this);
		}

		private void tsmiUILang_Click(object sender, EventArgs e)
		{
			DockContent dc = (DockContent)Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Apq.Windows.Forms.DockForms.UILangCfg));
			dc.Show(dockPanel1);
		}

		private void tsmiRSAKey_Click(object sender, EventArgs e)
		{
			Forms.RSAKey win = new ApqDBCManager.Forms.RSAKey();
			win.Show(dockPanel1);
		}

		private void tsmiDES_Click(object sender, EventArgs e)
		{
			DES r = new DES();
			r.Show(dockPanel1);
		}

		private void tsmiCryptCS_Click(object sender, EventArgs e)
		{
			Forms.CryptCS win = new Forms.CryptCS();
			win.Show(dockPanel1);
		}

		private void tsmiRandom_Click(object sender, EventArgs e)
		{
			Random win = new Random();
			win.Show(dockPanel1);
		}
		#endregion

		#region 窗口
		private void tsmiCloseAll_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (DockContent childForm in dockPanel1.Contents)
				{
					childForm.Close();
				}
			}
			catch { }
		}
		#endregion

		#region 帮助
		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			GlobalObject.AboutBox.Asm = GlobalObject.TheAssembly;
			GlobalObject.AboutBox.ShowDialog(this);
		}
		#endregion
	}
}