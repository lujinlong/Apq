IF( OBJECT_ID('dbo.Job_Apq_DTS_Send', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_Apq_DTS_Send AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_Apq_DTS_Send
	 @RunnerID	int	-- 传送者ID
	,@RunRowCount	int	-- 传送行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-08
-- 描述: 基于收集历史的数据传送作业
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_DTS_Send 1, 100;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@RunnerID IS NULL) SELECT @RunnerID = 1;
IF(@RunRowCount IS NULL) SELECT @RunRowCount = 100;

--定义变量
DECLARE @LocalSrvID nvarchar(32),
	@ID bigint,	-- 配置ID
	@CfgName nvarchar(256),		-- 配置名(唯一)
	@PickTime datetime,			-- 采集时间
	@HasContent tinyint,		-- 是否收集到数据
	@FileFolder nvarchar(3000),	-- 文件夹
	@FileName nvarchar(256),	-- 文件名
	@FileEX nvarchar(50),		-- 扩展名(.txt)
	@RunnerIDCfg int,			-- 指定执行者(0:未指定)
	@TransMethod tinyint,		-- 传送方式{1:BCP in,5:FTP}
	@SrvID int,					-- 服务器编号
	@DBName nvarchar(256),		-- 数据库名
	@SchemaName nvarchar(256),	-- 架构名
	@SPTName nvarchar(256),		-- 存储过程名
	@U nvarchar(256),
	@P nvarchar(256),
	@FTPIP tinyint,				-- FTP传送时选择IP{0:Lan,1:Wan1,2:Wan2}
	@FTPFolderTmp nvarchar(512),-- FTP 临时目录
	@FTPFolder nvarchar(512),	-- FTP 目录
	@LogID bigint,
	@t nvarchar(50),
	@r nvarchar(50),
	
	@LSName nvarchar(512),		-- 目标服务器别名
	@strFTPIP nvarchar(500),	-- 目标服务器FTPIP
	@FTPPort nvarchar(32),		-- 目标服务器FTP端口
	@ScpFullName nvarchar(3000),
	
	@rtn int, @cmd nvarchar(4000), @sql nvarchar(max)
	,@SPBeginTime datetime		-- 存储过程启动时间
	,@TodayTransTime datetime	-- 当天理论传送时间
	;
SELECT @SPBeginTime=GetDate();
SELECT @LocalSrvID = dbo.Apq_Ext_Get('',0,'LocalSrvID');

--定义游标
DECLARE @csr_DTS CURSOR;
SET @csr_DTS=CURSOR FOR
SELECT TOP(@RunRowCount) ds.ID, ds.CfgName, ds.TransMethod, ds.SrvID, rs.LSName, ds.DBName, ds.SchemaName, ds.SPTName, ds.U, ds.P, ds.FTPFolderTmp, ds.FTPFolder
	, LogID=l.ID,l.PickTime,l.HasContent,l.FileFolder + CASE WHEN RIGHT(l.FileFolder,1)<>'\' THEN '\' ELSE '' END,l.FileName, l.FileEX,t,r
  FROM dbo.DTS_Send ds INNER JOIN dbo.Log_DTS_LocalPick l ON l.CfgName = ds.CfgName INNER JOIN dbo.RSrvConfig rs ON ds.SrvID = rs.ID
 WHERE ds.Enabled = 1
	AND ((ds.RunnerIDCfg = 0 AND ds.RunnerIDRun = 0) OR ds.RunnerIDCfg = @RunnerID)
	AND l.TransTime IS NULL;

CREATE TABLE #t(s nvarchar(4000));

OPEN @csr_DTS;
FETCH NEXT FROM @csr_DTS INTO @ID,@CfgName,@TransMethod,@SrvID,@LSName,@DBName,@SchemaName,@SPTName,@U,@P,@FTPFolderTmp,@FTPFolder
	,@LogID,@PickTime,@HasContent,@FileFolder,@FileName,@FileEX,@t,@r;
WHILE(@@FETCH_STATUS=0)
BEGIN
	UPDATE dbo.DTS_Send SET [_Time] = getdate(),RunnerIDRun = @RunnerID WHERE ID = @ID;
	IF(@HasContent = 0) GOTO Success;	-- 无数据时直接认为成功

	-- 开始传送文件 ------------------------------------------------------------------------
	-- 1.BCP in
	IF(@TransMethod = 1)
	BEGIN
		SELECT @cmd = 'bcp "' + @DBName + '.' + @SchemaName + '.' + @SPTName + '" in "' + @FileFolder + @FileName + @FileEX
			+ '" -c -t' + CASE WHEN LEN(@t) > 0 THEN @t ELSE '\t' END	-- 列分隔符 推荐使用"~*"
			+ ' -r' + CASE WHEN LEN(@r) > 0 THEN @r ELSE '\n' END			-- 行分隔符 推荐使用"~*$"
			+ ' -U' + @U + ' -P' + @P + ' -S' + @LSName;
		--PRINT @cmd; GOTO NEXT_Trans;
		EXEC @rtn = master..xp_cmdshell @cmd;
		--PRINT @@ERROR; PRINT @rtn; GOTO NEXT_Trans;
		IF(@@ERROR <> 0 OR @rtn <> 0)
		BEGIN
			-- 失败
			GOTO NEXT_Trans;
		END
	END
	
	-- 5.FTP
	IF(@TransMethod = 5)
	BEGIN
		SELECT @ScpFullName = @FileFolder + @CfgName + '.scp';
		SELECT @strFTPIP = CASE @FTPIP WHEN 1 THEN IPWan1 WHEN 2 THEN IPWan2 ELSE IPLan END,@FTPPort = FTPPort
		  FROM dbo.RSrvConfig
		 WHERE ID = @SrvID;
	
		SELECT @cmd = 'echo open ' + @strFTPIP + ' ' + @FTPPort + '>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo user ' + @U + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo ' + @P + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo cd "' + @FTPFolderTmp + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo lcd "' + LEFT(@FileFolder,LEN(@FileFolder)-1) + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
	
		SELECT @cmd = 'echo binary>>"' + @ScpFullName + '"';-- 将文件传输类型设置为二进制
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo put "' + @FileName + @FileEX + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo rename "' + @FileName + @FileEX + '" "' + @FTPFolder + @FileName + @FileEX + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo bye>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'ftp -i -n -s:"' + @ScpFullName + '"';
		--SELECT @cmd;
		TRUNCATE TABLE #t;
		INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
		IF(@@ERROR <> 0 OR @rtn <> 0)
		BEGIN
			-- 失败
			GOTO NEXT_Trans;
		END
		SELECT * FROM #t;
		IF(EXISTS(SELECT TOP 1 1 FROM #t WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%'))
		BEGIN
			-- 失败
			GOTO NEXT_Trans;
		END
	END
	-- =====================================================================================
	
	Success:
	SELECT @sql = '
INSERT [' + @LSName + '].[' + @DBName + '].dbo.Log_DTS_Receive(CfgName, PickTime, RSrvID, SendTime)
VALUES(@CfgName,@PickTime,@RSrvID,@SendTime);
';
	EXEC @rtn = sp_executesql @sql,N'@CfgName nvarchar(256), @PickTime datetime, @RSrvID int, @SendTime datetime'
		,@CfgName = @CfgName,@PickTime = @PickTime, @RSrvID = @LocalSrvID, @SendTime = @SPBeginTime;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		-- 失败
		GOTO NEXT_Trans;
	END
	
	UPDATE dbo.Log_DTS_LocalPick
	   SET [_Time] = getdate(),TransTime = getdate()
	 WHERE ID = @LogID;
	
	SELECT @cmd = 'del "' + @FileFolder + @FileName + @FileEX + '" /f /q';
	EXEC master..xp_cmdshell @cmd;
	
	NEXT_Trans:
	UPDATE dbo.DTS_Send SET [_Time] = getdate(),RunnerIDRun = 0 WHERE ID = @ID;
	FETCH NEXT FROM @csr_DTS INTO @ID,@CfgName,@TransMethod,@SrvID,@LSName,@DBName,@SchemaName,@SPTName,@U,@P,@FTPFolderTmp,@FTPFolder
		,@LogID,@PickTime,@HasContent,@FileFolder,@FileName,@FileEX,@t,@r;
END
CLOSE @csr_DTS;

TRUNCATE TABLE #t;
DROP TABLE #t;

RETURN 1;
GO
