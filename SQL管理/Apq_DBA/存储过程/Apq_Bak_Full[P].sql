IF( OBJECT_ID('bak.Apq_Bak_Full', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Apq_Bak_Full AS BEGIN RETURN END';
GO
ALTER PROC bak.Apq_Bak_Full
	 @DBName		nvarchar(256)
	,@BakFileName	nvarchar(256) OUT	-- 备份文件名(不含路径)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 完整备份(仅本地)
-- 参数:
@DBName: 数据库名
@BakFolder: 备份路径
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_Bak_Full 'Apq_DBA';
SELECT @rtn;
-- =============================================
-2: 空间不足
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @BakFileFullName nvarchar(4000)
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	,@ID bigint
	,@BakFolder nvarchar(4000)
	,@FTPFolder nvarchar(4000)
	,@FTPFolderT nvarchar(4000)
	,@FTPFileFullName nvarchar(4000)
	,@NeedTruncate tinyint;
SELECT @SPBeginTime=GetDate();
SELECT @BakFolder = '', @NeedTruncate = 0;
SELECT @BakFolder = BakFolder, @FTPFolder = FTPFolder,@FTPFolderT = FTPFolderT, @NeedTruncate = NeedTruncate
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
SELECT @BakFileName = @DBName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].bak';
SELECT @BakFileFullName = @BakFolder + @BakFileName;
SELECT @cmd = '@echo [' + convert(nvarchar,@SPBeginTime,120) + ']' + @BakFileName + '>>' + @BakFolder + '[Log]Apq_Bak.txt'
EXEC @rtn = master..xp_cmdshell @cmd;

-- 检测剩余空间 ------------------------------------------------------------------------------------
DECLARE @spused float, @disksp float;
SELECT @sql = '
CREATE TABLE #spused(
	name		nvarchar(256),
	rows		varchar(11),
	reserved	varchar(18),
	data		varchar(18),
	index_size	varchar(18),
	unused		varchar(18)
);
EXEC ' + @DBName + '..sp_MSforeachtable "INSERT #spused EXEC sp_spaceused ''?'', ''true''";
SELECT @spused = 0;
SELECT @spused = @spused + LEFT(reserved,LEN(reserved)-3) FROM #spused;
SELECT @spused = @spused / 1024;
DROP TABLE #spused;
';
EXEC @rtn = sp_executesql @sql, N'@spused float out', @spused = @spused out;
CREATE TABLE #drives(
	drive	varchar(5),
	MB		float
);
INSERT #drives
EXEC master..xp_fixeddrives;

IF(EXISTS(SELECT TOP 1 1 FROM #drives WHERE MB < @spused * 0.7 AND drive IN(LEFT(@BakFolder,1),LEFT(@FTPFolder,1)))) -- 暂取0.7
RETURN -2;
-- =================================================================================================

--截断日志(仅限2000使用)
IF(@NeedTruncate=1)
BEGIN
	SELECT @sql='BACKUP LOG '+@DBName+' WITH NO_LOG';
	EXEC sp_executesql @sql;
END

SELECT @sql = 'BACKUP DATABASE @DBName TO DISK=@BakFile WITH INIT';
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
