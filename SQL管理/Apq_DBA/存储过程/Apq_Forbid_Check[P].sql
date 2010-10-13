
IF( OBJECT_ID('dbo.Apq_Forbid_Check', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Forbid_Check AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-26
-- 描述: 检测封号数据
-- =============================================
-10: 已被封
*/
ALTER PROC dbo.Apq_Forbid_Check
	@ExMsg	nvarchar(4000) out,
	@Type	int,
	@Value	nvarchar(450),
	@ETime	datetime out
AS
SET NOCOUNT ON;

DECLARE @Now datetime;
SELECT @Now = getdate();

-- 类型内检测
SELECT @ETime = ETime FROM Apq_Forbid WHERE @Now BETWEEN BTime AND ETime AND Type = @Type AND Value = @Value;
IF(@@ROWCOUNT > 0)
BEGIN
	RETURN -10;
END

-- 匹配检测
IF(@Type = 0 OR @Type = 1)
BEGIN
	SELECT @ETime = ETime FROM Apq_Forbid WHERE @Now BETWEEN BTime AND ETime AND Type = 3 AND Value LIKE @Value;
	IF(@@ROWCOUNT > 0)
	BEGIN
		RETURN -10;
	END
END
GO
