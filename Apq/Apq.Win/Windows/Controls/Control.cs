using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public class Control
	{
		#region 解决输入法全角问题
		/// <summary>
		/// 为控件及其子控件添加事件,控制输入法初始为半角
		/// </summary>
		/// <param name="Ctrl">控件/窗体</param>
		public static void AddImeHandler(System.Windows.Forms.Control Ctrl)
		{
			if (Ctrl != null)
			{
				#region ControlAdded
				Ctrl.ControlAdded += new System.Windows.Forms.ControlEventHandler(Ctrl_ControlAdded);
				#endregion

				#region Enter 和 CellEnter

				Ctrl.Enter += new EventHandler(Ctrl_Enter);

				Type t = Ctrl.GetType();
				System.Reflection.EventInfo ei = t.GetEvent("CellEnter");
				if (ei != null)
				{
					ei.AddEventHandler(Ctrl, new System.Windows.Forms.DataGridViewCellEventHandler(Ctrl_Enter));
				}

				#endregion

				if (Ctrl.Controls != null)
				{
					foreach (System.Windows.Forms.Control ctr in Ctrl.Controls)
					{
						AddImeHandler(ctr);
					}
				}
			}
		}

		/// <summary>
		/// 添加控件时,设置事件处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Ctrl_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
		{
			// 为控件添加输入法处理,即调用 AddImeHandler 方法
			AddImeHandler(e.Control);
		}
		/// <summary>
		/// 进入控件时,控制输入法为半角
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Ctrl_Enter(object sender, EventArgs e)
		{
			System.Windows.Forms.Control Ctrl = sender as System.Windows.Forms.Control;
			if (Ctrl == null)
			{
				return;
			}

			IntPtr HIme = Apq.DllImports.Imm32.ImmGetContext(Ctrl.Handle);
			if (Apq.DllImports.Imm32.ImmGetOpenStatus(HIme))
			{
				uint[] iMode = new uint[] { 0 };
				uint[] iSentence = new uint[] { 0 };
				if (Apq.DllImports.Imm32.ImmGetConversionStatus(HIme, ref iMode, ref iSentence))
				{
					if ((iMode[0] & Apq.DllImports.Imm32.IME_CMODE_FULLSHAPE) > 0) //如果是全角
					{
						Apq.DllImports.Imm32.ImmSimulateHotKey(Ctrl.Handle, Apq.DllImports.Imm32.IME_CHOTKEY_SHAPE_TOGGLE); //转换成半角
					}
				}
			}
		}
		#endregion
	}
}
