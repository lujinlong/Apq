using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Apq.Reg.Client
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
				xmlString = GlobalObject.XmlAsmConfig["RSAKey"] = "<RSAKeyValue><Modulus>ncidmJi2/tb77mi8trEpqeejyhRTJDm/uZ7CGYa67+2yoKyLJoaEtq4SQphRjUIvH0p7dson8pst5GeFrZd5D0ZTUtmZylU4ISo1BKT6380QP82AtlARME8a3CjIuYTLTSMcjFSTmAE0ID1TG8vWdsr/qg6kxQJcgsRJWYhAUDU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
				GlobalObject.XmlAsmConfig.Save();
			}
			return xmlString;
		}
		#endregion

		#region 字符串处理
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
