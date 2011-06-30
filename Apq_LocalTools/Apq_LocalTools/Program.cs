using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Apq_LocalTools
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
			Apq.Windows.Forms.Application.AddHandler();
			Application.Run(GlobalObject.MainForm);
			//Apq.Windows.Forms.Application.RunOnlyOne(GlobalObject.MainForm);
		}
	}
}