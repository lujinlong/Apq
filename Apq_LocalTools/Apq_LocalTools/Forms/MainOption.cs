using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apq_LocalTools.Forms
{
	public partial class MainOption : Apq.Windows.Forms.ImeForm
	{
		public MainOption()
		{
			InitializeComponent();
		}

		//Apq_LocalTools.Controls.MainOption.Favorites fav = new Apq_LocalTools.Controls.MainOption.Favorites();
		//Apq_LocalTools.Controls.MainOption.DBC dbc = new Apq_LocalTools.Controls.MainOption.DBC();

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			//fav.Confirm(GlobalObject.XmlConfigChain);
			//dbc.Confirm(GlobalObject.XmlConfigChain);
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void listView1_MouseClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo hInfo = listView1.HitTest(e.X, e.Y);
			switch (hInfo.Item.Index)
			{
				case 0:
					splitContainer1.Panel2.Controls.Clear();
					//splitContainer1.Panel2.Controls.Add(dbc);
					//dbc.Dock = DockStyle.Fill;
					//dbc.InitValue(GlobalObject.XmlConfigChain);
					break;
				case 1:
					splitContainer1.Panel2.Controls.Clear();
					//splitContainer1.Panel2.Controls.Add(fav);
					//fav.Dock = DockStyle.Fill;
					//fav.InitValue(GlobalObject.XmlConfigChain);
					break;
			}
		}
	}
}
