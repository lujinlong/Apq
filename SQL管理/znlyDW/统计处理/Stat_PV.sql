
-- 1.重新统计
DECLARE @BTime datetime, @ETime datetime, @CTimeB datetime, @CTimeE datetime, @rtn int;
SELECT @BTime = '20101001'
SELECT @ETime = '20101110'

SELECT @CTimeB = @BTime;
SELECT @CTimeE = dateadd(dd,1,@CTimeB);
WHILE(@CTimeB < @ETime)
BEGIN
	EXEC @rtn = dbo.Stat_PV @CTimeB,@CTimeE;
	SELECT @CTimeB = dateadd(dd,1,@CTimeB);
	SELECT @CTimeE = dateadd(dd,1,@CTimeB);
END
