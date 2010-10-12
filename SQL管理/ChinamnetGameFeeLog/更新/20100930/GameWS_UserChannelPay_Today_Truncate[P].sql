IF( OBJECT_ID('dbo.GameWS_UserChannelPay_Today_Truncate', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_UserChannelPay_Today_Truncate AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_UserChannelPay_Today_Truncate
	@ExMsg nvarchar(max) = '' OUT
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-09-30
-- 描述: 清空UserChannelPay_Today
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_UserChannelPay_Today_Truncate;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

TRUNCATE TABLE UserChannelPay_Today;

RETURN 1;
GO
GRANT EXECUTE ON [dbo].GameWS_UserChannelPay_Today_Truncate TO GameW
GO
