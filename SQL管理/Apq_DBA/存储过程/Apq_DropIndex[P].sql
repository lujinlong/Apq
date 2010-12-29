IF( OBJECT_ID('dbo.Apq_DropIndex', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_DropIndex AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-04-29
-- 描述: 删除某个表的全部索引并输出每个索引的创建语句
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max), @sql_Create nvarchar(max), @sql_Drop nvarchar(max);
EXEC @rtn = dbo.Apq_DropIndex @ExMsg out, 'dtxc', 'TaskVote_Log', @sql_Create out, @sql_Drop out, 0;
SELECT @rtn, @ExMsg, @sql_Create, @sql_Drop;
-- =============================================
*/
ALTER PROC dbo.Apq_DropIndex
	@ExMsg nvarchar(max) out
	
	,@Schema_Name	nvarchar(512)
	,@Table_Name	nvarchar(512)
	,@sql_Create	nvarchar(max) = '' out
	,@sql_Drop		nvarchar(max) = '' out
	,@DoDrop		tinyint = 1
AS
SET NOCOUNT ON;

SELECT @sql_Create = '', @sql_Drop = '';

DECLARE @sql nvarchar(max), @FullTableName nvarchar(640);
SELECT @sql = '', @FullTableName = @Schema_Name + '.' + @Table_Name;

CREATE TABLE #Apq_DropIndex_t_helpindex(
	index_name	nvarchar(512),
	index_description	varchar(210),
	index_keys	nvarchar(2078),
	IsPS tinyint,
	PSorFG nvarchar(512),
	ignore_dup_key tinyint,
	fill_factor	tinyint,
	IsPrimaryKey	int,
	IsClustered	int,
	IsUnique	int,
	Sql_CREATE	nvarchar(max),
	Sql_DROP	nvarchar(max)
);

INSERT #Apq_DropIndex_t_helpindex(index_name,index_description,index_keys) EXEC sp_helpindex @FullTableName;
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = '[' + REPLACE(index_keys,',','],[');	-- (,)前后加括号,最前面加([)
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = REPLACE(index_keys,'[ ','[');			-- 去掉([)后面的空格
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = REPLACE(index_keys,'(-)','] DESC') + ']';	-- 将(-)替换为排序方式,最后加(])
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = REPLACE(index_keys,'] DESC]','] DESC');	-- 去掉排序方式后多余的(])

UPDATE #Apq_DropIndex_t_helpindex
   SET IsPrimaryKey = OBJECTPROPERTY(OBJECT_ID(@Schema_Name+'.['+index_name+']'),'IsPrimaryKey')
	,IsClustered = INDEXPROPERTY(OBJECT_ID(@FullTableName), index_name, 'IsClustered')
	,IsUnique = INDEXPROPERTY(OBJECT_ID(@FullTableName), index_name, 'IsUnique');

-- 查找索引属性
UPDATE t
   SET t.IsPS = CASE dsi.type WHEN 'FG' THEN 0 WHEN 'PS' THEN 1 ELSE 2 END, t.PSorFG = dsi.name, t.ignore_dup_key = i.ignore_dup_key, t.fill_factor = i.fill_factor
  FROM #Apq_DropIndex_t_helpindex t INNER JOIN sys.indexes i ON t.index_name = i.name
	LEFT JOIN sys.data_spaces AS dsi ON dsi.data_space_id = i.data_space_id
 WHERE i.index_id > 0 and i.is_hypothetical = 0
	AND i.object_id=object_id(@FullTableName);

-- 查找传入分区方案的列
CREATE TABLE #Apq_DropIndex_t_index_PSColumn(
	index_name	nvarchar(512),
	partition_ordinal tinyint,
	name nvarchar(512)
);
INSERT #Apq_DropIndex_t_index_PSColumn ( index_name,partition_ordinal,name )
SELECT index_name = i.name,ic.partition_ordinal,c.name
  FROM sys.indexes AS i
	INNER JOIN sys.index_columns ic ON (ic.partition_ordinal > 0) AND (ic.index_id=CAST(i.index_id AS int) AND ic.object_id=CAST(i.object_id AS int))
	INNER JOIN sys.columns c ON c.object_id = ic.object_id and c.column_id = ic.column_id
 WHERE i.index_id > 0 and i.is_hypothetical = 0
	AND (i.object_id=object_id(@FullTableName))

UPDATE t
   SET t.Sql_DROP = 'ALTER TABLE ' + @FullTableName + ' DROP CONSTRAINT [' + index_name + ']'
	,t.Sql_CREATE = 'ALTER TABLE ' + @FullTableName + ' ADD CONSTRAINT [' + index_name + '] PRIMARY KEY ' + CASE IsClustered WHEN 1 THEN '' ELSE 'NON' END + 'CLUSTERED(' + index_keys + ')'
		+ CASE ignore_dup_key WHEN 1 THEN ' WITH (IGNORE_DUP_KEY = ON)' ELSE '' END
		+' ON '+ CASE WHEN t.PSorFG IS NULL THEN '[PRIMARY]' ELSE '[' + t.PSorFG + ']' END
		+ CASE t.IsPS WHEN 1 THEN '('+ll.PSCols+')' ELSE '' END
  FROM #Apq_DropIndex_t_helpindex t OUTER APPLY (
	SELECT PSCols = STUFF(REPLACE(REPLACE(
		(SELECT name FROM #Apq_DropIndex_t_index_PSColumn ld WHERE ld.index_name = t.index_name FOR XML AUTO),
		'<ld name="', ','), '"/>', ''), 1,1,''
	)) ll
 WHERE IsPrimaryKey = 1;

UPDATE t
   SET t.Sql_DROP = 'DROP INDEX ' + @FullTableName + '.[' + index_name + ']'
	,t.Sql_CREATE = 'CREATE ' + CASE IsUnique WHEN 1 THEN 'UNIQUE ' ELSE '' END
		+ CASE IsClustered WHEN 1 THEN '' ELSE 'NON' END + 'CLUSTERED INDEX [' + index_name + ']'
		+ ' ON ' + @FullTableName + '(' + index_keys + ')'
		+ CASE ignore_dup_key WHEN 1 THEN ' WITH (IGNORE_DUP_KEY = ON)' ELSE '' END
		+' ON '+ CASE WHEN t.PSorFG IS NULL THEN '[PRIMARY]' ELSE '[' + t.PSorFG + ']' END
		+ CASE t.IsPS WHEN 1 THEN '('+ll.PSCols+')' ELSE '' END
  FROM #Apq_DropIndex_t_helpindex t OUTER APPLY (
	SELECT PSCols = STUFF(REPLACE(REPLACE(
		(SELECT name FROM #Apq_DropIndex_t_index_PSColumn ld WHERE ld.index_name = t.index_name FOR XML AUTO),
		'<ld name="', ','), '"/>', ''), 1,1,''
	)) ll
 WHERE Sql_DROP IS NULL;

SELECT @sql_Drop = @sql_Drop + Sql_DROP + '; '
  FROM #Apq_DropIndex_t_helpindex
 ORDER BY IsClustered;

SELECT @sql_Create = @sql_Create + Sql_CREATE + '; '
  FROM #Apq_DropIndex_t_helpindex
 ORDER BY IsClustered DESC;
 
IF(object_id('dbo.Apq_Ext_Get') > 0 AND Len(@sql_Create) < 1)
BEGIN--表无索引时查找记录表
	SELECT @sql_Create = dbo.Apq_Ext_Get(@FullTableName,0,'Apq_DropIndex');
END
IF(object_id('dbo.Apq_Ext_Set') > 0 AND Len(@sql_Create) > 1)
BEGIN--记录索引创建语句
	EXEC dbo.Apq_Ext_Set @FullTableName, 0, 'Apq_DropIndex',@sql_Create;
END

TRUNCATE TABLE #Apq_DropIndex_t_helpindex;
TRUNCATE TABLE #Apq_DropIndex_t_index_PSColumn;
DROP TABLE #Apq_DropIndex_t_helpindex;
DROP TABLE #Apq_DropIndex_t_index_PSColumn;

IF(@DoDrop = 1 AND Len(@sql_Drop) > 1)
BEGIN
	EXEC sp_executesql @sql_Drop;
END

SELECT @ExMsg = '操作成功';
RETURN 1;
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_DropIndex', DEFAULT
GO
