using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ApqFtpWS
{
	/// <summary>
	/// Service1 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
	// [System.Web.Script.Services.ScriptService]
	public class wsFtp : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		/// <summary>
		/// Ftp上传请求入队
		/// </summary>
		/// <param name="Folder">文件目录</param>
		/// <param name="FileName">文件名</param>
		/// <param name="FtpSrv">Ftp服务器IP</param>
		/// <param name="FtpPort">端口</param>
		/// <param name="FtpU">用户名</param>
		/// <param name="FtpP">密码</param>
		/// <param name="FtpFolder">要上传到的Ftp目录</param>
		/// <returns></returns>
		[WebMethod]
		public void Enqueue(string Folder, string FileName, string FtpSrv, int FtpPort, string U, string P, string FtpFolder)
		{
			ApqFtpWS.App_Code.FtpQueue.Ftp_Enqueue(Folder, FileName, FtpSrv, FtpPort, U, P, FtpFolder);
		}
	}
}
