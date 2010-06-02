using System;
using System.Collections.Generic;
using System.Text;

namespace szDomain.ServerCenter.Structs
{
	/// <summary>
	/// 服务器结构抽象基类
	/// </summary>
	public abstract class ServerStruct
	{
		private int _RequestID;
		/// <summary>
		/// 请求号
		/// </summary>
		public int RequestID
		{
			get { return _RequestID; }
			protected set { _RequestID = value; }
		}

		/// <summary>
		/// 获取编码格式
		/// </summary>
		protected Encoding STEncoding
		{
			get
			{
				string strEncoding = GlobalObject.XmlAsmConfig["Encoding"];
				if (string.IsNullOrEmpty(strEncoding))
				{
					// 默认 gb2312
					strEncoding = "gb2312";
				}
				try
				{
					return Encoding.GetEncoding(strEncoding);
				}
				catch
				{
					return Encoding.GetEncoding("gb2312");
				}
			}
		}
	}
}
