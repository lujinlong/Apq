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
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Apq_LocalTools
{
	public partial class FSRename : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public FSRename()
		{
			InitializeComponent();
		}

		private Apq.Windows.Forms.TSProgressBarHelper pbHelper;
		private Regex _regex = null;

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["批量重命名"] + " - " + ++FormCount;
			TabText = Text;

			groupBox1.Text = Apq.GlobalObject.UILang["查找和替换"];
			label3.Text = Apq.GlobalObject.UILang["查找串："];
			label6.Text = Apq.GlobalObject.UILang["替换为："];
			label2.Text = Apq.GlobalObject.UILang["匹配方式："];
			label5.Text = Apq.GlobalObject.UILang["文件类型："];
			label4.Text = Apq.GlobalObject.UILang["两种方式均为匹配整个查找串"];

			cbContainsFolder.Text = Apq.GlobalObject.UILang["包含文件夹"];
			cbRecursive.Text = Apq.GlobalObject.UILang["包含子目录"];
			cbContainsFileExt.Text = Apq.GlobalObject.UILang["包含文件扩展名"];

			tsbRefresh.Text = Apq.GlobalObject.UILang["刷新(&F)"];
			btnFind.Text = Apq.GlobalObject.UILang["查找(&F)"];
			btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];

			cbMatchType.Items[0] = Apq.GlobalObject.UILang["普通"];
			cbMatchType.Items[1] = Apq.GlobalObject.UILang["正则表达式"];
		}

		private void TxtEncoding_Load(object sender, EventArgs e)
		{
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			pbHelper = new Apq.Windows.Forms.TSProgressBarHelper(tspb);
			pbHelper.Completed += new Action<ToolStripProgressBar>(pbHelper_Completed);

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

			cbMatchType.SelectedIndex = 0;
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

		private void fsExplorer1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = fsExplorer1.FocusedItem;
			if (node != null)
			{
				tsslStatus.Text = node.FullPath;
			}
		}

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

		void pbHelper_Completed(ToolStripProgressBar obj)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
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

				if (cbMatchType.SelectedIndex == 1)
				{
					_regex = new Regex(txtLook.Text, RegexOptions.IgnoreCase);
				}

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
				pbHelper.SetValue(0);
				tspb.Maximum = 0;

				//将需要移动的文件或文件夹记录到列表(注意:须从最底层开始)
				Dictionary<string, string> lstFiles = new Dictionary<string, string>();
				for (int i = fsExplorer1.Items.Count - 1; i >= 0; i--)
				{//文件夹处理
					TreeListViewItem node = fsExplorer1.Items[i];
					if (node.Checked)
					{
						int Type = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 2].Text);
						if (Type < 3)
						{//文件夹或盘符
							AddChildren(lstFiles, node.FullPath, cbRecursive.Checked);
						}
					}
				}
				for (int i = fsExplorer1.Items.Count - 1; i >= 0; i--)
				{//文件处理
					TreeListViewItem node = fsExplorer1.Items[i];
					if (node.Checked)
					{
						int Type = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 2].Text);
						if (Type == 3)
						{//文件
							AddFile(lstFiles, node.FullPath);
						}
					}
				}

				tspb.Maximum = lstFiles.Count;
				if (lstFiles.Count == 0)
				{
					UIEnable(true);
					btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
				}

				int pbFileCount = 0;
				// 开始处理
				foreach (KeyValuePair<string, string> de in lstFiles)
				{
					try
					{
						if (File.Exists(de.Key))
						{
							File.Move(de.Key, de.Value);
						}
						else
						{
							Directory.Move(de.Key, de.Value);
						}
					}
					catch { }
					finally
					{
						pbHelper.SetValue(++pbFileCount);
					}
				}

				tsslStatus.Text = Apq.GlobalObject.UILang["转换完成！"];
				MessageBox.Show(Apq.GlobalObject.UILang["转换完成！"]);

				// 处理完成,进度条回0
				pbHelper.SetValue(0);
			});
		}
		#endregion

		public void AddFile(Dictionary<string, string> lstFiles, string strFile)
		{
			string strOut = strFile;
			if (Apq.IO.PathHelper.MatchFileExt(strFile, txtExt.Text))
			{
				// 记录移动
				if (!lstFiles.ContainsKey(strFile) && FileReplace(strFile, ref strOut))
				{
					lstFiles.Add(strFile, strOut);
				}
			}
		}

		public void AddChildren(Dictionary<string, string> lstFiles, string strFolder, bool Recursive)
		{
			TreeListViewItem node = fsExplorer1.tlvHelper.FindNodeByFullPath(strFolder);
			int HasChildren = 0;
			if (node != null)
			{
				HasChildren = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 1].Text);
			}

			if (node == null ||
				(node.Checked && HasChildren >= 0)
			)
			{
				string[] aryFolders = Directory.GetDirectories(strFolder + Path.DirectorySeparatorChar);
				foreach (string str in aryFolders)
				{
					TreeListViewItem node1 = fsExplorer1.tlvHelper.FindNodeByFullPath(str);
					if (node1 == null)
					{
						if (Recursive)
						{
							AddChildren(lstFiles, str, Recursive);
						}
					}
					else if (node1.Checked)
					{
						AddChildren(lstFiles, str, Recursive);
					}
				}

				string[] aryFiles = Directory.GetFiles(strFolder + Path.DirectorySeparatorChar);
				foreach (string strFile in aryFiles)
				{
					TreeListViewItem nodeFile = fsExplorer1.tlvHelper.FindNodeByFullPath(strFile);
					if (nodeFile == null || nodeFile.Checked)
					{
						AddFile(lstFiles, strFile);
					}
				}
			}
		}

		/// <summary>
		/// 查找并替换文件名(返回值：是否查找到匹配项)
		/// </summary>
		public bool FileReplace(string strIn, ref string strOut)
		{
			strOut = strIn ?? string.Empty;//默认无替换

			string strSrcFullName = Path.GetFileNameWithoutExtension(strIn);//先取得文件名

			if (cbContainsFileExt.Checked)
			{//按需添加后缀
				strSrcFullName += Path.GetExtension(strIn);
			}
			if (cbContainsFolder.Checked)
			{//按需添加文件夹路径
				strSrcFullName = Path.GetDirectoryName(strIn) + Path.DirectorySeparatorChar + strSrcFullName;
			}

			//替换
			switch (cbMatchType.SelectedIndex)
			{
				case 0:
					if (!strSrcFullName.ToLower().Contains(txtLook.Text.ToLower()))
					{
						return false;//无需替换时直接返回
					}
					strOut = Strings.Replace(strSrcFullName, txtLook.Text, txtReplace.Text, 1, -1, CompareMethod.Text);
					break;
				case 1:
					if (!_regex.IsMatch(strSrcFullName))
					{
						return false;//无需替换时直接返回
					}
					strOut = _regex.Replace(strSrcFullName, txtReplace.Text);
					break;
				default:
					return false;
			}
			if (!cbContainsFileExt.Checked)
			{//按需补充后缀
				strOut += Path.GetExtension(strIn);
			}
			if (!cbContainsFolder.Checked)
			{//按需补充文件夹路径
				strOut = Path.GetDirectoryName(strIn) + Path.DirectorySeparatorChar + strOut;
			}
			return true;
		}

		/// <summary>
		/// 查找并替换文件夹名(返回值：是否查找到匹配项)
		/// </summary>
		public bool FolderReplace(string strIn, ref string strOut)
		{
			strOut = strIn ?? string.Empty;//默认无替换

			//替换
			switch (cbMatchType.SelectedIndex)
			{
				case 0:
					if (!strIn.ToLower().Contains(txtLook.Text.ToLower()))
					{
						return false;//无需替换时直接返回
					}
					strOut = Strings.Replace(strIn, txtLook.Text, txtReplace.Text, 1, -1, CompareMethod.Text);
					break;
				case 1:
					if (!_regex.IsMatch(strIn))
					{
						return false;//无需替换时直接返回
					}
					strOut = _regex.Replace(strIn, txtReplace.Text);
					break;
				default:
					return false;
			}

			return true;
		}

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			LoadData(FormDataSet);
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			if (!fsExplorer1.Focused)
			{
				fsExplorer1.Focus();
			}

			// 找到搜索起始结点
			TreeListViewItem node1 = fsExplorer1.FocusedItem;
			TreeListViewItem node2 = null;//从该结点开始检测
			if (node1 == null)
			{
				if (fsExplorer1.Items.Count > 0)
				{
					node2 = fsExplorer1.Items[0];
				}
			}
			else
			{
				node2 = node1.NextRowItem;
			}

			if (node2 == null)
			{
				DialogResult rDiag = MessageBox.Show(this,
					Apq.GlobalObject.UILang["搜索已到达末结点，是否再从头开始？"],
					Apq.GlobalObject.UILang["查找确认"],
					MessageBoxButtons.YesNo);
				if (rDiag == DialogResult.Yes)
				{
					if (fsExplorer1.FocusedItem != null)
					{
						fsExplorer1.FocusedItem.Focused = false;
					}

					btnFind_Click(sender, e);
				}
				return;
			}

			while (node2 != null)
			{
				int Type = Apq.Convert.ChangeType<int>(node2.SubItems[fsExplorer1.Columns.Count + 2].Text);
				// 加载子结点
				if (Type == 2 && node2.Checked)//此文件夹已勾选
				{
					int HasChildren = Apq.Convert.ChangeType<int>(node2.SubItems[fsExplorer1.Columns.Count + 1].Text);
					if (HasChildren == 0)
					{
						fsExplorer1.LoadChildren(node2, node2.FullPath, cbRecursive.Checked);
					}
				}

				if (Type == 3)
				{//文件
					if (Apq.IO.PathHelper.MatchFileExt(node2.FullPath, txtExt.Text))
					{
						string strOut = string.Empty;
						if (FileReplace(node2.FullPath, ref strOut))
						{
							node2.EnsureVisible();
							node2.Focused = true;
							return;
						}
					}
				}
				else if (cbContainsFolder.Checked)
				{//文件夹
					string strOut = string.Empty;
					if (FolderReplace(node2.FullPath, ref strOut))
					{
						node2.EnsureVisible();
						node2.Focused = true;
						return;
					}
				}

				node2 = node2.NextRowItem;
			}

			DialogResult eDiag = MessageBox.Show(this,
					Apq.GlobalObject.UILang["搜索已到达末结点，是否再从头开始？"],
					Apq.GlobalObject.UILang["查找确认"],
					MessageBoxButtons.YesNo);
			if (eDiag == DialogResult.Yes)
			{
				if (fsExplorer1.FocusedItem != null)
				{
					fsExplorer1.FocusedItem.Focused = false;
				}

				btnFind_Click(sender, e);
			}
		}
	}
}