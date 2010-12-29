IF( OBJECT_ID('etl.Etl_Load', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Etl_Load AS BEGIN RETURN END';
GO
ALTER PROC etl.Etl_Load
	@EtlName	nvarchar(50)
	,@DoLoad	tinyint = 1
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 加载到正式表
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Etl_Load 'ImeiLog';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @DoLoad = ISNULL(@DoLoad,1);

--定义变量
DECLARE @ID bigint
	,@SrcFullTableName nvarchar(256)
	,@DstFullTableName nvarchar(256)
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;

DECLARE @DBName nvarchar(512), @SchemaName nvarchar(512), @TableName nvarchar(512);
DECLARE @sql_Create nvarchar(4000), @sql_Drop nvarchar(4000);

SELECT @ID = ID,@SrcFullTableName=SrcFullTableName,@DstFullTableName=DstFullTableName
  FROM etl.LoadCfg
 WHERE EtlName = @EtlName;
IF(@@ROWCOUNT > 0)
BEGIN
	-- 分解表全名
	SELECT @DBName = ISNULL(PARSENAME(@DstFullTableName,3),'');
	SELECT @SchemaName = ISNULL(PARSENAME(@DstFullTableName,2),'dbo');
	SELECT @TableName = PARSENAME(@DstFullTableName,1);

	-- 目标表去掉索引并记录
	SELECT @sqlDB = '
DECLARE @ExMsg nvarchar(4000);
EXEC dbo.Apq_DropIndex @ExMsg out, @SchemaName, @TableName, @sql_Create out, @sql_Drop out, 1
';
	SELECT @sql = '
EXEC [' + @DBName + ']..sp_executesql @sqlDB
	,N''@SchemaName nvarchar(512), @TableName nvarchar(512), @sql_Create nvarchar(4000) out, @sql_Drop nvarchar(4000) out''
	,@SchemaName = @SchemaName, @TableName = @TableName, @sql_Create = @sql_Create1 out, @sql_Drop = @sql_Drop1 out
';
	EXEC sp_executesql @sql, N'@sqlDB nvarchar(4000), @SchemaName nvarchar(512), @TableName nvarchar(512), @sql_Create1 nvarchar(4000) out, @sql_Drop1 nvarchar(4000) out'
		,@sqlDB = @sqlDB, @SchemaName = @SchemaName, @TableName = @TableName, @sql_Create1 = @sql_Create out, @sql_Drop1 = @sql_Drop out;
	PRINT @sql_Create;

	-- 加载表
	SELECT @sql = '
/*
-- 渐进加载
WHILE(1=1)
BEGIN
	INSERT ' + @DstFullTableName + '
	SELECT TOP(10000) *
	  FROM ' + @SrcFullTableName + ';
	IF(@@ROWCOUNT = 0) BREAK;
	
	DELETE TOP(10000)
	  FROM ' + @SrcFullTableName + ';
END
*/

-- 一次加载
INSERT ' + @DstFullTableName + '
SELECT *
  FROM ' + @SrcFullTableName + ';

TRUNCATE TABLE ' + @SrcFullTableName + ';
';
	PRINT @sql;
	SELECT @sql;
	IF(@DoLoad = 1)
	BEGIN
		EXEC sp_executesql @sql, N'@DBName nvarchar(512), @SchemaName nvarchar(512), @TableName nvarchar(512)'
			,@DBName = @DBName, @SchemaName = @SchemaName, @TableName = @TableName;
	END

	-- 目标表还原索引
	IF(Len(@sql_Create) > 1)
	BEGIN
		SELECT @sql = '
EXEC [' + @DBName + ']..sp_executesql @sql_Create;
';
		EXEC sp_executesql @sql, N'@sql_Create nvarchar(4000)'
			,@sql_Create = @sql_Create;
	END
	
	UPDATE etl.LoadCfg SET _Time = getdate() WHERE ID = @ID;
END

RETURN 1;
GO
