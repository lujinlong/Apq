using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;

namespace Apq.Data.MySqlClient
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
		private static Dictionary<MySqlDbType, Type> _htSqlToCS = null;
		/// <summary>
		/// 获取SQL数据库类型与C#类型的对应表
		/// </summary>
		protected static Dictionary<MySqlDbType, Type> htSqlToCS
		{
			get
			{
				if (_htSqlToCS == null)
				{
					_htSqlToCS = new Dictionary<MySqlDbType, Type>();
					_htSqlToCS.Add(MySqlDbType.Int64, typeof(long));
					_htSqlToCS.Add(MySqlDbType.Binary, typeof(byte[]));
					_htSqlToCS.Add(MySqlDbType.Bit, typeof(bool));
					_htSqlToCS.Add(MySqlDbType.Date, typeof(DateTime));
					_htSqlToCS.Add(MySqlDbType.DateTime, typeof(DateTime));
					_htSqlToCS.Add(MySqlDbType.Decimal, typeof(decimal));
					_htSqlToCS.Add(MySqlDbType.Float, typeof(double));
					_htSqlToCS.Add(MySqlDbType.Blob, typeof(byte[]));
					_htSqlToCS.Add(MySqlDbType.Int32, typeof(int));
					_htSqlToCS.Add(MySqlDbType.Text, typeof(string));
					_htSqlToCS.Add(MySqlDbType.Time, typeof(DateTime));
					_htSqlToCS.Add(MySqlDbType.Timestamp, typeof(byte[]));
					_htSqlToCS.Add(MySqlDbType.Int16, typeof(short));
					_htSqlToCS.Add(MySqlDbType.VarBinary, typeof(byte[]));
					_htSqlToCS.Add(MySqlDbType.VarChar, typeof(string));
				}
				return _htSqlToCS;
			}
		}

		/// <summary>
		/// 获取数据库类型对应的C#类型
		/// </summary>
		/// <param name="sdt"></param>
		/// <returns></returns>
		public static Type GetCSType(MySqlDbType sdt)
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
		private static Dictionary<Type, MySqlDbType> _htCSToSql = null;
		/// <summary>
		/// 获取C#类型与SQL数据库类型的对应表
		/// </summary>
		protected static Dictionary<Type, MySqlDbType> htCSToSql
		{
			get
			{
				if (_htCSToSql == null)
				{
					_htCSToSql = new Dictionary<Type, MySqlDbType>();
					_htCSToSql.Add(typeof(bool), MySqlDbType.Bit);
					_htCSToSql.Add(typeof(byte), MySqlDbType.Byte);
					_htCSToSql.Add(typeof(byte[]), MySqlDbType.VarBinary);
					_htCSToSql.Add(typeof(DateTime), MySqlDbType.DateTime);
					_htCSToSql.Add(typeof(decimal), MySqlDbType.Decimal);
					_htCSToSql.Add(typeof(double), MySqlDbType.Double);
					_htCSToSql.Add(typeof(float), MySqlDbType.Float);
					_htCSToSql.Add(typeof(int), MySqlDbType.Int32);
					_htCSToSql.Add(typeof(long), MySqlDbType.Int64);
					_htCSToSql.Add(typeof(short), MySqlDbType.Int16);
					_htCSToSql.Add(typeof(string), MySqlDbType.VarChar);
				}
				return _htCSToSql;
			}
		}

		/// <summary>
		/// 获取C#类型对应的数据库类型
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static MySqlDbType GetMySqlDbType(Type t)
		{
			MySqlDbType sdt = MySqlDbType.VarChar;
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
		public static string ConvertToSqlON(MySqlDbType sdt, object obj)
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
		public static string ConvertToSqlON(MySqlDbType sdt, System.ValueType val)
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
			if (sdt == MySqlDbType.Int64
				|| sdt == MySqlDbType.Bit
				|| sdt == MySqlDbType.Decimal
				|| sdt == MySqlDbType.Float
				|| sdt == MySqlDbType.Int32
				|| sdt == MySqlDbType.Int16
				|| sdt == MySqlDbType.Int24
			)
			{
				string rtn = val.ToString();
				return rtn;
			}

			if (sdt == MySqlDbType.DateTime
				|| sdt == MySqlDbType.Date
				|| sdt == MySqlDbType.Time
				|| sdt == MySqlDbType.Text
				|| sdt == MySqlDbType.VarChar
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
		public static string ConvertToSqlON(MySqlDbType sdt, string str)
		{
			if (sdt == MySqlDbType.DateTime
				|| sdt == MySqlDbType.Date
				|| sdt == MySqlDbType.Time
				|| sdt == MySqlDbType.Text
				|| sdt == MySqlDbType.VarChar
			)
			{	// 需要在前后添加单引号(')
				string rtn = "'" + str.Replace("'", "''") + "'";
				return rtn;
			}
			return NonSupportedOutType;
		}

		/// <summary>
		/// 获取 SQL 语句常量表示串
		/// </summary>
		/// <param name="sdt">{Binary,Bit,Blob}</param>
		/// <param name="ary"></param>
		/// <returns></returns>
		public static string ConvertToSqlON(MySqlDbType sdt, params byte[] ary)
		{
			string rtn = NonSupportedOutType;
			if (sdt == MySqlDbType.Binary
				|| sdt == MySqlDbType.Blob
				|| sdt == MySqlDbType.VarBinary
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
				rtn = "0x" + (string.IsNullOrWhiteSpace(rtn) ? "00" : rtn);
			}
			return rtn;
		}

		/// <summary>
		/// 根据传入对象类型,获取对应的 SQL 语句常量表示串
		/// </summary>
		/// <returns></returns>
		public static string ConvertToSqlON(object obj)
		{
			MySqlDbType sdt = GetMySqlDbType(obj.GetType());
			string rtn = ConvertToSqlON(sdt, obj);
			return rtn;
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
		public static object ParseSqlON<T>(MySqlDbType sdt, string SqlON)
		{
			T rtn = default(T);
			#region byte[]
			if (typeof(T) == typeof(byte[]))
			{
				switch (sdt)
				{
					// 以 0x 开头
					case MySqlDbType.Binary:
					case MySqlDbType.Blob:
					case MySqlDbType.VarBinary:
						byte[] rtn1 = new byte[(SqlON.Length - 2) / 2];
						for (int i = 0; i < rtn1.Length; i++)
						{
							rtn1[i] = System.Convert.ToByte(SqlON.Substring(2 * (i + 1), 2), 16);
						}
						return rtn1;

					// 放在 '' 之间
					case MySqlDbType.VarChar:
					case MySqlDbType.Text:
					case MySqlDbType.DateTime:
					case MySqlDbType.Date:
					case MySqlDbType.Time:
					case MySqlDbType.Timestamp:
						string str = SqlON.Trim().Substring(1, SqlON.Length - 2).Replace("''", "'");
						byte[] rtn2 = System.Text.Encoding.Unicode.GetBytes(str);
						return rtn2;

					// 10进制整数
					case MySqlDbType.Int32:
					case MySqlDbType.Int64:
					case MySqlDbType.Int16:
					case MySqlDbType.Int24:
						long rtn3 = System.Convert.ToInt64(SqlON);
						return rtn3;

					// 10进制小数
					case MySqlDbType.Decimal:
					case MySqlDbType.Float:
						double rtn4 = System.Convert.ToDouble(SqlON);
						return rtn4;

					// 尚未明确
					case MySqlDbType.Bit:
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
