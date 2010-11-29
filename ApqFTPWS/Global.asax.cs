using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using ApqFtpWS.App_Code;
using System.IO;

namespace ApqFtpWS
{
	public class Global : System.Web.HttpApplication
	{
		private Thread _thdFtp = null;

		protected void Application_Start(object sender, EventArgs e)
		{
			// 加载Ftp队列数据
			FtpQueue.FtpFile = GlobalObject.XmlConfigChain[typeof(FtpQueue), "FtpFile"];
			if (FtpQueue.FtpFile != null && File.Exists(FtpQueue.FtpFile)) FtpQueue.dsFtp.ReadXml(FtpQueue.FtpFile);

			// 启动Ftp队列线程
			_thdFtp = new Thread(thdFtp_Start);
			_thdFtp.Start();
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{
			// 停止Ftp队列线程
			Apq.Threading.Thread.Abort(_thdFtp);
		}

		/// <summary>
		/// 执行Ftp上传
		/// </summary>
		private void thdFtp_Start()
		{
			while (true)
			{
				try
				{
					using (StreamWriter sr = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\log\Running.log", false))
					{
						sr.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					}
					if (FtpQueue.dsFtp.Tables.Count > 0)
					{
						for (int i = FtpQueue.dsFtp.Tables[0].Rows.Count - 1; i >= 0; i--)
						{
							int ID = Apq.Convert.ChangeType<int>(FtpQueue.dsFtp.Tables[0].Rows[i]["ID"], -1);
							FtpQueue.Ftp_Put(ID);
						}
					}
				}
				catch { }

				int ms = Apq.Convert.ChangeType<int>(GlobalObject.XmlConfigChain[typeof(Global), "Sleep"], 300000);
				Thread.Sleep(ms);
			}
		}
	}
}