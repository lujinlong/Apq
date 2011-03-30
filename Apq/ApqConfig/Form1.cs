using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApqConfig
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			DataSet ds = new DataSet();
			DataTable dt = ds.Tables.Add("RSAKeys");
			dt.Columns.Add("Product");
			dt.Columns.Add("Version");
			dt.Columns.Add("RSAKey");

			for (int i = 0; i < 10; i++)
			{
				DataRow dr = dt.NewRow();
				dr["Product"] = "ApqDBManager";
				dr["Version"] = string.Format("0.{0}", i);
				dr["RSAKey"] = "<RSAKeyValue><Modulus>ncidmJi2/tb77mi8trEpqeejyhRTJDm/uZ7CGYa67+2yoKyLJoaEtq4SQphRjUIvH0p7dson8pst5GeFrZd5D0ZTUtmZylU4ISo1BKT6380QP82AtlARME8a3CjIuYTLTSMcjFSTmAE0ID1TG8vWdsr/qg6kxQJcgsRJWYhAUDU=</Modulus><Exponent>AQAB</Exponent><P>06iHXfs5Vg0G9DwzAJQBxcp3tSVNV+2ZCuEepieUsAjMzaKhp/lDXFe39nbhSnLEIB80IAuSE2/XFzGkwxTU7w==</P><Q>vta6kDhMXlk9oZkBX59xa28rHRpne/m7wBELJd98pKewxWqcxnuQFIkpvKjLFqvdntbZDUN0/tFBDOLsrE3VGw==</Q><DP>j9zPzZhRW2TVYjJ8tBrlrYu1m+Fz1Z0AVf23uFXU4WXJ1seAu0xYda6FsrcQ4GprVi3/XvyeWCm/d9tdUt+Y7w==</DP><DQ>SwgT6/YmmIXPxIRq1NTUfCAGPHgQLd8/YUGSN37J+9bumn/TSfp06I4ROdrHlo9WIEhqqFtYWYOeZtmlog0r9w==</DQ><InverseQ>AgMvKhm3qoOUj/W8dcRZuTQba7hyBA5Kp7yKrNq7HWpxtR5LnNYD8dMTlLNCmhZk1TMEwbN7H/LWbp2tskLKag==</InverseQ><D>OcnXRqNwKogMv3Xm4Dak3tCzIXkuNk9cVBy8VGMPJn71dHmdgV+1Tb8VewSUodsCrUA3VfuWg/mn5kawJDMdKBKb8hJ1Y19PXa2OJhZ6v+GXyekQFv4lyntt+F2WpAXyW+EUaeCFllKR2o79Zao9aC8/ehDhuW4ubG/+xOauj4U=</D></RSAKeyValue>";
				dt.Rows.Add(dr);
			}

			GloablObject.XmlAsmConfig.SetTableConfig("aaa", "bbb", dt);
			GloablObject.XmlAsmConfig.Save();
		}
	}
}
