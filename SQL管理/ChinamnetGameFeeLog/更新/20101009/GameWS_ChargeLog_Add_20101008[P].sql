IF( OBJECT_ID('dbo.GameWS_ChargeLog_Add_R1', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_ChargeLog_Add_R1 AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_ChargeLog_Add_R1
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
	,@Imei			nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-08
-- 描述: 网游代扣日志记录,用户渠道日限额,月限额
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_ChargeLog_Add_R1;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@PayWay IS NULL) SELECT @PayWay = 1;

-- 记录日志
IF(@PayWay = 1)
BEGIN
	INSERT INTO T_Chuangyidaishou(pid,porder,uid,amt,scene,phone,private,status,faildcount,chargetime,channel,buycode,Imei,ipaddress)
	VALUES(@pid,@porder,@uid,Convert(int,@amt),@scene,@phone,@private,@status,@faildcount,@chargetime,@channel,@buycode,@Imei,@ipaddress);
END
IF(@PayWay = 2)
BEGIN
	INSERT INTO dbo.T_Sohudaishou(pid,porder,uid,amt,scene,phone,private,status,faildcount,chargetime,channel,buycode,ipaddress,keyvalue,Imei)
	VALUES(@pid,@porder,@uid,Convert(int,@amt),@scene,@phone,@private,@status,@faildcount,@chargetime,@channel,@buycode,@ipaddress,@keyvalue,@Imei);
END

-- 更新当日统计表
UPDATE dbo.UserChannelPay_Today
   SET [_Time] = getdate(), PayToday = PayToday + @amt, Imei = @Imei
 WHERE PayWay = @PayWay AND uid = @uid;
IF(@@ROWCOUNT = 0)
BEGIN
	INSERT dbo.UserChannelPay_Today ( PayWay,uid,PayToday,Imei )
	VALUES(@PayWay,@uid,@amt,@Imei);
END

-- 更新当月统计表
UPDATE dbo.UserChannelPay_Month
   SET [_Time] = getdate(), PayMonth = PayMonth + @amt, Imei = @Imei
 WHERE PayWay = @PayWay AND uid = @uid;
IF(@@ROWCOUNT = 0)
BEGIN
	INSERT dbo.UserChannelPay_Month ( PayWay,uid,PayMonth,Imei )
	VALUES(@PayWay,@uid,@amt,@Imei);
END

-- 判断月限额是否达到
IF(EXISTS(
SELECT TOP 1 1
  FROM dbo.UserChannelPay_Month a INNER JOIN dbo.UserChannelPayLimit b ON a.PayWay = b.PayWay AND b.LimitType = 2
 WHERE a.PayMonth >= b.Limit
	AND a.PayWay = @PayWay AND uid = @uid
))
BEGIN
	-- 达到月限时将日统计值设置为很大的值
	UPDATE dbo.UserChannelPay_Today
	   SET [_Time] = getdate(), PayToday = 99999
	 WHERE PayWay = @PayWay AND uid = @uid;
END

SELECT @ExMsg = '记录成功，且统计信息已更新！';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_ChargeLog_Add_R1] TO GameW
GO
