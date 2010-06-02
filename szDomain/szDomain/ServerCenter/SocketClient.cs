using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

namespace szDomain.ServerCenter
{
	/// <summary>
	/// 客户端
	/// </summary>
	public class SocketClient : IManager
	{
		#region IManager 成员

		/*
		/// <summary>
		/// T人封号
		/// </summary>
		/// <param name="ServerName">服务器名</param>
		/// <param name="strct"></param>
		public void TF(string ServerName, Structs.KickAndForbid strct)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		 * */

		/// <summary>
		/// T人
		/// </summary>
		/// <param name="IP">IP</param>
		/// <param name="Port">Port</param>
		/// <param name="strct"></param>
		public void T(string IP, int Port, Structs.Kick strct)
		{
			Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				System.Net.IPEndPoint ipe = new System.Net.IPEndPoint(IPAddress.Parse(IP), Port);
				s.Connect(ipe);
				byte[] bs = strct.Serialize();
				s.SendTo(bs, ipe);
			}
			catch (Exception ex)
			{
				Apq.GlobalObject.ApqLog.Debug("Socket发送时异常:", ex);
			}
			finally
			{
				s.Close();
			}
		}

		#endregion
	}
}
