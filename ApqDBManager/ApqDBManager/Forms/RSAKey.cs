using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace ApqDBManager.Forms
{
	public partial class RSAKey : Apq.Windows.Forms.DockForm
	{
		public RSAKey()
		{
			InitializeComponent();

			txtRSAKey.Encoding = System.Text.Encoding.Default;
		}

		private void RSAKey_Load(object sender, EventArgs e)
		{
			txtRSAKey.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			txtRSAKey.ShowEOLMarkers = false;
			txtRSAKey.ShowSpaces = false;
			txtRSAKey.ShowTabs = false;
			txtRSAKey.ShowVRuler = false;
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			txtRSAKey.Text = Apq.Security.Cryptography.RSAHelper.CreateKey(ceContainsPKey.Checked);
		}

		private void btnSaveToFile_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.Filter = "XML文件(*.xml)|*.xml|所有文件(*.*)|*.*";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtRSAKey.SaveFile(saveFileDialog.FileName);
			}
		}
	}
}
