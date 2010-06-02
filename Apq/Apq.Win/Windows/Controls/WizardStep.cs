using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Controls
{
	/// <summary>
	/// 向导步骤控件基类
	/// </summary>
	public partial class WizardStep : UserControl, Apq.Windows.Interfaces.IWizardStep
	{
		/// <summary>
		/// WizardStep
		/// </summary>
		public WizardStep()
		{
			InitializeComponent();
		}

		private int _StepIndex = 0;
		/// <summary>
		/// 获取或设置 当前步骤编号
		/// </summary>
		public int StepIndex
		{
			get { return _StepIndex; }
			internal set { _StepIndex = value; }
		}

		#region IWizardStep 成员

		/// <summary>
		/// 执行 上一步
		/// </summary>
		/// <returns></returns>
		public virtual Apq.Windows.Interfaces.IWizardStep Back()
		{
			throw new System.Exception("The method or operation is not implemented.");
		}

		/// <summary>
		/// 执行 下一步
		/// </summary>
		/// <returns></returns>
		public virtual Apq.Windows.Interfaces.IWizardStep Next()
		{
			throw new System.Exception("The method or operation is not implemented.");
		}

		/// <summary>
		/// 执行 完成
		/// </summary>
		public virtual void Finish()
		{
			throw new System.Exception("The method or operation is not implemented.");
		}

		/// <summary>
		/// 执行 取消
		/// </summary>
		public virtual void Cancel()
		{
			throw new System.Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
