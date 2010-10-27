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
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	;
	
SELECT @SPBeginTime = getdate();

DECLARE @ID bigint,
	@EtlName nvarchar(256),	-- 配置名
	@Folder nvarchar(512),	-- 本地文件目录(含时期)
	@FileName nvarchar(256),-- 文件名(前缀)(格式:FileName_SrvID.txt)
	@FullTableName nvarchar(512),-- 完整表名(数据库名.架构名.表名)
	@HasSTable tinyint
	;

DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT TOP(@TransRowCount) ID, EtlName, Folder, FileName, FullTableName, HasSTable
  FROM etl.BcpInQueue
 WHERE Enabled = 1 AND IsFinished = 0;

CREATE TABLE #t(s nvarchar(4000));

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@FileName,@FullTableName,@HasSTable;
WHILE(@@FETCH_STATUS=0)
BEGIN
	SELECT @cmd = 'bcp "' + @FullTableName + '" in "' + @Folder + @FileName + '" -c -t\t -r\n -T';
	
	Success:
	UPDATE etl.BcpInQueue SET IsFinished = 1 WHERE ID = @ID;
	SELECT @cmd = 'del "' + @Folder + @FileName + '" /f /q';
	EXEC xp_cmdshell @cmd;
	
	NEXT_File:
	FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@FileName,@FullTableName,@HasSTable;
END
CLOSE @csr;

RETURN 1;
GO
