using System;
using System.Collections.Generic;
using System.Text;

namespace szDomain.ServerCenter
{
	/// <summary>
	/// 管理接口
	/// </summary>
	public interface IManager
	{
		/*
		/// <summary>
		/// T人封号
		/// </summary>
		/// <param name="ServerName">服务器名</param>
		/// <param name="strct"></param>
		void TF(string ServerName, Structs.KickAndForbid strct);
		 * */
		/// <summary>
		/// T人
		/// </summary>
		/// <param name="IP">IP</param>
		/// <param name="Port">Port</param>
		/// <param name="strct"></param>
		void T(string IP, int Port, Structs.Kick strct);
	}
}
