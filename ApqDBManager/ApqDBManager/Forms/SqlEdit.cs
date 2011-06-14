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
using ApqDBManager.Controls;
using ICSharpCode.TextEditor.Document;
using System.Text.RegularExpressions;
using ApqDBManager.Forms;

namespace ApqDBManager
{
	/// <summary>
	/// 可作为 后台多线程示例
	/// </summary>
	public partial class SqlEdit : Apq.Windows.Forms.DockForm
	{
		public SqlEdit()
		{
			InitializeComponent();
		}

		public SqlOut SqlOut = new SqlOut();
		public SqlEditDoc SqlEditDoc = new SqlEditDoc();

		private void SqlEdit_Load(object sender, EventArgs e)
		{
			SqlEditDoc.Show(dockPanel1);
			//SqlOut.Show(dockPanel1);
		}

		private void SqlEdit_Activated(object sender, EventArgs e)
		{
			// 设置服务器和错误列表
			GlobalObject.SolutionExplorer.SetServers(SqlEditDoc._Sqls);
			GlobalObject.ErrList.Set_ErrList(SqlEditDoc.dsUI);
		}

		private void SqlEdit_Deactivate(object sender, EventArgs e)
		{
			GlobalObject.SolutionExplorer.SaveState2XSD();
		}
	}
}