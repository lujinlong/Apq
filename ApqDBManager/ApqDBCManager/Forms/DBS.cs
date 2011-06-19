using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;

namespace ApqDBCManager.Forms
{
	public partial class DBS : Apq.Windows.Forms.DockForm
	{
		public DBS()
		{
			InitializeComponent();

			Apq.Windows.Forms.DataGridViewHelper.SetDefaultStyle(dataGridView1);
			Apq.Windows.Forms.DataGridViewHelper.AddBehaivor(dataGridView1);
		}

		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["服务器列表"];
			this.TabText = this.Text;

			tsbSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			tsbInput.Text = Apq.GlobalObject.UILang["导入(&I)"];

			computerIDDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["编号"];
			computerNameDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["名称"];
			computerTypeDataGridViewTextBoxColumn.HeaderText = Apq.GlobalObject.UILang["类型"];
		}

		private void DBServer_Load(object sender, EventArgs e)
		{
			#region 添加图标
			this.tsbSave.Image = System.Drawing.Image.FromFile(Application.StartupPath + @"\Res\png\File\Save.png");
			#endregion
		}

		private void DBServer_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		#region IDataShow 成员
		/// <summary>
		/// 前期准备(如数据库连接或文件等)
		/// </summary>
		public override void InitDataBefore()
		{
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
			#endregion
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="ds"></param>
		public override void LoadData(DataSet ds)
		{
		}
		/// <summary>
		/// 显示数据
		/// </summary>
		public override void ShowData()
		{
			#region 设置Lookup
			bsComputerType.DataSource = GlobalObject.Lookup;
			#endregion

			bsDBS.DataSource = GlobalObject.Lookup;
		}

		#endregion

		//保存
		private void tsbSave_Click(object sender, EventArgs e)
		{
			dataGridView1.EndEdit();
			GlobalObject.Lookup_Save();
			tsslOutInfo.Text = Apq.GlobalObject.UILang["保存成功"];
		}

		private void tsbInput_Click(object sender, EventArgs e)
		{
			//+
		}
	}
}