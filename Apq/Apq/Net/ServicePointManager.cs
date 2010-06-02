using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Apq.Net
{
	/// <summary>
	/// ServicePointManager
	/// </summary>
	public class ServicePointManager
	{
		/// <summary>
		/// 用于忽略证书错误
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="certificate"></param>
		/// <param name="chain"></param>
		/// <param name="sslPolicyErrors"></param>
		/// <returns></returns>
		public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			//if (sslPolicyErrors == SslPolicyErrors.None) {
			//  return true;
			//}

			//Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

			//// Do not allow this client to communicate with unauthenticated servers.
			//return false;

			return true;
		}
	}
}
