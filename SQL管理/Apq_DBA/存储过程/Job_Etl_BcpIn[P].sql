IF( OBJECT_ID('etl.Job_Etl_BcpIn', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Job_Etl_BcpIn AS BEGIN RETURN END';
GO
ALTER PROC etl.Job_Etl_BcpIn
	@TransRowCount	int = 10000	-- 从队列中最多读取的行数
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-27
-- 描述: 读取BcpIn队列,执行BcpIn
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn_Init 10000;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @TransRowCount = ISNULL(@TransRowCount,10000);

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	;
	
SELECT @SPBeginTime = getdate();

DECLARE @ID bigint,
	@EtlName nvarchar(256),	-- 配置名
	@Folder nvarchar(512),	-- 本地文件目录(含时期)
	@FileName nvarchar(256),-- 文件名(前缀)(格式:FileName_SrvID.txt)
	@DBName nvarchar(256),
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@r nvarchar(10),
	@t nvarchar(10),
	@DBID int
	
	,@FullTableName nvarchar(512)-- 完整表名(数据库名.架构名.表名)
	;

DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT TOP(@TransRowCount) ID, EtlName, Folder, FileName, DBName, SchemaName, TName, t, r
  FROM etl.BcpInQueue
 WHERE Enabled = 1 AND IsFinished = 0;

DECLARE @sidx int;
CREATE TABLE #t(s nvarchar(4000));

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@FileName,@DBName,@SchemaName,@TName, @t, @r;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 解析出DBID
	SELECT @sidx = Charindex('[',@FileName);
	IF( @sidx > 0)
	BEGIN
		SELECT @DBID = substring(@FileName,@sidx+1,Charindex(']',@FileName,@sidx)-@sidx-1);
		UPDATE etl.BcpInQueue SET DBID = @DBID WHERE ID = @ID;
	END

	SELECT @FullTableName = '[' + @DBName + '].[' + @SchemaName + '].[' + @TName + ']';

	SELECT @cmd = 'bcp "' + @FullTableName + '" in "' + @Folder + @FileName + '" -c -t' + @t + ' -r' + @r + ' -T';
	--SELECT @cmd;
	TRUNCATE TABLE #t;
	INSERT #t
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		GOTO NEXT_File;
	END
	IF(EXISTS(SELECT TOP 1 * FROM #t WHERE left(s,7) = 'Error ='))
	BEGIN
		GOTO NEXT_File;
	END
	
	Success:
	UPDATE etl.BcpInQueue SET [_Time] = getdate(), IsFinished = 1 WHERE ID = @ID;
	SELECT @cmd = 'del "' + @Folder + @FileName + '" /f /q';
	EXEC xp_cmdshell @cmd;
	
	NEXT_File:
	FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@FileName,@DBName,@SchemaName,@TName, @t, @r;
END
CLOSE @csr;

TRUNCATE TABLE #t;
DROP TABLE #t;

RETURN 1;
GO
