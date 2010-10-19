IF( OBJECT_ID('bak.Apq_Restore', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE bak.Apq_Restore AS BEGIN RETURN END';
GO
ALTER PROC bak.Apq_Restore
	 @Action	tinyint	-- 动作{0x01:是否执行,0x02:是否打印语句,0x04:是否以结果集显示语句}
	,@DBName		nvarchar(128)	-- 从完整备份恢复时,物理文件名为 数据库名[文件组名].(mdf/ldf)
	,@FileFullName	nvarchar(4000)	-- 备份文件全名(含路径)
	,@RType			tinyint = 1	-- 还原类型{1:完整,2:日志}
	,@RFolder		nvarchar(4000) = ''	-- MOVE 目标目录
	,@NORECOVERY	tinyint = 0	-- 完整还原时 NORECOVERY 选项选择(日志还原一定使用STANDBY){0:无(即RECOVERY),1:NORECOVERY,2:STANDBY}
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-19
-- 描述: 还原数据库(完整/日志)
-- 参数:
@DBName: 还原使用的数据库名
@FileFullName: 备份文件全名(含路径)
@RType: 还原类型{1:完整,2:日志}
@RFolder: 还原使用的目录
@NORECOVERY: 完整还原时 NORECOVERY 选项选择(日志还原一定使用STANDBY){0:无(即RECOVERY),1:NORECOVERY,2:STANDBY}
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_Restore;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@RType IS NULL) SELECT @RType = 1;
IF(@RFolder IS NULL) SELECT @RFolder = '';

--定义变量
DECLARE @Restore_MOVE nvarchar(max)--Restore语句的MOVE子句

	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(max)
	,@SPBeginTime datetime
	,@LogicalName nvarchar(128)
	,@Type char(1)
	,@StdbFileFullName nvarchar(4000)	-- 临时变量:StandBy文件全名
	;
SELECT @SPBeginTime=GetDate();

CREATE TABLE #FileList(
	LogicalName nvarchar(128),
	Physicalname nvarchar(260),
	Type char(1),
	FileGroupName nvarchar(128),
	Size numeric(20,0),
	MaxSize numeric(20),
	FileID bigint,
	CreateLSN numeric(25,0),
	DropLSN numeric(25,0) NULL,
	UniqueID uniqueidentifier,
	ReadOnlyLSN numeric(25,0) NULL,
	ReadWriteLSN numeric(25,0) NULL,
	BackupSizeInBytes bigint,
	SourceBlockSize int,
	FileGroupID int ,
	LogGroupGUID uniqueidentifier NULL,
	DifferentialBaseLSN numeric(25,0) NULL,
	DifferentialBaseGUID uniqueidentifier,
	IsReadOnly bit,
	IsPresent bit
);

--定义游标
DECLARE @csrF CURSOR
SET @csrF=CURSOR FOR
SELECT LogicalName,Type FROM #FileList WHERE Type IN ('D','L');

-- 创建还原目录
IF(Len(@RFolder)>3)
BEGIN
	IF(Right(@RFolder,1) <> '\') SELECT @RFolder = @RFolder + '\';
	SELECT @cmd = 'md ' + LEFT(@RFolder, LEN(@RFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 踢数据库用户
EXEC dbo.Apq_KILL_DB @DBName = @DBName;

SELECT @StdbFileFullName = @RFolder + @DBName + '.lsf'

IF(@RType = 1)	-- 完整还原
BEGIN
	SELECT @sql = 'RESTORE FILELISTONLY FROM DISK=@BakFile',@Restore_MOVE = '';
	INSERT #FileList(LogicalName,Physicalname,Type,FileGroupName,Size,MaxSize,FileID,CreateLSN,DropLSN
		,UniqueID,ReadOnlyLSN,ReadWriteLSN,BackupSizeInBytes,SourceBlockSize,FileGroupID,LogGroupGUID
		,DifferentialBaseLSN,DifferentialBaseGUID,IsReadOnly,IsPresent)
	EXEC sp_executesql @sql,N'@BakFile nvarchar(512)', @BakFile=@FileFullName;

	OPEN @csrF;
	FETCH NEXT FROM @csrF INTO @LogicalName,@Type;
	WHILE(@@FETCH_STATUS=0)
	BEGIN
		SELECT @Restore_MOVE = @Restore_MOVE + ',
	MOVE ''' + @LogicalName + ''' TO ''' + @RFolder + @DBName + '[' + @LogicalName
			+ '].' + CASE @Type WHEN 'D' THEN 'mdf' ELSE 'ldf' END + '''';

		NEXT_File:
		FETCH NEXT FROM @csrF INTO @LogicalName,@Type;
	END
	CLOSE @csrF;
	
	SELECT @sql = 'DBCC TRACEON(1807);
RESTORE DATABASE @DBName FROM DISK=@BakFile WITH REPLACE' + CASE @NORECOVERY
			WHEN 1 THEN ', NORECOVERY'
			WHEN 2 THEN ', STANDBY=@StdbFileFullName'
			ELSE ''
		END + @Restore_MOVE;
	IF(@Action & 4 > 0) SELECT sql = @sql;
	IF(@Action & 2 > 0) PRINT @sql;
	IF(@Action & 1 > 0)
		EXEC sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000),@StdbFileFullName nvarchar(4000)'
			,@DBName = @DBName, @BakFile = @FileFullName, @StdbFileFullName = @StdbFileFullName;
END

IF(@RType = 2)	-- 日志还原
BEGIN
	SELECT @sql = 'RESTORE LOG @DBName FROM DISK=@BakFile WITH ' + CASE @NORECOVERY
			WHEN 1 THEN 'NORECOVERY'
			WHEN 2 THEN 'STANDBY=@StdbFileFullName'
			ELSE ''
		END;
	IF(@Action & 4 > 0) SELECT sql = @sql;
	IF(@Action & 2 > 0) PRINT @sql;
	IF(@Action & 1 > 0)
		EXEC sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000),@StdbFileFullName nvarchar(4000)'
			,@DBName = @DBName, @BakFile = @FileFullName, @StdbFileFullName = @StdbFileFullName;
END

DROP TABLE #FileList;
RETURN 1;
GO
 