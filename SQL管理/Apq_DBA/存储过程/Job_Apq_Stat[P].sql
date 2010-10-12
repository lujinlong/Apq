IF( OBJECT_ID('dbo.Job_Apq_Stat', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_Apq_Stat AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_Apq_Stat
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-11-05
-- 描述: 数据统计作业
-- 参数:
@StatName: 统计名
@StatTime: 统计时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_Stat;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @StatName nvarchar(50), -- 统计名
	@StatTime datetime,-- 统计时间
	@LastStatDate datetime -- 上次统计时间
	
	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@SPBeginTime datetime		-- 存储过程启动时间
	;
SELECT @SPBeginTime=GetDate();

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR FOR
SELECT StatName,StatTime
  FROM dbo.StatConfig_Day
 WHERE Enabled = 1 AND DateAdd(dd,2,LastStatDate) < @SPBeginTime -- 至少今天尚未统计
	AND Convert(datetime,CONVERT(nvarchar(11),@SPBeginTime,120) + Right(Convert(nvarchar,StatTime,120),8)) < @SPBeginTime; -- 时间已到

OPEN @csr;
FETCH NEXT FROM @csr INTO @StatName,@StatTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 统计数据
	EXEC @rtn = dbo.Apq_Stat @StatName;

	NEXT_Stat:
	FETCH NEXT FROM @csr INTO @StatName,@StatTime;
END
CLOSE @csr;

RETURN 1;
GO
