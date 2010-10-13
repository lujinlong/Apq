IF( OBJECT_ID('dbo.Apq_RConvertVarBinary8X_BigInt', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_RConvertVarBinary8X_BigInt()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-19
-- 描述: 逆置类型转换,符号扩展(最多支持8字节,bigint类型)
-- 示例:
SELECT dbo.Apq_RConvertVarBinary8X_BigInt(0x01F1)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_RConvertVarBinary8X_BigInt(
	@Input	varbinary(8)
)RETURNS bigint AS
BEGIN
	IF(DATALENGTH(@Input) < 8)
	BEGIN
		IF((128 & SubString(@Input,DataLength(@Input),1)) > 0)
		BEGIN
			-- 符号扩展
			SELECT @Input = @Input + 0xFFFFFFFFFFFFFF/*7字节*/
		END
	END
	
	RETURN dbo.Apq_VarBinary_Reverse(@Input);
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_RConvertVarBinary8X_BigInt', DEFAULT
