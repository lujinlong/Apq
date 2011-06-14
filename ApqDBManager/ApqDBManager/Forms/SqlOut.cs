using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ApqDBManager.Controls;

namespace ApqDBManager.Forms
{
	public partial class SqlOut : Apq.Windows.Forms.DockForm
	{
		public SqlOut()
		{
			InitializeComponent();
		}

		private void SqlOut_Load(object sender, EventArgs e)
		{
		}

		#region cms2
		//定位对应节点
		private void tsmiShowNode_Click(object sender, EventArgs e)
		{
			GlobalObject.SolutionExplorer.FocusAndExpandByID(Apq.Convert.ChangeType<int>(tabControl1.SelectedTab.Tag));
		}
		#endregion

		#region UI 公开方法
		/// <summary>
		/// 改变当前子窗口时调用
		/// </summary>
		public void Set_Out(List<DataSet> lstds)
		{
			Apq.Windows.Delegates.Action_UI<TabControl>(this, tabControl1, delegate(TabControl ctrl)
			{
				tabControl1.TabPages.Clear();
				foreach (DataSet ds in lstds)
				{
					TabPage tp = new TabPage(ds.DataSetName);
					tabControl1.TabPages.Add(tp);
					tp.Tag = ds.ExtendedProperties["SqlID"];

					ResultTable rt = new ResultTable();
					tp.Controls.Add(rt);

					rt.Dock = System.Windows.Forms.DockStyle.Fill;
					rt.Location = new System.Drawing.Point(0, 0);
					rt.Margin = new System.Windows.Forms.Padding(0);
					rt.BackDataSet = ds;
				}
			});
		}

		/// <summary>
		/// 显示指定名称的结果页
		/// </summary>
		/// <param name="Name"></param>
		public void ShowTabPage(string TabPageName)
		{
			foreach (TabPage tp in tabControl1.TabPages)
			{
				if (tp.Text == TabPageName)
				{
					tabControl1.SelectedTab = tp;
					return;
				}
			}
		}

		/// <summary>
		/// 获取当前有结果显示的名称列表
		/// </summary>
		/// <returns></returns>
		public string[] GetCurrentResultNames()
		{
			string[] aryNames = new string[tabControl1.TabPages.Count];
			for (int i = 0; i < aryNames.Length; i++)
			{
				aryNames[i] = tabControl1.TabPages[i].Text;
			}
			return aryNames;
		}
		#endregion
	}
}
