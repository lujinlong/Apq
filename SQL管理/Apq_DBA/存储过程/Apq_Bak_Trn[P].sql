IF( OBJECT_ID('bak.Apq_Bak_Trn', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Apq_Bak_Trn AS BEGIN RETURN END';
GO
ALTER PROC bak.Apq_Bak_Trn
	@DBName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 日志备份(仅本地)
-- 参数:
@DBName: 数据库名
@BakFolder: 备份路径
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_Bak_Trn 'dtxc';
SELECT @rtn;
-- =============================================
1: 备份成功
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @BakFileName nvarchar(256), @BakFileFullName nvarchar(4000)
	,@cmd nvarchar(4000)
	,@sql nvarchar(4000)
	,@ID bigint
	,@BakFolder nvarchar(4000)
	,@FTPFolder nvarchar(4000)
	,@FTPFolderT nvarchar(4000)
	,@FTPFileFullName nvarchar(4000);
SELECT @SPBeginTime=GetDate();
SELECT @BakFolder = '';
SELECT @BakFolder = BakFolder,@FTPFolder = FTPFolder,@FTPFolderT = FTPFolderT
  FROM bak.BakCfg
 WHERE DBName = @DBName;

IF(Len(@BakFolder)>3)
BEGIN
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@BakFolder, 1, LEN(@BakFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolder)>3)
BEGIN
	IF(RIGHT(@FTPFolder,1)<>'\') SELECT @FTPFolder = @FTPFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolderT)>3)
BEGIN
	IF(RIGHT(@FTPFolderT,1)<>'\') SELECT @FTPFolderT = @FTPFolderT+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 记录备份日志
SELECT @BakFileName = @DBName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].trn';
SELECT @BakFileFullName = @BakFolder + @BakFileName;
--SELECT @cmd = '@echo [' + convert(nvarchar,@SPBeginTime,120) + ']' + @BakFileName + '>>' + @BakFolder + '[Log]Apq_Bak.txt'
--EXEC @rtn = xp_cmdshell @cmd;

-- 备份
SELECT @sql = 'BACKUP LOG @DBName TO DISK=@BakFile';
EXEC @rtn = sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000)', @DBName = @DBName, @BakFile = @BakFileFullName;
IF(@@ERROR <> 0 OR @rtn<>0)
BEGIN
	RETURN -1;
END

-- 移动到FTP目录 -----------------------------------------------------------------------------------
IF(Len(@FTPFolderT)>0)
BEGIN
	SELECT @FTPFileFullName = @BakFolder + @BakFileName;
	SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1) + '"';
	EXEC master..xp_cmdshell @cmd;
END
ELSE
BEGIN
	SELECT @FTPFolderT = @BakFolder;
END
SELECT @FTPFileFullName = @FTPFolderT + @BakFileName;
SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1) + '"';
EXEC master..xp_cmdshell @cmd;
-- =================================================================================================

SELECT BakFileName = @BakFileName;
RETURN 1;
GO
