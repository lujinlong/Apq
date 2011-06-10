using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// DelayConfirmBox
	/// </summary>
	public partial class DelayConfirmBox : ImeForm
	{
		/// <summary>
		/// YesClick
		/// </summary>
		public event System.EventHandler YesClick;
		/// <summary>
		/// NoClick
		/// </summary>
		public event System.EventHandler NoClick;
		/// <summary>
		/// CancelClick
		/// </summary>
		public event System.EventHandler CancelClick;

		/// <summary>
		/// DelayConfirmBox
		/// </summary>
		public DelayConfirmBox()
		{
			InitializeComponent();
		}

		/// <summary>
		/// DelayConfirmBox
		/// </summary>
		/// <param name="Delay">等待时长(秒)</param>
		/// <param name="Msg">显示信息</param>
		public DelayConfirmBox(int Delay, string Msg)
			: this()
		{
			this.Delay = Delay;
			_Current = Delay;
			this.Msg = Msg;
		}

		private int _Delay;
		/// <summary>
		/// 获取或设置等待时长(秒)
		/// </summary>
		public int Delay
		{
			get { return _Delay; }
			set { _Delay = value; }
		}
		private int _Current;

		/// <summary>
		/// 获取或设置显示信息
		/// </summary>
		public string Msg
		{
			get { return lblMsg.Text; }
			set { lblMsg.Text = value; }
		}

		private void DelayConfirmBox_Load(object sender, EventArgs e)
		{
		}

		private void DelayConfirmBox_Shown(object sender, EventArgs e)
		{
			if (Delay > 0)
			{
				timer1.Start();
			}
		}

		#region 按钮回调(事件方式)
		private void btnYes_Click(object sender, EventArgs e)
		{
			timer1.Stop();
			Close();
			DialogResult = DialogResult.Yes;
			if (YesClick != null)
			{
				YesClick(sender, e);
			}
		}

		private void btnNo_Click(object sender, EventArgs e)
		{
			timer1.Stop();
			Close();
			DialogResult = DialogResult.No;
			if (NoClick != null)
			{
				NoClick(sender, e);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			timer1.Stop();
			Close();
			DialogResult = DialogResult.Cancel;
			if (CancelClick != null)
			{
				CancelClick(sender, e);
			}
		}
		#endregion

		#region 计时器
		private void timer1_Tick(object sender, EventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<System.Windows.Forms.Button>(this, btnYes, new Action<System.Windows.Forms.Button>(
					delegate(System.Windows.Forms.Button btn)
					{
						btn.Text = string.Format("是({0})", _Current);
					}
				)
			);

			if (_Current < 0)
			{
				timer1.Stop();
				Apq.Windows.Delegates.Action_UI<System.Windows.Forms.Button>(this, btnYes, new Action<System.Windows.Forms.Button>(
						delegate(System.Windows.Forms.Button btn)
						{
							btnYes_Click(sender, e);
						}
					)
				);
			}
			else
			{
				_Current--;
			}
		}
		#endregion
	}
}