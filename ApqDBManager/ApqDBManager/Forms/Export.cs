using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;

namespace ApqDBManager
{
	public partial class Export : Apq.Windows.Forms.ImeForm
	{
		[Obsolete("仅为能打开设计视图而保留")]
		private Export()
		{
			InitializeComponent();
		}

		public int ExcelMaxRowNumber = 65500;

		/// <summary>
		/// Export
		/// </summary>
		/// <param name="ds">数据集</param>
		public Export(DataSet ds)
		{
			InitializeComponent();

			this.ds = ds;
			foreach (DataTable dt in ds.Tables)
			{
				RowCount += dt.Rows.Count;
				MaxRowCount = MaxRowCount > dt.Rows.Count ? MaxRowCount : dt.Rows.Count;
			}
		}

		private DataSet _ds;
		/// <summary>
		/// 获取或设置导出数据集
		/// </summary>
		public DataSet ds
		{
			get { return _ds; }
			set { _ds = value; }
		}

		private long RowCount = 0;
		private int MaxRowCount = 0;

		private void Export_Load(object sender, EventArgs e)
		{
			//ShowInTaskbar = false;
			Icon = new Icon(Application.StartupPath + @"\Res\ico\sign141.ico");

			lcRowCount.Text += RowCount;

			string cfgcbExportType = GlobalObject.XmlConfigChain[this.GetType(), "cbExportType"];
			if (cfgcbExportType != null)
			{
				cbExportType.Text = cfgcbExportType;
			}
			string cfgceContainsColName = GlobalObject.XmlConfigChain[this.GetType(), "ceContainsColName"];
			if (cfgceContainsColName != null)
			{
				ceContainsColName.Checked = Apq.Convert.ChangeType<bool>(cfgceContainsColName);
			}
			string cfgcbColSpliter = GlobalObject.XmlConfigChain[this.GetType(), "cbColSpliter"];
			if (cfgcbColSpliter != null)
			{
				cbColSpliter.Text = cfgcbColSpliter;
			}
			string cfgcbRowSpliter = GlobalObject.XmlConfigChain[this.GetType(), "cbRowSpliter"];
			if (cfgcbRowSpliter != null)
			{
				cbRowSpliter.Text = cfgcbRowSpliter;
			}

			if (MaxRowCount > ExcelMaxRowNumber)
			{
				MessageBox.Show("存在最大行数超过 Excel 所支持的最大行数的表,导出为 Excel 时将自动导出分割为多个 Sheet。\r\n建议导出为文本文件.", "提示", MessageBoxButtons.OK);
			}
		}

		private void Export_FormClosed(object sender, FormClosedEventArgs e)
		{
			//Apq.Win.GlobalObject.XmlUserConfig.Save();
			//GlobalObject.XmlUserConfig.Save();
		}

		// 确定
		private void btnConfirm_Click(object sender, EventArgs e)
		{
			// 输入检测:信息完整性
			if (saveFile1.FileName.Length < 2)
			{
				MessageBox.Show("请选择正确的导出文件");
				saveFile1.Focus();
				return;
			}

			bsiStatus.Caption = "导出中...";
			int RowCount = 0;
			foreach (DataTable dt in ds.Tables)
			{
				RowCount += dt.Rows.Count;
			}
			ripb.Maximum = RowCount;

			if (cbExportType.Text == cbExportType.Properties.Items[0].ToString())
			{
				ExportToText();
			}
			if (cbExportType.Text == cbExportType.Properties.Items[1].ToString())
			{
				ExportToExcel();
			}
			bsiStatus.Caption = "导出完成";

			try
			{
				// 显示文件
				Process.Start(Apq.Dos.Common.EncodeParam(saveFile1.FileName));
			}
			catch { }

			this.Close();
		}

		private void ExportToText()
		{
			ripb.Maximum += ceContainsColName.Checked ? ds.Tables.Count : 0;

			string strColSpliter = cbColSpliter.Text;
			string strRowSpliter = cbRowSpliter.Text;
			// 输入检测:信息完整性
			if (strColSpliter.Length < 1)
			{
				MessageBox.Show("请设置正确的列分隔符");
				cbColSpliter.Focus();
				return;
			}
			if (strRowSpliter.Length < 1)
			{
				MessageBox.Show("请设置正确的行分隔符");
				cbRowSpliter.Focus();
				return;
			}

			//分隔符转换
			switch (strColSpliter)
			{
				case "\\t":
					strColSpliter = "\t";
					break;
			}
			switch (strRowSpliter)
			{
				case "\\r\\n":
					strRowSpliter = "\r\n";
					break;
			}

			// 执行导出
			using (StreamWriter sw = File.CreateText(saveFile1.FileName))
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (ceContainsColName.Checked)
					{
						for (int i = 0; i < dt.Columns.Count; i++)
						{
							DataColumn dc = dt.Columns[i];
							sw.Write(dc.ColumnName);
							if (i < dt.Columns.Count - 1)
							{
								sw.Write(strColSpliter);
							}
						}
						sw.Write(strRowSpliter);

						beiProcess.EditValue = Apq.Convert.ChangeType<int>(beiProcess.EditValue) + 1;
						Application.DoEvents();
					}
					foreach (DataRow dr in dt.Rows)
					{
						for (int i = 0; i < dr.ItemArray.Length; i++)
						{
							object obj = dr.ItemArray[i];
							string str = Apq.Data.SqlClient.Common.ConvertToSqlON(obj);
							sw.Write(str);
							if (i < dr.ItemArray.Length - 1)
							{
								sw.Write(strColSpliter);
							}
						}
						sw.Write(strRowSpliter);

						beiProcess.EditValue = Apq.Convert.ChangeType<int>(beiProcess.EditValue) + 1;
						Application.DoEvents();
					}
				}
			}
		}
		private void ExportToExcel()
		{
			// 整理数据集
			Apq.Data.DataSet.BuildupTabelForMaxrow(ds, ExcelMaxRowNumber);

			DataSet dsExcel = new DataSet();
			dsExcel.DataSetName = ds.DataSetName;
			foreach (DataTable dt in ds.Tables)
			{
				DataTable dtExcel = Apq.Data.DataTable.CloneToStringTable(dt);
				foreach (DataRow dr in dt.Rows)
				{
					DataRow drExcel = dtExcel.NewRow();
					foreach (DataColumn dc in dt.Columns)
					{
						drExcel[dc.ColumnName] = Apq.Data.SqlClient.Common.ConvertToSqlON(dr[dc]);
					}
					dtExcel.Rows.Add(drExcel);

					beiProcess.EditValue = Apq.Convert.ChangeType<int>(beiProcess.EditValue) + 1;
					Application.DoEvents();
				}
				dsExcel.Tables.Add(dtExcel);
			}

			FileStream fs = new FileStream(saveFile1.FileName, FileMode.Create);
			org.in2bits.MyXls.XlsDocument xd = new org.in2bits.MyXls.XlsDocument(dsExcel);
			xd.Save(fs);
			fs.Flush();
			fs.Close();
		}

		// 取消
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cbExportType_SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "cbExportType"] = cbExportType.Text;

			// 设置过滤器
			if (cbExportType.Text == "Excel文件")
			{
				saveFile1.SaveFileDialog.FilterIndex = 2;
			}
			else
			{
				saveFile1.SaveFileDialog.FilterIndex = 1;
			}
		}

		private void ceContainsColName_CheckedChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "ceContainsColName"] = ceContainsColName.Checked.ToString();
		}

		private void cbColSpliter_SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "cbColSpliter"] = cbColSpliter.Text;
		}

		private void cbRowSpliter_SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "cbRowSpliter"] = cbRowSpliter.Text;
		}
	}
}
