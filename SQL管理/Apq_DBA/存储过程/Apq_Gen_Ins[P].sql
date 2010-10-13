IF ( object_id('dbo.Apq_Gen_Ins','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Gen_Ins AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-15
-- 描述: 计算INSERT语句
-- 示例:
EXEC dbo.Apq_Gen_Ins 'dbo.Apq_Ext',1
-- =============================================
*/
ALTER PROC dbo.Apq_Gen_Ins
   @TableName nvarchar(512) = NULL	-- 表全名(架构.表名)
  ,@HasID tinyint = 0	-- 是否包含自增列
  ,@sqlWhere nvarchar(max) = NULL
AS 
SET NOCOUNT ON ;

IF(@HasID IS NULL) SELECT @HasID = 0;
IF(Charindex('.',@TableName) < 2) SELECT @TableName = '[dbo].' + @TableName;
IF(LEN(@sqlWhere) < 2) SELECT @sqlWhere = NULL;

DECLARE @objID int, @FullName nvarchar(512);
SELECT @objID = object_id(@TableName);

DECLARE @sql nvarchar(max),@sqlValues nvarchar(max);
DECLARE @DBName sysname;
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT object_id, '[' + schema_name(schema_id) + '].[' + name + ']'
  FROM sys.objects
 WHERE @objID IS NULL OR object_id = @objID

CREATE TABLE #sql(
	ID bigint IDENTITY(1,1),
	sql nvarchar(max)
);
CREATE TABLE #sqlT(
	ID bigint IDENTITY(1,1),
	sql nvarchar(max)
);

OPEN @csr;
FETCH NEXT FROM @csr INTO @objID,@TableName;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	TRUNCATE TABLE #sqlT
	IF(@HasID = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.columns WHERE object_id = @objID AND is_identity = 1))
	BEGIN
		INSERT #sqlT ( sql )
		SELECT 'SET IDENTITY_INSERT ' + @TableName + ' ON'
	END
	
	SELECT @sql ='('
	SELECT @sqlValues = 'VALUES (''+'
	SELECT @sqlValues = @sqlValues + cols + ' + '','' + ' ,@sql = @sql + '[' + name + '],'
	  FROM (SELECT name,Cols = CASE
				WHEN system_type_id IN (48,52,56,59,60,62,104,106,108,122,127)  --如果是数值型或MOENY型     
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE ' + 'Convert(nvarchar,['+ name + '])'+' END'
				WHEN system_type_id IN (165, 173) -- binary varbinary
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE ''0x'' + ' +  'dbo.Apq_ConvertVarBinary_HexStr([' + name + '])' + ' END'
				WHEN system_type_id IN (58,61) --如果是datetime或smalldatetime类型
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+''''''''' + ' + 'Convert(varchar,['+ name +'],121)'+ '+'''''''''+' END'
				WHEN system_type_id IN (167,175) --如果是varchar类型
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+''''''''' + ' + 'REPLACE(['+ name+'],'''''''','''''''''''')' + '+'''''''''+' END'
				WHEN system_type_id IN (231,239) --如果是nvarchar类型
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+'''N'''''' + ' + 'REPLACE(['+ name+'],'''''''','''''''''''')' + '+'''''''''+' END'
                /*
                WHEN system_type_id IN (175) --如果是CHAR类型
                     THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+''''''''' + ' + 'CAST(REPLACE('+ name+','''''''','''''''''''') AS char(' + cast(max_length as varchar)  + '))+'''''''''+' END'
                WHEN system_type_id IN (239) --如果是NCHAR类型
                     THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+'''N'''''' + ' + 'CAST(REPLACE('+ name+','''''''','''''''''''') AS char(' + cast(max_length as varchar)  + '))+'''''''''+' END'
				*/
				ELSE '''NULL'''
			END
	  FROM sys.columns
	 WHERE object_id = object_id(@TableName) AND (@HasID <> 0 OR is_identity = 0)
	) T
	SELECT @sql ='SELECT ''INSERT INTO '+ @TableName + left(@sql,len(@sql)-1)+') ' + left(@sqlValues,len(@sqlValues)-4) + ')'' FROM '+@TableName + '(NOLOCK)'
	IF(LEN(@sqlWhere) > 1)
		SELECT @sql = @sql + ' WHERE (' + @sqlWhere + ')';
	INSERT #sqlT ( sql )
	EXEC sp_executesql @sql

	IF(@HasID = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.columns WHERE object_id = @objID AND is_identity = 1))
	BEGIN
		INSERT #sqlT ( sql )
		SELECT 'SET IDENTITY_INSERT ' + @TableName + ' OFF'
	END
	
	INSERT #sql ( sql )
	SELECT sql FROM #sqlT ORDER BY ID;

	FETCH NEXT FROM @csr INTO @objID,@TableName;
END
CLOSE @csr;

SELECT sql FROM #sql ORDER BY ID;

TRUNCATE TABLE #sqlT
DROP TABLE #sqlT
TRUNCATE TABLE #sql
DROP TABLE #sql
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_Gen_Ins', DEFAULT
