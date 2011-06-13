using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqDBManager.Forms
{
	public partial class ErrList : Apq.Windows.Forms.DockForm
	{
		public ErrList()
		{
			InitializeComponent();
		}

		private void ErrList_Load(object sender, EventArgs e)
		{
			Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dataGridView1);
			Apq.Windows.Forms.DataGridViewHelper.AddBehaivor(dataGridView1);
		}

		#region UI 公开方法
		/// <summary>
		/// 改变当前子窗口时调用
		/// </summary>
		public void Set_ErrList(ErrList_XSD xsd)
		{
			errListBindingSource.DataSource = xsd;
		}
		#endregion
	}
}
