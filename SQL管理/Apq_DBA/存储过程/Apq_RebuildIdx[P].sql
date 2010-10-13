IF ( OBJECT_ID('dbo.Apq_RebuildIdx','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_RebuildIdx AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-05
-- 功能: 索引重命名,重建/整理本库索引
-- 描述:
	重命名规则:
		主键:	PK_表名
		非主键:	IX_表名:列名{,...}
		
	如果碎片率低于30％用 选项REORGANIZE / INDEXDEFRAG,如果高于30％用 选项REBUILD / DBREINDEX
-- 示例:
EXEC dbo.Apq_RebuildIdx
-- =============================================
*/
ALTER PROC dbo.Apq_RebuildIdx
AS 
SET NOCOUNT ON ;

DECLARE @objectid int,@indexid int,@partitioncount bigint,@schemaname sysname,@objectname sysname,@indexname sysname,@partitionnum bigint,@partitions bigint
	,@frag float,@is_unique tinyint,@is_primary_key tinyint
	,@idxNameOld nvarchar(776),@idxNameNew nvarchar(776)-- 新旧索引全名(架构.表名.索引名)
	;

DECLARE @sql nvarchar(max), @ColNames_idx nvarchar(max);
SELECT t.object_id,t.index_id,partition_number,avg_fragmentation_in_percent,ColNames=Convert(nvarchar(max),N'')
  INTO #Apq_RebuildIdx
  FROM sys.dm_db_index_physical_stats(DB_ID(),NULL,NULL,NULL,'LIMITED') t
 WHERE t.index_id > 0 ;

UPDATE t
   SET t.ColNames = s.ColNames
  FROM #Apq_RebuildIdx t INNER JOIN (
	SELECT * FROM (SELECT DISTINCT object_id,index_id FROM sys.index_columns) A OUTER APPLY( 
		SELECT ColNames= STUFF(REPLACE(REPLACE( 
			(SELECT c.name FROM sys.index_columns ic INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
			  WHERE ic.object_id = A.object_id AND ic.index_id = A.index_id
			  ORDER BY c.object_id,ic.index_id,ic.index_column_id
				FOR XML AUTO 
			), '<c name="', ','), '"/>', ''), 1, 1, '') 
		) r
	) s ON t.object_id = s.object_id AND t.index_id = s.index_id;

-- Declare the cursor for the list of partitions to be processed.
DECLARE @csr_partitions CURSOR
SET @csr_partitions = CURSOR FOR
SELECT object_id,index_id,partition_number,avg_fragmentation_in_percent,ColNames
  FROM #Apq_RebuildIdx ;

-- Open the cursor.
OPEN @csr_partitions ;

-- Loop through the @csr_partitions.
FETCH NEXT FROM @csr_partitions INTO @objectid, @indexid, @partitionnum, @frag,@ColNames_idx;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @objectname = o.name,@schemaname = s.name
	  FROM sys.objects AS o JOIN sys.schemas AS s ON s.schema_id = o.schema_id
	 WHERE o.object_id = @objectid ;

	SELECT @indexname = name, @is_unique = is_unique,@is_primary_key = is_primary_key
		,@idxNameOld = '[' + @schemaname + '].[' + @objectname + '].[' + name + ']'
	  FROM sys.indexes
	 WHERE object_id = @objectid AND index_id = @indexid ;

	SELECT @partitioncount = count(*)
	  FROM sys.partitions
	 WHERE object_id = @objectid AND index_id = @indexid ;

	BEGIN TRY
		-- 重命名
		SELECT @idxNameNew = 'IX_' + @objectname + ':' + @ColNames_idx;
		IF(@is_primary_key = 1)
			SELECT @idxNameNew = 'PK_' + @objectname;
		EXEC sp_rename @idxNameOld,@idxNameNew,'INDEX';
		
		-- (10,30)	整理索引
		IF(@frag > 10.0 AND @frag < 30.0)
		BEGIN
			SELECT  @sql = 'ALTER INDEX [' + @idxNameNew + '] ON [' + @schemaname + '].[' + @objectname + '] REORGANIZE';
			IF(@partitioncount > 1)
				SELECT  @sql = @sql + ' PARTITION=' + CONVERT(nvarchar(10),@partitionnum) ;
		END
		-- [30,]	重建索引
		IF(@frag >= 30.0)
		BEGIN
			SELECT @sql = 'ALTER INDEX [' + @idxNameNew + '] ON [' + @schemaname + '].[' + @objectname + '] REBUILD';
			IF(@partitioncount)> 1 
				SELECT  @sql = @sql + ' PARTITION=' + CONVERT (nvarchar(10),@partitionnum) ;

			/*
			填充因子(百分比)
			1.低更改的表(读写比率为100:1以上)	100
			2.高更改的表(写超过读)				50-70
			3.读写各一半						80-90
			*/
			IF(@is_unique = 0)
				SELECT @sql = @sql + ' WITH(PAD_INDEX = ON, FILLFACTOR = 75)';
		END
		
		EXEC sp_executesql @sql ;
		PRINT 'Executed ' + @sql ;
	END TRY
	BEGIN CATCH
		PRINT '处理失败'
	END CATCH

	FETCH NEXT FROM @csr_partitions INTO @objectid, @indexid, @partitionnum, @frag,@ColNames_idx;
END
-- Close and deallocate the cursor.
CLOSE @csr_partitions

-- drop the temporary table
DROP TABLE #Apq_RebuildIdx
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_RebuildIdx', DEFAULT
GO
