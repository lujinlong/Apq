using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace QQUnbindTXZ
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Thread.CurrentThread.IsBackground = true;
			Application.Run(GlobalObject.MainForm);
			//Apq.Windows.Forms.Application.RunOnlyOne(GlobalObject.MainForm);
		}
	}
}