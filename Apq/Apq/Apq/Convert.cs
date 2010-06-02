using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace Apq
{
	/// <summary>
	/// 将一个基本数据类型转换为另一个基本数据类型。
	/// </summary>
	public class Convert
	{
		#region ChangeType
		/// <summary>
		/// [无异常]强制类型转换,失败时返回 default(T)
		/// </summary>
		/// <typeparam name="T">输出类型</typeparam>
		/// <param name="obj">原始对象</param>
		/// <returns></returns>
		public static T ChangeType<T>(object obj)
		{
			return ChangeType<T>(obj, default(T));
		}

		/// <summary>
		/// [无异常]强制类型转换,失败时返回 Default
		/// </summary>
		/// <typeparam name="T">输出类型</typeparam>
		/// <param name="obj">原始对象</param>
		/// <param name="Default">默认值</param>
		/// <returns></returns>
		public static T ChangeType<T>(object obj, T Default)
		{
			// 先尝试强制类型转换(必须)
			try
			{
				return (T)obj;
			}
			catch { }
			// 失败后使用System.Convert.ChangeType
			try
			{
				return (T)System.Convert.ChangeType(obj, typeof(T));
			}
			catch //(System.Exception ex)
			{
				//Apq.GlobalObject.ApqLog.Warn(ex.Message);//产生日志量太大
				return Default;
			}
		}

		/// <summary>
		/// [无异常]强制类型转换,失败时返回默认值
		/// </summary>
		/// <param name="obj">原始对象</param>
		/// <param name="type">输出类型</param>
		/// <param name="Default">默认值</param>
		/// <returns></returns>
		public static object ChangeType(object obj, Type type, object Default)
		{
			try
			{
				return System.Convert.ChangeType(obj, type);
			}
			catch //(System.Exception ex)
			{
				//Apq.GlobalObject.ApqLog.Warn(ex.Message);//产生日志量太大
				return Default;
			}
		}
		#endregion

		#region LikeNull
		/// <summary>
		/// 返回指定对象是否与 DBNull 具有相似意义[仿JScrip]{null,DBNull,System.Convert.IsDBNull(obj)}
		/// </summary>
		/// <returns></returns>
		public static bool LikeDBNull(object value)
		{
			if (value == null || value == System.Convert.DBNull || System.Convert.IsDBNull(value))
			{
				return true;
			}
			if ((value as string) == string.Empty)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 返回指定对象是否为null{null,DBNull}
		/// </summary>
		/// <returns></returns>
		public static bool IsNull(object value)
		{
			if (value == null || value == System.Convert.DBNull || System.Convert.IsDBNull(value))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 返回指定对象是否有意义,不为{null,DBNull,string.Empty,false}
		/// </summary>
		/// <returns></returns>
		public static bool HasMean(object value)
		{
			if (value == null || value == System.Convert.DBNull || System.Convert.IsDBNull(value)
				|| string.IsNullOrEmpty(value.ToString()))
			{
				return false;
			}
			if (value is bool && !Apq.Convert.ChangeType<bool>(value))
			{
				return false;
			}
			return true;
		}
		#endregion

		#region ToJSO
		/// <summary>
		/// 将服务端字符串转为客户端字符串
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ToJSO(string input)
		{
			// 1. \\	==> \\\\
			// 2. \r\n	==> \n	==> \\\r\n	//统一两种回车
			// 3. \"	==> \\\"
			return input.Replace("\\", "\\\\").Replace("\r\n", "\n").Replace("\n", "\\\r\n").Replace("\"", "\\\"");
		}
		#endregion

		#region ToExcelObject
		/// <summary>
		/// 将各类型值转为 Excel 能接受的值
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static object ToExcelObject(object obj)
		{
			#region DBNull
			System.DBNull dbNull = obj as System.DBNull;
			if (dbNull != null)
			{
				return string.Empty;
			}
			#endregion

			#region bool || byte
			if (obj is bool || obj is byte)
			{
				int n = System.Convert.ToInt32(obj);
				return ToExcelObject(n);
			}
			#endregion

			#region Guid
			if (obj is System.Guid)
			{
				return obj.ToString();
			}
			#endregion

			#region byte[]
			byte[] aryBytes = obj as byte[];
			if (aryBytes != null)
			{
				return Apq.Data.SqlClient.Common.ConvertToSqlON(System.Data.SqlDbType.VarBinary, aryBytes);
			}
			#endregion

			return obj;
		}
		#endregion
	}
}
