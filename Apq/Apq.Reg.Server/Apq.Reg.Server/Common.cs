using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Apq.Reg.Server
{
	/// <summary>
	/// Common
	/// </summary>
	public static class Common
	{
		#region GetKey
		/// <summary>
		/// 获取密钥对(XmlString),包含私钥
		/// </summary>
		/// <returns></returns>
		public static string GetKey()
		{
			string xmlString = GlobalObject.XmlAsmConfig["RSAKey"];
			if (string.IsNullOrEmpty(xmlString))
			{
				xmlString = GlobalObject.XmlAsmConfig["RSAKey"] = "<RSAKeyValue><Modulus>ncidmJi2/tb77mi8trEpqeejyhRTJDm/uZ7CGYa67+2yoKyLJoaEtq4SQphRjUIvH0p7dson8pst5GeFrZd5D0ZTUtmZylU4ISo1BKT6380QP82AtlARME8a3CjIuYTLTSMcjFSTmAE0ID1TG8vWdsr/qg6kxQJcgsRJWYhAUDU=</Modulus><Exponent>AQAB</Exponent><P>06iHXfs5Vg0G9DwzAJQBxcp3tSVNV+2ZCuEepieUsAjMzaKhp/lDXFe39nbhSnLEIB80IAuSE2/XFzGkwxTU7w==</P><Q>vta6kDhMXlk9oZkBX59xa28rHRpne/m7wBELJd98pKewxWqcxnuQFIkpvKjLFqvdntbZDUN0/tFBDOLsrE3VGw==</Q><DP>j9zPzZhRW2TVYjJ8tBrlrYu1m+Fz1Z0AVf23uFXU4WXJ1seAu0xYda6FsrcQ4GprVi3/XvyeWCm/d9tdUt+Y7w==</DP><DQ>SwgT6/YmmIXPxIRq1NTUfCAGPHgQLd8/YUGSN37J+9bumn/TSfp06I4ROdrHlo9WIEhqqFtYWYOeZtmlog0r9w==</DQ><InverseQ>AgMvKhm3qoOUj/W8dcRZuTQba7hyBA5Kp7yKrNq7HWpxtR5LnNYD8dMTlLNCmhZk1TMEwbN7H/LWbp2tskLKag==</InverseQ><D>OcnXRqNwKogMv3Xm4Dak3tCzIXkuNk9cVBy8VGMPJn71dHmdgV+1Tb8VewSUodsCrUA3VfuWg/mn5kawJDMdKBKb8hJ1Y19PXa2OJhZ6v+GXyekQFv4lyntt+F2WpAXyW+EUaeCFllKR2o79Zao9aC8/ehDhuW4ubG/+xOauj4U=</D></RSAKeyValue>";
				GlobalObject.XmlAsmConfig.Save();
			}
			return xmlString;
		}
		#endregion

		#region 字符串处理
		/// <summary>
		/// RSA签名
		/// </summary>
		/// <param name="PlainText">原始字符串</param>
		/// <param name="xmlString">密钥(公钥私钥俱有)</param>
		/// <returns>Base64编码后的已签名字符串</returns>
		public static string SignString(string PlainText, string xmlString)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlString);

			RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
			f.SetHashAlgorithm("SHA1");
			byte[] bText = System.Text.Encoding.UTF8.GetBytes(PlainText);
			SHA1Managed sha = new SHA1Managed();
			byte[] hText = sha.ComputeHash(bText);
			byte[] bEnc = f.CreateSignature(hText);
			return System.Convert.ToBase64String(bEnc);
		}

		/// <summary>
		/// RSA验证
		/// </summary>
		/// <param name="CypherText">签名后的Base64字符串</param>
		/// <param name="xmlString">密钥(至少含公钥)</param>
		/// <param name="PlainText">原始串</param>
		public static bool VerifyString(string CypherText, string xmlString, string PlainText)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlString);

			RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsa);
			f.SetHashAlgorithm("SHA1");
			SHA1Managed sha = new SHA1Managed();
			byte[] bText = System.Convert.FromBase64String(CypherText);
			byte[] bEnc = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(PlainText));
			return f.VerifySignature(bEnc, bText);
		}
		#endregion
	}
}
