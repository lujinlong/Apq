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

namespace ApqDBManager.Forms
{
	public partial class CryptCS : Apq.Windows.Forms.DockForm
	{
		public CryptCS()
		{
			InitializeComponent();
		}

		private void cbDFile_DropDown(object sender, EventArgs e)
		{
			if (cbDFile.Text.Trim().Length > 0)
			{
				ofdDFile.FileName = cbDFile.Text.Trim();
			}
			if (ofdDFile.ShowDialog(this) == DialogResult.OK)
			{
				cbDFile.Text = ofdDFile.FileName;
			}
		}

		private void cbEFile_DropDown(object sender, EventArgs e)
		{
			if (cbEFile.Text.Trim().Length > 0)
			{
				ofdEFile.FileName = cbEFile.Text.Trim();
			}
			if (ofdEFile.ShowDialog(this) == DialogResult.OK)
			{
				cbEFile.Text = ofdEFile.FileName;
			}
		}

		private void btnEncryptFile_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			string str = File.ReadAllText(cbDFile.Text);
			string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(str, desKey, desIV);
			File.WriteAllText(cbEFile.Text, strCs, Encoding.UTF8);
			MessageBox.Show("加密完成");
		}

		private void btnDecryptFile_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			string strCs = File.ReadAllText(cbEFile.Text);
			string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
			File.WriteAllText(cbDFile.Text, str, Encoding.UTF8);
			MessageBox.Show("解密完成");
		}

		private void btnEncryptString_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			txtOutput.Text = Apq.Security.Cryptography.DESHelper.EncryptString(txtInput.Text, desKey, desIV);
			MessageBox.Show("加密完成");
		}

		private void btnDecryptString_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			txtInput.Text = Apq.Security.Cryptography.DESHelper.DecryptString(txtOutput.Text, desKey, desIV);
			MessageBox.Show("解密完成");
		}
	}
}