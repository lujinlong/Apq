using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using System.IO;
using System.Data.Common;

namespace ApqDBCManager.Forms
{
	public partial class DBC : Apq.Windows.Forms.DockForm, Apq.Editor.IFileLoader
	{
		public DBC()
		{
			InitializeComponent();

			Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dataGridView1);
			Apq.Windows.Forms.DataGridViewHelper.AddBehaivor(dataGridView1);
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["DB连接管理"];
			this.TabText = this.Text;

			acOpenFile.Text = Apq.GlobalObject.UILang["打开(&O)"];
			acOpenFile.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Open.png");
			tsbSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			tsbSelectAll.Text = Apq.GlobalObject.UILang["全选(&A)"];
			tssbSlts.Text = Apq.GlobalObject.UILang["批量设置(&E)"];
			tsmiSltsUserId.Text = Apq.GlobalObject.UILang["登录名"];
			tsmiSltsPwdD.Text = Apq.GlobalObject.UILang["密码"];
			tssbCreateCSFile.Text = Apq.GlobalObject.UILang["生成文件(&G)"];

			dBCIDDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["编号"];
			dBCNameDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["连接名"];
			computerIDDataGridViewComBoxColumn.HeaderText = Apq.GlobalObject.UILang["服务器"];
			dBIIDDataGridViewComboBoxColumn.HeaderText = Apq.GlobalObject.UILang["数据库实例"];
			dBNameDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["数据库名"];
			dBCUseTypeDataGridViewComboBoxColumn.HeaderText = Apq.GlobalObject.UILang["使用类型"];
			userIdDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["登录名"];
			pwdDDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["密码"];
			useTrustedDataGridViewCheckBoxColumn.HeaderText = Apq.GlobalObject.UILang["使用信任连接"];
			mirrorDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["镜像"];
			optionDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["选项"];

			sfd.Filter = Apq.GlobalObject.UILang["DBC文件(*.res)|*.res|所有文件(*.*)|*.*"];
			ofd.Filter = Apq.GlobalObject.UILang["DBC文件(*.res)|*.res|所有文件(*.*)|*.*"];
		}

		private void DBC_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsbSave.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Save.png");
			#endregion

			// 加载生成菜单
			foreach (DBS_XSD.DBCUseTypeRow dr in GlobalObject.Lookup.DBCUseType.Rows)
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem();
				tsmi.Text = dr.TypeCaption;
				tsmi.Tag = dr.DBCUseType;
				tsmi.Click += new EventHandler(tsmiCreatCSFile_Click);
				tssbCreateCSFile.DropDownItems.Add(tsmi);
			}
		}

		// 生成数据库连接文件
		void tsmiCreatCSFile_Click(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();

			ToolStripMenuItem tsb = sender as ToolStripMenuItem;
			sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if (tsb != null && sfd.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

				int nDBCUseType = Apq.Convert.ChangeType<int>(tsb.Tag);
				Apq.DBC.XSD xsd = new Apq.DBC.XSD();
				xsd.DBC.Merge(xsdDBC.DBC);
				xsd.DBC.Columns.Remove("ComputerID");
				xsd.DBC.Columns.Remove("PwdC");

				for (int i = xsd.DBC.Rows.Count - 1; i >= 0; i--)
				{
					if (Apq.Convert.ChangeType<int>(xsd.DBC.Rows[i]["DBCUseType"]) != nDBCUseType)
					{
						xsd.DBC.Rows.RemoveAt(i);
						continue;
					}
				}

				StringWriter sw = new StringWriter();
				xsd.WriteXml(sw, XmlWriteMode.IgnoreSchema);
				Common.SaveCSFile(sfd.FileName,sw.ToString());
				tsslOutInfo.Text = Apq.GlobalObject.UILang["保存文件成功"];
			}
		}

		private void tsbOpenFile_Click(object sender, EventArgs e)
		{
			ofd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "ofd_InitialDirectory"];
			if (ofd.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "ofd_InitialDirectory"] = System.IO.Path.GetDirectoryName(ofd.FileName);

				LoadData(FormDataSet);
				tsslOutInfo.Text = Apq.GlobalObject.UILang["加载成功"];
			}
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
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

			xsdDBC.DBC.ColumnChanged += new DataColumnChangeEventHandler(DBC_ColumnChanged);
			xsdDBC.DBC.RowChanged += new DataRowChangeEventHandler(DBC_RowChanged);
		}

		private void DBC_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
			{
				DBS_XSD.DBIRow drDBI = GlobalObject.Lookup.DBI.FindByDBIID(Convert.ToInt32(e.Row["DBIID"]));
				if (drDBI != null)
				{
					e.Row["ComputerID"] = drDBI.ComputerID;
				}
			}
		}

		private void DBC_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if (e.Column.ColumnName == "DBIID" && !Apq.Convert.LikeDBNull(e.Row["DBIID"]))
			{
				DBS_XSD.DBIRow drDBI = GlobalObject.Lookup.DBI.FindByDBIID(Convert.ToInt32(e.Row["DBIID"]));
				e.Row["ComputerID"] = drDBI.ComputerID;
			}
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
			if (System.IO.File.Exists(FileName))
			{
				try
				{
					string strCs = File.ReadAllText(FileName, Encoding.UTF8);
					string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs,
						GlobalObject.XmlConfigChain["Crypt", "DESKey"], GlobalObject.XmlConfigChain["Crypt", "DESIV"]);
					StringReader sr = new StringReader(str);
					xsdDBC.Clear();
					xsdDBC.DBC.ReadXml(sr);
				}
				catch { }
			}
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			bsComputer.DataSource = GlobalObject.Lookup;
			bsDBI.DataSource = GlobalObject.Lookup;
			bsDBCUseType.DataSource = GlobalObject.Lookup;
			#endregion

			bsDBC.DataSource = xsdDBC;
		}

		#endregion

		//打开
		private void acOpenFile_Execute(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			Open();
		}

		//保存
		private void tsbSave_Click(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			Save();
		}

		//全选
		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			dataGridView1.SelectAll();
		}

		#region 批量设置
		// 登录名
		private void tsmiSltsUserId_Click(object sender, EventArgs e)
		{
			int cellIndex = Apq.Windows.Forms.DataGridViewHelper.IndexOfHeader(dataGridView1.Columns, "登录名");

			foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
			{
				dgvRow.Cells[cellIndex].Value = tstbStr.Text;
			}
		}

		// 密码
		private void tsmiSltsPwdD_Click(object sender, EventArgs e)
		{
			int cellIndex = Apq.Windows.Forms.DataGridViewHelper.IndexOfHeader(dataGridView1.Columns, "密码");

			foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
			{
				dgvRow.Cells[cellIndex].Value = tstbStr.Text;
			}
		}
		#endregion

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentRow != null)
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;

					int DBIID = Apq.Convert.ChangeType<int>(dataGridView1.CurrentRow.Cells[Apq.GlobalObject.UILang["实例"]].Value);
					DBS_XSD.DBIRow drDBI = GlobalObject.Lookup.DBI.FindByDBIID(DBIID);

					string strUserId = Apq.Convert.ChangeType<string>(dataGridView1.CurrentRow.Cells[Apq.GlobalObject.UILang["登录名"]].Value);
					string strPwdD = drDBI.PwdD;
					if (string.IsNullOrEmpty(strPwdD))
					{
						strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(drDBI.PwdC, GlobalObject.XmlConfigChain["Crypt", "DESKey"], GlobalObject.XmlConfigChain["Crypt", "DESIV"]);
					}
					string strCS = Apq.ConnectionStrings.Common.GetConnectionString(
						(Apq.Data.Common.DBProduct)drDBI.DBProduct,
						drDBI.IP, drDBI.Port, strUserId, strPwdD);

					DbConnection dbc;
					switch (drDBI.DBProduct)
					{
						case (int)Apq.Data.Common.DBProduct.MySql:
							dbc = new MySql.Data.MySqlClient.MySqlConnection();
							break;
						case (int)Apq.Data.Common.DBProduct.MSSql:
						default:
							dbc = new System.Data.SqlClient.SqlConnection();
							break;
					}

					try
					{
						dbc.ConnectionString = strCS;
						Apq.Data.Common.DbConnectionHelper.Open(dbc);
						tsslTest.Text = drDBI.DBIName + Apq.GlobalObject.UILang["-->连接成功."];
					}
					catch
					{
						tsslTest.Text = drDBI.DBIName + Apq.GlobalObject.UILang["-X-连接失败!"];
					}
					finally
					{
						Apq.Data.Common.DbConnectionHelper.Close(dbc);
					}
				}
				finally
				{
					this.Cursor = Cursors.Default;
				}
			}
		}

		#region IFileLoader 成员

		private string _FileName = string.Empty;
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				Text = value;
				TabText = value;
			}
		}

		public void Open()
		{
			ofd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "ofd_InitialDirectory"];
			if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "ofd_InitialDirectory"] = System.IO.Path.GetDirectoryName(ofd.FileName);

				FileName = ofd.FileName;
				Apq.DBC.XSD xsd = new Apq.DBC.XSD();

				string strCs = File.ReadAllText(ofd.FileName, Encoding.UTF8);
				string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs,
					GlobalObject.XmlConfigChain["Crypt", "DESKey"], GlobalObject.XmlConfigChain["Crypt", "DESIV"]);
				StringReader sr = new StringReader(str);
				xsd.ReadXml(sr);
				xsdDBC.DBC.Merge(xsd.DBC);
				tsslOutInfo.Text = Apq.GlobalObject.UILang["打开文件成功"];
			}
		}

		public void Save()
		{
			if (!string.IsNullOrEmpty(FileName))
			{
				Apq.DBC.XSD xsd = new Apq.DBC.XSD();
				xsd.DBC.Merge(xsdDBC.DBC);
				xsd.DBC.Columns.Remove("ComputerID");
				xsd.DBC.Columns.Remove("PwdC");
				StringWriter sw = new StringWriter();
				xsd.WriteXml(sw, XmlWriteMode.IgnoreSchema);
				Common.SaveCSFile(FileName, sw.ToString());
				tsslOutInfo.Text = Apq.GlobalObject.UILang["保存文件成功"];
			}
		}

		public void SaveAs(string FileName)
		{
			sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if (sfd.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

				this.FileName = sfd.FileName;
				Save();
			}
		}

		#endregion
	}
}