using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ApqDBManager
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			#region 读取标题
			string cfgText = GlobalObject.XmlConfigChain[this.GetType(), "Text"];
			if (cfgText != null)
			{
				Text = cfgText;
			}
			#endregion

			#region 读取RsaKey
			txtRsaKey.Text = Apq.Reg.Server.Common.GetKey();
			#endregion
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			txtSN.Text = Apq.Reg.Server.Common.SignString(txtUserName.Text, txtRsaKey.Text);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			txtSN.SelectAll();
			txtSN.Copy();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}