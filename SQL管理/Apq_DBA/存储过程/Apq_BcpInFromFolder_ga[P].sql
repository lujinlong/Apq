IF( OBJECT_ID('bcp.Apq_BcpInFromFolder_ga', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bcp.Apq_BcpInFromFolder_ga AS BEGIN RETURN END';
GO
ALTER PROC bcp.Apq_BcpInFromFolder_ga
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-09-23
-- 描述: 从文件夹导入GameActor数据
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bcp.Apq_BcpInFromFolder_ga;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @FullTableName nvarchar(256),	-- 完整表名
	@BcpFolder nvarchar(4000),--数据目录
	@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@FileName nvarchar(4000)		-- 临时变量:数据文件名
	;

SELECT @BcpFolder = 'D:\FTP\GameActor\'
	,@FullTableName = 'Stat_QQHX.bcp.GameActor_Bcp';
-- 接收cmd返回结果
CREATE TABLE #t(s nvarchar(4000));

-- 读取文件
SELECT @cmd = 'dir /a:-d/b/o:n "' + @BcpFolder + 'ga_*.txt"';
INSERT #t(s) EXEC master..xp_cmdshell @cmd;

DECLARE @FileCount int;
SELECT @FileCount = COUNT(*) FROM #t WHERE s IS NOT NULL;
WHILE(@FileCount > 0 )
BEGIN
	SELECT TOP 1 @FileName = s FROM #t;
	DELETE #t WHERE s = @FileName;
	SELECT @FileCount = COUNT(*) FROM #t WHERE s IS NOT NULL;
	
	-- Bcp in
	SELECT @cmd = 'bcp "' + @FullTableName + '" in "' + @BcpFolder + @FileName + '" -c -r~*$';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		CONTINUE;
	END
	SELECT @cmd = 'del /F /Q "' + @BcpFolder + @FileName + '"'
	EXEC master..xp_cmdshell @cmd;
END

DROP TABLE #t;
RETURN 1;
GO
