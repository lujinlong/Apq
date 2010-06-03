using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Reflection;

namespace Apq_MP4
{
	public partial class Rename : Apq.Windows.Forms.ImeForm
	{
		System.Configuration.Configuration ConfigApq;
		public Rename()
		{
			InitializeComponent();
		}

		private void Rename_Load(object sender, EventArgs e)
		{
			Assembly asm = Assembly.GetAssembly(typeof(Apq.Configuration.Configs));
			ConfigApq = Apq.Configuration.Configs.GetConfig(asm);
			this.ToolTip.SetToolTip(this.txtInput, "请输入正则表达式");

			Init();
		}

		private void Init()
		{
			if (ConfigApq != null)
			{
				fb.txtPath.TextChanged += new EventHandler(txtPath_TextChanged);
				if (ConfigApq.AppSettings.Settings["LastFolder"] != null)
				{
					fb.SelectedPath = ConfigApq.AppSettings.Settings["LastFolder"].Value;
				}
			}
		}

		void txtPath_TextChanged(object sender, EventArgs e)
		{
			if (fb.Value.Length > 0)
			{
				Apq.Configuration.ConfigurationHelper ConfigHelper = new Apq.Configuration.ConfigurationHelper(ConfigApq);
				ConfigHelper.SetAppSettings("LastFolder", fb.Value);

				ConfigApq.Save(ConfigurationSaveMode.Minimal);
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.fb.SelectedPath))
				{
					throw new System.Exception("请指定路径!");
				}

				RenameFiles(this.fb.SelectedPath, this.txtInput.Text, this.txtReplace.Text, this.cbChildren.Checked, this.cbFont.Checked);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 批量重命名
		/// </summary>
		/// <param name="Path">目录</param>
		/// <param name="input">匹配串</param>
		/// <param name="replacement">替换串</param>
		/// <param name="All">是否包含下级</param>
		/// <param name="Font">是否繁体转简体</param>
		public static void RenameFiles(string Path, string input, string replacement, bool All, bool Font)
		{
			try
			{
				Regex reg = new Regex(input);
				string[] Files = Directory.GetFiles(Path);
				foreach (string file in Files)
				{
					string Dir = System.IO.Path.GetDirectoryName(file);
					if (Dir.Substring(Dir.Length - 1) != @"\")
					{
						Dir += @"\";
					}
					string FileName = System.IO.Path.GetFileName(file);
					string NewFullName = reg.Replace(FileName, replacement);
					if (Font)
					{
						NewFullName = Microsoft.VisualBasic.Strings.StrConv(NewFullName, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
					}
					File.Move(file, Dir + NewFullName);
				}

				string[] Directorys = Directory.GetDirectories(Path);
				foreach (string dir in Directorys)
				{
					string Parent = System.IO.Path.GetDirectoryName(dir);
					if (Parent.Substring(Parent.Length - 1) != @"\")
					{
						Parent += @"\";
					}
					string DirName = dir.Substring(dir.LastIndexOf(@"\"));
					string NewFullDirName = reg.Replace(DirName, replacement);
					if (Font)
					{
						NewFullDirName = Microsoft.VisualBasic.Strings.StrConv(NewFullDirName, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
					}
					if (DirName.ToLower() != NewFullDirName.ToLower())
					{
						Directory.Move(dir, Parent + NewFullDirName);
					}

					if (All)
					{
						RenameFiles(NewFullDirName, input, replacement, true, Font);
					}
				}
			}
			catch (System.UnauthorizedAccessException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}