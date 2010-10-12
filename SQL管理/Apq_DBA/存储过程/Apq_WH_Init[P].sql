IF ( OBJECT_ID('dbo.Apq_WH_Init','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_WH_Init AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 维护作业初始化
-- 步骤: 1.禁用相关作业 2.启用维护作业
-- 示例:
EXEC dbo.Apq_WH_Init
-- =============================================
*/
ALTER PROC dbo.Apq_WH_Init
AS
SET NOCOUNT ON ;

-- 1.禁用相关作业
--EXEC msdb..sp_update_job @job_name = '日志转移',@enabled = 0
DECLARE @job_name sysname
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT name FROM msdb..sysjobs WHERE name LIKE '%日志切换转移'

OPEN @csr;
FETCH NEXT FROM @csr INTO @job_name
WHILE(@@FETCH_STATUS = 0)
BEGIN
	EXEC msdb..sp_update_job @job_name = @job_name,@enabled = 0

	FETCH NEXT FROM @csr INTO @job_name
END
CLOSE @csr;

-- 2.启用维护作业
EXEC msdb..sp_update_job @job_name = '例行维护',@enabled = 1

GO
