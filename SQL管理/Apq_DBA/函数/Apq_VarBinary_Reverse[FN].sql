IF( OBJECT_ID('dbo.Apq_VarBinary_Reverse', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_VarBinary_Reverse()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-12-19
-- 描述: 逆转二进制串
-- 示例:
SELECT dbo.Apq_VarBinary_Reverse(0x00000000FFFFFFFF)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_VarBinary_Reverse(
	@Input	varbinary(max)
)RETURNS varbinary(max)AS
BEGIN
	DECLARE	@Byte binary(1), @rtn varbinary(max), @i int;
	SELECT	@i = 0, @rtn = 0x;
	WHILE(@i < DATALENGTH(@Input))
	BEGIN
		SELECT	@i = @i + 1;
		SELECT	@Byte = Substring(@Input, @i, 1);
		SELECT	@rtn = @Byte + @rtn;
	END
	RETURN @rtn;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_VarBinary_Reverse', DEFAULT
