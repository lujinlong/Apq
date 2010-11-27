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

		private void CryptCS_Load(object sender, EventArgs e)
		{
			string EncryptFile = GlobalObject.XmlConfigChain[this.GetType(), "EncryptFile"];
			if (EncryptFile != null)
			{
				beInput.Text = EncryptFile;
			}
			string DecryptFile = GlobalObject.XmlConfigChain[this.GetType(), "DecryptFile"];
			if (DecryptFile != null)
			{
				beOutput.Text = DecryptFile;
			}
			string CDString = GlobalObject.XmlConfigChain[this.GetType(), "CDString"];
			if (CDString != null)
			{
				meInput.Text = CDString;
			}
		}

		private void beInput_EditValueChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlUserConfig.SetValue("EncryptFile", beInput.Text);
		}

		private void beOutput_EditValueChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlUserConfig.SetValue("DecryptFile", beOutput.Text);
		}

		private void meInput_EditValueChanged(object sender, EventArgs e)
		{
			GlobalObject.XmlUserConfig.SetValue("CDString", meInput.Text);
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
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			string str = File.ReadAllText(beInput.Text);
			string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(str, desKey, desIV);
			File.WriteAllText(beOutput.Text, strCs, Encoding.UTF8);
			MessageBox.Show("加密完成");
		}

		private void btnDecryptFile_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			string strCs = File.ReadAllText(beInput.Text);
			string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
			File.WriteAllText(beOutput.Text, str, Encoding.UTF8);
			MessageBox.Show("解密完成");
		}

		private void btnEncryptString_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			meOutput.Text = Apq.Security.Cryptography.DESHelper.EncryptString(meInput.Text, desKey, desIV);
			MessageBox.Show("加密完成");
		}

		private void btnDecryptString_Click(object sender, EventArgs e)
		{
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			meOutput.Text = Apq.Security.Cryptography.DESHelper.DecryptString(meInput.Text, desKey, desIV);
			MessageBox.Show("解密完成");
		}
	}
}