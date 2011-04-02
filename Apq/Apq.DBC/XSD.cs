namespace Apq.DBC
{


	public partial class XSD
	{
		public partial class SqlInstanceDataTable
		{
			internal SqlInstanceRow FindBySqlID(int p)
			{
				foreach (SqlInstanceRow dr in this.Rows)
				{
					if (dr.SqlID == p)
					{
						return dr;
					}
				}
				return null;
			}
		}
	}
}
