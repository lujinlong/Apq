using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Windows.Interfaces
{
	/// <summary>
	/// IDataSetDTSControl
	/// </summary>
	public interface IWizardStep
	{
		/// <summary>
		/// 执行 上一步
		/// </summary>
		/// <returns></returns>
		IWizardStep Back();
		/// <summary>
		/// 执行 下一步
		/// </summary>
		/// <returns></returns>
		IWizardStep Next();
		/// <summary>
		/// 执行 完成
		/// </summary>
		void Finish();
		/// <summary>
		/// 执行 取消
		/// </summary>
		void Cancel();
	}
}
