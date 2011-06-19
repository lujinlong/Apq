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

		private void DBC_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsbOpenFile.Image = System.Drawing.Image.FromFile(@"Res\png\File\Open.png");
			this.tsbSave.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
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

		private DBS_XSD xsd = new DBS_XSD();

		// 生成数据库连接文件
		void tsmiCreatCSFile_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsb = sender as ToolStripMenuItem;
			sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if (tsb != null && sfd.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

				int nDBCUseType = Apq.Convert.ChangeType<int>(tsb.Tag);
				DataSet ds = new DataSet("XSD");
				ds.Namespace = _DBS_XSD.Namespace;
				DataTable dt = _DBS_XSD.DBC.Copy();
				ds.Tables.Add(dt);
				dt.Columns.Add("ServerName");
				dt.Columns.Remove("PwdC");

				for (int i = dt.Rows.Count - 1; i >= 0; i--)
				{
					if (Apq.Convert.ChangeType<int>(dt.Rows[i]["DBCUseType"]) != nDBCUseType)
					{
						dt.Rows.RemoveAt(i);
						continue;
					}

					// 计算ServerName
					DBS_XSD.DBIRow DBI = GlobalObject.Lookup.DBI.FindByDBIID(Apq.Convert.ChangeType<int>(dt.Rows[i]["DBIID"]));
					dt.Rows[i]["ServerName"] = DBI.IP;
					if (DBI != null && DBI.Port > 0)
					{
						dt.Rows[i]["ServerName"] += "," + DBI.Port;
					}
				}
				string csStr = ds.GetXml();
				string desKey = GlobalObject.XmlConfigChain["Crypt", "DESKey"];
				string desIV = GlobalObject.XmlConfigChain["Crypt", "DESIV"];
				string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(csStr, desKey, desIV);
				File.WriteAllText(sfd.FileName, strCs, Encoding.UTF8);
				tsslOutInfo.Text = Apq.GlobalObject.UILang["保存文件成功"];
			}
		}

		private void tsbOpenFile_Click(object sender, EventArgs e)
		{
			if (ofd.ShowDialog(this) == DialogResult.OK)
			{
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
					Apq.DBC.XSD _ds = new Apq.DBC.XSD();
					_ds.DBC.ReadXml(FileName);

					xsd.Clear();
					xsd.DBC.Merge(_ds.DBC);
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
			#endregion

			bsDBC.DataSource = xsd;
		}

		#endregion

		//保存
		private void tsbSave_Click(object sender, EventArgs e)
		{
			Apq.DBC.XSD _ds = new Apq.DBC.XSD();
			_ds.DBC.Merge(xsd.DBC);

			// 密码加密
			Common.PwdD2C(_ds.DBC);

			if (!string.IsNullOrEmpty(FileName))
			{
				_ds.DBC.WriteXml(FileName, XmlWriteMode.IgnoreSchema);
				tsslOutInfo.Text = Apq.GlobalObject.UILang["保存成功"];
			}
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
			LoadData(FormDataSet);
		}

		public void Save()
		{
			sfd.InitialDirectory = System.IO.Path.GetDirectoryName(FileName);
			if (string.IsNullOrEmpty(FileName))
			{
				sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
				if (ofd.ShowDialog(this) == DialogResult.OK)
				{
					GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

					FileName = ofd.FileName;
				}
			}

			SaveToFile();
		}

		public void SaveAs(string FileName)
		{
			ofd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if (ofd.ShowDialog(this) == DialogResult.OK)
			{
				FileName = ofd.FileName;
			}

			SaveToFile();
		}

		private void SaveToFile()
		{
			if (!string.IsNullOrEmpty(FileName))
			{
				Apq.DBC.XSD xsd = new Apq.DBC.XSD();
				xsd.DBC.Merge(_DBS_XSD.DBC);
				xsd.DBC.WriteXml(FileName, XmlWriteMode.IgnoreSchema);
			}
		}

		#endregion
	}
}