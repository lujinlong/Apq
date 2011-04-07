namespace Apq.DBC
{


	public partial class XSD
	{
		public partial class SqlInstanceDataTable
		{
			/// <summary>
			/// 根据SqlID查找数据行
			/// </summary>
			/// <param name="SqlID"></param>
			/// <returns></returns>
			public SqlInstanceRow FindBySqlID(int SqlID)
			{
				foreach (SqlInstanceRow dr in this.Rows)
				{
					if (dr.SqlID == SqlID)
					{
						return dr;
					}
				}
				return null;
			}
		}
	}
}
