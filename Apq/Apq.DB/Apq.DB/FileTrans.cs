using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Apq.DB
{
	/// <summary>
	/// 通过数据库传文件
	/// 数据库需求("本地":数据库服务器,"远程":客户端程序):
	/// 1.表:FileTrans(
	/// [ID] [bigint] IDENTITY(1,1),
	/// FileName nvarchar(500),
	/// DBFolder nvarchar(500),
	/// CFolder nvarchar(500),
	/// [FileStream] [varbinary](max),
	/// _InTime datetime)
	/// 2.SP:
	/// Apq_FileTrans_Insert		-- 远程写入数据库
	/// Apq_FileTrans_Insert_ADO	-- 本地写入数据库
	/// Apq_FileTrans_WriteToHD_ADO	-- 本地写入磁盘
	/// Apq_FileTrans_List			-- 远程列表(数据库),通过它下载文件
	/// 3.权限:
	/// 启动帐号对该表具有读写权限
	/// </summary>
	public class FileTrans
	{
		/// <summary>
		/// FileTrans
		/// </summary>
		/// <param name="conn">数据库连接</param>
		public FileTrans(System.Data.Common.DbConnection conn)
		{
			_Connection = conn;
		}

		private System.Data.Common.DbConnection _Connection;
		/// <summary>
		/// 获取或设置数据库连接
		/// </summary>
		public System.Data.Common.DbConnection Connection
		{
			get { return _Connection; }
			set { _Connection = value; }
		}

		/// <summary>
		/// 上传文件
		/// </summary>
		public void FileUp(string FullName, string DBFullName)
		{
			System.IO.FileStream fs = System.IO.File.OpenRead(FullName);
			byte[] bFile = Apq.IO.FileSystem.ReadFully(fs);
			FileUp(FullName, DBFullName, bFile);
		}

		/// <summary>
		/// 上传文件
		/// </summary>
		public void FileUp(string FullName, string DBFullName, byte[] bFile)
		{
			string FileName = System.IO.Path.GetFileName(FullName);
			string CFolder = System.IO.Path.GetDirectoryName(FullName);
			string DBFolder = System.IO.Path.GetDirectoryName(DBFullName);

			// 1.上传到数据库
			Apq.Data.Common.DbConnectionHelper.Open(Connection);
			System.Data.Common.DbCommand sqlCmd = Connection.CreateCommand();
			sqlCmd.CommandText = "Apq_FileTrans_Insert";
			sqlCmd.CommandType = CommandType.StoredProcedure;
			Apq.Data.Common.DbCommandHelper cmdHelper = new Apq.Data.Common.DbCommandHelper(sqlCmd);
			cmdHelper.AddParameter("@FileName", FileName);
			cmdHelper.AddParameter("@DBFolder", DBFolder);
			cmdHelper.AddParameter("@CFolder", CFolder);
			cmdHelper.AddParameter("@FileStream", bFile);
			cmdHelper.AddParameter("@ID", 0);
			sqlCmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
			sqlCmd.ExecuteNonQuery();
			int ID = Apq.Convert.ChangeType<int>(sqlCmd.Parameters["@ID"].Value);

			// 2.转到磁盘
			sqlCmd.Parameters.Clear();
			sqlCmd.CommandText = "Apq_FileTrans_WriteToHD_ADO";
			sqlCmd.CommandType = CommandType.StoredProcedure;
			cmdHelper.AddParameter("@ID", ID);
			cmdHelper.AddParameter("@KeepInDB", DBNull.Value);
			sqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// 下载文件
		/// </summary>
		public void FileDow(string DBFullName, string FullName)
		{
			string FileName = System.IO.Path.GetFileName(FullName);
			string CFolder = System.IO.Path.GetDirectoryName(FullName);
			string DBFolder = System.IO.Path.GetDirectoryName(DBFullName);

			// 准备本地目录
			if (!System.IO.Directory.Exists(CFolder))
			{
				System.IO.Directory.CreateDirectory(CFolder);
			}

			// 1.写入数据库
			Apq.Data.Common.DbConnectionHelper.Open(Connection);
			System.Data.Common.DbCommand sqlCmd = Connection.CreateCommand();
			sqlCmd.CommandText = "Apq_FileTrans_Insert_ADO";
			sqlCmd.CommandType = CommandType.StoredProcedure;
			Apq.Data.Common.DbCommandHelper cmdHelper = new Apq.Data.Common.DbCommandHelper(sqlCmd);
			cmdHelper.AddParameter("@FullName", DBFullName);
			cmdHelper.AddParameter("@CFolder", CFolder);
			cmdHelper.AddParameter("@ID", 0);
			sqlCmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
			sqlCmd.ExecuteNonQuery();
			int ID = Apq.Convert.ChangeType<int>(sqlCmd.Parameters["@ID"].Value);

			// 2.读取到本地
			DataSet ds = new DataSet();
			Apq.Data.Common.DbConnectionHelper connHelper = new Apq.Data.Common.DbConnectionHelper(Connection);
			DbDataAdapter da = connHelper.CreateAdapter();
			da.SelectCommand.CommandText = "Apq_FileTrans_List";
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			Apq.Data.Common.DbCommandHelper daHelper = new Apq.Data.Common.DbCommandHelper(da.SelectCommand);
			daHelper.AddParameter("@ID", ID);
			da.Fill(ds);

			// 3.保存文件
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				byte[] bFile = Apq.Convert.ChangeType<byte[]>(ds.Tables[0].Rows[0]["FileStream"]);
				System.IO.FileStream fs = new System.IO.FileStream(FullName, System.IO.FileMode.Create);
				fs.Write(bFile, 0, bFile.Length);
				fs.Flush();
				fs.Close();
			}

			// 4.删除数据库行
			da.SelectCommand.CommandText = "Apq_FileTrans_Delete";
			da.SelectCommand.ExecuteNonQuery();
		}
	}
}
