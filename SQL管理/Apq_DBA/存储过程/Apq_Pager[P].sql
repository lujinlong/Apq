IF( OBJECT_ID('dbo.Apq_Pager', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Pager AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-14
-- 描述: 分页
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.Apq_Pager @ExMsg out, 'ID, Name, Crt, Limit, Init, Inc, State, _Time, _InTime'
	, 'dbo.Apq_ID'
	, 'ID','_Time','', 20, 0;
SELECT @rtn, @ExMsg;
-- =============================================
*/
ALTER PROC dbo.Apq_Pager
	@ExMsg nvarchar(max) out,
	@sCols		nvarchar(max),		-- 输出列(不支持*)
    @fTable		nvarchar(max),		-- 表/子查询(子查询时必须带括号传入且里面用到的表须加NOLOCK)
    @pKeys		nvarchar(max),		-- 主键(NOT EXISTS里使用)
    @oCols		nvarchar(max),		-- 排序列(分隔符[,])
	@oBy		nvarchar(max) = '',	-- 排序方式(分隔符[,],与列对应)
    @pSize		int = 20,			-- 每页行数
    @pNumber	int = 0				-- 页码(从0开始)
AS
SET NOCOUNT ON;

IF(@pKeys IS NULL OR Len(@pKeys) < 1) SELECT @pKeys = @sCols;

DECLARE @rtn int, @sql nvarchar(max), @sqlTable1 nvarchar(max), @sqlTable2 nvarchar(max), @sqlNE nvarchar(max), @sqlIdx nvarchar(max), @sqlOrder nvarchar(max)
	, @PagerNo nvarchar(21),@Apq_Pager1Name nvarchar(max);

-- [Build]ORDER BY 后的片段
IF(LEN(@oBy) > 1)
BEGIN
	SELECT @sqlOrder = '';
	DECLARE @p1 int, @p2 int, @q1 int, @q2 int;
	SELECT @p1 = 1, @p2 = LEN(@oCols), @q1 = 1, @q2 = LEN(@oBy);
	WHILE(@p2 > 0)
	BEGIN
		SELECT @p2 = CHARINDEX(',', @oCols, @p1);
		IF(@p2 = 0)
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ',' + SubString(@oCols, @p1, LEN(@oCols)+1-@p1);
		END
		ELSE
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ',' + SubString(@oCols, @p1, @p2-@p1);
		END

		SELECT @q2 = CHARINDEX(',', @oBy, @q1);
		IF(@q2 = 0)
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ' ' + SubString(@oBy, @q1, LEN(@oBy)+1-@q1);
		END
		ELSE
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ' ' + SubString(@oBy, @q1, @q2-@q1);
		END

		SELECT @p1 = @p2 + 1;
		SELECT @q1 = @q2 + 1;
	END
	SELECT @sqlOrder = RIGHT(@sqlOrder,Len(@sqlOrder)-1);
END
ELSE
BEGIN
	SELECT @sqlOrder = @oCols + ' ' + @oBy;
END
--PRINT @sqlOrder;RETURN;

-- [Build]NOT EXISTS 里的关联条件,同时算出创建索引的语句
SELECT @sqlNE = '', @sqlIdx = '';
SELECT @p1 = 1, @p2 = LEN(@pKeys);
WHILE(@p2 > 0)
BEGIN
	SELECT @p2 = CHARINDEX(',', @pKeys, @p1);
	IF(@p2 = 0)
	BEGIN
		SELECT @sqlNE = @sqlNE + ' AND Apq_Pager1.' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1) + ' = Apq_Pager0.' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1);
		SELECT @sqlIdx = @sqlIdx + '
CREATE INDEX [IX_^Apq_Pager1$:' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1) + '] ON ^Apq_Pager1$(' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1) + ');'
	END
	ELSE
	BEGIN
		SELECT @sqlNE = @sqlNE + ' AND Apq_Pager1.' + SubString(@pKeys, @p1, @p2-@p1) + ' = Apq_Pager0.' + SubString(@pKeys, @p1, @p2-@p1);
		SELECT @sqlIdx = @sqlIdx + '
CREATE INDEX [IX_^Apq_Pager1$:' + SubString(@pKeys, @p1, @p2-@p1) + '] ON ^Apq_Pager1$(' + SubString(@pKeys, @p1, @p2-@p1) + ');'
	END

	SELECT @p1 = @p2 + 1;
END
SELECT @sqlNE = '(' + RIGHT(@sqlNE,Len(@sqlNE)-5) + ')';
--PRINT @sqlNE;RETURN;

EXEC @rtn = dbo.Apq_Identifier @ExMsg OUT, @Name = 'Apq_Pager', @Count = 1, @Next = @PagerNo OUT
IF(@@ERROR <> 0 OR @rtn <> 1)
BEGIN
	RETURN -1;
END
SELECT @Apq_Pager1Name = 'dbo.Apq_Pager1_' + @PagerNo;

-- 取前(@pNumber)页的主键并建索引
SELECT @sqlTable1 = '
SELECT TOP ^TOP$ ^pKeys$ INTO ^Apq_Pager1$ FROM ^fTable$ Apq_Pager0 ORDER BY ^sqlOrder$;'
	+ @sqlIdx;
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^TOP$', @pNumber * @pSize);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^pKeys$', @pKeys);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^fTable$', @fTable);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^sqlOrder$', @sqlOrder);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^Apq_Pager1$', @Apq_Pager1Name);
--PRINT @sqlTable1;RETURN;
EXEC sp_executesql @sqlTable1;
SELECT @sqlTable1 = @Apq_Pager1Name;

-- 输出
SELECT @sqlTable2 = '
SELECT TOP ^pSize$ ^sCols$ FROM ^fTable$ Apq_Pager0
 WHERE NOT EXISTS(SELECT TOP 1 1 FROM ^sqlTable1$ Apq_Pager1 WHERE ^sqlNE$)
 ORDER BY ^sqlOrder$;';
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^fTable$', @fTable);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sqlTable1$', @sqlTable1);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^pSize$', @pSize);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sCols$', @sCols);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sqlNE$', @sqlNE);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sqlOrder$', @sqlOrder);
--PRINT @sqlTable2; RETURN;
EXEC sp_executesql @sqlTable2;

-- 删除分页表
SELECT @sql ='
TRUNCATE TABLE ^sqlTable1$;
DROP TABLE ^sqlTable1$;';
SELECT @sql = REPLACE(@sql,'^sqlTable1$', @sqlTable1);
--PRINT @sql; RETURN;
EXEC sp_executesql @sql;

RETURN 1;
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_Pager', DEFAULT
GO
