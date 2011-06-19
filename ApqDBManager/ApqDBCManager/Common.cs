using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ApqDBCManager
{
	public class Common
	{
		/// <summary>
		/// 密码加密(有变更的行)
		/// </summary>
		public static void PwdD2C(DataTable dt)
		{
			DataRow[] drs = dt.Select("1=1", string.Empty, DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
			foreach (DataRow dr in drs)
			{
				if (!Apq.Convert.LikeDBNull(dr["PwdD"]))
				{
					dr["PwdC"] = Apq.Security.Cryptography.DESHelper.EncryptString(Apq.Convert.ChangeType<string>(dr["PwdD"]),
						GlobalObject.XmlConfigChain["Crypt", "DESKey"], GlobalObject.XmlConfigChain["Crypt", "DESIV"]);
				}
			}
		}

		/// <summary>
		/// 密码解密(所有行)
		/// </summary>
		public static void PwdC2D(DataTable dt)
		{
			foreach (DataRow dr in dt.Rows)
			{
				if (!Apq.Convert.LikeDBNull(dr["PwdC"]))
				{
					dr["PwdD"] = Apq.Security.Cryptography.DESHelper.DecryptString(Apq.Convert.ChangeType<string>(dr["PwdC"]),
						GlobalObject.XmlConfigChain["Crypt", "DESKey"], GlobalObject.XmlConfigChain["Crypt", "DESIV"]);
				}
			}
		}
	}
}
