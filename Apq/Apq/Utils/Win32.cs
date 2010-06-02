using System;
using System.Runtime.InteropServices;

namespace Apq.Utils
{
	/// <summary>
	/// Wrapper for Win32 API calls
	/// </summary>
	public class Win32
	{
		/// <summary>
		/// DriveType
		/// </summary>
		public enum DriveType
		{
			/// <summary>
			/// 
			/// </summary>
			Unknown = 0,
			/// <summary>
			/// 
			/// </summary>
			NoRootDirectory = 1,
			/// <summary>
			/// 
			/// </summary>
			Removable = 2,
			/// <summary>
			/// 
			/// </summary>
			Fixed = 3,
			/// <summary>
			/// 
			/// </summary>
			Remote = 4,
			/// <summary>
			/// 
			/// </summary>
			CdRom = 5,
			/// <summary>
			/// 
			/// </summary>
			RamDisk = 6
		}

		/// <summary>
		/// Returns the name of a logical drive
		/// </summary>
		/// <param name="driveLetter"></param>
		/// <returns>the name of the drive in the format "a:\"</returns>
		public static string GetVolumeName(String driveLetter)
		{
			const int namelen = 1025;
			int serial, maxCompLen, flags;
			byte[] namebuf = new byte[namelen];
			byte[] sysname = new byte[namelen];
			Apq.DllImports.Kernel32.GetVolumeInformation(driveLetter, namebuf, namelen, out serial, out maxCompLen, out flags, sysname, namelen);
			string name = System.Text.Encoding.ASCII.GetString(namebuf);
			name = name.Substring(0, name.IndexOf('\0'));
			return name;
		}

		/// <summary>
		/// Returns the type of the logical drive
		/// </summary>
		/// <param name="driveLetter"></param>
		/// <returns>the name of the drive in the format "a:\"</returns>
		public static DriveType GetDriveType(String driveLetter)
		{
			return (DriveType)Apq.DllImports.Kernel32.GetDriveType(driveLetter);
		}
	}
}
