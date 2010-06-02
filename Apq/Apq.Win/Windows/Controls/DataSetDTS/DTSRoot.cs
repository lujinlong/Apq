using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace Apq.Windows.Controls.DataSetDTS
{
	/// <summary>
	/// DataSet 导入导出步骤控件基类
	/// </summary>
	public partial class DTSRoot : WizardStep
	{
		[Obsolete("仅为能打开设计视图而保留")]
		private DTSRoot()
		{
			InitializeComponent();
		}

		private DataSet _ds = new DataSet();
		/// <summary>
		/// 获取或设置 DataSet
		/// </summary>
		public DataSet ds
		{
			get { return _ds; }
			set { _ds = value; }
		}

		private Direction _DTSDirection = Direction.Export;
		/// <summary>
		/// 获取或设置 DTS方向
		/// </summary>
		public Direction DTSDirection
		{
			get { return _DTSDirection; }
			set { _DTSDirection = value; }
		}

		private DataTableMappingCollection _DataSetMapping = new DataTableMappingCollection();
		/// <summary>
		/// 获取或设置 DataSet 映射
		/// </summary>
		public DataTableMappingCollection DataSetMapping
		{
			get { return _DataSetMapping; }
			set { _DataSetMapping = value; }
		}

		/// <summary>
		/// DTSRoot
		/// </summary>
		/// <param name="ds">DataSet</param>
		/// <param name="DataSetMapping">DataSet 映射</param>
		/// <param name="Direct">方向</param>
		public DTSRoot(DataSet ds, Direction Direct, DataTableMappingCollection DataSetMapping)
		{
			if (ds != null)
			{
				this.ds = ds;
			}
			DTSDirection = Direct;
			this.DataSetMapping = DataSetMapping;

			InitializeComponent();
		}
	}
}
