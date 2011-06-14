using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using ApqDBManager.Customization;

namespace ApqDBManager
{
	public partial class MainForm : Apq.Windows.Forms.ImeForm
	{
		public MainForm()
		{
			InitializeComponent();
			m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
		}

		private DeserializeDockContent m_deserializeDockContent;

		/// <summary>
		/// 根据上次退出时保存的界面状态,加载界面时调用
		/// </summary>
		/// <param name="persistString"></param>
		/// <returns></returns>
		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(ApqDBManager.Forms.SqlIns).ToString())
			{
				return GlobalObject.SolutionExplorer;
			}
			else if (persistString == typeof(ApqDBManager.Forms.Favorites).ToString())
			{
				return GlobalObject.Favorites;
			}
			else if (persistString == typeof(ApqDBManager.Forms.ErrList).ToString())
			{
				return GlobalObject.ErrList;
			}
			else
			{
				string[] parsedStrings = persistString.Split(new char[] { ',' });
				if (parsedStrings.Length != 3)
					return null;

				if (parsedStrings[0] != typeof(SqlEdit).ToString())
					return null;

				SqlEdit dummyDoc = new SqlEdit();
				//if (parsedStrings[1] != string.Empty)
				//    dummyDoc.FileName = parsedStrings[1];
				//if (parsedStrings[2] != string.Empty)
				//    dummyDoc.Text = parsedStrings[2];

				return dummyDoc;
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Extender.SetSchema(dockPanel1, Extender.Schema.VS2005);

			#region 读取标题
			string cfgText = GlobalObject.XmlConfigChain[this.GetType(), "Text"];
			if (cfgText != null)
			{
				Text = cfgText;
			}
			#endregion

			#region 添加图标
			//Icon = new Icon(@"Res\ico\sign125.ico");

			this.tsbNew.Image = System.Drawing.Image.FromFile(@"Res\png\File\New.png");
			this.tsbOpen.Image = System.Drawing.Image.FromFile(@"Res\png\File\Open.png");
			this.tsbSave.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			this.tsmiNew.Image = System.Drawing.Image.FromFile(@"Res\png\File\New.png");
			this.tsmiOpen.Image = System.Drawing.Image.FromFile(@"Res\png\File\Open.png");
			this.tsmiSave.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			//this.tsmiUndo.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Undo.png");
			//this.tsmiRedo.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Redo.png");
			//this.tsmiCut.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Cut.png");
			//this.tsmiCopy.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Copy.png");
			//this.tsmiPaste.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Paste.png");
			this.tsmiIndex.Image = System.Drawing.Image.FromFile(@"Res\png\Help.png");
			this.tsmiSearch.Image = System.Drawing.Image.FromFile(@"Res\png\Search.png");
			#endregion

			#region DockPanel.config
			if (File.Exists(GlobalObject.UserDockPanalConfigFile))
			{
				dockPanel1.LoadFromXml(GlobalObject.UserDockPanalConfigFile, m_deserializeDockContent);
			}
			else if (File.Exists(GlobalObject.AsmDockPanalConfigFile))
			{
				dockPanel1.LoadFromXml(GlobalObject.AsmDockPanalConfigFile, m_deserializeDockContent);
			}
			#endregion

			menuSolution_CheckedChanged(sender, null);
			menuFavorites_CheckedChanged(sender, null);
			menuErrList_CheckedChanged(sender, null);

			tsmiNew_Click(tsmiNew, null);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			dockPanel1.SaveAsXml(GlobalObject.UserDockPanalConfigFile);
		}

		#region 文件
		//新建
		private void tsmiNew_Click(object sender, EventArgs e)
		{
			SqlEdit childForm = new SqlEdit();
			childForm.Show(dockPanel1);
			childForm.Activate();
		}
		//打开Sql文件
		private void tsmiOpenSql_Click(object sender, EventArgs e)
		{
			if (!(ActiveMdiChild is SqlEdit))
			{
				tsmiNew_Click(sender, e);
			}

			SqlEdit Editor = ActiveMdiChild as SqlEdit;
			if (Editor != null)
			{
				Editor.SqlEditDoc.Open();
			}
		}
		//保存
		private void tsmiSave_Click(object sender, EventArgs e)
		{
			SqlEdit Editor = ActiveMdiChild as SqlEdit;
			if (Editor != null)
			{
				Editor.SqlEditDoc.Save();
			}
		}
		//另存为
		private void tsmiSaveAs_Click(object sender, EventArgs e)
		{
			SqlEdit Editor = ActiveMdiChild as SqlEdit;
			if (Editor != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					Editor.SqlEditDoc.FileName = saveFileDialog.FileName;
					Editor.SqlEditDoc.Save();
				}
			}
		}
		//退出
		private void tsmiExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region 视图

		private void tsmiToolBar_Click(object sender, EventArgs e)
		{
			tsmiToolBar.Checked = !tsmiToolBar.Checked;
		}

		private void tsmiStatusBar_Click(object sender, EventArgs e)
		{
			tsmiStatusBar.Checked = !tsmiStatusBar.Checked;
		}

		private void tsmiInstances_Click(object sender, EventArgs e)
		{
			tsmiInstances.Checked = !tsmiInstances.Checked;
		}

		private void tsmiFavorites_Click(object sender, EventArgs e)
		{
			tsmiFavorites.Checked = !tsmiFavorites.Checked;
		}

		private void tsmiErrList_Click(object sender, EventArgs e)
		{
			tsmiErrList.Checked = !tsmiErrList.Checked;
		}

		private void tsmiToolBar_CheckedChanged(object sender, EventArgs e)
		{
			toolStrip1.Visible = tsmiTool.Checked;
		}

		private void menuStatusBar_CheckedChanged(object sender, EventArgs e)
		{
			statusStrip1.Visible = tsmiStatusBar.Checked;
		}

		private void menuSolution_CheckedChanged(object sender, EventArgs e)
		{
			if (tsmiInstances.Checked)
				GlobalObject.SolutionExplorer.Show(dockPanel1);
			else
				GlobalObject.SolutionExplorer.Hide();
		}

		private void menuFavorites_CheckedChanged(object sender, EventArgs e)
		{
			if (tsmiFavorites.Checked)
				GlobalObject.Favorites.Show(dockPanel1);
			else
				GlobalObject.Favorites.Hide();
		}

		private void menuErrList_CheckedChanged(object sender, EventArgs e)
		{
			if (tsmiErrList.Checked)
				GlobalObject.ErrList.Show(dockPanel1);
			else
				GlobalObject.ErrList.Hide();
		}
		#endregion

		#region 工具
		public void tsmiOption_Click(object sender, EventArgs e)
		{
			ApqDBManager.Forms.MainOption win = new ApqDBManager.Forms.MainOption();
			win.ShowDialog(this);
		}

		private void tsmiRSAKey_Click(object sender, EventArgs e)
		{
			Forms.RSAKey win = new ApqDBManager.Forms.RSAKey();
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

		private void tsmiFTPFileTrans_Click(object sender, EventArgs e)
		{
			Forms.FTPFileTrans win = new ApqDBManager.Forms.FTPFileTrans();
			win.Show(dockPanel1);
		}

		private void tsmiFTPFileUp_Click(object sender, EventArgs e)
		{
			Forms.FTPFileUp win = new ApqDBManager.Forms.FTPFileUp();
			win.Show(dockPanel1);
		}

		private void tsmiDBServer_Click(object sender, EventArgs e)
		{
			Forms.SrvsMgr.DBServer win = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Forms.SrvsMgr.DBServer)) as Forms.SrvsMgr.DBServer;
			if (win != null)
			{
				win.Show(dockPanel1);
			}
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
		private void tsmiContents_Click(object sender, EventArgs e)
		{

		}

		private void tsmiIndex_Click(object sender, EventArgs e)
		{

		}

		private void tsmiSearch_Click(object sender, EventArgs e)
		{

		}

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