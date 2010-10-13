IF( OBJECT_ID('dbo.Apq_BuildSPTables', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_BuildSPTables AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-05-10
-- 描述: 创建存储过程参数列表(SQL Server 2008)
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(4000);
EXEC @rtn = dbo.Apq_BuildSPTables @ExMsg out, 'Apq', 'Apq_XSD';
SELECT @rtn, @ExMsg;
-- =============================================
*/
ALTER PROC dbo.Apq_BuildSPTables
	@ExMsg nvarchar(4000) out
	
	,@From	nvarchar(512)
	,@To	nvarchar(512)
AS
SET NOCOUNT ON;

-- 读取存储过程名及参数 ----------------------------------------------------------------------------
DECLARE @sql nvarchar(4000);
DECLARE @t_helpsp TABLE(
	object_id	int,
	SchemaName	sysname,
	SPName		sysname,
	ParamName	sysname,
	ParamType	sysname NULL,
	max_length	smallint,
	Precision	int,
	Scale		tinyint,
	parameter_id	int,
	Collation	nvarchar(256),
	IsOut		tinyint
);

SELECT @sql = '
SELECT obj.object_id, SCHEMA_NAME(obj.schema_id), obj.name, p.name, type_name(system_type_id), max_length
	,CASE WHEN type_name(system_type_id) = ''uniqueidentifier'' THEN precision ELSE OdbcPrec(system_type_id, max_length, precision) END
	,OdbcScale(system_type_id, scale), parameter_id
	,convert(sysname, case when system_type_id in (35, 99, 167, 175, 231, 239) then ServerProperty(''collation'') end)
	,is_output
  FROM ' + @From + '.sys.objects obj INNER JOIN ' + @From + '.sys.parameters p ON obj.object_id = p.object_id
 WHERE obj.type = ''P''
 ORDER BY obj.object_id, p.parameter_id;
 ';

INSERT @t_helpsp
EXEC sp_executesql @sql;

SELECT * FROM @t_helpsp;
-- =================================================================================================

DECLARE @csrT CURSOR, @csrC CURSOR, @object_id int, @SchemaName nvarchar(512), @TName nvarchar(512)
	,@CName nvarchar(512), @CType nvarchar(512), @max_length smallint, @CPrecision nvarchar(512)
	,@CScale nvarchar(512), @PIsOut tinyint
	,@sqlS nvarchar(4000), @sqlT nvarchar(4000), @sqlC nvarchar(4000)
	,@sqlP nvarchar(4000);
SET @csrT = CURSOR STATIC FOR
SELECT DISTINCT object_id, SchemaName, SPName FROM @t_helpsp;

OPEN @csrT;
FETCH NEXT FROM @csrT INTO @object_id, @SchemaName, @TName;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @sqlC = '';
	SET @csrC = CURSOR STATIC FOR
	SELECT ParamName, ParamType, max_length, Precision, Scale, IsOut FROM @t_helpsp WHERE object_id = @object_id;
	
	OPEN @csrC;
	FETCH NEXT FROM @csrC INTO @CName, @CType, @max_length, @CPrecision, @CScale, @PIsOut;
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF(@CType = 'table type')	-- 暂跳过表值参数
		BEGIN
			GOTO NEXT_Col;
		END
		
		SELECT @sqlC = @sqlC+',[' + SubString(@CName, 2, Len(@CName)-1) + CASE @PIsOut WHEN 1 THEN '_ooo' ELSE '' END + '] [' + @CType + ']';
		IF(@CType IN ('numeric', 'decimal', 'float', 'char', 'varchar', 'nchar', 'nvarchar', 'binary', 'varbinary'))
		BEGIN
			SELECT @sqlC = @sqlC+'(' + CASE @max_length WHEN -1 THEN 'max' ELSE Convert(nvarchar,@CPrecision) END;
			IF(@CType IN ('numeric', 'decimal')) SELECT @sqlC = @sqlC+',' + Convert(nvarchar,@CScale);
			SELECT @sqlC = @sqlC+')';
		END
	
		NEXT_Col:
		FETCH NEXT FROM @csrC INTO @CName, @CType, @max_length, @CPrecision, @CScale, @PIsOut;
	END
	CLOSE @csrC;

	IF(LEN(@sqlC)>1)	-- 只生成具有参数的存储过程表
	BEGIN
		SELECT @sqlS = 'CREATE SCHEMA [' + @SchemaName + ']''';
		SELECT @sqlT = '
		CREATE TABLE [' + @SchemaName + '].[' + @TName + '](
			' + SubString(@sqlC, 2, Len(@sqlC)-1) + '
		)';
		
		SELECT @sqlP = 'IF(SCHEMA_ID(''' + @SchemaName + ''') IS NULL) EXEC sp_executesql @sqlS;';
		SELECT @sql = 'EXEC [' + @To + '].sys.sp_executesql @sqlP, N''@sqlS nvarchar(4000)'', @sqlS = @sqlS';
		--SELECT @sql,@sqlS;
		EXEC sp_executesql @sql, N'@sqlP nvarchar(4000), @sqlS nvarchar(4000)', @sqlP = @sqlP, @sqlS = @sqlS;
		
		SELECT @sqlP = '
IF(OBJECT_ID(''' + @SchemaName + '.' + @TName + ''') IS NOT NULL) DROP TABLE [' + @SchemaName + '].[' + @TName + '];
EXEC sp_executesql @sqlT';
		SELECT @sql = 'EXEC [' + @To + '].sys.sp_executesql @sqlP, N''@sqlT nvarchar(4000)'', @sqlT = @sqlT';
		--SELECT @sql,@sqlT;
		EXEC sp_executesql @sql, N'@sqlP nvarchar(4000), @sqlT nvarchar(4000)', @sqlP = @sqlP, @sqlT = @sqlT;
	END

	NEXT_TABLE:
	FETCH NEXT FROM @csrT INTO @object_id, @SchemaName, @TName;
END
CLOSE @csrT;

SELECT @ExMsg = '操作成功';
RETURN 1;
GO
