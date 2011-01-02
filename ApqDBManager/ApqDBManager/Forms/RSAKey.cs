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
	public partial class RSAKey : Apq.Windows.Forms.DockForm, Apq.Editor.ITextEditor
	{
		public RSAKey()
		{
			InitializeComponent();

			txtRSAUKey.Encoding = System.Text.Encoding.Default;
			txtRSAPKey.Encoding = System.Text.Encoding.Default;
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

		#region ITextEditor 成员

		protected string _FileName = string.Empty;
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				Text = value;
				TabText = value;
			}
		}

		public void Save()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				if (FileName.Length < 1)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.Filter = "XML文件(*.xml)|*.xml";
					if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
					{
						FileName = saveFileDialog.FileName;
					}
					else
					{
						return;
					}
				}

				te.SaveFile(FileName);
			}
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			Save();
		}

		public void Copy()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				System.Windows.Forms.Clipboard.SetData(DataFormats.UnicodeText,
					string.IsNullOrEmpty(te.ActiveTextAreaControl.SelectionManager.SelectedText) ? te.Text
					: te.ActiveTextAreaControl.SelectionManager.SelectedText);
			}
		}

		public void Delete()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				te.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
			}
		}

		public void Open()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.RestoreDirectory = true;
				openFileDialog.Filter = "文本文件(*.txt;*.sql)|*.txt;*.sql|所有文件(*.*)|*.*";
				if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					FileName = openFileDialog.FileName;
					te.LoadFile(FileName);
				}
			}
		}

		public void Paste()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				string str = System.Windows.Forms.Clipboard.GetData(DataFormats.UnicodeText) as string;
				if (str != null) te.ActiveTextAreaControl.TextArea.InsertString(str);
			}
		}

		public void Redo()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				te.Redo();
			}
		}

		public void Reverse()
		{
		}

		public void SelectAll()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				Point startPoint = new Point(0, 0);
				Point endPoint = te.Document.OffsetToPosition(te.Document.TextLength);
				if (te.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
				{
					if (te.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].StartPosition == startPoint &&
						te.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].EndPosition == endPoint)
					{
						return;
					}
				}
				te.ActiveTextAreaControl.SelectionManager.SetSelection(new DefaultSelection(te.Document, startPoint, endPoint));
			}
		}

		public void Undo()
		{
			ICSharpCode.TextEditor.TextEditorControl te = ActiveControl as ICSharpCode.TextEditor.TextEditorControl;
			if (te != null)
			{
				te.Undo();
			}
		}

		#endregion
	}
}
