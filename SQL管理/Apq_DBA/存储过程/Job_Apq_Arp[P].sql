IF( OBJECT_ID('dbo.Job_Apq_Arp', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_Apq_Arp AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_Apq_Arp
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-07-01
-- 描述: Arp绑定
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_Arp;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,@GateWay nvarchar(50), @Mac nvarchar(50),
	
	@rtn int, @cmd nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	;
SELECT @JobTime=GetDate();

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,GateWay,Mac
  FROM dbo.ArpCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@GateWay,@Mac;
WHILE(@@FETCH_STATUS=0)
BEGIN
	SELECT @cmd = 'arp -s ' + @GateWay + ' ' + @Mac;
	EXEC master..xp_cmdshell @cmd;

	FETCH NEXT FROM @csr INTO @ID,@GateWay,@Mac;
END
CLOSE @csr;

RETURN 1;
GO
