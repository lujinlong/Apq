IF( OBJECT_ID('dbo.Apq_VarBinary_InsertAt', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_VarBinary_InsertAt()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-16
-- 描述: 在二进制串中插入二进制
-- 示例:
SELECT dbo.Apq_VarBinary_InsertAt(0x00000000FFFFFFFF, 2, 0x11);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_VarBinary_InsertAt(
	@vb1	varbinary(max),
	@index	int,
	@vb2	varbinary(max)
)RETURNS varbinary(max)AS
BEGIN
	IF(@index IS NULL OR @vb2 IS NULL OR @index < 0)
	BEGIN
		RETURN @vb1;
	END
	
	IF(@index = 0)
	BEGIN
		RETURN @vb2 + @vb1;
	END
	
	IF(@index > DATALENGTH(@vb1))
	BEGIN
		RETURN @vb1 + @vb2;
	END

	DECLARE @vb1b varbinary(max), @vb1e varbinary(max);
	SELECT @vb1b = SubString(@vb1, 1, @index);
	SELECT @vb1e = SubString(@vb1, @index, DATALENGTH(@vb1)-@index);
	RETURN @vb1b + @vb2 + @vb1e;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_VarBinary_InsertAt', DEFAULT
