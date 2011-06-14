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
			listBox1.DataSource = ds.dtExplorer.DefaultView;
			ds.dtExplorer.DefaultView.RowFilter = "FileName LIKE '%txt' OR FileName LIKE '%sql'";

			tsbRefresh_Click(tsbRefresh, null);
		}

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			string DirFullPath = GlobalObject.XmlConfigChain[typeof(ApqDBManager.Controls.MainOption.Favorites), "CFolder"];
			if (!System.IO.Directory.Exists(DirFullPath))
			{
				System.IO.Directory.CreateDirectory(DirFullPath);
			}

			listBox1.BeginUpdate();
			ds.dtExplorer.Clear();
			NextID = 1;
			Apq.IO.FileSystem.LoadFiles(ds.dtExplorer, ref NextID, 0, 0, DirFullPath);
			listBox1.EndUpdate();
		}

		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(listBox1.SelectedValue))
			{
				SqlEdit Editor = GlobalObject.MainForm.ActiveMdiChild as SqlEdit;
				if (Editor != null) Editor.SqlEditDoc.LoadFile(listBox1.SelectedValue.ToString());
			}
		}
	}
}
