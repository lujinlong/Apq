using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.Sim
{
	/// <summary>
	/// 模拟鼠标
	/// </summary>
	public class Mouse
	{
		/// <summary>
		/// 移动鼠标
		/// </summary>
		/// <param name="From_Handle_ToInt32">From控件Handle</param>
		/// <param name="To_Handle_ToInt32">To控件Handle</param>
		public static void Move(int From_Handle_ToInt32, int To_Handle_ToInt32)
		{
			DllImports.User32.RECT rectFrom = new DllImports.User32.RECT();
			DllImports.User32.RECT rectTo = new DllImports.User32.RECT();
			int i;
			DllImports.User32.GetWindowRect(From_Handle_ToInt32, ref rectFrom);
			DllImports.User32.GetWindowRect(To_Handle_ToInt32, ref rectTo);

			if ((rectFrom.left + rectFrom.right) / 2 - (rectTo.left + rectTo.right) / 2 > 0)
			{
				for (i = (rectFrom.left + rectFrom.right) / 2; i >= (rectTo.left + rectTo.right) / 2; i--)
				{
					DllImports.User32.SetCursorPos(i, (rectFrom.top + rectFrom.bottom) / 2);
					DllImports.Kernel32.Sleep(1);
				}
			}
			else
			{
				for (i = (rectFrom.left + rectFrom.right) / 2; i <= (rectTo.left + rectTo.right) / 2; i++)
				{
					DllImports.User32.SetCursorPos(i, (rectFrom.top + rectFrom.bottom) / 2);
					DllImports.Kernel32.Sleep(1);
				}
			}

			if ((rectFrom.top + rectFrom.bottom) / 2 - (rectTo.top + rectTo.bottom) / 2 > 0)
			{
				for (i = (rectFrom.top + rectFrom.bottom) / 2; i >= (rectTo.top + rectTo.bottom) / 2; i--)
				{
					DllImports.User32.SetCursorPos((rectTo.left + rectTo.right) / 2, i);
					DllImports.Kernel32.Sleep(1);
				}
			}
			else
			{
				for (i = (rectFrom.top + rectFrom.bottom) / 2; i <= (rectTo.top + rectTo.bottom) / 2; i++)
				{
					DllImports.User32.SetCursorPos((rectTo.left + rectTo.right) / 2, i);
					DllImports.Kernel32.Sleep(1);
				}
			}
		}

		/// <summary>
		/// 获取鼠标类型
		/// </summary>
		public static string Type
		{
			get
			{
				if (DllImports.User32.GetSystemMetrics(DllImports.User32.SM_MOUSEPRESENT) == 0)
				{
					return "本计算机尚未安装鼠标";
				}
				else
				{
					if (DllImports.User32.GetSystemMetrics(DllImports.User32.SM_MOUSEWHEELPRESENT) != 0)
					{
						return DllImports.User32.GetSystemMetrics(DllImports.User32.SM_CMOUSEBUTTONS) + "键滚轮鼠标";
					}
					else
					{
						return DllImports.User32.GetSystemMetrics(DllImports.User32.SM_CMOUSEBUTTONS) + "键鼠标";
					}
				}
			}
		}

		/// <summary>
		/// 获取或设置鼠标双击时间
		/// </summary>
		public static int DoubleClickTime
		{
			get { return DllImports.User32.GetDoubleClickTime(); }
			set { DllImports.User32.SetDoubleClickTime(value); }
		}

		/// <summary>
		/// 设置鼠标右键为确认(左手鼠标)
		/// </summary>
		public static void DefaultRightButton()
		{
			DllImports.User32.SwapMouseButton(1);
		}

		/// <summary>
		/// 设置鼠标左键为确认(右手鼠标)
		/// </summary>
		public static void DefaultLeftButton()
		{
			DllImports.User32.SwapMouseButton(0);
		}

		/// <summary>
		/// 左键按下
		/// </summary>
		private static void LeftDown()
		{
			DllImports.User32.mouse_event(DllImports.User32.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
		}

		/// <summary>
		/// 左键放开
		/// </summary>
		private static void LeftUp()
		{
			DllImports.User32.mouse_event(DllImports.User32.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		}

		/// <summary>
		/// 单击左键
		/// </summary>
		public static void LeftClick()
		{
			LeftDown();
			LeftUp();
		}

		/// <summary>
		/// 中键按下
		/// </summary>
		private static void MiddleDown()
		{
			DllImports.User32.mouse_event(DllImports.User32.MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
		}

		/// <summary>
		/// 中键放开
		/// </summary>
		private static void MiddleUp()
		{
			DllImports.User32.mouse_event(DllImports.User32.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
		}

		/// <summary>
		/// 单击中键
		/// </summary>
		public static void MiddleClick()
		{
			MiddleDown();
			MiddleUp();
		}

		/// <summary>
		/// 右键按下
		/// </summary>
		private static void RightDown()
		{
			DllImports.User32.mouse_event(DllImports.User32.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
		}

		/// <summary>
		/// 右键放开
		/// </summary>
		private static void RightUp()
		{
			DllImports.User32.mouse_event(DllImports.User32.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
		}

		/// <summary>
		/// 单击右键
		/// </summary>
		public static void RightClick()
		{
			RightDown();
			RightUp();
		}
	}
}
