using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using System.IO;
using DevExpress.XtraTreeList;
using System.Threading;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using System.Data.Common;
using DevExpress.XtraEditors;
using Apq.TreeListView;

namespace ApqDBManager.Forms
{
	public partial class FTPFileUp : Apq.Windows.Forms.DockForm
	{
		public FTPFileUp()
		{
			InitializeComponent();
		}

		private TreeListViewHelper tlvHelper;
		private Thread LoadFileThread;

		private Apq.XSD.Explorer dsExplorer = new Apq.XSD.Explorer();

		#region UI线程
		private void FileUp_Load(object sender, EventArgs e)
		{
			if (!Apq.Convert.HasMean(txtDBFolder_Up.Text))
			{
				txtDBFolder_Up.Text = GlobalObject.XmlConfigChain[this.GetType(), "DBFolder_Up"];
			}

			// 获取服务器列表
			_Sqls = GlobalObject.Sqls.Copy() as ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD;

			tsbRefresh_Click(sender, null);
		}

		private void FileUp_Activated(object sender, EventArgs e)
		{
			// 设置服务器和错误列表
			GlobalObject.SolutionExplorer.SetServers(_Sqls);
			GlobalObject.ErrList.Set_ErrList(_UI);
		}

		private void FileUp_Deactivate(object sender, EventArgs e)
		{
			GlobalObject.SolutionExplorer.SaveState2XSD();
		}

		private void txtDBFolder_Up_TextChanged(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(txtDBFolder_Up.Text))
			{
				GlobalObject.XmlConfigChain[this.GetType(), "DBFolder_Up"] = txtDBFolder_Up.Text;
			}
		}
		#endregion

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			tlvHelper = new TreeListViewHelper(treeListView1);
		}
		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		public override void InitData(DataSet ds)
		{
			dsExplorer.Init_luType();
		}
		#endregion

		#region 本地浏览
		// 刷新
		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			LoadLogicDrivers();
		}

		#region treeListView1
		/// <summary>
		/// 加载磁盘
		/// </summary>
		public void LoadLogicDrivers()
		{
			try
			{
				treeListView1.Items.Clear();
				string[] root = Directory.GetLogicalDrives();

				foreach (string s in root)
				{
					TreeListViewItem node = new TreeListViewItem(s);
					treeListView1.Items.Add(node);
					node.SubItems.Add(dsExplorer.luType.FindByType(0).TypeCaption);
					node.SubItems.Add("0");

					node.SubItems.Add(s);

					node.ImageIndex = 0;
				}
				tsslStatus.Text = "加载成功";
			}
			catch { }
		}
		/// <summary>
		/// 加载子级文件夹
		/// </summary>
		public void LoadFolders(TreeListViewItem parent, string FullName)
		{
			DirectoryInfo di;
			try
			{
				string[] root = Directory.GetDirectories(FullName);
				foreach (string s in root)
				{
					try
					{
						di = new DirectoryInfo(s);
						TreeListViewItem node = new TreeListViewItem(di.Name);
						parent.Items.Add(node);
						node.SubItems.Add(dsExplorer.luType.FindByType(1).TypeCaption);
						node.SubItems.Add("0");

						node.SubItems.Add(s);

						node.ImageIndex = 1;
						node.Checked = parent.Checked;
					}
					catch { }
				}
			}
			catch { }
		}
		/// <summary>
		/// 加载子级文件
		/// </summary>
		public void LoadFiles(TreeListViewItem parent, string FullName)
		{
			FileInfo fi;
			try
			{
				string[] root = Directory.GetFiles(FullName);
				foreach (string s in root)
				{
					fi = new FileInfo(s);
					TreeListViewItem node = new TreeListViewItem(fi.Name);
					parent.Items.Add(node);
					node.SubItems.Add(dsExplorer.luType.FindByType(2).TypeCaption);
					node.SubItems.Add(fi.Length.ToString());

					node.SubItems.Add(s);

					node.ImageIndex = 3;
					node.Checked = parent.Checked;
				}
			}
			catch { }
		}

		private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = treeListView1.FocusedItem;
			StatusBarDisplayText(node);

			// 逐渐加载下级
			if (node != null && node.ImageIndex < 3 && node.Items.Count == 0)
			{
				string FullName = node.SubItems[node.ListView.Columns.Count].Text;
				LoadFolders(node, FullName);
				LoadFiles(node, FullName);
			}
		}

		private void treeListView1_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.ImageIndex == 2) e.Item.ImageIndex = 1;
		}

		private void treeListView1_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.ImageIndex == 1) e.Item.ImageIndex = 2;
		}

		private void StatusBarDisplayText(TreeListViewItem node)
		{
			if (node != null)
			{
				tsslStatus.Text = node.SubItems[node.ListView.Columns.Count].Text;
			}
			else
			{
				tsslStatus.Text = string.Empty;
			}
		}

		#endregion
		#endregion

		#region 上传
		private List<Thread> thds = new List<Thread>();

		#region ResultTableLocks
		private Dictionary<string, object> _ResultTableLocks;
		/// <summary>
		/// 结果表格控件锁对象集合,名称为ServerID(供lock使用)
		/// </summary>
		public Dictionary<string, object> ResultTableLocks
		{
			get
			{
				if (_ResultTableLocks == null)
				{
					_ResultTableLocks = new Dictionary<string, object>();
				}
				return _ResultTableLocks;
			}
		}

		/// <summary>
		/// 获取控件锁
		/// </summary>
		/// <returns></returns>
		public object GetLock(string ServerID)
		{
			lock (ResultTableLocks)
			{
				if (!ResultTableLocks.ContainsKey(ServerID))
				{
					object obj = new object();
					ResultTableLocks.Add(ServerID, obj);
				}
				return ResultTableLocks[ServerID];
			}
		}
		#endregion

		// UI
		private void UIEnable(bool enabled)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				txtDBFolder_Up.Enabled = enabled;
				tsbRefresh.Enabled = enabled;
				tsbUp.Enabled = enabled;
				treeListView1.Enabled = enabled;
			});
		}

		private void tsbUp_Click(object sender, EventArgs e)
		{
			UIEnable(false);

			// 先终止上次线程
			Apq.Threading.Thread.Abort(MainBackThread);
			Apq.Threading.Thread.Abort(LoadFileThread);

			LoadFileThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(LoadFileStream));
		}

		private void LoadFileStream()
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				tsslStateFileUp.Text = "正在加载文件...";
				_UI.ErrList.Clear();
				_UI.ErrList.AcceptChanges();
			});

			// 读取文件
			dsExplorer.dtExplorer.Rows.Clear();
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				LoadFileStream(root);
			}

			MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(MainBackThread_Start));
		}

		private void LoadFileStream(TreeListViewItem node)
		{
			if (node.Checked && node.SubItems[1].Text == dsExplorer.luType.FindByType(2).TypeCaption)
			{
				Apq.XSD.Explorer.dtExplorerRow dr = dsExplorer.dtExplorer.NewdtExplorerRow();
				dr.ID = dsExplorer.dtExplorer.Rows.Count + 1;
				dr.FullName = node.SubItems[node.ListView.Columns.Count].Text;
				dr.FileName = node.SubItems[0].Text;
				dr.Size = Apq.Convert.ChangeType<long>(node.SubItems[2].Text);
				dr.Loaded = true;
				dsExplorer.dtExplorer.Rows.Add(dr);

				System.IO.FileStream fs = System.IO.File.OpenRead(dr.FullName);
				dr.bFile = Apq.IO.FileSystem.ReadFully(fs);
			}

			foreach (TreeListViewItem child in node.Items)
			{
				LoadFileStream(child);
			}
		}

		#region 后台线程
		/// <summary>
		/// 主后台线程入口,启动子线程/自己完成工作
		/// </summary>
		private void MainBackThread_Start()
		{
			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "CheckState = 1 AND SqlID > 1";
			if (dv.Count == 0)
			{
				UIEnable(true);
				return;
			}

			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				_UI.ErrList.Clear();
				_UI.ErrList.AcceptChanges();
			});
			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				dr["Err"] = false;
				dr["IsReadyToGo"] = false;
			}
			foreach (DataRowView drv in dv)
			{
				drv["IsReadyToGo"] = true;
			}
			_Sqls.SqlInstance.AcceptChanges();

			Workers_Stop();
			Workers_Start();
		}

		/// <summary>
		/// 停止上次工作(主后台线程或其子线程 调用)
		/// </summary>
		private void Workers_Stop()
		{
			object rtLock0 = GetLock("0");
			lock (rtLock0)
			{
				Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
				{
					tspb.Value = 0;
					tsslStateFileUp.Text = "停止中...";
					Application.DoEvents();
				});
				thds.Clear();
			}
			foreach (Thread t in thds)
			{
				Apq.Threading.Thread.Abort(t);
			}
			lock (rtLock0)
			{
				Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
				{
					tsslStateFileUp.Text = "准备完成,开始处理...";
				});
			}
		}

		/// <summary>
		/// 开始一次工作(主后台线程调用)
		/// </summary>
		private void Workers_Start()
		{
			object rtLock0 = GetLock("0");
			try
			{
				#region 获取服务器列表
				DataView dv = new DataView(_Sqls.SqlInstance);
				dv.RowFilter = "CheckState = 1 AND SqlID > 1";
				#endregion

				#region 开始
				lock (rtLock0)
				{
					Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
					{
						tsslStateFileUp.Text = "上传中...";
						tspb.Maximum = dv.Count;
					});
				}

				foreach (DataRowView drv in dv)
				{
					ParameterizedThreadStart pts = new ParameterizedThreadStart(Worker_Start);
					try
					{
						Thread t = Apq.Threading.Thread.StartNewThread(pts, drv["ID"]);
						if (!Convert.IsDBNull(drv["Name"]))
						{
							t.Name = drv["Name"].ToString();
						}
						thds.Add(t);
					}
					catch (Exception ex)
					{
						Apq.GlobalObject.ApqLog.Warn("启动线程:", ex);
					}
				}
				#endregion
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				Apq.GlobalObject.ApqLog.Warn("执行中发生异常:", ex);
			}
		}

		/// <summary>
		/// 到一个服务器执行并显示结果[多线程入口]
		/// </summary>
		/// <param name="ServerID"></param>
		private void Worker_Start(object ServerID)
		{
			object rtLock0 = GetLock("0");
			object rtLock = GetLock(ServerID.ToString());

			int nServerID = Apq.Convert.ChangeType<int>(ServerID);
			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "ID = " + nServerID;
			if (dv.Count == 0)
			{
				return;
			}
			DataRowView dr = dv[0];	// 取到服务器

			try
			{
				string FTPServer = Apq.Convert.ChangeType<string>(dv[0]["IPWan1"]);
				int FTPPort = Apq.Convert.ChangeType<int>(dv[0]["FTPPort"], 21);
				string FTPU = Apq.Convert.ChangeType<string>(dv[0]["FTPU"]);
				string FTPPD = Apq.Convert.ChangeType<string>(dv[0]["FTPPD"]);
				string FTPPath = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_In"].TrimEnd('/') + "/";
				Apq.Net.FtpClient fc = new Apq.Net.FtpClient(FTPServer, FTPPort, FTPPath, FTPU, FTPPD);

				string[] lstFullNames = Directory.GetFiles(GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_Out"]);
				foreach (string strFullName in lstFullNames)
				{
					fc.Upload(strFullName);
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				DataView dvErr = new DataView(_Sqls.SqlInstance);
				dvErr.RowFilter = "ID = " + nServerID;
				// 标记本服执行出错
				if (dvErr.Count > 0)
				{
					lock (rtLock)
					{
						Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
						{
							ErrList_XSD.ErrListRow drErrList = _UI.ErrList.NewErrListRow();
							drErrList.RSrvID = nServerID;
							drErrList["__ServerName"] = dvErr[0]["Name"];
							drErrList.s = ex.Message;
							_UI.ErrList.Rows.Add(drErrList);

							dvErr[0]["Err"] = true;
						});
					}
				}
				Apq.GlobalObject.ApqLog.Warn(dr["Name"], ex);
			}
			finally
			{
				lock (rtLock0)
				{
					Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
					{
						if (Apq.Convert.ChangeType<int>(tspb.Value) < tspb.Maximum)
						{
							tspb.Value = Apq.Convert.ChangeType<int>(tspb.Value) + 1;
							if (Apq.Convert.ChangeType<int>(tspb.Value) == tspb.Maximum)
							{
								_Sqls.SqlInstance.AcceptChanges();
								_UI.ErrList.AcceptChanges();
								tsslStateFileUp.Text = "已全部完成";
								UIEnable(true);

								DataView dvErr = new DataView(_Sqls.SqlInstance);
								dvErr.RowFilter = "Err = 1";
								// 标记本服执行出错
								if (dvErr.Count > 0)
								{
									tsslStateFileUp.Text += ",但有错误发生,请查看";
									GlobalObject.SolutionExplorer.FocusAndExpandByID(Apq.Convert.ChangeType<int>(dvErr[0]["ID"]));
								}
							}
						}
					});
				}
			}
		}

		/// <summary>
		/// 上传文件,并从服务器上保存出文件
		/// </summary>
		/// <param name="pnode"></param>
		/// <param name="sc"></param>
		private void DoFileUp(TreeListNode pnode, SqlConnection sc)
		{
			DataView dv = new DataView(dsExplorer.dtExplorer);
			dv.RowFilter = "CheckState = 1 AND Type = 2";
			SqlCommand sqlCmd = new SqlCommand("", sc);
			sqlCmd.CommandTimeout = 86400;//3600*24
			foreach (DataRowView drv in dv)
			{
				string strDBFolder_Up = GlobalObject.XmlConfigChain[this.GetType(), "DBFolder_Up"].TrimEnd('\\') + "\\";
				string DBFullName = strDBFolder_Up + drv["FileName"].ToString().Replace(":", string.Empty);
				Apq.DB.FileTrans ft = new Apq.DB.FileTrans(sc);
				ft.FileUp(drv["FileName"].ToString(), DBFullName, (byte[])drv["bFile"]);
			}
		}
		#endregion
		#endregion
	}
}
