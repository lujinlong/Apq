using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Apq.Interfaces
{
	/// <summary>
	/// 数据显示
	/// </summary>
	public interface IDataShow
	{
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		void InitDataBefore();

		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		void InitData(DataSet ds);

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		void LoadData(DataSet ds);

		/// <summary>
		/// 显示数据
		/// </summary>
		void ShowData();
	}
}
