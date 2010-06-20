using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Xml;

namespace Apq.Security.Cryptography
{
	/// <summary>
	/// RSA助手(非对称算法)
	/// </summary>
	public class RSAHelper
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
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			return rsa.ToXmlString(ContainsPKey);
		}
		#endregion

		#region 文件处理
		/// <summary>
		/// 加密文件
		/// </summary>
		/// <param name="inName">来源文件</param>
		/// <param name="outName">输出文件</param>
		/// <param name="xmlString">密钥(至少含公钥)</param>
		public static void EncryptFile(string inName, string outName, string xmlString)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlString);
			FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
			using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				fout.SetLength(0);

				long rdlen = 0;					//This is the total number of bytes written.
				int len;						//This is the number of bytes to be written at a time.
				byte[] bin = new byte[FileReadStep];
				while (rdlen < fin.Length)
				{
					len = fin.Read(bin, 0, FileReadStep);
					byte[] bs = rsa.Encrypt(bin, false);
					fout.Write(bs, 0, bs.Length);
					rdlen += len;
				}

				fin.Close();
			}
		}

		/// <summary>
		/// 解密文件
		/// </summary>
		/// <param name="inName">来源文件</param>
		/// <param name="outName">输出文件</param>
		/// <param name="xmlString">密钥(公钥私钥俱有)</param>
		public static void DecryptFile(string inName, string outName, string xmlString)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlString);
			FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
			using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				fout.SetLength(0);

				long rdlen = 0;					//This is the total number of bytes written.
				int len;						//This is the number of bytes to be written at a time.
				byte[] bin = new byte[FileReadStep];
				while (rdlen < fin.Length)
				{
					len = fin.Read(bin, 0, FileReadStep);
					byte[] bs = rsa.Decrypt(bin, false);
					fout.Write(bs, 0, bs.Length);
					rdlen += len;
				}

				fin.Close();
			}
		}
		#endregion

		#region 字符串处理
		/// <summary>
		/// 加密字符串
		/// </summary>
		/// <param name="PlainText">原始字符串</param>
		/// <param name="xmlString">密钥(至少含公钥)</param>
		/// <returns>Base64编码后的字符串</returns>
		public static string EncryptString(string PlainText, string xmlString)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlString);

			byte[] bText = System.Text.Encoding.UTF8.GetBytes(PlainText.ToCharArray());
			byte[] bEnc = rsa.Encrypt(bText, false);

			return System.Convert.ToBase64String(bEnc);
		}

		/// <summary>
		/// 解密字符串
		/// </summary>
		/// <param name="CypherText">加密后的Base64字符串</param>
		/// <param name="xmlString">密钥(公钥私钥俱有)</param>
		public static string DecryptString(string CypherText, string xmlString)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlString);

			byte[] bEnc = System.Convert.FromBase64String(CypherText);
			byte[] bText = rsa.Decrypt(bEnc, false);

			return System.Text.Encoding.UTF8.GetString(bEnc);
		}
		#endregion
	}
}
