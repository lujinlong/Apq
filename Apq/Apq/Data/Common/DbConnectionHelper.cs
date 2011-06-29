using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data.Common
{
	/// <summary>
	/// DbConnectionHelper
	/// </summary>
	public sealed class DbConnectionHelper
	{
		#region 静态方法
		/// <summary>
		/// [可重入]打开连接
		/// </summary>
		/// <param name="Connection"></param>
		public static void Open(System.Data.Common.DbConnection Connection)
		{
			if (Connection != null && Connection.State == System.Data.ConnectionState.Closed)
			{
				Connection.Open();
			}
		}

		/// <summary>
		/// [可重入]关闭连接
		/// </summary>
		/// <param name="Connection"></param>
		public static void Close(System.Data.Common.DbConnection Connection)
		{
			if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
			{
				Connection.Close();
			}
		}
		#endregion

		#region 字段
		private System.Data.Common.DbConnection _Connection;
		/// <summary>
		/// DbConnection
		/// </summary>
		public System.Data.Common.DbConnection Connection
		{
			get { return _Connection; }
		}
		#endregion

		#region 构造函数
		/// <summary>
		/// 装饰
		/// </summary>
		/// <param name="Connection"></param>
		public DbConnectionHelper(System.Data.Common.DbConnection Connection)
		{
			this._Connection = Connection;
		}
		#endregion

		#region 属性
		#endregion

		#region 方法
		/// <summary>
		/// [可重入]打开连接
		/// </summary>
		public void Open()
		{
			Open(_Connection);
		}

		/// <summary>
		/// [可重入]关闭连接
		/// </summary>
		public void Close()
		{
			Close(_Connection);
		}

		/// <summary>
		/// [反射]创建 DataAdapter
		/// </summary>
		/// <returns></returns>
		public System.Data.Common.DbDataAdapter CreateAdapter(string cmdSelect = "")
		{
			Type tConnection = Connection.GetType();
			Type tAdapter = tConnection.Assembly.GetType(tConnection.FullName.Replace("Connection", "DataAdapter"));
			return System.Activator.CreateInstance(tAdapter, cmdSelect, Connection) as System.Data.Common.DbDataAdapter;
		}
		#endregion
	}
}
