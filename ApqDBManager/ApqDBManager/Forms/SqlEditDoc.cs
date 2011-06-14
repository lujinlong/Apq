using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ApqDBManager.Forms
{
	public partial class SqlEditDoc : Apq.Windows.Forms.DockForm, Apq.Editor.ITextEditor
	{
		public SqlEditDoc()
		{
			InitializeComponent();

			txtSql.Encoding = System.Text.Encoding.Default;

			// 获取服务器列表
			_Sqls = GlobalObject.Sqls.Copy() as ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD;
		}

		public SqlEdit SqlEdit
		{
			get { return Parent as SqlEdit; }
		}

		private static string[] aryGo = { "\r\ngo\r\n", "\r\ngO\r\n", "\r\nGo\r\n", "\r\nGO\r\n" };

		private List<Thread> thds = new List<Thread>();
		private List<DataSet> lstds = new List<DataSet>();

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

		private void SqlEditDoc_Load(object sender, EventArgs e)
		{
			#region 添加图标
			//Icon = new Icon(@"Res\ico\sign125.ico");

			this.tsmiUndo.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Undo.png");
			this.tsmiRedo.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Redo.png");
			this.tsmiCut.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Cut.png");
			this.tsmiCopy.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Copy.png");
			this.tsmiPaste.Image = System.Drawing.Image.FromFile(@"Res\png\Editor\Paste.png");
			#endregion

			txtSql.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
			txtSql.ShowEOLMarkers = false;
			txtSql.ShowSpaces = false;
			txtSql.ShowTabs = false;
			txtSql.ShowVRuler = false;
			txtSql.Document.DocumentChanged += new DocumentEventHandler(txtSql_DocumentChanged);

			#region tscbDBName.Items
			List<string> cbDBName_Items = new List<string>();
			if (GlobalObject.XmlAsmConfig.GetValue("cbDBName_Items") != null)
			{
				cbDBName_Items.AddRange(GlobalObject.XmlAsmConfig.GetValue("cbDBName_Items").Split(','));
			}
			if (GlobalObject.XmlUserConfig.GetValue("cbDBName_Items") != null)
			{
				string[] aryDBName_Items = GlobalObject.XmlUserConfig.GetValue("cbDBName_Items").Split(',');
				foreach (string strDBName in aryDBName_Items)
				{
					if (!cbDBName_Items.Contains(strDBName))
					{
						cbDBName_Items.Add(strDBName);
					}
				}
			}
			tscbDBName.Items.AddRange(cbDBName_Items.ToArray());
			#endregion

			#region txtSql.Text
			string cfgtxtSql_Text = GlobalObject.XmlConfigChain[this.GetType(), "txtSql_Text"];
			if (cfgtxtSql_Text != null)
			{
				txtSql.Text = cfgtxtSql_Text;
			}
			/*
#if DEBUG
			if (string.IsNullOrEmpty(txtSql.Text))
			{
				this.txtSql.Text = @"
DECLARE @t table(ID int, ID1 int);
INSERT @t(ID, ID1)
SELECT 1,1
UNION ALL SELECT 2,2
UNION ALL SELECT 3,3
UNION ALL SELECT 4,4
UNION ALL SELECT 5,5
UNION ALL SELECT 6,6
UNION ALL SELECT 7,7
UNION ALL SELECT 8,8
UNION ALL SELECT 9,9
UNION ALL SELECT 10,10
UNION ALL SELECT 11,11
UNION ALL SELECT 12,12
UNION ALL SELECT 13,13
UNION ALL SELECT 14,14
UNION ALL SELECT 15,15
UNION ALL SELECT 16,16
UNION ALL SELECT 17,17
UNION ALL SELECT 18,18
UNION ALL SELECT 19,19
UNION ALL SELECT 20,20
UNION ALL SELECT 21,21;

SELECT t1.* FROM @t t1, @t t2;

SELECT 1,1
UNION ALL SELECT 2,2;";
			}
#endif
			 */
			#endregion

			#region tscbDBName.Text
			string cfgcbDBName = GlobalObject.XmlConfigChain[this.GetType(), "cbDBName"];
			if (cfgcbDBName != null)
			{
				tscbDBName.SelectedText = cfgcbDBName;
			}
			#endregion
		}

		void txtSql_DocumentChanged(object sender, DocumentEventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "txtSql_Text"] = txtSql.Text;
		}

		#region UI线程
		private void tscbDBName_Leave(object sender, EventArgs e)
		{
			string strDBName = tscbDBName.Text.Trim();
			GlobalObject.XmlUserConfig.SetValue("cbDBName", strDBName);

			bool IsFound = false;
			foreach (string cbItem in tscbDBName.Items)
			{
				if (cbItem == strDBName)
				{
					IsFound = true;
					break;
				}
			}

			if (!IsFound)
			{
				tscbDBName.Items.Add(strDBName);

				string strDBNames = string.Empty;
				foreach (string cbItem in tscbDBName.Items)
				{
					strDBNames += "," + cbItem;
				}
				if (strDBNames.Length > 1)
				{
					strDBNames = strDBNames.Substring(1);
				}

				GlobalObject.XmlUserConfig.SetValue("cbDBName_Items", strDBNames);
			}
		}

		private void tssbResult_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripMenuItem tsmi1 = e.ClickedItem as ToolStripMenuItem;
			if (tsmi1 != null)
			{
				tsmi1.Checked = true;
				ToolStripMenuItem tsmi2 = tssbResult.DropDownItems[System.Math.Abs(tssbResult.DropDownItems.IndexOf(tsmi1) - 1)] as ToolStripMenuItem;
				tsmi2.Checked = false;
			}
		}

		private void tsbExec_Click(object sender, EventArgs e)
		{
			// 读取服务器列表状态
			GlobalObject.SolutionExplorer.SaveState2XSD();

			string strSql = txtSql.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
			if (string.IsNullOrEmpty(strSql))
			{
				strSql = txtSql.Text;
			}
			if (string.IsNullOrEmpty(strSql))
			{
				return;
			}

			#region 风险提示
			// 检测语句中是否含有风险语句并提示
			if (strSql.ToUpper().Contains("DELETE"))
			{
				if (MessageBox.Show("检测到风险语句 DELETE,确定执行?", "风险提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
					!= DialogResult.OK)
				{
					return;
				}
			}
			Regex r = new Regex("((TRUNCATE|DROP)\\s+TABLE)\\s+[^@#]", RegexOptions.IgnoreCase);
			Match m = r.Match(strSql);
			if (m.Success)
			{
				string strSql_Risk = m.Groups[1].Value;
				if (MessageBox.Show(string.Format("检测到风险语句 {0},确定执行?", strSql_Risk), "风险提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
					!= DialogResult.OK)
				{
					return;
				}
			}
			r = new Regex("(UPDATE)\\s+[^@#]", RegexOptions.IgnoreCase);
			m = r.Match(strSql);
			if (m.Success)
			{
				string strSql_Risk = m.Groups[1].Value;
				if (MessageBox.Show(string.Format("检测到风险语句 {0},确定执行?", strSql_Risk), "风险提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
					!= DialogResult.OK)
				{
					return;
				}
			}
			#endregion

			if (!string.IsNullOrEmpty(strSql))
			{
				#region 注册检测
				DataView dv = new DataView(_Sqls.SqlInstance);
				dv.RowFilter = "CheckState = 1 AND SqlID > 1";
				if (dv.Count > 3 && !Apq.Reg.Client.Common.IsRegistration)
				{
					MessageBox.Show("谢谢您支持共享软件!共享版最多支持同时连接3个服务器,要获得更多的连接支持,请注册,谢谢使用", "注册提示");
					return;
				}
				#endregion

				tsbExec.Enabled = false;

				// 将线程中需要的控件值取到变量
				strSql += "\r\n";
				Apq.Windows.Controls.ControlExtension.SetControlValues(this, "arySql", strSql.Split(aryGo, StringSplitOptions.RemoveEmptyEntries));
				int nRIdx = tsmiResult1.Checked ? 1 : 0;
				Apq.Windows.Controls.ControlExtension.SetControlValues(this, "bliResult.ItemIndex", nRIdx);
				Apq.Windows.Controls.ControlExtension.SetControlValues(this, "tscbDBName.Text", tscbDBName.Text);

				// 先终止上一次主后台线程
				Apq.Threading.Thread.Abort(MainBackThread);
				MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(MainBackThread_Start));

				tsbCancel.Enabled = true;
			}
		}

		private void tsbCancel_Click(object sender, EventArgs e)
		{
			tsbCancel.Enabled = false;

			Apq.Threading.Thread.Abort(MainBackThread);
			foreach (Thread t in thds)
			{
				Apq.Threading.Thread.Abort(t);
				Application.DoEvents();
			}
			thds.Clear();
			SqlEdit.tsslStatus.Text = "已取消";
			SqlEdit.tspb.Value = 0;

			tsbExec.Enabled = true;
		}

		private void tsbSingleThread_Click(object sender, EventArgs e)
		{
			tsbSingleThread.Checked = !tsbSingleThread.Checked;
		}

		private void tsbExport_Click(object sender, EventArgs e)
		{
			if (lstds.Count > 0)
			{
				DataSet ds = lstds[0].Copy();

				// 增加服务器名列
				foreach (DataTable dt in ds.Tables)
				{
					dt.Columns.Add("__ServerName");
					// 设置第一个数据集的服务器名
					// 设置服务器名
					foreach (DataRow dr in dt.Rows)
					{
						dr["__ServerName"] = ds.DataSetName;
					}
				}

				for (int i = 1; i < lstds.Count; i++)
				{
					ds.Merge(lstds[i]);
					// 设置服务器名
					foreach (DataTable dt in ds.Tables)
					{
						foreach (DataRow dr in dt.Rows)
						{
							if (Apq.Convert.LikeDBNull(dr["__ServerName"]))
							{
								dr["__ServerName"] = lstds[i].DataSetName;
							}
						}
					}
				}

				Export FormE = new Export(ds);
				FormE.ShowDialog(this);
			}
		}
		#endregion

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
				return;
			}

			Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
			{
				dsUI.ErrList.Clear();
				dsUI.ErrList.AcceptChanges();
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

			if (tsbSingleThread.Checked)
			{
				Workers_Stop();

				foreach (DataRowView drv in dv)
				{
					Worker_Start(drv["ID"]);
				}
			}
			else
			{
				Workers_Stop();
				Workers_Start();
			}
		}

		/// <summary>
		/// 停止上次工作(主后台线程或其子线程 调用)
		/// </summary>
		private void Workers_Stop()
		{
			object rtLock0 = GetLock("0");
			lock (rtLock0)
			{
				Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
				{
					SqlEdit.tspb.Value = 0;
					SqlEdit.tsslStatus.Text = "停止中...";
				});
				foreach (DataSet item in lstds)
				{
					item.Clear();			// 清空行
					item.Tables.Clear();	// 清空表
				}
				lstds.Clear();
				thds.Clear();
			}
			foreach (Thread t in thds)
			{
				Apq.Threading.Thread.Abort(t);
			}
			lock (rtLock0)
			{
				Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
				{
					SqlEdit.tsslStatus.Text = "准备完成,开始处理...";
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
					int nbliResult_ItemIndex = Apq.Convert.ChangeType<int>(Apq.Windows.Controls.ControlExtension.GetControlValues(this, "bliResult.ItemIndex"));
					Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
					{
						SqlEdit.tsslStatus.Text = "启动中...";
						SqlEdit.tspb.Maximum = dv.Count;
					});
				}

				foreach (DataRowView drv in dv)
				{
					ParameterizedThreadStart pts = new ParameterizedThreadStart(Worker_Start);
					try
					{
						Thread t = Apq.Threading.Thread.StartNewThread(pts, drv["SqlID"]);
						if (!Convert.IsDBNull(drv["SqlName"]))
						{
							t.Name = drv["SqlName"].ToString();
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
		private void Worker_Start(object SqlID)
		{
			object rtLock0 = GetLock("0");
			object rtLock = GetLock(SqlID.ToString());

			int nSqlID = Apq.Convert.ChangeType<int>(SqlID);
			ApqDBManager.Forms.SrvsMgr.SrvsMgr_XSD.SqlInstanceRow dr = _Sqls.SqlInstance.FindBySqlID(nSqlID);
			DataSet dsTabPage = new DataSet(dr.SqlName);
			dsTabPage.ExtendedProperties.Add("SqlID", nSqlID);

			// 0.结果DataSet准备
			int nbliResult_ItemIndex = Apq.Convert.ChangeType<int>(Apq.Windows.Controls.ControlExtension.GetControlValues(this, "bliResult.ItemIndex"));
			if (nbliResult_ItemIndex == 0)//分列
			{

				lock (rtLock0)
				{
					Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
					{
						lstds.Add(dsTabPage);
					});
				}
			}
			else
			{
				dsTabPage = lstds[0];
			}

			SqlConnection sc = null;
			try
			{
				sc = new SqlConnection(dr["DBConnectionString"].ToString());

				SqlDataAdapter sda = new SqlDataAdapter(string.Empty, sc);
				sda.SelectCommand.CommandTimeout = 86400;//3600*24

				// 1.设置数据库
				Apq.Data.Common.DbConnectionHelper.Open(sc);
				string DBName = Apq.Convert.ChangeType<string>(Apq.Windows.Controls.ControlExtension.GetControlValues(this, "tscbDBName.Text"));
				sc.ChangeDatabase(DBName);//更改数据库后再启用消息事件可防止执行到其它数据库
				sc.StatisticsEnabled = true;// 启用统计
				sc.FireInfoMessageEventOnUserErrors = true;// 启用消息事件
				//sc.InfoMessage += new SqlInfoMessageEventHandler(rt.sc_InfoMessage);

				// 2.准备语句
				string[] arySql = Apq.Windows.Controls.ControlExtension.GetControlValues(this, "arySql") as string[];

				// 3.执行语句
				if (arySql != null)
				{
					int TableCount = 0;
					for (int i = 0; i < arySql.Length; i++)
					{
						DataSet ds1 = new DataSet();
						sda.SelectCommand.CommandText = arySql[i];
						//nRows += 
						sda.Fill(ds1);	// 连接对象启用消息事件后一般不会引发异常

						foreach (DataTable ds1Table in ds1.Tables)
						{
							ds1Table.TableName = "Apq_Table" + TableCount++;
							dsTabPage.Tables.Add(ds1Table.Copy());
						}
					}
				}
			}
			catch (ThreadAbortException)
			{
			}
			//catch (DbException ex)
			//{
			//}
			catch (Exception ex)
			{
				// 标记本服执行出错
				lock (rtLock)
				{
					Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
					{
						ErrList_XSD.ErrListRow drErrList = dsUI.ErrList.NewErrListRow();
						drErrList.RSrvID = nSqlID;
						drErrList["__ServerName"] = dr.SqlName;
						drErrList.s = ex.Message;
						dsUI.ErrList.Rows.Add(drErrList);

						dr["Err"] = true;
					});
				}
				Apq.GlobalObject.ApqLog.Warn(dr["SqlName"], ex);
			}
			finally
			{
				Apq.Data.Common.DbConnectionHelper.Close(sc);
				lock (rtLock0)
				{
					Apq.Windows.Delegates.Action_UI<ToolStripLabel>(this, tslDBName, delegate(ToolStripLabel ctrl)
					{
						if (SqlEdit.tspb.Value < SqlEdit.tspb.Maximum)
						{
							SqlEdit.tspb.Value++;
							if (SqlEdit.tspb.Value == SqlEdit.tspb.Maximum)
							{
								_Sqls.SqlInstance.AcceptChanges();
								dsUI.ErrList.AcceptChanges();
								SqlEdit.tsslStatus.Text = "已全部完成";
								tsbCancel.Enabled = false;
								tsbExec.Enabled = true;

								DataView dvErr = new DataView(_Sqls.SqlInstance);
								dvErr.RowFilter = "Err = 1";
								// 标记本服执行出错
								if (dvErr.Count > 0)
								{
									SqlEdit.tsslStatus.Text += ",但有错误发生,请查看";
									GlobalObject.SolutionExplorer.FocusAndExpandByID(Apq.Convert.ChangeType<int>(dr["SqlID"]));
								}
							}
						}
					});
				}
			}
		}
		#endregion

		#region cms1
		private void tsmiUndo_Click(object sender, EventArgs e)
		{
			this.Undo();
		}

		private void tsmiRedo_Click(object sender, EventArgs e)
		{
			this.Redo();
		}

		private void tsmiCut_Click(object sender, EventArgs e)
		{
			this.Copy();
			this.Delete();
		}

		private void tsmiCopy_Click(object sender, EventArgs e)
		{
			this.Copy();
		}

		private void tsmiPaste_Click(object sender, EventArgs e)
		{
			this.Paste();
		}

		private void tsmiSelectAll_Click(object sender, EventArgs e)
		{
			this.SelectAll();
		}
		#endregion

		#region ITextEditor 成员

		protected string _FileName = string.Empty;
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				Text = value;
				TabText = value;
			}
		}

		public void Save()
		{
			if (FileName.Length < 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "文本文件(*.txt;*.sql)|*.txt;*.sql|所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					FileName = saveFileDialog.FileName;
				}
				else
				{
					return;
				}
			}

			txtSql.SaveFile(FileName);
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			Save();
		}

		public void Copy()
		{
			System.Windows.Forms.Clipboard.SetData(DataFormats.UnicodeText,
				string.IsNullOrEmpty(txtSql.ActiveTextAreaControl.SelectionManager.SelectedText) ? txtSql.Text
				: txtSql.ActiveTextAreaControl.SelectionManager.SelectedText);
		}

		public void Delete()
		{
			txtSql.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
		}

		public void Open()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.RestoreDirectory = true;
			openFileDialog.Filter = "文本文件(*.txt;*.sql)|*.txt;*.sql|所有文件(*.*)|*.*";
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileName = openFileDialog.FileName;
				txtSql.LoadFile(FileName);
			}
		}

		public void Paste()
		{
			string str = System.Windows.Forms.Clipboard.GetData(DataFormats.UnicodeText) as string;
			if (str != null) txtSql.ActiveTextAreaControl.TextArea.InsertString(str);
		}

		public void Redo()
		{
			txtSql.Redo();
		}

		public void Reverse()
		{
		}

		public void SelectAll()
		{
			Point startPoint = new Point(0, 0);
			Point endPoint = txtSql.Document.OffsetToPosition(txtSql.Document.TextLength);
			if (txtSql.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
			{
				if (txtSql.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].StartPosition == startPoint &&
					txtSql.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].EndPosition == endPoint)
				{
					return;
				}
			}
			txtSql.ActiveTextAreaControl.SelectionManager.SetSelection(new DefaultSelection(txtSql.Document, startPoint, endPoint));
		}

		public void Undo()
		{
			txtSql.Undo();
		}

		#endregion

		public void LoadFile(string FileName)
		{
			this.FileName = FileName;
			txtSql.LoadFile(FileName);
		}
	}
}
