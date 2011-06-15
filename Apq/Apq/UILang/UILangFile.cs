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
		private Apq.Config.clsConfig _cfg = null;

		/// <summary>
		/// 获取或设置配置文件
		/// </summary>
		public string FileName
		{
			set
			{
				string Ext = System.IO.Path.GetExtension(value);
				if (Ext.ToLower() == "ini")
				{
					_cfg = new Apq.Config.IniConfig();
				}
				else
				{
					_cfg = new Apq.Config.XmlConfig();
				}
				_cfg.Open(value);
			}
		}
	}
}
