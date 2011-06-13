using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Security.Cryptography;

namespace ApqDBManager
{
	public partial class DES : Apq.Windows.Forms.DockForm
	{
		public DES()
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
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(txtKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(txtIV.Text);
			Apq.Security.Cryptography.DESHelper.EncryptFile(cbDFile.Text, cbEFile.Text, desKey, desIV);
		}

		private void btnDecryptFile_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(txtKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(txtIV.Text);
			Apq.Security.Cryptography.DESHelper.DecryptFile(cbEFile.Text, cbDFile.Text, desKey, desIV);
		}

		private void btnEncryptString_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(txtKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(txtIV.Text);
			txtOutput.Text = Apq.Security.Cryptography.DESHelper.EncryptString(txtInput.Text, desKey, desIV);
		}

		private void btnDecryptString_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(txtKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(txtIV.Text);
			txtInput.Text = Apq.Security.Cryptography.DESHelper.DecryptString(txtOutput.Text, desKey, desIV);
		}
	}
}