IF( OBJECT_ID('bak.Job_Apq_Bak_FTP_Enqueue', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Job_Apq_Bak_FTP_Enqueue AS BEGIN RETURN END';
GO
ALTER PROC bak.Job_Apq_Bak_FTP_Enqueue
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-20
-- 描述: 通过FTP队列上传备份文件
-- 示例:
EXEC bak.Job_Apq_Bak_FTP_Enqueue;
-- =============================================
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @cmd nvarchar(4000);
SELECT @SPBeginTime=GetDate();

DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT ID,DBName,LastFileName,FTPSrv,Folder,U,P,FTPFolder,FTPFolderTmp,Num_Full
  FROM bak.FTP_PutBak
 WHERE Enabled = 1;

DECLARE @csrFile CURSOR;
DECLARE @FTPFileName nvarchar(512);
CREATE TABLE #t1(s nvarchar(4000));
CREATE TABLE #t2(s nvarchar(4000));

DECLARE @ID bigint,@DBName nvarchar(256),@LastFileName nvarchar(256),@FTPSrv nvarchar(256),@FTPPort int,@Folder nvarchar(512)
	,@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512),@Num_Full int;
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@FTPPort,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	IF(RIGHT(@FTPFolder,1)<>'/') SELECT @FTPFolder = @FTPFolder+'/';
	IF(RIGHT(@FTPFolderTmp,1)<>'/') SELECT @FTPFolderTmp = @FTPFolderTmp+'/';
	SELECT @cmd = 'md ' + LEFT(@Folder,LEN(@Folder)-1);
	EXEC master..xp_cmdshell @cmd;

	-- 传送入队 -----------------------------------------------------------------------------------
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
	
	INSERT dbo.FTP_SendQueue ( Folder,FileName,Enabled,FTPSrv,FTPPort,U,P,FTPFolder,FTPFolderTmp )
	SELECT @Folder,s,1,@FTPSrv,@FTPPort,@U,@P,@FTPFolder,@FTPFolderTmp
	  FROM #t2
	
	SELECT @FTPFileName = NULL;
	SELECT @FTPFileName = Max(s) FROM #t2;
	-- 更新最后文件名
	IF(@FTPFileName IS NOT NULL) UPDATE bak.FTP_PutBak SET _Time = getdate(), LastFileName = @FTPFileName WHERE ID = @ID;
	-- =============================================================================================
	
	NEXT_DB:
	FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@FTPPort,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
END
CLOSE @csr;

Quit:
DROP TABLE #t1;
DROP TABLE #t2;
RETURN 1;
GO
