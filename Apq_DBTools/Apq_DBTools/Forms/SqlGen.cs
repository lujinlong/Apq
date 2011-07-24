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
using Apq_DBTools.Forms;
using Apq.TreeListView;
using System.IO;
using org.mozilla.intl.chardet;
using Apq.DllImports;
using System.Collections;

namespace Apq_DBTools
{
	public partial class SqlGen : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public SqlGen()
		{
			InitializeComponent();
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["文本文件编码转换"] + " - " + ++FormCount;
			TabText = Text;

			tsbConnectDB.Text = Apq.GlobalObject.UILang["连接"];
			tsmiRefresh.Text = Apq.GlobalObject.UILang["刷新(&F)"];

			tssbGenSql.Text = Apq.GlobalObject.UILang["生成"];
			tsmiMeta.Text = Apq.GlobalObject.UILang["元数据脚本(&M)"];
		}

		private void SqlGen_Load(object sender, EventArgs e)
		{
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
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
		}
		#endregion

		public void UIEnable(bool Enable)
		{
			dataGridView1.Enabled = Enable;
			tsbConnectDB.Enabled = Enable;
			tssbGenSql.Enabled = Enable;

			Cursor = Enable ? Cursors.Default : Cursors.WaitCursor;
		}

		private void SqlGen_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void SqlGen_Activated(object sender, EventArgs e)
		{
		}

		private void SqlGen_Deactivate(object sender, EventArgs e)
		{

		}

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			//+从数据库获取列表：表，存储过程 [默认全选]
        }

        private void tsbConnectDB_ButtonClick(object sender, EventArgs e)
        {
            //+连接到数据库并执行刷新动作
			Apq.Windows.Forms.DBConnector DBConnector = new Apq.Windows.Forms.DBConnector();
			DBConnector.ShowDialog(this);
        }

        private void tsmiMeta_Click(object sender, EventArgs e)
        {
            //+为列表中已选中的项生成语句，语句完成将这些项插入到dbv_table,dbv_column,dbv_proc中
        }
	}
}