using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Data.Dev
{
	/// <summary>
	/// Sqls
	/// </summary>
	public class Sqls
	{
		/// <summary>
		/// Apq_Tables 创建
		/// </summary>
		public const string Apq_TablesCreate = @"
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE {0}(
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DBName] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[name] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[NextID] [bigint] NOT NULL CONSTRAINT [DF_Apq_Tables_NextID]  DEFAULT ((1)),
 CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Apq_Tables, @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Apq_Tables, @level2type=N'COLUMN',@level2name=N'DBName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Apq_Tables, @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下一ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Apq_Tables, @level2type=N'COLUMN',@level2name=N'NextID'
";
		/// <summary>
		/// Apq_Tables 更新
		/// </summary>
		public const string Apq_TablesUpdate = @"
-- 删除 删除的表
DELETE T0
FROM {0} T0 LEFT JOIN 
	(SELECT * FROM {2}.sys.objects WHERE type LIKE 'U' OR type LIKE 'V'
	) obj ON T0.name LIKE obj.name
WHERE T0.DBName LIKE '{2}' AND obj.name IS NULL

-- 添加 新增的表
INSERT INTO {0}(DBName, name)
SELECT '{2}', obj.name
FROM {0} T0 RIGHT JOIN 
	(SELECT * FROM {2}.sys.objects WHERE type LIKE 'U' OR type LIKE 'V'
	) obj ON T0.DBName LIKE '{2}' AND T0.name LIKE obj.name
WHERE T0.name IS NULL
";
		/// <summary>
		/// Apq_Columns 创建
		/// </summary>
		public const string Apq_ColumnsCreate = @"
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE {0}(
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TableID] [bigint] NULL,
	[column_name] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[column_id] [int] NOT NULL,
	[system_type_id] [tinyint] NOT NULL,
	[is_primary_key] [bit] NULL,
	[is_nullable] [bit] NULL,
	[is_identity] [bit] NOT NULL,
	[is_computed] [bit] NOT NULL,
	[max_length] [smallint] NOT NULL,
	[Precision] [int] NULL,
	[Scale] [int] NULL,
	[Default] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[Type] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'TableID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'column_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'column_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'system_type_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'is_primary_key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'is_nullable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'is_identity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否计算' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'is_computed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'max_length'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'精度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'Precision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小数位数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'Scale'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'Default'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Columns, @level2type=N'COLUMN',@level2name=N'Type'
";
		/// <summary>
		/// Apq_Columns 更新
		/// </summary>
		public const string Apq_ColumnsUpdate = @"
-- 删除已不存在的表的所有列
DELETE T1
FROM {1} T1
WHERE T1.TableID NOT IN (SELECT ID FROM {0} T0 )

-- 删除多余的列
DELETE T1
FROM {1} T1 INNER JOIN {0} T0 ON T0.DBName LIKE '{2}' AND T1.TableID = T0.ID LEFT JOIN 
	(SELECT obj.name TName, c.name CName
	FROM {2}.sys.columns c INNER JOIN {2}.sys.objects obj ON c.[object_id] = obj.[object_id] AND (obj.type LIKE 'U' OR obj.type LIKE 'V')
	) f ON f.TName LIKE T0.name AND f.CName LIKE T1.column_name
WHERE f.CName IS NULL

-- 修改所有已存在的列
UPDATE T1
SET --T1.TableID = T0.ID
	--T1.column_name = f.column_name
	T1.column_id = f.column_id,
	T1.system_type_id = f.system_type_id,
	T1.is_primary_key = f.is_primary_key,
	T1.is_nullable = f.is_nullable,
	T1.is_identity = f.is_identity,
	T1.is_computed = f.is_computed,
	T1.max_length = f.max_length,
	T1.Precision = f.Precision,
	T1.Scale = f.Scale,
	T1.[Default] = f.[Default]
--	,T1.Type = f.Type--待加
FROM {1} T1 INNER JOIN {0} T0 ON T0.DBName LIKE '{2}' AND T1.TableID = T0.ID INNER JOIN 
	(SELECT obj.name, -- 表名视需要使用
		c.name column_name, c.column_id, c.system_type_id, ind.is_primary_key, c.is_nullable, c.is_identity, c.is_computed, c.max_length, 
		COLUMNPROPERTY( c.[object_id], c.[name], 'Precision' ) Precision, 
		COLUMNPROPERTY( c.[object_id], c.[name], 'Scale' ) Scale, d.definition [Default] 
	FROM {2}.sys.columns c 
		INNER JOIN {2}.sys.objects obj on c.[object_id] = obj.[object_id] AND (obj.type LIKE 'U' OR obj.type LIKE 'V')
		LEFT JOIN {2}.sys.default_constraints d on c.default_object_id = d.[object_id] 
		LEFT JOIN {2}.sys.index_columns ic ON c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id 
		LEFT JOIN {2}.sys.indexes ind ON c.[object_id] = ind.[object_id] AND ic.index_id = ind.index_id 
	--WHERE obj.[name] LIKE 'TableName'	-- 需要查看某个表信息时添加此条件
	) f ON f.name LIKE T0.name AND f.column_name LIKE T1.column_name

-- 添加 不存在的表的 列
INSERT INTO {1} (
	TableID, column_name, column_id, system_type_id, is_primary_key, is_nullable, is_identity, is_computed, max_length, Precision, Scale, [Default]--, Type
)
SELECT T0.ID, f.column_name, f.column_id, f.system_type_id, f.is_primary_key, f.is_nullable, f.is_identity, f.is_computed, f.max_length, f.Precision, f.Scale, f.[Default]--, f.Type
FROM {0} T0 INNER JOIN 
	(SELECT obj.name, -- 表名视需要使用
		c.name column_name, c.column_id, c.system_type_id, ind.is_primary_key, c.is_nullable, c.is_identity, c.is_computed, c.max_length, 
		COLUMNPROPERTY( c.[object_id], c.[name], 'Precision' ) Precision, 
		COLUMNPROPERTY( c.[object_id], c.[name], 'Scale' ) Scale, d.definition [Default] 
	FROM {2}.sys.columns c 
		INNER JOIN {2}.sys.objects obj on c.[object_id] = obj.[object_id] AND (obj.type LIKE 'U' OR obj.type LIKE 'V')
		LEFT JOIN {2}.sys.default_constraints d on c.default_object_id = d.[object_id] 
		LEFT JOIN {2}.sys.index_columns ic ON c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id 
		LEFT JOIN {2}.sys.indexes ind ON c.[object_id] = ind.[object_id] AND ic.index_id = ind.index_id 
	--WHERE obj.[name] LIKE 'TableName'	-- 需要查看某个表信息时添加此条件
	) f ON T0.DBName LIKE '{2}' AND T0.name LIKE f.name
	LEFT JOIN {1} T1 ON T0.ID = T1.TableID AND f.column_name LIKE T1.column_name
WHERE T1.TableID IS NULL
";
	}
}
