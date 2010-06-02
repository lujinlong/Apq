using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// DataSet 导入导出向导
	/// </summary>
	public partial class WizardDataSet : Apq.Windows.Forms.Wizard
	{
		/// <summary>
		/// WizardDataSet, 请先设置方向,数据集ds, 然后 Show
		/// </summary>
		public WizardDataSet()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 设置数据集
		/// </summary>
		public DataSet ds
		{
			get { return _ds; }
			set { _ds = value; }
		}

		private void WizardDataSet_Load(object sender, EventArgs e)
		{
			btnNext_Click(btnNext, e);
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			StepIndex--;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			StepIndex++;
		}

		private void btnFinish_Click(object sender, EventArgs e)
		{
			StepIndex = 3;
			// 执行导出

			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// 执行Step1-->Step2需要的操作
		/// </summary>
		/// <param name="Step1"></param>
		/// <param name="Step2"></param>
		private void StepChanged(int Step1, int Step2)
		{
			// 打开界面
			if (Step1 == 0)
			{
			}
			// 1-->2
			else if (Step1 == 1 && Step2 == 2)
			{
			}
			// 完成
			else if (Step2 == 3)
			{
			}
			// 2-->1
			else if (Step1 == 2 && Step2 == 1)
			{
			}
		}
	}
}

