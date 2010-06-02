using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

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
			Apq.Xtra.Grid.Common.AddBehaivor(gridView1);
			this.gridView1.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(Apq.Xtra.Grid.Common.gv_CustomDrawRowIndicator);
		}

		#region UI 公开方法
		/// <summary>
		/// 改变当前子窗口时调用
		/// </summary>
		public void Set_ErrList(ApqDBManager.XSD.UI ui)
		{
			gridControl1.DataSource = ui;
		}
		#endregion
	}
}
