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
using Apq_LocalTools.Forms;
using Apq.TreeListView;
using System.IO;
using org.mozilla.intl.chardet;

namespace Apq_LocalTools
{
	public partial class FSRename : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public FSRename()
		{
			InitializeComponent();
		}

		private List<Apq.IO.FsWatcher> lstFsws = new List<Apq.IO.FsWatcher>();

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["批量重命名"] + " - " + ++FormCount;
			TabText = Text;

			groupBox1.Text = Apq.GlobalObject.UILang["查找和替换"];
			label3.Text = Apq.GlobalObject.UILang["查找内容："];
			label6.Text = Apq.GlobalObject.UILang["替换为："];
			label2.Text = Apq.GlobalObject.UILang["匹配方式："];
			label5.Text = Apq.GlobalObject.UILang["文件类型："];
			label4.Text = Apq.GlobalObject.UILang["两种方式均为匹配整串"];

			cbContainsFolder.Text = Apq.GlobalObject.UILang["包含文件夹"];
			cbRecursive.Text = Apq.GlobalObject.UILang["包含子目录"];
			cbContainsFileExt.Text = Apq.GlobalObject.UILang["包含文件扩展名"];

			btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
		}

		private void TxtEncoding_Load(object sender, EventArgs e)
		{
			cbMatchType.SelectedIndex = 0;

			TransFinished += new EventHandler(FSRename_TransFinished);
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			#region 数据库连接
			#endregion
		}
		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		public override void InitData(DataSet ds)
		{
			#region 准备数据集结构
			#endregion

			#region 加载所有字典表
			#endregion
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
			try
			{
				// 为TreeListView添加根结点
				fsExplorer1.LoadDrives();
			}
			catch { }
		}
		#endregion

		#region treeListView1
		private void fsExplorer1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = fsExplorer1.FocusedItem;
			if (node != null)
			{
				tsslStatus.Text = node.FullPath;
			}
		}
		#endregion

		public void UIEnable(bool Enable)
		{
			fsExplorer1.Enabled = Enable;
			txtLook.Enabled = Enable;
			txtReplace.Enabled = Enable;
			cbMatchType.Enabled = Enable;
			txtExt.Enabled = Enable;
			cbContainsFolder.Enabled = Enable;
			cbRecursive.Enabled = Enable;
			cbContainsFileExt.Enabled = Enable;

			Cursor = Enable ? Cursors.Default : Cursors.WaitCursor;
		}

		void FSRename_TransFinished(object sender, EventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
			});
		}

		public event EventHandler TransFinished;
		/// <summary>
		/// 设置进度条的当前值，完成后引发“TransFinished”事件
		/// </summary>
		/// <param name="Value"></param>
		public void tspb_SetValue(int Value)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				tspb.Value = Value;

				if (tspb.Maximum > 0 && tspb.Value >= tspb.Maximum)
				{
					TransFinished.Invoke(tspb, new EventArgs());
				}
			});
		}

		private void TxtEncoding_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void TxtEncoding_Activated(object sender, EventArgs e)
		{
		}

		private void TxtEncoding_Deactivate(object sender, EventArgs e)
		{

		}

		private void btnTrans_Click(object sender, EventArgs e)
		{
			if (fsExplorer1.CheckedItems.LongLength <= 0)
			{
				return;
			}

			Apq.Threading.Thread.Abort(MainBackThread);

			if (btnTrans.Text == Apq.GlobalObject.UILang["开始替换(&H)"])
			{
				UIEnable(false);
				btnTrans.Text = Apq.GlobalObject.UILang["取消(&C)"];

				MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(Work));
			}
			else
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
			}
		}

		#region 工作线程
		private void Work()
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				tsslStatus.Text = Apq.GlobalObject.UILang["开始处理..."];
				tspb_SetValue(0);
				tspb.Maximum = 0;

				// 补全资源管理器
				foreach (Apq.IO.FsWatcher fsw in lstFsws)
				{
					fsw.Stop();
				}
				fsExplorer1.BeginUpdate();
				for (long i = fsExplorer1.CheckedItems.LongLength - 1; i >= 0; i--)
				{
					TreeListViewItem node = fsExplorer1.CheckedItems[i];
					int HasChildren = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 1].Text);
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 2].Text);
					if (HasChildren == 0 && Type == 2)
					{
						fsExplorer1.LoadChildren(node, node.FullPath, cbRecursive.Checked);
					}
				}
				fsExplorer1.EndUpdate();
				foreach (Apq.IO.FsWatcher fsw in lstFsws)
				{
					fsw.Start();
				}

				foreach (TreeListViewItem node in fsExplorer1.CheckedItems)
				{
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 2].Text);
					if (Type == 3) tspb.Maximum++;
				}

				int pbFileCount = 0;
				//+开始处理:深度搜索,从最深文件开始
				foreach (TreeListViewItem node in fsExplorer1.CheckedItems)
				{
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 2].Text);
					if (Type == 3 || Type == 2)
					{
						// 匹配文件类型
						bool isMatchExt = false;
						string[] strExts = txtExt.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
						foreach (string strExt in strExts)
						{
							if (strExt == "*.*")
							{
								isMatchExt = true;
								break;
							}
							if (Type == 2)
							{
								isMatchExt = cbContainsFolder.Checked;
								break;
							}
							if (Path.GetExtension(node.Text).Equals(Path.GetExtension(strExt), StringComparison.OrdinalIgnoreCase))
							{
								isMatchExt = true;
								break;
							}
						}

						if (isMatchExt)
						{
							if (cbMatchType.SelectedIndex == 0)
							{// 普通替换
								string strOldName = cbContainsFileExt.Checked ?
									Path.GetFileName(node.FullPath) :
									Path.GetFileNameWithoutExtension(node.FullPath);
								string strNewName = strOldName.ToLower().Replace(
									txtLook.Text.Trim().ToLower(),
									txtReplace.Text.Trim());

								string strFullPath = Path.Combine(
									Path.GetDirectoryName(node.FullPath),
									strNewName);
								if (!cbContainsFileExt.Checked)
								{
									strFullPath += Path.GetExtension(node.FullPath);
								}
							}
							if (cbMatchType.SelectedIndex == 1)
							{// 正则替换
							}
						}

						tspb_SetValue(++pbFileCount);
						Application.DoEvents();
					}
				}

				// 处理完成,进度条回0
				tspb_SetValue(0);
				tsslStatus.Text = Apq.GlobalObject.UILang["替换完成！"];
				MessageBox.Show(Apq.GlobalObject.UILang["替换完成！"]);
			});
		}
		#endregion
	}
}