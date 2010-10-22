IF( OBJECT_ID('bak.Job_Apq_RestoreFromFolder', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Job_Apq_RestoreFromFolder AS BEGIN RETURN END';
GO
ALTER PROC bak.Job_Apq_RestoreFromFolder
	 @RunnerID		int	-- 执行者ID
	,@RunRowCount	int	-- 执行行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-05-12
-- 描述: 从指定文件夹寻找最新的备份文件按设置还原数据库
-- 示例:
EXEC bak.Job_Apq_RestoreFromFolder 1,100;
-- =============================================
*/
SET NOCOUNT ON;

IF(@RunnerID IS NULL) SELECT @RunnerID = 1;
IF(@RunRowCount IS NULL) SELECT @RunRowCount = 100;

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@LFullName nvarchar(4000)	 -- 临时变量:本地文件全名
	,@Num_HisDB int	-- 临时变量:已有历史库个数
	,@FullFileToDel nvarchar(4000)--在此文件之前的文件将被删除
	,@ProductVersion	decimal
	;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@RunRowCount) ID, DBName, LastFileName, BakFolder, Num_Full, RestoreType, RestoreFolder, DB_HisNum
  FROM bak.RestoreFromFolder
 WHERE Enabled = 1
	AND ((RunnerIDCfg = 0 AND RunnerIDRun = 0) OR RunnerIDCfg = @RunnerID)

DECLARE @csrFile CURSOR;
-- 游标内临时变量
DECLARE @FTPFileName nvarchar(512)
	,@DBName_R nvarchar(256)
	;
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));
CREATE TABLE #t2(s nvarchar(4000));

DECLARE @ID bigint,@DBName nvarchar(256),@LastFileName nvarchar(256),@BakFolder nvarchar(4000),@Num_Full int
	,@RestoreType int,@RestoreFolder nvarchar(4000),@DB_HisNum int;
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@BakFolder,@Num_Full,@RestoreType,@RestoreFolder,@DB_HisNum;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	UPDATE bak.RestoreFromFolder SET [_Time] = getdate(),RunnerIDRun = @RunnerID WHERE ID = @ID;
	
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	
	-- 还原数据库 ----------------------------------------------------------------------------------
	TRUNCATE TABLE #t1;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @BakFolder + @DBName + '[*].*"';
	INSERT #t1(s) EXEC master..xp_cmdshell @cmd;
	
	TRUNCATE TABLE #t2;
	INSERT #t2(s)
	SELECT s FROM #t1
	 WHERE s > @LastFileName AND Len(s) > Len(@DBName) + 6
		AND Left(s,Len(@DBName)+1) = @DBName+'['
		AND SubString(s,Len(@DBName)+ 15,5) IN ('].bak','].trn')
	 ORDER BY s;
	
	SET @csrFile = CURSOR FOR
	SELECT s FROM #t2;
	
	SELECT @FTPFileName = NULL;
	OPEN @csrFile;
	FETCH NEXT FROM @csrFile INTO @FTPFileName
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		SELECT @DBName_R = @DBName + '_' + LEFT(REPLACE(CONVERT(nvarchar,Dateadd(dd,-1,Substring(@FTPFileName,Len(@DBName)+2,8)),120),'-',''),8)
		SELECT @LFullName = @BakFolder + @FTPFileName;
		-- 恢复完整备份到历史数据库
		IF(SubString(@FTPFileName,Len(@DBName)+ 15,5) = '].bak' AND @RestoreType & 1 <> 0 AND Len(@RestoreFolder) > 1)
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
			
			EXEC @rtn = bak.Apq_Restore 1,@DBName_R,@LFullName,1,@RestoreFolder,2
			IF(@@ERROR <> 0 OR @rtn <> 1)
			BEGIN
				CLOSE @csrFile;
				GOTO NEXT_DB;
			END
			
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
		
		-- 恢复日志备份到备用数据库
		ELSE IF(@RestoreType & 2 <> 0)
		BEGIN
			IF(SubString(@FTPFileName,Len(@DBName)+ 15,5) = '].bak')
			BEGIN
				EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,1,@RestoreFolder,2;	-- 日志传送
				--EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,1,@RestoreFolder,1;	-- 日志还原
				IF(@@ERROR <> 0 OR @rtn <> 1)
				BEGIN
					CLOSE @csrFile;
					GOTO NEXT_DB;
				END
			END
			IF(SubString(@FTPFileName,Len(@DBName)+ 15,5) = '].trn')
			BEGIN
				EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,2,@RestoreFolder,2	-- 日志传送
				--EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,2,@RestoreFolder,1	-- 日志还原
				IF(@@ERROR <> 0 OR @rtn <> 1)
				BEGIN
					CLOSE @csrFile;
					GOTO NEXT_DB;
				END;
			END
		END
	
		-- 更新最后文件名
		IF(@FTPFileName IS NOT NULL) UPDATE bak.RestoreFromFolder SET [_Time] = getdate(),LastFileName = @FTPFileName WHERE ID = @ID;

		NEXT_File:
		FETCH NEXT FROM @csrFile INTO @FTPFileName
	END
	CLOSE @csrFile;
	-- =============================================================================================
	
	-- 删除本地历史文件 ----------------------------------------------------------------------------
	-- 删除保留个数以外的备份文件
	TRUNCATE TABLE #t;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @BakFolder + @DBName + '[*].*"';
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
		SELECT @cmd = @cmd + '&&del /F /Q "' + @BakFolder + s + '"'
		  FROM #t
		 WHERE s < @FullFileToDel;

		IF(Len(@cmd) > 2)
		BEGIN
			SELECT @cmd = Right(@cmd,Len(@cmd)-2);
			SELECT @cmd;
			EXEC master..xp_cmdshell @cmd;
		END
	END
	-- =============================================================================================
	
	NEXT_DB:
	-- 恢复空闲状态
	UPDATE bak.RestoreFromFolder SET [_Time] = getdate(),RunnerIDRun = 0 WHERE ID = @ID;

	FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@BakFolder,@Num_Full,@RestoreType,@RestoreFolder,@DB_HisNum;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
DROP TABLE #t2;
RETURN 1;
GO
