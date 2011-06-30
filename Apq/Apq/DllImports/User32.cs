using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Apq.DllImports
{
	/// <summary>
	/// 
	/// </summary>
	public class User32
	{
		#region 常量
		/// <summary>
		/// 普通窗口
		/// </summary>
		public const int WS_SHOWNORMAL = 1;
		/// <summary>
		/// 通过操作系统获取是否已安装鼠标
		/// </summary>
		public const byte SM_MOUSEPRESENT = 19;
		/// <summary>
		/// 通过操作系统获取鼠标按键数
		/// </summary>
		public const byte SM_CMOUSEBUTTONS = 43;
		/// <summary>
		/// 通过操作系统获取鼠标是否具有滚轮
		/// </summary>
		public const byte SM_MOUSEWHEELPRESENT = 75;
		/// <summary>
		/// 鼠标移动事件
		/// </summary>
		public const int MOUSEEVENTF_MOVE = 0x1;
		/// <summary>
		/// 左键按下事件
		/// </summary>
		public const int MOUSEEVENTF_LEFTDOWN = 0x2;
		/// <summary>
		/// 左键放开事件
		/// </summary>
		public const int MOUSEEVENTF_LEFTUP = 0x4;
		/// <summary>
		/// 中键按下事件
		/// </summary>
		public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
		/// <summary>
		/// 中键放开事件
		/// </summary>
		public const int MOUSEEVENTF_MIDDLEUP = 0x40;
		/// <summary>
		/// 右键按下事件
		/// </summary>
		public const int MOUSEEVENTF_RIGHTDOWN = 0x8;
		/// <summary>
		/// 右键放开事件
		/// </summary>
		public const int MOUSEEVENTF_RIGHTUP = 0x10;
		#endregion

		#region 所需结构体
		/// <summary>
		/// 表示光标位置
		/// </summary>
		public struct POINTAPI
		{
			/// <summary>
			/// 光标X坐标
			/// </summary>
			public int x;
			/// <summary>
			/// 光标Y坐标
			/// </summary>
			public int y;
		}

		/// <summary>
		/// 表示矩形大小
		/// </summary>
		public struct RECT
		{
			/// <summary>
			/// left
			/// </summary>
			public int left;
			/// <summary>
			/// top
			/// </summary>
			public int top;
			/// <summary>
			/// right
			/// </summary>
			public int right;
			/// <summary>
			/// bottom
			/// </summary>
			public int bottom;
		}
		#endregion

		#region 操作系统
		/// <summary>
		/// 通过操作系统获取指定信息
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int GetSystemMetrics(int nIndex);
		#endregion

		#region 窗口
		/// <summary>
		/// 通过窗口句柄获取进程ID和线程ID
		/// </summary>
		/// <param name="hWnd">窗口句柄</param>
		/// <param name="lpdwProcessId">进程ID</param>
		/// <returns>线程ID</returns>
		[DllImport("User32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref Int64 lpdwProcessId);

		/// <summary>
		/// 获取窗口的屏幕范围
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

		/// <summary>
		/// 启用/禁用 窗口
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int EnableWindow(IntPtr hwnd, int fEnable);

		/// <summary>
		/// 显示/隐藏 窗口
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int type);

		/// <summary>
		/// 异步显示窗口
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="cmdShow"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

		/// <summary>
		/// 设置前台窗口
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// 查找窗口
		/// </summary>
		/// <param name="lpClassName"></param>
		/// <param name="lpWindowName">窗口名</param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		/// <summary>
		/// 查找子窗口
		/// </summary>
		/// <param name="hwndParent">父窗口</param>
		/// <param name="hwndChildAfter"></param>
		/// <param name="lpszClass"></param>
		/// <param name="lpszWindow"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
		#endregion

		#region 消息
		/// <summary>
		/// 给窗体发送信息
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="Msg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
		#endregion

		#region 键盘
		/// <summary>
		/// 获取键状态
		/// </summary>
		/// <param name="nVirtKey"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern short GetKeyState(int nVirtKey);
		#endregion

		#region 鼠标/光标
		/// <summary>
		/// 鼠标事件
		/// </summary>
		[DllImport("User32.dll", EntryPoint = "mouse_event")]
		public static extern void mouse_event(
			 int dwFlags,
			 int dx,
			 int dy,
			 int cButtons,
			 int dwExtraInfo
		);

		/// <summary>
		/// 交换鼠标左右键(左手键盘设置)
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int SwapMouseButton(int bSwap);

		/// <summary>
		/// 设置光标移动范围
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int ClipCursor(ref RECT lpRect);

		/// <summary>
		/// 显示/隐藏 光标
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static bool ShowCursor(bool bShow);

		/// <summary>
		/// 获取光标位置
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int GetCursorPos(ref POINTAPI lpPoint);

		/// <summary>
		/// 设置光标位置
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int SetCursorPos(int x, int y);

		/// <summary>
		/// 获取鼠标双击时间
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int GetDoubleClickTime();

		/// <summary>
		/// 设置鼠标双击时间
		/// </summary>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public extern static int SetDoubleClickTime(int wCount);
		#endregion

		#region 文件系统
		/// <summary>
		/// 释放图标资源
		/// </summary>
		/// <param name="hIcon"></param>
		/// <returns></returns>
		[DllImport("User32.dll", EntryPoint = "DestroyIcon")]
		public static extern int DestroyIcon(IntPtr hIcon);
		#endregion
	}
}
