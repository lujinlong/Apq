
-- 1.重新统计
DECLARE @BTime datetime, @ETime datetime, @rtn int;
SELECT @BTime = '20101001'
SELECT @ETime = '20101109'

EXEC @rtn = dbo.Stat_PV @BTime,@ETime,1;	-- 重新统计
--EXEC @rtn = dbo.Stat_PV @BTime,@ETime;
