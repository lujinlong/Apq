using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Net;

namespace ApqDBManager.Forms
{
	public partial class FTPFileTrans : Apq.Windows.Forms.DockForm
	{
		public FTPFileTrans()
		{
			InitializeComponent();
		}

		#region UI线程
		private void FileTrans_Load(object sender, EventArgs e)
		{
			#region 初始值
			if (!Apq.Convert.HasMean(beCFTPFolder_Out.EditValue))
			{
				beCFTPFolder_Out.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_Out"];
			}
			if (!Apq.Convert.HasMean(beDBFTPFolder_In.EditValue))
			{
				beDBFTPFolder_In.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_In"];
			}

			if (!Apq.Convert.HasMean(beDBFTPFolder_Out.EditValue))
			{
				beDBFTPFolder_Out.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_Out"];
			}
			if (!Apq.Convert.HasMean(beCFTPFolder_In.EditValue))
			{
				beCFTPFolder_In.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_In"];
			}
			#endregion

			// 获取服务器列表
			ReloadServers();
		}

		private void FileTrans_Activated(object sender, EventArgs e)
		{
			// 从ControlValues设置页面服务器列表,页面改变时,同时改变ControlValues里的值
			Forms.SolutionExplorer.UIState UISolution = Apq.Windows.Controls.ControlExtension.GetControlValues(this, "UISolution") as Forms.SolutionExplorer.UIState;
			GlobalObject.SolutionExplorer.SetServers(_Servers, UISolution);
			GlobalObject.ErrList.Set_ErrList(_UI);
		}

		private void FileTrans_Deactivate(object sender, EventArgs e)
		{
			ApqDBManager.Forms.SolutionExplorer.UIState State = GlobalObject.SolutionExplorer.GetUIState();
			Apq.Windows.Controls.ControlExtension.SetControlValues(this, "UISolution", State);
		}
		#endregion

		#region 目录设置
		private void beCFolder_Out_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (fbdCFTPFolder_Out.ShowDialog(this) == DialogResult.OK)
			{
				beCFTPFolder_Out.EditValue = fbdCFTPFolder_Out.SelectedPath;
			}
		}

		private void beCFolder_In_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (fbdCFolder_In.ShowDialog(this) == DialogResult.OK)
			{
				beCFTPFolder_In.EditValue = fbdCFolder_In.SelectedPath;
			}
		}

		private void beCFolder_Out_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beCFTPFolder_Out.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				fbdCFTPFolder_Out.SelectedPath = strFullName;
				GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_Out"] = strFullName;
			}
		}

		private void beDBFolder_In_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beDBFTPFolder_In.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_In"] = strFullName;
			}
		}

		private void beCFolder_In_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beCFTPFolder_In.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				fbdCFolder_In.SelectedPath = strFullName;
				GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_In"] = strFullName;
			}
		}

		private void beDBFolder_Out_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beDBFTPFolder_Out.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_Out"] = strFullName;
			}
		}
		#endregion

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
				beCFTPFolder_Out.Enabled = enabled;
				beDBFTPFolder_In.Enabled = enabled;
				btnDistribute.Enabled = enabled;

				beDBFTPFolder_Out.Enabled = enabled;
				beCFTPFolder_In.Enabled = enabled;
				btnCollect.Enabled = enabled;
			});
		}

		#region 分发
		private void btnDistribute_Click(object sender, EventArgs e)
		{
			UIEnable(false);
			// 先终止上一次主后台线程
			Apq.Threading.Thread.Abort(MainBackThread);
			MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(MainBackThread_StartDistribute));
		}

		#region 分发线程
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
					bsiState.Caption = "停止中...";
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
					bsiState.Caption = "准备完成,开始处理...";
				});
			}
		}

		/// <summary>
		/// 主后台线程入口,启动子线程/自己完成工作
		/// </summary>
		private void MainBackThread_StartDistribute()
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
			Workers_StartDistribute();
		}

		/// <summary>
		/// 开始一次工作(主后台线程调用)
		/// </summary>
		private void Workers_StartDistribute()
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
						bsiState.Caption = "启动中...";
						ripb.Maximum = dv.Count;
					});
				}

				foreach (DataRowView drv in dv)
				{
					ParameterizedThreadStart pts = new ParameterizedThreadStart(Worker_StartDistribute);
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
		/// 到一个服务器执行分发[多线程入口]
		/// </summary>
		/// <param name="ServerID"></param>
		private void Worker_StartDistribute(object ServerID)
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

			// 上传文件
			try
			{
				string FTPServer = Apq.Convert.ChangeType<string>(dv[0]["IPWan1"]);
				int FTPPort = Apq.Convert.ChangeType<int>(dv[0]["FTPPort"], 21);
				string FTPU = Apq.Convert.ChangeType<string>(dv[0]["FTPU"]);
				string FTPPD = Apq.Convert.ChangeType<string>(dv[0]["FTPPD"]);
				string FTPPath = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_In"].TrimEnd('/') + "/";
				Apq.Net.FtpClient fc = new Apq.Net.FtpClient(FTPServer, FTPPort, FTPPath, FTPU, FTPPD);

				string[] lstFullNames = Directory.GetFiles(GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_Out"].TrimEnd('\\') + "\\" + dr["Name"]);
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
				Apq.GlobalObject.ApqLog.Warn(dr["Name"], ex);
			}
			finally
			{
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
								bsiState.Caption = "已全部完成";
								UIEnable(true);

								DataView dvErr = new DataView(_Servers.dtServers);
								dvErr.RowFilter = "Err = 1";
								// 标记本服执行出错
								if (dvErr.Count > 0)
								{
									bsiState.Caption += ",但有错误发生,请查看";
									GlobalObject.SolutionExplorer.FocusAndExpandByID(Apq.Convert.ChangeType<int>(dvErr[0]["ID"]));
								}
							}
						}
					});
				}
			}
		}
		#endregion
		#endregion

		#region 收集
		private void btnCollect_Click(object sender, EventArgs e)
		{
			UIEnable(false);
			// 先终止上一次主后台线程
			Apq.Threading.Thread.Abort(MainBackThread);
			MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(MainBackThread_StartCollect));
		}

		#region 收集线程
		/// <summary>
		/// 主后台线程入口,启动子线程/自己完成工作
		/// </summary>
		private void MainBackThread_StartCollect()
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
			Workers_StartCollect();
		}

		/// <summary>
		/// 开始一次工作(主后台线程调用)
		/// </summary>
		private void Workers_StartCollect()
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
						bsiState.Caption = "启动中...";
						ripb.Maximum = dv.Count;
					});
				}

				foreach (DataRowView drv in dv)
				{
					ParameterizedThreadStart pts = new ParameterizedThreadStart(Worker_StartCollect);
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
		/// 到一个服务器执行收集[多线程入口]
		/// </summary>
		/// <param name="ServerID"></param>
		private void Worker_StartCollect(object ServerID)
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

			// 下载文件
			try
			{
				string FTPServer = Apq.Convert.ChangeType<string>(dv[0]["IPWan1"]);
				int FTPPort = Apq.Convert.ChangeType<int>(dv[0]["FTPPort"], 21);
				string FTPU = Apq.Convert.ChangeType<string>(dv[0]["FTPU"]);
				string FTPPD = Apq.Convert.ChangeType<string>(dv[0]["FTPPD"]);
				string FTPPath = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_Out"].TrimEnd('/') + "/";
				Apq.Net.FtpClient fc = new Apq.Net.FtpClient(FTPServer, FTPPort, FTPPath, FTPU, FTPPD);
				List<string> lstFileNames = fc.GetFileList("*.*");
				string strCFolder_In = GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_In"].TrimEnd('\\') + "\\";
				string strCFTPFolder_In = strCFolder_In + dr["Name"] + "\\";
				// 建立目录
				if (!Directory.Exists(strCFTPFolder_In)) Directory.CreateDirectory(strCFTPFolder_In);
				foreach (string strFileName in lstFileNames)
				{
					fc.Download(strCFTPFolder_In, strFileName);
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
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
				Apq.GlobalObject.ApqLog.Warn(dr["Name"], ex);
			}
			finally
			{
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
								bsiState.Caption = "已全部完成";
								UIEnable(true);

								DataView dvErr = new DataView(_Servers.dtServers);
								dvErr.RowFilter = "Err = 1";
								// 标记本服执行出错
								if (dvErr.Count > 0)
								{
									bsiState.Caption += ",但有错误发生,请查看";
									GlobalObject.SolutionExplorer.FocusAndExpandByID(Apq.Convert.ChangeType<int>(dvErr[0]["ID"]));
								}
							}
						}
					});
				}
			}
		}
		#endregion
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
