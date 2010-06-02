using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Apq.Windows.Controls;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 基页面
	///		全角问题
	///		//配置文件
	/// </summary>
	/// <remarks>注意:DockForm与此基本相同,修改时应考虑同步修改相应部分</remarks>
	public partial class ImeForm : DevExpress.XtraEditors.XtraForm
	{
		/// <summary>
		/// ImeForm
		/// </summary>
		public ImeForm()
		{
			InitializeComponent();
			this.Size = new System.Drawing.Size(608, 456);	// 子窗口
			//this.Size = new System.Drawing.Size(896, 458);	// 主窗口
		}

		private void ImeForm_Load(object sender, EventArgs e)
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