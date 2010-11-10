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
	,@ncFtp nvarchar(4000)
	,@ncFtpLs nvarchar(4000)
	,@ncFtpPut nvarchar(4000)
	,@LFullName nvarchar(4000)	 -- 临时变量:本地文件全名
	,@FullFileToDel nvarchar(4000)--在此文件之前的文件将被删除
	;
SELECT @SPBeginTime=GetDate()
	,@ncFtp = dbo.Apq_Ext_Get('ncftp',0,'ncftp')
	,@ncFtpLs = dbo.Apq_Ext_Get('ncftp',0,'ncFtpLs')
	,@ncFtpPut = dbo.Apq_Ext_Get('ncftp',0,'ncFtpPut')
	;
IF(@ncFtp IS NULL OR Len(@ncFtp) < 2) SELECT @ncFtp = '"D:\Programs\ncftp\ncftp.exe"';
IF(@ncFtpLs IS NULL OR Len(@ncFtpLs) < 2) SELECT @ncFtpLs = '"D:\Programs\ncftp\ncftpls.exe"';
IF(@ncFtpPut IS NULL OR Len(@ncFtpPut) < 2) SELECT @ncFtpPut = '"D:\Programs\ncftp\ncftpput.exe"';

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@TransRowCount) ID,Folder,FileName,LTRIM(RTRIM(FTPSrv)),FTPPort,U,P,FTPFolder,FTPFolderTmp,LSize,RSize
  FROM dbo.FTP_SendQueue
 WHERE Enabled = 1 AND IsSuccess = 0
 ORDER BY FileName

-- 游标内临时变量
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));

DECLARE @ID bigint,@Folder nvarchar(512),@FileName nvarchar(256),@LSize bigint,@RSize bigint,
	@FTPSrv nvarchar(256),@FTPPort int,@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512);
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@Folder,@FileName,@FTPSrv,@FTPPort,@U,@P,@FTPFolder,@FTPFolderTmp,@LSize,@RSize;
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
	SELECT @cmd = @ncFtpPut + ' -u ' + @U + ' -p ' + @P + ' -P '+ Convert(nvarchar(21),@FTPPort) + ' -m -T _up ' + @FTPSrv + ' ' + @FTPFolder + ' ' + @Folder + @FileName;
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	
	-- 获取远程文件大小 ------------------------------------------------------------------------
	SELECT @cmd = @ncFtpLs + ' -u ' + @U + ' -p ' + @P + ' -P '+ Convert(nvarchar(21),@FTPPort) + ' -l ftp://' + @FTPSrv + @FTPFolder + @FileName;
	--SELECT @cmd;
	TRUNCATE TABLE #t;
	INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
	--SELECT * FROM #t;
	DECLARE @sRDir nvarchar(4000);
	SELECT TOP 1 @sRDir = s FROM #t WHERE RIGHT(s,Len(@FileName)) = @FileName;
	--SELECT @sRDir;
	/*
-rw-rw-rw-   1 user     group      550912 Nov  9 17:31 BaseBusinessDb[20101109_1731].bak
-rw-rw-rw-   1 user     group    42460672 Oct 25 14:18 PayCenter[20101022_2150].trn 
-rw-rw-rw-   1 user     group    101897728 Oct 22 20:04 StreamMedia[20101022_1951].bak 
规律:从34列开始为文件大小,最少占8字符,不足8位数字前补0,因此,从42列开始查找下一个空格,以此作为结束点.
	*/
	DECLARE @sidx int;
	SELECT @sidx = charindex(' ',@sRDir,42);
	--SELECT @sidx;
	SELECT @RSize = Replace(LTRIM(RTRIM(Substring(@sRDir,34,@sidx-34))),',','');
	--SELECT @RSize;
	IF(@RSize>0)
	BEGIN
		UPDATE dbo.FTP_SendQueue SET [_Time] = getdate(),RSize = @RSize WHERE ID = @ID;
	END
	
	-- 上传成功
	IF(@LSize = @RSize) UPDATE dbo.FTP_SendQueue SET _Time = getdate(), IsSuccess = 1 WHERE ID = @ID;
	-- =============================================================================================
	
	NEXT_Row:
	FETCH NEXT FROM @csr INTO @ID,@Folder,@FileName,@FTPSrv,@FTPPort,@U,@P,@FTPFolder,@FTPFolderTmp,@LSize,@RSize;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
RETURN 1;
GO
