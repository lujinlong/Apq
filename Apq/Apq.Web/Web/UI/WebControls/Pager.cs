using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Apq.Web.UI.WebControls
{
	/// <summary>
	/// 
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
	public class Pager : WebControl
	{
		/// <summary>
		/// 分页界面
		/// </summary>
		public Pager()
			: base("div")
		{
			this.CssClass = "Apq_Pager";
		}

		/// <summary>
		/// 获取或设置控件内容
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				string s = (string)ViewState["Text"];
				return ((s == null) ? string.Empty : s);
			}

			set
			{
				ViewState["Text"] = value;
			}
		}

		/// <summary>
		/// 获取或设置总行数
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public long Total
		{
			get
			{
				return Apq.Convert.ChangeType<long>(ViewState["Total"]);
			}

			set
			{
				ViewState["Total"] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
		}

		/// <summary>
		/// 将控件的内容呈现到指定的编写器中。
		/// </summary>
		/// <param name="output"></param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(Text);
		}
	}
}
