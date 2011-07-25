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
using MySql.Data.MySqlClient;

namespace Apq_DBTools
{
	public partial class SqlGen : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public SqlGen()
		{
			InitializeComponent();

			//Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dataGridView1);
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

			sfdMeta.Filter = Apq.GlobalObject.UILang["SQL 文件|*.sql|所有文件|*.*"];
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
			_xsd.dic_ObjectType.InitData();
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
				_xsd.Meta.Rows.Clear();
				_xsd.dbv_proc.Rows.Clear();
				_xsd.dbv_column.Rows.Clear();
				_xsd.dbv_table.Rows.Clear();

				//+从数据库获取列表：表，存储过程 [默认全选]
				try
				{
					Apq.Data.Common.DbConnectionHelper dch = new Apq.Data.Common.DbConnectionHelper(_DBConnection);
					DbDataAdapter dda = dch.CreateAdapter();
					if (_DBConnectionType == Apq.Data.Common.DBProduct.MySql)
					{
						// 表
						dda.SelectCommand.CommandText = @"
SELECT `TABLE_SCHEMA` as SchemaName,`Table_Name` as TableName,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,'' as PrimaryKeys
  FROM `information_schema`.`TABLES`
 WHERE `TABLE_SCHEMA` = DATABASE();
";
						dda.Fill(_xsd.dbv_table);
						// 列
						dda.SelectCommand.CommandText = @"
SELECT `COLUMN_NAME` as ColName,`COLUMN_DEFAULT` as DefaultValue,
	CASE `IS_NULLABLE` WHEN 'NO' THEN 0 ELSE 1 END as NullAble,
	`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,
	`COLUMN_TYPE`,`COLUMN_KEY`,
	CASE WHEN INSTR(`EXTRA`,'auto_increment') > 0 THEN 1 ELSE 0 END as is_auto_increment,
	`COLUMN_COMMENT`,c.`TABLE_SCHEMA` as SchemaName,c.`TABLE_NAME` as TableName
  FROM `information_schema`.`COLUMNS` c 
	INNER JOIN `information_schema`.`TABLES` t ON c.`TABLE_SCHEMA` = t.`TABLE_SCHEMA` AND c.`TABLE_NAME` = t.`TABLE_NAME` 
	-- INNER JOIN `dbv_table` dt ON t.`TABLE_SCHEMA` = dt.`SchemaName` AND t.`TABLE_NAME` = dt.`TableName`
 WHERE c.`TABLE_SCHEMA` = DATABASE();
";
						dda.Fill(_xsd.dbv_column);
						// 存储过程
						dda.SelectCommand.CommandText = @"
SELECT `db` as SchemaName,`name` as ProcName,`param_list`,`returns`,`body`,`comment`
  FROM `mysql`.`proc`
 WHERE `db` = DATABASE();
";
						dda.Fill(_xsd.dbv_proc);

						// 为列设置TID
						foreach (SqlGen_XSD.dbv_columnRow dr in _xsd.dbv_column.Rows)
						{
							SqlGen_XSD.dbv_tableRow drT = _xsd.dbv_table.FindByTableName(dr.SchemaName, dr.TableName);
							dr.TID = drT.TID;
						}
						// 为表查找主键列
						foreach (SqlGen_XSD.dbv_tableRow dr in _xsd.dbv_table.Rows)
						{
							SqlGen_XSD.dbv_columnRow[] drCs = dr.GetChildRows("FK_dbv_table_dbv_column") as SqlGen_XSD.dbv_columnRow[];
							string strCols = string.Empty;
							foreach (SqlGen_XSD.dbv_columnRow drc in drCs)
							{
								if ("PRI".Equals(drc.COLUMN_KEY, StringComparison.OrdinalIgnoreCase))
								{
									strCols += "," + drc.ColName;
								}
							}
							if (strCols.Length > 0)
							{
								dr.PrimaryKeys = strCols.Substring(1);
							}
						}
						// 将表和存储过程名加到元数据表
						foreach (SqlGen_XSD.dbv_tableRow drT in _xsd.dbv_table.Rows)
						{
							SqlGen_XSD.MetaRow drMeta = _xsd.Meta.NewMetaRow();
							drMeta.ObjectType = 1;
							drMeta._CheckState = 1;
							drMeta.SchemaName = drT.SchemaName;
							drMeta.ObjectName = drT.TableName;
							_xsd.Meta.Rows.Add(drMeta);
						}
						foreach (SqlGen_XSD.dbv_procRow drP in _xsd.dbv_proc.Rows)
						{
							SqlGen_XSD.MetaRow drMeta = _xsd.Meta.NewMetaRow();
							drMeta.ObjectType = 2;
							drMeta._CheckState = 1;
							drMeta.SchemaName = drP.SchemaName;
							drMeta.ObjectName = drP.ProcName;
							_xsd.Meta.Rows.Add(drMeta);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message);
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
			StringBuilder sb = new StringBuilder();
			sfdMeta.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfdMeta_InitialDirectory"];
			if (sfdMeta.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfdMeta_InitialDirectory"] = Path.GetDirectoryName(sfdMeta.FileName);

				// 为列表中已选中的项生成语句，语句完成将这些项插入到dbv_table,dbv_column,dbv_proc中
				sb.Append(string.Format(@"
USE {0};

-- 元数据
DELETE FROM `dbv_proc`;
DELETE FROM `dbv_column`;
DELETE FROM `dbv_table`;
",
						_DBConnection.Database
					)
				);
				foreach (SqlGen_XSD.MetaRow dr in _xsd.Meta.Rows)
				{
					if (dr.ObjectType == 1)
					{
						SqlGen_XSD.dbv_tableRow drT = _xsd.dbv_table.FindByTableName(dr.SchemaName, dr.ObjectName);
						sb.Append(string.Format(@"

-- 表:{0}
INSERT INTO `dbv_table`(`TID`,`SchemaName`,`TableName`,`ENGINE`,`CREATE_OPTIONS`,`TABLE_COMMENT`,`PrimaryKeys`) VALUES({1},{2},{3},{4},{5},{6},{7});",
								drT.TableName,
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT.TID),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT.SchemaName),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT.TableName),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT["ENGINE"]),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT["CREATE_OPTIONS"]),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT["TABLE_COMMENT"]),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drT.PrimaryKeys)
							)
						);
						SqlGen_XSD.dbv_columnRow[] drCs = drT.GetChildRows("FK_dbv_table_dbv_column") as SqlGen_XSD.dbv_columnRow[];
						foreach (SqlGen_XSD.dbv_columnRow drC in drCs)
						{
							sb.Append(string.Format(@"
INSERT INTO `dbv_column`(`TID`,`ColName`,`DefaultValue`,`NullAble`,`DATA_TYPE`,`CHARACTER_MAXIMUM_LENGTH`,`CHARACTER_OCTET_LENGTH`,`NUMERIC_PRECISION`,`NUMERIC_SCALE`,`CHARACTER_SET_NAME`,`COLLATION_NAME`,`COLUMN_TYPE`,`COLUMN_KEY`,`is_auto_increment`,`COLUMN_COMMENT`,`SchemaName`,`TableName`) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16});",
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.TID),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.ColName),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["DefaultValue"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.NullAble),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.DATA_TYPE),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["CHARACTER_MAXIMUM_LENGTH"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["CHARACTER_OCTET_LENGTH"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["NUMERIC_PRECISION"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["NUMERIC_SCALE"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["CHARACTER_SET_NAME"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["COLLATION_NAME"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.COLUMN_TYPE),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["COLUMN_KEY"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.is_auto_increment),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC["COLUMN_COMMENT"]),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.SchemaName),
									Apq.Data.MySqlClient.Common.ConvertToSqlON(drC.TableName)
								)
							);
						}
					}
					if (dr.ObjectType == 2)
					{
						SqlGen_XSD.dbv_procRow drP = _xsd.dbv_proc.FindByProcName(dr.SchemaName, dr.ObjectName);
						sb.Append(string.Format(@"

-- 存储过程:{0}
INSERT INTO `dbv_proc`(`SchemaName`,`ProcName`,`param_list`,`returns`,`body`,`comment`) VALUES({1},{2},{3},{4},{5},{6});",
								drP.ProcName,
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drP.SchemaName),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drP.ProcName),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drP.param_list),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drP.returns),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drP.body),
								Apq.Data.MySqlClient.Common.ConvertToSqlON(drP.comment)
							)
						);
					}
				}

				sb.Append(@"
");
				File.WriteAllText(sfdMeta.FileName, sb.ToString());
			}
		}
		// 生成初始数据语句并保存到文件
		private void tsmiData_Click(object sender, EventArgs e)
		{
			try
			{
				Apq.Data.Common.DbConnectionHelper.Open(_DBConnection);
			}
			catch
			{
				MessageBox.Show(this, Apq.GlobalObject.UILang["数据库未连接或已断开"]);
				return;
			}
			finally
			{
				Apq.Data.Common.DbConnectionHelper.Close(_DBConnection);
			}

			Apq.Data.Common.DbConnectionHelper dch = new Apq.Data.Common.DbConnectionHelper(_DBConnection);
			StringBuilder sb = new StringBuilder();
			sfdMeta.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfdMeta_InitialDirectory"];
			if (sfdMeta.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfdMeta_InitialDirectory"] = Path.GetDirectoryName(sfdMeta.FileName);

				// 为列表中已选中的表生成初始化语句，语句功能：完成将数据库中的数据插入到对应的表中，按主键添加
				sb.Append(string.Format(@"
USE {0};

-- 初始数据
",
						_DBConnection.Database
					)
				);
				foreach (SqlGen_XSD.MetaRow dr in _xsd.Meta.Rows)
				{
					if (dr.ObjectType == 1)
					{
						SqlGen_XSD.dbv_tableRow drT = _xsd.dbv_table.FindByTableName(dr.SchemaName, dr.ObjectName);

						//+将表读到内存，
						DataSet ds = new DataSet();
						DbDataAdapter dda = dch.CreateAdapter(string.Format(@"SELECT * FROM {0}", drT.TableName));
						dda.Fill(ds);
					}
				}

				sb.Append(@"
");
				File.WriteAllText(sfdMeta.FileName, sb.ToString());
			}
		}

		#region 选择
		private void tsmiSelectTable_CheckedChanged(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			int _CheckState = Apq.Convert.ChangeType<int>(tsmiSelectTable.Checked);
			foreach (SqlGen_XSD.MetaRow dr in _xsd.Meta.Rows)
			{
				if (dr.ObjectType == 1)
				{
					dr._CheckState = _CheckState;
				}
			}
		}

		private void tsmiSelectProc_CheckedChanged(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			int _CheckState = Apq.Convert.ChangeType<int>(tsmiSelectTable.Checked);
			foreach (SqlGen_XSD.MetaRow dr in _xsd.Meta.Rows)
			{
				if (dr.ObjectType == 2)
				{
					dr._CheckState = _CheckState;
				}
			}
		}

		private void tsmiSelectAll_Click(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			foreach (SqlGen_XSD.MetaRow dr in _xsd.Meta.Rows)
			{
				dr._CheckState = 1;
			}
		}

		private void tsmiSelectReverse_Click(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			foreach (SqlGen_XSD.MetaRow dr in _xsd.Meta.Rows)
			{
				dr._CheckState = Math.Abs(dr._CheckState - 1);
			}
		}
		#endregion
	}
}