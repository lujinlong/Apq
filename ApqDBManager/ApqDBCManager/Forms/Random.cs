using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections;

namespace ApqDBCManager
{
	public partial class Random : Apq.Windows.Forms.DockForm
	{
		protected string _FileName = string.Empty;

		public Random()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["随机串生成器"];
			this.TabText = this.Text;

			label1.Text = Apq.GlobalObject.UILang["长度"];
			label2.Text = Apq.GlobalObject.UILang["个数"];
			label3.Text = Apq.GlobalObject.UILang["范围"];

			btnGo.Text = Apq.GlobalObject.UILang["生成"];
			btnCopy.Text = Apq.GlobalObject.UILang["复制"];
		}

		private void Random_Load(object sender, EventArgs e)
		{
			txtChars.Text = "ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy0123456789";

			lstStrings.Items.AddRange(new string[] {
				"ABCDEFGHIJKLMNOPQRSTUVWXYZ",
				"abcdefghijklmnopqrstuvwxyz",
				"0123456789",
				"()[]{}~$",
				"()[]{}<>",
				"`-=\\~!@#$%^&*_+|;\':\",./?",
				"ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy"
			});
			lstStrings.SetItemChecked(2, true);
			lstStrings.SetItemChecked(6, true);

			Apq.Windows.Forms.ListViewHelper.AddBehaivor(listView1);
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			string[] ary = Apq.String.Random(Apq.Convert.ChangeType<uint>(txtCount.Text, 10), Apq.Convert.ChangeType<int>(txtLength.Text, 16), false, null, txtChars.Text);
			listView1.Items.Clear();

			foreach (string str in ary)
			{
				listView1.Items.Add(str);
			}
		}

		private void lstStrings_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.Index >= 0)
			{
				if (e.NewValue == CheckState.Checked)
				{
					if (txtChars.Text.IndexOf(lstStrings.Items[e.Index].ToString()) == -1)
					{
						txtChars.Text += lstStrings.Items[e.Index];
					}
				}
				if (e.NewValue == CheckState.Unchecked)
				{
					if (txtChars.Text.IndexOf(lstStrings.Items[e.Index].ToString()) >= 0)
					{
						txtChars.Text = txtChars.Text.Replace(lstStrings.Items[e.Index].ToString(), string.Empty);
					}
				}
			}
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			IList lst = listView1.SelectedItems.Count > 0 ? (IList)listView1.SelectedItems : (IList)listView1.Items;
			List<string> lstStrs = new List<string>();
			foreach (ListViewItem lvi in lst)
			{
				lstStrs.Add(lvi.Text);
			}
			Clipboard.SetText(string.Join("\r\n", lstStrs.ToArray()));
		}

		private void Random_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void btnGUID_Click(object sender, EventArgs e)
		{
			int nCount = Apq.Convert.ChangeType<int>(txtCount.Text, 10);
			string[] ary = new string[nCount];
			listView1.Items.Clear();

			for (int i = 0; i < nCount; i++)
			{
				string str = System.Guid.NewGuid().ToString();
				listView1.Items.Add(str);
			}
		}
	}
}