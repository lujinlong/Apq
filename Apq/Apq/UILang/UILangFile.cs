using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apq.UILang
{
	/// <summary>
	/// 界面语言(配置文件)
	/// </summary>
	public class UILangFile : UILang
	{
		private string _FileName = string.Empty;

		/// <summary>
		/// 获取或设置语言文件
		/// </summary>
		public string FileName
		{
			get { return _FileName; }
			set
			{
				if (System.IO.File.Exists(value))
				{
					_FileName = value;
					_lst._UILang.ReadXml(_FileName);
				}
			}
		}

		/// <summary>
		/// 加载语言配置
		/// </summary>
		public override void Load()
		{
			if (System.IO.File.Exists(_FileName))
			{
				try
				{
					_lst._UILang.WriteXml(_FileName, System.Data.XmlWriteMode.IgnoreSchema);
				}
				catch { }
			}
		}

		/// <summary>
		/// 保存语言配置
		/// </summary>
		public override void Save()
		{
			if (!string.IsNullOrEmpty(_FileName))
			{
				_lst._UILang.WriteXml(_FileName, System.Data.XmlWriteMode.IgnoreSchema);
			}
		}
	}
}
