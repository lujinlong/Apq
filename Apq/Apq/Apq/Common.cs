using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Apq
{
	/// <summary>
	/// 公用功能
	/// </summary>
	public class Common
	{
		#region GetCallingMethod
		/// <summary>
		/// GetCallingMethod(调用时注意需要加try-catch块防止优化)
		/// </summary>
		/// <returns></returns>
		public static MethodBase GetCallingMethod()
		{
			MethodBase mb = null;
			try
			{
				StackFrame frame = new StackFrame(2);
				mb = frame.GetMethod();
				return mb;
			}
			catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
			{
				Apq.GlobalObject.ApqLog.Debug("运行 GetCallingMethod() 时发生异常:", ex);
				return mb;
			}
		}

		/// <summary>
		/// GetCallingMethod(调用时注意需要加try-catch块防止优化)
		/// </summary>
		/// <param name="skipFrames">堆栈上要跳过的帧数</param>
		/// <returns></returns>
		public static MethodBase GetCallingMethod(int skipFrames)
		{
			MethodBase mb = null;
			try
			{
				StackFrame frame = new StackFrame(skipFrames);
				mb = frame.GetMethod();
				return mb;
			}
			catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
			{
				Apq.GlobalObject.ApqLog.Debug("运行 GetCallingMethod() 时发生异常:", ex);
				return mb;
			}
		}
		#endregion

		#region GetCallingClass
		/// <summary>
		/// GetCallingClass(调用时注意需要加try-catch块防止优化)
		/// </summary>
		/// <returns></returns>
		public static Type GetCallingClass()
		{
			Type t = typeof(object);
			try
			{
				MethodBase mb = GetCallingMethod(3);
				return mb.ReflectedType;
			}
			catch (System.Exception ex)	// 一般是不可能捕获到异常的,防止编译器优化
			{
				Apq.GlobalObject.ApqLog.Debug("运行 GetCallingMethod() 时发生异常:", ex);
				return t;
			}
		}
		#endregion
	}
}
