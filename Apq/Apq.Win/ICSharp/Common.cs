using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;

namespace Apq.ICSharp
{
	/// <summary>
	/// ICSharp公用功能
	/// </summary>
	public class Common
	{

		#region Behaivor

		/// <summary>
		/// 常用扩展,添加常用功能
		/// </summary>
		[Obsolete("不能生效,暂未启用")]
		public static void AddBehaivor(TextEditorControlBase tecb)
		{
			tecb.KeyDown += new System.Windows.Forms.KeyEventHandler(tecb_KeyDown);
		}

		/// <summary>
		/// 常用扩展,去除常用功能
		/// </summary>
		public static void RemoveBehaivor(TextEditorControlBase tecb)
		{
			tecb.KeyDown -= new System.Windows.Forms.KeyEventHandler(tecb_KeyDown);
		}

		#endregion

		#region 复制粘贴快捷键
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void tecb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			TextEditorControlBase tecb = sender as TextEditorControlBase;
			if (tecb == null)
			{
				return;
			}

			if (e.Control && (e.KeyCode == Keys.C))
			{
				System.Windows.Forms.Clipboard.SetData(DataFormats.UnicodeText,
					string.IsNullOrEmpty(tecb.ActiveTextAreaControl.SelectionManager.SelectedText) ? tecb.Text
					: tecb.ActiveTextAreaControl.SelectionManager.SelectedText);
			}
			if (e.Control && (e.KeyCode == Keys.V))
			{
				string str = System.Windows.Forms.Clipboard.GetData(DataFormats.UnicodeText) as string;
				if (str != null) tecb.ActiveTextAreaControl.TextArea.InsertString(str);
			}
		}
		#endregion
	}
}
