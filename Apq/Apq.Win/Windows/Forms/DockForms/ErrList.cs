using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Apq.Windows.Forms.DockForms
{
	/// <summary>
	/// 错误列表
	/// </summary>
	public partial class ErrList : DockForm
	{
		/// <summary>
		/// 获取数据集
		/// </summary>
		public ErrList_XSD ErrXSD
		{
			get
			{
				return _xsd;
			}
		}

		private List<string> _lstRowFilterType = new List<string>();
		private Apq.Collections.IListT<string> LstRowFilterType = null;
		private List<string> _lstRowFilterSeverity = new List<string>();
		private Apq.Collections.IListT<string> LstRowFilterSeverity = null;

		/// <summary>
		/// 错误列表
		/// </summary>
		public ErrList()
		{
			InitializeComponent();

			DataGridViewHelper.SetDefaultStyle(dataGridView1);
			DataGridViewHelper.AddBehaivor(dataGridView1);
		}

		/// <summary>
		/// 设置界面语言值
		/// </summary>
		/// <param name="UILang"></param>
		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["错误列表"];
			this.TabText = this.Text;

			#region 工具栏
			tsbType.Text = Apq.GlobalObject.UILang["类型"];
			tsmiTypeError.Text = Apq.GlobalObject.UILang["错误"];
			tsmiTypeWarn.Text = Apq.GlobalObject.UILang["警告"];
			tsmiTypeTrace.Text = Apq.GlobalObject.UILang["跟踪"];
			tsmiTypeInfo.Text = Apq.GlobalObject.UILang["信息"];

			tsbSeverity.Text = Apq.GlobalObject.UILang["严重度"];
			#endregion

			#region 列头
			col_InTime.HeaderText = Apq.GlobalObject.UILang["时间"];
			colType.HeaderText = Apq.GlobalObject.UILang["类型"];
			colSeverity.HeaderText = Apq.GlobalObject.UILang["严重度"];
			colTitle.HeaderText = Apq.GlobalObject.UILang["标题"];
			colMsg.HeaderText = Apq.GlobalObject.UILang["内容"];
			colAlarmGroupID.HeaderText = Apq.GlobalObject.UILang["报警"];
			colState.HeaderText = Apq.GlobalObject.UILang["状态"];
			#endregion

			base.SetUILang(UILang);
		}

		private void ErrorList_Load(object sender, EventArgs e)
		{
			//tsmiTypeError_Click(sender, e);
			//tsmiTypeWarn_Click(sender, e);
			//tsmiTypeTrace_Click(sender, e);
			//tsmiTypeInfo_Click(sender, e);
			//tsmiSeverity15_Click(sender, e);
			//tsmiSeverity20_Click(sender, e);
			//tsmiSeverity21_Click(sender, e);
		}

		void LstRowFilter_ListChanged(object sender, ListChangedEventArgs e)
		{
			string strRowFilterType = string.Join(" OR ", _lstRowFilterType);
			string strRowFilterSeverity = string.Join(" OR ", _lstRowFilterSeverity);

			List<string> lstRowFilter = new List<string>();
			if (!string.IsNullOrWhiteSpace(strRowFilterType))
			{
				lstRowFilter.Add("(" + strRowFilterType + ")");
			}
			if (!string.IsNullOrWhiteSpace(strRowFilterSeverity))
			{
				lstRowFilter.Add("(" + strRowFilterSeverity + ")");
			}
			_dv.RowFilter = string.Join(" AND ", lstRowFilter);
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
			#region 数据库连接
			#endregion

			LstRowFilterType = new Collections.IListT<string>(_lstRowFilterType);
			LstRowFilterSeverity = new Collections.IListT<string>(_lstRowFilterSeverity);

			LstRowFilterType.ListChanged += new ListChangedEventHandler(LstRowFilter_ListChanged);
			LstRowFilterSeverity.ListChanged += new ListChangedEventHandler(LstRowFilter_ListChanged);
		}
		/// <summary>
		/// 初始数据(如Lookup数据等)
		/// </summary>
		/// <param name="ds"></param>
		public override void InitData(DataSet ds)
		{
			#region 准备数据集结构
			#endregion

			#region 加载所有字典表
			_xsd.dic_Type.InitData();
			#endregion
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
		}
		#endregion

		private void tsmiTypeError_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Type = 4)";
			if (tsmiTypeError.Checked)
			{
				LstRowFilterType.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterType.Remove(strRowFilter);
			}
		}

		private void tsmiTypeWarn_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Type = 3)";
			if (tsmiTypeWarn.Checked)
			{
				LstRowFilterType.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterType.Remove(strRowFilter);
			}
		}

		private void tsmiTypeTrace_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Type = 2)";
			if (tsmiTypeTrace.Checked)
			{
				LstRowFilterType.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterType.Remove(strRowFilter);
			}
		}

		private void tsmiTypeInfo_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Type = 1)";
			if (tsmiTypeInfo.Checked)
			{
				LstRowFilterType.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterType.Remove(strRowFilter);
			}
		}

		private void tsmiSeverity15_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Severity <= 15)";
			if (tsmiSeverity15.Checked)
			{
				LstRowFilterSeverity.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterSeverity.Remove(strRowFilter);
			}
		}

		private void tsmiSeverity20_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Severity >= 16 AND Severity <= 20)";
			if (tsmiSeverity20.Checked)
			{
				LstRowFilterSeverity.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterSeverity.Remove(strRowFilter);
			}
		}

		private void tsmiSeverity21_Click(object sender, EventArgs e)
		{
			string strRowFilter = "(Severity >= 21)";
			if (tsmiSeverity21.Checked)
			{
				LstRowFilterSeverity.AddUnique(strRowFilter);
			}
			else
			{
				LstRowFilterSeverity.Remove(strRowFilter);
			}
		}

		public void AddRow(DateTime _InTime, int Type, int Severity, string Title, string Msg, int AlarmGroupID = 0, int State = 0)
		{
			ErrList_XSD.ErrListRow dr = _xsd.ErrList.NewErrListRow();
			dr._InTime = _InTime;
			dr.Type = Type;
			dr.Severity = Severity;
			dr.Title = Title;
			dr.Msg = Msg;
			dr.AlarmGroupID = AlarmGroupID;
			dr.State = State;

			_xsd.ErrList.Rows.Add(dr);
		}
	}
}
