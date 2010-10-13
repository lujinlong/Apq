IF( OBJECT_ID('dbo.Apq_ConvertVarBinary_HexStr', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertVarBinary_HexStr()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-15
-- 描述: 将VarBinary转换为16进制字符串表示(不含0x)
-- 示例:
SELECT dbo.Apq_ConvertVarBinary_HexStr(0x0123456789ABCDEF)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertVarBinary_HexStr
(
	@bin	varbinary(max)
)
RETURNS varchar(max)
AS
BEGIN
	DECLARE @Return varchar(max), @ind int, @byte binary(1),@byte1 int, @byte2 int;
	SELECT @Return = '',@ind = 1;
	
    WHILE ( @ind <= datalength(@bin) )
    BEGIN
		SELECT @byte = substring(@bin, @ind, 1);
        SET @byte1 = @byte / 16
        IF(@byte1 >= 10)
			SELECT @Return = @Return + 
				CASE @byte1
					WHEN 10 THEN 'A'
					WHEN 11 THEN 'B'
					WHEN 12 THEN 'C'
					WHEN 13 THEN 'D'
					WHEN 14 THEN 'E'
					WHEN 15 THEN 'F'
				END
		ELSE
			SELECT @Return = @Return + convert(char(1),@byte1)

        SET @byte2 = @byte % 16
        IF(@byte2 >= 10)
			SELECT @Return = @Return + 
				CASE @byte2
					WHEN 10 THEN 'A'
					WHEN 11 THEN 'B'
					WHEN 12 THEN 'C'
					WHEN 13 THEN 'D'
					WHEN 14 THEN 'E'
					WHEN 15 THEN 'F'
				END
		ELSE
			SELECT @Return = @Return + convert(char(1),@byte2)

		SELECT @ind = @ind + 1;
    END

	RETURN @Return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertVarBinary_HexStr', DEFAULT
GO
