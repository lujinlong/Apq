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
	public partial class MainOption : Apq.Windows.Forms.ImeForm
	{
		public MainOption()
		{
			InitializeComponent();
		}

		ApqDBManager.Controls.MainOption.XmlServers xs = new ApqDBManager.Controls.MainOption.XmlServers();
		ApqDBManager.Controls.MainOption.Favorites fav = new ApqDBManager.Controls.MainOption.Favorites();
		ApqDBManager.Controls.MainOption.DBC dbc = new ApqDBManager.Controls.MainOption.DBC();

		private void MainOption_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			xs.Confirm(GlobalObject.XmlConfigChain);
			fav.Confirm(GlobalObject.XmlConfigChain);
			dbc.Confirm(GlobalObject.XmlConfigChain);
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void nbiXmlServers_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			panelControl1.Controls.Clear();
			panelControl1.Controls.Add(xs);
			xs.Dock = DockStyle.Fill;
			xs.InitValue(GlobalObject.XmlConfigChain);
		}

		private void nbiFavorites_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			panelControl1.Controls.Clear();
			panelControl1.Controls.Add(fav);
			fav.Dock = DockStyle.Fill;
			fav.InitValue(GlobalObject.XmlConfigChain);
		}

		public void nbiDBC_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			panelControl1.Controls.Clear();
			panelControl1.Controls.Add(dbc);
			dbc.Dock = DockStyle.Fill;
			dbc.InitValue(GlobalObject.XmlConfigChain);
		}
	}
}
