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
		/// <summary>
		/// 已加载到内存的语言配置表
		/// </summary>
		protected XSD.UILang _lst = new XSD.UILang();

		/// <summary>
		/// 获取或设置界面值
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		public string this[string Name]
		{
			get
			{
				XSD.UILang.UILangRow dr = _lst._UILang.FindByname(Name);
				if (dr != null && !string.IsNullOrEmpty(dr.value))
				{
					return dr.value;
				}

				return Name;
			}
			set
			{
				XSD.UILang.UILangRow dr = _lst._UILang.FindByname(Name);
				if (dr == null)
				{
					_lst._UILang.AddUILangRow(Name, string.Empty);
				}

				dr.value = value;
			}
		}

		/// <summary>
		/// 加载语言配置
		/// </summary>
		public virtual void Load()
		{
		}
		
		/// <summary>
		/// 保存语言配置
		/// </summary>
		public virtual void Save()
		{
		}
	}
}
