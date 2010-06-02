using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Apq.Net
{
	/// <summary>
	/// Apq.Net.HttpWebRequest
	/// </summary>
	public class HttpWebRequest
	{
		/// <summary>
		/// 以指定编码发送请求到一 Url
		/// </summary>
		/// <param name="eEncoding">编码</param>
		/// <param name="Url"></param>
		/// <param name="inData">内容</param>
		/// <param name="Method">GET 或 POST</param>
		/// <param name="cc"></param>
		/// <param name="ContentType"></param>
		/// <returns></returns>
		public static string Request(Encoding eEncoding, string Url, string inData, string Method, CookieContainer cc, string ContentType)
		{
			string outdata = "";
			System.Net.HttpWebRequest hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(Url);
			hwRequest.ContentType = ContentType;// "application/x-www-form-urlencoded";
			hwRequest.ContentLength = inData.Length;
			hwRequest.Method = Method;// "POST";
			hwRequest.CookieContainer = cc;
			Stream hwrStream = hwRequest.GetRequestStream();
			StreamWriter sw = new StreamWriter(hwrStream, eEncoding);//Encoding.GetEncoding("gb2312"));
			sw.Write(inData);
			sw.Close();
			hwrStream.Close();

			HttpWebResponse myHttpWebResponse = (HttpWebResponse)hwRequest.GetResponse();
			myHttpWebResponse.Cookies = cc.GetCookies(hwRequest.RequestUri);
			Stream myResponseStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, eEncoding);
			outdata = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();
			return outdata;
		}

		/// <summary>
		/// 以默认编码 UTF8 发送请求到一 Url
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="inData">内容</param>
		/// <param name="Method">GET 或 POST</param>
		/// <returns></returns>
		public static string Request(string Url, string inData, string Method)
		{
			return Request(Encoding.UTF8, Url, inData, Method, new CookieContainer(), "application/x-www-form-urlencoded");
		}

		/// <summary>
		/// 以指定编码发送请求到一 Url
		/// </summary>
		/// <param name="eEncoding">编码</param>
		/// <param name="Url"></param>
		/// <param name="inData">内容</param>
		/// <param name="Method">GET 或 POST</param>
		/// <returns></returns>
		public static string Request(Encoding eEncoding, string Url, string inData, string Method)
		{
			return Request(eEncoding, Url, inData, Method, new CookieContainer(), "application/x-www-form-urlencoded");
		}

		/// <summary>
		/// 文件下载
		/// </summary>
		/// <param name="Url">下载路径</param>
		/// <param name="FileName">保存路径</param>
		public static void DownFile(string Url, string FileName)
		{
			System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(Url);
			System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
			long totalBytes = myrp.ContentLength;
			System.IO.Stream st = myrp.GetResponseStream();
			System.IO.Stream so = new System.IO.FileStream(FileName, System.IO.FileMode.Create);
			byte[] by = new byte[1024];
			int osize = st.Read(by, 0, (int)by.Length);
			while (osize > 0)
			{
				so.Write(by, 0, osize);
				osize = st.Read(by, 0, (int)by.Length);
			}
			so.Close();
			st.Close();
		}
	}
}
