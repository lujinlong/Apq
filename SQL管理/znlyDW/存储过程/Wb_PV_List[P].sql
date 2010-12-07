IF( OBJECT_ID('dbo.Wb_PV_List', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Wb_PV_List AS BEGIN RETURN END';
GO
ALTER PROC dbo.Wb_PV_List
	@ExMsg nvarchar(max) = NULL out
	
	,@Imei nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-11-08
-- 描述: 查询PV
-- 示例:
DECLARE @rtn int,@ExMsg nvarchar(max),@Imei nvarchar(50)
	;
SELECT @Imei = '019d3110a2145081'
	;

EXEC @rtn = dbo.Wb_PV_List @ExMsg out, @Imei
	;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT TOP(1) f.Imei
	,VisitCountTotal, VisitCountWeek, VisitCountDWeek, VisitCountMonth, VisitCountNMonth
	,FirstLogType, FirstTime, FirstPlatform, FirstSMSC, FirstProvince, FristPlatformDate
	,LastLogType, LastTime, LastPlatform, LastPlatformDate, LastSMSC, LastProvince
  FROM dbo.PV_Imei_First f(NOLOCK)
	LEFT JOIN dbo.PV_Imei t(NOLOCK) ON f.Imei = t.Imei
	LEFT JOIN dbo.PV_Imei_Last l(NOLOCK) ON f.Imei = l.Imei
 WHERE f.Imei = @Imei

RETURN 1;
GO
