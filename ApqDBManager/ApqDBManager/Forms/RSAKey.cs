using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.Security.Cryptography;

namespace ApqDBManager.Forms
{
	public partial class RSAKey : Apq.Windows.Forms.DockForm
	{
		public RSAKey()
		{
			InitializeComponent();

			txtRSAUKey.Encoding = System.Text.Encoding.Default;
		}

		private void RSAKey_Load(object sender, EventArgs e)
		{
			txtRSAUKey.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			txtRSAUKey.ShowEOLMarkers = false;
			txtRSAUKey.ShowSpaces = false;
			txtRSAUKey.ShowTabs = false;
			txtRSAUKey.ShowVRuler = false;

			txtRSAPKey.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			txtRSAPKey.ShowEOLMarkers = false;
			txtRSAPKey.ShowSpaces = false;
			txtRSAPKey.ShowTabs = false;
			txtRSAPKey.ShowVRuler = false;
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			txtRSAUKey.Text = string.Empty;
			txtRSAPKey.Text = string.Empty;
			txtRSAPKey.Refresh();

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			txtRSAUKey.Text = rsa.ToXmlString(false);
			if (ceContainsPKey.Checked)
			{
				txtRSAPKey.Text = rsa.ToXmlString(true);
			}
		}

		private void btnSaveUToFile_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.Filter = "XML文件(*.xml)|*.xml|所有文件(*.*)|*.*";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtRSAUKey.SaveFile(saveFileDialog.FileName);
			}
		}

		private void btnSavePToFile_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.Filter = "XML文件(*.xml)|*.xml|所有文件(*.*)|*.*";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtRSAPKey.SaveFile(saveFileDialog.FileName);
			}
		}
	}
}
