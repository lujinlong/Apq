using System;
using System.Collections.Generic;
using System.Text;

namespace Apq
{
	/// <summary>
	/// Locks
	/// </summary>
	public class Locks
	{
		/// <summary>
		/// 获取文件锁
		/// </summary>
		/// <param name="FullFileName">文件全名(含路径)</param>
		/// <returns></returns>
		public static object GetFileLock(string FullFileName)
		{
			string LockName = "file://" + FullFileName;
			if (!Apq.GlobalObject.Locks.ContainsKey(LockName))
			{
				object obj = new object();
				Apq.GlobalObject.Locks.Add(LockName, obj);
			}
			return Apq.GlobalObject.Locks[LockName];
		}
	}
}
