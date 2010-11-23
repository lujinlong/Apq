using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using WeifenLuo.WinFormsUI.Docking;
using ApqDBManager.XSD;
using System.Data.SqlClient;
using System.Net;

namespace ApqDBManager.XmlServersCfg.Forms
{
	public partial class ServersEditor : Apq.Windows.Forms.DockForm, Apq.Editor.IFileLoader
	{
		public ServersEditor()
		{
			InitializeComponent();
		}

		private void ServersEditor_Load(object sender, EventArgs e)
		{
			#region luType
			/// 初始化类型查找表
			Servers.luType.Clear();
			string[] lstTypes = GlobalObject.XmlConfigChain["ApqDBManager.Forms.SolutionExplorer", "RDBTypes"].Split(',');
			foreach (string strType in lstTypes)
			{
				// 获取类型值
				int idxBegin = strType.IndexOf("&") + 1;
				int idxLength = strType.Length - 1 - idxBegin;
				int nType = Apq.Convert.ChangeType<int>(strType.Substring(idxBegin, idxLength), -1);
				string strTypeCaption = strType.Substring(0, idxBegin - 2);

				Servers.luType.AddluTypeRow(nType, strTypeCaption);
			}
			#endregion

			luType.DataSource = Servers.luType;

			Servers.dtServers.TableNewRow += new DataTableNewRowEventHandler(dtServers_TableNewRow);
			treeList1.DataSource = Servers;
			Servers_AddRoot();
			treeList1.BestFitColumns();

			Apq.Windows.Controls.Control.AddImeHandler(this);
			Apq.Xtra.TreeList.Common.AddBehaivor(treeList1);
		}

		private void Servers_AddRoot()
		{
			if (Servers.dtServers.Count < 1)
			{
				Servers.dtServersRow sr = Servers.dtServers.NewdtServersRow();
				sr.ID = 1;
				sr.Name = "全部服务器";
				Servers.dtServers.Rows.Add(sr);
				Servers.dtServers.AcceptChanges();
			}
		}

		void dtServers_TableNewRow(object sender, DataTableNewRowEventArgs e)
		{
			// 获取最大ID,当前行ID
			e.Row["ID"] = Apq.Convert.ChangeType<int>(Servers.dtServers.Compute("Max(ID)", "true")) + 1;
			if (treeList1.FocusedNode != null)
			{
				e.Row["ParentID"] = treeList1.FocusedNode["ID"];
			}

			e.Row["Name"] = "新建服务器[名称]";
			e.Row["UID"] = GlobalObject.XmlConfigChain[this.GetType(), "NewUID"];
			e.Row["SqlPort"] = Apq.Convert.ChangeType<int>(GlobalObject.XmlConfigChain[this.GetType(), "NewSqlPort"], 1433);
			e.Row["FTPPort"] = Apq.Convert.ChangeType<int>(GlobalObject.XmlConfigChain[this.GetType(), "NewFTPPort"], 21);
		}

		//全选
		private void bbiSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in Servers.dtServers.Rows)
			{
				dr["CheckState"] = 1;
			}
			Servers.dtServers.AcceptChanges();
			treeList1.EndUpdate();
		}
		//反选
		private void bbiReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			treeList1.BeginUpdate();
			foreach (DataRow dr in Servers.dtServers.Rows)
			{
				int check = Apq.Convert.ChangeType<int>(dr["CheckState"]);
				if (check == 2 || check == 0)
				{
					check = 1;
				}
				else
				{
					check = 0;
				}
				dr["CheckState"] = check;
			}
			Servers.dtServers.AcceptChanges();
			treeList1.EndUpdate();
		}
		//全部展开
		private void bbiExpandAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (bbiExpandAll.Caption == "全部展开(&D)")
			{
				treeList1.ExpandAll();
				bbiExpandAll.Caption = "全部收起(&D)";
				return;
			}
			if (bbiExpandAll.Caption == "全部收起(&D)")
			{
				treeList1.CollapseAll();
				bbiExpandAll.Caption = "全部展开(&D)";
				return;
			}
		}

		#region treeList1
		// Checked图片
		private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			e.NodeImageIndex = Apq.Convert.ChangeType<int>(e.Node["CheckState"]);
		}

		#region 选中状态
		private void SetCheckedNode(TreeListNode node, bool checkParent, bool checkChildren)
		{
			int Checked = Apq.Convert.ChangeType<int>(node["CheckState"]);
			if (Checked == 2 || Checked == 0) Checked = 1;
			else Checked = 0;

			treeList1.BeginUpdate();
			node["CheckState"] = Checked;
			if (checkParent)
			{
				SetCheckedParentNodes(node, Checked);
			}
			if (checkChildren)
			{
				SetCheckedChildNodes(node, Checked);
			}
			Servers.dtServers.AcceptChanges();
			treeList1.EndUpdate();
		}
		private void SetCheckedChildNodes(TreeListNode node, int Checked)
		{
			foreach (TreeListNode tln in node.Nodes)
			{
				tln["CheckState"] = Checked;
				SetCheckedChildNodes(tln, Checked);
			}
		}
		private void SetCheckedParentNodes(TreeListNode node, int Checked)
		{
			if (node.ParentNode != null)
			{
				node.ParentNode["CheckState"] = Checked;
				SetCheckedParentNodes(node.ParentNode, Checked);
			}
		}
		#endregion

		#region 点击
		private void treeList1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
				if (hInfo.Node != null && hInfo.HitInfoType == HitInfoType.StateImage)//左键点中状态框(Checked)
				{
					short KeyState = Apq.DllImports.User32.GetKeyState(Apq.Utils.VirtKeys.VK_CONTROL);
					bool checkChildren = (KeyState & 0xF0) != 0 || hInfo.Node.ParentNode == null;//按住Ctrl或为根结点
					SetCheckedNode(hInfo.Node, false, checkChildren);
				}
			}
		}

		private void treeList1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				if (Apq.Convert.ChangeType<int>(treeList1.FocusedNode["ID"]) == 1)
				{
					SetCheckedNode(treeList1.FocusedNode, false, true);
				}
				else
				{
					SetCheckedNode(treeList1.FocusedNode, false, false);
				}
			}
		}
		private void treeList1_EditorKeyUp(object sender, KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == Keys.C))
			{
				Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode.GetDisplayText(0));
			}
			#endregion
		}
		private void treeList1_KeyUp(object sender, KeyEventArgs e)
		{
			#region Ctrl&C
			if (e.Control && (e.KeyCode == Keys.C))
			{
				Clipboard.SetData(DataFormats.UnicodeText, treeList1.FocusedNode.GetDisplayText(0));
			}
			#endregion
		}
		#endregion

		private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			if (e.Node != null)
			{
				bsiOutInfo.Caption = e.Node.GetDisplayText("Name");
			}
		}

		private void tsmiAdd_Click(object sender, EventArgs e)
		{
			DataRow dr = Servers.dtServers.NewRow();
			Servers.dtServers.Rows.Add(dr);
		}

		private void tsmiDel_Click(object sender, EventArgs e)
		{
			treeList1.Nodes.Remove(treeList1.FocusedNode);
		}

		private void tsmiTestOpen_Click(object sender, EventArgs e)
		{
			TreeListNode tln = treeList1.FocusedNode;
			if (tln != null && tln.ParentNode != null)
			{
				string strPwdD = Apq.Convert.ChangeType<string>(tln["PwdD"]);
				if (string.IsNullOrEmpty(strPwdD))
				{
					strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(tln["PwdC"].ToString(), GlobalObject.RegConfigChain["Crypt","DESKey"], GlobalObject.RegConfigChain["Crypt","DESIV"]);
				}
				SqlConnection sc = new SqlConnection(string.Format("Data Source={0},{1};User Id={2};Password={3};",
					tln["IPWan1"], tln["SqlPort"], tln["UID"], strPwdD));
				try
				{
					Apq.Data.Common.DbConnectionHelper.Open(sc);
					bsiTest.Caption = tln.GetDisplayText("Name") + "-->连接成功.";
				}
				catch
				{
					bsiTest.Caption = tln.GetDisplayText("Name") + "-X-连接失败!";
				}
				finally
				{
					Apq.Data.Common.DbConnectionHelper.Close(sc);
				}
			}
		}

		private void tsmiFTPTest_Click(object sender, EventArgs e)
		{
			TreeListNode tln = treeList1.FocusedNode;
			if (tln != null && tln.ParentNode != null)
			{
				string Server = Apq.Convert.ChangeType<string>(tln["IPWan1"]);
				int FtpPort = Apq.Convert.ChangeType<int>(tln["FTPPort"], 21);
				string FtpU = Apq.Convert.ChangeType<string>(tln["FTPU"]);
				string FtpP = Apq.Convert.ChangeType<string>(tln["FTPPD"]);
				if (string.IsNullOrEmpty(FtpP))
				{
					FtpP = Apq.Security.Cryptography.DESHelper.DecryptString(tln["FTPPC"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}
				if (string.IsNullOrEmpty(FtpU))
				{
					FtpU = "anonymous";
				}
				if (string.IsNullOrEmpty(FtpP))
				{
					FtpP = "anonymous@";
				}

				FtpWebRequest fwr = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}:{1}", Server, FtpPort)));
				try
				{
					fwr.Credentials = new NetworkCredential(FtpU, FtpP);
					fwr.Method = System.Net.WebRequestMethods.Ftp.PrintWorkingDirectory;
					fwr.GetResponse();
					bsiTest.Caption = tln.GetDisplayText("Name") + "-->FTP连接成功.";
				}
				catch
				{
					bsiTest.Caption = tln.GetDisplayText("Name") + "-X-FTP连接失败!";
				}
			}
		}
		#endregion

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
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.RestoreDirectory = true;
			openFileDialog.Filter = "配置文件(*.xml)|*.xml|所有文件(*.*)|*.*";
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileName = openFileDialog.FileName;
				Servers.dtServers.Clear();
				Servers.dtServers.ReadXml(FileName);
				bsiOutInfo.Caption = "打开成功";
				treeList1.BestFitColumns();
			}
		}

		public void Save()
		{
			if (FileName.Length < 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "配置文件(*.xml)|*.xml|所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					FileName = saveFileDialog.FileName;
				}
				else
				{
					return;
				}
			}

			DataSet ds = Servers.Copy();
			ds.DataSetName = Servers.DataSetName;
			ds.Namespace = Servers.Namespace;
			dt_CryptPwdD(ds.Tables["dtServers"]);
			ds.Tables["dtServers"].WriteXml(FileName);
			bsiOutInfo.Caption = "保存成功";
		}

		private void dt_CryptPwdD(DataTable dt)
		{
			foreach (DataRow dr in dt.Rows)
			{
				if (Apq.Convert.HasMean(dr["IPWan1"]) && Apq.Convert.HasMean(dr["SqlPort"])
					&& Apq.Convert.HasMean(dr["UID"]) && Apq.Convert.HasMean(dr["PwdD"]))
				{
					dr["PwdC"] = Apq.Security.Cryptography.DESHelper.EncryptString(dr["PwdD"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}
				if (Apq.Convert.HasMean(dr["FTPPD"]))
				{
					dr["FTPPC"] = Apq.Security.Cryptography.DESHelper.EncryptString(dr["FTPPD"].ToString(), GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
				}
				dr["CheckState"] = 0;
				dr["Err"] = 0;
				dr["PwdD"] = null;
				dr["FTPPD"] = null;
			}
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			Save();
		}
		#endregion

		private void bbiSlts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (treeList1.FocusedColumn != null)
			{
				string strcn = treeList1.FocusedColumn.FieldName;
				foreach (DataRow dr in Servers.dtServers.Rows)
				{
					if (Apq.Convert.ChangeType<bool>(dr["CheckState"]))
					{
						dr[strcn] = beiStr.EditValue;
					}
				}
				Servers.dtServers.AcceptChanges();
			}
		}

		private void bbiLoadFromDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			// 从数据库加载
			string strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(GlobalObject.XmlConfigChain[this.GetType(), "PwdC"], GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
			string strConn = string.Format("Data Source={0},{1};User Id={2};Password={3};Initial Catalog={4};"
				, GlobalObject.XmlConfigChain[this.GetType(), "IP"]
				, GlobalObject.XmlConfigChain[this.GetType(), "SqlPort"]
				, GlobalObject.XmlConfigChain[this.GetType(), "UID"]
				, strPwdD
				, GlobalObject.XmlConfigChain[this.GetType(), "DBName"]);
			string strSql = string.Format("SELECT * FROM {0}", GlobalObject.XmlConfigChain[this.GetType(), "TFName"]);
			SqlDataAdapter sda = new SqlDataAdapter(strSql, strConn);
			XSD.Servers ds = new Servers();
			sda.Fill(ds.dtServers);
			Servers.Merge(ds);
			Servers.dtServers.AcceptChanges();
			treeList1.BestFitColumns();
			bsiOutInfo.Caption = "加载成功";
		}

		private void bbiSaveToDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DataTable dt = Servers.dtServers.Copy();
			dt_CryptPwdD(dt);
			// 保存到数据库
			string strPwdD = Apq.Security.Cryptography.DESHelper.DecryptString(GlobalObject.XmlConfigChain[this.GetType(), "PwdC"], GlobalObject.RegConfigChain["Crypt", "DESKey"], GlobalObject.RegConfigChain["Crypt", "DESIV"]);
			string strConn = string.Format("Data Source={0},{1};User Id={2};Password={3};Initial Catalog={4};"
				, GlobalObject.XmlConfigChain[this.GetType(), "IP"]
				, GlobalObject.XmlConfigChain[this.GetType(), "SqlPort"]
				, GlobalObject.XmlConfigChain[this.GetType(), "UID"]
				, strPwdD
				, GlobalObject.XmlConfigChain[this.GetType(), "DBName"]);
			string strSql = GlobalObject.XmlConfigChain[this.GetType(), "SPName"];
			using (SqlConnection sc = new SqlConnection(strConn))
			{
				sc.Open();
				SqlCommand scCmd = new SqlCommand(strSql, sc);
				scCmd.CommandType = CommandType.StoredProcedure;
				Apq.Data.Common.DbCommandHelper cmdHelper = new Apq.Data.Common.DbCommandHelper(scCmd);
				foreach (DataRow dr in dt.Rows)
				{
					cmdHelper.AddParameter("ID", dr["ID"]);
					cmdHelper.AddParameter("ParentID", dr["ParentID"]);
					cmdHelper.AddParameter("Name", dr["Name"]);
					cmdHelper.AddParameter("UID", dr["UID"]);
					cmdHelper.AddParameter("PwdC", dr["PwdC"]);
					cmdHelper.AddParameter("Type", dr["Type"]);
					cmdHelper.AddParameter("IPLan", dr["IPLan"]);
					cmdHelper.AddParameter("IPWan1", dr["IPWan1"]);
					cmdHelper.AddParameter("IPWan2", dr["IPWan2"]);
					cmdHelper.AddParameter("FTPPort", dr["FTPPort"]);
					cmdHelper.AddParameter("FTPU", dr["FTPU"]);
					cmdHelper.AddParameter("FTPPC", dr["FTPPC"]);
					cmdHelper.AddParameter("SqlPort", dr["SqlPort"]);
					scCmd.ExecuteNonQuery();
				}
			}
			bsiOutInfo.Caption = "成功保存到数据库";
		}
	}
}