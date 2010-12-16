using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace Apq.Data
{
	/// <summary>
	/// DataTable
	/// </summary>
	public class DataTable
	{
		private System.Data.DataTable _Table;
		/// <summary>
		/// 获取表
		/// </summary>
		public System.Data.DataTable Table
		{
			get { return _Table; }
		}

		/// <summary>
		/// 装饰
		/// </summary>
		public DataTable(System.Data.DataTable Table)
		{
			_Table = Table;
		}

		#region 方法
		#region GetColNames
		/// <summary>
		/// 获取列名数组
		/// </summary>
		/// <param name="dt">表</param>
		/// <returns></returns>
		public static string[] GetColNames(System.Data.DataTable dt)
		{
			string[] rtn = new string[dt.Columns.Count];
			for (int i = 0; i < rtn.Length; i++)
			{
				rtn[i] = dt.Columns[i].ColumnName;
			}
			return rtn;
		}
		/// <summary>
		/// 获取列名数组
		/// </summary>
		/// <returns></returns>
		public string[] GetColNames()
		{
			return GetColNames(Table);
		}
		#endregion

		#region GetDistinct
		/// <summary>
		/// 获取不重复列表
		/// </summary>
		/// <param name="dtSrc">源表</param>
		/// <param name="DataColumnNames">列名列表</param>
		/// <returns></returns>
		public static System.Data.DataTable GetDistinct(System.Data.DataTable dtSrc, string[] DataColumnNames)
		{
			if (DataColumnNames == null || DataColumnNames.Length == 0)
			{
				DataColumnNames = GetColNames(dtSrc);
			}

			System.Data.DataTable dt = dtSrc.Copy();
			dt.Constraints.Clear();

			// 去掉多余列
			for (int i = dt.Columns.Count - 1; i >= 0; i--)
			{
				bool NeedDelete = true;
				foreach (string DataColumnName in DataColumnNames)
				{
					if (dt.Columns[i].ColumnName == DataColumnName)
					{
						NeedDelete = false;
						break;
					}
				}
				if (NeedDelete)
				{
					dt.Columns.RemoveAt(i);
				}
			}

			// 去掉重复行
			for (int r = 0; r < dt.Rows.Count; r++)
			{
				for (int f = dt.Rows.Count - 1; f > r; f--)
				{
					if (Apq.Data.DataRow.Equals(dt.Rows[r], dt.Rows[f]))
					{
						dt.Rows.RemoveAt(f);
					}
				}
			}
			return dt;
		}
		/// <summary>
		/// 获取不重复列表
		/// </summary>
		/// <param name="DataColumnNames">列名列表</param>
		/// <returns></returns>
		public System.Data.DataTable GetDistinct(string[] DataColumnNames)
		{
			return GetDistinct(Table, DataColumnNames);
		}
		#endregion

		#region CloneToStringTable
		/// <summary>
		/// 创建一个与源表 列名相同，列类型为字符串 的空表
		/// </summary>
		/// <param name="dtSrc">源表</param>
		public static System.Data.DataTable CloneToStringTable(System.Data.DataTable dtSrc)
		{
			System.Data.DataTable dt = new System.Data.DataTable();
			foreach (DataColumn dc in dtSrc.Columns)
			{
				dt.Columns.Add(dc.ColumnName);
			}
			return dt;
		}
		/// <summary>
		/// 创建一个与源表 列名相同，列类型为字符串 的空表
		/// </summary>
		public System.Data.DataTable CloneToStringTable()
		{
			return CloneToStringTable(Table);
		}
		#endregion

		#region TreeXml
		/// <summary>
		/// 将表转换到 XmlNode(树)
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="Columns">需要的列名列表</param>
		/// <param name="Key">主键列名(一列)</param>
		/// <param name="Parent">上级列名(一列)</param>
		/// <param name="xn">XmlNode</param>
		/// <param name="tag">结点名</param>
		/// <param name="ChildrenFilter">子行筛选</param>
		public static void ConvertTreeXmlNode(System.Data.DataTable dt, string[] Columns, string Key, string Parent,
			XmlNode xn, string tag, string ChildrenFilter)
		{
			XmlDocument xd = xn.OwnerDocument;
			tag = string.IsNullOrEmpty(tag) ? dt.TableName : tag;

			System.Data.DataView dv = new System.Data.DataView(dt);
			dv.RowFilter = ChildrenFilter;
			foreach (DataRowView drv in dv)
			{
				XmlElement xe = xd.CreateElement(tag);
				xn.AppendChild(xe);
				for (int c = 0; c < Columns.Length; c++)
				{
					XmlAttribute xa = xd.CreateAttribute(Columns[c]);
					xa.Value = drv[Columns[c]].ToString();
					xe.Attributes.Append(xa);
				}

				ConvertTreeXmlNode(dt, Columns, Key, Parent, xe, tag, Parent + " = " + drv[Key]);
			}
		}

		/// <summary>
		/// 从 XmlNode(树) 加载表
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="Columns">需要的列名列表</param>
		/// <param name="xn">XmlNode</param>
		public static void LoadTreeXmlNode(System.Data.DataTable dt, string[] Columns,
			XmlNode xn)
		{
			System.Data.DataRow dr = dt.NewRow();
			foreach (string ColName in Columns)
			{
				if (xn.Attributes[ColName] != null)
				{
					dr[ColName] = xn.Attributes[ColName].Value;
				}
			}
			dt.Rows.Add(dr);

			foreach (XmlNode xnc in xn.ChildNodes)
			{
				LoadTreeXmlNode(dt, Columns, xnc);
			}
		}

		/// <summary>
		/// 将表转换到 XmlNode(树)
		/// </summary>
		/// <param name="Columns">需要的列名列表</param>
		/// <param name="Key">主键列名(一列)</param>
		/// <param name="Parent">上级列名(一列)</param>
		/// <param name="xn">XmlNode</param>
		/// <param name="tag">结点名</param>
		/// <param name="ChildrenFilter">子行筛选</param>
		public void ConvertTreeXmlNode(string[] Columns, string Key, string Parent,
			XmlNode xn, string tag, string ChildrenFilter)
		{
			ConvertTreeXmlNode(Table, Columns, Key, Parent, xn, tag, ChildrenFilter);
		}

		/// <summary>
		/// 从 XmlNode(树) 加载表
		/// </summary>
		/// <param name="Columns">需要的列名列表</param>
		/// <param name="xn">XmlNode</param>
		public void LoadTreeXmlNode(string[] Columns,
			XmlNode xn)
		{
			LoadTreeXmlNode(Table, Columns, xn);
		}
		#endregion

		#region Excel

		/// <summary>
		/// 在 Excel 文件中添加该表映射的页(Worksheet)和列.(对已存在的页则清空原数据)
		/// </summary>
		/// <param name="odc">已打开的连接</param>
		/// <param name="dtm">该表的映射关系</param>
		[Obsolete("此方法暂未启用:未完成")]
		public static void BuildExcelSheet(System.Data.OleDb.OleDbConnection odc, System.Data.Common.DataTableMapping dtm)
		{
			// 获取已有结构
			System.Data.DataSet dsSheet = new System.Data.DataSet();
			dsSheet.Tables.Add(odc.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null));
			System.Data.OleDb.OleDbDataAdapter odda = new System.Data.OleDb.OleDbDataAdapter(string.Empty, odc);
			foreach (System.Data.DataRow dr in dsSheet.Tables[0].Rows)
			{
				string SheetName = dr[2].ToString().Trim('\'');
				odda.SelectCommand.CommandText = "SELECT * FROM [" + SheetName + "]";
				odda.FillSchema(dsSheet, SchemaType.Mapped, SheetName);
			}
		}

		/// <summary>
		/// 导出到 Excel 文件
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="odc"></param>
		/// <param name="dcmc">列映射</param>
		public static void ExportToExcel(System.Data.DataTable dt, System.Data.OleDb.OleDbConnection odc, System.Data.Common.DataColumnMappingCollection dcmc)
		{
			System.Data.Common.DataColumnMappingCollection dcmc1 = new System.Data.Common.DataColumnMappingCollection();
			if (dcmc == null || dcmc.Count == 0)
			{
				foreach (DataColumn dc in dt.Columns)
				{
					dcmc1.Add(dc.ColumnName, dc.ColumnName);
				}
			}
			else
			{
				dcmc1 = dcmc;
			}

			string[] ColNames = Apq.Data.Common.DataColumnMappingCollectionHelper.GetSourceColNames(dcmc1);
			string strColNames = string.Join(",", ColNames);
			string strInsert = Apq.Data.Common.SQLHelper.BuildInsertSegment("[" + dt.TableName + "$]", strColNames, true);

			try
			{
				Apq.Data.Common.DbConnectionHelper.Open(odc);
				foreach (System.Data.DataRow dr in dt.Rows)
				{
					string[] aryRows = new string[ColNames.Length];
					for (int i = 0; i < aryRows.Length; i++)
					{
						aryRows[i] = Apq.Data.SqlClient.Common.ConvertToSqlON(SqlDbType.VarChar, dr[ColNames[i]]);
					}
					string strRows = string.Join(",", aryRows);
					string strValues = Apq.Data.Common.SQLHelper.BuildValuesSegment(strRows);
					string sql = string.Format("{0}{1}", strInsert, strValues);
					try
					{
						System.Data.Common.DbCommand dbcmd = odc.CreateCommand();
						dbcmd.CommandText = sql;
						dbcmd.ExecuteNonQuery();
					}
					catch (System.Exception ex)
					{
						Apq.GlobalObject.ApqLog.Warn(ex.Message);
					}
				}
			}
			catch (System.Exception ex)
			{
				Apq.GlobalObject.ApqLog.Warn(ex.Message);
			}
		}
		/// <summary>
		/// 导出到 Excel 文件
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="odc"></param>
		public static void ExportToExcel(System.Data.DataTable dt, System.Data.OleDb.OleDbConnection odc)
		{
			ExportToExcel(dt, odc, null);
		}
		/// <summary>
		/// 导出到 Excel 文件
		/// </summary>
		/// <param name="odc"></param>
		/// <param name="dcmc">列映射</param>
		public void ExportToExcel(System.Data.OleDb.OleDbConnection odc, System.Data.Common.DataColumnMappingCollection dcmc)
		{
			ExportToExcel(Table, odc, dcmc);
		}
		/// <summary>
		/// 导出到 Excel 文件
		/// </summary>
		/// <param name="odc"></param>
		public void ExportToExcel(System.Data.OleDb.OleDbConnection odc)
		{
			ExportToExcel(Table, odc);
		}
		#endregion

		#region Text

		/// <summary>
		/// 导出到文本文件
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="FileName"></param>
		/// <param name="strColSpliter">列分隔符</param>
		/// <param name="strRowSpliter">行分隔符</param>
		/// <param name="ContainsColName">是否包含列名</param>
		public static void ExportToText(System.Data.DataTable dt, string FileName, string strColSpliter, string strRowSpliter, bool ContainsColName)
		{
			using (StreamWriter sw = File.AppendText(FileName))
			{
				if (ContainsColName)
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
				}

				foreach (System.Data.DataRow dr in dt.Rows)
				{
					for (int i = 0; i < dr.ItemArray.Length; i++)
					{
						object obj = dr.ItemArray[i];
						sw.Write(obj);
						if (i < dr.ItemArray.Length - 1)
						{
							sw.Write(strColSpliter);
						}
					}
					sw.Write(strRowSpliter);
				}

				sw.Flush();
				sw.Close();
			}
		}

		/// <summary>
		/// 导出到文本文件
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="FileName"></param>
		/// <param name="strColSpliter">列分隔符</param>
		/// <param name="strRowSpliter">行分隔符</param>
		public static void ExportToText(System.Data.DataTable dt, string FileName, string strColSpliter, string strRowSpliter)
		{
			ExportToText(dt, FileName, strColSpliter, strRowSpliter, false);
		}
		/// <summary>
		/// 导出到文本文件
		/// </summary>
		/// <param name="FileName"></param>
		/// <param name="strColSpliter">列分隔符</param>
		/// <param name="strRowSpliter">行分隔符</param>
		/// <param name="ContainsColName">是否包含列名</param>
		public void ExportToText(string FileName, string strColSpliter, string strRowSpliter, bool ContainsColName)
		{
			ExportToText(Table, FileName, strColSpliter, strRowSpliter, ContainsColName);
		}
		/// <summary>
		/// 导出到文本文件
		/// </summary>
		/// <param name="FileName"></param>
		/// <param name="strColSpliter">列分隔符</param>
		/// <param name="strRowSpliter">行分隔符</param>
		public void ExportToText(string FileName, string strColSpliter, string strRowSpliter)
		{
			ExportToText(Table, FileName, strColSpliter, strRowSpliter, false);
		}
		#endregion
		#endregion
	}
}
