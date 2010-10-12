IF( OBJECT_ID('bak.Job_Apq_FTP_PutBak', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Job_Apq_FTP_PutBak AS BEGIN RETURN END';
GO
ALTER PROC bak.Job_Apq_FTP_PutBak
	 @TransferID	int	-- 传送者ID
	,@TransRowCount	int	-- 传送行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-05-12
-- 描述: 通过FTP上传备份文件
-- 原因: FTP下载时是先写入临时文件,可能引起空间不足
-- 示例:
EXEC bak.Job_Apq_FTP_PutBak 1,100;
-- =============================================
*/
SET NOCOUNT ON;

IF(@TransferID IS NULL) SELECT @TransferID = 1;
IF(@TransRowCount IS NULL) SELECT @TransRowCount = 100;

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	,@ScpFolder nvarchar(4000)
	,@ScpFileName nvarchar(128)
	,@ScpFullName nvarchar(4000)
	,@LFullName nvarchar(4000)	 -- 临时变量:本地文件全名
	,@FullFileToDel nvarchar(4000)--在此文件之前的文件将被删除
	;
SELECT @SPBeginTime=GetDate(),@ScpFolder = 'D:\Apq_DBA\Scp\'
SELECT @ScpFileName = 'Job_Apq_FTP_PutBak['+LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].scp';
SELECT @ScpFullName = @ScpFolder + @ScpFileName;
IF(RIGHT(@ScpFolder,1)<>'\') SELECT @ScpFolder = @ScpFolder+'\';
SELECT @cmd = 'md ' + LEFT(@ScpFolder,LEN(@ScpFolder)-1);
EXEC master..xp_cmdshell @cmd;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@TransRowCount) ID,DBName,LastFileName,FTPSrv,Folder,U,P,FTPFolder,FTPFolderTmp,Num_Full
  FROM bak.FTP_PutBak
 WHERE Enabled = 1
	AND ((TransferIDCfg = 0 AND TransferIDRun = 0) OR TransferIDCfg = @TransferID)

DECLARE @csrFile CURSOR;
-- 游标内临时变量
DECLARE @FTPFileName nvarchar(512)
	,@DBName_R nvarchar(256)
	;
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));
CREATE TABLE #t2(s nvarchar(4000));

DECLARE @ID bigint,@DBName nvarchar(256),@LastFileName nvarchar(256),@FTPSrv nvarchar(256),@Folder nvarchar(512)
	,@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512),@Num_Full int;
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	UPDATE bak.FTP_PutBak SET [_Time] = getdate(),TransferIDRun = @TransferID WHERE ID = @ID;
	
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	IF(RIGHT(@FTPFolder,1)<>'/') SELECT @FTPFolder = @FTPFolder+'/';
	IF(RIGHT(@FTPFolderTmp,1)<>'/') SELECT @FTPFolderTmp = @FTPFolderTmp+'/';
	SELECT @cmd = 'md ' + LEFT(@Folder,LEN(@Folder)-1);
	EXEC master..xp_cmdshell @cmd;

	-- 传送文件 -----------------------------------------------------------------------------------
	TRUNCATE TABLE #t1;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @Folder + @DBName + '[*].*"';
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
		SELECT @cmd = 'echo open ' + @FTPSrv + '>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo user ' + @U + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo ' + @P + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo cd "' + @FTPFolderTmp + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo lcd "' + LEFT(@Folder,LEN(@Folder)-1) + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
	
		SELECT @cmd = 'echo binary>>"' + @ScpFullName + '"';-- 将文件传输类型设置为二进制
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo put "' + @FTPFileName + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo rename "' + @FTPFileName + '" "' + @FTPFolder + @FTPFileName + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo bye>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'ftp -i -n -s:"' + @ScpFullName + '"';
		--SELECT @cmd;
		TRUNCATE TABLE #t;
		INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
		IF(@@ERROR <> 0 OR @rtn <> 0)
		BEGIN
			CLOSE @csrFile;
			GOTO NEXT_DB;
		END
		SELECT * FROM #t;
		IF(EXISTS(SELECT TOP 1 1 FROM #t WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%' OR s LIKE 'Permission denied%'
				OR s LIKE '%Unable to rename%'))
		BEGIN
			CLOSE @csrFile;
			GOTO NEXT_DB;
		END
		
		-- 更新最后文件名
		IF(@FTPFileName IS NOT NULL) UPDATE bak.FTP_PutBak SET _Time = getdate(), LastFileName = @FTPFileName WHERE ID = @ID;

		NEXT_File:
		FETCH NEXT FROM @csrFile INTO @FTPFileName
	END
	CLOSE @csrFile;
	-- =============================================================================================
	
	-- 删除本地历史文件 ----------------------------------------------------------------------------
	-- 删除保留个数以外的备份文件
	TRUNCATE TABLE #t;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @Folder + @DBName + '[*].*"';
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
		SELECT @cmd = @cmd + '&&del /F /Q "' + @Folder + s + '"'
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
	-- 删除FTP命令文件
	SELECT @cmd = 'del ""' + @ScpFullName + '"" /q';
	EXEC master..xp_cmdshell @cmd;
	-- 恢复空闲状态
	UPDATE bak.FTP_PutBak SET _Time = getdate(), TransferIDRun = 0 WHERE ID = @ID;

	FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
DROP TABLE #t2;
RETURN 1;
GO
