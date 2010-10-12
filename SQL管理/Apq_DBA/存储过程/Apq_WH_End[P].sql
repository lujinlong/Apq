IF ( OBJECT_ID('dbo.Apq_WH_End','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_WH_End AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 结束维护作业
-- 步骤: 1.启用相关作业 2.禁用维护作业
-- 示例:
EXEC dbo.Apq_WH_End
-- =============================================
*/
ALTER PROC dbo.Apq_WH_End
AS
SET NOCOUNT ON ;

-- 1.启用相关作业
--EXEC msdb..sp_update_job @job_name = '日志转移',@enabled = 1
DECLARE @job_name sysname
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT name FROM msdb..sysjobs WHERE name LIKE '%日志切换转移'

OPEN @csr;
FETCH NEXT FROM @csr INTO @job_name
WHILE(@@FETCH_STATUS = 0)
BEGIN
	EXEC msdb..sp_update_job @job_name = @job_name,@enabled = 1

	FETCH NEXT FROM @csr INTO @job_name
END
CLOSE @csr;

-- 2.禁用维护作业
EXEC msdb..sp_update_job @job_name = '例行维护',@enabled = 0

GO
