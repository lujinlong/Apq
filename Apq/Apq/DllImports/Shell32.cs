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
		/// 定义获取资源标识
		/// </summary>
		public enum GetFileInfoFlags : uint
		{
			SHGFI_ICON = 0x000000100,     // get icon
			SHGFI_DISPLAYNAME = 0x000000200,     // get display name
			SHGFI_TYPENAME = 0x000000400,     // get type name
			SHGFI_ATTRIBUTES = 0x000000800,     // get attributes
			SHGFI_ICONLOCATION = 0x000001000,     // get icon location
			SHGFI_EXETYPE = 0x000002000,     // return exe type
			SHGFI_SYSICONINDEX = 0x000004000,     // get system icon index
			SHGFI_LINKOVERLAY = 0x000008000,     // put a link overlay on icon
			SHGFI_SELECTED = 0x000010000,     // show icon in selected state
			SHGFI_ATTR_SPECIFIED = 0x000020000,     // get only specified attributes
			SHGFI_LARGEICON = 0x000000000,     // get large icon
			SHGFI_SMALLICON = 0x000000001,     // get small icon
			SHGFI_OPENICON = 0x000000002,     // get open icon
			SHGFI_SHELLICONSIZE = 0x000000004,     // get shell size icon
			SHGFI_PIDL = 0x000000008,     // pszPath is a pidl
			SHGFI_USEFILEATTRIBUTES = 0x000000010,     // use passed dwFileAttribute
			SHGFI_ADDOVERLAYS = 0x000000020,     // apply the appropriate overlays
			SHGFI_OVERLAYINDEX = 0x000000040      // Get the index of the overlay
		}

		/// <summary>
		/// 定义文件属性标识
		/// </summary>
		public enum FileAttributeFlags : int
		{
			FILE_ATTRIBUTE_READONLY = 0x00000001,
			FILE_ATTRIBUTE_HIDDEN = 0x00000002,
			FILE_ATTRIBUTE_SYSTEM = 0x00000004,
			FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
			FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
			FILE_ATTRIBUTE_DEVICE = 0x00000040,
			FILE_ATTRIBUTE_NORMAL = 0x00000080,
			FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
			FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
			FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
			FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
			FILE_ATTRIBUTE_OFFLINE = 0x00001000,
			FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
			FILE_ATTRIBUTE_ENCRYPTED = 0x00004000
		}

		/// <summary>
		/// 定义SHFILEINFO结构
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public int iIcon;
			public int dwAttributes;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		}

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
		private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
		/// <summary>
		/// 获取文件图标+文件信息
		/// </summary>
		/// <param name="fileName">文件类型的扩展名 或 文件的绝对路径</param>
		/// <param name="shFileInfo">文件信息</param>
		/// <param name="isLargeIcon">是否获取大图标</param>
		public static Icon GetFileInfo(string fileName, ref SHFILEINFO shFileInfo, bool isLargeIcon = false)
		{
			GetFileInfoFlags uIconFlags = isLargeIcon ? GetFileInfoFlags.SHGFI_LARGEICON : GetFileInfoFlags.SHGFI_SMALLICON;
			SHFILEINFO shfi = new SHFILEINFO();
			SHGetFileInfo(fileName, (uint)FileAttributeFlags.FILE_ATTRIBUTE_NORMAL, ref shfi, (uint)Marshal.SizeOf(shfi),
				(uint)(GetFileInfoFlags.SHGFI_USEFILEATTRIBUTES |
					GetFileInfoFlags.SHGFI_ICON | uIconFlags |
					GetFileInfoFlags.SHGFI_TYPENAME | GetFileInfoFlags.SHGFI_DISPLAYNAME
				)
			);

			Icon icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;
			User32.DestroyIcon(shfi.hIcon); //释放资源
			return icon;
		}
		/// <summary>
		/// 获取文件夹图标+文件夹信息
		/// </summary>
		/// <param name="shFolderInfo">文件夹信息</param>
		/// <param name="isLargeIcon">是否获取大图标</param>
		/// <param name="isOpenState">是否打开状态</param>
		public static Icon GetFolderIcon(ref SHFILEINFO shFolderInfo, bool isLargeIcon = false, bool isOpenState = false)
		{
			GetFileInfoFlags uIconFlags = isLargeIcon ? GetFileInfoFlags.SHGFI_LARGEICON : GetFileInfoFlags.SHGFI_SMALLICON;
			GetFileInfoFlags uOpenFlags = isOpenState ? GetFileInfoFlags.SHGFI_OPENICON : 0;
			SHGetFileInfo(string.Empty, (uint)FileAttributeFlags.FILE_ATTRIBUTE_DIRECTORY, ref shFolderInfo, (uint)Marshal.SizeOf(shFolderInfo),
				(uint)(GetFileInfoFlags.SHGFI_USEFILEATTRIBUTES |
					GetFileInfoFlags.SHGFI_ICON | uIconFlags | uOpenFlags |
					GetFileInfoFlags.SHGFI_TYPENAME | GetFileInfoFlags.SHGFI_DISPLAYNAME
				)
			);

			Icon icon = Icon.FromHandle(shFolderInfo.hIcon).Clone() as Icon;
			User32.DestroyIcon(shFolderInfo.hIcon); //释放资源
			return icon;
		}
	}
}
