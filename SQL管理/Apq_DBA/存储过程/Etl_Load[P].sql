IF( OBJECT_ID('etl.Etl_Load', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Etl_Load AS BEGIN RETURN END';
GO
ALTER PROC etl.Etl_Load
	@EtlName	nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 加载到正式表
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Etl_Load 'ImeiLog';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@SrcFullTableName nvarchar(256),
	@DstFullTableName nvarchar(256)
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;

SELECT @ID = ID,@SrcFullTableName=SrcFullTableName,@DstFullTableName=DstFullTableName
  FROM etl.LoadCfg
 WHERE EtlName = @EtlName;
IF(@@ROWCOUNT > 0)
BEGIN
	-- 加载表
	SELECT @sql = '
INSERT ' + @DstFullTableName + '
SELECT *
FROM ' + @SrcFullTableName + ';

TRUNCATE TABLE ' + @SrcFullTableName + ';
';
	EXEC sp_executesql @sql;
	
	UPDATE etl.LoadCfg SET _Time = getdate() WHERE ID = @ID;
END

RETURN 1;
GO
