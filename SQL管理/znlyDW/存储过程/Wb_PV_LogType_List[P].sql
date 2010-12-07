IF( OBJECT_ID('dbo.Wb_PV_LogType_List', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Wb_PV_LogType_List AS BEGIN RETURN END';
GO
ALTER PROC dbo.Wb_PV_LogType_List
	@ExMsg nvarchar(max) = NULL out
	
	,@Imei nvarchar(50)
	,@LogType int
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-11-08
-- 描述: 查询PV
-- 示例:
DECLARE @rtn int,@ExMsg nvarchar(max),@Imei nvarchar(50),@LogType int
	;
SELECT @Imei = '019d3110a2145081'
	,@LogType = 1
	;

EXEC @rtn = dbo.Wb_PV_LogType_List @ExMsg out, @Imei, @LogType
	;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT TOP(1) f.Imei, f.LogType
	,VisitCountTotal, VisitCountWeek, VisitCountDWeek, VisitCountMonth, VisitCountNMonth
	,FirstTime, FirstPlatform, FirstSMSC, FirstProvince, FristPlatformDate
	,LastTime, LastPlatform, LastPlatformDate, LastSMSC, LastProvince
  FROM dbo.PV_Imei_LogType_First f(NOLOCK)
	LEFT JOIN dbo.PV_Imei_LogType t(NOLOCK) ON f.Imei = t.Imei AND f.LogType = t.LogType
	LEFT JOIN dbo.PV_Imei_LogType_Last l(NOLOCK) ON f.Imei = l.Imei AND f.LogType = l.LogType
 WHERE f.Imei = @Imei AND f.LogType = @LogType

RETURN 1;
GO
