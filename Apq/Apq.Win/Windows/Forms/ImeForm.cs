using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Apq.Windows.Controls;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 基页面
	///		全角问题
	///		//配置文件
	/// </summary>
	/// <remarks>注意:DockForm与此基本相同,修改时应考虑同步修改相应部分</remarks>
	public partial class ImeForm : Form, Apq.Interfaces.IDataShow
	{
		/// <summary>
		/// ImeForm
		/// </summary>
		public ImeForm()
		{
			InitializeComponent();
			this.Size = new System.Drawing.Size(608, 456);	// 子窗口
			//this.Size = new System.Drawing.Size(896, 458);	// 主窗口
		}

		private void ImeForm_Load(object sender, EventArgs e)
		{
			SetUILang(Apq.GlobalObject.UILang);
			//Apq.Windows.Controls.Control.AddImeHandler(this);

			Apq.Interfaces.IDataShow DataShowForm = this as Apq.Interfaces.IDataShow;
			DataShowForm.InitDataBefore();
			DataShowForm.InitData(FormDataSet);
			DataShowForm.LoadData(FormDataSet);
			DataShowForm.ShowData();
		}

		/// <summary>
		/// 设置界面语言(由子类实现具体方法,基类Load事件自动调用)
		/// </summary>
		public virtual void SetUILang(Apq.UILang.UILang UILang)
		{
		}

		#region MainBackThread
		/// <summary>
		/// 主后台线程
		/// </summary>
		public Thread MainBackThread = null;
		#endregion

		#region FormDataSet
		private int _dsCount = 0;
		private DataSet _FormDataSet = null;
		/// <summary>
		/// 获取或设置数据集(存放所有表)
		/// </summary>
		[
		Browsable(true),
		Category("Data"),
		Description("获取或设置数据集"),
		Editor()
		]
		public DataSet FormDataSet
		{
			get
			{
				if (_FormDataSet == null)
				{
					_FormDataSet = new DataSet();
					_FormDataSet.DataSetName = "DataSet_" + ++_dsCount;
				}
				return _FormDataSet;
			}
			set
			{
				_FormDataSet = value;
			}
		}
		#endregion

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public virtual void InitDataBefore()
		{
		}
		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		public virtual void InitData(DataSet ds)
		{
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public virtual void LoadData(DataSet ds)
		{
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public virtual void ShowData()
		{
		}

		#endregion
	}
}