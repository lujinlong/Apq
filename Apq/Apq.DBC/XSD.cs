using System;
namespace Apq.DBC
{
	public partial class XSD
	{
		public partial class DBIDataTable
		{
			/// <summary>
			/// 根据DBName查找数据行
			/// </summary>
			public DBIRow FindByDBIName(string DBIName)
			{
				foreach (DBIRow dr in this.Rows)
				{
					if (dr.DBIName.ToLower() == DBIName.ToLower())
					{
						return dr;
					}
				}
				return null;
			}
		}

		public partial class DBCDataTable
		{
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
