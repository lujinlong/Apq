using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Configuration
{
	/// <summary>
	/// ConfigurationHelper
	/// </summary>
	public class ConfigurationHelper
	{
		/// <summary>
		/// 配置文件
		/// </summary>
		private System.Configuration.Configuration Config;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="Config"></param>
		public ConfigurationHelper(System.Configuration.Configuration Config)
		{
			this.Config = Config;
		}

		/// <summary>
		/// 设置 AppSettings
		/// </summary>
		/// <param name="Key"></param>
		/// <param name="Value"></param>
		public void SetAppSettings(string Key, string Value)
		{
			if (Config.AppSettings.Settings[Key] != null)
			{
				Config.AppSettings.Settings.Remove(Key);
			}
			Config.AppSettings.Settings.Add(Key, Value);
		}
	}
}
