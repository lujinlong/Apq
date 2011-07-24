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

			Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dataGridView1);
			Apq.Windows.Forms.DataGridViewHelper.AddBehaivor(dataGridView1);
		}

		private Apq.Data.Common.DBProduct _DBConnectionType = Apq.Data.Common.DBProduct.MySql;
		private DbConnection _DBConnection;

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["脚本生成"] + " - " + ++FormCount;
			TabText = Text;

			tsbConnectDB.Text = Apq.GlobalObject.UILang["连接"];
			tsmiRefresh.Text = Apq.GlobalObject.UILang["刷新(&F)"];

			tssbGenSql.Text = Apq.GlobalObject.UILang["生成"];
			tsmiMeta.Text = Apq.GlobalObject.UILang["元数据脚本(&M)"];
			tsmiData.Text = Apq.GlobalObject.UILang["初始化数据(&D)"];

			// 列
			checkStateDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["选择"];
			objectTypeDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["类型"];
			schemaNameDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["架构"];
			objectNameDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["名称"];
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
			if (_DBConnection != null)
			{
				//+从数据库获取列表：表，存储过程 [默认全选]
				try
				{
					/*
// 表
SELECT `TABLE_SCHEMA`,`Table_Name`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`
  FROM `information_schema`.`TABLES`
 WHERE `TABLE_SCHEMA` = DATABASE();
// 列
SELECT dt.`TID`,`COLUMN_NAME`,`COLUMN_DEFAULT`,
	CASE `IS_NULLABLE` WHEN 'NO' THEN 0 ELSE 1 END,
	`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,
	`COLUMN_TYPE`,`COLUMN_KEY`,
	CASE WHEN INSTR(`EXTRA`,'auto_increment') > 0 THEN 1 ELSE 0 END,
	`COLUMN_COMMENT`,c.`TABLE_SCHEMA`,c.`TABLE_NAME`
  FROM `information_schema`.`COLUMNS` c 
	INNER JOIN `information_schema`.`TABLES` t ON c.`TABLE_SCHEMA` = t.`TABLE_SCHEMA` AND c.`TABLE_NAME` = t.`TABLE_NAME` 
	INNER JOIN `dbv_table` dt ON t.`TABLE_SCHEMA` = dt.`SchemaName` AND t.`TABLE_NAME` = dt.`TableName`
 WHERE c.`TABLE_SCHEMA` = DATABASE();				
// 存储过程
SELECT `db`,`name`,`param_list`,`returns`,`body`,`comment` FROM `mysql`.`proc` WHERE `db` = DATABASE();
*/
				}
				catch
				{
				}
				finally
				{
				}
			}
		}

		private void tsbConnectDB_ButtonClick(object sender, EventArgs e)
		{
			// 连接到数据库并执行刷新动作
			Apq.Windows.Forms.DBConnector DBConnector = new Apq.Windows.Forms.DBConnector();
			if (DBConnector.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				_DBConnectionType = DBConnector.DBSelected;
				_DBConnection = DBConnector.DBConnection;
				tsbRefresh_Click(sender, e);
			}
		}
		// 生成元数据语句并保存到文件
		private void tsmiMeta_Click(object sender, EventArgs e)
		{
			//+为列表中已选中的项生成语句，语句完成将这些项插入到dbv_table,dbv_column,dbv_proc中
		}
	}
}