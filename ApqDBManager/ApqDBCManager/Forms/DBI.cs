using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using System.Data.Common;
using Apq.TreeListView;

namespace ApqDBCManager.Forms
{
	public partial class DBI : Apq.Windows.Forms.DockForm
	{
		public DBI()
		{
			InitializeComponent();
		}

		private TreeListViewHelper tlvHelper;

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["数据库列表"];
			this.TabText = this.Text;

			acSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			//acSave.ToolTipText = Apq.GlobalObject.UILang["保存"];
			acSelectAll.Text = Apq.GlobalObject.UILang["全选(&A)"];
			//acSelectAll.ToolTipText = Apq.GlobalObject.UILang["全选"];
			acReverse.Text = Apq.GlobalObject.UILang["反选(&V)"];
			//acReverse.ToolTipText = Apq.GlobalObject.UILang["反选"];
			acExpandAll.Text = Apq.GlobalObject.UILang["全部收起(&D)"];
			//acExpandAll.ToolTipText = Apq.GlobalObject.UILang["全部收起"];
			tssbSlts.Text = Apq.GlobalObject.UILang["批量设置(&E)"];
			tsmiSltsUserId.Text = Apq.GlobalObject.UILang["登录名"];
			tsmiSltsPwdD.Text = Apq.GlobalObject.UILang["密码"];
			tsmiSltsPort.Text = Apq.GlobalObject.UILang["端口"];
			acCreateFile.Text = Apq.GlobalObject.UILang["生成文件(&G)"];

			tsmiTestOpen.Text = Apq.GlobalObject.UILang["测试(&T)"];
			tsmiAdd.Text = Apq.GlobalObject.UILang["添加(&A)"];
			tsmiDel.Text = Apq.GlobalObject.UILang["删除(&D)"];

			columnHeader1.Text = Apq.GlobalObject.UILang["名称"];
			columnHeader2.Text = Apq.GlobalObject.UILang["服务器"];
			columnHeader7.Text = Apq.GlobalObject.UILang["DBMS"];
			columnHeader3.Text = Apq.GlobalObject.UILang["登录名"];
			columnHeader4.Text = Apq.GlobalObject.UILang["密码"];
			columnHeader5.Text = Apq.GlobalObject.UILang["IP"];
			columnHeader6.Text = Apq.GlobalObject.UILang["端口"];

			sfd.Filter = Apq.GlobalObject.UILang["DBC文件(*.res)|*.res"];
		}

		private void DBI_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsbSave.Image = System.Drawing.Image.FromFile(@"Res\png\File\Save.png");
			#endregion
		}

		private void DBI_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		#region treeListView1

		private void treeListView1_BeforeLabelEdit(object sender, TreeListViewBeforeLabelEditEventArgs e)
		{
			if (e.Item.ListView.Columns[e.ColumnIndex].Text == Apq.GlobalObject.UILang["服务器"])
			{
				ComboBox cb = new ComboBox();
				cb.DropDownStyle = ComboBoxStyle.DropDownList;
				cb.DisplayMember = "ComputerName";
				cb.ValueMember = "ComputerName";
				cb.DataSource = GlobalObject.Lookup.Computer;

				e.Editor = cb;
			}

			if (e.Item.ListView.Columns[e.ColumnIndex].Text == Apq.GlobalObject.UILang["DBMS"])
			{
				ComboBox cb = new ComboBox();
				cb.DropDownStyle = ComboBoxStyle.DropDownList;
				cb.DisplayMember = "DBProductName";
				cb.ValueMember = "DBProductName";
				cb.DataSource = GlobalObject.Lookup.DBProduct;

				e.Editor = cb;
			}
		}

		private void treeListView1_AfterLabelEdit(object sender, TreeListViewLabelEditEventArgs e)
		{
			ColumnHeader ch = e.Item.ListView.Columns[e.ColumnIndex];
			DataColumnMapping dcm = tlvHelper.TableMapping.ColumnMappings[ch.Text];

			long DBIID = Apq.Convert.ChangeType<long>(e.Item.SubItems[e.Item.ListView.Columns.Count].Text);
			DataRow[] drs = GlobalObject.Lookup.DBI.Select("DBIID = " + DBIID);
			if (drs.Length > 0)
			{
				drs[0][dcm.DataSetColumn] = e.Label;
			}
		}

		#region 批量设置
		// 登录名
		private void tsmiSltsUserId_Click(object sender, EventArgs e)
		{
			int subIndex = Apq.Windows.Forms.ListViewHelper.IndexOfHeader(treeListView1.Columns, Apq.GlobalObject.UILang["登录名"]);

			foreach (TreeListViewItem node in treeListView1.CheckedItems)
			{
				long DBIID = Apq.Convert.ChangeType<long>(node.SubItems[treeListView1.Columns.Count].Text);
				DataRow[] drs = GlobalObject.Lookup.DBI.Select("DBIID = " + DBIID);
				if (drs.Length > 0)
				{
					try
					{
						drs[0]["UserId"] = tstbStr.Text;
						node.SubItems[subIndex].Text = tstbStr.Text;
					}
					catch { }
				}
			}
		}

		// 密码
		private void tsmiSltsPwdD_Click(object sender, EventArgs e)
		{
			int subIndex = Apq.Windows.Forms.ListViewHelper.IndexOfHeader(treeListView1.Columns, Apq.GlobalObject.UILang["密码"]);

			foreach (TreeListViewItem node in treeListView1.CheckedItems)
			{
				long DBIID = Apq.Convert.ChangeType<long>(node.SubItems[treeListView1.Columns.Count].Text);
				DataRow[] drs = GlobalObject.Lookup.DBI.Select("DBIID = " + DBIID);
				if (drs.Length > 0)
				{
					try
					{
						drs[0]["PwdD"] = tstbStr.Text;
						node.SubItems[subIndex].Text = tstbStr.Text;
					}
					catch { }
				}
			}
		}

		// 端口
		private void tsmiSltsPort_Click(object sender, EventArgs e)
		{
			int subIndex = Apq.Windows.Forms.ListViewHelper.IndexOfHeader(treeListView1.Columns, Apq.GlobalObject.UILang["端口"]);

			foreach (TreeListViewItem node in treeListView1.CheckedItems)
			{
				long DBIID = Apq.Convert.ChangeType<long>(node.SubItems[treeListView1.Columns.Count].Text);
				DataRow[] drs = GlobalObject.Lookup.DBI.Select("DBIID = " + DBIID);
				if (drs.Length > 0)
				{
					try
					{
						drs[0]["Port"] = tstbStr.Text;
						node.SubItems[subIndex].Text = tstbStr.Text;
					}
					catch { }
				}
			}
		}
		#endregion

		private void tsmiAdd_Click(object sender, EventArgs e)
		{
			TreeListViewItem node = new TreeListViewItem();
			DataRow dr = GlobalObject.Lookup.DBI.NewRow();

			if (treeListView1.FocusedItem != null && treeListView1.FocusedItem.Selected)
			{
				treeListView1.FocusedItem.Items.Add(node);

				long DBIID = Apq.Convert.ChangeType<long>(treeListView1.FocusedItem.SubItems[treeListView1.Columns.Count].Text);
				dr["ParentID"] = DBIID;
			}
			else
			{
				treeListView1.Items.Add(node);
			}

			GlobalObject.Lookup.DBI.Rows.Add(dr);
			tlvHelper.BindRow(node, dr);
		}

		private void tsmiDel_Click(object sender, EventArgs e)
		{
			if (treeListView1.FocusedItem != null)
			{
				long DBIID = Apq.Convert.ChangeType<long>(treeListView1.FocusedItem.SubItems[treeListView1.Columns.Count].Text);
				DataRow[] drs = GlobalObject.Lookup.DBI.Select("DBIID = " + DBIID);
				if (drs.Length > 0)
				{
					// 这里不能使用Remove方法
					drs[0].Delete();
				}

				treeListView1.BeginUpdate();
				treeListView1.Items.Remove(treeListView1.FocusedItem);
				treeListView1.EndUpdate();
			}
		}
		#endregion

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			tlvHelper = new TreeListViewHelper(treeListView1);
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["名称"], "DBIName");
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["服务器"], "ComputerName");
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["DBMS"], "DBMS");
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["登录名"], "UserId");
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["密码"], "PwdD");
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["IP"], "IP");
			tlvHelper.TableMapping.ColumnMappings.Add(Apq.GlobalObject.UILang["端口"], "Port");
			tlvHelper.Key = "DBIID";
			tlvHelper.HiddenColNames = new List<string>(new string[] { "DBIID" });

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
			// 绑定到TreeListView
			treeListView1.Items.Clear();
			if (GlobalObject.Lookup.DBI.Rows.Count > 0)
			{
				tlvHelper.BindDataTable(GlobalObject.Lookup.DBI);
				treeListView1.ExpandAll();
			}

			tsslOutInfo.Text = Apq.GlobalObject.UILang["加载成功"];
		}
		#endregion

		//生成文件
		private void tsbCreateFile_Click(object sender, EventArgs e)
		{
			sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if (sfd.ShowDialog(this) == DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

				DBS_XSD xsd = new DBS_XSD();
				xsd.DBI.Merge(GlobalObject.Lookup.DBI);
				xsd.DBI.Columns.Remove("PwdD");
				xsd.DBI.Columns.Remove("ComputerName");
				xsd.DBI.WriteXml(sfd.FileName);

				tsslOutInfo.Text = Apq.GlobalObject.UILang["保存文件成功"];
			}
		}

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				treeListView1.EndUpdate();
				TreeListViewItem tln = treeListView1.FocusedItem;
				long DBIID = Apq.Convert.ChangeType<long>(treeListView1.FocusedItem.SubItems[treeListView1.Columns.Count].Text);
				DataRow[] drs = GlobalObject.Lookup.DBI.Select("DBIID = " + DBIID);

				if (drs.Length > 0 && tln != null && tln.Parent != null)
				{
					string strPwdD = Apq.Convert.ChangeType<string>(drs[0]["PwdD"]);
					if (string.IsNullOrEmpty(strPwdD))
					{
						strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(drs[0]["PwdC"].ToString(), GlobalObject.XmlConfigChain["Crypt", "DESKey"], GlobalObject.XmlConfigChain["Crypt", "DESIV"]);
					}

					string strServerName = Apq.Convert.ChangeType<string>(drs[0]["IP"]);
					if (Apq.Convert.ChangeType<int>(drs[0]["Port"]) > 0)
					{
						strServerName += "," + Apq.Convert.ChangeType<int>(drs[0]["Port"]);
					}
					string strConn = "";
					SqlConnection sc = new SqlConnection(strConn);
					try
					{
						Apq.Data.Common.DbConnectionHelper.Open(sc);
						tsslTest.Text = drs[0]["SqlName"] + "-->连接成功.";
					}
					catch
					{
						tsslTest.Text = drs[0]["SqlName"] + "-X-连接失败!";
					}
					finally
					{
						Apq.Data.Common.DbConnectionHelper.Close(sc);
					}
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void acSave_Execute(object sender, EventArgs e)
		{
			treeListView1.EndUpdate();
			GlobalObject.Lookup_Save();
			tsslOutInfo.Text = Apq.GlobalObject.UILang["保存成功"];
		}

		private void acSelectAll_Execute(object sender, EventArgs e)
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				Apq.TreeListView.TreeListViewHelper.SetCheckedNode(root, true, false, true);
			}
		}

		private void acReverse_Execute(object sender, EventArgs e)
		{
			foreach (TreeListViewItem root in treeListView1.Items)
			{
				Apq.TreeListView.TreeListViewHelper.ChgCheckedNode(root, false, true);
			}
		}

		private void acExpandAll_Execute(object sender, EventArgs e)
		{
			if (acExpandAll.Text == Apq.GlobalObject.UILang["全部展开(&D)"])
			{
				treeListView1.ExpandAll();
				acExpandAll.Text = Apq.GlobalObject.UILang["全部收起(&D)"];
				acExpandAll.ToolTipText = Apq.GlobalObject.UILang["全部收起"];
				return;
			}
			if (acExpandAll.Text == Apq.GlobalObject.UILang["全部收起(&D)"])
			{
				treeListView1.CollapseAll();
				acExpandAll.Text = Apq.GlobalObject.UILang["全部展开(&D)"];
				acExpandAll.ToolTipText = Apq.GlobalObject.UILang["全部展开"];
				return;
			}
		}

		private void acCreateFile_Execute(object sender, EventArgs e)
		{
			treeListView1.EndUpdate();

			DBS_XSD xsd = new DBS_XSD();
			xsd.DBI.Merge(GlobalObject.Lookup.DBI);
			xsd.DBI.Columns.Remove("PwdD");
			xsd.DBI.Columns.Remove("ComputerName");
			xsd.DBI.Columns.Remove("DBMS");

			sfd.InitialDirectory = GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"];
			if(sfd.ShowDialog(this)== DialogResult.OK)
			{
				GlobalObject.XmlConfigChain[this.GetType(), "sfd_InitialDirectory"] = System.IO.Path.GetDirectoryName(sfd.FileName);

				xsd.DBI.WriteXml(sfd.FileName, XmlWriteMode.IgnoreSchema);
				tsslOutInfo.Text = Apq.GlobalObject.UILang["保存文件成功"];
			}
		}
	}
}