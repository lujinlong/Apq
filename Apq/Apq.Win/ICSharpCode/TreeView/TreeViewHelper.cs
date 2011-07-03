using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.TreeView;
using System.IO;

namespace Apq.ICSharpCode.TreeView
{
	public class TreeViewHelper
	{
		/// <summary>
		/// _TreeView
		/// </summary>
		protected SharpTreeView _TreeView;
		/// <summary>
		/// 获取TreeView
		/// </summary>
		public SharpTreeView TreeView
		{
			get { return _TreeView; }
		}

		/// <summary>
		/// TreeView助手
		/// </summary>
		/// <param name="TreeView"></param>
		public TreeViewHelper(SharpTreeView TreeView)
		{
			_TreeView = TreeView;
		}

		#region FindNodeByFullPath
		/// <summary>
		/// 查找指定全路径的节点
		/// </summary>
		public FileSystemNode FindNodeByFullPath(string FullPath)
		{
			// 查找到对应结点
			string[] lstPaths = FullPath.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
			bool IsFound = false;// 是否找到最终结点
			FileSystemNode ndFound = null;// 查找过程已搜索到的结点
			foreach (FileSystemNode node in TreeView.Items)
			{
				if (lstPaths[0].Equals(node.Text.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					ndFound = node;
					if (lstPaths.Length == 1)
					{
						IsFound = true;
					}
				}
			}

			if (ndFound != null && lstPaths.Length > 1)
			{
				int countPath = lstPaths.Length;
				for (int i = 1; i < countPath; i++)
				{
					FileSystemNode ndChild = null;// 查找过程中搜索到的下级结点
					foreach (FileSystemNode node in ndFound.Children)
					{
						if (node.Text.ToString().Equals(lstPaths[i], StringComparison.OrdinalIgnoreCase))
						{
							ndChild = node;

							if (i == countPath - 1)// 最后一级
							{
								IsFound = true;
							}
						}
					}

					if (ndChild == null)
					{
						break;
					}
					ndFound = ndChild;
				}
			}

			if (IsFound && ndFound != null)
			{
				return ndFound;
			}
			return null;
		}
		#endregion
	}
}
