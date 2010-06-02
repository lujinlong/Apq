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
	/// 向导窗口
	/// </summary>
	public partial class Wizard : ImeForm
	{
		/// <summary>
		/// 向导窗口
		/// </summary>
		public Wizard()
		{
			InitializeComponent();
		}

		private void Wizard_Load(object sender, EventArgs e)
		{

		}

		private int _StepIndex;
		/// <summary>
		/// 当前步骤编号
		/// </summary>
		public int StepIndex
		{
			get { return _StepIndex; }
			set { _StepIndex = value; }
		}
	}
}