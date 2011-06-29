using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Apq.Com;
using System.Data.SqlClient;
using ApqDBManager.Forms;
using ICSharpCode.AvalonEdit;

namespace ApqDBManager.Controls
{
	public partial class ResultTable : UserControl
	{
		public ResultTable()
		{
			InitializeComponent();
		}

		private List<DataGridView> lstgc = new List<DataGridView>();

		private void ResultTable_Load(object sender, EventArgs e)
		{
		}

		private long _MaxShowRowCount = 100;
		/// <summary>
		/// 获取或设置显示的最大行数
		/// </summary>
		public long MaxShowRowCount
		{
			get { return _MaxShowRowCount; }
			set { _MaxShowRowCount = value; }
		}

		private DataSet _DisplayDataSet;
		/// <summary>
		/// 获取显示数据集
		/// </summary>
		public DataSet DisplayDataSet
		{
			get { return _DisplayDataSet; }
		}

		private DataSet _BackDataSet;
		/// <summary>
		/// 获取或设置后台数据集(实现重新绑定功能)
		/// </summary>
		public DataSet BackDataSet
		{
			get { return _BackDataSet; }
			set
			{
				if (value != null)
				{
					_BackDataSet = value;

					// 清掉当前显示内容
					if (_DisplayDataSet != null)
					{
						_DisplayDataSet.Clear();
						foreach (DataGridView item in lstgc)
						{
							Controls.Remove(item);
							//item.Dispose();
						}
						lstgc.Clear();
					}

					// 首先假设显示数据集即为后台数据集
					_DisplayDataSet = value;

					#region 需要复制数据时创建新显示数据集
					if (_MaxShowRowCount > 0)
					{
						bool NeedCopy = false;
						foreach (DataTable dtDisplay in _DisplayDataSet.Tables)
						{
							#region 检查列类型
							foreach (DataColumn dcDisplay in dtDisplay.Columns)
							{
								if (dcDisplay.DataType == typeof(byte[]))
								{
									NeedCopy = true;
									break;
								}
							}
							if (NeedCopy)
							{
								break;
							}
							#endregion

							// 检查行数
							if (dtDisplay.Rows.Count > _MaxShowRowCount)
							{
								NeedCopy = true;
								break;
							}
						}

						if (NeedCopy)
						{
							_DisplayDataSet = _BackDataSet.Clone();
							foreach (DataTable dt in _BackDataSet.Tables)
							{
								DataTable dtDisplay = _DisplayDataSet.Tables[dt.TableName];

								foreach (DataColumn dcDisplay in dtDisplay.Columns)
								{
									if (dcDisplay.DataType == typeof(byte[]))
									{
										dcDisplay.DataType = typeof(string);
									}
								}

								for (int r = 0; r < dt.Rows.Count && (MaxShowRowCount < 0 || r < MaxShowRowCount); r++)
								{
									DataRow drDisplay = dtDisplay.NewRow();
									for (int c = 0; c < dtDisplay.Columns.Count; c++)
									{
										if (dt.Columns[c].DataType == typeof(byte[]))
										{
											drDisplay[c] = Apq.Data.SqlClient.Common.ConvertToSqlON(dt.Rows[r][c]);
										}
										else
										{
											drDisplay[c] = dt.Rows[r][c];
										}
									}
									dtDisplay.Rows.Add(drDisplay);
								}
							}
						}
					}
					#endregion

					// 显示表格
					int nX = 3, nY = 9;// +teMsg.Size.Height;
					for (int i = 0; i < _DisplayDataSet.Tables.Count; i++, nY += 206)
					{
						DataTable dtDisplay = _DisplayDataSet.Tables[i];
						DataGridView dgv = new DataGridView();
						((System.ComponentModel.ISupportInitialize)(dgv)).BeginInit();

						//
						// dgv
						//
						Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dgv);
						dgv.Location = new System.Drawing.Point(nX, nY);
						dgv.Size = new System.Drawing.Size(this.Parent.Parent.Width - 40, 200);
						//gc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
						dgv.TabIndex = i + 100;
						dgv.AutoGenerateColumns = true;
						dgv.ReadOnly = true;
						dgv.AllowUserToAddRows = false;
						dgv.AllowUserToDeleteRows = false;
						//dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

						((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();

						tpRt.Controls.Add(dgv);
						lstgc.Add(dgv);

						// 绑定数据
						dgv.DataSource = _DisplayDataSet;
						dgv.DataMember = dtDisplay.TableName;
						Apq.Windows.Forms.DataGridViewHelper.AddBehaivor(dgv);

						// 禁止排序
						foreach (DataGridViewColumn gc in dgv.Columns)
						{
							gc.SortMode = DataGridViewColumnSortMode.NotSortable;
						}
					}
				}
			}
		}

		//导出
		private void tsbExport_Click(object sender, EventArgs e)
		{
			if (BackDataSet.Tables.Count > 0)
			{
				Export FormE = new Export(BackDataSet);
				FormE.ShowDialog(this);
			}

			//Apq.Windows.Forms.Wizard w = new Apq.Windows.Forms.Wizard();
			//w.StepIndex = 0;
			//w.btnBack.Click += new EventHandler(btnBack_Click);
			//w.btnNext.Click += new EventHandler(btnNext_Click);
			//w.btnFinish.Click += new EventHandler(btnFinish_Click);
			//w.btnCancel.Click += new EventHandler(btnCancel_Click);

			//ShowExportForm(w, 0, 1);
		}

		/// <summary>
		/// 获取数据库连接传回的消息并显示到界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void sc_InfoMessage(object sender, SqlInfoMessageEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<TabPage>(this, tpInfo, delegate(TabPage ctrl)
			{
				foreach (SqlError r in e.Errors)
				{
					if (r.Class > 0)
					{
						TextBox txtMsg = new TextBox();
						txtMsg.Multiline = true;
						txtMsg.ReadOnly = true;
						txtMsg.ScrollBars = ScrollBars.Vertical;
						txtMsg.WordWrap = false;
						txtMsg.AcceptsTab = true;
						txtMsg.AcceptsReturn = true;

						txtMsg.BorderStyle = BorderStyle.None;
						txtMsg.BackColor = System.Drawing.SystemColors.Control;
						txtMsg.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						txtMsg.Size = new System.Drawing.Size(tc.Width - 30, 20);

						int nY = 9;
						foreach (Control c in tpInfo.Controls)
						{
							nY += c.Size.Height;
							nY += 6;
						}
						txtMsg.Location = new System.Drawing.Point(3, nY);

						if (r.Class > 10)
						{
							txtMsg.Size = new System.Drawing.Size(this.Parent.Parent.Width - 40, 140);//7行
							//meMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
							txtMsg.ForeColor = System.Drawing.Color.Red;
							txtMsg.Text += string.Format("消息 {0}, 级别 {1}, 状态 {2}, 第 {3} 行\r\n", r.Number, r.Class, r.State, r.LineNumber);
						}

						txtMsg.Text += r.Message;

						tpInfo.Controls.Add(txtMsg);

						// 将错误记录到 SqlEdit 的 dsServers 和 dsUI
						SqlEdit se = null;
						Control ctrlTmp = this;
						int ServerID = 0;
						while (ctrlTmp != null)
						{
							ctrlTmp = ctrlTmp.Parent;
							se = ctrlTmp as SqlEdit;
							if (se != null)
							{
								break;
							}

							TabPage xtp = ctrlTmp as TabPage;
							if (xtp != null)
							{
								ServerID = Apq.Convert.ChangeType<int>(xtp.Tag);
							}
						}

						DataView dvErr = new DataView(se.SqlEditDoc.dsDBC.DBI);
						dvErr.RowFilter = "DBIID = " + ServerID;
						// 标记本服执行出错
						if (dvErr.Count > 0)
						{
							object rtLock = se.SqlEditDoc.GetLock(ServerID.ToString());
							lock (rtLock)
							{
								ErrList_XSD.ErrListRow drErrList = se.SqlEditDoc.dsErr.ErrList.NewErrListRow();
								drErrList.DBIID = ServerID;
								drErrList["__DBIName"] = dvErr[0]["SqlName"];
								drErrList.s = r.Message;
								se.SqlEditDoc.dsErr.ErrList.Rows.Add(drErrList);

								dvErr[0]["Err"] = true;
							}
						}
					}
				}
			});
		}
	}
}
