using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data
{
	/// <summary>
	/// 数据库类型枚举
	/// </summary>
	public enum SqlType
	{
		/// <summary>
		/// BigInt
		/// </summary>
		bigint = 127,
		/// <summary>
		/// binary
		/// </summary>
		binary = 173,
		/// <summary>
		/// bit
		/// </summary>
		bit = 104,
		/// <summary>
		/// char
		/// </summary>
		@char = 175,
		/// <summary>
		/// datetime
		/// </summary>
		datetime = 61,
		/// <summary>
		/// decimal
		/// </summary>
		@decimal = 106,
		/// <summary>
		/// float
		/// </summary>
		@float = 62,
		/// <summary>
		/// image
		/// </summary>
		[Obsolete("请改用varbinary(max)")]
		image = 34,
		/// <summary>
		/// int
		/// </summary>
		@int = 56,
		/// <summary>
		/// money
		/// </summary>
		money = 60,
		/// <summary>
		/// nchar
		/// </summary>
		nchar = 239,
		/// <summary>
		/// ntext
		/// </summary>
		[Obsolete("请改用nvarchar(max)")]
		ntext = 99,
		/// <summary>
		/// nvarchar
		/// </summary>
		nvarchar = 231,
		/// <summary>
		/// numeric
		/// </summary>
		numeric = 108,
		/// <summary>
		/// real
		/// </summary>
		real = 59,
		/// <summary>
		/// smalldatetime
		/// </summary>
		smalldatetime = 58,
		/// <summary>
		/// smallint
		/// </summary>
		smallint = 52,
		/// <summary>
		/// smallmoney
		/// </summary>
		smallmoney = 122,
		/// <summary>
		/// text
		/// </summary>
		[Obsolete("请改用varchar(max)")]
		text = 35,
		/// <summary>
		/// timestamp
		/// </summary>
		timestamp = 189,
		/// <summary>
		/// tinyint
		/// </summary>
		tinyint = 48,
		/// <summary>
		/// uniqueidentifier
		/// </summary>
		uniqueidentifier = 36,
		/// <summary>
		/// varbinary
		/// </summary>
		varbinary = 165,
		/// <summary>
		/// varchar
		/// </summary>
		varchar = 167,
		/// <summary>
		/// xml
		/// </summary>
		xml = 241,
	}
}
