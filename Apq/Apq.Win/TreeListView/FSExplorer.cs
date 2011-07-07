using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Apq.DllImports;

namespace Apq.TreeListView
{
	/// <summary>
	/// 基于TreeListView的资源浏览器
	/// </summary>
	public class FSExplorer : System.Windows.Forms.TreeListView
	{
		private TreeListViewHelper _tlvHelper;
		/// <summary>
		/// 获取关联的TreeListView助手
		/// </summary>
		public TreeListViewHelper tlvHelper
		{
			get
			{
				if (_tlvHelper == null)
				{
					_tlvHelper = new TreeListViewHelper(this);
				}
				return _tlvHelper;
			}
		}

		private System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer _Comparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
		private ImageList _imgList
		{
			get { return Apq.Windows.Forms.IconChache.ImgList; }
		}
		private List<Apq.IO.FsWatcher> lstFsws = new List<Apq.IO.FsWatcher>();
		/// <summary>
		/// 基于TreeListView的资源浏览器
		/// 隐藏列依次为:
		///		完整路径
		///		HasChildren{-1:无,0:未知,1:有}
		///		类型{1:Drive,2:Folder,3:File}
		/// </summary>
		public FSExplorer()
		{
			_imgList.ColorDepth = ColorDepth.Depth32Bit;
			_imgList.ImageSize = new Size(16, 16);
			_imgList.TransparentColor = Color.Transparent;

			// 添加显示的列
			Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new System.Windows.Forms.ColumnHeader(),
            new System.Windows.Forms.ColumnHeader(),
            new System.Windows.Forms.ColumnHeader(),
            new System.Windows.Forms.ColumnHeader(),
            new System.Windows.Forms.ColumnHeader()});

			Columns[0].Name = "Name";
			Columns[0].Text = Apq.GlobalObject.UILang["名称"];
			Columns[0].Width = 400;

			Columns[1].Name = "Size";
			Columns[1].Text = Apq.GlobalObject.UILang["大小(B)"];
			Columns[1].Width = 150;
			Columns[1].TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

			Columns[2].Name = "Type";
			Columns[2].Text = Apq.GlobalObject.UILang["类型"];
			Columns[2].Width = 100;

			Columns[3].Name = "CreationTime";
			Columns[3].Text = Apq.GlobalObject.UILang["创建时间"];
			Columns[3].Width = 140;

			Columns[4].Name = "LastWriteTime";
			Columns[4].Text = Apq.GlobalObject.UILang["修改时间"];
			Columns[4].Width = 140;

			_Comparer1.Column = 2;
			_Comparer1.SortOrder = System.Windows.Forms.SortOrder.None;

			Comparer = _Comparer1;
			SmallImageList = _imgList;

			// 事件处理
			BeforeExpand += new TreeListViewCancelEventHandler(FSExplorer_BeforeExpand);
			BeforeCollapse += new TreeListViewCancelEventHandler(FSExplorer_BeforeCollapse);
			SelectedIndexChanged += new EventHandler(FSExplorer_SelectedIndexChanged);
		}

		#region 内置事件处理
		void fsw_Created(object sender, FileSystemEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<FSExplorer>(this, this, delegate(FSExplorer ctrl)
			{
				try
				{
					TreeListViewItem ndFolder = null;
					string strFolder = e.FullPath;

					strFolder = Path.GetDirectoryName(e.FullPath);
					ndFolder = tlvHelper.FindNodeByFullPath(strFolder);
					if (ndFolder != null)
					{
						// 首次加载时Load,否则Add
						int HasChildren = Apq.Convert.ChangeType<int>(ndFolder.SubItems[Columns.Count + 1].Text);
						if (HasChildren == 0)
						{
							LoadChildren(ndFolder, strFolder, false);
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
					}
				}
				catch { }
			});
		}

		void fsw_Deleted(object sender, FileSystemEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<FSExplorer>(this, this, delegate(FSExplorer ctrl)
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
			Apq.Windows.Delegates.Action_UI<FSExplorer>(this, this, delegate(FSExplorer ctrl)
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
			Apq.Windows.Delegates.Action_UI<FSExplorer>(this, this, delegate(FSExplorer ctrl)
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

		void FSExplorer_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.Level > 0)
			{
				if (e.Item.ImageKey == "文件夹收起")
				{
					Shell32.SHFILEINFO shFolderInfo = new Shell32.SHFILEINFO();
					Apq.Windows.Forms.IconChache.GetFileSystemIcon(e.Item.FullPath, ref shFolderInfo, true);
					e.Item.ImageKey = "文件夹展开";
				}
			}
		}

		void FSExplorer_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.Level > 0)
			{
				if (e.Item.ImageKey == "文件夹展开")
				{
					e.Item.ImageKey = "文件夹收起";
				}
			}
		}

		void FSExplorer_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = FocusedItem;
			if (node != null && node.SubItems[Columns.Count + 1].Text == "0")
			{
				LoadChildren(node, node.FullPath);
			}
		}
		#endregion

		#region 显示文件系统
		public void LoadDrives()
		{
			Apq.Windows.Delegates.Action_UI<FSExplorer>(this, this, delegate(FSExplorer ctrl)
			{
				// 为TreeListView添加根结点
				Items.Clear();
				//Items.SortOrder = SortOrder.None;

				DriveInfo[] fsDrives = DriveInfo.GetDrives();

				foreach (DriveInfo fsDrive in fsDrives)
				{
					string strExt = fsDrive.Name;

					TreeListViewItem ndRoot = new TreeListViewItem(fsDrive.Name.Substring(0, 2));
					Items.Add(ndRoot);
					Apq.DllImports.Shell32.SHFILEINFO shFileInfo = new DllImports.Shell32.SHFILEINFO();
					Icon SmallIcon = Apq.Windows.Forms.IconChache.GetFileSystemIcon(strExt, ref shFileInfo);
					ndRoot.ImageIndex = _imgList.Images.IndexOfKey(shFileInfo.szTypeName);
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

					// 排序
					//try { Items.Sort(false); }
					//catch { }

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
			});
		}

		public void LoadChildren(TreeListViewItem node, string fsFullPath, bool Recursive = false)
		{
			string[] strChildren = Directory.GetDirectories(fsFullPath + "\\");
			if (strChildren.LongLength > 0)
			{
				node.SubItems[Columns.Count + 1].Text = "1";
			}
			foreach (string strChild in strChildren)
			{
				AddFolder(node, strChild, Recursive);
			}

			strChildren = Directory.GetFiles(fsFullPath + "\\");
			if (node.SubItems[Columns.Count + 1].Text == "0")
			{
				node.SubItems[Columns.Count + 1].Text = strChildren.LongLength > 0 ? "1" : "-1";
			}
			foreach (string strChild in strChildren)
			{
				AddFile(node, strChild);
			}

			// 排序
			//try { node.Items.Sort(Recursive); }
			//catch { }
		}

		public void AddFolder(TreeListViewItem node, string fsFullPath, bool ContainsChildren = false)
		{
			try
			{
				DirectoryInfo diChild = new DirectoryInfo(fsFullPath);
				TreeListViewItem ndChild = new TreeListViewItem(diChild.Name);
				node.Items.Add(ndChild);
				ndChild.Checked = node.Checked;
				Apq.DllImports.Shell32.SHFILEINFO shFolderInfo = new DllImports.Shell32.SHFILEINFO();
				Apq.Windows.Forms.IconChache.GetFileSystemIcon(fsFullPath, ref shFolderInfo);
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
					LoadChildren(ndChild, ndChild.FullPath, ContainsChildren);
				}
			}
			catch { }
		}

		public void ChangeFolder(TreeListViewItem node, string fsFullPath)
		{
			DirectoryInfo diChild = new DirectoryInfo(fsFullPath);
			node.Text = diChild.Name;
			node.SubItems[3].Text = diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
			node.SubItems[4].Text = diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

			node.SubItems[5].Text = (diChild.FullName);
		}

		public void AddFile(TreeListViewItem node, string fsFullPath)
		{
			try
			{
				FileInfo diChild = new FileInfo(fsFullPath);
				string strExt = diChild.Extension.ToLower();
				if (strExt == ".exe")
				{
					strExt = diChild.FullName.ToLower();
				}

				Shell32.SHFILEINFO shFileInfo = new Shell32.SHFILEINFO();
				Icon SmallIcon = Apq.Windows.Forms.IconChache.GetFileSystemIcon(diChild.FullName, ref shFileInfo);

				TreeListViewItem ndChild = new TreeListViewItem(diChild.Name);
				node.Items.Add(ndChild);
				ndChild.Checked = node.Checked;
				ndChild.ImageIndex = _imgList.Images.IndexOfKey(strExt);
				ndChild.SubItems.Add(diChild.Length.ToString("n0"));
				if (strExt.Contains("\\"))
				{
					strExt = "应用程序";
				}
				ndChild.SubItems.Add(shFileInfo.szTypeName);
				ndChild.SubItems.Add(diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
				ndChild.SubItems.Add(diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));

				ndChild.SubItems.Add(diChild.Name);
				ndChild.SubItems.Add("-1");
				ndChild.SubItems.Add("3");//类型{1:Drive,2:Folder,3:File}
			}
			catch { }
		}

		public void ChangeFile(TreeListViewItem node, string fsFullPath)
		{
			FileInfo diChild = new FileInfo(fsFullPath);
			node.Text = diChild.Name;
			node.SubItems[1].Text = diChild.Length.ToString("n0");
			node.SubItems[3].Text = diChild.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
			node.SubItems[4].Text = diChild.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

			node.SubItems[5].Text = (diChild.FullName);
		}
		#endregion

		#region 公开方法
		/// <summary>
		/// 文件系统监视开始
		/// </summary>
		public void FSWatcherStart()
		{
			foreach (Apq.IO.FsWatcher fsw in lstFsws)
			{
				fsw.Start();
			}
		}

		/// <summary>
		/// 文件系统监视停止
		/// </summary>
		public void FSWatcherStop()
		{
			foreach (Apq.IO.FsWatcher fsw in lstFsws)
			{
				fsw.Stop();
			}
		}
		#endregion
	}
}
