IF( OBJECT_ID('etl.Job_Etl_Load', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Job_Etl_Load AS BEGIN RETURN END';
GO
ALTER PROC etl.Job_Etl_Load
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 加载到正式表
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_Load;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@EtlName nvarchar(256),
	@SrcFullTableName nvarchar(256),
	@DstFullTableName nvarchar(256),
	@Cycle int,			-- 切换周期(分钟)
	@LTime smalldatetime,-- 切换时间
	@PreLTime datetime	-- 上一次切换时间
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID, EtlName, SrcFullTableName, DstFullTableName, Cycle, LTime, PreLTime
  FROM etl.LoadCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@SrcFullTableName,@DstFullTableName,@Cycle,@LTime,@PreLTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	SELECT @PreLTime = ISNULL(@PreLTime,dateadd(n,4-@Cycle,@LTime))
	SELECT @TodaySTime = Convert(nvarchar(11),@Today,120) + Left(Convert(nvarchar(50),@LTime,108),6)+'00';

	IF(DATEDIFF(n, @PreLTime, @JobTime) >= @Cycle - 1	-- 执行周期已到
		--AND datediff(n,@JobTime,@TodaySTime) BETWEEN 0 AND 5	-- 今天执行时间已到
	)BEGIN
		-- 切换表
		EXEC @rtn = etl.Etl_Load @EtlName;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			UPDATE etl.LoadCfg SET _Time = getdate(), PreLTime = @JobTime WHERE ID = @ID;
		END
	END

	NEXT_Load:
	FETCH NEXT FROM @csr INTO @ID,@EtlName,@SrcFullTableName,@DstFullTableName,@Cycle,@LTime,@PreLTime;
END
CLOSE @csr;

RETURN 1;
GO
