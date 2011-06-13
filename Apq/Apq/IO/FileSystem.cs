using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Apq.IO
{
	/// <summary>
	/// FileSystem
	/// </summary>
	public class FileSystem
	{
		#region 读文件
		/// <summary>
		/// 从流(长度未知)读取到数组
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static byte[] ReadFully(Stream stream)
		{
			byte[] buffer = new byte[32768];
			using (MemoryStream ms = new MemoryStream())
			{
				while (true)
				{
					int read = stream.Read(buffer, 0, buffer.Length);
					if (read <= 0) return ms.ToArray();
					ms.Write(buffer, 0, read);
				}
			}
		}
		#endregion

		#region Explorer
		/// <summary>
		/// 返回指定目录是否具有下级
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool HasChildren(string path)
		{
			try
			{
				string[] root = Directory.GetFiles(path);
				if (root.Length > 0) return true;
			}
			catch { }
			try
			{
				string[] root = Directory.GetDirectories(path);
				if (root.Length > 0) return true;
			}
			catch { }
			return false;
		}

		/// <summary>
		/// [列表]加载磁盘
		/// </summary>
		public static void LoadLogicDrivers(DataTable dt, ref long FirstID)
		{
			try
			{
				string[] root = Directory.GetLogicalDrives();

				foreach (string s in root)
				{
					dt.Rows.Add(true, 0, 0, FirstID++, 0, s, s, 0, 0, false, null);
				}
			}
			catch { }
		}
		/// <summary>
		/// [列表]加载子级文件夹
		/// </summary>
		public static void LoadFolders(DataTable dt, ref long FirstID, long ID, int Checked)
		{
			try
			{
				DataView dv = new DataView(dt);
				dv.RowFilter = "ID = " + ID;
				if (dv.Count > 0)
				{
					string FullName = dv[0]["FullName"].ToString();
					LoadFolders(dt, ref FirstID, ID, Checked, FullName);
				}
			}
			catch { }
		}
		/// <summary>
		/// [列表]加载子级文件
		/// </summary>
		public static void LoadFiles(DataTable dt, ref long FirstID, long ID, int Checked)
		{
			try
			{
				DataView dv = new DataView(dt);
				dv.RowFilter = "ID = " + ID;
				if (dv.Count > 0)
				{
					string FullName = dv[0]["FullName"].ToString();
					LoadFiles(dt, ref FirstID, ID, Checked, FullName);
				}
			}
			catch { }
		}

		/// <summary>
		/// [列表]加载子级文件夹
		/// </summary>
		public static void LoadFolders(DataTable dt, ref long FirstID, long ID, int Checked, string FullName)
		{
			DirectoryInfo di;
			try
			{
				string[] root = Directory.GetDirectories(FullName);
				foreach (string s in root)
				{
					try
					{
						di = new DirectoryInfo(s);
						dt.Rows.Add(HasChildren(s), 1, Checked, FirstID++, ID, s, di.Name, 1, 0, false, null);
					}
					catch { }
				}
			}
			catch { }
		}
		/// <summary>
		/// [列表]加载子级文件
		/// </summary>
		public static void LoadFiles(DataTable dt, ref long FirstID, long ID, int Checked, string FullName)
		{
			FileInfo fi;
			try
			{
				string[] root = Directory.GetFiles(FullName);
				foreach (string s in root)
				{
					fi = new FileInfo(s);
					dt.Rows.Add(false, 3, Checked, FirstID++, ID, s, fi.Name, 2, fi.Length, false, null);
				}
			}
			catch { }
		}
		#endregion
	}
}
