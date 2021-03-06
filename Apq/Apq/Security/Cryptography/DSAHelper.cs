﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Apq.Security.Cryptography
{
	/// <summary>
	/// DSA助手(数字签名)
	/// </summary>
	[Obsolete("实现有错误")]
	public class DSAHelper
	{
		/// <summary>
		/// 文件操作时单次读取的最大字节数
		/// </summary>
		public static int FileReadStep = 4194304;	// 4M

		#region CreateKey
		/// <summary>
		/// 创建密钥对(XmlString),使用者应将结果保存到文件存档
		/// </summary>
		/// <param name="ContainsPKey">是否包含私钥</param>
		/// <returns></returns>
		public static string CreateKey(bool ContainsPKey)
		{
			DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
			return dsa.ToXmlString(ContainsPKey);
		}
		#endregion

		//#region 文件处理
		///// <summary>
		///// 加密文件
		///// </summary>
		///// <param name="inName">来源文件</param>
		///// <param name="outName">输出文件</param>
		///// <param name="xmlString">密钥(至少含公钥)</param>
		//public static void EncryptFile(string inName, string outName, string xmlString)
		//{
		//    DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
		//    dsa.FromXmlString(xmlString);
		//    FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
		//    using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
		//    {
		//        fout.SetLength(0);

		//        long rdlen = 0;					//This is the total number of bytes written.
		//        int len;						//This is the number of bytes to be written at a time.
		//        byte[] bin = new byte[FileReadStep];
		//        while (rdlen < fin.Length)
		//        {
		//            len = fin.Read(bin, 0, FileReadStep);
		//            byte[] bs = dsa.Encrypt(bin, false);
		//            fout.Write(bs, 0, bs.Length);
		//            rdlen += len;
		//        }

		//        fin.Close();
		//    }
		//}

		///// <summary>
		///// 解密文件
		///// </summary>
		///// <param name="inName">来源文件</param>
		///// <param name="outName">输出文件</param>
		///// <param name="xmlString">密钥(公钥私钥俱有)</param>
		//public static void DecryptFile(string inName, string outName, string xmlString)
		//{
		//    DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
		//    dsa.FromXmlString(xmlString);
		//    FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
		//    using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
		//    {
		//        fout.SetLength(0);

		//        long rdlen = 0;					//This is the total number of bytes written.
		//        int len;						//This is the number of bytes to be written at a time.
		//        byte[] bin = new byte[FileReadStep];
		//        while (rdlen < fin.Length)
		//        {
		//            len = fin.Read(bin, 0, FileReadStep);
		//            byte[] bs = dsa.Decrypt(bin, false);
		//            fout.Write(bs, 0, bs.Length);
		//            rdlen += len;
		//        }

		//        fin.Close();
		//    }
		//}
		//#endregion

		#region 字符串处理
		/// <summary>
		/// DSA签名
		/// </summary>
		/// <param name="PlainText">原始字符串</param>
		/// <param name="xmlString">密钥(公钥私钥俱有)</param>
		/// <returns>Base64编码后的已签名字符串</returns>
		public static string SignString(string PlainText, string xmlString)
		{
			DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
			dsa.FromXmlString(xmlString);

			if (!string.IsNullOrEmpty(PlainText))
			{
				byte[] bText = System.Text.Encoding.UTF8.GetBytes(PlainText.ToCharArray());

				DSASignatureFormatter DSAFormatter = new DSASignatureFormatter(dsa);
				DSAFormatter.SetHashAlgorithm("SHA1");
				byte[] bEnc = DSAFormatter.CreateSignature(bText);

				return System.Convert.ToBase64String(bEnc);
			}
			return string.Empty;
		}

		/// <summary>
		/// DSA验证
		/// </summary>
		/// <param name="CypherText">签名后的Base64字符串</param>
		/// <param name="xmlString">密钥(至少含公钥)</param>
		/// <param name="signString">签名串</param>
		public static bool VerifyString(string CypherText, string xmlString, string signString)
		{
			DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
			dsa.FromXmlString(xmlString);

			if (!string.IsNullOrEmpty(CypherText))
			{
				byte[] bEnc = System.Convert.FromBase64String(CypherText);
				byte[] bText = System.Text.Encoding.UTF8.GetBytes(signString.ToCharArray());

				DSASignatureDeformatter DSADeformatter = new DSASignatureDeformatter(dsa);
				DSADeformatter.SetHashAlgorithm("SHA1");
				return DSADeformatter.VerifySignature(bEnc, bText);
			}
			return false;
		}
		#endregion
	}
}
