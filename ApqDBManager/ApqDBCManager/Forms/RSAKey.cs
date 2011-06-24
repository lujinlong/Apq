using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using ICSharpCode.AvalonEdit.Indentation;
using ICSharpCode.AvalonEdit.Folding;
using System.Windows.Threading;

namespace ApqDBCManager.Forms
{
	public partial class RSAKey : Apq.Windows.Forms.DockForm
	{
		private TextEditor txtRSAUKey = new TextEditor();
		private TextEditor txtRSAPKey = new TextEditor();

		public RSAKey()
		{
			InitializeComponent();

			txtRSAUKey.Encoding = System.Text.Encoding.UTF8;
			txtRSAPKey.Encoding = System.Text.Encoding.UTF8;

			txtRSAUKey.IsReadOnly = false;
			txtRSAUKey.Options.ShowSpaces = true;
			txtRSAUKey.Options.ShowTabs = true;
			txtRSAUKey.Options.IndentationSize = 100;
			txtRSAUKey.Options.AllowScrollBelowDocument = true;

			txtRSAPKey.IsReadOnly = false;
			txtRSAPKey.Options.ShowSpaces = true;
			txtRSAPKey.Options.ShowTabs = true;
			txtRSAPKey.Options.IndentationSize = 100;
			txtRSAPKey.Options.AllowScrollBelowDocument = true;

			elementHost1.Child = txtRSAUKey;
			elementHost2.Child = txtRSAPKey;
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["RSA密钥对"];
			this.TabText = this.Text;

			cbContainsPKey.Text = Apq.GlobalObject.UILang["包含私钥"];

			btnCreate.Text = Apq.GlobalObject.UILang["创建"];
			btnSaveUToFile.Text = Apq.GlobalObject.UILang["保存公钥"];
			btnSavePToFile.Text = Apq.GlobalObject.UILang["保存私钥"];

			sfdU.Filter = Apq.GlobalObject.UILang["XML文件(*.xml)|*.xml|所有文件(*.*)|*.*"];
			sfdP.Filter = Apq.GlobalObject.UILang["XML文件(*.xml)|*.xml|所有文件(*.*)|*.*"];
		}

		private void RSAKey_Load(object sender, EventArgs e)
		{
			// Load our custom highlighting definition
			IHighlightingDefinition customHighlighting;
			using (XmlReader reader = new XmlTextReader(Application.StartupPath + "\\XML-Mode.xshd"))
			{
				customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
					HighlightingLoader.Load(reader, HighlightingManager.Instance);
			}
			// and register it in the HighlightingManager
			HighlightingManager.Instance.RegisterHighlighting("Custom Highlighting", new string[] { ".xml" }, customHighlighting);

			txtRSAUKey.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".XML");
			txtRSAPKey.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".XML");
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			txtRSAUKey.Text = string.Empty;
			txtRSAPKey.Text = string.Empty;

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			txtRSAUKey.Text = rsa.ToXmlString(false);
			if (cbContainsPKey.Checked)
			{
				txtRSAPKey.Text = rsa.ToXmlString(true);
			}
		}

		private void btnSaveUToFile_Click(object sender, EventArgs e)
		{
			sfdU.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfdU_InitialDirectory"];
			if (sfdU.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfdU_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfdU.FileName);

				txtRSAUKey.Save(sfdU.FileName);
			}
		}

		private void btnSavePToFile_Click(object sender, EventArgs e)
		{
			sfdP.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfdP_InitialDirectory"];
			if (sfdP.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfdP_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfdP.FileName);

				txtRSAPKey.Save(sfdP.FileName);
			}
		}
	}
}
