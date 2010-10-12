IF( OBJECT_ID('dbo.Apq_Stat', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Stat AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_Stat
	 @StatName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-11-05
-- 描述: 数据统计
-- 参数:
@StatName: 统计名
@STMT: 统计语句
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_Stat 'GameActor';
SELECT @rtn;
-- =============================================
-1:未找到配置
-2:检测未通过
-3:时间未到
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime;
SELECT @SPBeginTime=GetDate();

DECLARE @Detect nvarchar(4000)
	,@STMT nvarchar(4000)
	,@LastStatDate datetime
	
	,@StatDate datetime
	;
SELECT @Detect = Detect, @STMT = STMT, @LastStatDate = DateAdd(dd,0,DateDiff(dd,0,LastStatDate))
  FROM dbo.StatConfig_Day
 WHERE StatName = @StatName;
IF(@@ROWCOUNT = 0)
BEGIN
	RETURN -1;
END

-- 检测数据(通过才统计)
IF(LEN(@Detect)>1)
BEGIN
	EXEC @rtn = sp_executesql @Detect;
	IF(@@ERROR <> 0 OR @rtn <> 1)
	BEGIN
		RETURN -2;
	END
END

SELECT @StatDate = DATEADD(DD,1,@LastStatDate);
IF(DATEADD(DD,1,@StatDate) > @SPBeginTime)
BEGIN
	RETURN -3;
END

-- 先修改已统计日期
UPDATE dbo.StatConfig_Day
   SET _Time = GETDATE(), LastStatDate = @StatDate
 WHERE StatName = @StatName;

EXEC @rtn = sp_executesql @STMT, N'@StatDate datetime', @StatDate = @StatDate;

RETURN 1;
GO
