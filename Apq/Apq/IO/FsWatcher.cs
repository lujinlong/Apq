using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Apq.IO
{
	// 无法继承 FileSystemWatcher
	/// <summary>
	/// 文件系统监视器,采用计时器解决Changed事件多次触发问题
	/// </summary>
	public class FsWatcher
	{
		#region 成员或属性
		private System.Threading.Timer _Timer = null;
		/// <summary>
		/// 获取延迟通知的毫秒数
		/// </summary>
		public int ChangedDelayMillis = 1000;

		private FileSystemWatcher _FileSystemWatcher = new FileSystemWatcher();
		/// <summary>
		/// 获取内部监视器对象，仅在开始监视前使用。如，设置Path,Filter等。
		/// 可设置属性：Path,Filter,IncludeSubdirectories,InternalBufferSize,NotifyFilter
		/// </summary>
		public FileSystemWatcher FileSystemWatcher
		{
			get { return _FileSystemWatcher; }
		}
		/// <summary>
		/// 保存当前已发生的Changed事件,仅保留最后一次 完整路径 和 事件参数
		/// </summary>
		private Hashtable _ChangedFullPaths = new Hashtable();
		#endregion

		#region 事件
		/// <summary>
		/// 重命名
		/// </summary>
		public event RenamedEventHandler Renamed;
		/// <summary>
		/// 延迟通知的更改
		/// </summary>
		public event FileSystemEventHandler Changed;
		/// <summary>
		/// 创建
		/// </summary>
		public event FileSystemEventHandler Created;
		/// <summary>
		/// 删除
		/// </summary>
		public event FileSystemEventHandler Deleted;
		#endregion

		#region 构造函数
		/// <summary> 
		/// 文件系统监视器,采用计时器解决Changed事件多次触发问题 
		/// </summary>
		/// <param name="ChangedDelayMillis">延后处理的时长</param>
		public FsWatcher(int ChangedDelayMillis = 1000)
		{
			FileSystemWatcher.EnableRaisingEvents = false;
			this.ChangedDelayMillis = ChangedDelayMillis;

			FileSystemWatcher.Renamed += new RenamedEventHandler(fsWather_Renamed);
			FileSystemWatcher.Changed += new FileSystemEventHandler(fsWather_Changed);
			FileSystemWatcher.Created += new FileSystemEventHandler(fsWather_Created);
			FileSystemWatcher.Deleted += new FileSystemEventHandler(fsWather_Deleted);

			if (_Timer == null)
			{
				//设置定时器的回调函数。此时定时器未启动
				_Timer = new System.Threading.Timer(new System.Threading.TimerCallback(OnWatchedChange),
					null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
			}
		}
		#endregion

		#region 事件转发
		private void fsWather_Renamed(object sender, RenamedEventArgs e)
		{
			if (Renamed != null)
			{
				Renamed(this, e);
			}
		}

		private void fsWather_Created(object sender, FileSystemEventArgs e)
		{
			if (Created != null)
			{
				Created(this, e);
			}
		}

		private void fsWather_Deleted(object sender, FileSystemEventArgs e)
		{
			if (Deleted != null)
			{
				Deleted(this, e);
			}
		}

		private void fsWather_Changed(object sender, FileSystemEventArgs e)
		{
			System.Threading.Mutex mutex = new System.Threading.Mutex(false, "fsw");
			mutex.WaitOne();
			if (!_ChangedFullPaths.ContainsKey(e.FullPath))
			{
				_ChangedFullPaths.Add(e.FullPath, e);
			}
			mutex.ReleaseMutex();

			// 触发计时器
			_Timer.Change(ChangedDelayMillis, System.Threading.Timeout.Infinite);
		}

		protected virtual void OnWatchedChange(object state)
		{
			Hashtable backup = new Hashtable();

			System.Threading.Mutex mutex = new System.Threading.Mutex(false, "fsw");
			mutex.WaitOne();
			foreach (DictionaryEntry de in _ChangedFullPaths)
			{
				backup.Add(de.Key, de.Value);
			}
			_ChangedFullPaths.Clear();
			mutex.ReleaseMutex();

			if (Changed != null)
			{
				foreach (DictionaryEntry de in backup)
				{
					Changed(this, (FileSystemEventArgs)de.Value);
				}
			}
		}
		#endregion

		#region 公开方法
		/// <summary>
		/// 开始监视
		/// </summary>
		public void Start()
		{
			FileSystemWatcher.EnableRaisingEvents = true;
		}

		/// <summary>
		/// 停止监视
		/// </summary>
		public void Stop()
		{
			FileSystemWatcher.EnableRaisingEvents = false;
		}
		#endregion
	}
}
