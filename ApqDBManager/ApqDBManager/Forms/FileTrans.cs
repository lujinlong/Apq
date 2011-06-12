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

namespace ApqDBManager.Forms
{
	public partial class FileTrans : Apq.Windows.Forms.DockForm
	{
		public FileTrans()
		{
			InitializeComponent();
		}

		#region UI线程
		private void FileTrans_Load(object sender, EventArgs e)
		{
			#region 初始值
			if (!Apq.Convert.HasMean(beCFolder_Out.EditValue))
			{
				beCFolder_Out.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_Out"];
			}
			if (!Apq.Convert.HasMean(beDBFolder_In.EditValue))
			{
				beDBFolder_In.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_In"];
			}

			if (!Apq.Convert.HasMean(beDBFolder_Out.EditValue))
			{
				beDBFolder_Out.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_Out"];
			}
			if (!Apq.Convert.HasMean(beCFolder_In.EditValue))
			{
				beCFolder_In.EditValue = GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_In"];
			}
			#endregion

			// 获取服务器列表
			ReloadServers();
		}

		private void FileTrans_Activated(object sender, EventArgs e)
		{
			// 设置服务器列表
			GlobalObject.SolutionExplorer.SetServers(_Sqls);
		}

		private void FileTrans_Deactivate(object sender, EventArgs e)
		{
			GlobalObject.SolutionExplorer.SaveState2XSD();
		}
		#endregion

		#region 目录设置
		private void beCFolder_Out_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (fbdCFolder_Out.ShowDialog(this) == DialogResult.OK)
			{
				beCFolder_Out.EditValue = fbdCFolder_Out.SelectedPath;
			}
		}

		private void beCFolder_In_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (fbdCFolder_In.ShowDialog(this) == DialogResult.OK)
			{
				beCFolder_In.EditValue = fbdCFolder_In.SelectedPath;
			}
		}

		private void beCFolder_Out_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beCFolder_Out.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				fbdCFolder_Out.SelectedPath = strFullName;
				GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_Out"] = strFullName;
			}
		}

		private void beDBFolder_In_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beDBFolder_In.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				GlobalObject.XmlConfigChain[this.GetType(), "DBFTPFolder_In"] = strFullName;
			}
		}

		private void beCFolder_In_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beCFolder_In.EditValue);
			if (!string.IsNullOrEmpty(strFullName))
			{
				fbdCFolder_In.SelectedPath = strFullName;
				GlobalObject.XmlConfigChain[this.GetType(), "CFTPFolder_In"] = strFullName;
			}
		}

		private void beDBFolder_Out_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beDBFolder_Out.EditValue);
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
			beCFolder_Out.Enabled = enabled;
			beDBFolder_In.Enabled = enabled;
			btnDistribute.Enabled = enabled;

			beDBFolder_Out.Enabled = enabled;
			beCFolder_In.Enabled = enabled;
			btnCollect.Enabled = enabled;
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
			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "CheckState = 1 AND ID > 1";
			if (dv.Count == 0)
			{
				UIEnable(true);
				return;
			}

			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				dr["Err"] = false;
			}
			_Sqls.SqlInstance.AcceptChanges();

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
				DataView dv = new DataView(_Sqls.SqlInstance);
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
			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "ID = " + nServerID;
			if (dv.Count == 0)
			{
				return;
			}
			DataRowView dr = dv[0];	// 取到服务器

			// 读取文件
			Dictionary<string, byte[]> upFiles = new Dictionary<string, byte[]>();
			FileInfo fi;
			try
			{
				string[] lstFullNames = Directory.GetFiles(GlobalObject.XmlConfigChain[this.GetType(), "CFolder_Out"].TrimEnd('\\') + "\\" + dr["Name"]);
				foreach (string strFullName in lstFullNames)
				{
					try
					{
						System.IO.FileStream fs = System.IO.File.OpenRead(strFullName);
						fi = new FileInfo(strFullName);
						upFiles.Add(fi.FullName, Apq.IO.FileSystem.ReadFully(fs));
					}
					catch (Exception ex)
					{
						Apq.GlobalObject.ApqLog.Error("读取文件时出错:", ex);
					}
				}
			}
			catch { }

			// 上传文件
			SqlConnection sc = new SqlConnection(dr["ConnectionString"].ToString());
			try
			{
				Apq.Data.Common.DbConnectionHelper.Open(sc);
				sc.ChangeDatabase("Apq_DBA");
				SqlCommand sqlCmd = sc.CreateCommand();
				sqlCmd.CommandTimeout = 86400;//3600*24
				foreach (string strFullName in upFiles.Keys)
				{
					string strDBFolder_Up = GlobalObject.XmlConfigChain[this.GetType(), "DBFolder_In"].TrimEnd('\\') + "\\";
					string DBFullName = strDBFolder_Up + Path.GetFileName(strFullName);
					Apq.DB.FileTrans ft = new Apq.DB.FileTrans(sc);
					ft.FileUp(strFullName, DBFullName, upFiles[strFullName]);
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (DbException)
			{
				DataView dvErr = new DataView(_Sqls.SqlInstance);
				dvErr.RowFilter = "ID = " + nServerID;
				// 标记本服执行出错
				if (dvErr.Count > 0)
				{
					dvErr[0]["Err"] = true;
					_Sqls.SqlInstance.AcceptChanges();
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
								bsiState.Caption = "已全部完成";
								UIEnable(true);

								DataView dvErr = new DataView(_Sqls.SqlInstance);
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
			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "CheckState = 1 AND ID > 1";
			if (dv.Count == 0)
			{
				UIEnable(true);
				return;
			}

			foreach (DataRow dr in _Sqls.SqlInstance.Rows)
			{
				dr["Err"] = false;
			}
			_Sqls.SqlInstance.AcceptChanges();

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
				DataView dv = new DataView(_Sqls.SqlInstance);
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
			DataView dv = new DataView(_Sqls.SqlInstance);
			dv.RowFilter = "ID = " + nServerID;
			if (dv.Count == 0)
			{
				return;
			}
			DataRowView dr = dv[0];	// 取到服务器

			SqlConnection sc = new SqlConnection(dr["ConnectionString"].ToString());
			try
			{
				Apq.Data.Common.DbConnectionHelper.Open(sc);
				sc.ChangeDatabase("master");

				// 服务器收集文件到数据库
				DataSet ds = new DataSet();
				string strDBFolder_Out = GlobalObject.XmlConfigChain[typeof(ApqDBManager.Forms.FileTrans), "DBFTPFolder_Out"].TrimEnd('\\') + "\\";
				string strCFolder_In = GlobalObject.XmlConfigChain[typeof(ApqDBManager.Forms.FileTrans), "CFTPFolder_In"].TrimEnd('\\') + "\\";
				SqlDataAdapter sda = new SqlDataAdapter("EXEC @rtn = xp_cmdshell @cmd", sc);
				Apq.Data.Common.DbCommandHelper cmdHelper = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
				cmdHelper.AddParameter("@rtn", 0, DbType.Int32);
				sda.SelectCommand.Parameters["@rtn"].Direction = ParameterDirection.InputOutput;// 参数化查询
				cmdHelper.AddParameter("@cmd", string.Format("dir {0} /b /a-d", Apq.Dos.Common.EncodeParam(strDBFolder_Out)));
				sda.Fill(ds);
				if (ds.Tables.Count == 0 || Apq.Convert.ChangeType<bool>(sda.SelectCommand.Parameters["@rtn"].Value))
				{
					return;
				}

				sc.ChangeDatabase("Apq_DBA");
				Apq.DB.FileTrans ft = new Apq.DB.FileTrans(sc);
				foreach (DataRow drDBFile in ds.Tables[0].Rows)
				{
					if (Apq.Convert.HasMean(drDBFile[0]))
					{
						ft.FileDow(strDBFolder_Out + drDBFile[0], strCFolder_In + dr["Name"] + "\\" + drDBFile[0]);
					}
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (DbException)
			{
				DataView dvErr = new DataView(_Sqls.SqlInstance);
				dvErr.RowFilter = "ID = " + nServerID;
				// 标记本服执行出错
				if (dvErr.Count > 0)
				{
					dvErr[0]["Err"] = true;
					_Sqls.SqlInstance.AcceptChanges();
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
								bsiState.Caption = "已全部完成";
								UIEnable(true);

								DataView dvErr = new DataView(_Sqls.SqlInstance);
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
			_Sqls.SqlInstance.Clear();
			_Sqls.SqlInstance.Merge(GlobalObject.Sqls.SqlInstance);
		}
	}
}
