using System;
namespace Apq.DBC
{
	public partial class XSD
	{
		public partial class DBCDataTable
		{
			/// <summary>
			/// 根据DBCID查找数据行
			/// </summary>
			public DBCRow FindByDBCID(int DBCID)
			{
				foreach (DBCRow dr in this.Rows)
				{
					if (dr.DBCID == DBCID)
					{
						return dr;
					}
				}
				return null;
			}

			/// <summary>
			/// 根据DBName查找数据行
			/// </summary>
			public DBCRow FindByDBName(string DBName)
			{
				foreach (DBCRow dr in this.Rows)
				{
					if (dr.DBName.ToLower() == DBName.ToLower())
					{
						return dr;
					}
				}
				return null;
			}
		}
	}
}
