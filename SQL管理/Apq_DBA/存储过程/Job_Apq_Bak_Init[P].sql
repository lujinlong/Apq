IF( OBJECT_ID('bak.Job_Apq_Bak_Init', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Job_Apq_Bak_Init AS BEGIN RETURN END';
GO
ALTER PROC bak.Job_Apq_Bak_Init
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 备份作业(仅本地)的初始化
-- 功能: 计算下一次备份作业启动后对"哪些"数据库执行"何种"备份
-- 参数:
@JobTime: 作业启动时间
@DBName: 数据库名
@FullCycle: 完整备份周期(天)
@FullTime: 完整备份时间(每次日期与周期相关)
@TrnCycle: 日志备份周期(分钟,请尽量作业周期的倍数)
@PreFullTime: 上一次完整备份时间
@PreBakTime: 上一次备份时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Job_Apq_Bak_Init;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@FullCycle int,			-- 完整备份周期(天)
	@FullTime smalldatetime,-- 完整备份时间(每次日期与周期相关)
	@TrnCycle smallint,		-- 日志备份周期(分钟,请尽量作业周期的倍数)
	@PreFullTime datetime,	-- 上一次完整备份时间
	@PreBakTime datetime	-- 上一次备份时间
	
	,@rtn int, @sql nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@ProductVersion	decimal	-- 服务器版本(取前两字符)
	,@ForceFull tinyint
	;
SELECT @JobTime=GetDate(), @ForceFull = 0
	,@ProductVersion = CONVERT(decimal,LEFT(Convert(nvarchar,SERVERPROPERTY('ProductVersion')),2));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,DBName,FullCycle,FullTime,TrnCycle,PreFullTime,PreBakTime
  FROM bak.BakCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@FullCycle,@FullTime,@TrnCycle,@PreFullTime,@PreBakTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 2000以上版本判断是否已完整备份过,没有则强制完整备份
	SELECT @ForceFull = 0;
	IF(@ProductVersion > 8 AND DATABASEPROPERTYEX(@DBName,'Recovery')<>'SIMPLE')
	BEGIN
		SELECT @sql = 'SELECT @ForceFull = 0;
IF(EXISTS(SELECT * FROM sys.database_recovery_status WHERE last_log_backup_lsn IS NULL AND database_id = DB_ID(@DBName)))
SELECT @ForceFull = 1;';
		EXEC sp_executesql @sql,N'@ForceFull int out, @DBName nvarchar(256)', @ForceFull = @ForceFull out, @DBName = @DBName;
	END
	
	IF(@ForceFull = 1 OR @PreFullTime IS NULL
		OR DATEDIFF(n, @PreFullTime, @JobTime) >= 1440 * @FullCycle - 2
	)BEGIN
		UPDATE [bak].BakCfg SET ReadyAction = ReadyAction | 1 WHERE ID = @ID;
	END
	ELSE IF(DATABASEPROPERTYEX(@DBName,'Recovery')<>'SIMPLE' AND DateDiff(N,@PreBakTime,@JobTime) >= @TrnCycle - 1)
	BEGIN
		UPDATE [bak].BakCfg SET ReadyAction = ReadyAction | 2 WHERE ID = @ID;
	END

	FETCH NEXT FROM @csr INTO @ID,@DBName,@FullCycle,@FullTime,@TrnCycle,@PreFullTime,@PreBakTime;
END
CLOSE @csr;

RETURN 1;
GO
