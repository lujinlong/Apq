using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Apq.Net
{
	/// <summary>
	/// 提供常用FTP操作(每个操作连接一次)
	/// </summary>
	public class FtpClient
	{
		private string _Server;
		private int _Port = 21;
		private string _RemotePath;
		private string _U = "anonymous";
		private string _P = "anonymous@";
		private string _FtpURI;
		private int _CacheSize = 8192;	// 缓存(默认8K)

		/// <summary>
		/// 设置FTP连接信息
		/// </summary>
		/// <param name="Server">FTP连接地址</param>
		/// <param name="FtpPort">FTP端口</param>
		/// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
		/// <param name="FtpU">用户名</param>
		/// <param name="FtpP">密码</param>
		public FtpClient(string Server, int FtpPort, string FtpRemotePath, string FtpU, string FtpP)
		{
			_Server = Server;
			_Port = FtpPort;
			_RemotePath = ("/" + FtpRemotePath.Trim('/') + "/").Replace("//", "/");
			_U = string.IsNullOrEmpty(FtpU) ? _U : FtpU;
			_P = string.IsNullOrEmpty(FtpP) ? _P : FtpP;
			_FtpURI = string.Format("ftp://{0}:{1}{2}", _Server, _Port, _RemotePath);
		}

		/// <summary>
		/// 上传文件
		/// </summary>
		public void Upload(string FullName)
		{
			FileInfo fileInf = new FileInfo(FullName);
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI + fileInf.Name));
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			reqFTP.KeepAlive = false;
			reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
			reqFTP.UseBinary = true;
			reqFTP.ContentLength = fileInf.Length;
			byte[] buff = new byte[_CacheSize];
			int contentLen;
			FileStream fs = fileInf.OpenRead();
			try
			{
				Stream strm = reqFTP.GetRequestStream();
				contentLen = fs.Read(buff, 0, _CacheSize);
				while (contentLen != 0)
				{
					strm.Write(buff, 0, contentLen);
					contentLen = fs.Read(buff, 0, _CacheSize);
				}
				strm.Close();
			}
			finally
			{
				fs.Close();
			}
		}

		/// <summary>
		/// 下载文件
		/// </summary>
		public void Download(string LocalPath, string FileName)
		{

			FileStream outputStream = new FileStream(LocalPath.TrimEnd('\\') + "\\" + FileName, FileMode.Create);
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI + FileName));
			reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
			reqFTP.UseBinary = true;
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			try
			{
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();
				long cl = response.ContentLength;
				int readCount;
				byte[] buffer = new byte[_CacheSize];

				readCount = ftpStream.Read(buffer, 0, _CacheSize);
				while (readCount > 0)
				{
					outputStream.Write(buffer, 0, readCount);
					readCount = ftpStream.Read(buffer, 0, _CacheSize);
				}

				ftpStream.Close();
				response.Close();
			}
			finally
			{
				outputStream.Close();
			}
		}

		/// <summary>
		/// 删除文件
		/// </summary>
		public void Delete(string FileName)
		{
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI + FileName));
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			reqFTP.KeepAlive = false;
			reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

			string result = string.Empty;
			FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
			long size = response.ContentLength;
			Stream datastream = response.GetResponseStream();
			StreamReader sr = new StreamReader(datastream);
			result = sr.ReadToEnd();
			sr.Close();
			datastream.Close();
			response.Close();
		}

		/// <summary>
		/// 获取当前目录下明细(包含文件和文件夹)
		/// </summary>
		public List<string> GetFilesDetailList()
		{
			List<string> lst = new List<string>();
			FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI));
			ftp.Credentials = new NetworkCredential(_U, _P);
			ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
			WebResponse response = ftp.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
			string line = reader.ReadLine();
			line = reader.ReadLine();
			line = reader.ReadLine();
			while (line != null)
			{
				lst.Add(line);
				line = reader.ReadLine();
			}
			reader.Close();
			response.Close();
			return lst;
		}

		/// <summary>
		/// 获取当前目录下文件列表(仅文件)
		/// </summary>
		public List<string> GetFileList(string mask)
		{
			List<string> lst = new List<string>();
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI));
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
			WebResponse response = reqFTP.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);

			string line = reader.ReadLine();
			while (line != null)
			{
				if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
				{
					string mask_ = mask.Substring(0, mask.IndexOf("*"));
					if (line.Substring(0, mask_.Length) == mask_)
					{
						lst.Add(line);
					}
				}
				else
				{
					lst.Add(line);
				}
				line = reader.ReadLine();
			}
			reader.Close();
			response.Close();
			return lst;
		}

		/// <summary>
		/// [未尝试]获取当前目录下所有的文件夹列表(仅文件夹)
		/// </summary>
		public List<string> GetDirectoryList()
		{
			List<string> lst = new List<string>();
			List<string> drectory = GetFilesDetailList();
			foreach (string str in drectory)
			{
				if (str.Trim().Substring(0, 1).ToUpper() == "D")
				{
					lst.Add(str.Substring(54).Trim());
				}
			}
			return lst;
		}

		/// <summary>
		/// [未尝试]判断当前目录下指定的子目录是否存在
		/// </summary>
		/// <param name="RemoteDirectoryName">指定的目录名</param>
		public bool DirectoryExist(string RemoteDirectoryName)
		{
			List<string> dirList = GetDirectoryList();
			return dirList.Contains(RemoteDirectoryName);
		}

		/// <summary>
		/// 判断当前目录下指定的文件是否存在
		/// </summary>
		/// <param name="RemoteFileName">远程文件名</param>
		public bool FileExist(string RemoteFileName)
		{
			List<string> fileList = GetFileList("*.*");
			return fileList.Contains(RemoteFileName);
		}

		/// <summary>
		/// 创建文件夹
		/// </summary>
		/// <param name="dirName"></param>
		public void MakeDir(string dirName)
		{
			// dirName = name of the directory to create.
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI + dirName));
			reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
			reqFTP.UseBinary = true;
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
			Stream ftpStream = response.GetResponseStream();

			ftpStream.Close();
			response.Close();
		}

		/// <summary>
		/// 获取指定文件大小
		/// </summary>
		public long GetFileSize(string FileName)
		{
			long fileSize = 0;
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI + FileName));
			reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
			reqFTP.UseBinary = true;
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
			Stream ftpStream = response.GetResponseStream();
			fileSize = response.ContentLength;

			ftpStream.Close();
			response.Close();
			return fileSize;
		}

		/// <summary>
		/// 改名
		/// </summary>
		public void Rename(string Name, string NewName)
		{
			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_FtpURI + Name));
			reqFTP.Method = WebRequestMethods.Ftp.Rename;
			reqFTP.RenameTo = NewName;
			reqFTP.UseBinary = true;
			reqFTP.Credentials = new NetworkCredential(_U, _P);
			FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
			Stream ftpStream = response.GetResponseStream();

			ftpStream.Close();
			response.Close();
		}

		/// <summary>
		/// 移动文件
		/// </summary>
		public void MovieFile(string Name, string NewName)
		{
			Rename(Name, NewName);
		}

		/// <summary>
		/// 切换当前目录
		/// </summary>
		/// <param name="DirectoryName"></param>
		/// <param name="IsRoot">true 绝对路径   false 相对路径</param> 
		public void GotoDirectory(string DirectoryName, bool IsRoot)
		{
			if (IsRoot)
			{
				_RemotePath = DirectoryName;
			}
			else
			{
				_RemotePath += DirectoryName + "/";
			}
			_FtpURI = string.Format("ftp://{0}:{1}{2}", _Server, _Port, _RemotePath);
		}
	}
}
