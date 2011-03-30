using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Apq.Windows.Forms
{
	/// <summary>
	/// 关于对话框
	/// </summary>
	public partial class AboutBox : ImeForm
	{
		/// <summary>
		/// 显示关于 asm
		/// </summary>
		/// <param name="Parent"></param>
		/// <param name="asm"></param>
		public static void ShowForm(IWin32Window Parent, Assembly asm)
		{
			AboutBox abForm = new AboutBox();
			abForm.Asm = asm;
			abForm.ShowDialog(Parent);
		}
		/// <summary>
		/// 显示关于 Apq.Win
		/// </summary>
		/// <param name="Parent"></param>
		public static void ShowForm(IWin32Window Parent)
		{
			ShowForm(Parent, Apq.Win.GlobalObject.TheAssembly);
		}

		/// <summary>
		/// 关于对话框
		/// </summary>
		public AboutBox()
		{
			InitializeComponent();
		}

		private Assembly _Asm;
		/// <summary>
		/// 获取或设置程序集
		/// </summary>
		public Assembly Asm
		{
			get { return _Asm; }
			set
			{
				_Asm = value;

				Text = string.Format("关于 {0}", AssemblyTitle);
				labelProductName.Text = AssemblyProduct;
				labelVersion.Text = string.Format("版本 {0}", AssemblyVersion);
				labelCopyright.Text = AssemblyCopyright;
				labelCompanyName.Text = AssemblyCompany;
				textBoxDescription.Text = AssemblyDescription;
				logoPictureBox.Image = AssemblyLogoPicture;
			}
		}

		#region 程序集属性访问器

		/// <summary>
		/// 获取程序集标题
		/// </summary>
		public string AssemblyTitle
		{
			get
			{
				// 获取此程序集上的所有 Title 属性
				object[] attributes = Asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// 如果至少有一个 Title 属性
				if (attributes.Length > 0)
				{
					// 请选择第一个属性
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					// 如果该属性为非空字符串，则将其返回
					if (!string.IsNullOrEmpty(titleAttribute.Title))
						return titleAttribute.Title;
				}
				// 如果没有 Title 属性，或者 Title 属性为一个空字符串，则返回 .exe 的名称
				return System.IO.Path.GetFileNameWithoutExtension(Asm.Location);
			}
		}

		/// <summary>
		/// 获取程序集版本
		/// </summary>
		public string AssemblyVersion
		{
			get
			{
				return Asm.GetName().Version.ToString();
			}
		}

		/// <summary>
		/// 获取程序集描述
		/// </summary>
		public string AssemblyDescription
		{
			get
			{
				// 获取此程序集的所有 Description 属性
				object[] attributes = Asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				// 如果 Description 属性不存在，则返回一个空字符串
				if (attributes.Length == 0)
					return "";
				// 如果有 Description 属性，则返回该属性的值
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		/// <summary>
		/// 获取程序集产品
		/// </summary>
		public string AssemblyProduct
		{
			get
			{
				// 获取此程序集上的所有 Product 属性
				object[] attributes = Asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				// 如果 Product 属性不存在，则返回一个空字符串
				if (attributes.Length == 0)
					return "";
				// 如果有 Product 属性，则返回该属性的值
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		/// <summary>
		/// 获取程序集版权
		/// </summary>
		public string AssemblyCopyright
		{
			get
			{
				// 获取此程序集上的所有 Copyright 属性
				object[] attributes = Asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				// 如果 Copyright 属性不存在，则返回一个空字符串
				if (attributes.Length == 0)
					return "";
				// 如果有 Copyright 属性，则返回该属性的值
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		/// <summary>
		/// 获取程序集公司
		/// </summary>
		public string AssemblyCompany
		{
			get
			{
				// 获取此程序集上的所有 Company 属性
				object[] attributes = Asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				// 如果 Company 属性不存在，则返回一个空字符串
				if (attributes.Length == 0)
					return "";
				// 如果有 Company 属性，则返回该属性的值
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}

		/// <summary>
		/// 获取程序集Logo图片
		/// </summary>
		public System.Drawing.Image AssemblyLogoPicture
		{
			get
			{
				string FileFullPath = System.IO.Path.GetFullPath(Asm.Location) + "Res/Pic/Logo.png";
				if (System.IO.File.Exists(FileFullPath))
				{
					return System.Drawing.Image.FromFile(FileFullPath);
				}
				return null;
			}
		}
		#endregion
	}
}
