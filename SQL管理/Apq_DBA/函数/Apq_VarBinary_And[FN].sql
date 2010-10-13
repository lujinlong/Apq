IF( OBJECT_ID('dbo.Apq_VarBinary_And', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_VarBinary_And()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-25
-- 描述: 二进制位与
-- 示例:
SELECT dbo.Apq_VarBinary_And(0x010101, 0xFF00FF11FF)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_VarBinary_And(
	@vb1	varbinary(max),
	@vb2	varbinary(max)
)RETURNS varbinary(max) AS
BEGIN
	DECLARE	@rtn varbinary(max)
		,@l1 int, @l2 int
		,@lmin int, @lmax int, @i int
		,@t1 tinyint, @b2 binary(1)
		;
	SELECT @rtn = 0x, @l1 = DataLength(@vb1), @l2 = DataLength(@vb2);
	SELECT @i = 1, @lmin = CASE WHEN @l1 < @l2 THEN @l1 ELSE @l2 END, @lmax = CASE WHEN @l1 > @l2 THEN @l1 ELSE @l2 END;

	WHILE(@i <= @lmin)
	BEGIN
		SELECT @t1 = SubString(@vb1, @i, 1);
		SELECT @b2 = SubString(@vb2, @i, 1);

		SELECT @rtn = @rtn + Convert(binary(1),(@t1 & @b2));

		SELECT @i = @i + 1;
	END

	WHILE(@i <= @lmax)
	BEGIN
		SELECT @rtn = @rtn + 0x00;

		SELECT @i = @i + 1;
	END
	
	RETURN @rtn;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_VarBinary_And', DEFAULT
