using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ApqDBManager.Controls.MainOption
{
	public partial class XmlServers : UserControl
	{
		public XmlServers()
		{
			InitializeComponent();
		}

		private void beXmlServers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beXmlServers.EditValue);
			if (!string.IsNullOrEmpty(strFullName)) ofbXmlServers.FileName = strFullName;
			if (ofbXmlServers.ShowDialog(this) == DialogResult.OK)
			{
				beXmlServers.EditValue = ofbXmlServers.FileName;
			}
		}

		private void beXmlServers_EditValueChanged(object sender, EventArgs e)
		{
			string strFullName = Apq.Convert.ChangeType<string>(beXmlServers.EditValue);
			if (!string.IsNullOrEmpty(strFullName)) ofbXmlServers.InitialDirectory = Path.GetDirectoryName(strFullName);
		}

		public void InitValue(Apq.Config.ConfigChain cfg)
		{
			if (!Apq.Convert.HasMean(beXmlServers.EditValue))
			{
				beXmlServers.EditValue = cfg[typeof(GlobalObject), "XmlServers"];
			}
			if (lbcRDBTypes.Items.Count == 0)
			{
				string[] lstRDBTypes = cfg[typeof(ApqDBManager.Forms.SqlIns), "RDBTypes"].Split(',');
				foreach (string strRDBType in lstRDBTypes)
				{
					lbcRDBTypes.Items.Add(strRDBType);
				}
			}
		}

		public void Confirm(Apq.Config.ConfigChain cfg)
		{
			if (Apq.Convert.HasMean(beXmlServers.EditValue))
			{
				cfg[typeof(GlobalObject), "XmlServers"] = Apq.Convert.ChangeType<string>(beXmlServers.EditValue);
			}
			if (lbcRDBTypes.Items.Count > 0)
			{
				string[] lstRDBTypes = new string[lbcRDBTypes.Items.Count];
				for (int i = 0; i < lbcRDBTypes.Items.Count; i++)
				{
					lstRDBTypes[i] = lbcRDBTypes.Items[i] as string;
				}
				cfg[typeof(ApqDBManager.Forms.SqlIns), "RDBTypes"] = string.Join(",", lstRDBTypes);
			}
			cfg.UserConfig.Save();
		}

		#region 服务器类型
		private void lbcRDBTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtRDBType.EditValue = lbcRDBTypes.SelectedItem as string;
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (lbcRDBTypes.SelectedIndex > 0)
			{
				int idx = lbcRDBTypes.SelectedIndex;
				string strRDBType = lbcRDBTypes.SelectedItem as string;
				lbcRDBTypes.Items.RemoveAt(idx);
				lbcRDBTypes.Items.Insert(idx - 1, strRDBType);
				lbcRDBTypes.SelectedIndex = idx - 1;
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (lbcRDBTypes.SelectedIndex < lbcRDBTypes.Items.Count - 1)
			{
				int idx = lbcRDBTypes.SelectedIndex;
				string strRDBType = lbcRDBTypes.SelectedItem as string;
				lbcRDBTypes.Items.RemoveAt(idx);
				lbcRDBTypes.Items.Insert(idx + 1, strRDBType);
				lbcRDBTypes.SelectedIndex = idx + 1;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(txtRDBType.EditValue))
				lbcRDBTypes.Items.Add(txtRDBType.EditValue);
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			if (Apq.Convert.HasMean(txtRDBType.EditValue) && lbcRDBTypes.SelectedIndex > -1)
			{
				int idx = lbcRDBTypes.SelectedIndex;
				string strRDBType = txtRDBType.EditValue as string;
				lbcRDBTypes.Items.RemoveAt(idx);
				lbcRDBTypes.Items.Insert(idx, strRDBType);
				lbcRDBTypes.SelectedIndex = idx;
			}
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			lbcRDBTypes.Items.Remove(lbcRDBTypes.SelectedItem);
		}
		#endregion
	}
}
