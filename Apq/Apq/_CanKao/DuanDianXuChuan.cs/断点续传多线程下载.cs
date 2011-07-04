/* .Net/C#: 实现支持断点续传多线程下载的 Http Web 客户端工具类 (C# DIY HttpWebClient)
* Reflector 了一下 System.Net.WebClient ,重载或增加了若干:
* DownLoad、Upload 相关方法!
* DownLoad 相关改动较大!
* 增加了 DataReceive、ExceptionOccurrs 事件!
* 了解服务器端与客户端交互的 HTTP 协议参阅:
* 使文件下载的自定义连接支持 FlashGet 的断点续传多线程链接下载! JSP/Servlet 实现!
* http://blog.csdn.net/playyuer/archive/00/08/0/80.aspx
* 使文件下载的自定义连接支持 FlashGet 的断点续传多线程链接下载! C#/ASP.Net 实现! 
* http://blog.csdn.net/playyuer/archive/00/08/0/88.aspx
*/


namespace Microshaoft.Utils
{
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using System.Security;
	using System.Threading;
	using System.Collections.Specialized;

	/// <summary>
	/// 记录下载的字节位置
	/// </summary>
	public class DownLoadState
	{
		private string _FileName;

		private string _AttachmentName;
		private int _Position;
		private string _RequestURL;
		private string _ResponseURL;
		private int _Length;

		private byte[] _Data;

		public string FileName
		{
			get
			{
				return _FileName;
			}
		}

		public int Position
		{
			get
			{
				return _Position;
			}
		}

		public int Length
		{
			get
			{
				return _Length;
			}
		}


		public string AttachmentName
		{
			get
			{
				return _AttachmentName;
			}
		}

		public string RequestURL
		{
			get
			{
				return _RequestURL;
			}
		}

		public string ResponseURL
		{
			get
			{
				return _ResponseURL;
			}
		}


		public byte[] Data
		{
			get
			{
				return _Data;
			}
		}

		internal DownLoadState(string RequestURL, string ResponseURL, string FileName, string AttachmentName, int Position, int Length, byte[] Data)
		{
			this._FileName = FileName;
			this._RequestURL = RequestURL;
			this._ResponseURL = ResponseURL;
			this._AttachmentName = AttachmentName;
			this._Position = Position;
			this._Data = Data;
			this._Length = Length;
		}

		internal DownLoadState(string RequestURL, string ResponseURL, string FileName, string AttachmentName, int Position, int Length, ThreadCallbackHandler tch)
		{
			this._RequestURL = RequestURL;
			this._ResponseURL = ResponseURL;
			this._FileName = FileName;
			this._AttachmentName = AttachmentName;
			this._Position = Position;
			this._Length = Length;
			this._ThreadCallback = tch;
		}

		internal DownLoadState(string RequestURL, string ResponseURL, string FileName, string AttachmentName, int Position, int Length)
		{
			this._RequestURL = RequestURL;
			this._ResponseURL = ResponseURL;
			this._FileName = FileName;
			this._AttachmentName = AttachmentName;
			this._Position = Position;
			this._Length = Length;
		}

		private ThreadCallbackHandler _ThreadCallback;

		//
		internal void StartDownloadFileChunk()
		{
			if (this._ThreadCallback != null)
			{
				this._ThreadCallback(this._RequestURL, this._FileName, this._Position, this._Length);
			}
		}

	}

	//委托代理线程的所执行的方法签名一致
	public delegate void ThreadCallbackHandler(string S, string s, int I, int i);

	//异常处理动作
	public enum ExceptionActions
	{
		Throw,
		CancelAll,
		Ignore,
		Retry
	}

	/// <summary>
	/// 包含 Exception 事件数据的类
	/// </summary>
	public class ExceptionEventArgs : System.EventArgs
	{
		private System.Exception _Exception;
		private ExceptionActions _ExceptionAction;

		private DownLoadState _DownloadState;

		public DownLoadState DownloadState
		{
			get
			{
				return _DownloadState;
			}
		}

		public Exception Exception
		{
			get
			{
				return _Exception;
			}
		}

		public ExceptionActions ExceptionAction
		{
			get
			{
				return _ExceptionAction;
			}
			set
			{
				_ExceptionAction = value;
			}
		}

		internal ExceptionEventArgs(System.Exception e, DownLoadState DownloadState)
		{
			this._Exception = e;
			this._DownloadState = DownloadState;
		}
	}

	/// <summary>
	/// 包含 DownLoad 事件数据的类
	/// </summary>
	public class DownLoadEventArgs : System.EventArgs
	{
		private DownLoadState _DownloadState;

		public DownLoadState DownloadState
		{
			get
			{
				return _DownloadState;
			}
		}

		public DownLoadEventArgs(DownLoadState DownloadState)
		{
			this._DownloadState = DownloadState;
		}

	}

	/// <summary>
	/// 支持断点续传多线程下载的类
	/// </summary>
	public class HttpWebClient
	{
		private static object _SyncLockObject = new object();

		public delegate void DataReceiveEventHandler(HttpWebClient Sender, DownLoadEventArgs e);

		public event DataReceiveEventHandler DataReceive; //接收字节数据事件

		public delegate void ExceptionEventHandler(HttpWebClient Sender, ExceptionEventArgs e);

		public event ExceptionEventHandler ExceptionOccurrs; //发生异常事件

		private int _FileLength; //下载文件的总大小

		public int FileLength
		{
			get
			{
				return _FileLength;
			}
		}

		/// <summary>
		/// 分块下载文件
		/// </summary>
		/// <param name="Address">URL 地址</param>
		/// <param name="FileName">保存到本地的路径文件名</param>
		/// <param name="ChunksCount">块数,线程数</param>
		public void DownloadFile(string Address, string FileName, int ChunksCount)
		{
			int p = 0; // position
			int s = 0; // chunk size
			string a = null;
			HttpWebRequest hwrq;
			HttpWebResponse hwrp = null;
			try
			{
				hwrq = (HttpWebRequest)WebRequest.Create(this.GetUri(Address));
				hwrp = (HttpWebResponse)hwrq.GetResponse();
				long L = hwrp.ContentLength;

				hwrq.Credentials = this.m_credentials;

				L = ((L == -1) || (L > 0x7fffffff)) ? ((long)0x7fffffff) : L; //Int.MaxValue 该常数的值为 ,7,8,67; 即十六进制的 0x7FFFFFFF

				int l = (int)L;

				this._FileLength = l;

				// 在本地预定空间(竟然在多线程下不用先预定空间)
				// FileStream sw = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				// sw.Write(new byte[l], 0, l);
				// sw.Close();
				// sw = null;

				bool b = (hwrp.Headers["Accept-Ranges"] != null & hwrp.Headers["Accept-Ranges"] == "bytes");
				a = hwrp.Headers["Content-Disposition"]; //attachment
				if (a != null)
				{
					a = a.Substring(a.LastIndexOf("filename=") + 9);
				}
				else
				{
					a = FileName;
				}

				int ss = s;
				if (b)
				{
					s = l / ChunksCount;
					if (s < 0x60) //块大小至少为 8 K 字节
					{
						s = 0x60;
					}
					ss = s;
					int i = 0;
					while (l > s)
					{
						l -= s;
						if (l < s)
						{
							s += l;
						}
						if (i++ > 0)
						{
							DownLoadState x = new DownLoadState(Address, hwrp.ResponseUri.AbsolutePath, FileName, a, p, s, new ThreadCallbackHandler(this.DownloadFileChunk));
							// 单线程下载
							// x.StartDownloadFileChunk();

							//多线程下载
							//Thread t = 
							new Thread(new ThreadStart(x.StartDownloadFileChunk)).Start();
							//t.Start();
						}
						p += s;
					}
					s = ss;
					byte[] buffer = this.ResponseAsBytes(Address, hwrp, s, FileName);

					// lock (_SyncLockObject)
					// {
					// this._Bytes += buffer.Length;
					// }
				}
			}
			catch (Exception e)
			{
				ExceptionActions ea = ExceptionActions.Throw;
				if (this.ExceptionOccurrs != null)
				{
					DownLoadState x = new DownLoadState(Address, hwrp.ResponseUri.AbsolutePath, FileName, a, p, s);
					ExceptionEventArgs eea = new ExceptionEventArgs(e, x);
					ExceptionOccurrs(this, eea);
					ea = eea.ExceptionAction;
				}

				if (ea == ExceptionActions.Throw)
				{
					if (!(e is WebException) && !(e is SecurityException))
					{
						throw new WebException("net_webclient", e);
					}
					throw;
				}
			}

		}

		/// <summary>
		/// 下载一个文件块,利用该方法可自行实现多线程断点续传
		/// </summary>
		/// <param name="Address">URL 地址</param>
		/// <param name="FileName">保存到本地的路径文件名</param>
		/// <param name="Length">块大小</param>
		public void DownloadFileChunk(string Address, string FileName, int FromPosition, int Length)
		{
			HttpWebResponse hwrp = null;
			string a = null;
			try
			{
				//this._FileName = FileName;
				HttpWebRequest hwrq = (HttpWebRequest)WebRequest.Create(this.GetUri(Address));
				//hwrq.Credentials = this.m_credentials;
				hwrq.AddRange(FromPosition);
				hwrp = (HttpWebResponse)hwrq.GetResponse();
				a = hwrp.Headers["Content-Disposition"]; //attachment
				if (a != null)
				{
					a = a.Substring(a.LastIndexOf("filename=") + 9);
				}
				else
				{
					a = FileName;
				}

				byte[] buffer = this.ResponseAsBytes(Address, hwrp, Length, FileName);
				// lock (_SyncLockObject)
				// {
				// this._Bytes += buffer.Length;
				// }
			}
			catch (Exception e)
			{
				ExceptionActions ea = ExceptionActions.Throw;
				if (this.ExceptionOccurrs != null)
				{
					DownLoadState x = new DownLoadState(Address, hwrp.ResponseUri.AbsolutePath, FileName, a, FromPosition, Length);
					ExceptionEventArgs eea = new ExceptionEventArgs(e, x);
					ExceptionOccurrs(this, eea);
					ea = eea.ExceptionAction;
				}

				if (ea == ExceptionActions.Throw)
				{
					if (!(e is WebException) && !(e is SecurityException))
					{
						throw new WebException("net_webclient", e);
					}
					throw;
				}
			}
		}

		internal byte[] ResponseAsBytes(string RequestURL, WebResponse Response, long Length, string FileName)
		{
			string a = null; //AttachmentName
			int P = 0; //整个文件的位置指针
			long num = 0;
			try
			{
				a = Response.Headers["Content-Disposition"]; //attachment
				if (a != null)
				{
					a = a.Substring(a.LastIndexOf("filename=") + 9);
				}

				num = Length; //Response.ContentLength;
				bool flag = false;
				if (num == -1)
				{
					flag = true;
					num = 0x0000; //6k
				}
				byte[] buffer = new byte[(int)num];


				int p = 0; //本块的位置指针

				string s = Response.Headers["Content-Range"];
				if (s != null)
				{
					s = s.Replace("bytes ", "");
					s = s.Substring(0, s.IndexOf("-"));
					P = Convert.Toint(s);
				}
				num = 0;

				Stream S = Response.GetResponseStream();
				do
				{
					num = S.Read(buffer, num, ((int)num) - num);

					num += num;
					if (flag && (num == num))
					{
						num += 0x0000;
						byte[] buffer = new byte[(int)num];
						Buffer.BlockCopy(buffer, 0, buffer, 0, num);
						buffer = buffer;
					}

					// lock (_SyncLockObject)
					// {
					// this._bytes += num;
					// }
					if (num > 0)
					{
						if (this.DataReceive != null)
						{
							byte[] buffer = new byte[num];
							Buffer.BlockCopy(buffer, p, buffer, 0, buffer.Length);
							DownLoadState dls = new DownLoadState(RequestURL, Response.ResponseUri.AbsolutePath, FileName, a, P, num, buffer);
							DownLoadEventArgs dlea = new DownLoadEventArgs(dls);
							//触发事件
							this.OnDataReceive(dlea);
							//System.Threading.Thread.Sleep(00);

						}
						p += num; //本块的位置指针
						P += num; //整个文件的位置指针
					}
					else
					{
						break;
					}

				}
				while (num != 0);

				S.Close();
				S = null;
				if (flag)
				{
					byte[] buffer = new byte[num];
					Buffer.BlockCopy(buffer, 0, buffer, 0, num);
					buffer = buffer;
				}
				return buffer;
			}
			catch (Exception e)
			{
				ExceptionActions ea = ExceptionActions.Throw;
				if (this.ExceptionOccurrs != null)
				{
					DownLoadState x = new DownLoadState(RequestURL, Response.ResponseUri.AbsolutePath, FileName, a, P, num);
					ExceptionEventArgs eea = new ExceptionEventArgs(e, x);
					ExceptionOccurrs(this, eea);
					ea = eea.ExceptionAction;
				}

				if (ea == ExceptionActions.Throw)
				{
					if (!(e is WebException) && !(e is SecurityException))
					{
						throw new WebException("net_webclient", e);
					}
					throw;
				}
				return null;
			}
		}

		private void OnDataReceive(DownLoadEventArgs e)
		{
			//触发数据到达事件
			DataReceive(this, e);
		}

		public byte[] UploadFile(string address, string fileName)
		{
			return this.UploadFile(address, "POST", fileName, "file");
		}

		public string UploadFileEx(string address, string method, string fileName, string fieldName)
		{
			return Encoding.ASCII.GetString(UploadFile(address, method, fileName, fieldName));
		}

		public byte[] UploadFile(string address, string method, string fileName, string fieldName)
		{
			byte[] buffer;
			FileStream stream = null;
			try
			{
				fileName = Path.GetFullPath(fileName);
				string text = "---------------------" + DateTime.Now.Ticks.ToString("x");

				string text = "application/octet-stream";

				stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				WebRequest request = WebRequest.Create(this.GetUri(address));
				request.Credentials = this.m_credentials;
				request.ContentType = "multipart/form-data; boundary=" + text;

				request.Method = method;
				string[] textArray = new string[7] { "--", text, "\r\nContent-Disposition: form-data; name=\"" + fieldName + "\"; filename=\"", Path.GetFileName(fileName), "\"\r\nContent-Type: ", text, "\r\n\r\n" };
				string text = string.Concat(textArray);
				byte[] buffer = Encoding.UTF8.GetBytes(text);
				byte[] buffer = Encoding.ASCII.GetBytes("\r\n--" + text + "\r\n");
				long num = 0x7fffffffffffffff;
				try
				{
					num = stream.Length;
					request.ContentLength = (num + buffer.Length) + buffer.Length;
				}
				catch
				{
				}
				byte[] buffer = new byte[Math.Min(0x000, (int)num)];
				using (Stream stream = request.GetRequestStream())
				{
					int num;
					stream.Write(buffer, 0, buffer.Length);
					do
					{
						num = stream.Read(buffer, 0, buffer.Length);
						if (num != 0)
						{
							stream.Write(buffer, 0, num);
						}
					}
					while (num != 0);
					stream.Write(buffer, 0, buffer.Length);
				}
				stream.Close();
				stream = null;
				WebResponse response = request.GetResponse();

				buffer = this.ResponseAsBytes(response);
			}
			catch (Exception exception)
			{
				if (stream != null)
				{
					stream.Close();
					stream = null;
				}
				if (!(exception is WebException) && !(exception is SecurityException))
				{
					//throw new WebException(SR.GetString("net_webclient"), exception);
					throw new WebException("net_webclient", exception);
				}
				throw;
			}
			return buffer;
		}

		private byte[] ResponseAsBytes(WebResponse response)
		{
			int num;
			long num = response.ContentLength;
			bool flag = false;
			if (num == -1)
			{
				flag = true;
				num = 0x0000;
			}
			byte[] buffer = new byte[(int)num];
			Stream stream = response.GetResponseStream();
			int num = 0;
			do
			{
				num = stream.Read(buffer, num, ((int)num) - num);
				num += num;
				if (flag && (num == num))
				{
					num += 0x0000;
					byte[] buffer = new byte[(int)num];
					Buffer.BlockCopy(buffer, 0, buffer, 0, num);
					buffer = buffer;
				}
			}
			while (num != 0);
			stream.Close();
			if (flag)
			{
				byte[] buffer = new byte[num];
				Buffer.BlockCopy(buffer, 0, buffer, 0, num);
				buffer = buffer;
			}
			return buffer;
		}

		private NameValueCollection m_requestParameters;
		private Uri m_baseAddress;
		private ICredentials m_credentials = CredentialCache.DefaultCredentials;

		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		public NameValueCollection QueryString
		{
			get
			{
				if (this.m_requestParameters == null)
				{
					this.m_requestParameters = new NameValueCollection();
				}
				return this.m_requestParameters;
			}
			set
			{
				this.m_requestParameters = value;
			}
		}

		public string BaseAddress
		{
			get
			{
				if (this.m_baseAddress != null)
				{
					return this.m_baseAddress.ToString();
				}
				return string.Empty;
			}
			set
			{
				if ((value == null) || (value.Length == 0))
				{
					this.m_baseAddress = null;
				}
				else
				{
					try
					{
						this.m_baseAddress = new Uri(value);
					}
					catch (Exception exception)
					{
						throw new ArgumentException("value", exception);
					}
				}
			}
		}

		private Uri GetUri(string path)
		{
			Uri uri;
			try
			{
				if (this.m_baseAddress != null)
				{
					uri = new Uri(this.m_baseAddress, path);
				}
				else
				{
					uri = new Uri(path);
				}
				if (this.m_requestParameters == null)
				{
					return uri;
				}
				StringBuilder builder = new StringBuilder();
				string text = string.Empty;
				for (int num = 0; num < this.m_requestParameters.Count; num++)
				{
					builder.Append(text + this.m_requestParameters.AllKeys[num] + "=" + this.m_requestParameters[num]);
					text = "&";
				}
				UriBuilder builder = new UriBuilder(uri);
				builder.Query = builder.ToString();
				uri = builder.Uri;
			}
			catch (UriFormatException)
			{
				uri = new Uri(Path.GetFullPath(path));
			}
			return uri;
		}

	}

}

/// <summary>
/// 测试类
/// </summary>
class AppTest
{
	static void Main()
	{
		AppTest a = new AppTest();
		Microshaoft.Utils.HttpWebClient x = new Microshaoft.Utils.HttpWebClient();

		//订阅 DataReceive　事件
		x.DataReceive += new Microshaoft.Utils.HttpWebClient.DataReceiveEventHandler(a.x_DataReceive);
		//订阅 ExceptionOccurrs　事件
		x.ExceptionOccurrs += new Microshaoft.Utils.HttpWebClient.ExceptionEventHandler(a.x_ExceptionOccurrs);

		string F = "http://localhost/download/phpMyAdmin-.6.-pl.zip";
		a._F = F;
		F = "http://localhost/download/jdk-__0_0-windows-i86-p.aa.exe";
		//F = "http://localhost/download/ReSharper..exe";

		//F = "http://localhost/mywebapplications/WebApplication7/WebForm.aspx";
		//F = "http://localhost:080/test/download.jsp";

		//F = "http://localhost/download/Webcast000_PPT.zip";
		//F = "http://www.morequick.com/greenbrowsergb.zip";
		//F = "http://localhost/download/test_local.rar";
		string f = F.Substring(F.LastIndexOf("/") + 1);

		//(new System.Threading.Thread(new System.Threading.ThreadStart(new ThreadProcessState(F, @"E:\temp\" + f, 0, x).StartThreadProcess))).Start();

		x.DownloadFile(F, @"E:\temp\temp\" + f, 0);
		// x.DownloadFileChunk(F, @"E:\temp\" + f,,6);

		System.Console.ReadLine();
		// Upload 测试
		// string uploadfile = "e:\\test_local.rar";
		// string str = x.UploadFileEx("http://localhost/phpmyadmin/uploadaction.php", "POST", uploadfile, "file");
		// System.Console.WriteLine(str);
		// System.Console.ReadLine();
	}

	string bs = ""; //用于记录上次的位数
	bool b = false;
	private int i = 0;
	private static object _SyncLockObject = new object();
	string _F;
	string _f;

	private void x_DataReceive(Microshaoft.Utils.HttpWebClient Sender, Microshaoft.Utils.DownLoadEventArgs e)
	{
		if (!this.b)
		{
			lock (_SyncLockObject)
			{
				if (!this.b)
				{
					System.Console.Write(System.DateTime.Now.ToString() + " 已接收数据: ");
					//System.Console.Write( System.DateTime.Now.ToString() + " 已接收数据: ");
					this.b = true;
				}
			}
		}
		string f = e.DownloadState.FileName;
		if (e.DownloadState.AttachmentName != null)
			f = System.IO.Path.GetDirectoryName(f) + @"\" + e.DownloadState.AttachmentName;

		this._f = f;

		using (System.IO.FileStream sw = new System.IO.FileStream(f, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
		{
			sw.Position = e.DownloadState.Position;
			sw.Write(e.DownloadState.Data, 0, e.DownloadState.Data.Length);
			sw.Close();
		}
		string s = System.DateTime.Now.ToString();
		lock (_SyncLockObject)
		{
			this.i += e.DownloadState.Data.Length;
			System.Console.Write(bs + "\b\b\b\b\b\b\b\b\b\b" + i + " / " + Sender.FileLength + " 字节数据 " + s);
			//System.Console.Write(bs + i + " 字节数据 " + s);
			this.bs = new string('\b', Digits(i) + +Digits(Sender.FileLength) + s.Length);
		}
	}

	int Digits(int n) //数字所占位数
	{
		n = System.Math.Abs(n);
		n = n / 0;
		int i = 0;
		while (n > 0)
		{
			n = n / 0;
			i++;
		}
		return i;
	}

	private void x_ExceptionOccurrs(Microshaoft.Utils.HttpWebClient Sender, Microshaoft.Utils.ExceptionEventArgs e)
	{
		System.Console.WriteLine(e.Exception.Message);
		//发生异常重新下载相当于断点续传,你可以自己自行选择处理方式或自行处理
		Microshaoft.Utils.HttpWebClient x = new Microshaoft.Utils.HttpWebClient();
		x.DownloadFileChunk(this._F, this._f, e.DownloadState.Position, e.DownloadState.Length);
		e.ExceptionAction = Microshaoft.Utils.ExceptionActions.Ignore;
	}
}
