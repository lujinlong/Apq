IF( OBJECT_ID('etl.Job_Etl_BcpIn_Init', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE etl.Job_Etl_BcpIn_Init AS BEGIN RETURN END';
GO
ALTER PROC etl.Job_Etl_BcpIn_Init
	@CfgRowCount	int = 10000	-- 从配置中最多读取的行数
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

DECLARE @rtn int, @SPBeginTime datetime
	,@yyyy int, @mm int, @dd int, @hh int, @mi int
	,@ww int
	,@yyyyStr nvarchar(50), @mmStr nvarchar(50), @ddStr nvarchar(50), @hhStr nvarchar(50), @miStr nvarchar(50)
	,@wwStr nvarchar(50)
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	;

SELECT @SPBeginTime = getdate();

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
	
	,@strPeriod nvarchar(50)
	,@LastFileName nvarchar(512)
	
	,@BTime datetime, @ETime datetime, @CTime datetime
	--,@BcpInFullTableName nvarchar(512)-- BcpIn到的完整表名(数据库名.架构名.表名)
	,@FileName_tmp nvarchar(512)
	,@idxTimeB int,@idxTimeE int, @FilePeriod datetime, @FileNo int
	,@strFilePeriod nvarchar(50)
	;

DECLARE @csr CURSOR, @csrFile CURSOR;
SET @csr = CURSOR STATIC FOR
SELECT TOP(@CfgRowCount) ID, EtlName, Folder, PeriodType, FileName, DBName, SchemaName, TName, r, t
  FROM etl.EtlCfg
 WHERE Enabled = 1

DECLARE @DataPeriod datetime;
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #FileExist(
	FileExist tinyint,
	IsFolder tinyint,
	ParentExist tinyint
);

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@PeriodType,@FileName,@DBName,@SchemaName,@TName,@r,@t;
WHILE(@@FETCH_STATUS=0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	SELECT @strPeriod = '',@LastFileName = @FileName;
	
	-- 独立完整文件:直接将配置的文件名入队
	IF(@PeriodType = 0)
	BEGIN
		TRUNCATE TABLE #FileExist;
		SELECT @FileName_tmp = @Folder + @FileName + '.txt';
		INSERT #FileExist
		EXEC sys.xp_fileexist @FileName_tmp;
		
		IF(EXISTS(SELECT TOP(1) 1 FROM #FileExist WHERE FileExist = 1)
			AND NOT EXISTS(SELECT TOP(1) 1 FROM etl.BcpInQueue WHERE Enabled = 1 AND EtlName = @EtlName AND IsFinished = 0)
		)
		BEGIN
			INSERT etl.BcpInQueue ( EtlName, Folder, FileName, DBName, SchemaName, TName, t, r )
			VALUES(@EtlName,@Folder,@FileName+'.txt',@DBName,@SchemaName,@TName,@t,@r);
		END
		
		GOTO NEXT_Etl;
	END
	
	-- 1.找出该数据的所有源文件
	TRUNCATE TABLE #t;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @Folder + @FileName + '[*.txt"';
	--SELECT @cmd;
	INSERT #t(s) EXEC master..xp_cmdshell @cmd;
	
	-- 2.计算待导入文件的时期和编号
	SET @csrFile = CURSOR STATIC FOR
	SELECT s FROM #t WHERE Left(s,Len(@FileName)+1) = @FileName+'[';
	
	OPEN @csrFile;
	FETCH NEXT FROM @csrFile INTO @FileName_tmp;
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		SELECT @idxTimeB = 0, @idxTimeE = 0;
		SELECT @idxTimeB = charindex('[',@FileName_tmp);
		IF(@idxTimeB > 1)
		BEGIN
			SELECT @idxTimeE = charindex(']',@FileName_tmp);
		END
		IF(@idxTimeE > 2 AND @idxTimeE > @idxTimeB)
		BEGIN
			SELECT @strFilePeriod = substring(@FileName_tmp,@idxTimeB+1,@idxTimeE-@idxTimeB-1);
			IF(@PeriodType = 1) SELECT @FilePeriod = @strFilePeriod + '0101';
			IF(@PeriodType IN (2,3,4)) SELECT @FilePeriod = @strFilePeriod + '01';
			IF(@PeriodType = 5)
			BEGIN
				SELECT @FilePeriod = Left(@strFilePeriod,4) + '0101';
				SELECT @FilePeriod = dateadd(ww,Convert(int,Right(@strFilePeriod,Len(@strFilePeriod)-5)),@FilePeriod);
			END
			IF(@PeriodType = 6) SELECT @FilePeriod = @strFilePeriod;
			IF(@PeriodType = 7)
			BEGIN
				SELECT @FilePeriod = Left(@strFilePeriod,8);
				SELECT @FilePeriod = dateadd(hh,Convert(int,Right(@strFilePeriod,Len(@strFilePeriod)-9)),@FilePeriod);
			END
			IF(@PeriodType = 8)
			BEGIN
				SELECT @FilePeriod = Left(@strFilePeriod,8);
				SELECT @FilePeriod = dateadd(hh,Convert(int,substring(@strFilePeriod,9,2)),@FilePeriod);
				SELECT @FilePeriod = dateadd(mi,Convert(int,Right(@strFilePeriod,2)),@FilePeriod);
			END
			
			SELECT @idxTimeB = charindex('[',@FileName_tmp,@idxTimeE);
			SELECT @idxTimeE = charindex(']',@FileName_tmp,@idxTimeB);
			SELECT @FileNo = substring(@FileName_tmp,@idxTimeB+1,@idxTimeE-@idxTimeB-1);
			
			IF(NOT EXISTS(SELECT TOP 1 1 FROM log.BcpInInit WHERE EtlName = @EtlName AND FileNo = @FileNo AND strPeriod = @strFilePeriod))
			BEGIN
				INSERT etl.BcpInQueue ( EtlName, Folder, FileName, DBName, SchemaName, TName, t, r, FileNo,Period )
				VALUES(@EtlName,@Folder,@FileName_tmp,@DBName,@SchemaName,@TName,@t,@r,@FileNo,@strFilePeriod);
				
				INSERT log.BcpInInit (EtlName,Period,FileNo,strPeriod )
				VALUES(@EtlName,@FilePeriod,@FileNo,@strFilePeriod);
			END
		END
		
		NEXT_File:
		FETCH NEXT FROM @csrFile INTO @FileName_tmp;
	END

	NEXT_Etl:
	FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@PeriodType,@FileName,@DBName,@SchemaName,@TName,@r,@t;
END
CLOSE @csr;

Quit:
DROP TABLE #t;

RETURN 1;
GO
