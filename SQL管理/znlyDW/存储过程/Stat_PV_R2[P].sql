IF( OBJECT_ID('dbo.Stat_PV_R2', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Stat_PV_R2 AS BEGIN RETURN END';
GO
ALTER PROC dbo.Stat_PV_R2
	 @StartTime	datetime
	,@EndTime	datetime
	,@IsAgain	tinyint = 0
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-11-16
-- 描述: 统计UV,PV及首次,最后信息
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Stat_PV_R2 '20101105','20101106';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @IsAgain = ISNULL(@IsAgain,0);

--定义变量
DECLARE @rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000),@Now datetime, @strNow nvarchar(50)
	;

DECLARE @BTimeWeek datetime,@BTimeDWeek datetime,@BTimeMonth datetime,@BTimeNMonth datetime
SELECT @BTimeWeek = dateadd(dd,-7,@EndTime)
	,@BTimeDWeek = dateadd(dd,-14,@EndTime)
	,@BTimeMonth = dateadd(mm,-1,@EndTime)
	,@BTimeNMonth = CASE datepart(dd,@EndTime) WHEN 1 THEN Convert(nvarchar(8),@StartTime,120) ELSE Convert(nvarchar(8),@EndTime,120) END+'01'
	;

DECLARE @BID bigint, @EID bigint, @CID bigint, @CIDE bigint;
DECLARE @ExMsg nvarchar(max),@sql_Create nvarchar(max), @sql_Drop nvarchar(max)

--SELECT @BTimeWeek, @BTimeDWeek,@BTimeMonth,@BTimeNMonth;
-- 记录时间点1
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time01',@strNow;

-- PV_Imei_LogType ---------------------------------------------------------------------------------
SELECT @CID = -1,@sql_Create='',@sql_Drop='';
SELECT @BID = min(l.ID),@EID = max(l.ID) FROM log.ImeiLog l(NOLOCK) WHERE l.LogTime >= @StartTime AND l.LogTime < @EndTime;
IF(@BID IS NULL OR @EID IS NULL) RETURN -1;
IF(@IsAgain = 0) SELECT @CID = ISNULL(dbo.Apq_Ext_Get('PV_Stat',0,'PV_Imei_LogType_First_CID_ImeiLog'),@BID);
IF(@IsAgain = 1) SELECT @CID = @BID;
IF(NOT @CID BETWEEN @BID AND @EID)
BEGIN
	SELECT @CID = @BID;
END
PRINT '[1]'+Convert(nvarchar(21),@BID)+','+Convert(nvarchar(21),@CID)+','+Convert(nvarchar(21),@EID)

-- 记录时间点2
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time02',@strNow;

-- 加入新增用户
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		DELETE TOP(10000) t
		  FROM dbo.PV_Imei_LogType_First t
		 WHERE t.FirstTime >= @StartTime
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

-- 记录时间点3
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time03',@strNow;

WHILE(@CID <= @EID)
BEGIN
	--SELECT 1,@BID,@CID,@EID;
	PRINT '[1]'+Convert(nvarchar(21),@CID);
	EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'PV_Imei_LogType_First_CID_ImeiLog',@CID;
	
	TRUNCATE TABLE dbo.PV_Imei_LogType_First1;
	
	SELECT @CIDE = @CID + 10000;
	INSERT dbo.PV_Imei_LogType_First1 ( Imei,LogType,FirstTime,FirstPlatform,FirstSMSC,FirstProvince,FristPlatformDate,FirstImeiLogID )
	SELECT l.Imei,l.LogType,l.LogTime,l.[Platform],l.SMSC,l.Province,PlatformDate,l.ID
	  FROM log.ImeiLog l(NOLOCK)
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_LogType_First d WHERE d.LogType = l.LogType AND d.Imei = l.Imei)
		AND l.LogTime >= @StartTime AND l.LogTime < @EndTime
		AND l.ID >= @CID AND l.ID < @CIDE
		AND l.Imei IS NOT NULL AND l.LogType IS NOT NULL
	 ORDER BY l.LogTime;
	INSERT dbo.PV_Imei_LogType_First ( Imei,LogType,FirstTime,FirstPlatform,FirstSMSC,FirstProvince,FristPlatformDate,FirstImeiLogID )
	SELECT Imei,LogType,FirstTime,FirstPlatform,FirstSMSC,FirstProvince,FristPlatformDate,FirstImeiLogID
	  FROM dbo.PV_Imei_LogType_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_LogType_First d WHERE d.LogType = l.LogType AND d.Imei = l.Imei)
	INSERT dbo.PV_Imei_LogType ( Imei,LogType )
	SELECT Imei,LogType
	  FROM dbo.PV_Imei_LogType_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_LogType d WHERE d.LogType = l.LogType AND d.Imei = l.Imei)
	INSERT dbo.PV_Imei_LogType_Last ( Imei,LogType )
	SELECT Imei,LogType
	  FROM dbo.PV_Imei_LogType_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_LogType_Last d WHERE d.LogType = l.LogType AND d.Imei = l.Imei)
	INSERT dbo.PV_Imei_LogType_SLast ( Imei,LogType )
	SELECT Imei,LogType
	  FROM dbo.PV_Imei_LogType_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_LogType_SLast d WHERE d.LogType = l.LogType AND d.Imei = l.Imei)
	
	SELECT @CID = @CIDE;
END

-- 插入完成,记录最后ID
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'PV_Imei_LogType_First_CID_ImeiLog',@EID;

-- 记录时间点4
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time04',@strNow;

-- PV
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		UPDATE TOP(10000) t
		   SET _Time = getdate(), VisitCount_Time = dateadd(n,-1,@EndTime)
		  FROM dbo.PV_Imei_LogType t
		WHERE t.VisitCount_Time >= @EndTime;
		IF(@@ROWCOUNT = 0) BREAK;
	END
END
WHILE(1=1)
BEGIN
	UPDATE TOP(10000) t
	   SET _Time = getdate(), VisitCount_Time = @EndTime
		,t.VisitCountWeek = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeWeek AND l.LogTime < @EndTime),0)
		,t.VisitCountDWeek = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeDWeek AND l.LogTime < @EndTime),0)
		,t.VisitCountMonth = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeMonth AND l.LogTime < @EndTime),0)
		,t.VisitCountNMonth = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeNMonth AND l.LogTime < @EndTime),0)
		,t.VisitCountTotal = CASE @IsAgain WHEN 1 THEN T.PreVisitCountTotal ELSE t.VisitCountTotal END
			+ ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @StartTime AND l.LogTime < @EndTime),0)
	  FROM dbo.PV_Imei_LogType t
	 WHERE t.VisitCount_Time < @EndTime;
	IF(@@ROWCOUNT = 0) BREAK;
END

-- 记录时间点5
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time05',@strNow;

-- Last
TRUNCATE TABLE dbo.PV_Imei_LogType_Last1;
INSERT dbo.PV_Imei_LogType_Last1(Imei, LogType, LastImeiLogID)
SELECT Imei, LogType, Max(ID)
  FROM log.ImeiLog(NOLOCK)
 WHERE LogTime >= @StartTime AND LogTime < @EndTime
	AND Imei IS NOT NULL AND LogType IS NOT NULL
 GROUP BY Imei, LogType
UPDATE t
   SET t.LastTime = l.LogTime
	,t.LastPlatform = l.[Platform]
	,t.LastPlatformDate = l.PlatformDate
	,t.LastSMSC = l.SMSC
	,t.LastProvince = l.Province
  FROM dbo.PV_Imei_LogType_Last1 t INNER JOIN log.ImeiLog l(NOLOCK) ON t.Imei = l.Imei AND t.LogType = l.LogType
-- 记录时间点6
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time06',@strNow;
UPDATE t
   SET _Time = getdate()
	,t.LastImeiLogID = l.LastImeiLogID
	,t.SLastImeiLogID = t.LastImeiLogID
  FROM dbo.PV_Imei_LogType_Last t INNER JOIN dbo.PV_Imei_LogType_Last1 l(NOLOCK) ON t.Imei = l.Imei AND t.LogType = l.LogType
 WHERE t.LastImeiLogID <> l.LastImeiLogID
UPDATE t	-- SLast
   SET _Time = getdate()
	,t.SLastImeiLogID = l.LastImeiLogID
	,t.SLastTime = l.LastTime
	,t.SLastPlatform = l.LastPlatform
	,t.SLastPlatformDate = l.LastPlatformDate
	,t.SLastSMSC = l.LastSMSC
	,t.SLastProvince = l.LastProvince
  FROM dbo.PV_Imei_LogType_SLast t INNER JOIN dbo.PV_Imei_LogType_Last l(NOLOCK) ON t.Imei = l.Imei AND t.LogType = l.LogType
 WHERE l.LastImeiLogID <> l.SLastImeiLogID
UPDATE t
   SET _Time = getdate()
	,t.LastTime = l.LastTime
	,t.LastPlatform = l.LastPlatform
	,t.LastPlatformDate = l.LastPlatformDate
	,t.LastSMSC = l.LastSMSC
	,t.LastProvince = l.LastProvince
  FROM dbo.PV_Imei_LogType_Last t INNER JOIN dbo.PV_Imei_LogType_Last1 l(NOLOCK) ON t.Imei = l.Imei AND t.LogType = l.LogType

-- 记录时间点7
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time07',@strNow;
-- =================================================================================================

-- PV_Imei -----------------------------------------------------------------------------------------
SELECT @CID = -1;
SELECT @BID = min(l.ID),@EID = max(l.ID) FROM dbo.PV_Imei_LogType_First l(NOLOCK) WHERE l.FirstTime >= @StartTime AND l.FirstTime < @EndTime;
IF(@BID IS NULL OR @EID IS NULL) RETURN -1;
IF(@IsAgain = 0) SELECT @CID = ISNULL(dbo.Apq_Ext_Get('PV_Stat',0,'PV_Imei_First_CID_ImeiLog'),@BID);
IF(@IsAgain = 1) SELECT @CID = @BID;
IF(NOT @CID BETWEEN @BID AND @EID)
BEGIN
	SELECT @CID = @BID;
END
PRINT '[2]'+Convert(nvarchar(21),@BID)+','+Convert(nvarchar(21),@CID)+','+Convert(nvarchar(21),@EID)

-- 记录时间点8
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time08',@strNow;

-- 加入新增用户
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		DELETE TOP(10000) t
		  FROM dbo.PV_Imei_First t
		 WHERE t.FirstTime >= @StartTime-- AND t.FirstTime < @EndTime
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

WHILE(@CID <= @EID)
BEGIN
	--SELECT 2,@BID,@CID,@EID;
	PRINT '[2]'+Convert(nvarchar(21),@CID);
	EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'PV_Imei_First_CID_ImeiLog',@CID;
	
	TRUNCATE TABLE dbo.PV_Imei_First1;
	
	SELECT @CIDE = @CID + 10000;
	INSERT dbo.PV_Imei_First1 ( Imei,FirstLogType,FirstTime,FirstPlatform,FristPlatformDate,FirstSMSC,FirstProvince,FristImeiLogID )
	SELECT t.Imei,ISNULL(LogType,0),FirstTime,FirstPlatform,FristPlatformDate,FirstSMSC,FirstProvince,FirstImeiLogID
	  FROM dbo.PV_Imei_LogType_First t(NOLOCK)
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei d(NOLOCK) WHERE d.Imei = t.Imei)
		AND t.FirstTime >= @StartTime AND t.FirstTime < @EndTime
		AND t.ID >= @CID AND t.ID < @CIDE
	 ORDER BY t.FirstTime;
	INSERT dbo.PV_Imei_First ( Imei, FristImeiLogID, FirstLogType, FirstTime, FirstPlatform, FristPlatformDate, FirstSMSC, FirstProvince )
	SELECT Imei, FristImeiLogID, FirstLogType, FirstTime, FirstPlatform, FristPlatformDate, FirstSMSC, FirstProvince
	  FROM dbo.PV_Imei_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_First d WHERE d.Imei = l.Imei)
	INSERT dbo.PV_Imei ( Imei )
	SELECT Imei
	  FROM dbo.PV_Imei_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei d WHERE d.Imei = l.Imei)
	INSERT dbo.PV_Imei_Last ( Imei )
	SELECT Imei
	  FROM dbo.PV_Imei_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_Last d WHERE d.Imei = l.Imei)
	INSERT dbo.PV_Imei_SLast ( Imei )
	SELECT Imei
	  FROM dbo.PV_Imei_First1 l
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_SLast d WHERE d.Imei = l.Imei)
	 
	SELECT @CID = @CIDE;
END

-- 插入完成,记录最后ID
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'PV_Imei_First_CID_ImeiLog',@EID;

-- 记录时间点9
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time09',@strNow;

-- PV
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		UPDATE TOP(10000) t
		   SET _Time = getdate(), VisitCount_Time = dateadd(n,-1,@EndTime)
		  FROM dbo.PV_Imei t
		WHERE t.VisitCount_Time >= @EndTime;
		IF(@@ROWCOUNT = 0) BREAK;
	END
END
WHILE(1=1)
BEGIN
	UPDATE TOP(10000) t
	   SET _Time = getdate(), VisitCount_Time = @EndTime
		,t.VisitCountWeek = ISNULL((SELECT Sum(VisitCountWeek) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountDWeek = ISNULL((SELECT Sum(VisitCountDWeek) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountMonth = ISNULL((SELECT Sum(VisitCountMonth) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountNMonth = ISNULL((SELECT Sum(VisitCountNMonth) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountTotal = ISNULL((SELECT Sum(VisitCountTotal) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
	  FROM dbo.PV_Imei t
	 WHERE t.VisitCount_Time < @EndTime;
	IF(@@ROWCOUNT = 0) BREAK;
END

-- 记录时间点10
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time10',@strNow;

-- Last
TRUNCATE TABLE dbo.PV_Imei_Last1;
INSERT dbo.PV_Imei_Last1(Imei, LastImeiLogID)
SELECT Imei, Max(LastImeiLogID)
  FROM dbo.PV_Imei_LogType_Last(NOLOCK)
 WHERE LastTime >= @StartTime AND LastTime < @EndTime
 GROUP BY Imei
UPDATE t
   SET t.LastLogType = l.LogType
	,t.LastTime = l.LastTime
	,t.LastPlatform = l.LastPlatform
	,t.LastPlatformDate = l.LastPlatformDate
	,t.LastSMSC = l.LastSMSC
	,t.LastProvince = l.LastProvince
  FROM dbo.PV_Imei_Last1 t INNER JOIN dbo.PV_Imei_LogType_Last l(NOLOCK) ON t.Imei = l.Imei AND t.LastImeiLogID = l.LastImeiLogID
-- 记录时间点11
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time11',@strNow;
UPDATE t
   SET _Time = getdate()
	,t.LastImeiLogID = l.LastImeiLogID
	,t.SLastImeiLogID = t.LastImeiLogID
  FROM dbo.PV_Imei_Last t INNER JOIN dbo.PV_Imei_Last1 l(NOLOCK) ON t.Imei = l.Imei
 WHERE t.LastImeiLogID <> l.LastImeiLogID
UPDATE t	-- SLast
   SET _Time = getdate()
	,t.SLastLogType = l.LastLogType
	,t.SLastImeiLogID = l.LastImeiLogID
	,t.SLastTime = l.LastTime
	,t.SLastPlatform = l.LastPlatform
	,t.SLastPlatformDate = l.LastPlatformDate
	,t.SLastSMSC = l.LastSMSC
	,t.SLastProvince = l.LastProvince
  FROM dbo.PV_Imei_SLast t INNER JOIN dbo.PV_Imei_Last l(NOLOCK) ON t.Imei = l.Imei
 WHERE l.LastImeiLogID <> l.SLastImeiLogID
UPDATE t
   SET _Time = getdate()
	,t.LastLogType = l.LastLogType
	,t.LastTime = l.LastTime
	,t.LastPlatform = l.LastPlatform
	,t.LastPlatformDate = l.LastPlatformDate
	,t.LastSMSC = l.LastSMSC
	,t.LastProvince = l.LastProvince
  FROM dbo.PV_Imei_Last t INNER JOIN dbo.PV_Imei_Last1 l(NOLOCK) ON t.Imei = l.Imei

-- 记录时间点12
SELECT @Now = getdate();
SELECT @strNow = Convert(nvarchar(50),@Now,121)
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'_Time12',@strNow;
-- =================================================================================================

RETURN 1;
GO
