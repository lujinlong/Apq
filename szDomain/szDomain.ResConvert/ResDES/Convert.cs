using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using Hx2.WebMaster;
using Hx2.WebMaster.ConnKeyResourceManager.Components;
using Hx2.WebMaster.DataAccess;
using System.Collections;

namespace szDomain.ResDES
{
	/// <summary>
	/// 加密文件转换
	/// </summary>
	public class Convert
	{
		#region ToXmlNode
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static XmlNode ToXmlNode()
		{
			return null;
		}
		#endregion

		#region ToDataTable
		/// <summary>
		/// 转换为 DataTable
		/// </summary>
		/// <returns></returns>
		public static DataTable ToDataTable()
		{
			ConnectionConfiguration cc = ConnectionConfiguration.getConfig();
			return ToDataTable(cc);
		}

		/// <summary>
		/// 转换为 DataTable
		/// </summary>
		/// <param name="cc">连接库对象</param>
		/// <returns></returns>
		public static DataTable ToDataTable(ConnectionConfiguration cc)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ID", typeof(int));
			dt.Columns.Add("ParentID", typeof(int));
			dt.Columns.Add("Name");
			dt.Columns.Add("Type", typeof(int));
			dt.Columns.Add("ConnectionString");
			dt.Constraints.Add("PK_Table1", dt.Columns["ID"], true);

			// 全部服务器根
			DataRow drRoot = dt.NewRow();
			drRoot["ID"] = 0;
			drRoot["Name"] = "全部服务器";
			drRoot["Type"] = 4;

			dt.Rows.Add(drRoot);

			int MaxID = 0;

			// 区
			for (int i = 0; i < cc.ConnKeyRecordSet.GameAreaList.Count; i++)
			{
				GameAreaNC gaNC = cc.ConnKeyRecordSet.GameAreaList[i];
				ConnectionString cs = cc.ConnKeyRecordSet.ConnectionStringList[gaNC.ConnKey] as ConnectionString;
				if (cs != null)
				{
					int AreaID = ++MaxID;
					DataRow drAera = dt.NewRow();
					drAera["ID"] = AreaID;
					drAera["ParentID"] = 0;
					drAera["Name"] = gaNC.Name;
					drAera["Type"] = 1;
					drAera["ConnectionString"] = cs.ConnString;

					dt.Rows.Add(drAera);

					// 游戏
					for (int j = 0; j < gaNC.GameServerList.Count; j++)
					{
						GameServerNC gsNC = gaNC.GameServerList[j];
						cs = cc.ConnKeyRecordSet.ConnectionStringList[gsNC.ConnKey] as ConnectionString;
						DataRow drServer = dt.NewRow();
						drServer["ID"] = ++MaxID;
						drServer["ParentID"] = AreaID;
						drServer["Name"] = gsNC.Name;
						drServer["Type"] = 2;
						drServer["ConnectionString"] = cs.ConnString;

						dt.Rows.Add(drServer);
					}
				}
			}


			// 其它
			for (int i = 0; i < cc.ConnKeyRecordSet.ElseConnectionStringList.Count; i++)
			{
				ConnectionString cs = cc.ConnKeyRecordSet.ElseConnectionStringList[i];
				DataRow dr = dt.NewRow();
				dr["ID"] = ++MaxID;
				dr["ParentID"] = 0;
				dr["Name"] = cs.CNName;
				dr["Type"] = 3;
				dr["ConnectionString"] = cs.ConnString;

				dt.Rows.Add(dr);
			}

			return dt;
		}
		#endregion
	}
}
