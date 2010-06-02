using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Apq.Com
{
	/// <summary>
	/// Excel
	/// </summary>
	public class Excel
	{
		/// <summary>
		/// Excel2007之前版本 支持的最大行数
		/// </summary>
		public const int MaxRowNumber = 65500;
		/// <summary>
		/// Excel2007 支持的最大行数
		/// </summary>
		public const int MaxRowNumber2007 = 1048000;

		#region BuildSheets
		/// <summary>
		/// [Microsoft.Office.Interop.Excel]建立表列结构(保留不使用的页(Worksheet).(创建/覆盖文件)
		/// </summary>
		/// <param name="FileName">文件路径</param>
		/// <param name="ds">数据集</param>
		public static void BuildSheets(string FileName, DataSet ds)
		{
			DataTableMappingCollection dtmc = Apq.Data.DataSet.CreateDefaultMapping(ds);
			BuildSheets(FileName, dtmc);
		}

		/// <summary>
		/// [Microsoft.Office.Interop.Excel]建立表列结构(保留不使用的页(Worksheet).(创建/覆盖文件)
		/// </summary>
		/// <param name="FileName">文件路径</param>
		/// <param name="dtmc">取其 Source 属性值建立空文件</param>
		public static void BuildSheets(string FileName, DataTableMappingCollection dtmc)
		{
			Application app = new Application();
			try
			{
				Workbook wb;
				if (File.Exists(FileName))
				{
					wb = app.Workbooks.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
						, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				}
				else
				{
					wb = app.Workbooks.Add(Type.Missing);
				}

				foreach (DataTableMapping dtm in dtmc)
				{
					try
					{
						Worksheet ws;
						try
						{
							ws = wb.Worksheets[dtm.SourceTable] as Worksheet;
							// 清空表
							ws.UsedRange.ClearContents();
						}
						catch
						{
							ws = wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing) as Worksheet;
							// 表名
							ws.Name = dtm.SourceTable;
						}

						((_Worksheet)ws).Activate();
						// 列名
						for (int i = 1; i <= dtm.ColumnMappings.Count; i++)
						{
							DataColumnMapping dcm = dtm.ColumnMappings[i - 1];
							ws.Cells[1, i] = dcm.SourceColumn;
						}
					}
					catch (System.Exception ex)
					{
						Apq.GlobalObject.ApqLog.Warn(string.Format("构建文件结构[{0}]失败:", dtm.SourceTable), ex);
					}
				}

				// 保存
				if (File.Exists(FileName))
				{
					wb.Save();
				}
				else
				{
					wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
						, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, true);
				}

				// 关闭
				wb.Close(XlSaveAction.xlDoNotSaveChanges, FileName, false);
				wb = null;
			}
			finally
			{
				try
				{
					Int64 appID = 0;
					IntPtr hwnd = new IntPtr(app.Hwnd);
					int appThreadID = Apq.DllImports.User32.GetWindowThreadProcessId(hwnd, ref appID);

					// 退出 Excel
					app.Quit();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

					if (appID > 0)
					{
						try
						{
							Process pr = Process.GetProcessById(System.Convert.ToInt32(appID));
							pr.Kill();
						}
						catch { }
					}
					app = null;
					GC.Collect();
				}
				catch (System.Exception ex)
				{
					Apq.GlobalObject.ApqLog.Warn("关闭Excel失败", ex);
				}
			}
		}
		#endregion

		/// <summary>
		/// 建立新文件并保存(要求文件不存在)
		/// </summary>
		/// <param name="FileName">文件路径</param>
		[Obsolete("此方法暂未启用")]
		public static void CreateNewFile(string FileName)
		{
			Application app = new Application();
			try
			{
				Workbook wb = app.Workbooks.Add(Type.Missing);

				// 保存
				wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
					, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, true);

				// 关闭
				wb.Close(XlSaveAction.xlDoNotSaveChanges, FileName, false);
			}
			finally
			{
				try
				{
					Int64 appID = 0;
					IntPtr hwnd = new IntPtr(app.Hwnd);
					int appThreadID = Apq.DllImports.User32.GetWindowThreadProcessId(hwnd, ref appID);

					// 退出 Excel
					app.Quit();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

					if (appID > 0)
					{
						try
						{
							Process pr = Process.GetProcessById(System.Convert.ToInt32(appID));
							pr.Kill();
						}
						catch { }
					}
					app = null;
					GC.Collect();
				}
				catch (System.Exception ex)
				{
					Apq.GlobalObject.ApqLog.Warn("关闭Excel失败", ex);
				}
			}
		}

		/// <summary>
		/// 将 DataTabel 写入到 Worksheet
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="ws"></param>
		/// <param name="RowNumber">从 Worksheet 的第几行开始写入</param>
		public void WriteDataTabelToWorksheet(System.Data.DataTable dt, Worksheet ws, int RowNumber)
		{
			foreach (DataRow dr in dt.Rows)
			{
				for (int i = 1; i <= dr.ItemArray.Length; i++)
				{
					object obj = dr[i - 1];
					ws.Cells[RowNumber, i] = obj;
				}

				RowNumber++;
			}
		}
	}
}
