
DECLARE @BTime datetime, @ETime datetime, @CTime datetime, @rtn int;
SELECT @BTime = '20101001'
SELECT @ETime = '20101106'

SELECT @CTime = @BTime;
WHILE(@CTime <= @ETime)
BEGIN
	SELECT @CTime = dateadd(hh,1,@CTime);
	EXEC @rtn = etl.Job_Etl_BcpIn_Init 10000,@CTime;
END

DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn 10000;
SELECT @rtn;

DECLARE @rtn int;
EXEC @rtn = etl.Etl_SwitchBcpTable 'ImeiLog';
SELECT @rtn;

DECLARE @rtn int;
EXEC @rtn = etl.Etl_Load 'ImeiLog';
SELECT @rtn;
