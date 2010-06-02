using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Apq.Runtime.Serialization.Formatters.Binary
{
	/// <summary>
	/// BinaryFormatter
	/// </summary>
	public class BinaryFormatter
	{
		/// <summary>
		/// 简单序列化
		/// </summary>
		/// <param name="obj">对象</param>
		/// <returns></returns>
		public static byte[] Serialize(object obj)
		{
			MemoryStream fs = new MemoryStream();
			try
			{
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				formatter.Serialize(fs, obj);
				return fs.ToArray();
			}
			finally
			{
				fs.Close();
			}
		}

		/// <summary>
		/// 简单反序列化
		/// </summary>
		/// <param name="bs"></param>
		/// <returns></returns>
		public static object Deserialize(byte[] bs)
		{
			MemoryStream fs = new MemoryStream();
			try
			{
				fs = new MemoryStream(bs);
				fs.Position = 0;
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				return formatter.Deserialize(fs);
			}
			finally
			{
				fs.Close();
			}
		}
	}
}
