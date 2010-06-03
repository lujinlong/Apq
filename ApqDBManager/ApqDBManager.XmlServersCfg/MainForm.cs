﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using ApqDBManager.Customization;

namespace ApqDBManager.XmlServersCfg
{
	public partial class MainForm : Apq.Windows.Forms.ImeForm
	{
		public MainForm()
		{
			InitializeComponent();
			m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
		}

		private int childFormNumber = 0;
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
			return null;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Extender.SetSchema(dockPanel1, Extender.Schema.VS2005);

			#region 读取标题
			string cfgText = GlobalObject.XmlConfigChain[this.GetType(),"Text"];
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

			//menuNew_ItemClick(menuNew, null);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			dockPanel1.SaveAsXml(UserDockPanelConfigFile);
		}

		#region 文件
		//新建
		private void menuNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Forms.ServersEditor childForm = new Forms.ServersEditor();
			// 在显示该窗体前使其成为此 MDI 窗体的子窗体。
			childForm.Text = "窗口" + ++childFormNumber;
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
			Apq.Editor.IFileLoader Editor1 = ActiveMdiChild as Apq.Editor.IFileLoader;
			if (Editor1 != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "配置文件(*.xml)|*.xml|所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					Editor1.SaveAs(saveFileDialog.FileName);
				}
				return;
			}
		}
		//退出
		private void menuExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		#region TextEdit
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
		#endregion

		#region 工具
		private void menuRandom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Random r = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(Random)) as Random;
			if (r == null)
			{
				r = new Random();
			}
			r.Show(dockPanel1);
		}

		private void menuDES_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DES r = new DES();
			r.Show(dockPanel1);
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