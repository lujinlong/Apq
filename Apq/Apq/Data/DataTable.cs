using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.Common;

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
		#region CreateDefaultTableMapping
		/// <summary>
		/// 生成默认映射表
		/// </summary>
		/// <param name="dt">表</param>
		/// <returns></returns>
		public static DataTableMapping CreateDefaultTableMapping(System.Data.DataTable dt)
		{
			DataTableMapping rtn = new DataTableMapping();
			rtn.SourceTable = rtn.DataSetTable = dt.TableName;
			foreach (DataColumn dc in dt.Columns)
			{
				rtn.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
			}
			return rtn;
		}
		/// <summary>
		/// 生成默认映射表
		/// </summary>
		/// <returns></returns>
		public DataTableMapping CreateDefaultTableMapping()
		{
			return CreateDefaultTableMapping(Table);
		}
		#endregion

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

		#region GetRootRows
		/// <summary>
		/// 获取根行
		/// </summary>
		/// <param name="dt">表</param>
		/// <param name="Key">主键列名(一列)</param>
		/// <param name="Parent">上级列名(一列)</param>
		public static List<System.Data.DataRow> GetRootRows(System.Data.DataTable dt, string Key, string Parent)
		{
			List<System.Data.DataRow> Rows = new List<System.Data.DataRow>();
			System.Data.DataView dv = new System.Data.DataView(dt);
			//dv.RowFilter = ChildrenFilter;

			foreach (System.Data.DataRow dr in dt.Rows)
			{
				if (Apq.Convert.LikeDBNull(dr[Parent]))
				{
					Rows.Add(dr);
					continue;
				}

				string strSqlValue = Apq.Data.SqlClient.Common.ConvertToSqlON(dr[Parent]);
				System.Data.DataRow[] p = dt.Select(Key + " = " + strSqlValue);
				if (p == null || p.Length == 0)
				{
					Rows.Add(dr);
				}
			}
			return Rows;
		}

		/// <summary>
		/// 获取根行
		/// </summary>
		/// <param name="Key">主键列名(一列)</param>
		/// <param name="Parent">上级列名(一列)</param>
		public List<System.Data.DataRow> GetRootRows(string Key, string Parent)
		{
			return GetRootRows(_Table, Key, Parent);
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
		public static void ExportToText(System.Data.DataTable dt, string FileName, string strColSpliter, string strRowSpliter, bool ContainsColName = false)
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
		/// <param name="FileName"></param>
		/// <param name="strColSpliter">列分隔符</param>
		/// <param name="strRowSpliter">行分隔符</param>
		/// <param name="ContainsColName">是否包含列名</param>
		public void ExportToText(string FileName, string strColSpliter, string strRowSpliter, bool ContainsColName = false)
		{
			ExportToText(Table, FileName, strColSpliter, strRowSpliter, ContainsColName);
		}
		#endregion
		#endregion
	}
}
