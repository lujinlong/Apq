using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Apq.DllImports
{
	/// <summary>
	/// Imm32
	/// </summary>
	public class Imm32
	{
		/// <summary>
		/// 全角
		/// </summary>
		public const int IME_CMODE_FULLSHAPE = 0x8;
		/// <summary>
		/// 半角
		/// </summary>
		public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;

		#region OpenStatus
		/// <summary>
		/// 获取输入法打开状态
		/// </summary>
		/// <param name="himc"></param>
		/// <returns></returns>
		[DllImport("Imm32.dll")]
		public static extern bool ImmGetOpenStatus(IntPtr himc);
		/// <summary>
		/// 设置输入法打开状态
		/// </summary>
		/// <param name="himc"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		[DllImport("Imm32.dll")]
		public static extern bool ImmSetOpenStatus(IntPtr himc, int b);
		#endregion

		/// <summary>
		/// 获取输入法句柄
		/// </summary>
		/// <param name="hwnd"></param>
		/// <returns></returns>
		[DllImport("Imm32.dll")]
		public static extern IntPtr ImmGetContext(IntPtr hwnd);

		/// <summary>
		/// 检索输入法信息
		/// </summary>
		/// <param name="himc"></param>
		/// <param name="lpdw"></param>
		/// <param name="lpdw2"></param>
		/// <returns></returns>
		[DllImport("Imm32.dll")]
		public static extern bool ImmGetConversionStatus(IntPtr himc, ref uint[] lpdw, ref uint[] lpdw2);
		/// <summary>
		/// 模拟按键(用于转换半角)
		/// </summary>
		/// <param name="hwnd"></param>
		/// <param name="lngHotkey"></param>
		/// <returns></returns>
		[DllImport("Imm32.dll")]
		public static extern int ImmSimulateHotKey(IntPtr hwnd, uint lngHotkey);
	}
}
