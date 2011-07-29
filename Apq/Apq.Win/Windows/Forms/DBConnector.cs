using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Xml;
using System.IO;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 数据库连接对话框
	/// </summary>
	public partial class DBConnector : DockForm
	{
		private static string _ConnectionFolder = string.Empty;
		/// <summary>
		/// 获取连接文件夹
		/// </summary>
		private string ConnectionFolder
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_ConnectionFolder))
				{
					_ConnectionFolder = System.IO.Path.GetDirectoryName(Apq.GlobalObject.TheProcess.MainModule.FileName) + @"\DBConnect";
				}
				return _ConnectionFolder;
			}
		}

		// 点击TabPage时改变其值,用于返回数据库连接
		private Apq.Data.Common.DBProduct _DBSelected = Apq.Data.Common.DBProduct.MySql;
		/// <summary>
		/// 获取数据库连接类型
		/// </summary>
		public Apq.Data.Common.DBProduct DBSelected
		{
			get { return _DBSelected; }
		}
		/// <summary>
		/// 获取数据库连接
		/// </summary>
		public DbConnection DBConnection
		{
			get
			{
				switch (_DBSelected)
				{
					case Apq.Data.Common.DBProduct.MySql:
						return _MySqlConnection;
					case Apq.Data.Common.DBProduct.MsSql:
					default:
						return _SqlConnection;
				}
			}
		}

		/// <summary>
		/// 数据库连接对话框
		/// </summary>
		public DBConnector()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 设置界面语言值
		/// </summary>
		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["连接到数据库"];
			this.TabText = this.Text;

			#region MySql
			tabPage1.Text = Apq.GlobalObject.UILang["MySql"];
			label3.Text = Apq.GlobalObject.UILang["名称："];
			label1.Text = Apq.GlobalObject.UILang["服务器："];
			label2.Text = Apq.GlobalObject.UILang["用户名："];
			label4.Text = Apq.GlobalObject.UILang["密码："];
			label5.Text = Apq.GlobalObject.UILang["端口："];
			label6.Text = Apq.GlobalObject.UILang["数据库列表："];
			cbMySqlSavePwd.Text = Apq.GlobalObject.UILang["保存密码"];
			btnMySqlSaveName.Text = Apq.GlobalObject.UILang["保存(&S)"];
			btnMySqlRefresh.Text = Apq.GlobalObject.UILang["刷新(&F)"];
			btnMySqlConfirm.Text = Apq.GlobalObject.UILang["确定(&O)"];
			btnMySqlCancel.Text = Apq.GlobalObject.UILang["取消(&C)"];
			btnMySqlTest.Text = Apq.GlobalObject.UILang["测试连接(&T)"];
			#endregion

			#region MsSql
			tabPage2.Text = Apq.GlobalObject.UILang["MsSql"];
			btnMsSqlConfirm.Text = Apq.GlobalObject.UILang["确定(&O)"];
			btnMsSqlCancel.Text = Apq.GlobalObject.UILang["取消(&C)"];
			btnMsSqlTest.Text = Apq.GlobalObject.UILang["测试连接(&T)"];
			#endregion

			base.SetUILang(UILang);
		}

		private void DBConnector_Load(object sender, EventArgs e)
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

			#region 加载所有查找表
			// 读取MySql连接文件列表
			if (System.IO.Directory.Exists(ConnectionFolder + @"\MySql"))
			{
				string strMySqlName = Apq.Win.GlobalObject.XmlConfigChain[this.GetType(), "MySqlName"];
				string[] aryFiles = System.IO.Directory.GetFiles(ConnectionFolder + @"\MySql", "*.xml");
				for (int i = 0; i < aryFiles.Length; i++)
				{
					string strFileName = System.IO.Path.GetFileNameWithoutExtension(aryFiles[i]);
					cbMySqlName.Items.Add(strFileName);

					if (strFileName.Equals(strMySqlName, StringComparison.OrdinalIgnoreCase))
					{
						cbMySqlName.SelectedIndex = i;
					}
				}
			}
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

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex == 0)
			{
				_DBSelected = Apq.Data.Common.DBProduct.MySql;
			}
			if (tabControl1.SelectedIndex == 1)
			{
				_DBSelected = Apq.Data.Common.DBProduct.MsSql;
			}
		}

		public void MySqlUIEnable(bool Enable)
		{
			tabControl1.Enabled = Enable;

			cbMySqlName.Enabled = Enable;
			txtMySqlServer.Enabled = Enable;
			txtMySqlUserId.Enabled = Enable;
			txtMySqlPwd.Enabled = Enable;
			txtMySqlPort.Enabled = Enable;
			cbMySqlDBName.Enabled = Enable;
			cbMySqlSavePwd.Enabled = Enable;

			btnMySqlConfirm.Enabled = Enable;
			btnMySqlCancel.Enabled = Enable;
			btnMySqlTest.Enabled = Enable;
		}

		public void MsSqlUIEnable(bool Enable)
		{
			tabControl1.Enabled = Enable;

			btnMsSqlConfirm.Enabled = Enable;
			btnMsSqlCancel.Enabled = Enable;
			btnMsSqlTest.Enabled = Enable;
		}

		#region MySql
		// 启用“保存”按钮可用状态
		private void MySql_SaveName_Enable()
		{
			if (!btnMySqlSaveName.Enabled && !string.IsNullOrWhiteSpace(cbMySqlName.Text))
			{
				btnMySqlSaveName.Enabled = true;
			}
		}

		// 输入连接名
		private void cbMySqlName_TextUpdate(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();

			//cbMySqlName.SelectedText = cbMySqlName.Text;
		}
		// 输入服务器
		private void txtMySqlServer_TextChanged(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		// 输入用户名
		private void txtMySqlUserId_TextChanged(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		// 输入密码
		private void txtMySqlPwd_TextChanged(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		// 输入端口
		private void txtMySqlPort_TextChanged(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		// 是否保存密码
		private void cbMySqlSavePwd_CheckedChanged(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		// 改变数据库
		private void cbMySqlDBName_TextUpdate(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		private void cbMySqlDBName_SelectedIndexChanged(object sender, EventArgs e)
		{
			MySql_SaveName_Enable();
		}
		// 保存
		private void btnMySqlSaveName_Click(object sender, EventArgs e)
		{
			try
			{
				if (!cbMySqlName.Items.Contains(cbMySqlName.Text))
				{
					cbMySqlName.Items.Add(cbMySqlName.Text);
				}

				//获取界面值，加密密码后生成连接文件，覆盖保存
				DBConnector_XSD xsd = new DBConnector_XSD();
				DataRow dr = xsd.MySql.NewRow();
				xsd.MySql.Rows.Add(dr);
				dr["Server"] = txtMySqlServer.Text;
				dr["Port"] = txtMySqlPort.Text;
				dr["Uid"] = txtMySqlUserId.Text;
				dr["PwdD"] = txtMySqlPwd.Text;
				if (!string.IsNullOrWhiteSpace(cbMySqlDBName.Text))
				{
					dr["DBName"] = cbMySqlDBName.Text;
				}
				if (cbMySqlSavePwd.Checked)
				{
					dr["PwdC"] = Apq.Security.Cryptography.DESHelper.EncryptString(txtMySqlPwd.Text,
						Apq.Win.GlobalObject.XmlAsmConfig["Apq.Win.GlobalObject", "DESKey"],
						Apq.Win.GlobalObject.XmlAsmConfig["Apq.Win.GlobalObject", "DESIV"]
					);
				}

				xsd.MySql.Columns.Remove("PwdD");
				if (!cbMySqlSavePwd.Checked)
				{
					xsd.MySql.Columns.Remove("PwdC");
				}
				if (!Directory.Exists(ConnectionFolder + @"\MySql"))
				{
					Directory.CreateDirectory(ConnectionFolder + @"\MySql");
				}
				xsd.MySql.WriteXml(ConnectionFolder + @"\MySql\" + cbMySqlName.Text + ".xml", XmlWriteMode.IgnoreSchema);

				btnMySqlSaveName.Enabled = false;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
		}
		// 打开连接文件
		private void cbMySqlName_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				//打开连接文件解密密码并对界面赋值
				DBConnector_XSD xsd = new DBConnector_XSD();
				xsd.MySql.ReadXml(ConnectionFolder + @"\MySql\" + cbMySqlName.Text + ".xml");
				if (xsd.MySql.Rows.Count > 0)
				{
					DBConnector_XSD.MySqlRow dr = xsd.MySql.Rows[0] as DBConnector_XSD.MySqlRow;
					dr.PwdD = Apq.Security.Cryptography.DESHelper.DecryptString(Apq.Convert.ChangeType<string>(dr["PwdC"]),
						Apq.Win.GlobalObject.XmlAsmConfig["Apq.Win.GlobalObject", "DESKey"],
						Apq.Win.GlobalObject.XmlAsmConfig["Apq.Win.GlobalObject", "DESIV"]
					);

					txtMySqlServer.Text = dr.Server;
					txtMySqlUserId.Text = dr.Uid;
					txtMySqlPwd.Text = dr.PwdD;
					txtMySqlPort.Text = dr.Port;
					if (!Apq.Convert.IsNull(dr["DBName"]))
					{
						cbMySqlDBName.Text = dr.DBName;
					}

					btnMySqlSaveName.Enabled = false;
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
		}
		// 刷新
		private void btnMySqlRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				btnMySqlRefresh.Enabled = false;
				MySqlUIEnable(false);
				cbMySqlDBName.Items.Clear();

				// 创建连接
				CreateMySqlConnection();

				// 获取数据库列表
				DataSet ds = new DataSet();
				Apq.Data.Common.DbConnectionHelper dch = new Data.Common.DbConnectionHelper(_MySqlConnection);
				DbDataAdapter dda = dch.CreateAdapter(@"SELECT SCHEMA_NAME FROM information_schema.SCHEMATA");
				dda.Fill(ds);

				// 填充到下拉框选项
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					cbMySqlDBName.Items.Add(dr[0]);
				}
				btnMySqlRefresh.Enabled = true;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
			finally
			{
				MySqlUIEnable(true);
			}
		}
		// 确定
		private void btnMySqlConfirm_Click(object sender, EventArgs e)
		{
			try
			{
				CreateMySqlConnection();
				if (TestMySqlConnection())
				{
					DialogResult = System.Windows.Forms.DialogResult.OK;
					Close();
				}
				else
				{
					MessageBox.Show(this, Apq.GlobalObject.UILang["连接失败"]);
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
		}
		// 取消
		private void btnMySqlCancel_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}
		// 测试
		private void btnMySqlTest_Click(object sender, EventArgs e)
		{
			try
			{
				MySqlUIEnable(false);

				CreateMySqlConnection();
				if (TestMySqlConnection())
				{
					MessageBox.Show(this, Apq.GlobalObject.UILang["连接成功"]);
					btnMySqlRefresh.Enabled = true;
				}
				else
				{
					MessageBox.Show(this, Apq.GlobalObject.UILang["连接失败"]);
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
			finally
			{
				MySqlUIEnable(true);
			}
		}
		// 从界面创建MySql连接
		private MySqlConnection CreateMySqlConnection()
		{
			Apq.Data.Common.DbConnectionHelper.Close(_MySqlConnection);
			_MySqlConnection.ConnectionString = Apq.ConnectionStrings.Common.GetConnectionString(Data.Common.DBProduct.MySql,
				txtMySqlServer.Text,
				Apq.Convert.ChangeType<int>(txtMySqlPort.Text, 3306),
				txtMySqlUserId.Text,
				txtMySqlPwd.Text,
				string.IsNullOrWhiteSpace(cbMySqlDBName.Text) ? "mysql" : cbMySqlDBName.Text
			);

			return _MySqlConnection;
		}
		// MySql连接测试
		private bool TestMySqlConnection()
		{
			CreateMySqlConnection();

			try
			{
				_MySqlConnection.Open();
				Apq.Data.Common.DbConnectionHelper.Close(_MySqlConnection);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}
