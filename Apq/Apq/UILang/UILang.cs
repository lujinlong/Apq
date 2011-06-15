using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Apq.UILang
{
	/// <summary>
	/// 界面语言基类
	/// </summary>
	public abstract class UILang
	{
		protected NameValueCollection _lst = new NameValueCollection();

		/// <summary>
		/// 获取或设置界面值
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		public string this[string Name]
		{
			get
			{
				string str = _lst[Name];
				if (string.IsNullOrEmpty(str))
				{
					str = Name;
				}
				return str;
			}
			set
			{
				if (!_lst.AllKeys.Contains<string>(Name))
				{
					_lst.Add(Name, string.Empty);
				}
				_lst[Name] = value;
			}
		}

		/// <summary>
		/// 加载语言配置
		/// </summary>
		public virtual void Load()
		{
		}
	}
}
