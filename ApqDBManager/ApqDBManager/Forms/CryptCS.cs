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
	public partial class CryptCS : Apq.Windows.Forms.DockForm
	{
		public CryptCS()
		{
			InitializeComponent();
		}

		private void beInput_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (beInput.Text.Trim().Length > 0)
			{
				openFileDialog1.FileName = beInput.Text.Trim();
			}
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				beInput.Text = openFileDialog1.FileName;
			}
		}

		private void beOutput_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (beOutput.Text.Trim().Length > 0)
			{
				saveFileDialog1.FileName = beInput.Text.Trim();
			}
			if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				beOutput.Text = saveFileDialog1.FileName;
			}
		}

		private void btnEncryptFile_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(teKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(teIV.Text);
			Apq.Security.Cryptography.DESHelper.EncryptFile(beInput.Text, beOutput.Text, desKey, desIV);
		}

		private void btnDecryptFile_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(teKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(teIV.Text);
			Apq.Security.Cryptography.DESHelper.DecryptFile(beInput.Text, beOutput.Text, desKey, desIV);
		}

		private void btnEncryptString_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(teKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(teIV.Text);
			meOutput.Text = Apq.Security.Cryptography.DESHelper.EncryptString(meInput.Text, desKey, desIV);
		}

		private void btnDecryptString_Click(object sender, EventArgs e)
		{
			byte[] desKey = System.Text.Encoding.Unicode.GetBytes(teKey.Text);
			byte[] desIV = System.Text.Encoding.Unicode.GetBytes(teIV.Text);
			meOutput.Text = Apq.Security.Cryptography.DESHelper.DecryptString(meInput.Text, desKey, desIV);
		}
	}
}