using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.Compilation;
using System.Resources;
using System.Collections;
using System.Collections.Specialized;

namespace Apq.Web.Compilation
{
	/// <summary>
	/// 
	/// </summary>
	public static class SqlResourceHelper
	{
		private const string DatabaseLocationKey = "LocalizationDatabasePath";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="virtualPath"></param>
		/// <param name="className"></param>
		/// <param name="cultureName"></param>
		/// <param name="designMode"></param>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public static IDictionary GetResources(string virtualPath, string className, string cultureName, bool designMode, IServiceProvider serviceProvider)
		{
			SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["your_connection_string"].ToString());
			SqlCommand com = new SqlCommand();

			//
			// Build correct select statement to get resource values
			//
			if (!string.IsNullOrEmpty(virtualPath))
			{
				//
				// Get Local resources
				//
				if (string.IsNullOrEmpty(cultureName))
				{
					// default resource values (no culture defined)
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @virtual_path and culture_name is null";
					com.Parameters.AddWithValue("@virtual_path", virtualPath);
				}
				else
				{
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @virtual_path and culture_name = @culture_name ";
					com.Parameters.AddWithValue("@virtual_path", virtualPath);
					com.Parameters.AddWithValue("@culture_name", cultureName);
				}
			}
			else if (!string.IsNullOrEmpty(className))
			{
				//
				// Get Global resources
				// 
				if (string.IsNullOrEmpty(cultureName))
				{
					// default resource values (no culture defined)
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @class_name and culture_name is null";
					com.Parameters.AddWithValue("@class_name", className);
				}
				else
				{
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @class_name and culture_name = @culture_name ";
					com.Parameters.AddWithValue("@class_name", className);
					com.Parameters.AddWithValue("@culture_name", cultureName);
				}
			}
			else
			{
				//
				// Neither virtualPath or className provided, unknown if Local or Global resource
				//
				throw new System.Exception("SqlResourceHelper.GetResources() - virtualPath or className missing from parameters.");
			}


			ListDictionary resources = new ListDictionary();
			try
			{
				com.Connection = con;
				con.Open();
				SqlDataReader sdr = com.ExecuteReader(CommandBehavior.CloseConnection);

				while (sdr.Read())
				{
					string rn = sdr.GetString(sdr.GetOrdinal("resource_name"));
					string rv = sdr.GetString(sdr.GetOrdinal("resource_value"));
					resources.Add(rn, rv);
				}
			}
			catch (System.Exception e)
			{
				throw new System.Exception(e.Message, e);
			}
			finally
			{
				if (con.State == ConnectionState.Open)
				{
					con.Close();
				}
			}

			return resources;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="resource_name"></param>
		/// <param name="virtualPath"></param>
		/// <param name="className"></param>
		/// <param name="cultureName"></param>
		public static void AddResource(string resource_name, string virtualPath, string className, string cultureName)
		{

			SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["your_connection_string"].ToString());
			SqlCommand com = new SqlCommand();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into ASPNET_GLOBALIZATION_RESOURCES (resource_name ,resource_value,resource_object,culture_name ) ");
			sb.Append(" values (@resource_name ,@resource_value,@resource_object,@culture_name) ");
			com.CommandText = sb.ToString();
			com.Parameters.AddWithValue("@resource_name", resource_name);
			com.Parameters.AddWithValue("@resource_value", resource_name + " * DEFAULT * " + (string.IsNullOrEmpty(cultureName) ? string.Empty : cultureName));
			com.Parameters.AddWithValue("@culture_name", (string.IsNullOrEmpty(cultureName) ? SqlString.Null : cultureName));

			string resource_object = "UNKNOWN **ERROR**";
			if (!string.IsNullOrEmpty(virtualPath))
			{
				resource_object = virtualPath;
			}
			else if (!string.IsNullOrEmpty(className))
			{
				resource_object = className;
			}
			com.Parameters.AddWithValue("@resource_object", resource_object);


			try
			{
				com.Connection = con;
				con.Open();
				com.ExecuteNonQuery();
			}
			catch (System.Exception e)
			{
				throw new System.Exception(e.ToString());
			}
			finally
			{
				if (con.State == ConnectionState.Open)
					con.Close();
			}

		}
	}
}
