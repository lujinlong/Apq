IF( OBJECT_ID('dbo.Job_FTP_Send', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_FTP_Send AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_FTP_Send
	@TransRowCount	int = 100	-- 传送行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-19
-- 描述: 通过FTP上传文件
-- 原因: FTP下载时是先写入临时文件,可能引起空间不足
-- 示例:
EXEC dbo.Job_FTP_Send 100;
-- =============================================
*/
SET NOCOUNT ON;

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
SELECT @ScpFileName = 'FTP_Send['+LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].scp';
SELECT @ScpFullName = @ScpFolder + @ScpFileName;
IF(RIGHT(@ScpFolder,1)<>'\') SELECT @ScpFolder = @ScpFolder+'\';
SELECT @cmd = 'md ' + LEFT(@ScpFolder,LEN(@ScpFolder)-1);
EXEC master..xp_cmdshell @cmd;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@TransRowCount) ID,Folder,FileName,FTPSrv,U,P,FTPFolder,FTPFolderTmp,LSize,RSize
  FROM dbo.FTP_SendQueue
 WHERE Enabled = 1 AND IsSuccess = 0

-- 游标内临时变量
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));

DECLARE @ID bigint,@Folder nvarchar(512),@FileName nvarchar(256),@LSize bigint,@RSize bigint,
	@FTPSrv nvarchar(256),@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512);
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@Folder,@FileName,@FTPSrv,@U,@P,@FTPFolder,@FTPFolderTmp,@LSize,@RSize;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	IF(RIGHT(@FTPFolder,1)<>'/') SELECT @FTPFolder = @FTPFolder+'/';
	IF(RIGHT(@FTPFolderTmp,1)<>'/') SELECT @FTPFolderTmp = @FTPFolderTmp+'/';
	SELECT @cmd = 'md ' + LEFT(@Folder,LEN(@Folder)-1);
	EXEC master..xp_cmdshell @cmd;
	
	-- 获取本地文件大小 ----------------------------------------------------------------------------
	SELECT @cmd = 'dir "' + @Folder + @FileName + '"';
	TRUNCATE TABLE #t1;
	INSERT #t1 EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		GOTO NEXT_Row;
	END
	
	DECLARE @sLDir nvarchar(4000);
	SELECT TOP 1 @sLDir = s FROM #t1 WHERE RIGHT(s,Len(@FileName)) = @FileName;
	--SELECT @sLDir;
	IF(@sLDir IS NOT NULL) SELECT @LSize = Replace(LTRIM(RTRIM(Substring(@sLDir,18,Len(@sLDir)-18-Len(@FileName)))),',','');
	IF(@LSize>0)
	BEGIN
		UPDATE dbo.FTP_SendQueue SET [_Time] = getdate(),LSize = @LSize WHERE ID = @ID;
	END

	-- 传送文件 ------------------------------------------------------------------------------------
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
	SELECT @cmd = 'echo put "' + @FileName + '">>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo rename "' + @FileName + '" "' + @FTPFolder + @FileName + '">>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo dir "' + @FTPFolder + @FileName + '">>"' + @ScpFullName + '"';	-- 获取已上传文件大小
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo bye>>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'ftp -i -n -s:"' + @ScpFullName + '"';
	--SELECT @cmd;
	TRUNCATE TABLE #t;
	INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		GOTO NEXT_Row;
	END
	SELECT * FROM #t;
	IF(EXISTS(SELECT TOP 1 1 FROM #t WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%' OR s LIKE 'Permission denied%'
			OR s LIKE '%Unable to rename%'))
	BEGIN
		GOTO NEXT_Row;
	END
	-- 获取远程文件大小 ------------------------------------------------------------------------
	DECLARE @sRDir nvarchar(4000);
	SELECT TOP 1 @sRDir = s FROM #t WHERE Left(RIGHT(s,Len(@FileName)+1),Len(@FileName)) = @FileName;
	--SELECT @sRDir;	-- -rw-rw-rw-   1 user     group    101897728 Oct 22 20:04 StreamMedia[20101022_1951].bak 
	IF(@sRDir IS NOT NULL) SELECT @RSize = Replace(LTRIM(RTRIM(Substring(@sRDir,30,14))),',','');
	IF(@RSize>0)
	BEGIN
		UPDATE dbo.FTP_SendQueue SET [_Time] = getdate(),RSize = @RSize WHERE ID = @ID;
	END
	
	-- 上传成功
	IF(@LSize = @RSize) UPDATE dbo.FTP_SendQueue SET _Time = getdate(), IsSuccess = 1 WHERE ID = @ID;
	-- =============================================================================================
	
	NEXT_Row:
	-- 删除FTP命令文件
	SELECT @cmd = 'del ""' + @ScpFullName + '"" /q';
	EXEC master..xp_cmdshell @cmd;

	FETCH NEXT FROM @csr INTO @ID,@Folder,@FileName,@FTPSrv,@U,@P,@FTPFolder,@FTPFolderTmp,@LSize,@RSize;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
RETURN 1;
GO
