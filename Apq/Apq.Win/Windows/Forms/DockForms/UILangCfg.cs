using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Forms.DockForms
{
	/// <summary>
	/// 语言设置
	/// </summary>
	public partial class UILangCfg : DockForm, Apq.Editor.IFileLoader
	{
		/// <summary>
		/// 语言设置
		/// </summary>
		public UILangCfg()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 设置界面语言值
		/// </summary>
		/// <param name="UILang"></param>
		public override void SetUILang(Apq.UILang.UILang UILang)
		{
			this.Text = Apq.GlobalObject.UILang["语言设置"];
			this.TabText = this.Text;

			#region 工具栏
			tsbSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
			tsbApply.Text = Apq.GlobalObject.UILang["应用(&A)"];
			#endregion

			#region 列头
			col1.HeaderText = Apq.GlobalObject.UILang["原文"];
			col2.HeaderText = Apq.GlobalObject.UILang["中文"];
			#endregion
		}

		private void UILangCfg_Load(object sender, EventArgs e)
		{
			// 读取文件列表,"UILang\"
			if (System.IO.Directory.Exists("UILang"))
			{
				foreach (string strFileName in System.IO.Directory.GetFiles("UILang", "*.xml"))
				{
					tscbFile.Items.Add(System.IO.Path.GetFileNameWithoutExtension(strFileName));
				}
			}

			tscbFile.Text = Apq.GlobalObject.XmlConfigChain[typeof(Apq.GlobalObject), "UILang"];

			DataGridViewHelper.SetDefaultStyle(dataGridView1);
			DataGridViewHelper.AddBehaivor(dataGridView1);
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
			#endregion
		}

		#endregion

		private void tsbSave_Click(object sender, EventArgs e)
		{
			if (!tscbFile.Items.Contains(tscbFile.Text))
			{
				tscbFile.Items.Add(tscbFile.Text);
			}

			Save();
		}

		private void tsbApply_Click(object sender, EventArgs e)
		{
			tsbSave_Click(sender, e);

			Apq.GlobalObject.UILang.FileName = "UILang\\" + FileName + ".xml";
			Apq.GlobalObject.UILang.Load();
			Apq.GlobalObject.XmlConfigChain[typeof(Apq.GlobalObject), "UILang"] = Apq.GlobalObject.UILang.FileName;

			ImeForm dfParent = this.MdiParent as ImeForm;
			if (dfParent != null)
			{
				dfParent.SetUILang(Apq.GlobalObject.UILang);
			}
			foreach (DockForm dc in dfParent.MdiChildren)
			{
				dc.SetUILang(Apq.GlobalObject.UILang);
			}
		}

		#region IFileLoader 成员
		private string _FileName = string.Empty;
		/// <summary>
		/// 获取或设置语言文件名(不含文件夹和后缀)
		/// </summary>
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				this.Text = Apq.GlobalObject.UILang["语言设置"] + " - " + _FileName;
				this.TabText = this.Text;
			}
		}

		/// <summary>
		/// 打开语言文件
		/// </summary>
		public void Open()
		{
			if (System.IO.File.Exists("UILang\\" + FileName + ".xml"))
			{
				try
				{
					uiLang1.Clear();
					uiLang1._UILang.ReadXml("UILang\\" + FileName + ".xml");
				}
				catch { }
			}
		}

		/// <summary>
		/// 保存语言文件
		/// </summary>
		public void Save()
		{
			if (!string.IsNullOrEmpty(FileName))
			{
				if (!System.IO.Directory.Exists("UILang"))
				{
					System.IO.Directory.CreateDirectory("UILang");
				}
				uiLang1._UILang.WriteXml("UILang\\" + FileName + ".xml", XmlWriteMode.IgnoreSchema);
			}
		}

		/// <summary>
		/// 另存为
		/// </summary>
		/// <param name="FileName"></param>
		public void SaveAs(string FileName)
		{
			throw new NotImplementedException();
		}

		#endregion

		private void tscbFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			FileName = tscbFile.Text;
			Open();
		}

		private void tscbFile_TextChanged(object sender, EventArgs e)
		{
			FileName = tscbFile.Text;
		}
	}
}
