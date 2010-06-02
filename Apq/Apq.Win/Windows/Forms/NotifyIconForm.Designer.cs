namespace Apq.Windows.Forms
{
	partial class NotifyIconForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.timerNotifyIcon1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// timerNotifyIcon1
			// 
			this.timerNotifyIcon1.Interval = 500;
			this.timerNotifyIcon1.Tick += new System.EventHandler(this.timerNotifyIcon1_Tick);
			// 
			// NotifyIconForm
			// 
			this.ClientSize = new System.Drawing.Size(292, 267);
			this.Name = "NotifyIconForm";
			this.SizeChanged += new System.EventHandler(this.NotifyIconForm_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// notifyIcon1
		/// </summary>
		public System.Windows.Forms.NotifyIcon notifyIcon1;
		/// <summary>
		/// 计时器,用于闪动图标
		/// </summary>
		public System.Windows.Forms.Timer timerNotifyIcon1;

	}
}
