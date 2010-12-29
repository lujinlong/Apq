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
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using DevExpress.XtraTab;

namespace ApqDBManager.Controls
{
	public partial class ResultTable : UserControl
	{
		public ResultTable()
		{
			InitializeComponent();
		}

		private List<GridControl> lstgc = new List<GridControl>();

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
						foreach (GridControl item in lstgc)
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
						GridControl gc = new GridControl();
						GridView gv = new DevExpress.XtraGrid.Views.Grid.GridView();
						gc.BeginInit();
						gv.BeginInit();

						//
						// gc
						//
						gc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
						gc.EmbeddedNavigator.Name = "";
						gc.Location = new System.Drawing.Point(nX, nY);
						gc.MainView = gv;
						//gc.Name = "gc";
						//gc.Size = new System.Drawing.Size(674, 200);
						gc.Size = new System.Drawing.Size(Width - 30, 200);
						gc.TabIndex = i + 100;
						gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gv });

						//
						// gv
						//
						gv.GridControl = gc;
						//gv.Name = "gv";
						gv.OptionsView.ColumnAutoWidth = false;
						gv.OptionsSelection.MultiSelect = true;
						gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
						gv.OptionsBehavior.Editable = false;
						gv.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
						gv.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(Apq.Xtra.Grid.Common.gv_CustomDrawRowIndicator);

						gc.EndInit();
						gv.EndInit();

						tpRt.Controls.Add(gc);
						lstgc.Add(gc);

						// 绑定数据
						gc.DataSource = _DisplayDataSet;
						gc.DataMember = dtDisplay.TableName;
						Apq.Xtra.Grid.Common.ShowTime(gv);

						//Graphics g = Graphics.FromHwnd(Handle);
						//SizeF sf = g.MeasureString(dt.Rows.Count.ToString(), gv.Appearance.RowSeparator.Font);
						//Size s = sf.ToSize();
						//gv.IndicatorWidth = Apq.Convert.ChangeType<int>(s.Width + 10);
						gv.IndicatorWidth = 90;
						gv.Invalidate();
						gv.BestFitColumns();
						Apq.Xtra.Grid.Common.AddBehaivor(gv);
					}
				}
			}
		}

		//导出
		private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
		//未使用
		void btnBack_Click(object sender, EventArgs e)
		{
			SimpleButton btn = sender as SimpleButton;
			if (btn != null)
			{
				Apq.Windows.Forms.Wizard w = btn.FindForm() as Apq.Windows.Forms.Wizard;
				if (w != null)
				{
					ShowExportForm(w, w.StepIndex, w.StepIndex - 1);
				}
			}
		}
		//未使用
		void btnNext_Click(object sender, EventArgs e)
		{
			SimpleButton btn = sender as SimpleButton;
			if (btn != null)
			{
				Apq.Windows.Forms.Wizard w = btn.FindForm() as Apq.Windows.Forms.Wizard;
				if (w != null)
				{
					ShowExportForm(w, w.StepIndex, w.StepIndex + 1);
				}
			}
		}
		//未使用
		void btnFinish_Click(object sender, EventArgs e)
		{
			SimpleButton btn = sender as SimpleButton;
			if (btn != null)
			{
				Apq.Windows.Forms.Wizard w = btn.FindForm() as Apq.Windows.Forms.Wizard;
				if (w != null)
				{
					ShowExportForm(w, w.StepIndex, 3);
				}
			}
		}
		//未使用
		void btnCancel_Click(object sender, EventArgs e)
		{
			SimpleButton btn = sender as SimpleButton;
			if (btn != null)
			{
				Apq.Windows.Forms.Wizard w = btn.FindForm() as Apq.Windows.Forms.Wizard;
				if (w != null)
				{
					w.Close();
				}
			}
		}
		//未使用
		/// <summary>
		/// 向导窗口由 第nStep1步 进入 第nStep2步{0:未显示,1:选择表,2:选择目标,3:完成}
		/// </summary>
		/// <param name="w"></param>
		/// <param name="nStep1">0-->n</param>
		/// <param name="nStep2">0-->n</param>
		private void ShowExportForm(Apq.Windows.Forms.Wizard w, int nStep1, int nStep2)
		{
			// 完成
			if (nStep2 == 3)
			{
			}

			// 显示第一步
			else if (nStep1 == 0)
			{
			}

			// 第一步 --> 第二步
			else if (nStep1 == 1 && nStep2 == 2)
			{
			}

			// 第二步 --> 第一步
			else if (nStep1 == 2 && nStep2 == 1)
			{
			}
		}

		/// <summary>
		/// 获取数据库连接传回的消息并显示到界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void sc_InfoMessage(object sender, SqlInfoMessageEventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<DevExpress.XtraTab.XtraTabPage>(this, tpInfo, delegate(DevExpress.XtraTab.XtraTabPage ctrl)
			{
				foreach (SqlError r in e.Errors)
				{
					if (r.Class > 0)
					{
						MemoEdit meMsg = new MemoEdit();
						meMsg.Properties.ReadOnly = true;
						meMsg.Properties.WordWrap = false;
						meMsg.Properties.AcceptsTab = true;
						meMsg.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
						meMsg.Properties.Appearance.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						meMsg.Properties.Appearance.Options.UseBackColor = true;
						meMsg.Properties.Appearance.Options.UseFont = true;
						meMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;

						meMsg.Size = new System.Drawing.Size(tpInfo.TabControl.Width - 30, 20);
						//meMsg.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
						int nY = 9;
						foreach (Control c in tpInfo.Controls)
						{
							nY += c.Size.Height;
							nY += 6;
						}
						meMsg.Location = new System.Drawing.Point(3, nY);

						if (r.Class > 10)
						{
							meMsg.Size = new System.Drawing.Size(tpInfo.TabControl.Width - 30, 140);//7行
							meMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
							meMsg.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
							meMsg.Properties.Appearance.Options.UseForeColor = true;
							meMsg.Text += string.Format("消息 {0}，级别 {1}，状态 {2}，第 {3} 行\r\n", r.Number, r.Class, r.State, r.LineNumber);
						}

						meMsg.Text += r.Message;

						tpInfo.Controls.Add(meMsg);

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

							XtraTabPage xtp = ctrlTmp as XtraTabPage;
							if (xtp != null)
							{
								ServerID = Apq.Convert.ChangeType<int>(xtp.Tag);
							}
						}

						DataView dvErr = new DataView(se.dsServers.dtServers);
						dvErr.RowFilter = "ID = " + ServerID;
						// 标记本服执行出错
						if (dvErr.Count > 0)
						{
							object rtLock = se.GetLock(ServerID.ToString());
							lock (rtLock)
							{
								XSD.UI.ErrListRow drErrList = se.dsUI.ErrList.NewErrListRow();
								drErrList.RSrvID = ServerID;
								drErrList["__ServerName"] = dvErr[0]["Name"];
								drErrList.s = r.Message;
								se.dsUI.ErrList.Rows.Add(drErrList);

								dvErr[0]["Err"] = true;
							}
						}
					}
				}
			});
		}
	}
}
