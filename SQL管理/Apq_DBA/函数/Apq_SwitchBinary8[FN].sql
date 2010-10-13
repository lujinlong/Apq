IF( OBJECT_ID('dbo.Apq_SwitchBinary8', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_SwitchBinary8()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-12-18
-- 描述: 交换高低位,便于C++处理
-- 示例:
SELECT dbo.Apq_SwitchBinary8(0x00000000FFFFFFFF)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_SwitchBinary8(
	@Input	binary(8)
)RETURNS binary(8)AS
BEGIN
	DECLARE	@bin41 binary(4), @bin42 binary(4);
	SELECT	 @bin41 = @Input					-- 高位
			,@bin42 = Substring(@Input, 5, 4)	-- 低位
	RETURN @bin42 + @bin41;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_SwitchBinary8', DEFAULT
