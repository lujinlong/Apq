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

-- PV_Imei_LogType ---------------------------------------------------------------------------------
SELECT @CID = -1,@sql_Create='',@sql_Drop='';
SELECT @BID = min(l.ID),@EID = max(l.ID) FROM log.ImeiLog l(NOLOCK) WHERE l.LogTime >= @StartTime AND l.LogTime < @EndTime;
IF(@BID IS NULL OR @EID IS NULL) RETURN -1;
IF(@IsAgain = 0) SELECT @CID = ISNULL(dbo.Apq_Ext_Get('PV_Stat',0,'CID_PV_Imei_LogType'),@BID);
IF(@IsAgain = 1) SELECT @CID = @BID;
IF(NOT @CID BETWEEN @BID AND @EID)
BEGIN
	SELECT @CID = @BID;
END
PRINT '[1]'+Convert(nvarchar(21),@BID)+','+Convert(nvarchar(21),@CID)+','+Convert(nvarchar(21),@EID)

IF(@CID < @EID)
BEGIN
	-- 开始插入前去掉索引
	EXEC dbo.Apq_DropIndex @ExMsg OUT, 'dbo', 'PV_Imei_LogType', @sql_Create OUT, @sql_Drop OUT, 1
	IF(Len(@sql_Create)>1) EXEC dbo.Apq_Ext_Set 'PV_Imei_LogType', 0, 'Apq_CreateIndex',@sql_Create; -- 有索引时存档
	ELSE SELECT @sql_Create = dbo.Apq_Ext_Get('PV_Imei_LogType', 0, 'Apq_CreateIndex');	-- 否则读档

	PRINT '-- 创建索引 ---------------------------------------------------------------------------------'
	PRINT @sql_Create
	PRINT '-- 删除索引 ---------------------------------------------------------------------------------'
	PRINT @sql_Drop
END

-- 加入新增用户
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		DELETE TOP(1000) t
		  FROM dbo.PV_Imei_LogType t
		 WHERE t.FirstTime >= @StartTime-- AND t.FirstTime < @EndTime
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

WHILE(@CID <= @EID)
BEGIN
	--SELECT 1,@BID,@CID,@EID;
	PRINT '[1]'+Convert(nvarchar(21),@CID);
	EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'CID_PV_Imei_LogType',@CID;
	
	SELECT @CIDE = @CID + 10000;
	INSERT dbo.PV_Imei_LogType ( Imei,LogType,FirstTime,FirstPlatform,FirstSMSC,FirstProvince )
	SELECT l.Imei,l.LogType,ISNULL(l.LogTime,'9999-12-31 23:59:59.997'),ISNULL(l.Platform,'未知'),ISNULL(l.SMSC,'未知'),ISNULL(l.Province,'未知')
	  FROM log.ImeiLog l(NOLOCK)
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei_LogType d(NOLOCK) WHERE d.LogType = l.LogType AND d.Imei = l.Imei)
		AND l.LogTime >= @StartTime AND l.LogTime < @EndTime
		AND l.ID >= @CID AND l.ID < @CIDE
		AND l.Imei IS NOT NULL AND l.LogType IS NOT NULL;
		
	SELECT @CID = @CIDE;
END

-- 插入完成,记录最后ID
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'CID_PV_Imei_LogType',@EID;

-- 插入完成后按需创建索引
IF(LEN(@sql_Create)>1) EXEC sp_executesql @sql_Create;

-- 统计最近访问次数
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		UPDATE TOP(1000) t
		   SET _Time = getdate(), VisitCountLately_Time = dateadd(n,-1,@EndTime)
		  FROM dbo.PV_Imei_LogType t
		WHERE t.VisitCountLately_Time >= @EndTime;
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

WHILE(1=1)
BEGIN
	UPDATE TOP(1000) t
	   SET _Time = getdate(), VisitCountLately_Time = @EndTime
		,t.VisitCountWeek = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeWeek AND l.LogTime < @EndTime),0)
		,t.VisitCountDWeek = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeDWeek AND l.LogTime < @EndTime),0)
		,t.VisitCountMonth = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeMonth AND l.LogTime < @EndTime),0)
		,t.VisitCountNMonth = ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @BTimeNMonth AND l.LogTime < @EndTime),0)
	  FROM dbo.PV_Imei_LogType t
	 WHERE t.VisitCountLately_Time < @EndTime;
	IF(@@ROWCOUNT = 0) BREAK;
END

-- 累加访问总次数
WHILE(1=1)
BEGIN
	UPDATE TOP(1000) t
	   SET _Time = getdate(), VisitCountTotal_Time = @EndTime
		,t.VisitCountTotal = CASE @IsAgain WHEN 1 THEN T.PreVisitCountTotal ELSE t.VisitCountTotal END
			+ ISNULL((SELECT Count(l.Imei) FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei AND l.LogTime >= @StartTime AND l.LogTime < @EndTime),0)
	  FROM dbo.PV_Imei_LogType t
	 WHERE t.VisitCountTotal_Time < @EndTime;
	IF(@@ROWCOUNT = 0) BREAK;
END

-- 更新最后访问信息
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		UPDATE TOP(1000) t
		   SET _Time = getdate(), Last_Time = dateadd(n,-1,@EndTime)
		  FROM dbo.PV_Imei_LogType t
		WHERE t.Last_Time >= @EndTime;
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

WHILE(1=1)
BEGIN
	UPDATE TOP(1000) t
	   SET _Time = getdate(), Last_Time = @EndTime
		,t.LastTime = ISNULL((SELECT TOP 1 LogTime FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC, ID DESC),0)
		,t.LastPlatform = ISNULL((SELECT TOP 1 LastPlatform FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC, ID DESC),'未知')
		,t.LastPlatformDate = (SELECT TOP 1 PlatformDate FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC, ID DESC)
		,t.LastSMSC = ISNULL((SELECT TOP 1 LastSMSC FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC, ID DESC),'未知')
		,t.LastProvince = ISNULL((SELECT TOP 1 Province FROM log.ImeiLog l(NOLOCK) WHERE l.LogType = t.LogType AND l.Imei = t.Imei ORDER BY LogTime DESC, ID DESC),'未知')
	  FROM dbo.PV_Imei_LogType t
	 WHERE t.Last_Time < @EndTime;
	IF(@@ROWCOUNT = 0) BREAK;
END
-- =================================================================================================

-- PV_Imei -----------------------------------------------------------------------------------------
SELECT @CID = -1,@sql_Create='',@sql_Drop='';
SELECT @BID = min(l.ID),@EID = max(l.ID) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.FirstTime >= @StartTime AND l.FirstTime < @EndTime;
IF(@BID IS NULL OR @EID IS NULL) RETURN -1;
IF(@IsAgain = 0) SELECT @CID = ISNULL(dbo.Apq_Ext_Get('PV_Stat',0,'CID_PV_Imei'),@BID);
IF(@IsAgain = 1) SELECT @CID = @BID;
IF(NOT @CID BETWEEN @BID AND @EID)
BEGIN
	SELECT @CID = @BID;
END
PRINT '[2]'+Convert(nvarchar(21),@BID)+','+Convert(nvarchar(21),@CID)+','+Convert(nvarchar(21),@EID)

IF(@CID < @EID)
BEGIN
	-- 开始插入前去掉索引
	EXEC dbo.Apq_DropIndex @ExMsg OUT, 'dbo', 'PV_Imei', @sql_Create OUT, @sql_Drop OUT, 1
	IF(Len(@sql_Create)>1) EXEC dbo.Apq_Ext_Set 'PV_Imei', 0, 'Apq_CreateIndex',@sql_Create; -- 有索引时存档
	ELSE SELECT @sql_Create = dbo.Apq_Ext_Get('PV_Imei', 0, 'Apq_CreateIndex');	-- 否则读档

	PRINT '-- 创建索引 ---------------------------------------------------------------------------------'
	PRINT @sql_Create
	PRINT '-- 删除索引 ---------------------------------------------------------------------------------'
	PRINT @sql_Drop
END

-- 加入新增用户
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		DELETE TOP(10000) t
		  FROM dbo.PV_Imei t
		 WHERE t.FirstTime >= @StartTime-- AND t.FirstTime < @EndTime
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

WHILE(@CID <= @EID)
BEGIN
	--SELECT 2,@BID,@CID,@EID;
	PRINT '[2]'+Convert(nvarchar(21),@CID);
	EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'CID_PV_Imei',@CID;
	
	SELECT @CIDE = @CID + 10000;
	INSERT dbo.PV_Imei ( Imei,FirstLogType,FirstTime,FirstPlatform,FristPlatformDate,FirstSMSC,FirstProvince )
	SELECT t.Imei,ISNULL(LogType,0),FirstTime,FirstPlatform,FristPlatformDate,FirstSMSC,FirstProvince
	  FROM dbo.PV_Imei_LogType t(NOLOCK)
	 WHERE NOT EXISTS(SELECT TOP 1 1 FROM dbo.PV_Imei d(NOLOCK) WHERE d.Imei = t.Imei)
		AND t.FirstTime >= @StartTime AND t.FirstTime < @EndTime
		AND t.ID >= @CID AND t.ID < @CIDE
	 ORDER BY t.FirstTime;
	 
	SELECT @CID = @CIDE;
END

-- 插入完成,记录最后ID
EXEC dbo.Apq_Ext_Set 'PV_Stat',0,'CID_PV_Imei',@EID;

-- 更新其余统计信息
IF(@IsAgain = 1)
BEGIN
	WHILE(1=1)
	BEGIN
		UPDATE TOP(1000) t
		   SET _Time = getdate(), VisitCount_Time = dateadd(n,-1,@EndTime)
		  FROM dbo.PV_Imei t
		WHERE t.VisitCount_Time >= @EndTime;
		IF(@@ROWCOUNT = 0) BREAK;
	END
END

WHILE(1=1)
BEGIN
	UPDATE TOP(1000) t
	   SET _Time = getdate(), VisitCount_Time = @EndTime
		,t.VisitCountWeek = ISNULL((SELECT Sum(VisitCountWeek) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountDWeek = ISNULL((SELECT Sum(VisitCountDWeek) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountMonth = ISNULL((SELECT Sum(VisitCountMonth) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountNMonth = ISNULL((SELECT Sum(VisitCountNMonth) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		,t.VisitCountTotal = ISNULL((SELECT Sum(VisitCountTotal) FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei),0)
		
		,t.LastLogType = (SELECT TOP 1 LogType FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei ORDER BY LastTime DESC, ID DESC)
		,t.LastTime = (SELECT TOP 1 LastTime FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei ORDER BY LastTime DESC, ID DESC)
		,t.LastPlatform = (SELECT TOP 1 LastPlatform FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei ORDER BY LastTime DESC, ID DESC)
		,t.LastPlatformDate = (SELECT TOP 1 LastPlatformDate FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei ORDER BY LastTime DESC, ID DESC)
		,t.LastSMSC = (SELECT TOP 1 LastSMSC FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei ORDER BY LastTime DESC, ID DESC)
		,t.LastProvince = (SELECT TOP 1 LastProvince FROM dbo.PV_Imei_LogType l(NOLOCK) WHERE l.Imei = t.Imei ORDER BY LastTime DESC, ID DESC)
	  FROM dbo.PV_Imei t
	 WHERE t.VisitCount_Time < @EndTime;
	IF(@@ROWCOUNT = 0) BREAK;
END
-- =================================================================================================

RETURN 1;
GO
