using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Editor
{
	/// <summary>
	/// IEditor
	/// </summary>
	public interface ITextEditor:IFileLoader
	{
		/// <summary>
		/// 撤消
		/// </summary>
		void Undo();
		/// <summary>
		/// 重做
		/// </summary>
		void Redo();
		/// <summary>
		/// 复制
		/// </summary>
		void Copy();
		/// <summary>
		/// 粘贴
		/// </summary>
		void Paste();
		/// <summary>
		/// 删除
		/// </summary>
		void Delete();

		/// <summary>
		/// 全选
		/// </summary>
		void SelectAll();
		/// <summary>
		/// 反选
		/// </summary>
		void Reverse();
	}
}
