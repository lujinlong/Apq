using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Threading
{
	/// <summary>
	/// 线程
	/// </summary>
	public class Thread
	{
		/// <summary>
		/// [无异常]终止线程
		/// </summary>
		/// <param name="t"></param>
		public static void Abort(System.Threading.Thread t)
		{
			if (t != null)
			{
				if (t.ThreadState == System.Threading.ThreadState.Background
					|| t.ThreadState == System.Threading.ThreadState.Running
					|| t.ThreadState == System.Threading.ThreadState.Suspended
					|| t.ThreadState == System.Threading.ThreadState.WaitSleepJoin
					)
				{
					t.Abort();
				}
			}
		}

		/// <summary>
		/// 启动新线程
		/// </summary>
		/// <param name="pts"></param>
		/// <param name="param"></param>
		public static System.Threading.Thread StartNewThread(System.Threading.ParameterizedThreadStart pts, object param)
		{
			System.Threading.Thread t = new System.Threading.Thread(pts);
			t.Start(param);
			return t;
		}

		/// <summary>
		/// 启动新线程
		/// </summary>
		/// <param name="ts"></param>
		public static System.Threading.Thread StartNewThread(System.Threading.ThreadStart ts)
		{
			System.Threading.Thread t = new System.Threading.Thread(ts);
			t.Start();
			return t;
		}
	}
}
