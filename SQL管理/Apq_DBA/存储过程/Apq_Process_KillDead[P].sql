IF( OBJECT_ID('dbo.Apq_Process_KillDead', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Process_KillDead AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_Process_KillDead
	 @PName	nvarchar(256) = 'ftp.exe'	-- 进程名
	,@MaxRunMinutes int = 120
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-20
-- 描述: 结束死掉的进程(运行时间超过指定时长(分钟),默认120)
-- 示例:
EXEC dbo.Apq_Process_KillDead 'cmd.exe';
-- =============================================
*/
SET NOCOUNT ON;

SELECT @PName = ISNULL(@PName,'ftp.exe')
	,@MaxRunMinutes = ISNULL(@MaxRunMinutes,120)
	;

DECLARE @rtn int, @SPBeginTime datetime;
SELECT @SPBeginTime=GetDate();

DECLARE @cmd nvarchar(4000)
	;
CREATE TABLE #cmd(
	s	nvarchar(4000)
);

SELECT @cmd = 'wmic process where (name="' + @PName + '") get CreationDate,ProcessId';
EXEC @rtn = xp_cmdshell @cmd;	-- 先运行一次防止未安装
INSERT #cmd EXEC @rtn = xp_cmdshell @cmd;

DECLARE @s nvarchar(4000),@PID int,@StartTime datetime;
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT s FROM #cmd WHERE len(s) > 27 AND Left(s,12) <> 'CreationDate'

OPEN @csr;
FETCH NEXT FROM @csr INTO @s;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @PID = LEFT(Right(@s,12),10),@StartTime = LEFT(@s,8) + ' ' + Substring(@s,9,2)+':' + Substring(@s,11,2)+':'+Substring(@s,13,2);
	IF(datediff(n,@StartTime,@SPBeginTime) > @MaxRunMinutes)
	BEGIN
		SELECT @cmd = 'wmic process where (ProcessId=' + Convert(nvarchar,@PID) + ') delete';
		EXEC xp_cmdshell @cmd;
	END

	FETCH NEXT FROM @csr INTO @s;
END
CLOSE @csr;

DROP TABLE #cmd;
RETURN 1;
GO
