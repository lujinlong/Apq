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

		private TreeListViewHelper tlvHelper;
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
			cbContainsChildren.Text = Apq.GlobalObject.UILang["包含子目录"];
			cbContainsFileExt.Text = Apq.GlobalObject.UILang["包含文件扩展名"];

			btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];

			columnHeader1.Text = Apq.GlobalObject.UILang["名称"];
			columnHeader2.Text = Apq.GlobalObject.UILang["大小(B)"];
			columnHeader3.Text = Apq.GlobalObject.UILang["类型"];
			columnHeader5.Text = Apq.GlobalObject.UILang["创建日期"];
			columnHeader4.Text = Apq.GlobalObject.UILang["修改日期"];
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
			tlvHelper = new TreeListViewHelper(treeListView1);

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
				treeListView1.Items.Clear();

				DriveInfo[] fsDrives = DriveInfo.GetDrives();

				foreach (DriveInfo fsDrive in fsDrives)
				{
					string strExt = fsDrive.Name;
					Icon SmallIcon = null;
					Apq.DllImports.Shell32.SHFILEINFO shFileInfo = Apq.DllImports.Shell32.GetFileInfo(strExt, false, ref SmallIcon);

					if (!imgList.Images.ContainsKey(fsDrive.DriveType.ToString()))
					{
						imgList.Images.Add(fsDrive.DriveType.ToString(), SmallIcon);
					}

					TreeListViewItem ndRoot = new TreeListViewItem(fsDrive.Name.Substring(0, 2));
					treeListView1.Items.Add(ndRoot);
					ndRoot.ImageIndex = imgList.Images.IndexOfKey(fsDrive.DriveType.ToString());
					if (fsDrive.IsReady)
					{
						ndRoot.SubItems.Add(fsDrive.TotalSize.ToString("n0"));
					}
					else
					{
						ndRoot.SubItems.Add("0");
					}
					ndRoot.SubItems.Add(shFileInfo.szTypeName);
					ndRoot.SubItems.Add(fsDrive.RootDirectory.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
					ndRoot.SubItems.Add(fsDrive.RootDirectory.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));

					ndRoot.SubItems.Add(fsDrive.Name);
					ndRoot.SubItems.Add(fsDrive.IsReady ? "0" : "-1");
					ndRoot.SubItems.Add("1");//类型{1:Drive,2:Folder,3:File}

					// 添加监视器
					if (fsDrive.IsReady)
					{
						Apq.IO.FsWatcher fsw = new Apq.IO.FsWatcher();
						fsw.FileSystemWatcher.Path = fsDrive.RootDirectory.FullName;
						fsw.FileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime
							| NotifyFilters.DirectoryName
							| NotifyFilters.FileName
							| NotifyFilters.LastWrite
							| NotifyFilters.Size;
						fsw.FileSystemWatcher.IncludeSubdirectories = true;
						fsw.Created += new FileSystemEventHandler(fsw_Created);
						fsw.Renamed += new RenamedEventHandler(fsw_Renamed);
						fsw.Deleted += new FileSystemEventHandler(fsw_Deleted);
						fsw.Changed += new FileSystemEventHandler(fsw_Changed);
						lstFsws.Add(fsw);
						fsw.Start();
					}
				}
			}
			catch { }
		}

		void fsw_Created(object sender, FileSystemEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				try
				{
					TreeListViewItem ndFolder = null;
					string strFolder = e.FullPath;

					strFolder = Path.GetDirectoryName(e.FullPath);
					ndFolder = tlvHelper.FindNodeByFullPath(strFolder);
					if (ndFolder != null)
					{
						treeListView1.BeginUpdate();
						// 首次加载时Load,否则Add
						int HasChildren = Apq.Convert.ChangeType<int>(ndFolder.SubItems[treeListView1.Columns.Count + 1].Text);
						if (HasChildren == 0)
						{
							// 移除子结点，重新添加
							LoadFolders(ndFolder, strFolder, false);
							LoadFiles(ndFolder, strFolder);
						}
						else
						{
							if (File.Exists(e.FullPath))
							{
								AddFile(ndFolder, e.FullPath);
							}
							else if (Directory.Exists(e.FullPath))
							{
								AddFolder(ndFolder, e.FullPath, false);
							}
						}
						treeListView1.EndUpdate();
					}
				}
				catch { }
			});
		}

		void fsw_Deleted(object sender, FileSystemEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				try
				{
					TreeListViewItem ndFound = tlvHelper.FindNodeByFullPath(e.FullPath);

					if (ndFound != null)
					{
						ndFound.Remove();
					}
				}
				catch { }
			});
		}

		void fsw_Renamed(object sender, RenamedEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				try
				{
					TreeListViewItem ndFound = tlvHelper.FindNodeByFullPath(e.OldFullPath);

					if (ndFound != null)
					{
						if (File.Exists(e.FullPath))
						{
							ndFound.Text = e.Name;
						}
						else if (Directory.Exists(e.FullPath))
						{
							ndFound.Text = Path.GetFileName(e.FullPath);
						}
					}
				}
				catch { }
			});
		}

		void fsw_Changed(object sender, FileSystemEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				try
				{
					TreeListViewItem ndFound = tlvHelper.FindNodeByFullPath(e.FullPath);

					if (ndFound != null)
					{
						if (File.Exists(e.FullPath))
						{
							ChangeFile(ndFound, e.FullPath);
						}
						else if (Directory.Exists(e.FullPath))
						{
							ChangeFolder(ndFound, e.FullPath);
						}
					}
				}
				catch { }
			});
		}
		#endregion

		#region treeListView1
		private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = treeListView1.FocusedItem;
			if (node != null)
			{
				tsslStatus.Text = node.FullPath;

				if (node.SubItems[treeListView1.Columns.Count + 1].Text == "0")
				{
					treeListView1.BeginUpdate();
					LoadFolders(node, node.FullPath);
					LoadFiles(node, node.FullPath);
					treeListView1.EndUpdate();
				}
			}
		}

		private void LoadFolders(TreeListViewItem node, string fsFullPath, bool ContainsChildren = false)
		{
			try
			{
				if (!imgList.Images.ContainsKey("文件夹收起"))
				{
					Icon SmallIcon = Apq.DllImports.Shell32.GetFolderIcon(false, false);
					imgList.Images.Add("文件夹收起", SmallIcon);
				}
				if (!imgList.Images.ContainsKey("文件夹展开"))
				{
					Icon SmallIcon = Apq.DllImports.Shell32.GetFolderIcon(false, true);
					imgList.Images.Add("文件夹展开", SmallIcon);
				}

				string[] strChildren = Directory.GetDirectories(fsFullPath + "\\");
				node.SubItems[treeListView1.Columns.Count + 1].Text = strChildren.LongLength > 0 ? "1" : "-1";
				foreach (string strChild in strChildren)
				{
					AddFolder(node, strChild, ContainsChildren);
				}
			}
			catch { }
		}

		private void LoadFiles(TreeListViewItem node, string fsFullPath)
		{
			try
			{
				string[] strChildren = Directory.GetFiles(fsFullPath + "\\");
				foreach (string strChild in strChildren)
				{
					AddFile(node, strChild);
				}
			}
			catch { }
		}

		private void AddFolder(TreeListViewItem node, string fsFullPath, bool ContainsChildren = false)
		{
			DirectoryInfo diChild = new DirectoryInfo(fsFullPath);
			TreeListViewItem ndChild = new TreeListViewItem(diChild.Name);
			node.Items.Add(ndChild);
			ndChild.Checked = node.Checked;
			ndChild.ImageKey = "文件夹收起";
			ndChild.SubItems.Add("0");
			ndChild.SubItems.Add(Apq.GlobalObject.UILang["文件夹"]);
			ndChild.SubItems.Add(diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
			ndChild.SubItems.Add(diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));

			ndChild.SubItems.Add(diChild.FullName);
			ndChild.SubItems.Add("0");
			ndChild.SubItems.Add("2");//类型{1:Drive,2:Folder,3:File}

			if (ContainsChildren)
			{
				LoadFolders(ndChild, ndChild.FullPath, ContainsChildren);
				LoadFiles(ndChild, ndChild.FullPath);
			}
		}

		private void ChangeFolder(TreeListViewItem node, string fsFullPath)
		{
			DirectoryInfo diChild = new DirectoryInfo(fsFullPath);
			node.Text = diChild.Name;
			node.SubItems[3].Text = diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
			node.SubItems[4].Text = diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

			node.SubItems[5].Text = (diChild.FullName);
		}

		private void AddFile(TreeListViewItem node, string fsFullPath)
		{
			FileInfo diChild = new FileInfo(fsFullPath);
			//string strExt = fsDrive.DriveType.ToString();
			string strExt = diChild.Extension.ToLower();
			if (strExt == ".exe")
			{
				strExt = diChild.FullName.ToLower();
			}

			Icon SmallIcon = null;
			Apq.DllImports.Shell32.SHFILEINFO shFileInfo = Apq.DllImports.Shell32.GetFileInfo(strExt, false, ref SmallIcon);

			if (!imgList.Images.ContainsKey(strExt))
			{
				imgList.Images.Add(strExt, SmallIcon);
			}

			TreeListViewItem ndChild = new TreeListViewItem(diChild.Name);
			node.Items.Add(ndChild);
			ndChild.Checked = node.Checked;
			ndChild.ImageIndex = imgList.Images.IndexOfKey(strExt);
			ndChild.SubItems.Add(diChild.Length.ToString("n0"));
			if (strExt.Contains("\\"))
			{
				strExt = "应用程序";
			}
			//ndChild.SubItems.Add(Apq.GlobalObject.UILang[strExt]);
			ndChild.SubItems.Add(shFileInfo.szTypeName);
			ndChild.SubItems.Add(diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
			ndChild.SubItems.Add(diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));

			ndChild.SubItems.Add(diChild.Name);
			ndChild.SubItems.Add("-1");
			ndChild.SubItems.Add("3");//类型{1:Drive,2:Folder,3:File}
		}

		private void ChangeFile(TreeListViewItem node, string fsFullPath)
		{
			FileInfo diChild = new FileInfo(fsFullPath);
			node.Text = diChild.Name;
			node.SubItems[1].Text = diChild.Length.ToString("n0");
			node.SubItems[3].Text = diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
			node.SubItems[4].Text = diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

			node.SubItems[5].Text = (diChild.FullName);
		}

		// 展开文件夹图标
		private void treeListView1_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.Level > 0)
			{
				if (e.Item.ImageKey == "文件夹收起")
				{
					e.Item.ImageKey = "文件夹展开";
				}
			}
		}

		// 收起文件夹图标
		private void treeListView1_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.Level > 0)
			{
				if (e.Item.ImageKey == "文件夹展开")
				{
					e.Item.ImageKey = "文件夹收起";
				}
			}
		}
		#endregion

		public void UIEnable(bool Enable)
		{
			treeListView1.Enabled = Enable;
			txtLook.Enabled = Enable;
			txtReplace.Enabled = Enable;
			cbMatchType.Enabled = Enable;
			txtExt.Enabled = Enable;
			cbContainsFolder.Enabled = Enable;
			cbContainsChildren.Enabled = Enable;
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
			if (treeListView1.CheckedItems.LongLength <= 0)
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
				treeListView1.BeginUpdate();
				for (long i = treeListView1.CheckedItems.LongLength - 1; i >= 0; i--)
				{
					TreeListViewItem node = treeListView1.CheckedItems[i];
					int HasChildren = Apq.Convert.ChangeType<int>(node.SubItems[treeListView1.Columns.Count + 1].Text);
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[treeListView1.Columns.Count + 2].Text);
					if (HasChildren == 0 && Type == 2)
					{
						LoadFolders(node, node.FullPath, cbContainsChildren.Checked);
						LoadFiles(node, node.FullPath);
					}
				}
				treeListView1.EndUpdate();
				foreach (Apq.IO.FsWatcher fsw in lstFsws)
				{
					fsw.Start();
				}

				foreach (TreeListViewItem node in treeListView1.CheckedItems)
				{
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[treeListView1.Columns.Count + 2].Text);
					if (Type == 3) tspb.Maximum++;
				}

				int pbFileCount = 0;
				//+开始处理:深度搜索,从最深文件开始
				foreach (TreeListViewItem node in treeListView1.CheckedItems)
				{
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[treeListView1.Columns.Count + 2].Text);
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