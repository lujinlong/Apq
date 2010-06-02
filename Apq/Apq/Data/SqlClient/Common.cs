using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Apq.Data.SqlClient
{
	/// <summary>
	/// 公用模块
	/// </summary>
	public class Common
	{
		/// <summary>
		/// 对于不支持的来源类型返回的默认值
		/// </summary>
		public static string NonSupportedInType = "'<不支持的来源类型>'";
		/// <summary>
		/// 对于不支持的目标类型返回的默认值
		/// </summary>
		public static string NonSupportedOutType = "'<不支持的输出类型>'";

		#region htSqlToCS
		private static Dictionary<SqlDbType, Type> _htSqlToCS = null;
		/// <summary>
		/// 获取SQL数据库类型与C#类型的对应表
		/// </summary>
		protected static Dictionary<SqlDbType, Type> htSqlToCS
		{
			get
			{
				if (_htSqlToCS == null)
				{
					_htSqlToCS = new Dictionary<SqlDbType, Type>();
					_htSqlToCS.Add(SqlDbType.BigInt, typeof(long));
					_htSqlToCS.Add(SqlDbType.Binary, typeof(byte[]));
					_htSqlToCS.Add(SqlDbType.Bit, typeof(bool));
					_htSqlToCS.Add(SqlDbType.Char, typeof(string));
					_htSqlToCS.Add(SqlDbType.Date, typeof(DateTime));
					_htSqlToCS.Add(SqlDbType.DateTime, typeof(DateTime));
					_htSqlToCS.Add(SqlDbType.DateTime2, typeof(DateTime));
					_htSqlToCS.Add(SqlDbType.DateTimeOffset, typeof(System.DateTimeOffset));
					_htSqlToCS.Add(SqlDbType.Decimal, typeof(decimal));
					_htSqlToCS.Add(SqlDbType.Float, typeof(double));
					_htSqlToCS.Add(SqlDbType.Image, typeof(byte[]));
					_htSqlToCS.Add(SqlDbType.Int, typeof(int));
					_htSqlToCS.Add(SqlDbType.Money, typeof(Single));
					_htSqlToCS.Add(SqlDbType.NChar, typeof(string));
					_htSqlToCS.Add(SqlDbType.NText, typeof(string));
					_htSqlToCS.Add(SqlDbType.NVarChar, typeof(string));
					_htSqlToCS.Add(SqlDbType.Real, typeof(Single));
					_htSqlToCS.Add(SqlDbType.SmallDateTime, typeof(DateTime));
					_htSqlToCS.Add(SqlDbType.SmallInt, typeof(Int16));
					_htSqlToCS.Add(SqlDbType.SmallMoney, typeof(Single));
					_htSqlToCS.Add(SqlDbType.Structured, typeof(System.Data.DataTable));
					_htSqlToCS.Add(SqlDbType.Text, typeof(string));
					_htSqlToCS.Add(SqlDbType.Time, typeof(DateTime));
					_htSqlToCS.Add(SqlDbType.Timestamp, typeof(byte[]));
					_htSqlToCS.Add(SqlDbType.TinyInt, typeof(byte));
					_htSqlToCS.Add(SqlDbType.Udt, typeof(object));
					_htSqlToCS.Add(SqlDbType.UniqueIdentifier, typeof(Guid));
					_htSqlToCS.Add(SqlDbType.VarBinary, typeof(byte[]));
					_htSqlToCS.Add(SqlDbType.VarChar, typeof(string));
					_htSqlToCS.Add(SqlDbType.Variant, typeof(object));
					_htSqlToCS.Add(SqlDbType.Xml, typeof(System.Xml.XmlDocument));
				}
				return _htSqlToCS;
			}
		}

		/// <summary>
		/// 获取数据库类型对应的C#类型
		/// </summary>
		/// <param name="sdt"></param>
		/// <returns></returns>
		public static Type GetCSType(SqlDbType sdt)
		{
			Type t = typeof(string);
			if (htSqlToCS.ContainsKey(sdt))
			{
				t = htSqlToCS[sdt];
			}
			return t;
		}
		#endregion

		#region htCSToSql
		private static Dictionary<Type, SqlDbType> _htCSToSql = null;
		/// <summary>
		/// 获取C#类型与SQL数据库类型的对应表
		/// </summary>
		protected static Dictionary<Type, SqlDbType> htCSToSql
		{
			get
			{
				if (_htCSToSql == null)
				{
					_htCSToSql = new Dictionary<Type, SqlDbType>();
					_htCSToSql.Add(typeof(bool), SqlDbType.TinyInt);
					_htCSToSql.Add(typeof(byte), SqlDbType.TinyInt);
					_htCSToSql.Add(typeof(byte[]), SqlDbType.VarBinary);
					_htCSToSql.Add(typeof(DateTime), SqlDbType.DateTime);
					_htCSToSql.Add(typeof(System.DateTimeOffset), SqlDbType.DateTimeOffset);
					_htCSToSql.Add(typeof(decimal), SqlDbType.Decimal);
					_htCSToSql.Add(typeof(double), SqlDbType.Float);
					_htCSToSql.Add(typeof(float), SqlDbType.Float);
					_htCSToSql.Add(typeof(Guid), SqlDbType.UniqueIdentifier);
					_htCSToSql.Add(typeof(int), SqlDbType.Int);
					_htCSToSql.Add(typeof(long), SqlDbType.BigInt);
					_htCSToSql.Add(typeof(short), SqlDbType.SmallInt);
					_htCSToSql.Add(typeof(string), SqlDbType.VarChar);//视实际情况,可选用 NVarChar
					_htCSToSql.Add(typeof(System.Xml.XmlDocument), SqlDbType.Xml);
				}
				return _htCSToSql;
			}
		}

		/// <summary>
		/// 获取C#类型对应的数据库类型
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static SqlDbType GetSqlDbType(Type t)
		{
			SqlDbType sdt = SqlDbType.NVarChar;
			if (htCSToSql.ContainsKey(t))
			{
				sdt = htCSToSql[t];
			}
			return sdt;
		}
		#endregion

		#region EncodeString
		/// <summary>
		/// 对字符串编码
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string EncodeString(string str)
		{
			return str.Replace("'", "''");
		}
		#endregion

		#region ConvertToSqlON

		/// <summary>
		/// 获取 SQL 语句常量表示串
		/// </summary>
		/// <param name="sdt">已支持类型受限于下级方法实现</param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ConvertToSqlON(System.Data.SqlDbType sdt, object obj)
		{
			System.ValueType val = obj as System.ValueType;
			if (val != null)
			{	// 值类型
				string rtn = ConvertToSqlON(sdt, val);
				return rtn;
			}
			else
			{	// 以下为引用类型处理
				#region DBNull
				DBNull dbNull = obj as DBNull;
				if (dbNull != null)
				{
					return "NULL";
				}
				#endregion

				#region string
				string str = obj as string;
				if (str != null)
				{
					string rtn = ConvertToSqlON(sdt, str);
					return rtn;
				}
				#endregion

				#region byte[]
				byte[] aryBytes = obj as byte[];
				if (aryBytes != null)
				{
					string rtn = ConvertToSqlON(sdt, aryBytes);
					return rtn;
				}
				#endregion
			}

			return NonSupportedInType;
		}

		/// <summary>
		/// 获取 SQL 语句常量表示串
		/// </summary>
		/// <param name="sdt">已支持类型受限于下级方法实现</param>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string ConvertToSqlON(System.Data.SqlDbType sdt, System.ValueType val)
		{
			#region bool || byte
			if (val is bool || val is byte)
			{
				int n = System.Convert.ToInt32(val);
				string rtn = ConvertToSqlON(sdt, n);
				return rtn.ToString();
			}
			#endregion

			#region 默认处理方式
			if (sdt == System.Data.SqlDbType.BigInt
				|| sdt == System.Data.SqlDbType.Bit
				|| sdt == System.Data.SqlDbType.Decimal
				|| sdt == System.Data.SqlDbType.Float
				|| sdt == System.Data.SqlDbType.Int
				|| sdt == System.Data.SqlDbType.Money
				|| sdt == System.Data.SqlDbType.Real
				|| sdt == System.Data.SqlDbType.SmallInt
				|| sdt == System.Data.SqlDbType.TinyInt
			)
			{
				string rtn = val.ToString();
				return rtn;
			}

			if (sdt == System.Data.SqlDbType.Char
				|| sdt == System.Data.SqlDbType.DateTime
				|| sdt == System.Data.SqlDbType.NChar
				|| sdt == System.Data.SqlDbType.NText
				|| sdt == System.Data.SqlDbType.NVarChar
				|| sdt == System.Data.SqlDbType.SmallDateTime
				|| sdt == System.Data.SqlDbType.Text
				|| sdt == System.Data.SqlDbType.UniqueIdentifier
				|| sdt == System.Data.SqlDbType.VarChar
			)
			{
				string str = val.ToString();
				string rtn = ConvertToSqlON(sdt, str);
				return rtn;
			}
			#endregion

			return NonSupportedOutType;
		}

		/// <summary>
		/// 获取 SQL 语句常量表示串
		/// </summary>
		/// <param name="sdt">{Char,DateTime,NChar,NText,NVarChar,SmallDateTime,Text,UniqueIdentifier,VarChar}</param>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ConvertToSqlON(System.Data.SqlDbType sdt, string str)
		{
			if (sdt == System.Data.SqlDbType.Char
				|| sdt == System.Data.SqlDbType.DateTime
				|| sdt == System.Data.SqlDbType.NChar
				|| sdt == System.Data.SqlDbType.NText
				|| sdt == System.Data.SqlDbType.NVarChar
				|| sdt == System.Data.SqlDbType.SmallDateTime
				|| sdt == System.Data.SqlDbType.Text
				|| sdt == System.Data.SqlDbType.UniqueIdentifier
				|| sdt == System.Data.SqlDbType.VarChar
			)
			{	// 需要在前后添加单引号(')
				string rtn = "'" + str.Replace("'", "''") + "'";

				if (sdt == System.Data.SqlDbType.NChar
					|| sdt == System.Data.SqlDbType.NText
					|| sdt == System.Data.SqlDbType.NVarChar
				)
				{
					rtn = "N" + rtn;
				}
				return rtn;
			}
			return NonSupportedOutType;
		}

		/// <summary>
		/// 获取 SQL 语句常量表示串
		/// </summary>
		/// <param name="sdt">{Binary,Bit,Image,Variant,UniqueIdentifier}</param>
		/// <param name="ary"></param>
		/// <returns></returns>
		public static string ConvertToSqlON(System.Data.SqlDbType sdt, params byte[] ary)
		{
			string rtn = NonSupportedOutType;
			if (sdt == System.Data.SqlDbType.Binary
				|| sdt == System.Data.SqlDbType.Image
				|| sdt == System.Data.SqlDbType.VarBinary
			)
			{
				rtn = string.Empty;
				foreach (byte b in ary)
				{
					string str = System.Convert.ToString(b, 16);
					if (str.Length < 2)
					{
						str = "0" + str;
					}
					rtn += str;
				}
				rtn = "0x" + rtn;
			}
			else if (sdt == System.Data.SqlDbType.UniqueIdentifier)
			{	// 需要在前后添加单引号(')
				rtn = "'";
				for (int i = 0; i < 4; i++)
				{
					rtn += System.Convert.ToString(ary[i], 16);
				}
				rtn += "-";
				for (int i = 4; i < 6; i++)
				{
					rtn += System.Convert.ToString(ary[i], 16);
				}
				rtn += "-";
				for (int i = 6; i < 8; i++)
				{
					rtn += System.Convert.ToString(ary[i], 16);
				}
				rtn += "-";
				for (int i = 8; i < 10; i++)
				{
					rtn += System.Convert.ToString(ary[i], 16);
				}
				rtn += "-";
				for (int i = 10; i < 16; i++)
				{
					rtn += System.Convert.ToString(ary[i], 16);
				}
				rtn += "'";
			}
			return rtn;
		}

		/// <summary>
		/// 根据传入对象类型,获取对应的 SQL 语句常量表示串
		/// </summary>
		/// <returns></returns>
		public static string ConvertToSqlON(object obj)
		{
			SqlDbType sdt = GetSqlDbType(obj.GetType());
			return ConvertToSqlON(sdt, obj);
		}
		#endregion

		#region ParseSqlON
		/// <summary>
		/// 解析 SqlON 对象
		/// </summary>
		/// <typeparam name="T">解析结果目标类型</typeparam>
		/// <param name="sdt">来源类型</param>
		/// <param name="SqlON">来源 SqlON 字符串</param>
		/// <returns></returns>
		public static object ParseSqlON<T>(System.Data.SqlDbType sdt, string SqlON)
		{
			T rtn = default(T);
			#region byte[]
			if (typeof(T) == typeof(byte[]))
			{
				switch (sdt)
				{
					// 以 0x 开头
					case SqlDbType.Binary:
					case SqlDbType.Image:
					case SqlDbType.VarBinary:
						byte[] rtn1 = new byte[(SqlON.Length - 2) / 2];
						for (int i = 0; i < rtn1.Length; i++)
						{
							rtn1[i] = System.Convert.ToByte(SqlON.Substring(2 * (i + 1), 2), 16);
						}
						return rtn1;

					// 放在 '' 之间
					case SqlDbType.NVarChar:
					case SqlDbType.VarChar:
					case SqlDbType.NChar:
					case SqlDbType.Char:
					case SqlDbType.NText:
					case SqlDbType.Text:
					case SqlDbType.DateTime:
					case SqlDbType.SmallDateTime:
					case SqlDbType.Date:
					case SqlDbType.Time:
					case SqlDbType.DateTime2:
					case SqlDbType.DateTimeOffset:
					case SqlDbType.Timestamp:
					case SqlDbType.UniqueIdentifier:
						string str = SqlON.Trim().Substring(1, SqlON.Length - 2).Replace("''", "'");
						byte[] rtn2 = System.Text.Encoding.Unicode.GetBytes(str);
						return rtn2;

					// 10进制整数
					case SqlDbType.Int:
					case SqlDbType.BigInt:
					case SqlDbType.SmallInt:
					case SqlDbType.TinyInt:
						long rtn3 = System.Convert.ToInt64(SqlON);
						return rtn3;

					// 10进制小数
					case SqlDbType.Decimal:
					case SqlDbType.Float:
					case SqlDbType.Money:
					case SqlDbType.Real:
					case SqlDbType.SmallMoney:
						double rtn4 = System.Convert.ToDouble(SqlON);
						return rtn4;

					// 尚未明确
					case SqlDbType.Bit:
						return rtn;

					default:
						return rtn;
				}
			}
			#endregion
			return rtn;
		}
		#endregion
	}
}
