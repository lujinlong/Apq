using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 进度条助手
	///     增加完成事件
	/// </summary>
	public class TSProgressBarHelper
	{
		private ToolStripProgressBar _tspb;
		/// <summary>
		/// 进度条装饰者
		/// </summary>
		/// <param name="tspb"></param>
		public TSProgressBarHelper(System.Windows.Forms.ToolStripProgressBar tspb)
		{
			 _tspb = tspb;
		}

		/// <summary>
		/// 进度条已填满
		/// </summary>
		public event Action<ToolStripProgressBar> Completed;

		/// <summary>
		/// 设置当前值
		/// </summary>
		/// <param name="Value"></param>
		public void SetValue(int Value)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripProgressBar>(_tspb.Owner, _tspb, delegate(ToolStripProgressBar ctrl)
			{
				_tspb.Value = Value;

				if (_tspb.Maximum > 0 && _tspb.Value >= _tspb.Maximum)
				{
					if (Completed != null)
					{
						Completed(_tspb);
					}
				}
				System.Windows.Forms.Application.DoEvents();
			});
		}
	}
}
