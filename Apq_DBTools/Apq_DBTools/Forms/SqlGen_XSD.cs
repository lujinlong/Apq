using System.Collections.Generic;
namespace Apq_DBTools.Forms
{
	public partial class SqlGen_XSD
	{
		public partial class dic_ObjectTypeDataTable
		{
			public void InitData()
			{
				this.Rows.Add(1, Apq.GlobalObject.UILang["表"]);
				this.Rows.Add(2, Apq.GlobalObject.UILang["存储过程"]);
			}
		}

		public partial class dbv_tableDataTable
		{
			public dbv_tableRow FindByTableName(string SchemaName, string TableName)
			{
				dbv_tableRow dr = null;
				foreach (dbv_tableRow dr1 in Rows)
				{
					if (SchemaName.Equals(dr1.SchemaName, System.StringComparison.OrdinalIgnoreCase)
						&& TableName.Equals(dr1.TableName, System.StringComparison.OrdinalIgnoreCase)
					)
					{
						dr = dr1;
						break;
					}
				}
				return dr;
			}
		}

		public partial class dbv_columnDataTable
		{
			public List<dbv_columnRow> FindByTableName(string SchemaName, string TableName)
			{
				List<dbv_columnRow> drs = new List<dbv_columnRow>();
				foreach (dbv_columnRow dr1 in Rows)
				{
					if (SchemaName.Equals(dr1.SchemaName, System.StringComparison.OrdinalIgnoreCase)
						&& TableName.Equals(dr1.TableName, System.StringComparison.OrdinalIgnoreCase)
					)
					{
						drs.Add(dr1);
					}
				}
				return drs;
			}
		}

		public partial class dbv_procDataTable
		{
			public dbv_procRow FindByProcName(string SchemaName, string ProcName)
			{
				dbv_procRow dr = null;
				foreach (dbv_procRow dr1 in Rows)
				{
					if (SchemaName.Equals(dr1.SchemaName, System.StringComparison.OrdinalIgnoreCase)
						&& ProcName.Equals(dr1.ProcName, System.StringComparison.OrdinalIgnoreCase)
					)
					{
						dr = dr1;
						break;
					}
				}
				return dr;
			}
		}
	}
}
