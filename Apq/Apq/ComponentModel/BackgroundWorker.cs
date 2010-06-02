using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.ComponentModel
{
	/// <summary>
	/// BackgroundWorker
	/// </summary>
	public class BackgroundWorker
	{
		/// <summary>
		/// [可重入]取消后台操作
		/// </summary>
		/// <param name="bw"></param>
		public static void CancelAsync(System.ComponentModel.BackgroundWorker bw)
		{
			if (bw.IsBusy && !bw.CancellationPending)
			{
				bw.CancelAsync();
			}
		}
	}
}
