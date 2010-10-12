IF( OBJECT_ID('dbo.Apq_DataTrans', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_DataTrans AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_DataTrans
	 @TransName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-08-11
-- 描述: 数据传送(条件为OR)
-- 替换: 对STMT进行
^LastID$: 上一次传送的最大编号
^LastTime$: 上一次传送数据的最后时间
^MaxID$: 本次传送最大编号
^MaxTime$: 本次传送最后时间
-- 参数:
@TransName: 传送名
@TransMethod: 传送方法{1:BCP queryout & in,2:BCP out & in,3:远程SP,4:LinkServer,5:BCP queryout & FTP,6:BCP out & FTP}
@STMT: 获取源数据语句(3时为实参列表)
@SrvName: 目标服名(可为IP,端口)
@DBName: 目标数据库名
@SchemaName: 架构名
@SPTName: 存储过程名 或 表名(4时可含列名)
@LastID: 上一次传送的最大编号
@LastTime: 上一次传送数据的最后时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_DataTrans 'GameActor';
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

DECLARE @Return int, @rtn int, @SPBeginTime datetime, @cmd nvarchar(4000), @sql nvarchar(4000), @strSPBeginTimeTrim nvarchar(20);
SELECT @SPBeginTime=GetDate();
SELECT @strSPBeginTimeTrim = REPLACE(REPLACE(REPLACE(CONVERT(nvarchar(19),@SPBeginTime,120),'-',''),':',''),' ','_');

DECLARE @TransMethod tinyint
	,@Detect nvarchar(4000)
	,@STMT nvarchar(4000)
	,@SrvName nvarchar(256)
	,@DBName nvarchar(256)
	,@SchemaName nvarchar(256)
	,@SPTName nvarchar(256)
	,@LastID bigint
	,@LastTime datetime
	,@STMTMax nvarchar(4000)
	,@U nvarchar(256)
	,@P nvarchar(256)
	
	,@sqlSTMT nvarchar(4000)
	,@MaxID bigint
	,@MaxTime datetime
	;
SELECT @TransMethod = TransMethod, @Detect = Detect, @STMT = STMT, @SrvName = SrvName, @DBName = DBName, @SchemaName = SchemaName, @SPTName = SPTName
	,@LastID = LastID, @LastTime = LastTime, @STMTMax = STMTMax, @U = U, @P = P
  FROM dbo.DTSConfig
 WHERE TransName = @TransName;
IF(@@ROWCOUNT = 0)
BEGIN
	RETURN -1;
END

-- 检测数据(通过才发送)
IF(LEN(@Detect)>1)
BEGIN
	EXEC @rtn = sp_executesql @Detect;
	IF(@@ERROR <> 0 OR @rtn <> 1)
	BEGIN
		RETURN -2;
	END
END

-- 计算中间文件名,传送数据ID和时间范围
DECLARE @BcpFile nvarchar(4000), @strLastID nvarchar(50), @strMaxID nvarchar(50), @qLastTime nvarchar(21), @qMaxTime nvarchar(21);
SELECT @BcpFile = 'D:\' + @TransName + '[' + @strSPBeginTimeTrim + '].txt'
	,@qLastTime = '''' + CONVERT(nvarchar(19),@LastTime,120) + ''''
	,@strLastID = @LastID;
IF(LEN(@STMTMax) > 1)
BEGIN
	EXEC sp_executesql @STMTMax, N'@MaxID bigint out, @MaxTime datetime out'
		,@MaxID = @MaxID out, @MaxTime = @MaxTime out;

	SELECT @strMaxID = @MaxID, @qMaxTime = '''' + CONVERT(nvarchar(19),@MaxTime,120) + '''';
END
-- 防止替换结果为NULL
IF(@strLastID IS NULL) SELECT @strLastID = '';
IF(@strMaxID IS NULL) SELECT @strMaxID = '';
IF(@qLastTime IS NULL) SELECT @qLastTime = '';
IF(@qMaxTime IS NULL) SELECT @qMaxTime = '';

SELECT @sqlSTMT = REPLACE(REPLACE(REPLACE(REPLACE(@STMT,'^LastID$',@strLastID),'^MaxID$',@strMaxID),'^LastTime$',@qLastTime),'^MaxTime$',@qMaxTime);

-- BCP queryout & in
IF(@TransMethod = 1)
BEGIN
	-- BCP queryout
	SELECT @cmd = 'bcp "' + @sqlSTMT + '" queryout "' + @BcpFile + N'" -c -t~* -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- BCP in
	SELECT @cmd = 'bcp "' + @DBName + '.' + @SchemaName + '.' + @SPTName + '" in "' + @BcpFile + '" -c -t~* -r~*$ -S' + @SrvName + ' -U' + @U + ' -P' + @P;
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END
END

-- BCP out & in
IF(@TransMethod = 2)
BEGIN
	-- BCP out
	SELECT @cmd = 'bcp "' + @STMT + '" out "' + @BcpFile + N'" -c -t~* -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- BCP in
	SELECT @cmd = 'bcp "' + @DBName + '.' + @SchemaName + '.' + @SPTName + '" in "' + @BcpFile + '" -c -t~* -r~*$ -S' + @SrvName + ' -U' + @U + ' -P' + @P;
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END
END

-- 远程SP
IF(@TransMethod = 3)
BEGIN
	SELECT @sql = @SrvName + '.' + @DBName + '.' + @SchemaName + '.' + @SPTName + ' ' + @STMT;
	EXEC sp_executesql @sql, N'@LastID bigint, @LastTime datetime, @MaxID bigint, @MaxTime datetime'
		,@LastID = @LastID, @LastTime = @LastTime, @MaxID = @MaxID, @MaxTime = @MaxTime;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		RETURN -1;
	END
END

-- LinkServer
IF(@TransMethod = 4)
BEGIN
	SELECT @sql = 'INSERT ' + @SrvName + '.' + @DBName + '.' + @SchemaName + '.' + @SPTName + ' ' + @STMT;
	EXEC sp_executesql @sql, N'@LastID bigint, @LastTime datetime, @MaxID bigint, @MaxTime datetime'
		,@LastID = @LastID, @LastTime = @LastTime, @MaxID = @MaxID, @MaxTime = @MaxTime;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		RETURN -1;
	END
END

-- BCP queryout & FTP
IF(@TransMethod = 5)
BEGIN
	-- BCP queryout
	SELECT @cmd = 'bcp "' + @sqlSTMT + '" queryout "' + @BcpFile + N'" -c -t~* -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- 记录FTP开始时间
	UPDATE dbo.DTSConfig
	   SET TodayBeginTime = GETDATE()
	 WHERE TransName = @TransName;

	-- FTP
	SELECT @cmd = 'echo open ' + @SrvName + '>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo user ' + @U + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo ' + @P + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;
	--SELECT @cmd = 'echo cd DownLoad\QQHX\UserActor>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--EXEC master..xp_cmdshell @cmd;
	IF(Len(@SchemaName) > 1)
	BEGIN
		SELECT @cmd = 'echo cd ' + @SchemaName + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
		EXEC master..xp_cmdshell @cmd;
	END
	IF(Len(@SPTName) > 1)
	BEGIN
		DECLARE @ftpFileName1 nvarchar(256);
		EXEC sp_executesql @SPTName, N'@ftpFileName nvarchar(256) out', @ftpFileName = @ftpFileName1 out;
		SELECT @cmd = 'echo put "'+ @BcpFile +'" "' + @ftpFileName1 + '">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	ELSE
	BEGIN
		SELECT @cmd = 'echo put "'+ @BcpFile +'">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo bye>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;

	CREATE TABLE #t1(s nvarchar(4000));
	SELECT @cmd = 'ftp -i -n -s:D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--SELECT @cmd;
	INSERT #t1 EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		DROP TABLE #t1;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	SELECT * FROM #t1;
	IF(EXISTS(SELECT TOP 1 1 FROM #t1 WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%'))
	BEGIN
		DROP TABLE #t1;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	DROP TABLE #t1;
	-- 删除FTP命令文件
	SELECT @cmd = 'del "D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp" /q';
	EXEC master..xp_cmdshell @cmd;
END

-- BCP out & FTP
IF(@TransMethod = 6)
BEGIN
	-- BCP out
	SELECT @cmd = 'bcp "' + @sqlSTMT + '" queryout "' + @BcpFile + N'" -c -t~* -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- 记录FTP开始时间
	UPDATE dbo.DTSConfig
	   SET TodayBeginTime = GETDATE()
	 WHERE TransName = @TransName;

	-- FTP
	SELECT @cmd = 'echo open ' + @SrvName + '>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo user ' + @U + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo ' + @P + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;
	--SELECT @cmd = 'echo cd DownLoad\QQHX\UserActor>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--EXEC master..xp_cmdshell @cmd;
	IF(Len(@SchemaName) > 1)
	BEGIN
		SELECT @cmd = 'echo cd ' + @SchemaName + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
		EXEC master..xp_cmdshell @cmd;
	END
	IF(Len(@SPTName) > 1)
	BEGIN
		DECLARE @ftpFileName2 nvarchar(256);
		EXEC sp_executesql @SPTName, N'@ftpFileName nvarchar(256) out', @ftpFileName = @ftpFileName2 out;
		SELECT @cmd = 'echo put "'+ @BcpFile +'" "' + @ftpFileName2 + '">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	ELSE
	BEGIN
		SELECT @cmd = 'echo put "'+ @BcpFile +'">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo bye>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;

	CREATE TABLE #t2(s nvarchar(4000));
	SELECT @cmd = 'ftp -i -n -s:D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--SELECT @cmd;
	INSERT #t2 EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		DROP TABLE #t2;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	SELECT * FROM #t2;
	IF(EXISTS(SELECT TOP 1 1 FROM #t2 WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%'))
	BEGIN
		DROP TABLE #t2;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	DROP TABLE #t2;
	-- 删除FTP命令文件
	SELECT @cmd = 'del "D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp" /q';
	EXEC master..xp_cmdshell @cmd;
END

-- 记录已成功上传日期
UPDATE dbo.DTSConfig
   SET _Time = GETDATE(), LastTransTime = @SPBeginTime, LastID = @MaxID, LastTime = @MaxTime
 WHERE TransName = @TransName;
SELECT @Return = 1;

-- 删除中间文件
DelFile:
SELECT @cmd = 'del "' + @BcpFile + '" /q';
EXEC master..xp_cmdshell @cmd;

RETURN @Return;
GO
