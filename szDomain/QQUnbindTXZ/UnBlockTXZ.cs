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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Web.Security;
using Hx2.WebMaster.DataAccess;

namespace QQUnbindTXZ
{
	/// <summary>
	/// 可作为 定时后台单线程示例
	/// </summary>
	public partial class UnBlockTXZ : DockContent, Apq.Editor.IEditor
	{
		#region MainBackThread
		/// <summary>
		/// 主后台线程,仅提供变量定义
		/// </summary>
		protected Thread _MainBackThread = null;
		/// <summary>
		/// 获取或设置主后台线程,仅提供变量定义
		/// </summary>
		public Thread MainBackThread
		{
			get { return _MainBackThread; }
			set { _MainBackThread = value; }
		}
		#endregion

		protected string _FileName = string.Empty;

		public UnBlockTXZ()
		{
			InitializeComponent();

			dataSet1.DataTable1.DataTable1RowChanged += new QQUnbindTXZ.xsd.DataSet1.DataTable1RowChangeEventHandler(DataTable1_DataTable1RowChanged);
		}

		#region UI线程
		private void UnBlockTXZ_Load(object sender, EventArgs e)
		{
			Apq.Windows.Controls.Control.AddImeHandler(this);
			Apq.Xtra.Common.AddBehaivor(gv);

			timer1.Interval = Apq.Convert.ChangeType<int>(GlobalObject.XmlAsmConfig["Interval"]) * 1000;

			// 自动开始
			if (Apq.Convert.ChangeType<bool>(GlobalObject.XmlAsmConfig["AutoStart"]))
			{
				btnGo_Click(btnGo, e);
			}
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			try
			{
				btnGo.Enabled = false;

				if (btnGo.Text == "停止")
				{
					btnGo_停止();
				}

				if (btnGo.Text == "开始")
				{
					btnGo_开始();
				}
			}
			finally
			{
				btnGo.Text = btnGo.Text == "开始" ? "停止" : "开始";
				btnGo.Enabled = true;
			}
		}

		private void btnGo_停止()
		{
			timer1.Stop();	// 停止计时
			Apq.Threading.Thread.Abort(MainBackThread);
		}

		private void btnGo_开始()
		{
			timer1.Start();	// 开始计时
			MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(timer1_Tick));	// 启动首次
		}

		private void btnSaveAs_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				MsgSaveAs(saveFileDialog1.FileName);
			}
		}

		/// <summary>
		/// 数据量达到 10000 条时转储记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DataTable1_DataTable1RowChanged(object sender, QQUnbindTXZ.xsd.DataSet1.DataTable1RowChangeEvent e)
		{
			while (dataSet1.DataTable1.Rows.Count > 10000)
			{
				MsgSaveAs("log\\Msg" + DateTime.Now.ToString() + ".xml");
			}
		}

		public void MsgSaveAs(string FileName)
		{
			dataSet1.DataTable1.WriteXml(saveFileDialog1.FileName);
			dataSet1.DataTable1.Rows.Clear();
			dataSet1.DataTable1.AcceptChanges();
		}
		#endregion

		#region IEditor 成员

		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
			}
		}

		public void Save()
		{
			if (FileName.Trim().Length > 0)
			{

			}
		}

		public void Copy()
		{
			gv.SelectAll();
			gv.CopyToClipboard();
		}

		public void Delete()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Open()
		{
			if (FileName.Trim().Length > 0)
			{

			}
		}

		public void Paste()
		{
		}

		public void Redo()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Reverse()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SelectAll()
		{
			gv.SelectAll();
		}

		public void Undo()
		{
		}

		#endregion

		#region timer1

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1_Tick();
		}

		// 一次完整过程
		private void timer1_Tick()
		{
			Worker_Stop();
			Worker_Start();
		}

		private void Worker_Stop()
		{
			//Apq.Threading.Thread.Abort(MainBackThread);
		}

		private void Worker_Start()
		{
			Worker_Start(0);
		}

		// 到一个服务器执行并显示结果
		private void Worker_Start(object ServerID)
		{
			try
			{
				SqlConnection sc = DALConnction.getConnection("QQHXUserDBCenterConnKey");
				try
				{
					Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, new Action<BarStaticItem>(
							delegate(BarStaticItem ctrl)
							{
								bsiState.Caption = "处理中...";
							}
						)
					);

					Apq.Data.Common.DbConnectionHelper.Open(sc);
					SqlDataAdapter sda = new SqlDataAdapter("dbo.PrBp_QQUnbindName_List", sc);
					sda.SelectCommand.CommandType = CommandType.StoredProcedure;
					Apq.Data.Common.DbCommandHelper dcHelper = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
					dcHelper.AddParameter("ExMsg", string.Empty);
					sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
					DataSet ds = new DataSet();
					int nRows = sda.Fill(ds);

					Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, new Action<BarStaticItem>(
							delegate(BarStaticItem ctrl)
							{
								ripb.Maximum = ds.Tables[0].Rows.Count;
							}
						)
					);
					foreach (DataRow dr2 in ds.Tables[0].Rows)
					{
						int UserID1 = Apq.Convert.ChangeType<int>(dr2["UserID1"]);
						int UserID2 = Apq.Convert.ChangeType<int>(dr2["UserID2"]);

						try
						{
							// 调用 WebService 解封
							string strPKey = GlobalObject.XmlAsmConfig["SHA1PKey"] + UserID1.ToString();
							strPKey = FormsAuthentication.HashPasswordForStoringInConfigFile(strPKey, "SHA1");
							WS.QQUnbindTXZ.UserInfo ws = new WS.QQUnbindTXZ.UserInfo();
							WS.QQUnbindTXZ.Result rt = ws.ConfirmQQHXUnBind(UserID1, UserID2, strPKey);
							if (rt.Success)
							{
								// 修改状态 PrBp_QQUnbindName_Finaly @ExMsg out, @UserID1(bigint)
								SqlCommand sqlCmd = new SqlCommand("dbo.PrBp_QQUnbindName_Finaly", sc);
								sqlCmd.CommandType = CommandType.StoredProcedure;
								Apq.Data.Common.DbCommandHelper dcHelper2 = new Apq.Data.Common.DbCommandHelper(sqlCmd);
								dcHelper2.AddParameter("ExMsg", string.Empty);
								sqlCmd.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;
								dcHelper2.AddParameter("UserID1", UserID1.ToString());
								sqlCmd.ExecuteNonQuery();

								// 显示该行解封完成
								DataRow dr1 = dataSet1.DataTable1.NewRow();
								dr1["str"] = "解封成功:" + dr2["UserID1"] + " --> " + dr2["UserID2"];
								dataSet1.DataTable1.Rows.Add(dr1);
							}
							else
							{
								DataRow dr1 = dataSet1.DataTable1.NewRow();
								dr1["str"] = "解封失败:" + dr2["UserID1"] + " --> " + dr2["UserID2"] + " 原因:" + rt.Message;
								dataSet1.DataTable1.Rows.Add(dr1);
							}
							dataSet1.DataTable1.AcceptChanges();
						}
						catch (Exception ex)
						{
							Apq.GlobalObject.ApqLog.Warn(string.Format("解封[{0}]时发生异常", UserID1), ex);
						}

						Apq.Windows.Delegates.Action_UI<RepositoryItemProgressBar>(this, ripb, new Action<RepositoryItemProgressBar>(
								delegate(RepositoryItemProgressBar ctrl)
								{
									ripb.Step++;
								}
							)
						);
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (DbException ex)
				{
					DataRow dr3 = dataSet1.DataTable1.NewRow();
					dr3["str"] = ex.Message;
					dataSet1.DataTable1.Rows.Add(dr3);
					dataSet1.DataTable1.AcceptChanges();
				}
				catch (Exception ex)
				{
					Apq.GlobalObject.ApqLog.Warn("未知异常", ex);
				}
				finally
				{
					Apq.Data.Common.DbConnectionHelper.Close(sc);

					Apq.Windows.Delegates.Action_UI<BarStaticItem>(this, bsiState, new Action<BarStaticItem>(
							delegate(BarStaticItem ctrl)
							{
								ripb.Step = 0;
								bsiState.Caption = "完成";
							}
						)
					);
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				Apq.GlobalObject.ApqLog.Warn("读取服务器异常", ex);
			}
		}
		#endregion
	}
}