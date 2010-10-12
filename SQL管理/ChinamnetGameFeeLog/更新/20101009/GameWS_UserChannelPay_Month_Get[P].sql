IF( OBJECT_ID('dbo.GameWS_UserChannelPay_Month_Get', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_UserChannelPay_Month_Get AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_UserChannelPay_Month_Get
	 @ExMsg	nvarchar(max) OUT,
	 
	 @PayWay		int			-- 支付方式:{1:创艺,2:搜狐}
	,@uid			varchar(50)
	
	,@PayMonth		money OUT
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-09
-- 描述: 获取用户当月已用额度
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_UserChannelPay_Month_Get;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@PayWay IS NULL) SELECT @PayWay = 1;

SELECT @PayMonth = 0;
SELECT @PayMonth = PayMonth
  FROM dbo.UserChannelPay_Month
 WHERE PayWay = @PayWay AND uid = @uid;

SELECT @ExMsg = '获取成功';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_UserChannelPay_Month_Get] TO GameW
GO
