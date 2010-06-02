using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqDBManager.Forms
{
	public partial class Favorites : Apq.Windows.Forms.DockForm
	{
		public Favorites()
		{
			InitializeComponent();
		}

		private Apq.XSD.Explorer ds = new Apq.XSD.Explorer();
		private long NextID = 1;

		private void Favorites_Load(object sender, EventArgs e)
		{
			listBoxControl1.DataSource = ds.dtExplorer.DefaultView;
			ds.dtExplorer.DefaultView.RowFilter = "FileName LIKE '%txt' OR FileName LIKE '%sql'";

			Apq.Windows.Controls.Control.AddImeHandler(this);
			btnRefresh_ItemClick(sender, null);
		}

		private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string DirFullPath = GlobalObject.XmlConfigChain[typeof(ApqDBManager.Controls.MainOption.Favorites), "CFolder"];
			if (!System.IO.Directory.Exists(DirFullPath))
			{
				System.IO.Directory.CreateDirectory(DirFullPath);
			}

			listBoxControl1.BeginUpdate();
			ds.dtExplorer.Clear();
			NextID = 1;
			Apq.IO.FileSystem.LoadFiles(ds.dtExplorer, ref NextID, 0, 0, DirFullPath);
			listBoxControl1.EndUpdate();
		}

		private void listBoxControl1_DoubleClick(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(listBoxControl1.SelectedValue))
			{
				SqlEdit Editor = GlobalObject.MainForm.ActiveMdiChild as SqlEdit;
				if (Editor != null) Editor.LoadFile(listBoxControl1.SelectedValue.ToString());
			}
		}
	}
}
