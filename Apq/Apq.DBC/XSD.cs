using System;
namespace Apq.DBC
{


	public partial class XSD
	{
		public partial class DBIDataTable
		{
			/// <summary>
			/// 根据DBIID查找数据行
			/// </summary>
			public DBIRow FindByDBIID(int DBIID)
			{
				foreach (DBIRow dr in this.Rows)
				{
					if (dr.DBIID == DBIID)
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

		public partial class DBProductDataTable
		{
			/// <summary>
			/// 加载默认数据
			/// </summary>
			public void InitDefaultData()
			{
				string[] DBPs = Enum.GetNames(typeof(Apq.Data.Common.DBProduct));
				foreach (string DBP in DBPs)
				{
					this.Rows.Add((int)Enum.Parse(typeof(Apq.Data.Common.DBProduct), DBP), DBP);
				}
			}
		}
	}
}
