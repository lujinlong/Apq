using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apq.Net;
using System.IO;

namespace ApqFtpWS.App_Code
{
	public class FtpQueue
	{
		public static string FtpFile = string.Empty;
		public static System.Data.DataSet dsFtp = new System.Data.DataSet();	// 存储Ftp队列表

		/// <summary>
		/// Ftp上传请求入队
		/// </summary>
		/// <param name="Folder">文件目录</param>
		/// <param name="FileName">文件名</param>
		/// <param name="FtpSrv">Ftp服务器IP</param>
		/// <param name="FtpPort">端口</param>
		/// <param name="FtpU">用户名</param>
		/// <param name="FtpP">密码</param>
		/// <param name="FtpFolder">要上传到的Ftp目录</param>
		/// <returns></returns>
		public static void Ftp_Enqueue(string Folder, string FileName, string FtpSrv, int FtpPort, string FtpU, string FtpP, string FtpFolder)
		{
			if (!Folder.EndsWith("\\")) Folder += "\\";

			if (FtpFile == null || FtpFile.Length < 1)
			{
				FtpFile = GlobalObject.XmlConfigChain[typeof(FtpQueue), "FtpFile"];
				// 加载Ftp队列数据
				if (FtpFile != null && File.Exists(FtpQueue.FtpFile)) FtpQueue.dsFtp.ReadXml(FtpQueue.FtpFile);
			}
			if (FtpFile.Length < 1)
			{
				throw new Exception("无法保存队列,请检查队列文件设置");
			}

			if (dsFtp.Tables.Count == 0)
			{
				// 给定架构
				System.Data.DataTable dt = dsFtp.Tables.Add("FtpQueue");
				dt.Columns.Add("ID", typeof(int));
				dt.Columns.Add("Folder");
				dt.Columns.Add("FileName");
				dt.Columns.Add("FtpSrv");
				dt.Columns.Add("FtpPort", typeof(int));
				dt.Columns.Add("FtpU");
				dt.Columns.Add("FtpP");
				dt.Columns.Add("FtpFolder");

				dt.Columns["ID"].AutoIncrementSeed = 1;
				dt.Columns["ID"].AutoIncrementStep = 1;
				dt.Columns["ID"].AutoIncrement = true;
			}

			System.Data.DataRow dr = dsFtp.Tables[0].NewRow();
			dr["Folder"] = Folder;
			dr["FileName"] = FileName;
			dr["FtpSrv"] = FtpSrv;
			dr["FtpPort"] = FtpPort;
			dr["FtpU"] = FtpU;
			dr["FtpP"] = FtpP;
			dr["FtpFolder"] = FtpFolder;
			dsFtp.Tables[0].Rows.InsertAt(dr, 0);

			// 存回文件
			dsFtp.WriteXml(FtpFile, System.Data.XmlWriteMode.WriteSchema);
		}

		public static void Ftp_Put(int ID)
		{
			try
			{
				string Folder = string.Empty;
				string FileName = string.Empty;
				string FtpSrv = string.Empty;
				string FtpU = string.Empty;
				string FtpP = string.Empty;
				string FtpFolder = string.Empty;
				int FtpPort = 21;

				for (int i = dsFtp.Tables[0].Rows.Count - 1; i >= 0; i--)
				{
					if (ID.Equals(dsFtp.Tables[0].Rows[i]["ID"]))
					{
						Folder = Apq.Convert.ChangeType<string>(dsFtp.Tables[0].Rows[i]["Folder"]);
						FileName = Apq.Convert.ChangeType<string>(dsFtp.Tables[0].Rows[i]["FileName"]);
						FtpSrv = Apq.Convert.ChangeType<string>(dsFtp.Tables[0].Rows[i]["FtpSrv"]);
						FtpU = Apq.Convert.ChangeType<string>(dsFtp.Tables[0].Rows[i]["FtpU"]);
						FtpP = Apq.Convert.ChangeType<string>(dsFtp.Tables[0].Rows[i]["FtpP"]);
						FtpFolder = Apq.Convert.ChangeType<string>(dsFtp.Tables[0].Rows[i]["FtpFolder"]);
						FtpPort = Apq.Convert.ChangeType<int>(dsFtp.Tables[0].Rows[i]["FtpPort"]);
						break;
					}
				}
				if (FileName.Length <= 0)
				{
					return;
				}

				FtpClient fc = new FtpClient(FtpSrv, FtpPort, FtpFolder, FtpU, FtpP);
				fc.Upload(Folder + "_up" + FileName);
				fc.Rename("_up" + FileName, FileName);

				// 出队
				for (int i = dsFtp.Tables[0].Rows.Count - 1; i >= 0; i--)
				{
					if (ID.Equals(dsFtp.Tables[0].Rows[i]["ID"]))
					{
						dsFtp.Tables[0].Rows.RemoveAt(i);
						break;
					}
				}

				// 存回文件
				dsFtp.WriteXml(FtpFile, System.Data.XmlWriteMode.WriteSchema);
			}
			catch
			{
				// 截获所有异常
			}
		}
	}
}
