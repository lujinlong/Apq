using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Apq.Web.UI
{
	/// <summary>
	/// 数据
	/// </summary>
	public class BaseDataPage : System.Web.UI.Page
	{
		/// <summary>
		/// 数据集
		/// </summary>
		protected DataSet ds = new DataSet();

		/// <summary>
		/// 数据库对象
		/// </summary>
		protected Database DB = DatabaseFactory.CreateDatabase();

		/// <summary>
		/// 语言版本
		/// </summary>
		protected string Lang;

		/// <summary>
		/// BaseDataPage
		/// </summary>
		public BaseDataPage()
		{
			Load += new EventHandler(Page_Load);
		}

		/// <summary>
		/// Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, EventArgs e)
		{
			Lang = base.Request.Cookies["lang"] == null ? "en" : base.Request.Cookies["lang"].Value;
		}
	}
}
