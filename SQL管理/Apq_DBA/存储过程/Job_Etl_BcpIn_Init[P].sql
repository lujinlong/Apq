IF( OBJECT_ID('etl.Job_Etl_BcpIn_Init', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Job_Etl_BcpIn_Init AS BEGIN RETURN END';
GO
ALTER PROC etl.Job_Etl_BcpIn_Init
	@CfgRowCount	int = 10000	-- 从配置中最多读取的行数
	,@BcpPeriod		datetime = NULL
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-27
-- 描述: 根据ETL配置检查已有文件,并将需要BcpIn的文件插入BcpIn队列
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn_Init;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @CfgRowCount = ISNULL(@CfgRowCount,10000);
SELECT @BcpPeriod = ISNULL(@BcpPeriod,getdate());

DECLARE @rtn int, @SPBeginTime datetime, @BCPDay datetime
	,@yyyy int, @mm int, @dd int, @hh int, @mi int
	,@ww int
	,@yyyyStr nvarchar(50), @mmStr nvarchar(50), @ddStr nvarchar(50), @hhStr nvarchar(50), @miStr nvarchar(50)
	,@wwStr nvarchar(50)
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	;

SELECT @SPBeginTime = getdate();
SELECT @BCPDay = dateadd(dd,0,datediff(dd,0,@BcpPeriod));	-- 数据传送时间(一般当天传送上一周期的全部数据)

DECLARE @ID bigint,
	@EtlName nvarchar(256),	-- 配置名
	@Folder nvarchar(512),	-- 本地文件目录(不含时期)
	@PeriodType int,		-- 时期类型{1:年,2:半年,3:季度,4:月,5:周,6:日,7:时,8:分}
	@FileName nvarchar(256),-- 文件名(前缀)(格式:FileName_SrvID.txt)
	@DBName nvarchar(256),
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@r nvarchar(10),
	@t nvarchar(10)
	
	,@BTime datetime, @ETime datetime, @CTime datetime
	,@FullFolder nvarchar(512)-- 目录(含时期)
	--,@BcpInFullTableName nvarchar(512)-- BcpIn到的完整表名(数据库名.架构名.表名)
	;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@CfgRowCount) ID, EtlName, Folder, PeriodType, FileName, DBName, SchemaName, TName, r, t
  FROM etl.EtlCfg
 WHERE Enabled = 1

DECLARE @DataPeriod datetime;
CREATE TABLE #t(s nvarchar(4000));

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@PeriodType,@FileName,@DBName,@SchemaName,@TName,@r,@t;
WHILE(@@FETCH_STATUS=0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	SELECT @FullFolder = @Folder;
	
	-- 从当前时间计算应该获取的数据时间(往前推一周期,最小为日) -------------------------------------
	SELECT @DataPeriod = dateadd(dd,-1,@BCPDay); -- 默认取前1天
	IF(@PeriodType = 1) SELECT @DataPeriod = dateadd(yyyy,-1,@BCPDay);
	IF(@PeriodType = 2) SELECT @DataPeriod = dateadd(mm,-6,@BCPDay);
	IF(@PeriodType = 3) SELECT @DataPeriod = dateadd(mm,-3,@BCPDay);
	IF(@PeriodType = 4) SELECT @DataPeriod = dateadd(mm,-1,@BCPDay);
	-- =============================================================================================
	
	-- 循环检查数据目录 ----------------------------------------------------------------------------
	SELECT @BTime = @DataPeriod
	SELECT @ETime = @BCPDay
	SELECT @CTime = @BTime;
	
	WHILE(@CTime < @ETime)
	BEGIN
		IF(@PeriodType > 0)
		BEGIN
			SELECT @yyyy = datepart(yyyy,@CTime)
				,@mm = datepart(mm,@CTime)
				,@dd = datepart(dd,@CTime)
				,@hh = datepart(hh,@CTime)
				,@mi = datepart(n,@CTime)
				;
				
			SELECT @yyyyStr = Convert(nvarchar(50),@yyyy)
				,@mmStr = CASE WHEN @mm < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@mm)
				,@ddStr = CASE WHEN @dd < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@dd)
				,@hhStr = CASE WHEN @hh < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@hh)
				,@miStr = CASE WHEN @mi < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@mi)
				;
				
			IF(@PeriodType = 1) SELECT @FullFolder = @Folder + @yyyyStr;
			IF(@PeriodType IN (2,3,4)) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr;
			IF(@PeriodType = 5) SELECT @FullFolder = @Folder + @yyyyStr + '_' + @wwStr;
			IF(@PeriodType = 6) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr + @ddStr;
			IF(@PeriodType = 7) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr + @ddStr + '_' + @hhStr;
			IF(@PeriodType = 8) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr + @ddStr + '_' + @hhStr + @miStr;
		END
		
		IF(RIGHT(@FullFolder,1)<>'\') SELECT @FullFolder = @FullFolder+'\';
		
		-- 入队 --------------------------------------------------------------------------------
		TRUNCATE TABLE #t;
		SELECT @cmd = 'dir /a:-d/b/o:n "' + @FullFolder + @FileName + '*.txt"';
		--SELECT @cmd;
		INSERT #t(s) EXEC master..xp_cmdshell @cmd;
		
		INSERT etl.BcpInQueue ( EtlName, Folder, FileName, DBName, SchemaName, TName, t, r )
		SELECT @EtlName,@FullFolder,s,@DBName,@SchemaName,@TName,@t,@r
		  FROM #t
		 WHERE (@PeriodType = 0 AND Left(s,Len(@FileName)+1) = @FileName+'.')
			OR (@PeriodType > 0 AND Left(s,Len(@FileName)+1) = @FileName+'[' AND NOT EXISTS(SELECT TOP 1 * FROM etl.BcpInQueue t WHERE EtlName = @EtlName AND Folder = @FullFolder AND Left(s,Len(FileName)) = FileName))
		-- =====================================================================================
		
		IF(@PeriodType = 0) SELECT @CTime = @ETime;
		IF(@PeriodType = 1) SELECT @CTime = dateadd(yyyy,1,@CTime);
		IF(@PeriodType = 2) SELECT @CTime = dateadd(yyyy,6,@CTime);
		IF(@PeriodType = 3) SELECT @CTime = dateadd(mm,3,@CTime);
		IF(@PeriodType = 4) SELECT @CTime = dateadd(mm,1,@CTime);
		IF(@PeriodType = 5) SELECT @CTime = dateadd(dd,7,@CTime);
		IF(@PeriodType = 6) SELECT @CTime = dateadd(dd,1,@CTime);
		IF(@PeriodType = 7) SELECT @CTime = dateadd(hh,1,@CTime);
		IF(@PeriodType = 8) SELECT @CTime = dateadd(n,1,@CTime);
	END
	-- ================================================================================================

	FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@PeriodType,@FileName,@DBName,@SchemaName,@TName,@r,@t;
END
CLOSE @csr;

Quit:
DROP TABLE #t;

RETURN 1;
GO
