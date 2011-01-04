using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apq.Reg.Client.Win
{
	/// <summary>
	/// 注册窗口
	/// </summary>
	public partial class RegForm : Form
	{
		/// <summary>
		/// 注册窗口
		/// </summary>
		public RegForm()
		{
			InitializeComponent();
		}

		private void RegForm_Load(object sender, EventArgs e)
		{
			txtName.Text = GlobalObject.XmlAsmConfig[typeof(Apq.Reg.Client.Common), "Name"] ?? string.Empty;
			txtSN.Text = GlobalObject.XmlAsmConfig[typeof(Apq.Reg.Client.Common), "SN"] ?? string.Empty;
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			string xmlString = Apq.Reg.Client.Common.GetKey();
			string strName = txtName.Text.Trim().ToLower();
			string strSN = txtSN.Text.Trim();

			try
			{
				if (Apq.Reg.Client.Common.VerifyString(strSN, xmlString, strName))
				{
					GlobalObject.XmlAsmConfig[typeof(Apq.Reg.Client.Common), "Name"] = strName;
					GlobalObject.XmlAsmConfig[typeof(Apq.Reg.Client.Common), "SN"] = strSN;
					GlobalObject.XmlAsmConfig.Save();

					MessageBox.Show("成功", "注册结果");
					this.Close();
				}
				else
				{
					MessageBox.Show("失败", "注册结果");
				}
			}
			catch
			{
				MessageBox.Show("失败", "注册结果");
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
