namespace ApqDBManager.Forms.SrvsMgr {


	public partial class SrvsMgr_XSD
	{
		public partial class ComputerDataTable
		{
			/// <summary>
			/// 根据服务器名查找数据行
			/// </summary>
			/// <param name="SqlID"></param>
			/// <returns></returns>
			public ComputerRow FindByComputerName(string ComputerName)
			{
				foreach (ComputerRow dr in this.Rows)
				{
					if (dr.ComputerName == ComputerName)
					{
						return dr;
					}
				}
				return null;
			}
		}
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
