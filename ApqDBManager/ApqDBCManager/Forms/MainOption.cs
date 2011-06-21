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

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["选项"];

			label1.Text = Apq.GlobalObject.UILang["加解密密码(至少9个字符)"];
			btnShowPwd.Text = Apq.GlobalObject.UILang["显示密码"];
			btnConfirm.Text = Apq.GlobalObject.UILang["确定"];
			btnCancel.Text = Apq.GlobalObject.UILang["取消"];
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			if (txtDesKey.Text.Length > 8)
			{
				string strDESKey = txtDesKey.Text.Substring(0, 8);
				string strDESIV = txtDesKey.Text.Substring(8);
				if (Apq.Convert.HasMean(txtDesKey.Text))
				{
					GlobalObject.XmlConfigChain["Crypt", "DESKey"] = strDESKey;
					GlobalObject.XmlConfigChain["Crypt", "DESIV"] = strDESIV;
				}
				GlobalObject.XmlConfigChain.Save();
				this.Close();
			}
			else
			{
				MessageBox.Show(Apq.GlobalObject.UILang["密码太短"]);
			}
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
				string strDESKey = GlobalObject.XmlConfigChain["Crypt", "DESKey"];
				string strDESIV = GlobalObject.XmlConfigChain["Crypt", "DESIV"];

				txtDesKey.Text = strDESKey + strDESIV;
			}
		}
	}
}
