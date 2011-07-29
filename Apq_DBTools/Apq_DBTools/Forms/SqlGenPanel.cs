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
using System.Text.RegularExpressions;

namespace Apq_DBTools
{
	public partial class SqlGenPanel : Apq.Windows.Forms.DockForm
	{
		private static int FormCount = 0;

		public SqlGen SqlGen = new SqlGen();

		public SqlGenPanel()
		{
			InitializeComponent();

			SqlGen.SqlGenPanel = this;
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			Text = Apq.GlobalObject.UILang["脚本生成"] + " - " + ++FormCount;
			TabText = Text;

			base.SetUILang(UILang);
		}

		private void SqlGen_Load(object sender, EventArgs e)
		{
			SqlGen.Show(DockDoc);
		}
	}
}