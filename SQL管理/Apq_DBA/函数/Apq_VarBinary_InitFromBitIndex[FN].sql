IF( OBJECT_ID('dbo.Apq_VarBinary_InitFromBitIndex', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_VarBinary_InitFromBitIndex()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-25
-- 描述: 以指定索引位为1来初始化二进制串
-- 示例:
SELECT dbo.Apq_VarBinary_InitFromBitIndex(159)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_VarBinary_InitFromBitIndex(
	@idx	int
)RETURNS varbinary(max) AS
BEGIN
	DECLARE @rtn varbinary(max), @i tinyint
		,@q int, @r int;
	SELECT @rtn = 0x, @q = @idx / 8, @r = @idx % 8;
	IF(@q > 0 AND @r = 0)
	BEGIN
		SELECT @q = @q - 1;
	END

	SELECT @i = 0;
	WHILE(@i < @q)
	BEGIN
		SELECT @rtn = @rtn + 0x00;

		SELECT @i = @i + 1;
	END

	SELECT @rtn = @rtn + CASE @r WHEN 0 THEN 0x01 ELSE Convert(binary(1), Power(2,8-@r)) END;
	
	RETURN @rtn;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_VarBinary_InitFromBitIndex', DEFAULT
