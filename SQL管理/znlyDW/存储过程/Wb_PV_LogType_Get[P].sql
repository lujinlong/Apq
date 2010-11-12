IF( OBJECT_ID('dbo.Wb_PV_LogType_Get', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Wb_PV_LogType_Get AS BEGIN RETURN END';
GO
ALTER PROC dbo.Wb_PV_LogType_Get
	@ExMsg nvarchar(max) = NULL out
	
	,@Imei nvarchar(50)
	,@LogType int
	
	,@FirstTime datetime = NULL OUT
	,@FirstPlatform nvarchar(100) = NULL OUT
	,@FirstSMSC nvarchar(50) = NULL OUT
	,@FirstProvince nvarchar(50) = NULL OUT
	,@LastTime datetime = NULL OUT
	,@LastPlatform nvarchar(100) = NULL OUT
	,@LastSMSC nvarchar(50) = NULL OUT
	,@LastProvince nvarchar(50) = NULL OUT
	,@VisitCountTotal int = NULL OUT
	,@VisitCountWeek int = NULL OUT
	,@VisitCountDWeek int = NULL OUT
	,@VisitCountMonth int = NULL OUT
	,@VisitCountNMonth int = NULL OUT
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-11-08
-- 描述: 查询PV
-- 示例:
DECLARE @rtn int,@ExMsg nvarchar(max),@Imei nvarchar(50),@LogType int
	,@FirstTime datetime,@FirstPlatform nvarchar(100),@FirstSMSC nvarchar(50),@FirstProvince nvarchar(50)
	,@LastTime datetime,@LastPlatform nvarchar(100),@LastSMSC nvarchar(50),@LastProvince nvarchar(50)
	,@VisitCountTotal int,@VisitCountWeek int,@VisitCountDWeek int,@VisitCountMonth int,@VisitCountNMonth int
	;
SELECT @Imei = '019d3110a2145081'
	,@LogType = 1
	;

EXEC @rtn = dbo.Wb_PV_LogType_Get @ExMsg out, @Imei, @LogType
	,@FirstTime out, @FirstPlatform out, @FirstSMSC out, @FirstProvince out
	,@LastTime out,@LastPlatform out, @LastSMSC out,@LastProvince out
	,@VisitCountTotal out,@VisitCountWeek out,@VisitCountDWeek out,@VisitCountMonth out,@VisitCountNMonth out
	;
SELECT @rtn;
SELECT @FirstTime,@FirstPlatform,@FirstSMSC,@FirstProvince
	,@LastTime,@LastPlatform,@LastSMSC,@LastProvince
	,@VisitCountTotal,@VisitCountWeek,@VisitCountDWeek,@VisitCountMonth,@VisitCountNMonth
-- =============================================
*/
SET NOCOUNT ON;

SELECT @FirstTime = FirstTime,@FirstPlatform=FirstPlatform,@FirstSMSC=FirstSMSC,@FirstProvince=FirstProvince
	,@LastTime=LastTime,@LastPlatform=LastPlatform,@LastSMSC=LastSMSC,@LastProvince=LastProvince
	
	,@VisitCountTotal=VisitCountTotal,@VisitCountWeek=VisitCountWeek,@VisitCountDWeek=VisitCountDWeek,@VisitCountMonth=VisitCountMonth
	,@VisitCountNMonth=VisitCountNMonth
  FROM dbo.PV_Imei_LogType(NOLOCK)
 WHERE Imei = @Imei AND LogType = @LogType

RETURN 1;
GO
