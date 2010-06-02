using System;
using System.Collections.Generic;
using System.Text;

namespace szDomain.ServerCenter.Structs
{
	/// <summary>
	/// 服务器结构体
	/// </summary>
	public interface IServerStruct
	{
		/// <summary>
		/// 序列化,用于Socket发送数据
		/// </summary>
		/// <returns></returns>
		byte[] Serialize();
	}
}
