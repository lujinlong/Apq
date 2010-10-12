IF( OBJECT_ID('dbo.GameWS_UserChannelPayLimit_Today_Set', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_UserChannelPayLimit_Today_Set AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_UserChannelPayLimit_Today_Set
	 @ExMsg	nvarchar(max) OUT,
	 
	 @PayWay	int			-- 支付方式:{1:创艺,2:搜狐}
	,@Limit		money
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-09-30
-- 描述: 用户渠道日限额设置
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_UserChannelPayLimit_Today_Set;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@PayWay IS NULL) SELECT @PayWay = 1;

UPDATE dbo.UserChannelPayLimit_Today
   SET _Time = getdate(), Limit = @Limit
 WHERE PayWay = @PayWay;
IF(@@ROWCOUNT = 0)
BEGIN
	INSERT dbo.UserChannelPayLimit_Today ( PayWay,Limit )
	VALUES(@PayWay,@Limit);
END

SELECT @ExMsg = '设置成功！';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_UserChannelPayLimit_Today_Set] TO GameW
GO
