using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using QQUnbindTXZ.Customization;

namespace QQUnbindTXZ
{
	public partial class MainForm : Apq.Windows.Forms.ImeForm
	{
		private int childFormNumber = 1;
		private DeserializeDockContent m_deserializeDockContent;

		public MainForm()
		{
			InitializeComponent();
			m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
		}

		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(SolutionExplorer).ToString())
			{
				return GlobalObject.SolutionExplorer;
			}
			else
			{
				string[] parsedStrings = persistString.Split(new char[] { ',' });
				if (parsedStrings.Length != 3)
					return null;

				if (parsedStrings[0] != typeof(UnBlockTXZ).ToString())
					return null;

				UnBlockTXZ dummyDoc = new UnBlockTXZ();
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

			// 主窗口标题
			Text = "QQHX通知通行证解封";

			#region 添加图标
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

			string configFile = "DockPanel.config";
			if (File.Exists(configFile))
			{
				dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);
			}

			// 打开解封窗口
			menuNew_ItemClick(menuNew, null);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			string configFile = "DockPanel.config";
			dockPanel1.SaveAsXml(configFile);
		}

		#region 文件
		private void menuNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			// 创建此子窗体的一个新实例。
			UnBlockTXZ childForm = new UnBlockTXZ();
			// 在显示该窗体前使其成为此 MDI 窗体的子窗体。
			childForm.Text = "窗口" + childFormNumber++;
			childForm.Show(dockPanel1);
			childForm.Open();
		}

		private void menuOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "文本文件(*.txt)|*.txt|SQL文件(*.sql)|*.sql|所有文件(*.*)|*.*";
				if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					Editor.FileName = openFileDialog.FileName;
					Editor.Open();
				}
			}
		}

		private void menuSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.Save();
			}
		}

		private void menuSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "文本文件(*.txt)|*.txt|SQL文件(*.sql)|*.sql|所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					Editor.FileName = saveFileDialog.FileName;
					Editor.Save();
				}
			}
		}

		private void menuExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		#region 编辑
		private void menuUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.Undo();
			}
		}

		private void menuRedo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.Redo();
			}
		}

		private void menuCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.Copy();
				Editor.Delete();
			}
		}

		private void menuCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.Copy();
			}
		}

		private void menuPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.Paste();
			}
		}

		private void menuSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
			if (Editor != null)
			{
				Editor.SelectAll();
			}
		}

		private void menuReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Apq.Editor.IEditor Editor = ActiveMdiChild as Apq.Editor.IEditor;
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

		private void menuSolution_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			GlobalObject.SolutionExplorer.Show(dockPanel1);
		}
		#endregion

		#region 工具
		private void menuOption_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

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