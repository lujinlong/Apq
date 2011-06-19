using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Security.Cryptography;
using System.IO;

namespace ApqDBCManager.Forms
{
	public partial class CryptCS : Apq.Windows.Forms.DockForm
	{
		public CryptCS()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["DB连接加解密"];
			this.TabText = this.Text;

			tabPage1.Text = Apq.GlobalObject.UILang["文件"];
			label3.Text = Apq.GlobalObject.UILang["原始文件"];
			label4.Text = Apq.GlobalObject.UILang["加密文件"];
			btnEncryptFile.Text = Apq.GlobalObject.UILang["加密↓"];
			btnDecryptFile.Text = Apq.GlobalObject.UILang["解密↑"];

			tabPage2.Text = Apq.GlobalObject.UILang["字符串"];
			btnEncryptString.Text = Apq.GlobalObject.UILang["加密↓"];
			btnDecryptString.Text = Apq.GlobalObject.UILang["解密↑"];

			ofdDFile.Filter = Apq.GlobalObject.UILang["XML文件(*.xml)|*.xml|所有文件(*.*)|*.*"];
			ofdEFile.Filter = Apq.GlobalObject.UILang["DBC文件(*.res)|*.res|所有文件(*.*)|*.*"];
		}

		private void cbDFile_DropDown(object sender, EventArgs e)
		{
			if (cbDFile.Text.Trim().Length > 0)
			{
				ofdDFile.FileName = cbDFile.Text.Trim();
			}
			ofdDFile.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "ofdDFile_InitialDirectory"];
			if (ofdDFile.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "ofdDFile_InitialDirectory"] = System.IO.Path.GetDirectoryName(ofdDFile.FileName);

				cbDFile.Text = ofdDFile.FileName;
			}
		}

		private void cbEFile_DropDown(object sender, EventArgs e)
		{
			if (cbEFile.Text.Trim().Length > 0)
			{
				ofdEFile.FileName = cbEFile.Text.Trim();
			}
			ofdEFile.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "ofdEFile_InitialDirectory"];
			if (ofdEFile.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "ofdEFile_InitialDirectory"] = System.IO.Path.GetDirectoryName(ofdEFile.FileName);

				cbEFile.Text = ofdEFile.FileName;
			}
		}

		private void btnEncryptFile_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.XmlConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.XmlConfigChain["Crypt", "DESIV"];
			string str = File.ReadAllText(cbDFile.Text);
			string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(str, desKey, desIV);
			File.WriteAllText(cbEFile.Text, strCs, Encoding.UTF8);
			MessageBox.Show(Apq.GlobalObject.UILang["加密完成"]);
		}

		private void btnDecryptFile_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.XmlConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.XmlConfigChain["Crypt", "DESIV"];
			string strCs = File.ReadAllText(cbEFile.Text);
			string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
			File.WriteAllText(cbDFile.Text, str, Encoding.UTF8);
			MessageBox.Show(Apq.GlobalObject.UILang["解密完成"]);
		}

		private void btnEncryptString_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.XmlConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.XmlConfigChain["Crypt", "DESIV"];
			txtOutput.Text = Apq.Security.Cryptography.DESHelper.EncryptString(txtInput.Text, desKey, desIV);
			MessageBox.Show(Apq.GlobalObject.UILang["加密完成"]);
		}

		private void btnDecryptString_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.XmlConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.XmlConfigChain["Crypt", "DESIV"];
			txtInput.Text = Apq.Security.Cryptography.DESHelper.DecryptString(txtOutput.Text, desKey, desIV);
			MessageBox.Show(Apq.GlobalObject.UILang["解密完成"]);
		}
	}
}