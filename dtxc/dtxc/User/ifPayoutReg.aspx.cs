using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Data;

namespace dtxc.User
{
	public partial class ifPayoutReg : CheckPwdExpirePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//绑定对象

			//默认值

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
					using (SqlConnection SqlConn = new SqlConnection(Apq.DB.GlobalObject.SqlConnectionString))
					{
						Apq.STReturn stReturn = new Apq.STReturn();
						SqlDataAdapter sda = new SqlDataAdapter("dtxc.Apq_Users_ListOne", SqlConn);
						sda.SelectCommand.CommandType = CommandType.StoredProcedure;
						Apq.Data.Common.DbCommandHelper dch = new Apq.Data.Common.DbCommandHelper(sda.SelectCommand);
						dch.AddParameter("rtn", 0, DbType.Int32);
						dch.AddParameter("ExMsg", stReturn.ExMsg, DbType.String, -1);

						dch.AddParameter("UserID", ApqSession.UserID);

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
						txtBalanceMoney.InnerText = ds.Tables[0].Rows[0]["BalanceMoney"].ToString();
					}
					break;
			}

			//设置只读
			if (m == "v" || m == "V")
			{
			}
		}
	}
}
