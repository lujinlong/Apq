IF( OBJECT_ID('bak.Job_Apq_Bak', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Job_Apq_Bak AS BEGIN RETURN END';
GO
ALTER PROC bak.Job_Apq_Bak
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 备份作业(仅本地)
-- 参数:
@JobTime: 作业启动时间
@DBName: 数据库名
@BakFolder: 备份路径
@FullCycle: 完整备份周期(天)
@FullTime: 完整备份时间(每次日期与周期相关)
@TrnCycle: 日志备份周期(分钟,请尽量作业周期的倍数)
@PreFullTime: 上一次完整备份时间
@PreBakTime: 上一次备份时间
@Num_Full: 备份文件保留个数
@NeedRestore: 是否需要还原
@RestoreFolder: 还原历史库的目录(本机)
@NeedTruncate: 是否需要截断日志(兼容2000)
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Job_Apq_Bak;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@BakFolder nvarchar(4000),--备份目录
	@FTPFolder nvarchar(4000),--FTP目录
	@ReadyAction tinyint,	-- 备份操作
	@FullTime smalldatetime, -- 完整备份时间
	@PreFullTime datetime,	-- 上一次完整备份时间
	@PreBakTime datetime,	-- 上一次备份时间
	@Num_Full int,		-- 完整备份文件保留个数
	@FullFileToDel nvarchar(4000),--在此文件之前的文件将被删除
	@NeedRestore tinyint,
	@RestoreFolder nvarchar(4000),
	@DB_HisNum int
	,@Num_HisDB int	-- 临时变量:已有历史库个数
	
	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@BakFile nvarchar(4000)		-- 临时变量:备份文件
	,@FullBakFile nvarchar(4000)	-- 完整备份文件
	,@TodayFullTime	datetime	-- 当天完整备份的时间
	,@BakFileName	nvarchar(512)	-- 完整备份文件名(不含目录)
	,@ProductVersion	decimal
	;
SELECT @JobTime=Convert(nvarchar(16),GetDate(),120) + ':00'	--只精确到分

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,DBName,BakFolder+CASE RIGHT(BakFolder,1) WHEN '\' THEN '' ELSE '\' END,FTPFolder+CASE RIGHT(FTPFolder,1) WHEN '\' THEN '' ELSE '\' END
	,FullTime,PreFullTime,PreBakTime,Num_Full,ReadyAction,NeedRestore,RestoreFolder,DB_HisNum
  FROM bak.BakCfg
 WHERE Enabled = 1 AND ReadyAction > 0;

-- 接收cmd返回结果
CREATE TABLE #t(s nvarchar(4000));

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@BakFolder,@FTPFolder,@FullTime,@PreFullTime,@PreBakTime,@Num_Full,@ReadyAction,@NeedRestore,@RestoreFolder,@DB_HisNum;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 计算当天完整备份的时间
	IF((@ReadyAction & 1) <> 0)
	BEGIN
		UPDATE bak.BakCfg SET State = State | 1 WHERE ID = @ID;
	
		-- 先删除历史备份文件 ----------------------------------------------------------------------
		-- 删除保留个数以外的备份文件
		TRUNCATE TABLE #t;
		SELECT @cmd = 'dir /a:-d/b/o:n "' + @FTPFolder + @DBName + '[*].*"';
		INSERT #t(s) EXEC master..xp_cmdshell @cmd;
		
		SELECT TOP(@Num_Full) @FullFileToDel = s
		  FROM #t
		 WHERE Len(s) > Len(@DBName) + 6
			AND Left(s,Len(@DBName)+1) = @DBName+'['
			AND SubString(s,Len(@DBName)+ 15,5) = '].bak'
		 ORDER BY s DESC;
		IF(@@ROWCOUNT >= @Num_Full)
		BEGIN
			SELECT @cmd = '';
			SELECT @cmd = @cmd + '&&del /F /Q "' + @FTPFolder + s + '"'
			  FROM #t
			 WHERE s <= @FullFileToDel;

			IF(Len(@cmd) > 2)
			BEGIN
				SELECT @cmd = Right(@cmd,Len(@cmd)-2);
				SELECT @cmd;
				EXEC master..xp_cmdshell @cmd;
			END
		END
		-- =========================================================================================

		--完整备份
		EXEC @rtn = bak.Apq_Bak_Full @DBName, @BakFileName OUT;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			SELECT @TodayFullTime = CONVERT(nvarchar(11),@JobTime,120) + Right(Convert(nvarchar,@FullTime,120),8);
			UPDATE bak.BakCfg
			   SET PreBakTime = @JobTime, PreFullTime = @TodayFullTime, ReadyAction = 0
			 WHERE ID = @ID;

			-- 本地恢复历史库
			IF(@NeedRestore & 1 <> 0)
			BEGIN
				-- 删除历史库
				SELECT @Num_HisDB = Count(name)
				  FROM sys.databases
				 WHERE Len(name) > Len(@DBName) AND Left(name,Len(@DBName)+1) = @DBName + '_';
				IF(@Num_HisDB >= @DB_HisNum)
				BEGIN
					SELECT @sql = '';
					SELECT TOP(@Num_HisDB-@DB_HisNum+1) @sql = @sql + 'EXEC dbo.Apq_KILL_DB ''' + name + ''';DROP DATABASE [' + name + '];'
					  FROM sys.databases
					 WHERE Len(name) > Len(@DBName) AND Left(name,Len(@DBName)+1) = @DBName + '_'
					 ORDER BY name;
					EXEC sp_executesql @sql;
				END
				
				DECLARE @DBName_R nvarchar(256);
				SELECT @DBName_R = @DBName + '_' + LEFT(REPLACE(CONVERT(nvarchar,Dateadd(dd,-1,Substring(@BakFileName,Len(@DBName)+2,8)),120),'-',''),8)
					,@FullBakFile = @FTPFolder + @BakFileName;
				EXEC bak.Apq_Restore 1,@DBName_R,@FullBakFile,1,@RestoreFolder
				
				SELECT @ProductVersion = CONVERT(decimal,LEFT(Convert(nvarchar,SERVERPROPERTY('ProductVersion')),2));
				IF(@ProductVersion<10)
				BEGIN
					SELECT @sql='BACKUP LOG '+@DBName_R+' WITH NO_LOG';
					EXEC sp_executesql @sql;
					SELECT @sqlDB = 'DBCC SHRINKFILE(''' + @DBName + '_Log'',10)'
					SELECT @sql = 'EXEC [' + @DBName_R + ']..sp_executesql @sqlDB';
					EXEC sp_executesql @sql,N'@sqlDB nvarchar(4000)',@sqlDB=@sqlDB;
				END
			END
		END
		
		UPDATE bak.BakCfg SET State = State & (~1) WHERE ID = @ID;
	END
	ELSE IF((@ReadyAction & 2) <> 0)
	BEGIN
		UPDATE bak.BakCfg SET State = State | 2 WHERE ID = @ID;
		
		--日志备份
		EXEC @rtn = bak.Apq_Bak_Trn @DBName;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			UPDATE bak.BakCfg SET PreBakTime=@JobTime,ReadyAction = ReadyAction & (~2) WHERE ID = @ID;
		END
		
		UPDATE bak.BakCfg SET State = State & (~2) WHERE ID = @ID;
	END

	NEXT_DB:
	FETCH NEXT FROM @csr INTO @ID,@DBName,@BakFolder,@FTPFolder,@FullTime,@PreFullTime,@PreBakTime,@Num_Full,@ReadyAction,@NeedRestore,@RestoreFolder,@DB_HisNum;
END
CLOSE @csr;

DROP TABLE #t;
RETURN 1;
GO
