IF( OBJECT_ID('etl.Etl_SwitchBcpTable', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Etl_SwitchBcpTable AS BEGIN RETURN END';
GO
ALTER PROC etl.Etl_SwitchBcpTable
	@EtlName	nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 切换BCP接收表
-- 功能: 按预定时间切换BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Etl_SwitchBcpTable 'ImeiLog';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@STName nvarchar(256),
	@Cycle int,			-- 切换周期(分钟)
	@STime smalldatetime,-- 切换时间
	@PreSTime datetime	-- 上一次切换时间
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

SELECT @ID=ID,@DBName=DBName,@SchemaName=SchemaName,@TName=TName,@STName=STName
  FROM etl.BcpSTableCfg
 WHERE EtlName = @EtlName;
IF(@@ROWCOUNT > 0)
BEGIN
	-- 切换表
	SELECT @sqlDB = 'EXEC sp_rename ''' + @SchemaName + '.' + @STName + ''', ''' + @STName + '_SwithTmp'';
EXEC sp_rename ''' + @SchemaName + '.' + @TName + ''', ''' + @STName + ''';
EXEC sp_rename ''' + @SchemaName + '.' + @STName + '_SwithTmp'', ''' + @TName + ''';
';
	SELECT @sql = 'EXEC [' + @DBName + ']..sp_executesql @sqlDB';
	EXEC sp_executesql @sql, N'@sqlDB nvarchar(4000)', @sqlDB = @sqlDB;
	UPDATE etl.BcpSTableCfg SET _Time = getdate() WHERE ID = @ID;
END

RETURN 1;
GO
