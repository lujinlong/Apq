using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Apq.Security.Cryptography
{
	/// <summary>
	/// 对称算法助手
	/// </summary>
	public class SymmetricAlgorithmHelper
	{
		/// <summary>
		/// 文件操作时单次读取的最大字节数
		/// </summary>
		public static int FileReadStep = 4194304;	// 4M

		#region FormatKey
		/// <summary>
		/// 格式化密钥,跳过全0字节,不足时补入0xFF
		/// </summary>
		/// <param name="desKey">密钥</param>
		/// <param name="Size">返回的位长度(实际按字节处理,所以应为8的倍数)</param>
		/// <returns></returns>
		public static byte[] FormatKey(byte[] desKey, int Size)
		{
			byte[] rtn = new byte[Size / 8];

			int wLength = 0;//当前写入位置
			int p = 0;//当前读取位置
			while (wLength < rtn.Length && p < desKey.Length)
			{
				byte b = desKey[p++];
				if (b > 0)
				{
					rtn[wLength++] = b;
				}
			}

			while (wLength < rtn.Length)
			{
				rtn[wLength++] = 0xFF;
			}

			return rtn;
		}
		#endregion

		#region 文件处理
		/// <summary>
		/// 加密文件
		/// </summary>
		/// <param name="inName">来源文件</param>
		/// <param name="outName">输出文件</param>
		/// <param name="Algorithm">对称算法</param>
		public static void EncryptFile(string inName, string outName, SymmetricAlgorithm Algorithm)
		{
			FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
			using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				fout.SetLength(0);

				CryptoStream encStream = new CryptoStream(fout, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);

				//Read from the input file, then encrypt and write to the output file.
				//Create variables to help with read and write.
				long rdlen = 0;					//This is the total number of bytes written.
				int len;						//This is the number of bytes to be written at a time.
				byte[] bin = new byte[FileReadStep];
				while (rdlen < fin.Length)
				{
					len = fin.Read(bin, 0, FileReadStep);
					encStream.Write(bin, 0, len);
					rdlen += len;
				}

				encStream.Close();
				fin.Close();
			}
		}

		/// <summary>
		/// 解密文件
		/// </summary>
		/// <param name="inName">来源文件</param>
		/// <param name="outName">输出文件</param>
		/// <param name="Algorithm">对称算法</param>
		public static void DecryptFile(string inName, string outName, SymmetricAlgorithm Algorithm)
		{
			FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
			using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				fout.SetLength(0);

				CryptoStream encStream = new CryptoStream(fout, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);

				//Read from the input file, then decrypt and write to the output file.
				//Create variables to help with read and write.
				long rdlen = 0;					//This is the total number of bytes written.
				int len;						//This is the number of bytes to be written at a time.
				byte[] bin = new byte[FileReadStep];
				while (rdlen < fin.Length)
				{
					len = fin.Read(bin, 0, FileReadStep);
					encStream.Write(bin, 0, len);
					rdlen += len;
				}

				encStream.Close();
				fin.Close();
			}
		}
		#endregion

		#region 字符串处理
		/// <summary>
		/// 加密字符串
		/// </summary>
		/// <param name="PlainText">原始字符串</param>
		/// <param name="Algorithm">对称算法</param>
		/// <returns>Base64编码后的字符串</returns>
		public static string EncryptString(string PlainText, SymmetricAlgorithm Algorithm)
		{
			// Create a memory stream.
			MemoryStream ms = new MemoryStream();

			// Create a CryptoStream using the memory stream and the 
			// CSP DES key.  
			CryptoStream encStream = new CryptoStream(ms, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);

			// Create a StreamWriter to write a string
			// to the stream.
			StreamWriter sw = new StreamWriter(encStream);

			// Write the plaintext to the stream.
			sw.Write(PlainText);

			// Close the StreamWriter and CryptoStream.
			sw.Close();
			encStream.Close();

			// Get an array of bytes that represents
			// the memory stream.
			byte[] buffer = ms.ToArray();

			// Close the memory stream.
			ms.Close();

			// Return the encrypted byte array.
			return System.Convert.ToBase64String(buffer);
		}

		/// <summary>
		/// 解密字符串
		/// </summary>
		/// <param name="CypherText">加密后的Base64字符串</param>
		/// <param name="Algorithm">对称算法</param>
		/// <returns>原始字符串</returns>
		public static string DecryptString(string CypherText, SymmetricAlgorithm Algorithm)
		{
			// Create a memory stream to the passed buffer.
			MemoryStream ms = new MemoryStream(System.Convert.FromBase64String(CypherText));

			// Create a CryptoStream using the memory stream and the 
			// CSP DES key. 
			CryptoStream encStream = new CryptoStream(ms, Algorithm.CreateDecryptor(), CryptoStreamMode.Read);

			// Create a StreamReader for reading the stream.
			StreamReader sr = new StreamReader(encStream);

			// Read the stream as a string.
			string val = sr.ReadToEnd();

			// Close the streams.
			sr.Close();
			encStream.Close();
			ms.Close();

			return val;
		}
		#endregion
	}
}
