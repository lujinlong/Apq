using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace Apq.Data
{
	/// <summary>
	/// DataSet
	/// </summary>
	public class DataSet
	{
		#region CreateDefaultMapping
		/// <summary>
		/// 创建默认表列映射
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static System.Data.Common.DataTableMappingCollection CreateDefaultMapping(System.Data.DataSet ds)
		{
			DataTableMappingCollection rtn = new DataTableMappingCollection();

			foreach (System.Data.DataTable dt in ds.Tables)
			{
				DataTableMapping dtm = rtn.Add(dt.TableName, dt.TableName);
				foreach (DataColumn dc in dt.Columns)
				{
					dtm.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
				}
			}

			return rtn;
		}
		#endregion

		#region ExportToExcel
		/// <summary>
		/// 导出指定表到 Excel 文件
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="dtmc">需要导出的表列映射(空列表表示全部)</param>
		/// <param name="odc"></param>
		public static void ExportToExcel(System.Data.DataSet ds, System.Data.OleDb.OleDbConnection odc, System.Data.Common.DataTableMappingCollection dtmc)
		{
			if (dtmc == null || dtmc.Count == 0)
			{
				foreach (System.Data.DataTable dt in ds.Tables)
				{
					Apq.Data.DataTable.ExportToExcel(dt, odc, null);
				}
			}
			else
			{
				foreach (System.Data.Common.DataTableMapping dtm in dtmc)
				{
					Apq.Data.DataTable.ExportToExcel(ds.Tables[dtm.DataSetTable], odc, dtm.ColumnMappings);
				}
			}
		}

		/// <summary>
		/// 按最大行数限制组织表(新建表,转移数据,新表名为原表名+"续"+编号)
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="Maxrow">每个表最大行数</param>
		public static void BuildupTabelForMaxrow(System.Data.DataSet ds, int Maxrow)
		{
			int dtCount = ds.Tables.Count;
			for (int i = 0; i < dtCount; i++)
			{
				System.Data.DataTable dt = ds.Tables[i];
				int nAddNo = 0;
				while (dt.Rows.Count > Maxrow)
				{
					// 加入表
					System.Data.DataTable dtClone = dt.Clone();
					dtClone.TableName = dt.TableName + "续" + ++nAddNo;
					ds.Tables.Add(dtClone);

					// 转移数据
					for (int j = 0; j < Maxrow && dt.Rows.Count > Maxrow; j++)
					{
						dtClone.Rows.Add(dt.Rows[Maxrow].ItemArray);
						dt.Rows.RemoveAt(Maxrow);
					}
				}
			}
		}
		#endregion

		#region ExportToText
		/// <summary>
		/// [追加]导出指定表到文本文件
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="FileName"></param>
		/// <param name="strColSpliter">列分隔符</param>
		/// <param name="strRowSpliter">行分隔符</param>
		/// <param name="ContainsColName">是否包含列名</param>
		public static void ExportToText(System.Data.DataSet ds, string FileName, string strColSpliter, string strRowSpliter, bool ContainsColName)
		{
			foreach (System.Data.DataTable dt in ds.Tables)
			{
				Apq.Data.DataTable.ExportToText(dt, FileName, strColSpliter, strRowSpliter, ContainsColName);
			}
		}
		#endregion
	}
}
