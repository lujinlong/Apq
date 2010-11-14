
-- 1.重新统计
DECLARE @BTime datetime, @ETime datetime, @rtn int;
SELECT @BTime = '20101001'
SELECT @ETime = '20101101'

--EXEC @rtn = dbo.Stat_PV @BTime,@ETime,1;	-- 重新统计
EXEC @rtn = dbo.Stat_PV @BTime,@ETime;

SELECT @BTime = '20101101'
SELECT @ETime = dateadd(dd,0,datediff(dd,0,getdate()))

--EXEC @rtn = dbo.Stat_PV @BTime,@ETime,1;	-- 重新统计
EXEC @rtn = dbo.Stat_PV @BTime,@ETime;
