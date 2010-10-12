IF( OBJECT_ID('dbo.Job_Pick_JobHis', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_Pick_JobHis AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_Pick_JobHis
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 作业日志本地收集
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Pick_JobHis
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @LocalSrvID nvarchar(32),
	@CfgName nvarchar(256),		-- 配置名(唯一)
	@PickLastID bigint,
	@PickLastTime datetime,
	@HasContent tinyint,
	@FileFolder nvarchar(3000),	-- 文件夹
	@FileName nvarchar(256),	-- 文件名
	@FileEX nvarchar(50),		-- 扩展名(.txt)
	@SrvID int,					-- 服务器编号
	@DBName nvarchar(256),		-- 数据库名
	@t nvarchar(50),
	@r nvarchar(50),
	
	@LSName nvarchar(512),		-- 目标服务器别名
	@PickMaxID bigint,
	@PickMaxTime datetime,
	
	@rtn int, @cmd nvarchar(4000), @sql nvarchar(max)
	,@SPBeginTime datetime		-- 存储过程启动时间
	;
SELECT @SPBeginTime=GetDate();
SELECT @CfgName = 'JobHis'	-- 指定配置名
	,@FileFolder = 'D:\'
	,@FileEX = '.txt'
	,@t = '~*'	-- 列分隔符 推荐使用"~*"
	,@r = '~*$'	-- 行分隔符 推荐使用"~*$"
SELECT @LocalSrvID = dbo.Apq_Ext_Get('',0,'LocalSrvID');
SELECT @FileName = @CfgName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+']';
SELECT @HasContent = 0;

IF(Len(@FileFolder)>3)
BEGIN
	IF(RIGHT(@FileFolder,1)<>'\') SELECT @FileFolder = @FileFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FileFolder, 1, LEN(@FileFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 取出上次收集的最后ID和最后时间
SELECT @PickLastID = PickLastID, @PickLastTime = PickLastTime
  FROM dbo.DTS_Send
 WHERE CfgName = @CfgName;

-- 计算本次收集的最后ID或最后时间
SELECT @PickMaxID = ISNULL(max(instance_id),-1), @PickMaxTime = ISNULL(max(Convert(datetime,(convert(nvarchar(20), run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time)))),getdate())
  FROM msdb.dbo.sysjobhistory

-- 有数据时导出
IF(EXISTS(SELECT TOP 1 1 FROM msdb.dbo.sysjobhistory WHERE run_status = 0
	AND ((instance_id > @PickLastID AND instance_id <= @PickMaxID)
	OR (Convert(datetime,(convert(nvarchar(20), run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time))) > @PickLastTime
	AND Convert(datetime,(convert(nvarchar(20), run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time))) <= @PickMaxTime))
	)
)
BEGIN
	SELECT @HasContent = 1;
	SELECT @sql = 'EXEC Apq_DBA.dbo.Pick_JobHis ' + dbo.Apq_CreateSqlON(@PickLastID);
	SELECT @sql = @sql + ',' + dbo.Apq_CreateSqlON(@PickMaxID);
	SELECT @sql = @sql + ',' + dbo.Apq_CreateSqlON(@PickLastTime);
	SELECT @sql = @sql + ',' + dbo.Apq_CreateSqlON(@PickMaxTime);
	-- BCP queryout
	SELECT @cmd = 'bcp "' + @sql + '" queryout "' + @FileFolder + @FileName + @FileEX
			+ '" -c -t' + CASE WHEN LEN(@t) > 0 THEN @t ELSE '\t' END
			+ ' -r' + CASE WHEN LEN(@r) > 0 THEN @r ELSE '\n' END
			+ ' -T'
	--Print @sql; Print @cmd; RETURN;
	EXEC @rtn = master..xp_cmdshell @cmd;
END

-- 更新上次收集的最后ID和最后时间
UPDATE dbo.DTS_Send
   SET _Time = GETDATE(), PickLastID = @PickMaxID, PickLastTime = @PickMaxTime
 WHERE CfgName = @CfgName;

-- 记录收集日志
INSERT dbo.Log_DTS_LocalPick ( CfgName,PickTime,HasContent,FileFolder,FileName,FileEX,t,r )
VALUES  (@CfgName,@SPBeginTime,@HasContent,@FileFolder,@FileName,@FileEX,@t,@r)

RETURN 1;
GO
