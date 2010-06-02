using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Apq.Data.Common
{
	/// <summary>
	/// DbCommandHelper
	/// </summary>
	public sealed class DbCommandHelper
	{
		#region 字段
		private DbCommand _Command;
		/// <summary>
		/// 获取 DbCommand
		/// </summary>
		public DbCommand Command
		{
			get { return _Command; }
		}
		#endregion

		#region 构造函数
		/// <summary>
		/// 装饰
		/// </summary>
		/// <param name="Command"></param>
		public DbCommandHelper(DbCommand Command)
		{
			this._Command = Command;
		}
		#endregion

		#region 方法
		/// <summary>
		/// [覆盖添加]添加参数
		/// </summary>
		/// <param name="Parameter"></param>
		public void AddParameter(DbParameter Parameter)
		{
			DbParameterCollectionHelper ParametersHelper = new DbParameterCollectionHelper(Command.Parameters);
			ParametersHelper[Parameter.ParameterName] = Parameter;
		}

		/// <summary>
		/// [覆盖添加]添加参数
		/// </summary>
		/// <param name="ParameterName"></param>
		/// <param name="Value"></param>
		public void AddParameter(string ParameterName, object Value)
		{
			DbParameter DbParam = Command.CreateParameter();
			DbParam.ParameterName = ParameterName;
			DbParam.Value = Value;
			AddParameter(DbParam);
		}

		/// <summary>
		/// [覆盖添加]添加参数
		/// </summary>
		/// <param name="ParameterName"></param>
		/// <param name="Value"></param>
		/// <param name="DbType"></param>
		public void AddParameter(string ParameterName, object Value, System.Data.DbType DbType)
		{
			DbParameter DbParam = Command.CreateParameter();
			DbParam.ParameterName = ParameterName;
			DbParam.DbType = DbType;
			DbParam.Value = Value;
			AddParameter(DbParam);
		}

		/// <summary>
		/// [覆盖添加]添加参数
		/// </summary>
		/// <param name="ParameterName"></param>
		/// <param name="Value"></param>
		/// <param name="DbType"></param>
		/// <param name="Size"></param>
		public void AddParameter(string ParameterName, object Value, System.Data.DbType DbType, int Size)
		{
			DbParameter DbParam = Command.CreateParameter();
			DbParam.ParameterName = ParameterName;
			DbParam.DbType = DbType;
			DbParam.Size = Size;
			DbParam.Value = Value;
			AddParameter(DbParam);
		}
		#endregion
	}
}
