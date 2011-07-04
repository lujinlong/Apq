using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Apq.DllImports;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 图标缓存
	/// </summary>
	public static class IconChache
	{
		private static System.Windows.Forms.ImageList _imgList = new System.Windows.Forms.ImageList();
		/// <summary>
		/// 获取已缓存图标列表
		/// </summary>
		public static System.Windows.Forms.ImageList ImgList
		{
			get { return _imgList; }
		}

		/// <summary>
		/// 获取系统图标并缓存
		/// </summary>
		/// <param name="fsFullPath">完整路径</param>
		/// <param name="shFileInfo">同时传出文件系统信息</param>
		/// <param name="Expanded">是否获取展开图标</param>
		/// <returns></returns>
		public static Icon GetFileSystemIcon(string fsFullPath, ref Shell32.SHFILEINFO shFileInfo, bool Expanded = false)
		{
			if (!Path.IsPathRooted(fsFullPath))
			{
				return null;
			}

			Icon SmallIcon = null;
			if (File.Exists(fsFullPath))
			{//文件
				FileInfo diChild = new FileInfo(fsFullPath);
				string strExt = diChild.Extension.ToLower();
				if (strExt == ".exe")
				{
					strExt = diChild.FullName.ToLower();
				}

				SmallIcon = Shell32.GetFileInfo(strExt, ref shFileInfo);

				if (!ImgList.Images.ContainsKey(strExt))
				{
					ImgList.Images.Add(strExt, SmallIcon);
				}
			}
			else if (Directory.Exists(fsFullPath))
			{//文件夹或盘符
				if (fsFullPath.Length == 3)
				{//盘符
					string cDrive = fsFullPath.Substring(0, 1);
					DriveInfo di = new DriveInfo(cDrive);
					SmallIcon = Shell32.GetFileInfo(fsFullPath, ref shFileInfo);

					if (!ImgList.Images.ContainsKey(shFileInfo.szTypeName))
					{
						ImgList.Images.Add(shFileInfo.szTypeName, SmallIcon);
					}
				}
				else if (Expanded)
				{//文件夹展开
					if (!ImgList.Images.ContainsKey("文件夹展开"))
					{
						SmallIcon = Shell32.GetFolderIcon(ref shFileInfo, false, true);
						ImgList.Images.Add("文件夹展开", SmallIcon);
					}
					Bitmap bmp = new Bitmap(ImgList.Images["文件夹展开"]);
					SmallIcon = Icon.FromHandle(bmp.GetHicon());
				}
				else
				{//文件夹收起
					if (!ImgList.Images.ContainsKey("文件夹收起"))
					{
						SmallIcon = Apq.DllImports.Shell32.GetFolderIcon(ref shFileInfo);
						ImgList.Images.Add("文件夹收起", SmallIcon);
					}
					Bitmap bmp = new Bitmap(ImgList.Images["文件夹收起"]);
					SmallIcon = Icon.FromHandle(bmp.GetHicon());
				}
			}
			else
			{//盘符
				string cDrive = fsFullPath.Substring(0, 1);
				DriveInfo di = new DriveInfo(cDrive);
				SmallIcon = Shell32.GetFileInfo(fsFullPath, ref shFileInfo);

				if (!ImgList.Images.ContainsKey(shFileInfo.szTypeName))
				{
					ImgList.Images.Add(shFileInfo.szTypeName, SmallIcon);
				}
			}

			return SmallIcon;
		}
	}
}
