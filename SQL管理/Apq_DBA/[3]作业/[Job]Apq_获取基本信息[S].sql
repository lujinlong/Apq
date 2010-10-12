USE [msdb]
GO

/****** Object:  Job [Apq_获取基本信息]    Script Date: 06/09/2010 15:59:08 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [[Uncategorized (Local)]]]    Script Date: 06/09/2010 15:59:08 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'Apq_获取基本信息', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'无描述。', 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'apq', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [RSrvConfig]    Script Date: 06/09/2010 15:59:09 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'RSrvConfig', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'
DECLARE @sql nvarchar(max);
SELECT @sql = ''
SELECT ID, ParentID, Name, LSName, UID, PwdC, Type, LSMaxTimes, LSErrTimes, LSState, IPLan, IPWan1, IPWan2, FTPPort, FTPU, FTPPC, SqlPort, Enabled
  INTO #RSrvConfig
  FROM gla_通行证.Apq_DBA.dbo.RSrvConfig;
IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
BEGIN
	TRUNCATE TABLE dbo.RSrvConfig;
	INSERT dbo.RSrvConfig(ID, ParentID, Name, LSName, UID, PwdC, Type, LSMaxTimes, LSErrTimes, LSState, IPLan, IPWan1, IPWan2, FTPPort, FTPU, FTPPC, SqlPort, Enabled)
	SELECT ID, ParentID, Name, LSName, UID, PwdC, Type, LSMaxTimes, LSErrTimes, LSState, IPLan, IPWan1, IPWan2, FTPPort, FTPU, FTPPC, SqlPort, Enabled
	  FROM #RSrvConfig;
END

TRUNCATE TABLE #RSrvConfig;
DROP TABLE #RSrvConfig;
'';
EXEC sp_executesql @sql;
', 
		@database_name=N'Apq_DBA', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'计划1', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20100609, 
		@active_end_date=99991231, 
		@active_start_time=20000, 
		@active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO
