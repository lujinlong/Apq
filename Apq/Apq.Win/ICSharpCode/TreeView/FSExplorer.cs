using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.TreeView;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Apq.ICSharpCode.TreeView
{
	/// <summary>
	/// 基于SharpTreeView的资源浏览器
	/// </summary>
	public class FSExplorer : SharpTreeView
	{
		private TreeViewHelper tvHelper;

		/// <summary>
		/// 基于SharpTreeView的资源浏览器
		/// 隐藏列依次为:
		///		完整路径
		///		HasChildren{-1:无,0:未知,1:有}
		///		类型{1:Drive,2:Folder,3:File}
		/// </summary>
		public FSExplorer()
		{
			tvHelper = new TreeViewHelper(this);
		}

		#region 显示文件系统
		public void LoadDrives()
		{
			Apq.Windows.Delegates.Action_UI<FSExplorer>(this, this, delegate(FSExplorer ctrl)
			{
				// 为TreeView添加根结点
				Items.Clear();

				DriveInfo[] fsDrives = DriveInfo.GetDrives();

				foreach (DriveInfo fsDrive in fsDrives)
				{
					string strExt = fsDrive.Name;

					FolderNode ndRoot = new FolderNode(fsDrive.Name.Substring(0, 2));
					Items.Add(ndRoot);
				}
			});
		}
		#endregion
	}
}
