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

			lblRowCount.Text += RowCount;

			cbExportType.SelectedIndex = Apq.Convert.ChangeType<int>(GlobalObject.XmlConfigChain[this.GetType(), "cbExportType"]);
			cbContainsColName.Checked = Apq.Convert.ChangeType<bool>(GlobalObject.XmlConfigChain[this.GetType(), "cbContainsColName"]);

			if (MaxRowCount > ExcelMaxRowNumber)
			{
				MessageBox.Show("存在最大行数超过 Excel 所支持的最大行数的表,导出为 Excel 时将自动导出分割为多个 Sheet。\r\n建议导出为文本文件.", "提示", MessageBoxButtons.OK);
			}
		}

		private void Export_FormClosed(object sender, FormClosedEventArgs e)
		{
			Apq.Win.GlobalObject.XmlUserConfig.Save();
			//GlobalObject.XmlUserConfig.Save();
		}

		// 确定
		private void btnConfirm_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				tsslStatus.Text = "导出中...";
				int RowCount = 0;
				foreach (DataTable dt in ds.Tables)
				{
					RowCount += dt.Rows.Count;
				}
				tspbProcess.Value = 0;
				tspbProcess.Maximum = RowCount;

				if (cbExportType.SelectedIndex == 0)
				{
					if (!ExportToText()) return;
				}
				if (cbExportType.SelectedIndex == 1)
				{
					if (!ExportToExcel()) return;
				}
				tsslStatus.Text = "导出完成";

				try
				{
					// 显示文件
					Process.Start(Apq.Dos.Common.EncodeParam(saveFileDialog1.FileName));
				}
				catch { }

				this.Close();
			}
		}

		private bool ExportToText()
		{
			tspbProcess.Maximum += cbContainsColName.Checked ? ds.Tables.Count : 0;

			string strColSpliter = cbColSpliter.Text;
			string strRowSpliter = cbRowSpliter.Text;
			// 输入检测:信息完整性
			if (strColSpliter.Length < 1)
			{
				MessageBox.Show("请设置正确的列分隔符");
				cbColSpliter.Focus();
				return false;
			}
			if (strRowSpliter.Length < 1)
			{
				MessageBox.Show("请设置正确的行分隔符");
				cbRowSpliter.Focus();
				return false;
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

			StreamWriter sw = File.CreateText(saveFileDialog1.FileName);
			// 执行导出
			try
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (cbContainsColName.Checked)
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
						sw.Flush();

						tspbProcess.Value++;
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
						sw.Flush();

						tspbProcess.Value++;
						Application.DoEvents();
					}
				}
				return true;
			}
			catch (System.IO.DirectoryNotFoundException)
			{
				MessageBox.Show("文件路径不正确", "输入错误");
				return false;
			}
			finally
			{
				sw.Close();
			}
		}
		private bool ExportToExcel()
		{
			try
			{
				// 整理数据集
				Apq.Data.DataSet.BuildupTabelForMaxrow(ds, ExcelMaxRowNumber);

				DataSet dsExcel = new DataSet();
				dsExcel.DataSetName = saveFileDialog1.FileName;
				foreach (DataTable dt in ds.Tables)
				{
					DataTable dtExcel = Apq.Data.DataTable.CloneToStringTable(dt);
					dtExcel.TableName = dt.TableName;
					dsExcel.Tables.Add(dtExcel);

					foreach (DataRow dr in dt.Rows)
					{
						DataRow drExcel = dtExcel.NewRow();
						foreach (DataColumn dc in dt.Columns)
						{
							drExcel[dc.ColumnName] = Apq.Data.SqlClient.Common.ConvertToSqlON(dr[dc]);
						}
						dtExcel.Rows.Add(drExcel);

						tspbProcess.Value++;
						Application.DoEvents();
					}
				}

				org.in2bits.MyXls.XlsDocument xd = new org.in2bits.MyXls.XlsDocument(dsExcel);
				FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
				xd.Save(fs);
				fs.Flush();
				fs.Close();
				return true;
			}
			catch (System.IO.DirectoryNotFoundException)
			{
				MessageBox.Show("文件路径不正确", "输入错误");
				return false;
			}
		}

		// 取消
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cbExportType_SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "cbExportType"] = cbExportType.SelectedIndex.ToString();

			// 设置过滤器
			if (cbExportType.Text == "Excel文件")
			{
				saveFileDialog1.FilterIndex = 2;
				saveFileDialog1.DefaultExt = "xls";
			}
			else
			{
				saveFileDialog1.FilterIndex = 1;
				saveFileDialog1.DefaultExt = "txt";
			}
		}

		private void cbContainsColName_CheckedChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlConfigChain[this.GetType(), "cbContainsColName"] = cbContainsColName.Checked.ToString();
		}
	}
}
