USE [msdb]
GO
/****** 对象:  Job [[每日3:00]]chinamnetgamefeelog日志转移到历史表]    脚本日期: 10/11/2010 11:35:17 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** 对象:  JobCategory [[Uncategorized (Local)]]]    脚本日期: 10/11/2010 11:35:17 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'[每日3:00]chinamnetgamefeelog日志转移到历史表', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'无描述。', 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** 对象:  Step [日志转移]    脚本日期: 10/11/2010 11:35:18 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'日志转移', 
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
DECLARE @Now datetime,@Today datetime,@Day2Trans datetime;
SELECT @Now = getdate();
SELECT @Today = DateAdd(dd,0,DateDiff(dd,0,@Now));
SELECT @Day2Trans = DateAdd(dd,-2,@Today );

DELETE dbo.T_Chuangyidaishou
OUTPUT deleted.pkid, deleted.pid, deleted.porder, deleted.uid, deleted.amt, deleted.scene, deleted.phone, deleted.private, deleted.status, deleted.faildcount, deleted.chargetime, deleted.channel, deleted.buycode, deleted.beginposttime, deleted.responsevalue, deleted.ipaddress
  INTO dbo.T_Chuangyidaishou_history(pkid, pid, porder, uid, amt, scene, phone, private, status, faildcount, chargetime, channel, buycode, beginposttime,responsevalue,ipaddress)
 WHERE chargetime < @Day2Trans

DELETE dbo.T_Sohudaishou
OUTPUT deleted.pkid, deleted.pid, deleted.porder, deleted.uid, deleted.amt, deleted.scene, deleted.phone, deleted.private, deleted.status, deleted.faildcount, deleted.chargetime, deleted.channel, deleted.buycode, deleted.beginposttime, deleted.ipaddress, deleted.keyvalue
  INTO dbo.T_Sohudaishou_history(pkid, pid, porder, uid, amt, scene, phone, private, status, faildcount, chargetime, channel, buycode, beginposttime, ipaddress, keyvalue)
 WHERE chargetime < @Day2Trans
', 
		@database_name=N'chinamnetgamefeelog', 
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
		@active_start_date=20101011, 
		@active_end_date=99991231, 
		@active_start_time=30000, 
		@active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
