IF( OBJECT_ID('dbo.Job_UserChannelPay_Today_Truncate', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_UserChannelPay_Today_Truncate AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_UserChannelPay_Today_Truncate
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-09-30
-- 描述: 清空UserChannelPay_Today
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_UserChannelPay_Today_Truncate;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
TRUNCATE TABLE UserChannelPay_Today;

RETURN 1;
GO
