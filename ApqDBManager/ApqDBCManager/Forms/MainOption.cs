using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqDBCManager.Forms
{
	public partial class MainOption : Apq.Windows.Forms.ImeForm
	{
		public MainOption()
		{
			InitializeComponent();
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(txtDesKey.Text))
			{
				GlobalObject.XmlConfigChain[this.GetType(), "DesKey"] = txtDesKey.Text.Trim();
			}
			GlobalObject.XmlConfigChain.Save();
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnShowPwd_Click(object sender, EventArgs e)
		{
			txtDesKey.PasswordChar = new char();
		}

		private void MainOption_Load(object sender, EventArgs e)
		{
			if (!Apq.Convert.HasMean(txtDesKey.Text))
			{
				txtDesKey.Text = GlobalObject.XmlConfigChain[this.GetType(), "DesKey"];
			}
		}
	}
}
