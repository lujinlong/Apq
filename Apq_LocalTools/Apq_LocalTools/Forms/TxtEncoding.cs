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

namespace Apq_LocalTools
{
	public partial class TxtEncoding : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public TxtEncoding()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["文本文件编码转换"] + " - " + ++FormCount;
			TabText = Text;

			groupBox1.Text = Apq.GlobalObject.UILang["读取参数"];

			groupBox1.Text = Apq.GlobalObject.UILang["转换参数"];
		}

		private void TxtEncoding_Load(object sender, EventArgs e)
		{
			TransFinished += new EventHandler(TxtEncoding_TransFinished);
		}

		public void UIEnable(bool Enable)
		{
			treeListView1.Enabled = Enable;
			cbSrcEncoding.Enabled = Enable;
			cbDefaultEncoding.Enabled = Enable;
			cbDstEncoding.Enabled = Enable;
			cbContainsChildren.Enabled = Enable;
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
				btnTrans.Text = Apq.GlobalObject.UILang["开始转换(&T)"];
			}
		}

		private void Work()
		{
			tspb_SetValue(0);
		}
	}
}