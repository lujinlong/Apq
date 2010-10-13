IF( OBJECT_ID('dbo.Apq_ConvertHexStr_VarBinary', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertHexStr_VarBinary()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-04-30
-- 描述: [未检测溢出]将一串16进制的字符串 @str 转换为 VarBinary
-- 示例:
SELECT dbo.Apq_ConvertHexStr_VarBinary('7FFFFFFFFFFFFFFF'/*所支持的最大值*/)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertHexStr_VarBinary
(
	@hexstr	varchar(max)
)
RETURNS varbinary(max)
AS
BEGIN
	DECLARE @Return varbinary(max), @ind int, @byte1 int, @byte2 int;
	SELECT @Return = 0x;
	IF(lower(substring(@hexstr, 1, 2)) = '0x') SET @ind = 3;
	ELSE SET @ind = 1;
	
    WHILE ( @ind <= len(@hexstr) )
    BEGIN
        SET @byte1 = ascii(substring(@hexstr, @ind, 1))
        SET @byte2 = ascii(substring(@hexstr, @ind + 1, 1))
        SET @Return = @Return + convert(binary(1),
                  case
                        when @byte1 between 48 and 57 then @byte1 - 48
                        when @byte1 between 65 and 70 then @byte1 - 55
                        when @byte1 between 97 and 102 then @byte1 - 87
                        else null end * 16 +
                  case
                        when @byte2 between 48 and 57 then @byte2 - 48
                        when @byte2 between 65 and 70 then @byte2 - 55
                        when @byte2 between 97 and 122 then @byte2 - 87
                        else null end)
        SET @ind = @ind + 2
    END

	RETURN @Return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertHexStr_VarBinary', DEFAULT
GO
