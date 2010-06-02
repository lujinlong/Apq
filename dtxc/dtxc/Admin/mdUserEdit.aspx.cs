using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Data;

namespace dtxc.Admin
{
	public partial class mdUserEdit : CheckAdminPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//绑定对象

			//默认值
			txtRegTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
			txtExpire.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");

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
					long UserID = System.Convert.ToInt64(Request.QueryString["UserID"]);
					using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
					{
						Apq.STReturn stReturn = new Apq.STReturn();
						SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Users_ListOne", SqlConn);
						sda.SelectCommand.CommandType = CommandType.StoredProcedure;
						Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
						dch.AddParameter("rtn", 0, DbType.Int32);
						dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

						dch.AddParameter("UserID", UserID);

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
						txtUserID.Text = ds.Tables[0].Rows[0]["UserID"].ToString();
						txtUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
						ddlSex.SelectedValue = ds.Tables[0].Rows[0]["Sex"].ToString();
						cbStatus.Checked = !System.Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"]);
						cbIsAdmin.Checked = System.Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAdmin"]);
						txtAlipay.Text = ds.Tables[0].Rows[0]["Alipay"].ToString();
						if (!System.Convert.IsDBNull(ds.Tables[0].Rows[0]["Birthday"]))
							txtBirthday.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["Birthday"]).ToString("yyyy-MM-dd");
						txtExpire.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["Expire"]).ToString("yyyy-MM-dd");
						txtRegTime.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["RegTime"]).ToString("yyyy-MM-dd");
						txtIntroUserID.Text = ds.Tables[0].Rows[0]["IntroUserID"].ToString();
						txtIDCard.Text = ds.Tables[0].Rows[0]["IDCard"].ToString();
					}
					break;
			}

			//设置只读
			if (m == "v" || m == "V")
			{
				txtUserID.Enabled = false;
				txtUserName.Enabled = false;
				ddlSex.Enabled = false;
				cbStatus.Enabled = false;
				cbIsAdmin.Enabled = false;
				txtAlipay.Enabled = false;
				txtBirthday.Enabled = false;
				txtExpire.Enabled = false;
				txtIntroUserID.Enabled = false;
				txtIDCard.Enabled = false;
			}
		}
	}
}
