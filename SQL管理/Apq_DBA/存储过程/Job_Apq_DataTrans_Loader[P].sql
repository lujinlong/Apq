IF( OBJECT_ID('dbo.Job_Apq_DataTrans_Loader', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_Apq_DataTrans_Loader AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_Apq_DataTrans_Loader
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-08-11
-- 描述: 数据传送作业
-- 参数:
@TransName: 传送名
@TransCycle: 传送周期(天)
@TransTime: 传送时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_DataTrans_Loader;
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

--定义变量
DECLARE @rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@SPBeginTime datetime		-- 存储过程启动时间
	,@Today datetime
	;
SELECT @SPBeginTime=GetDate();
SELECT @Today = DATEADD(DD,0,DATEDIFF(dd,0,@SPBeginTime));

UPDATE DTSConfig
   SET NeedTrans = 1
 WHERE Enabled = 1
	AND (DATEDIFF(MI, LastTransTime, @SPBeginTime) >= 1440 * TransCycle
		OR DateDiff(ss,@SPBeginTime,CONVERT(nvarchar(11),@SPBeginTime,120) + Right(Convert(nvarchar,TransTime,120),8)) BETWEEN -90 AND 209);

-- 检测作业是否卡死在ftp进程上,是则终止ftp进程
UPDATE DTSConfig
   SET KillFtpTime = @SPBeginTime
 WHERE Enabled = 1 AND TransMethod IN (5,6) AND NeedTrans = 1
	AND DATEDIFF(N,TodayBeginTime,@SPBeginTime) > 30
	AND DATEDIFF(N,KillFtpTime,@SPBeginTime) > 60;
IF(@@ROWCOUNT > 0)
BEGIN
	CREATE TABLE #cmd(s nvarchar(4000))
	SELECT @cmd = 'TaskList /nh /fi "Imagename eq ftp.exe"';
	INSERT #cmd
	EXEC @rtn = master..xp_cmdshell @cmd;
	DECLARE @i int, @n int;
	SELECT @i = 0;
	SELECT @n = COUNT(*) FROM #cmd WHERE s IS NOT NULL;
	WHILE(@i < @n)
	BEGIN
		SELECT @cmd = 'TaskKill /im "ftp.exe" /f';
		EXEC master..xp_cmdshell @cmd;
		SELECT @i = @i + 1;
	END
	DROP TABLE #cmd;
END

RETURN 1;
GO
