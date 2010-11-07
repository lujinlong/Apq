IF( OBJECT_ID('dbo.Stat_PV', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Stat_PV AS BEGIN RETURN END';
GO
ALTER PROC dbo.Stat_PV
	 @StartTime	datetime
	,@EndTime	datetime
	,@IsAgain	tinyint = 0
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-11-08
-- 描述: 统计UV,PV及首次,最后信息
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Stat_PV '20101105','20101106';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @IsAgain = ISNULL(@IsAgain,0);

--定义变量
DECLARE @rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

DECLARE @BTimeWeek datetime,@BTimeDWeek datetime,@BTimeMonth datetime,@BTimeNMonth datetime;
SELECT @BTimeWeek = dateadd(dd,-7,@EndTime)
	,@BTimeDWeek = dateadd(dd,-14,@EndTime)
	,@BTimeMonth = dateadd(mm,-1,@EndTime)
	,@BTimeNMonth = Convert(nvarchar(8),@StartTime)+'01'
	;
	
-- PV_Imei -----------------------------------------------------------------------------------------
-- 加入新增用户
INSERT dbo.PV_Imei ( Imei,FirstLogType,FirstTime,FirstSource,FirstProvince )
SELECT t.Imei
	,ISNULL((SELECT TOP 1 LogType FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime),0)
	,ISNULL((SELECT TOP 1 LogTime FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime),0)
	,ISNULL((SELECT TOP 1 Source FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime),0)
	,ISNULL((SELECT TOP 1 Province FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime),'未知')
  FROM log.ImeiLog(NOLOCK) t
 WHERE t.LogTime >= @StartTime AND t.LogTime < @EndTime
	AND t.Imei IS NOT NULL
 GROUP BY t.Imei

-- 更新最后访问信息
UPDATE t
   SET t.LastLogType = ISNULL((SELECT TOP 1 LogType FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime DESC),0)
	,t.LastTime = ISNULL((SELECT TOP 1 LogTime FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime DESC),0)
	,t.LastSource = ISNULL((SELECT TOP 1 Source FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime DESC),0)
	,t.LastProvince = ISNULL((SELECT TOP 1 Province FROM log.ImeiLog(NOLOCK) l WHERE l.Imei = t.Imei ORDER BY LogTime DESC),'未知')
  FROM dbo.PV_Imei t

-- 统计最近访问次数
UPDATE t
   SET t.VisitCountWeek = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.Imei = t.Imei AND l.LogTime >= @BTimeWeek AND l.LogTime < @EndTime),0)
   ,t.VisitCountDWeek = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.Imei = t.Imei AND l.LogTime >= @BTimeDWeek AND l.LogTime < @EndTime),0)
   ,t.VisitCountMonth = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.Imei = t.Imei AND l.LogTime >= @BTimeMonth AND l.LogTime < @EndTime),0)
   ,t.VisitCountNMonth = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.Imei = t.Imei AND l.LogTime >= @BTimeNMonth AND l.LogTime < @EndTime),0)
  FROM dbo.PV_Imei t
-- 累加访问总次数
UPDATE t
   SET t.VisitCountTotal = CASE @IsAgain WHEN 1 THEN T.PreVisitCountTotal ELSE t.VisitCountTotal END + ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.Imei = t.Imei AND l.LogTime >= @StartTime AND l.LogTime < @EndTime),0)
	,T.PreVisitCountTotal = CASE @IsAgain WHEN 1 THEN T.PreVisitCountTotal ELSE t.VisitCountTotal END
  FROM dbo.PV_Imei t
-- =================================================================================================

-- PV_Imei_LogType ---------------------------------------------------------------------------------
-- 加入新增用户
INSERT dbo.PV_Imei_LogType ( Imei,LogType,FirstTime,FirstSource,FirstProvince )
SELECT t.Imei,t.LogType
	,ISNULL((SELECT TOP 1 LogTime FROM log.ImeiLog(NOLOCK) l WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime),0)
	,ISNULL((SELECT TOP 1 Source FROM log.ImeiLog(NOLOCK) l WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime),0)
	,ISNULL((SELECT TOP 1 Province FROM log.ImeiLog(NOLOCK) l WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime),'未知')
  FROM log.ImeiLog(NOLOCK) t
 WHERE t.LogTime >= @StartTime AND t.LogTime < @EndTime
	AND t.Imei IS NOT NULL AND t.LogType IS NOT NULL
 GROUP BY t.Imei,t.LogType

-- 更新最后访问信息
UPDATE t
   SET t.LastTime = ISNULL((SELECT TOP 1 LogTime FROM log.ImeiLog(NOLOCK) l WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC),0)
	,t.LastSource = ISNULL((SELECT TOP 1 Source FROM log.ImeiLog(NOLOCK) l WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC),0)
	,t.LastProvince = ISNULL((SELECT TOP 1 Province FROM log.ImeiLog(NOLOCK) l WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC),'未知')
  FROM dbo.PV_Imei_LogType t

-- 统计最近访问次数
UPDATE t
   SET t.VisitCountWeek = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeWeek AND l.LogTime < @EndTime),0)
   ,t.VisitCountDWeek = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeDWeek AND l.LogTime < @EndTime),0)
   ,t.VisitCountMonth = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeMonth AND l.LogTime < @EndTime),0)
   ,t.VisitCountNMonth = ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeNMonth AND l.LogTime < @EndTime),0)
  FROM dbo.PV_Imei_LogType t
-- 累加访问总次数
UPDATE t
   SET t.VisitCountTotal = CASE @IsAgain WHEN 1 THEN T.PreVisitCountTotal ELSE t.VisitCountTotal END
	+ ISNULL((SELECT Count(*) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @StartTime AND l.LogTime < @EndTime),0)
	,T.PreVisitCountTotal = CASE @IsAgain WHEN 1 THEN T.PreVisitCountTotal ELSE t.VisitCountTotal END
  FROM dbo.PV_Imei_LogType t
-- =================================================================================================

RETURN 1;
GO
