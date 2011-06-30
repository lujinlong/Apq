using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Apq.DllImports
{
	/// <summary>
	/// Shell32
	/// </summary>
	public class Shell32
	{
		/// <summary>
		/// 获取文件信息
		/// </summary>
		/// <param name="pszPath">文件名(需要无扩展名文件信息时请传入不带扩展名的任意字符串)</param>
		/// <param name="dwFileAttributes">文件属性标记(过滤)</param>
		/// <param name="psfi">保存文件信息结果</param>
		/// <param name="cbFileInfo">指示文件信息结构大小</param>
		/// <param name="uFlags">指示过滤标记使用情况</param>
		/// <returns></returns>
		[DllImport("Shell32.dll", EntryPoint = "SHGetFileInfo", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
		#region API 参数的常量定义
		private const uint SHGFI_ICON = 0x100;
		private const uint SHGFI_LARGEICON = 0x0; //大图标 32×32
		private const uint SHGFI_SMALLICON = 0x1; //小图标 16×16
		private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
		#endregion
		/// <summary>
		/// 保存文件信息的结构体
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHFILEINFO
		{
			/// <summary>
			/// 图标
			/// </summary>
			public IntPtr hIcon;
			/// <summary>
			/// 图标索引
			/// </summary>
			public int iIcon;
		}
		/// <summary>
		/// 获取文件类型的关联图标
		/// </summary>
		/// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
		/// <param name="isLargeIcon">是否返回大图标</param>
		/// <returns>获取到的图标</returns>
		public static Icon GetIcon(string fileName, bool isLargeIcon)
		{
			SHFILEINFO shfi = new SHFILEINFO();
			IntPtr hI;
			if (isLargeIcon)
			{
				hI = SHGetFileInfo(fileName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | SHGFI_LARGEICON);
			}
			else
			{
				hI = SHGetFileInfo(fileName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | SHGFI_SMALLICON);
			}
			Icon icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;
			User32.DestroyIcon(shfi.hIcon); //释放资源
			return icon;
		}
	}
}
