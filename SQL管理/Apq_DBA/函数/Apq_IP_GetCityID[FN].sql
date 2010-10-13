
IF( OBJECT_ID('dbo.Apq_IP_GetCityID', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_IP_GetCityID()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-11-19
-- 描述: 根据IP获取所在城市ID
-- 输入: 二进制IP
-- 返回: 城市ID,未知时返回-10
-- 示例:
SELECT dbo.Apq_IP_GetCityID(0x0000000000000000000000000029F4475B);
-- =============================================
-10: 未知
*/
ALTER FUNCTION dbo.Apq_IP_GetCityID(
	@binIP	binary(16)
)RETURNS bigint
AS
BEGIN
	DECLARE @CityID bigint
	SELECT @CityID = -10;	-- 未知
	SELECT TOP 1 @CityID = CityID FROM Apq_IP WHERE @binIP BETWEEN IPBegin AND IPEnd ORDER BY _Time DESC;
	RETURN @CityID;
END
GO
