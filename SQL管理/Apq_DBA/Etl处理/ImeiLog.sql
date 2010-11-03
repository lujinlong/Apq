
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn_Init 10000,'20101101';
SELECT @rtn;

DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn_Init 10000,'20101102';
SELECT @rtn;

DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn 10000;
SELECT @rtn;

DECLARE @rtn int;
EXEC @rtn = etl.Etl_SwitchBcpTable 'ImeiLog';
SELECT @rtn;

DECLARE @rtn int;
EXEC @rtn = etl.Etl_Load 'ImeiLog';
SELECT @rtn;
