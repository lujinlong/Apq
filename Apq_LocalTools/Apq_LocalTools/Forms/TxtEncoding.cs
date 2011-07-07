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
using org.mozilla.intl.chardet;
using Apq.DllImports;
using System.Collections;

namespace Apq_LocalTools
{
	public partial class TxtEncoding : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public TxtEncoding()
		{
			InitializeComponent();
		}

		private Apq.Windows.Forms.TSProgressBarHelper pbHelper;

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["文本文件编码转换"] + " - " + ++FormCount;
			TabText = Text;

			groupBox1.Text = Apq.GlobalObject.UILang["读取参数"];
			label3.Text = Apq.GlobalObject.UILang["原始编码："];
			label2.Text = Apq.GlobalObject.UILang["默认编码："];
			label5.Text = Apq.GlobalObject.UILang["文件类型："];
			label4.Text = Apq.GlobalObject.UILang["自动检测无法确定编码时使用默认编码读取原始文件"];
			cbRecursive.Text = Apq.GlobalObject.UILang["包含子目录"];

			groupBox2.Text = Apq.GlobalObject.UILang["转换参数"];
			label6.Text = Apq.GlobalObject.UILang["目标编码："];
			label1.Text = Apq.GlobalObject.UILang["重命名："];
			rbKeep.Text = Apq.GlobalObject.UILang["原名"];
			rbEncodeName.Text = Apq.GlobalObject.UILang["原名_编码"];
			rbCustomer.Text = Apq.GlobalObject.UILang["原名_自定义"];

			tsbRefresh.Text = Apq.GlobalObject.UILang["刷新(&F)"];
			btnTrans.Text = Apq.GlobalObject.UILang["开始转换(&T)"];
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

			cbSrcEncoding.SelectedIndex = 0;
			cbDefaultEncoding.SelectedIndex = 1;
			cbDstEncoding.SelectedIndex = 0;
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
			cbSrcEncoding.Enabled = Enable;
			cbDefaultEncoding.Enabled = Enable;
			txtExt.Enabled = Enable;
			cbRecursive.Enabled = Enable;

			cbDstEncoding.Enabled = Enable;
			rbKeep.Enabled = Enable;
			rbEncodeName.Enabled = Enable;
			rbCustomer.Enabled = Enable;
			txtCustomer.Enabled = Enable;

			Cursor = Enable ? Cursors.Default : Cursors.WaitCursor;
		}

		void pbHelper_Completed(ToolStripProgressBar obj)
		{
			Apq.Windows.Delegates.Action_UI<ToolStripStatusLabel>(this, tsslStatus, delegate(ToolStripStatusLabel ctrl)
			{
				UIEnable(true);
				btnTrans.Text = Apq.GlobalObject.UILang["开始转换(&T)"];
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
						AddFile(lstFiles, node.FullPath);
					}
					else
					{
						AddChildren(lstFiles, node.FullPath, cbRecursive.Checked);
					}
				}

				tspb.Maximum = lstFiles.Count;

				int pbFileCount = 0;
				// 开始处理
				string strDstEncodingName = cbDstEncoding.SelectedItem.ToString();
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

		public void AddFile(Dictionary<string, string> lstFiles, string strFile)
		{
			if (Apq.IO.PathHelper.MatchFileExt(strFile, txtExt.Text))
			{
				// 结果文件
				string strDstFullName = strFile;
				string strDstEncodingName = cbDstEncoding.SelectedItem.ToString();
				if (rbEncodeName.Checked)
				{
					strDstFullName = Path.GetDirectoryName(strFile) + Path.DirectorySeparatorChar;
					strDstFullName += Path.GetFileNameWithoutExtension(strFile) + "_" + strDstEncodingName;
					strDstFullName += Path.GetExtension(strFile);
				}
				if (rbCustomer.Checked)
				{
					strDstFullName = Path.GetDirectoryName(strFile) + Path.DirectorySeparatorChar;
					strDstFullName += Path.GetFileNameWithoutExtension(strFile) + "_" + txtCustomer.Text;
					strDstFullName += Path.GetExtension(strFile);
				}

				if (!lstFiles.ContainsKey(strFile))
				{
					lstFiles.Add(strFile, strDstFullName);
				}
			}
		}

		public void AddChildren(Dictionary<string, string> lstFiles, string strFolder, bool Recursive)
		{
			TreeListViewItem node = fsExplorer1.tlvHelper.FindNodeByFullPath(strFolder);
			int HasChildren = 0;
			if (node != null)
			{
				HasChildren = Apq.Convert.ChangeType<int>(node.SubItems[fsExplorer1.Columns.Count + 1].Text);
			}

			if (HasChildren >= 0)
			{
				if (Recursive)
				{
					string[] aryFolders = Directory.GetDirectories(strFolder);
					foreach (string str in aryFolders)
					{
						AddChildren(lstFiles, str, Recursive);
					}
				}

				string[] aryFiles = Directory.GetFiles(strFolder);
				foreach (string strFile in aryFiles)
				{
					TreeListViewItem nodeFile = fsExplorer1.tlvHelper.FindNodeByFullPath(strFile);
					if (nodeFile == null || nodeFile.Checked)
					{
						AddFile(lstFiles, strFile);
					}
				}
			}
		}

		public static bool found = false;
		public static string detEncoding = string.Empty;// 自动检测到的文件类型
		public void TransEncoding(string srcFullName, string dstFullName,
			string dstEncoding = "utf8",
			bool IsAutoDet = true, string srcEncoding = "gb2312")
		{
			Encoding Edst = Encoding.GetEncoding(dstEncoding);
			Encoding Esrc = Encoding.GetEncoding(srcEncoding);

			if (IsAutoDet)
			{
				#region 检测编码
				nsDetector det = new nsDetector();
				Notifier not = new Notifier();
				det.Init(not);

				byte[] buf = new byte[1024];
				int len;
				bool done = false;
				bool isAscii = true;

				FileStream fs = File.OpenRead(srcFullName);
				len = fs.Read(buf, 0, buf.Length);
				while (len > 0)
				{
					// Check if the stream is only ascii.
					if (isAscii)
						isAscii = det.isAscii(buf, len);

					// DoIt if non-ascii and not done yet.
					if (!isAscii && !done)
						done = det.DoIt(buf, len, false);

					len = fs.Read(buf, 0, buf.Length);
				}
				det.DataEnd();
				fs.Close();

				if (isAscii)
				{
					found = true;
					detEncoding = "ASCII";
				}

				if (!found)
				{
					string[] prob = det.getProbableCharsets();
					if (prob.Length > 0)
					{
						detEncoding = prob[0];
					}
					else
					{
						detEncoding = srcEncoding;
					}
				}
				#endregion

				Esrc = Encoding.GetEncoding(detEncoding);
			}

			#region 编码转换
			string strAll = File.ReadAllText(srcFullName, Esrc);
			File.WriteAllText(dstFullName, strAll, Edst);
			#endregion
		}

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			LoadData(FormDataSet);
		}
	}

	// C# 1 doesn't support anonymous methods...
	public class Notifier : nsICharsetDetectionObserver
	{
		public void Notify(string charset)
		{
			TxtEncoding.found = true;
			TxtEncoding.detEncoding = charset;
		}
	}
}