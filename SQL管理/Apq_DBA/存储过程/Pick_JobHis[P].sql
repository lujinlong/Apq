IF ( OBJECT_ID('dbo.Pick_JobHis','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Pick_JobHis AS BEGIN RETURN END' ;
GO
ALTER PROC dbo.Pick_JobHis
    @PickLastID bigint
   ,@PickMaxID bigint
   ,@PickLastTime datetime
   ,@PickMaxTime datetime
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 作业日志收集,供BCP使用
-- 示例:
DECLARE @rtn int,@PickLastID bigint,@PickMaxID bigint,@PickLastTime datetime,@PickMaxTime datetime;
SELECT @PickLastID = -1
	,@PickMaxID = 500
	,@PickLastTime = DateAdd(dd,-1,getdate())
	,@PickMaxTime = getdate()
EXEC @rtn = dbo.Pick_JobHis @PickLastID, @PickMaxID, @PickLastTime,@PickMaxTime
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON ;

--定义变量
DECLARE @LocalSrvID nvarchar(32)
   ,@SPBeginTime datetime		-- 存储过程启动时间
	;
SELECT  @SPBeginTime = GetDate() ;
SELECT  @LocalSrvID = dbo.Apq_Ext_Get('',0,'LocalSrvID') ;

SELECT  @LocalSrvID,j.name,h.step_id,h.step_name,h.sql_message_id,h.sql_severity,h.message,h.run_status
	,CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )),h.run_duration
FROM    msdb.dbo.sysjobhistory h
        INNER JOIN msdb.dbo.sysjobs j
            ON h.job_id = j.job_id
WHERE   step_id > 0
		AND name NOT IN ('Apq_Bak','Apq_Bak_Init','Apq_Bak_Raiserror','Apq_FTP_PutBak[1]','Apq_RestoreFromFolder[1]','Apq_杀掉死进程')
        AND run_status = 0
        AND ( /*( h.instance_id > @PickLastID
                AND h.instance_id <= @PickMaxID
              )
              OR */( CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) > @PickLastTime
                   AND CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) <= @PickMaxTime
                 )
            )
UNION ALL
SELECT  @LocalSrvID,j.name,h.step_id,h.step_name,h.sql_message_id,h.sql_severity,h.message,h.run_status
	,CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )),h.run_duration
FROM    msdb.dbo.sysjobhistory h
        INNER JOIN msdb.dbo.sysjobs j
            ON h.job_id = j.job_id
WHERE   step_id > 0
		AND name IN ('Apq_Bak','Apq_Bak_Init','Apq_Bak_Raiserror','Apq_FTP_PutBak[1]','Apq_RestoreFromFolder[1]','Apq_杀掉死进程')
        AND run_status = 0
        AND ( CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) > Dateadd(n,-30,@SPBeginTime)
               AND CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) <= @PickMaxTime
             )
GO
