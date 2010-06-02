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
	/// NotifyIconForm
	/// </summary>
	public partial class NotifyIconForm : Apq.Windows.Forms.ImeForm
	{
		private FormWindowState _FormWindowState;

		private Icon _IconClarity = null;
		/// <summary>
		/// 获取或设置透明图标(闪动时切换使用)
		/// </summary>
		public Icon IconClarity
		{
			get
			{
				if (_IconClarity == null)
				{
					string strIconClarity = Apq.Win.GlobalObject.XmlAsmConfig["IconClarity"];
					if (string.IsNullOrEmpty(strIconClarity))
					{
						strIconClarity = System.Windows.Forms.Application.StartupPath + @"\Res\ico\clarity.ico";
					}
					try
					{
						_IconClarity = new Icon(strIconClarity);
					}
					catch { }
				}
				return _IconClarity;
			}
			set { _IconClarity = value; }
		}

		private Icon _NotifyIcon;
		/// <summary>
		/// 获取或设置 NotifyIcon
		/// </summary>
		public Icon NotifyIcon
		{
			get { return _NotifyIcon; }
			set { _NotifyIcon = value; }
		}

		/// <summary>
		/// NotifyIconForm
		/// </summary>
		public NotifyIconForm()
		{
			InitializeComponent();
		}

		private void NotifyIconForm_SizeChanged(object sender, EventArgs e)
		{
			_FormWindowState = WindowState;
			if (WindowState == FormWindowState.Minimized)
			{
				Hide();
				notifyIcon1.Icon = NotifyIcon;
				notifyIcon1.Visible = true;
			}
		}

		/// <summary>
		/// 点击右下角图标,窗口最大化,停止闪动
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void notifyIcon1_Click(object sender, EventArgs e)
		{
			Show();
			WindowState = _FormWindowState;
			timerNotifyIcon1.Stop();
			notifyIcon1.Visible = false;
			Focus();
		}

		private void timerNotifyIcon1_Tick(object sender, EventArgs e)
		{
			notifyIcon1.Icon = notifyIcon1.Icon == IconClarity ? NotifyIcon : IconClarity;
		}
	}
}
