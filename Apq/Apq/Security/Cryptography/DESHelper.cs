using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Apq.Security.Cryptography
{
	/// <summary>
	/// DESHelper
	/// </summary>
	public class DESHelper
	{
		#region FormatKey
		/// <summary>
		/// 格式化密钥
		/// </summary>
		/// <param name="desKey">密钥</param>
		/// <returns></returns>
		public static byte[] FormatKey(byte[] desKey)
		{
			DES des = new DESCryptoServiceProvider();
			return SymmetricAlgorithmHelper.FormatKey(desKey, des.KeySize);
		}
		#endregion

		#region 文件处理
		/// <summary>
		/// 加密文件
		/// </summary>
		/// <param name="inName">来源文件</param>
		/// <param name="outName">输出文件</param>
		/// <param name="desKey">加密密钥</param>
		/// <param name="desIV">初始向量</param>
		public static void EncryptFile(string inName, string outName, byte[] desKey, byte[] desIV)
		{
			DES des = DES.Create();
			des.Key = FormatKey(desKey);
			des.IV = FormatKey(desIV);
			SymmetricAlgorithmHelper.EncryptFile(inName, outName, des);
		}

		/// <summary>
		/// 解密文件
		/// </summary>
		/// <param name="inName">来源文件</param>
		/// <param name="outName">输出文件</param>
		/// <param name="desKey">解密密钥</param>
		/// <param name="desIV">初始向量</param>
		public static void DecryptFile(string inName, string outName, byte[] desKey, byte[] desIV)
		{
			DES des = DES.Create();
			des.Key = FormatKey(desKey);
			des.IV = FormatKey(desIV);
			SymmetricAlgorithmHelper.DecryptFile(inName, outName, des);
		}
		#endregion

		#region 字符串处理
		/// <summary>
		/// 加密字符串
		/// </summary>
		/// <param name="PlainText">原始字符串</param>
		/// <param name="desKey">加密密钥</param>
		/// <param name="desIV">初始向量</param>
		/// <returns>Base64编码后的字符串</returns>
		public static string EncryptString(string PlainText, byte[] desKey, byte[] desIV)
		{
			DES des = DES.Create();
			des.Key = FormatKey(desKey);
			des.IV = FormatKey(desIV);
			return SymmetricAlgorithmHelper.EncryptString(PlainText, des);
		}

		/// <summary>
		/// 解密字符串
		/// </summary>
		/// <param name="CypherText">加密后的Base64字符串</param>
		/// <param name="desKey">解密密钥</param>
		/// <param name="desIV">初始向量</param>
		public static string DecryptString(string CypherText, byte[] desKey, byte[] desIV)
		{
			DES des = DES.Create();
			des.Key = FormatKey(desKey);
			des.IV = FormatKey(desIV);
			return SymmetricAlgorithmHelper.DecryptString(CypherText, des);
		}

		/// <summary>
		/// 加密字符串
		/// </summary>
		/// <param name="PlainText">原始字符串</param>
		/// <param name="desKey">加密密钥</param>
		/// <param name="desIV">初始向量</param>
		/// <returns>Base64编码后的字符串</returns>
		public static string EncryptString(string PlainText, string desKey, string desIV)
		{
			return EncryptString(PlainText, System.Text.Encoding.Unicode.GetBytes(desKey), System.Text.Encoding.Unicode.GetBytes(desIV));
		}

		/// <summary>
		/// 解密字符串
		/// </summary>
		/// <param name="CypherText">加密后的Base64字符串</param>
		/// <param name="desKey">解密密钥</param>
		/// <param name="desIV">初始向量</param>
		public static string DecryptString(string CypherText, string desKey, string desIV)
		{
			return DecryptString(CypherText, System.Text.Encoding.Unicode.GetBytes(desKey), System.Text.Encoding.Unicode.GetBytes(desIV));
		}
		#endregion
	}
}
