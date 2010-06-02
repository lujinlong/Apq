using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// Application
	/// </summary>
	public class Application
	{
		#region AppAssemblyName
		/// <summary>
		/// 处理事件的程序集全名
		/// </summary>
		public static string AppAssemblyName;
		private const string _AppAssemblyName = "Apq.Win";	// 默认值
		#endregion
		#region AppClassName
		/// <summary>
		/// 处理事件的类全名
		/// </summary>
		public static string AppClassName;
		private const string _AppClassName = "Apq.Windows.Forms.Application";	// 默认值
		#endregion

		#region App
		private static Application _App;
		/// <summary>
		/// 获取处理事件的对象
		/// </summary>
		public static Application App
		{
			get
			{
				if (_App == null)
				{
					#region AppAssemblyName
					if (Apq.Convert.LikeDBNull(AppAssemblyName))
					{
						AppAssemblyName = Apq.Win.GlobalObject.XmlAsmConfig["AppAssemblyName"];
					}
					if (Apq.Convert.LikeDBNull(AppAssemblyName))
					{
						AppAssemblyName = _AppAssemblyName;
					}
					#endregion
					#region AppClassName
					if (Apq.Convert.LikeDBNull(AppClassName))
					{
						AppClassName = Apq.Win.GlobalObject.XmlAsmConfig["AppClassName"];
					}
					if (Apq.Convert.LikeDBNull(AppClassName))
					{
						AppClassName = _AppClassName;
					}
					#endregion

					Assembly asm = System.Reflection.Assembly.Load(AppAssemblyName);
					_App = Activator.CreateInstance(asm.GetType(AppClassName)) as Application;
				}
				return _App;
			}
		}
		#endregion

		#region AddHandler
		/// <summary>
		/// 添加默认事件处理
		/// </summary>
		/// <param name="App">处理事件的对象</param>
		public static void AddHandler(Application App)
		{
			if (App != null)
			{
				System.Windows.Forms.Application.ApplicationExit += new EventHandler(App.Application_ApplicationExit);
				System.Windows.Forms.Application.ThreadExit += new EventHandler(App.Application_ThreadExit);
				System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(App.Application_ThreadException);
				System.Windows.Forms.Application.LeaveThreadModal += new EventHandler(App.Application_LeaveThreadModal);
				System.Windows.Forms.Application.EnterThreadModal += new EventHandler(App.Application_EnterThreadModal);
				System.Windows.Forms.Application.Idle += new EventHandler(App.Application_Idle);
			}
		}

		/// <summary>
		/// 添加默认事件处理
		/// </summary>
		public static void AddHandler()
		{
			AddHandler(App);
		}
		#endregion

		#region Handler
		/// <summary>
		/// 程序退出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Application_ApplicationExit(object sender, EventArgs e)
		{
			try
			{
				// 保存所有用户配置文件
				Apq.Config.ApqConfigs.SaveAllUserConfig();
				// 记录关闭日志
				Apq.GlobalObject.ApqLog.InfoFormat("{0} 应用程序[{1}]正常关闭", DateTime.Now, System.Windows.Forms.Application.ExecutablePath);
			}
			catch //(System.Exception ex)
			{
				//Environment.FailFast("关闭时异常,强制终止");
			}
		}

		/// <summary>
		/// 线程退出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Application_ThreadExit(object sender, EventArgs e)
		{
			try
			{
				Apq.GlobalObject.ApqLog.InfoFormat("{0} 线程[{1}]正常退出", DateTime.Now, Thread.CurrentThread.Name);
			}
			catch //(System.Exception ex)
			{
			}
		}

		/// <summary>
		/// 线程异常
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				Apq.Exception A_ex = new Apq.Exception(e.Exception);
				Apq.GlobalObject.ApqLog.Error("线程异常", A_ex.OriginalException);

				DelayConfirmBox dcb;
				switch (A_ex.Level)
				{
					case 1:	// 提示 + 可选
						dcb = new DelayConfirmBox(10, A_ex.OriginalException.Message + "\n\n是否继续运行?");
						dcb.Text = "错误";
						dcb.NoClick += new EventHandler(dcb_NoClick);
						dcb.Show();
						break;
					case 2:	// 提示 + 退出
						dcb = new DelayConfirmBox(10, A_ex.OriginalException.Message + "\n\n请重新运行程序.\n如果经常看到此对话框,请联系管理员");
						dcb.Text = "灾难性错误";
						dcb.YesClick += new EventHandler(dcb_NoClick);
						dcb.NoClick += new EventHandler(dcb_NoClick);
						dcb.CancelClick += new EventHandler(dcb_NoClick);
						dcb.ShowDialog();
						break;
				}
			}
			catch
			{
			}
		}

		private void dcb_NoClick(object sender, EventArgs e)
		{
			System.Windows.Forms.Application.Exit();
		}

		/// <summary>
		/// 离开模式状态
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Application_LeaveThreadModal(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 进入模式状态
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Application_EnterThreadModal(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 空闲
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Application_Idle(object sender, EventArgs e)
		{
		}
		#endregion

		/// <summary>
		/// 只运行一个实例
		/// </summary>
		/// <param name="mainForm"></param>
		public static void RunOnlyOne(System.Windows.Forms.Form mainForm)
		{
			Process instance = Apq.Diagnostics.Process.GetRunningAnotherInstance();
			if (instance == null)
			{
				Apq.Windows.Forms.Application.AddHandler();
				System.Windows.Forms.Application.Run(mainForm);
			}
			else
			{
				Apq.DllImports.User32.SetForegroundWindow(instance.MainWindowHandle);
			}
		}

	}
}
