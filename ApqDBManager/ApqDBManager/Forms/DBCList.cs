using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Security.Cryptography;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace ApqDBManager.Forms
{
	public partial class DBCList : Apq.Windows.Forms.DockForm, Apq.Editor.IFileLoader
	{
		public DBCList()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 连接字符串
		/// </summary>
		protected DataSet ds = new DataSet();

		private void DBCList_Load(object sender, EventArgs e)
		{
			// 数据集结构
			DataTable dt = ds.Tables.Add("DBC");
			dt.Columns.Add("name");
			dt.Columns.Add("value");
			dt.Columns.Add("DBName");
			dt.Columns.Add("ServerName");
			dt.Columns.Add("Mirror");
			dt.Columns.Add("UseTrusted", typeof(bool));
			dt.Columns.Add("UserId");
			dt.Columns.Add("Pwd");
			dt.Columns.Add("Option");

			//dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);

			// 设置绑定
			gridControl1.DataSource = ds;
			gridControl1.DataMember = "DBC";

			Apq.Windows.Controls.Control.AddImeHandler(this);
			Apq.Xtra.Grid.Common.AddBehaivor(gridView1);

		}

		//void dt_RowChanged(object sender, DataRowChangeEventArgs e)
		//{
		//    if (gridView1.FocusedRowHandle > -1)
		//    {
		//        if (Apq.Convert.ChangeType<bool>(e.Row["UseTrusted"]))
		//        {
		//            GridRow gr = gridView1.GetFocusedRow() as GridRow;
		//            gridView1.set
		//        }
		//    }
		//}

		#region IFileLoader 成员

		protected string _FileName = string.Empty;
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
			openFileDialog.Filter = "DBC文件(*.res)|*.res|所有文件(*.*)|*.*";
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileName = openFileDialog.FileName;
				string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
				string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
				string strCs = File.ReadAllText(FileName);
				string str = Apq.Security.Cryptography.DESHelper.DecryptString(strCs, desKey, desIV);
				StringReader sr = new StringReader(str);
				ds.Clear();
				ds.ReadXml(sr);
			}
		}

		public void Save()
		{
			if (FileName.Length < 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "DBC文件(*.res)|*.res|所有文件(*.*)|*.*";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					FileName = saveFileDialog.FileName;
				}
				else
				{
					return;
				}
			}

			string csStr = ds.GetXml();
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(csStr, desKey, desIV);
			File.WriteAllText(FileName, strCs, Encoding.UTF8);
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			Save();
		}

		#endregion

		private void ribeName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
			if (dr != null)
			{
				gridView1.SetFocusedRowCellValue(gridView1.Columns["name"], dr["DBName"]);
			}
		}

		private void ribePwd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
			if (dr != null)
			{
				MessageBox.Show(dr["Pwd"].ToString(), "查看密码");
			}
		}

		private void bbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DBCEdit win = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBCEdit)) as DBCEdit;
			if (win != null)
			{
				DataRow dr = ds.Tables[0].NewRow();
				win.ViewOne(dr, false);
				if (win.ShowDialog(this) == DialogResult.OK)
				{
					ds.Tables[0].Rows.Add(dr);
				}
			}
		}

		/*
		private void bbiOld_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.Filter = "DBC旧版文件(*.res)|*.res|所有文件(*.*)|*.*";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				//FileName = saveFileDialog.FileName;
			}
			else
			{
				return;
			}

			System.Xml.XmlDocument xd = Apq.Xml.XmlDocument.NewXmlDocument();

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				System.Xml.XmlElement xe = xd.CreateElement("cs");
				xd.DocumentElement.AppendChild(xe);

				System.Xml.XmlAttribute xa = xd.CreateAttribute("name");
				xa.Value = dr["name"].ToString();
				xe.Attributes.Append(xa);

				xa = xd.CreateAttribute("value");
				string str = Apq.ConnectionStrings.SQLServer.SqlConnection.GetConnectionString(
					dr["ServerName"].ToString(),
					dr["UserId"].ToString(),
					dr["Pwd"].ToString(),
					dr["DBName"].ToString(),
					dr["Option"].ToString()
				);
				xa.Value = str;
				xe.Attributes.Append(xa);
			}

			string csStr = xd.DocumentElement.OuterXml;
			string desKey = GlobalObject.RegConfigChain["Crypt", "DESKey"];
			string desIV = GlobalObject.RegConfigChain["Crypt", "DESIV"];
			string strCs = Apq.Security.Cryptography.DESHelper.EncryptString(csStr, desKey, desIV);
			File.WriteAllText(saveFileDialog.FileName, strCs, Encoding.UTF8);
		}
		 */

		private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0)
			{
				DBCEdit win = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBCEdit)) as DBCEdit;
				if (win != null)
				{
					DataRow dr = ds.Tables[0].Rows[gridView1.FocusedRowHandle];
					win.ViewOne(dr, false);
					win.ShowDialog(this);
				}
			}
		}

		private void bbiView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0)
			{
				DBCEdit win = Apq.Windows.Forms.SingletonForms.GetInstance(typeof(DBCEdit)) as DBCEdit;
				if (win != null)
				{
					DataRow dr = ds.Tables[0].Rows[gridView1.FocusedRowHandle];
					win.ViewOne(dr, true);
					win.ShowDialog(this);
				}
			}
		}

		private void bbiCs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0)
			{
				DataRow dr = ds.Tables[0].Rows[gridView1.FocusedRowHandle];
				Apq.ConnectionStrings.SQLServer.SqlConnection sc = new Apq.ConnectionStrings.SQLServer.SqlConnection();
				sc.ServerName = dr["ServerName"].ToString();
				sc.DBName = dr["DBName"].ToString();
				sc.Mirror = dr["Mirror"].ToString();
				sc.UseTrusted = Apq.Convert.ChangeType<bool>(dr["UseTrusted"]);
				sc.UserId = dr["UserId"].ToString();
				sc.Pwd = dr["Pwd"].ToString();
				sc.Option = dr["Option"].ToString();
				string strCs = sc.GetConnectionString();
				MessageBox.Show(strCs, "查看连接字符串");
			}
		}

		private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0 && MessageBox.Show("确实要删除当前行?", "删除确认") == DialogResult.OK)
			{
				ds.Tables[0].Rows.RemoveAt(gridView1.FocusedRowHandle);
				ds.Tables[0].AcceptChanges();
			}
		}

		private void bciShowPwd_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ribePwd.PasswordChar = bciShowPwd.Checked ? new char() : '*';
		}
	}
}