IF( OBJECT_ID('dbo.GameWS_UserChannelPay_Today_Get', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_UserChannelPay_Today_Get AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_UserChannelPay_Today_Get
	 @ExMsg	nvarchar(max) OUT,
	 
	 @PayWay		int			-- 支付方式:{1:创艺,2:搜狐}
	,@uid			varchar(50)
	
	,@PayToday		money OUT
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-09-30
-- 描述: 获取用户当日已用额度
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_UserChannelPay_Today_Get;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@PayWay IS NULL) SELECT @PayWay = 1;

SELECT @PayToday = 0;
SELECT @PayToday = PayToday
  FROM dbo.UserChannelPay_Today
 WHERE PayWay = @PayWay AND uid = @uid;

SELECT @ExMsg = '获取成功';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_UserChannelPay_Today_Get] TO GameW
GO
