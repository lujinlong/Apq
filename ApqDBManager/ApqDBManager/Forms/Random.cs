using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ApqDBManager
{
	public partial class Random : Apq.Windows.Forms.DockForm
	{
		protected string _FileName = string.Empty;

		public Random()
		{
			InitializeComponent();
		}

		private void Random_Load(object sender, EventArgs e)
		{
			this.textEdit1.EditValue = "ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy0123456789";

			this.lstStrings.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("ABCDEFGHIJKLMNOPQRSTUVWXYZ"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("abcdefghijklmnopqrstuvwxyz"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("0123456789", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("()[]{}~$"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("()[]{}<>"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("`-=\\~!@#$%^&*_+|;\':\",./?"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy", System.Windows.Forms.CheckState.Checked)});

			//Apq.Xtra.Grid.Common.AddBehaivor(gridView1);
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			string[] ary = Apq.String.Random(Apq.Convert.ChangeType<uint>(spinEdit2.Value, 10), Apq.Convert.ChangeType<int>(spinEdit1.Value, 16), false, null, textEdit1.Text);
			random1.DataTable1.Clear();
			random1.DataTable1.AcceptChanges();

			foreach (string str in ary)
			{
				DataRow dr = random1.DataTable1.NewRow();
				dr["str"] = str;
				random1.DataTable1.Rows.Add(dr);
			}

			random1.DataTable1.AcceptChanges();
		}

		private void lstStrings_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (e.Index >= 0)
			{
				if (e.State == CheckState.Checked)
				{
					textEdit1.Text += lstStrings.Items[e.Index].Value.ToString();
				}
				if (e.State == CheckState.Unchecked)
				{
					if (textEdit1.Text.IndexOf(lstStrings.Items[e.Index].Value.ToString()) >= 0)
					{
						textEdit1.Text = textEdit1.Text.Replace(lstStrings.Items[e.Index].Value.ToString(), string.Empty);
					}
				}
			}
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			gridView1.SelectAll();
			gridView1.CopyToClipboard();
		}

		private void Random_FormClosing(object sender, FormClosingEventArgs e)
		{
			Apq.Windows.Forms.DelayConfirmBox dcb = new Apq.Windows.Forms.DelayConfirmBox(10, "确定退出");

			// 以下两种方式任选一种均可
			#region 事件方式
			dcb.NoClick += new EventHandler(delegate(object sender1, EventArgs e1)
			{
				e.Cancel = true;
			});
			dcb.CancelClick += new EventHandler(delegate(object sender1, EventArgs e1)
			{
				e.Cancel = true;
			});
			#endregion
			dcb.ShowDialog();
			#region 直接方式
			//if (dcb.ShowDialog() != DialogResult.Yes)
			//{
			//    e.Cancel = true;
			//}
			#endregion
		}

		private void btnGUID_Click(object sender, EventArgs e)
		{
			int nCount = Apq.Convert.ChangeType<int>(spinEdit2.Value, 10);
			string[] ary = new string[nCount];
			random1.DataTable1.Clear();
			random1.DataTable1.AcceptChanges();

			for (int i = 0; i < nCount; i++)
			{
				string str = System.Guid.NewGuid().ToString();
				DataRow dr = random1.DataTable1.NewRow();
				dr["str"] = str;
				random1.DataTable1.Rows.Add(dr);
			}

			random1.DataTable1.AcceptChanges();
		}
	}
}