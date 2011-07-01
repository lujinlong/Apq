using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;
using Apq_LocalTools.Forms;
using Apq.TreeListView;
using System.IO;

namespace Apq_LocalTools
{
	public partial class TxtEncoding : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public TxtEncoding()
		{
			InitializeComponent();
		}

		private TreeListViewHelper tlvHelper;

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["文本文件编码转换"] + " - " + ++FormCount;
			TabText = Text;

			groupBox1.Text = Apq.GlobalObject.UILang["读取参数"];
			label3.Text = Apq.GlobalObject.UILang["原始编码："];
			label2.Text = Apq.GlobalObject.UILang["默认编码："];
			label5.Text = Apq.GlobalObject.UILang["文件类型："];
			label4.Text = Apq.GlobalObject.UILang["自动检测无法确定编码时使用默认编码读取原始文件"];
			cbContainsChildren.Text = Apq.GlobalObject.UILang["包含子目录"];

			groupBox2.Text = Apq.GlobalObject.UILang["转换参数"];
			label6.Text = Apq.GlobalObject.UILang["目标编码："];
			label1.Text = Apq.GlobalObject.UILang["重命名："];
			rbKeep.Text = Apq.GlobalObject.UILang["原名"];
			rbEncodeName.Text = Apq.GlobalObject.UILang["原名_编码"];
			rbCustomer.Text = Apq.GlobalObject.UILang["原名_自定义"];

			btnTrans.Text = Apq.GlobalObject.UILang["开始转换(&T)"];

			columnHeader1.Text = Apq.GlobalObject.UILang["名称"];
			columnHeader2.Text = Apq.GlobalObject.UILang["大小(B)"];
			columnHeader3.Text = Apq.GlobalObject.UILang["类型"];
			columnHeader5.Text = Apq.GlobalObject.UILang["创建日期"];
			columnHeader4.Text = Apq.GlobalObject.UILang["修改日期"];
		}

		private void TxtEncoding_Load(object sender, EventArgs e)
		{
			cbSrcEncoding.SelectedIndex = 0;
			cbDefaultEncoding.SelectedIndex = 1;
			cbDstEncoding.SelectedIndex = 0;

			TransFinished += new EventHandler(TxtEncoding_TransFinished);
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			tlvHelper = new TreeListViewHelper(treeListView1);

			#region 数据库连接
			#endregion
		}
		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		public override void InitData(DataSet ds)
		{
			#region 准备数据集结构
			#endregion

			#region 加载所有字典表
			#endregion
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
			try
			{
				// 为TreeListView添加根结点
				treeListView1.Items.Clear();

				DriveInfo[] fsDrives = DriveInfo.GetDrives();

				foreach (DriveInfo fsDrive in fsDrives)
				{
					string strExt = fsDrive.Name;
					Icon SmallIcon = null;
					Apq.DllImports.Shell32.SHFILEINFO shFileInfo = Apq.DllImports.Shell32.GetFileInfo(strExt, false, ref SmallIcon);

					if (!imgList.Images.ContainsKey(fsDrive.DriveType.ToString()))
					{
						imgList.Images.Add(fsDrive.DriveType.ToString(), SmallIcon);
					}

					TreeListViewItem ndRoot = new TreeListViewItem(fsDrive.Name.Substring(0, 2));
					treeListView1.Items.Add(ndRoot);
					ndRoot.ImageIndex = imgList.Images.IndexOfKey(fsDrive.DriveType.ToString());
					if (fsDrive.IsReady)
					{
						ndRoot.SubItems.Add(fsDrive.TotalSize.ToString("n0"));
					}
					else
					{
						ndRoot.SubItems.Add("0");
					}
					ndRoot.SubItems.Add(shFileInfo.szTypeName);
					ndRoot.SubItems.Add(fsDrive.RootDirectory.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
					ndRoot.SubItems.Add(fsDrive.RootDirectory.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));

					ndRoot.SubItems.Add(fsDrive.Name);
					ndRoot.SubItems.Add(fsDrive.IsReady ? "0" : "-1");
				}
			}
			catch { }
		}
		#endregion

		#region treeListView1
		private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = treeListView1.FocusedItem;
			if (node != null)
			{
				tsslStatus.Text = node.FullPath;

				if (node.SubItems[treeListView1.Columns.Count + 1].Text == "0")
				{
					LoadFolders(node, node.FullPath);
					LoadFiles(node, node.FullPath);

					node.SubItems[treeListView1.Columns.Count + 1].Text = node.Items.Count > 0 ? "1" : "-1";
				}
			}
		}

		private void LoadFolders(TreeListViewItem node, string fsFullPath)
		{
			try
			{
				if (!imgList.Images.ContainsKey("文件夹收起"))
				{
					Icon SmallIcon = Apq.DllImports.Shell32.GetFolderIcon(false, false);
					imgList.Images.Add("文件夹收起", SmallIcon);
				}
				if (!imgList.Images.ContainsKey("文件夹展开"))
				{
					Icon SmallIcon = Apq.DllImports.Shell32.GetFolderIcon(false, true);
					imgList.Images.Add("文件夹展开", SmallIcon);
				}

				string[] strChildren = Directory.GetDirectories(fsFullPath + "\\");
				foreach (string strChild in strChildren)
				{
					DirectoryInfo diChild = new DirectoryInfo(strChild);
					TreeListViewItem ndChild = new TreeListViewItem(diChild.Name);
					node.Items.Add(ndChild);
					ndChild.ImageKey = "文件夹收起";
					ndChild.SubItems.Add("0");
					ndChild.SubItems.Add(Apq.GlobalObject.UILang["文件夹"]);
					ndChild.SubItems.Add(diChild.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
					ndChild.SubItems.Add(diChild.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));

					ndChild.SubItems.Add(diChild.FullName);
					ndChild.SubItems.Add("0");
				}
			}
			catch { }
		}

		private void LoadFiles(TreeListViewItem node, string fsFullPath)
		{
			try
			{
				string[] strChildren = Directory.GetFiles(fsFullPath + "\\");
				foreach (string strChild in strChildren)
				{
					FileInfo diChild = new FileInfo(strChild);
					//string strExt = fsDrive.DriveType.ToString();
					string strExt = diChild.Extension.ToLower();
					if (strExt == ".exe")
					{
						strExt = diChild.FullName.ToLower();
					}

					Icon SmallIcon = null;
					Apq.DllImports.Shell32.SHFILEINFO shFileInfo = Apq.DllImports.Shell32.GetFileInfo(strExt, false, ref SmallIcon);

					if (!imgList.Images.ContainsKey(strExt))
					{
						imgList.Images.Add(strExt, SmallIcon);
					}

					TreeListViewItem ndChild = new TreeListViewItem(diChild.Name);
					node.Items.Add(ndChild);
					ndChild.ImageIndex = imgList.Images.IndexOfKey(strExt);
					ndChild.SubItems.Add(diChild.Length.ToString());
					if (strExt.Contains("\\"))
					{
						strExt = "应用程序";
					}
					//ndChild.SubItems.Add(Apq.GlobalObject.UILang[strExt]);
					ndChild.SubItems.Add(shFileInfo.szTypeName);
					ndChild.SubItems.Add(diChild.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
					ndChild.SubItems.Add(diChild.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss"));

					ndChild.SubItems.Add(diChild.Name);
					ndChild.SubItems.Add("-1");
				}
			}
			catch { }
		}

		// 展开文件夹图标
		private void treeListView1_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.Level > 0)
			{
				if (e.Item.ImageKey == "文件夹收起")
				{
					e.Item.ImageKey = "文件夹展开";
				}
			}
		}

		// 收起文件夹图标
		private void treeListView1_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.Level > 0)
			{
				if (e.Item.ImageKey == "文件夹展开")
				{
					e.Item.ImageKey = "文件夹收起";
				}
			}
		}
		#endregion

		public void UIEnable(bool Enable)
		{
			treeListView1.Enabled = Enable;
			cbSrcEncoding.Enabled = Enable;
			cbDefaultEncoding.Enabled = Enable;
			txtExt.Enabled = Enable;
			cbContainsChildren.Enabled = Enable;

			cbDstEncoding.Enabled = Enable;
			rbKeep.Enabled = Enable;
			rbEncodeName.Enabled = Enable;
			rbCustomer.Enabled = Enable;
			txtCustomer.Enabled = Enable;

			Cursor = Enable ? Cursors.Default : Cursors.WaitCursor;
		}

		void TxtEncoding_TransFinished(object sender, EventArgs e)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始转换(&T)"];
			});
		}

		public event EventHandler TransFinished;
		/// <summary>
		/// 设置进度条的当前值，完成后引发“TransFinished”事件
		/// </summary>
		/// <param name="Value"></param>
		public void tspb_SetValue(int Value)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				tspb.Value = Value;

				if (tspb.Maximum > 0 && tspb.Value >= tspb.Maximum)
				{
					TransFinished.Invoke(tspb, new EventArgs());
				}
			});
		}

		private void TxtEncoding_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void TxtEncoding_Activated(object sender, EventArgs e)
		{
		}

		private void TxtEncoding_Deactivate(object sender, EventArgs e)
		{

		}

		private void btnTrans_Click(object sender, EventArgs e)
		{
			Apq.Threading.Thread.Abort(MainBackThread);

			if (btnTrans.Text == Apq.GlobalObject.UILang["开始转换(&T)"])
			{
				UIEnable(false);
				btnTrans.Text = Apq.GlobalObject.UILang["取消(&C)"];

				MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(Work));
			}
			else
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始转换(&T)"];
			}
		}

		private void Work()
		{
			tspb_SetValue(0);
		}
	}
}