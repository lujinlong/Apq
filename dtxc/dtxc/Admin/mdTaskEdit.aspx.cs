using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace dtxc.Admin
{
	public partial class mdTaskEdit : CheckAdminPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//绑定对象

			//默认值
			txtBTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
			txtETime.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");

			string m = Request.QueryString["m"];
			switch (m)
			{
				//添加
				case "a":
				case "A":
					break;

				//查看
				//修改
				default:
					//获取数据
					long TaskID = System.Convert.ToInt64(Request.QueryString["TaskID"]);
					using (SqlConnection SqlConn = new SqlConnection(Apq.DB.Common.GetSqlConnectionString("SqlConnectionString2")))
					{
						Apq.STReturn stReturn = new Apq.STReturn();
						SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Task_ListOne", SqlConn);
						sda.SelectCommand.CommandType = CommandType.StoredProcedure;
						Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
						dch.AddParameter("rtn", 0, DbType.Int32);
						dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

						dch.AddParameter("TaskID", TaskID);

						sda.SelectCommand.Parameters["rtn"].Direction = ParameterDirection.ReturnValue;
						sda.SelectCommand.Parameters["ExMsg"].Direction = ParameterDirection.InputOutput;

						SqlConn.Open();
						sda.Fill(ds);

						stReturn.NReturn = System.Convert.ToInt32(sda.SelectCommand.Parameters["rtn"].Value);
						stReturn.ExMsg = sda.SelectCommand.Parameters["ExMsg"].Value.ToString();
						stReturn.FNReturn = ds.Tables[0];

						sda.Dispose();
						SqlConn.Close();
					}

					if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						//页面赋值
						txtTaskID.Text = ds.Tables[0].Rows[0]["TaskID"].ToString();
						txtTaskName.Text = ds.Tables[0].Rows[0]["TaskName"].ToString();
						cbNeedChangeIP.Checked = System.Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedChangeIP"]);
						cbIsAutoStart.Checked = System.Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAutoStart"]);
						txtBTime.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["BTime"]).ToString("yyyy-MM-dd");
						txtETime.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["ETime"]).ToString("yyyy-MM-dd");
						txtTaskContent.Text = ds.Tables[0].Rows[0]["TaskContent"].ToString();
						txtAddinID.Text = ds.Tables[0].Rows[0]["AddinID"].ToString();
						txtTaskMoney.Text = ds.Tables[0].Rows[0]["TaskMoney"].ToString();
						txtPrice.Text = ds.Tables[0].Rows[0]["Price"].ToString();
						txtParentPrice.Text = ds.Tables[0].Rows[0]["ParentPrice"].ToString();
					}
					break;
			}

			//设置只读
			if (m == "v" || m == "V")
			{
				txtTaskID.Enabled = false;
				txtTaskName.Enabled = false;
				cbNeedChangeIP.Enabled = false;
				cbIsAutoStart.Enabled = false;
				txtBTime.Enabled = false;
				txtETime.Enabled = false;
				txtTaskContent.Enabled = false;
				txtAddinID.Enabled = false;
				txtTaskMoney.Enabled = false;
				txtPrice.Enabled = false;
				txtParentPrice.Enabled = false;
				txtBTime.Enabled = false;
				txtETime.Enabled = false;
			}
		}
	}
}
