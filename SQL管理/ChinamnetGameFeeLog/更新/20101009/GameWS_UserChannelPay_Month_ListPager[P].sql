IF( OBJECT_ID('dbo.GameWS_UserChannelPay_Month_ListPager', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_UserChannelPay_Month_ListPager AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_UserChannelPay_Month_ListPager
	 @ExMsg	nvarchar(max) OUT,
	 
    @pSize		int = 20,			-- 每页行数
    @pNumber	int = 0,			-- 页码(从0开始)
    @pRowCount	int = 0 out
	 
	,@PayWay	int			-- 支付方式:{1:创艺,2:搜狐}
	,@uid		varchar(50)
	,@Imei		nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-09-30
-- 描述: 分页显示用户限额表
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_UserChannelPay_Month_ListPager;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@pSize IS NULL) SELECT @pSize = 20;
IF(@pNumber IS NULL) SELECT @pNumber = 0;
IF(@pRowCount IS NULL) SELECT @pRowCount = 0;

IF(@PayWay IS NULL) SELECT @PayWay = 0;
IF(@uid IS NULL) SELECT @uid = '';
IF(@Imei IS NULL) SELECT @Imei = '';

SELECT @pRowCount = Count(*)
  FROM dbo.UserChannelPay_Month(NOLOCK)
 WHERE (@PayWay NOT IN (1,2) OR PayWay = @PayWay)
	AND (@uid = '' OR uid = @uid)
	AND (@Imei = '' OR Imei = @Imei)

DECLARE @nTop0 int;
SELECT @nTop0 = @pNumber * @pSize;

SELECT TOP(@nTop0) ID
  INTO #UserChannelPay_Month_ListPager
  FROM UserChannelPay_Month(NOLOCK)
 WHERE (@PayWay NOT IN (1,2) OR PayWay = @PayWay)
	AND (@uid = '' OR uid = @uid)
	AND (@Imei = '' OR Imei = @Imei)
 ORDER BY PayMonth DESC;
 
SELECT TOP(@pSize) t.ID, t.PayWay, t.uid, t.PayMonth, t._Time, t.Imei
  FROM dbo.UserChannelPay_Month t(NOLOCK)
 WHERE (@PayWay NOT IN (1,2) OR PayWay = @PayWay)
	AND (@uid = '' OR uid = @uid)
	AND (@Imei = '' OR Imei = @Imei)
	AND NOT EXISTS(SELECT TOP 1 1 FROM #UserChannelPay_Month_ListPager Apq_Pager1 WHERE t.ID = Apq_Pager1.ID)
 ORDER BY PayMonth DESC;

TRUNCATE TABLE #UserChannelPay_Month_ListPager;
DROP TABLE #UserChannelPay_Month_ListPager;

SELECT @ExMsg = '获取成功';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_UserChannelPay_Month_ListPager] TO GameW
GO
