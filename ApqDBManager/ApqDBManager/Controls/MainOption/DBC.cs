using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqDBManager.Controls.MainOption
{
	public partial class DBC : UserControl
	{
		public DBC()
		{
			InitializeComponent();
		}

		public void InitValue(Apq.Config.ConfigChain cfg)
		{
			if (!Apq.Convert.HasMean(txtDBName.Text))
			{
				txtDBName.Text = cfg[this.GetType(), "DBName"];
				txtServerName.Text = cfg[this.GetType(), "ServerName"];
				txtUserId.Text = cfg[this.GetType(), "UserId"];
				string PwdC = cfg[this.GetType(), "Pwd"];
				if (!string.IsNullOrEmpty(PwdC))
				{
					string PwdD = Apq.Security.Cryptography.DESHelper.DecryptString(PwdC, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
					txtPwd.Text = PwdD;
				}
			}
		}

		public void Confirm(Apq.Config.ConfigChain cfg)
		{
			if (Apq.Convert.HasMean(txtDBName.Text))
			{
				cfg[this.GetType(), "DBName"] = txtDBName.Text.Trim();
				cfg[this.GetType(), "ServerName"] = txtDBName.Text.Trim();
				cfg[this.GetType(), "UserId"] = txtDBName.Text.Trim();
				string PwdD = txtPwd.Text;
				if (!string.IsNullOrEmpty(PwdD))
				{
					string PwdC = Apq.Security.Cryptography.DESHelper.EncryptString(PwdD, GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
					cfg[this.GetType(), "Pwd"] = PwdC;
				}
			}
			cfg.UserConfig.Save();
		}
	}
}
