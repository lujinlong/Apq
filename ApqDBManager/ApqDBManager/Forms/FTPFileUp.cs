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

namespace ApqDBManager.Forms
{
	public partial class FTPFileUp : Apq.Windows.Forms.DockForm
	{
		public FTPFileUp()
		{
			InitializeComponent();

			this.tlcFullName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.tlcFileName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.tlcType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.luType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.tlcSize = new DevExpress.XtraTreeList.Columns.TreeListColumn();

			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlcFullName,
            this.tlcFileName,
            this.tlcType,
            this.tlcSize});
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.luType});
			// 
			// tlcFullName
			// 
			this.tlcFullName.Caption = "FullName";
			this.tlcFullName.FieldName = "FullName";
			this.tlcFullName.Name = "tlcFullName";
			// 
			// tlcFileName
			// 
			this.tlcFileName.Caption = "名称";
			this.tlcFileName.FieldName = "FileName";
			this.tlcFileName.MinWidth = 35;
			this.tlcFileName.Name = "tlcFileName";
			this.tlcFileName.OptionsColumn.AllowEdit = false;
			this.tlcFileName.OptionsColumn.ReadOnly = true;
			this.tlcFileName.Visible = true;
			this.tlcFileName.VisibleIndex = 0;
			// 
			// tlcType
			// 
			this.tlcType.Caption = "类型";
			this.tlcType.ColumnEdit = this.luType;
			this.tlcType.FieldName = "Type";
			this.tlcType.Name = "tlcType";
			this.tlcType.OptionsColumn.AllowEdit = false;
			this.tlcType.OptionsColumn.ReadOnly = true;
			this.tlcType.Visible = true;
			this.tlcType.VisibleIndex = 1;
			// 
			// luType
			// 
			this.luType.AutoHeight = false;
			this.luType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Type", "Type", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeCaption", "类型", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
			this.luType.DisplayMember = "TypeCaption";
			this.luType.Name = "luType";
			this.luType.ReadOnly = true;
			this.luType.ValueMember = "Type";
			// 
			// tlcSize
			// 
			this.tlcSize.AppearanceCell.Options.UseTextOptions = true;
			this.tlcSize.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.tlcSize.Caption = "大小(Bytes)";
			this.tlcSize.FieldName = "Size";
			this.tlcSize.Name = "tlcSize";
			this.tlcSize.OptionsColumn.AllowEdit = false;
			this.tlcSize.OptionsColumn.ReadOnly = true;
			this.tlcSize.Visible = true;
			this.tlcSize.VisibleIndex = 2;
		}

		private DevExpress.XtraTreeList.Columns.TreeListColumn tlcFileName;
		private DevExpress.XtraTreeList.Columns.TreeListColumn tlcType;
		private DevExpress.XtraTreeList.Columns.TreeListColumn tlcSize;
		private DevExpress.XtraTreeList.Columns.TreeListColumn tlcFullName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luType;

		private Apq.XSD.Explorer ds = new Apq.XSD.Explorer();
		private long NextID = 1;

		#region UI线程
		private void FileUp_Load(object sender, EventArgs e)
		{
			if (!Apq.Convert.HasMean(beDBFolder_Up.EditValue))
			{
				beDBFolder_Up.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "DBFolder_Up"];
			}

			ds.Init_luType();
			luType.DataSource = ds.luType;
			treeList1.DataSource = ds;
			btnRefresh_ItemClick(sender, null);

			Apq.Xtra.TreeList.Common.AddBehaivor(treeList1);

			// 获取服务器列表
			ReloadServers();
		}

		private void FileUp_Activated(object sender, EventArgs e)
		{
			// 从ControlValues设置页面服务器列表,页面改变时,同时改变ControlValues里的值
			Forms.SolutionExplorer.UIState UISolution = Apq.Windows.Controls.ControlExtension.GetControlValues(this, "UISolution") as Forms.SolutionExplorer.UIState;
			GlobalObject.SolutionExplorer.SetServers(_Servers, UISolution);
		}

		private void FileUp_Deactivate(object sender, EventArgs e)
		{
			ApqDBManager.Forms.SolutionExplorer.UIState State = GlobalObject.SolutionExplorer.GetUIState();
			Apq.Windows.Controls.ControlExtension.SetControlValues(this, "UISolution", State);
		}
		#endregion

		#region 本地浏览
		// 刷新
		private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
		{
			treeList1.BeginUpdate();
			ds.dtExplorer.Clear();
			NextID = 1;
			Apq.IO.FileSystem.LoadLogicDrivers(ds.dtExplorer, ref NextID);
			treeList1.EndUpdate();
		}

		#region treeList1
		// Checked图片
		private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			e.Node.HasChildren = Apq.Convert.ChangeType<bool>(e.Node["HasChildren"]);
			e.NodeImageIndex = Apq.Convert.ChangeType<int>(e.Node["CheckState"]);
		}

		#region 选中状态
		private void SetCheckedNode(TreeListNode node)
		{
			int Checked = (int)node["CheckState"];
			if (Checked == 2 || Checked == 0) Checked = 1;
			else Checked = 0;

			treeList1.BeginUpdate();
			node["CheckState"] = Checked;
			SetCheckedChildNodes(node, Checked);
			SetCheckedParentNodes(node, Checked);
			ds.dtExplorer.AcceptChanges();
			treeList1.EndUpdate();

			StatusBarDisplayText(treeList1.FocusedNode);
		}
		/*
		private void SetCheckedNode(TreeListNode node, bool checkParent, bool checkChildren)
		{
			int Checked = (int)node["CheckState"];
			if (Checked == 2 || Checked == 0) Checked = 1;
			else Checked = 0;

			treeList1.BeginUpdate();
			node["CheckState"] = Checked;
			if (checkParent) SetCheckedParentNodes(node, Checked);
			if (checkChildren) SetCheckedChildNodes(node, Checked);
			treeList1.EndUpdate();
		}
		*/
		private void SetCheckedChildNodes(TreeListNode node, int Checked)
		{
			foreach (TreeListNode tln in node.Nodes)
			{
				tln["CheckState"] = Checked;
				SetCheckedChildNodes(tln, Checked);
			}
		}
		private void SetCheckedParentNodes(TreeListNode node, int Checked)
		{
			if (node.ParentNode != null)
			{
				bool b = false;
				for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
				{
					if (!Checked.Equals(node.ParentNode.Nodes[i]["CheckState"]))
					{
						b = !b;
						break;
					}
				}
				node.ParentNode["CheckState"] = b ? 2 : 1; ;
				SetCheckedParentNodes(node.ParentNode, Checked);
			}
		}
		/*
		private void SetCheckedParentNodes(TreeListNode node, int Checked)
		{
			if (node.ParentNode != null)
			{
				node.ParentNode[CheckState] = Checked;
				SetCheckedParentNodes(node.ParentNode, check);
			}
		}
		*/
		#endregion

		#region 点击
		private void treeList1_StateImageClick(object sender, NodeClickEventArgs e)
		{
			SetCheckedNode(e.Node);
		}

		private void treeList1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				SetCheckedNode(treeList1.FocusedNode);
			}
		}
		private void treeList1_EditorKeyUp(object sender, KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == Keys.C))
			{
				Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode["FullName"]);
			}
			#endregion
		}
		private void treeList1_KeyUp(object sender, KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == Keys.C))
			{
				Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode["FullName"]);
			}
			#endregion
		}
		#endregion

		#region 展开
		private void treeList1_BeforeCollapse(object sender, BeforeCollapseEventArgs e)
		{
			if (1.Equals(e.Node["Type"]))//文件夹
			{
				e.Node["SelectImageIndex"] = 0;
			}
		}
		private void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
		{
			if (1.Equals(e.Node["Type"]) && Apq.Convert.ChangeType<bool>(e.Node["HasChildren"]))//有子级的文件夹
			{
				e.Node["SelectImageIndex"] = Apq.Convert.ChangeType<bool>(e.Node["SelectImageIndex"]) ? 0 : 1;
			}
			if (Apq.Convert.ChangeType<bool>(e.Node["HasChildren"]) && !Apq.Convert.ChangeType<bool>(e.Node["Loaded"]))
			{
				Cursor currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				Apq.IO.FileSystem.LoadFolders(ds.dtExplorer, ref NextID, Apq.Convert.ChangeType<long>(e.Node["ID"]), Apq.Convert.ChangeType<int>(e.Node["CheckState"]));
				Apq.IO.FileSystem.LoadFiles(ds.dtExplorer, ref NextID, Apq.Convert.ChangeType<long>(e.Node["ID"]), Apq.Convert.ChangeType<int>(e.Node["CheckState"]));
				e.Node["Loaded"] = true;
				Cursor.Current = currentCursor;
			}
		}
		#endregion

		private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
			StatusBarDisplayText(e.Node);
		}
		private void StatusBarDisplayText(TreeListNode node)
		{
			if (node != null)
			{
				bsiState.Caption = node.GetDisplayText("FullName");
			}
			else
				bsiState.Caption = string.Empty;
		}

		private void treeList1_CompareNodeValues(object sender, DevExpress.XtraTreeList.CompareNodeValuesEventArgs e)
		{
			int type1 = Apq.Convert.ChangeType<int>(e.Node1["Type"]);
			int type2 = Apq.Convert.ChangeType<int>(e.Node2["Type"]);
			if (type1 != type2)
			{
				if (type1 == 1)
					e.Result = (e.SortOrder == System.Windows.Forms.SortOrder.Ascending) ? -1 : 1;
				else
					e.Result = (e.SortOrder == System.Windows.Forms.SortOrder.Ascending) ? 1 : -1;
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
			Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
			{
				beDBFolder_Up.Enabled = enabled;
				btnRefresh.Enabled = enabled;
				btnUp.Enabled = enabled;
				treeList1.Enabled = enabled;
			});
		}

		private void btnUp_ItemClick(object sender, ItemClickEventArgs e)
		{
			UIEnable(false);
			// 先终止上一次主后台线程
			Apq.Threading.Thread.Abort(MainBackThread);

			// 读取文件
			bsiStateFileUp.Caption = "正在加载文件...";
			DataView dv = new DataView(ds.dtExplorer);
			dv.RowFilter = "CheckState = 1 AND Type = 2";
			foreach (DataRowView drv in dv)
			{
				if (!Apq.Convert.ChangeType<bool>(drv["Loaded"]))
				{
					drv["Loaded"] = true;
					System.IO.FileStream fs = System.IO.File.OpenRead(drv["FullName"].ToString());
					drv["bFile"] = Apq.IO.FileSystem.ReadFully(fs);
				}
			}

			MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(MainBackThread_Start));
		}

		#region 后台线程
		/// <summary>
		/// 主后台线程入口,启动子线程/自己完成工作
		/// </summary>
		private void MainBackThread_Start()
		{
			DataView dv = new DataView(_Servers.dtServers);
			dv.RowFilter = "CheckState = 1 AND ID > 1";
			if (dv.Count == 0)
			{
				UIEnable(true);
				return;
			}

			Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
			{
				_UI.ErrList.Clear();
				_UI.ErrList.AcceptChanges();
			});
			foreach (DataRow dr in _Servers.dtServers.Rows)
			{
				dr["Err"] = false;
				dr["IsReadyToGo"] = false;
			}
			foreach (DataRowView drv in dv)
			{
				drv["IsReadyToGo"] = true;
			}
			_Servers.dtServers.AcceptChanges();

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
				Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
				{
					beiPb1.EditValue = 0;
					bsiStateFileUp.Caption = "停止中...";
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
				Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
				{
					bsiStateFileUp.Caption = "准备完成,开始处理...";
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
				DataView dv = new DataView(_Servers.dtServers);
				dv.RowFilter = "CheckState = 1 AND ID > 1";
				#endregion

				#region 开始
				lock (rtLock0)
				{
					Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
					{
						bsiStateFileUp.Caption = "启动中...";
						ripb.Maximum = dv.Count;
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
			DataView dv = new DataView(_Servers.dtServers);
			dv.RowFilter = "ID = " + nServerID;
			if (dv.Count == 0)
			{
				return;
			}
			DataRowView dr = dv[0];	// 取到服务器

			SqlConnection sc = new SqlConnection(dr["ConnectionString"].ToString());
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
			catch (DbException ex)
			{
				DataView dvErr = new DataView(_Servers.dtServers);
				dvErr.RowFilter = "ID = " + nServerID;
				// 标记本服执行出错
				if (dvErr.Count > 0)
				{
					lock (rtLock)
					{
						Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
						{
							XSD.UI.ErrListRow drErrList = _UI.ErrList.NewErrListRow();
							drErrList.RSrvID = nServerID;
							drErrList["__ServerName"] = dvErr[0]["Name"];
							drErrList.s = ex.Message;
							_UI.ErrList.Rows.Add(drErrList);

							dvErr[0]["Err"] = true;
						});
					}
				}
			}
			catch (Exception ex)
			{
				Apq.GlobalObject.ApqLog.Warn(dr["Name"], ex);
			}
			finally
			{
				Apq.Data.Common.DbConnectionHelper.Close(sc);
				lock (rtLock0)
				{
					Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, delegate(BarStaticItem ctrl)
					{
						if (Apq.Convert.ChangeType<int>(beiPb1.EditValue) < ripb.Properties.Maximum)
						{
							beiPb1.EditValue = Apq.Convert.ChangeType<int>(beiPb1.EditValue) + 1;
							if (Apq.Convert.ChangeType<int>(beiPb1.EditValue) == ripb.Properties.Maximum)
							{
								_Servers.dtServers.AcceptChanges();
								_UI.ErrList.AcceptChanges();
								bsiStateFileUp.Caption = "已全部完成";
								UIEnable(true);

								DataView dvErr = new DataView(_Servers.dtServers);
								dvErr.RowFilter = "Err = 1";
								// 标记本服执行出错
								if (dvErr.Count > 0)
								{
									bsiStateFileUp.Caption += ",但有错误发生,请查看";
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
			DataView dv = new DataView(ds.dtExplorer);
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

		private void beDBFolder_Up_EditValueChanged(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(beDBFolder_Up.EditValue))
			{
				GlobalObject.XmlConfigChain[this.GetType(), "DBFolder_Up"] = Apq.Convert.ChangeType<string>(beDBFolder_Up.EditValue);
			}
		}
		#endregion

		/// <summary>
		/// 重新加载服务器列表
		/// </summary>
		public void ReloadServers()
		{
			_Servers.dtServers.Clear();
			_Servers.dtServers.Merge(GlobalObject.Servers.dtServers);
		}
	}
}
