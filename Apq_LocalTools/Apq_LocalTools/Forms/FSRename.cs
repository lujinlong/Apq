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
	public partial class FSRename : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public FSRename()
		{
			InitializeComponent();
		}

		private TreeListViewHelper tlvHelper;
		private Apq.Windows.Forms.TSProgressBarHelper pbHelper;

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["批量重命名"] + " - " + ++FormCount;
			TabText = Text;

			groupBox1.Text = Apq.GlobalObject.UILang["查找和替换"];
			label3.Text = Apq.GlobalObject.UILang["查找串："];
			label6.Text = Apq.GlobalObject.UILang["替换为："];
			label2.Text = Apq.GlobalObject.UILang["匹配方式："];
			label5.Text = Apq.GlobalObject.UILang["文件类型："];
			label4.Text = Apq.GlobalObject.UILang["两种方式均为匹配整个查找串"];

			cbContainsFolder.Text = Apq.GlobalObject.UILang["包含文件夹"];
			cbRecursive.Text = Apq.GlobalObject.UILang["包含子目录"];
			cbContainsFileExt.Text = Apq.GlobalObject.UILang["包含文件扩展名"];

			btnFind.Text = Apq.GlobalObject.UILang["查找(&F)"];
			btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];

			cbMatchType.Items[0] = Apq.GlobalObject.UILang["普通"];
			cbMatchType.Items[1] = Apq.GlobalObject.UILang["正则表达式"];
		}

		private void TxtEncoding_Load(object sender, EventArgs e)
		{
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			tlvHelper = new TreeListViewHelper(fsExplorer1);
			pbHelper = new Apq.Windows.Forms.TSProgressBarHelper(tspb);
			pbHelper.Completed += new Action<ToolStripProgressBar>(pbHelper_Completed);

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

			cbMatchType.SelectedIndex = 0;
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
				fsExplorer1.LoadDrives();
			}
			catch { }
		}
		#endregion

		private void fsExplorer1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeListViewItem node = fsExplorer1.FocusedItem;
			if (node != null)
			{
				tsslStatus.Text = node.FullPath;
			}
		}

		public void UIEnable(bool Enable)
		{
			fsExplorer1.Enabled = Enable;
			txtLook.Enabled = Enable;
			txtReplace.Enabled = Enable;
			cbMatchType.Enabled = Enable;
			txtExt.Enabled = Enable;
			cbContainsFolder.Enabled = Enable;
			cbRecursive.Enabled = Enable;
			cbContainsFileExt.Enabled = Enable;

			Cursor = Enable ? Cursors.Default : Cursors.WaitCursor;
		}

		void pbHelper_Completed(ToolStripProgressBar obj)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
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
			if (fsExplorer1.CheckedItems.LongLength <= 0)
			{
				return;
			}

			Apq.Threading.Thread.Abort(MainBackThread);

			if (btnTrans.Text == Apq.GlobalObject.UILang["开始替换(&H)"])
			{
				UIEnable(false);
				btnTrans.Text = Apq.GlobalObject.UILang["取消(&C)"];

				MainBackThread = Apq.Threading.Thread.StartNewThread(new ThreadStart(Work));
			}
			else
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始替换(&H)"];
			}
		}

		#region 工作线程
		private void Work()
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				tsslStatus.Text = Apq.GlobalObject.UILang["开始处理..."];
				pbHelper.SetValue(0);
				tspb.Maximum = 0;

				//将需要转换的文件记录到列表
				Dictionary<string, string> lstFiles = new Dictionary<string, string>();
				for (long i = fsExplorer1.CheckedItems.LongLength - 1; i >= 0; i--)
				{
					TreeListViewItem node = fsExplorer1.CheckedItems[i];
					int Type = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 2].Text);
					if (Type == 3)
					{
						AddFile2FileList(lstFiles, node.FullPath);
					}
					else
					{
						AddFolder2FileList(lstFiles, node.FullPath, cbRecursive.Checked);
					}
				}

				tspb.Maximum = lstFiles.Count;

				int pbFileCount = 0;
				// 开始处理
				foreach (KeyValuePair<string, string> de in lstFiles)
				{
					TransEncoding(de.Key, de.Value, strDstEncodingName,
						cbSrcEncoding.SelectedIndex == 0, cbDefaultEncoding.SelectedItem.ToString());

					pbHelper.SetValue(++pbFileCount);
				}

				tsslStatus.Text = Apq.GlobalObject.UILang["转换完成！"];
				MessageBox.Show(Apq.GlobalObject.UILang["转换完成！"]);

				// 处理完成,进度条回0
				pbHelper.SetValue(0);
			});
		}
		#endregion

		public void AddFile2FileList(Dictionary<string, string> lstFiles, string strFile)
		{
			// 匹配过滤
			bool bMatch = false;
			string[] strExts = txtExt.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string strExt in strExts)
			{
				if (strExt == "*.*")
				{
					bMatch = true;
					break;
				}
				if (Path.GetExtension(strFile).Equals(Path.GetExtension(strExt), StringComparison.OrdinalIgnoreCase))
				{
					bMatch = true;
					break;
				}
			}

			if (bMatch)
			{
				// 结果文件
				string strDstFullName = strFile;
				string strDstEncodingName = cbDstEncoding.SelectedItem.ToString();
				if (rbEncodeName.Checked)
				{
					strDstFullName = Path.GetDirectoryName(strFile) + "\\";
					strDstFullName += Path.GetFileNameWithoutExtension(strFile) + "_" + strDstEncodingName;
					strDstFullName += Path.GetExtension(strFile);
				}
				if (rbCustomer.Checked)
				{
					strDstFullName = Path.GetDirectoryName(strFile) + "\\";
					strDstFullName += Path.GetFileNameWithoutExtension(strFile) + "_" + txtCustomer.Text;
					strDstFullName += Path.GetExtension(strFile);
				}

				if (!lstFiles.ContainsKey(strFile))
				{
					lstFiles.Add(strFile, strDstFullName);
				}
			}
		}

		public void AddFolder2FileList(Dictionary<string, string> lstFiles, string strFolder, bool Recursive)
		{
			TreeListViewItem node = tlvHelper.FindNodeByFullPath(strFolder);
			int HasChildren = 0;
			if (node != null)
			{
				HasChildren = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 1].Text);
			}

			if (HasChildren == 0)
			{
				string[] aryFiles = Directory.GetFiles(strFolder);
				foreach (string strFile in aryFiles)
				{
					TreeListViewItem nodeFile = tlvHelper.FindNodeByFullPath(strFile);
					if (nodeFile == null || nodeFile.Checked)
					{
						AddFile2FileList(lstFiles, strFile);
					}
				}

				if (Recursive)
				{
					string[] aryFolders = Directory.GetDirectories(strFolder);
					foreach (string str in aryFolders)
					{
						AddFolder2FileList(lstFiles, str, Recursive);
					}
				}
			}
		}
	}
}