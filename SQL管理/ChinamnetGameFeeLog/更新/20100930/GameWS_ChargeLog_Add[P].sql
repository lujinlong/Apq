IF( OBJECT_ID('dbo.GameWS_ChargeLog_Add', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_ChargeLog_Add AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_ChargeLog_Add
	 @ExMsg	nvarchar(max) OUT,
	 
	 @PayWay		int			-- 支付方式:{1:创艺,2:搜狐}
	,@pid			varchar(50)
	,@porder		varchar(50)
	,@uid			varchar(50)
	,@amt			money
	,@scene			varchar(50)
	,@phone			varchar(13)
	,@private		varchar(250)
	,@status		tinyint
	,@faildcount	int
	,@chargetime	datetime
	,@channel		varchar(30)
	,@buycode		varchar(30)
	,@beginposttime	datetime
	,@ipaddress		varchar(30)
	,@keyvalue		varchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-09-30
-- 描述: 网游代扣日志记录,用户渠道日限额
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_ChargeLog_Add;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@PayWay IS NULL) SELECT @PayWay = 1;

-- 记录日志
IF(@PayWay = 1)
BEGIN
	INSERT INTO T_Chuangyidaishou(pid,porder,uid,amt,scene,phone,private,status,faildcount,chargetime,channel,buycode)
	VALUES(@pid,@porder,@uid,@amt,@scene,@phone,@private,@status,@faildcount,@chargetime,@channel,@buycode);
END
IF(@PayWay = 2)
BEGIN
	INSERT INTO dbo.T_Sohudaishou(pid,porder,uid,amt,scene,phone,private,status,faildcount,chargetime,channel,buycode,ipaddress,keyvalue)
	VALUES(@pid,@porder,@uid,@amt,@scene,@phone,@private,@status,@faildcount,@chargetime,@channel,@buycode,@ipaddress,@keyvalue);
END

-- 更新今日统计表
UPDATE dbo.UserChannelPay_Today
   SET [_Time] = getdate(), PayToday = PayToday + @amt
 WHERE PayWay = @PayWay AND uid = @uid;
IF(@@ROWCOUNT = 0)
BEGIN
	INSERT dbo.UserChannelPay_Today ( PayWay,uid,PayToday )
	VALUES(@PayWay,@uid,@amt);
END

SELECT @ExMsg = '记录成功，且今日统计已更新！';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_ChargeLog_Add] TO GameW
GO
