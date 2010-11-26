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
		private static string AsmDockPanelConfigFile = "DockPanel.config";
		private static string UserDockPanelConfigFile = "DockPanel." + Environment.UserName + ".config";

		/// <summary>
		/// 根据上次退出时保存的界面状态,加载界面时调用
		/// </summary>
		/// <param name="persistString"></param>
		/// <returns></returns>
		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(ApqDBManager.Forms.SolutionExplorer).ToString())
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
				if (parsedStrings[1] != string.Empty)
					dummyDoc.FileName = parsedStrings[1];
				if (parsedStrings[2] != string.Empty)
					dummyDoc.Text = parsedStrings[2];

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

			this.blbiNew.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\New.png");
			this.blbiOpen.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Open.png");
			this.blbiSave.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			this.menuNew.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\New.png");
			this.menuOpen.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Open.png");
			this.menuSave.Glyph = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			this.menuUndo.Glyph = System.Drawing.Image.FromFile(@"Res\png\Editor\Undo.png");
			this.menuRedo.Glyph = System.Drawing.Image.FromFile(@"Res\png\Editor\Redo.png");
			this.menuCut.Glyph = System.Drawing.Image.FromFile(@"Res\png\Editor\Cut.png");
			this.menuCopy.Glyph = System.Drawing.Image.FromFile(@"Res\png\Editor\Copy.png");
			this.menuPaste.Glyph = System.Drawing.Image.FromFile(@"Res\png\Editor\Paste.png");
			this.menuIndex.Glyph = System.Drawing.Image.FromFile(@"Res\png\Help.png");
			this.menuSearch.Glyph = System.Drawing.Image.FromFile(@"Res\png\Search.png");
			#endregion

			#region DockPanel.config
			if (File.Exists(UserDockPanelConfigFile))
			{
				dockPanel1.LoadFromXml(UserDockPanelConfigFile, m_deserializeDockContent);
			}
			else if (File.Exists(AsmDockPanelConfigFile))
			{
				dockPanel1.LoadFromXml(AsmDockPanelConfigFile, m_deserializeDockContent);
			}
			#endregion

			menuSolution_CheckedChanged(sender, null);
			menuFavorites_CheckedChanged(sender, null);
			menuErrList_CheckedChanged(sender, null);

			menuNew_ItemClick(menuNew, null);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			dockPanel1.SaveAsXml(UserDockPanelConfigFile);
		}

		#region 文件
		//新建
		private void menuNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SqlEdit childForm = new SqlEdit();
			childForm.Show(dockPanel1);
			childForm.Activate();
		}
		//打开
		private void menuOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!(ActiveMdiChild is Apq.Editor.IFileLoader))
			{
				menuNew_ItemClick(sender, e);
			}

			Apq.Editor.IFileLoader Editor = ActiveMdiChild as Apq.Editor.IFileLoader;
			if (Editor != null)
			{
				Editor.Open();
			}
		}
		//保存
		private void menuSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IFileLoader Editor = ActiveMdiChild as Apq.Editor.IFileLoader;
			if (Editor != null)
			{
				Editor.Save();
			}
		}
		//另存为
		private void menuSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IFileLoader Editor = ActiveMdiChild as Apq.Editor.IFileLoader;
			if (Editor != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "文本文件(*.txt;*.sql)|*.txt;*.sql|所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					Editor.FileName = saveFileDialog.FileName;
					Editor.Save();
				}
			}
		}
		//退出
		private void menuExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		#region 编辑
		private void menuUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.Undo();
			}
		}

		private void menuRedo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.Redo();
			}
		}

		private void menuCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.Copy();
				Editor.Delete();
			}
		}

		private void menuCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.Copy();
			}
		}

		private void menuPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.Paste();
			}
		}

		private void menuSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.SelectAll();
			}
		}

		private void menuReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.ITextEditor Editor = ActiveMdiChild as Apq.Editor.ITextEditor;
			if (Editor != null)
			{
				Editor.Reverse();
			}
		}
		#endregion

		#region 视图
		private void menuToolBar_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			bar1.Visible = menuToolBar.Checked;
		}

		private void menuStatusBar_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			bar3.Visible = menuStatusBar.Checked;
		}

		private void menuSolution_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (menuSolution.Checked)
				GlobalObject.SolutionExplorer.Show(dockPanel1);
			else
				GlobalObject.SolutionExplorer.Hide();
		}

		private void menuFavorites_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (menuFavorites.Checked)
				GlobalObject.Favorites.Show(dockPanel1);
			else
				GlobalObject.Favorites.Hide();
		}

		private void menuErrList_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (menuErrList.Checked)
				GlobalObject.ErrList.Show(dockPanel1);
			else
				GlobalObject.ErrList.Hide();
		}
		#endregion

		#region 工具
		private void menuOption_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			GlobalObject.MainOption.ShowDialog(this);
		}

		private void menuDES_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DES r = new DES();
			r.Show(dockPanel1);
		}

		private void menuRandom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Random win = new Random();
			win.Show(dockPanel1);
		}

		private void menuFileUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.FileUp win = new Forms.FileUp();
			win.Show(dockPanel1);
		}

		private void menuFileTrans_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.FileTrans win = new ApqDBManager.Forms.FileTrans();
			win.Show(dockPanel1);
		}

		private void menuFTP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.FTPFileTrans win = new ApqDBManager.Forms.FTPFileTrans();
			win.Show(dockPanel1);
		}

		private void menuFTPFileUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.FTPFileUp win = new ApqDBManager.Forms.FTPFileUp();
			win.Show(dockPanel1);
		}

		private void menuDBC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.CryptCS win = new Forms.CryptCS();
			win.Show(dockPanel1);
		}

		private void menuRSAKey_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.RSAKey win = new ApqDBManager.Forms.RSAKey();
			win.Show(dockPanel1);
		}
		#endregion

		#region 窗口
		private void menuCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}

		private void menuNewApp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			System.Diagnostics.Process.Start(Application.ExecutablePath);
		}
		#endregion

		#region 帮助
		private void menuContents_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		private void menuIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		private void menuSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}

		private void menuAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}
		#endregion
	}
}