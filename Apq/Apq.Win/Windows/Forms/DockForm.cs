using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Apq.Windows.Controls;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 可停靠窗口
	///		全角问题
	///		//配置文件
	/// </summary>
	/// <remarks>注意:ImeForm与此基本相同,修改时应考虑同步修改相应部分</remarks>
	public partial class DockForm : DockContent
	{
		/// <summary>
		/// DockForm
		/// </summary>
		public DockForm()
		{
			InitializeComponent();
		}

		private void DockForm_Load(object sender, EventArgs e)
		{
			Apq.Windows.Controls.Control.AddImeHandler(this);
		}

		#region MainBackThread
		/// <summary>
		/// 获取或设置主后台线程(记录于Tag中)
		/// </summary>
		public Thread MainBackThread
		{
			get { return this.GetControlValues("__MainBackThread") as Thread; }
			set { this.SetControlValues("__MainBackThread", value); }
		}
		#endregion
	}
}
