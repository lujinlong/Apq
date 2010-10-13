IF( OBJECT_ID('dbo.Apq_IsNumeric', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_IsNumeric()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-18
-- 描述: 判断字符串是否全为数字
-- 示例:
SELECT dbo.Apq_IsNumeric('30000229');
-- =============================================
*/
ALTER FUNCTION dbo.Apq_IsNumeric(
	@s	nvarchar(50)
)RETURNS tinyint
AS
BEGIN
	DECLARE @rtn tinyint, @i int, @c nvarchar(1);

	SELECT @i = 1;
	WHILE(@i <= LEN(@s))
	BEGIN
		SELECT @c = SubString(@s, @i, 1);
		IF(@c < '0' OR '9' < @c)
		BEGIN
			RETURN 0;
		END

		SELECT @i = @i + 1;
	END

	RETURN 1;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_IsNumeric', DEFAULT
GO
