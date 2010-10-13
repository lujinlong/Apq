IF( OBJECT_ID('dbo.Apq_RConvertBigInt_VarBinary8', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_RConvertBigInt_VarBinary8()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-05-07
-- 描述: 逆置类型转换(最多支持8字节,bigint类型)
-- 示例:
SELECT dbo.Apq_RConvertBigInt_VarBinary8(0x0100)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_RConvertBigInt_VarBinary8(
	@Input	bigint,
	@Length	tinyint	-- [1,8]
)RETURNS varbinary(8) AS
BEGIN
	DECLARE	@bin varbinary(8), @rtn varbinary(8);
	SELECT @bin = Convert(varbinary, @Input);
	SELECT @bin = dbo.Apq_VarBinary_Reverse(@bin);
	SELECT @rtn = SubString(@bin,1, @Length);
	RETURN @rtn;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_RConvertBigInt_VarBinary8', DEFAULT
