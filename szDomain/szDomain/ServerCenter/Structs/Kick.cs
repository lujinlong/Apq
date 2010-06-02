using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace szDomain.ServerCenter.Structs
{
	/// <summary>
	/// T人结构
	/// </summary>
	public class Kick : ServerStruct, IServerStruct
	{
		/// <summary>
		/// Kick
		/// </summary>
		public Kick()
		{
			RequestID = 3;
		}

		/// <summary>
		/// 最多请求项个数
		/// </summary>
		private static int MaxCount = 10;


		/// <summary>
		/// 请求数组
		/// </summary>
		public Item[] Items;


		/// <summary>
		/// 请求项结构
		/// </summary>
		public class Item
		{
			/// <summary>
			/// 单项长度
			/// </summary>
			public static int Length = 32;

			/// <summary>
			/// ActorName
			/// </summary>
			[MarshalAs(UnmanagedType.LPStr, SizeConst = 32)]
			public string ActorName;
		}

		#region IServerStruct 成员

		/// <summary>
		/// 序列化,用于Socket发送数据
		/// </summary>
		/// <returns></returns>
		public byte[] Serialize()
		{
			byte[] bAll = new byte[4096];
			int Length = 8;

			// bAll[0-3]
			byte[] bRequestID = BitConverter.GetBytes(RequestID);
			bRequestID.CopyTo(bAll, 0);

			// 最大个数限制
			int _Count = Items.Length < MaxCount ? Items.Length : MaxCount;

			// bAll[4-7]
			byte[] bCount = BitConverter.GetBytes(_Count);
			bCount.CopyTo(bAll, 4);

			byte[] bs = null;
			for (int i = 0; i < _Count; i++)
			{
				Item item = Items[i];

				// 以下序列化每个子项,每个接口均需要修改以实现功能
				#region 子项序列化
				bs = STEncoding.GetBytes(item.ActorName);
				bs.CopyTo(bAll, Length);

				Length += Item.Length;	// 子项长度
				#endregion
			}

			// 请求内容
			byte[] rtn = new byte[Length + 4];
			byte[] bLength1 = BitConverter.GetBytes(Length + 2);
			byte[] bLength2 = BitConverter.GetBytes(Length);
			bLength1.CopyTo(rtn, 0);
			bLength2.CopyTo(rtn, 2);
			Array.Copy(bAll, 0, rtn, 4, Length);
			return rtn;
		}

		#endregion
	}
}
