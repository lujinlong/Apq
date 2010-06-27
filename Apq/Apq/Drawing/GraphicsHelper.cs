using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Apq.Drawing
{
	/// <summary>
	/// 绘图
	/// </summary>
	public class GraphicsHelper
	{
		/// <summary>
		/// 创建验证图片
		/// </summary>
		/// <param name="Width">宽度</param>
		/// <param name="Height">高度</param>
		/// <param name="VerifyCode">验证码值</param>
		/// <returns></returns>
		public static byte[] CreateVerifyGif(string VerifyCode, int Width, int Height)
		{
			int randAngle = 45; //随机转动角度
			int mapWidth = (int)(VerifyCode.Length * 16) > Width ? (int)(VerifyCode.Length * 16) : Width;
			int mapHeight = Height > 24 ? Height : 24;
			System.Drawing.Bitmap map = new System.Drawing.Bitmap(mapWidth, mapHeight);//创建图片背景
			System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(map);
			graph.Clear(System.Drawing.Color.AliceBlue);//清除画面，填充背景
			graph.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
			//graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式

			Random rand = new Random();

			//背景噪点生成
			System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.LightGray, 0);
			for (int i = 0; i < 50; i++)
			{
				int x = rand.Next(0, map.Width);
				int y = rand.Next(0, map.Height);
				graph.DrawRectangle(blackPen, x, y, 1, 1);
			}


			//验证码旋转，防止机器识别
			char[] chars = VerifyCode.ToCharArray();//拆散字符串成单字符数组

			//文字距中
			System.Drawing.StringFormat format = new System.Drawing.StringFormat(System.Drawing.StringFormatFlags.NoClip);
			format.Alignment = System.Drawing.StringAlignment.Center;
			format.LineAlignment = System.Drawing.StringAlignment.Center;

			//定义颜色
			System.Drawing.Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
			//定义字体
			string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

			for (int i = 0; i < chars.Length; i++)
			{
				int cindex = rand.Next(c.Length);
				int findex = rand.Next(font.Length);

				Font f = new System.Drawing.Font(font[findex], 14, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
				Brush b = new System.Drawing.SolidBrush(c[cindex]);

				Point dot = new Point(14, 14);
				//graph.DrawString(dot.X.ToString(),fontstyle,new SolidBrush(Color.Black),10,150);//测试X坐标显示间距的
				float angle = rand.Next(-randAngle, randAngle);//转动的度数

				graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
				graph.RotateTransform(angle);
				graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
				//graph.DrawString(chars[i].ToString(),fontstyle,new SolidBrush(Color.Blue),1,1,format);
				graph.RotateTransform(-angle);//转回去
				graph.TranslateTransform(-2, -dot.Y);//移动光标到指定位置，每个字符紧凑显示，避免被软件识别
			}
			//生成图片
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
			graph.Dispose();
			map.Dispose();

			return ms.ToArray();
		}
	}
}
