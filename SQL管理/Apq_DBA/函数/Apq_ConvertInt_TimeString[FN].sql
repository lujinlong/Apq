IF( OBJECT_ID('dbo.Apq_ConvertInt_TimeString', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertInt_TimeString()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 将整数表示的时间 转换为 24小时时间字符串,格式[hh:mi:ss]
-- 示例:
SELECT dbo.Apq_ConvertInt_TimeString('310')
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertInt_TimeString
(
	@nTime	int
)
RETURNS nvarchar(8)
AS
BEGIN
	DECLARE @Return nvarchar(8), @strTime nvarchar(8), @p int;
    SELECT @strTime = RIGHT('000000'+Convert(nvarchar(6),@nTime),6);
	SELECT @Return = LEFT(@strTime,2);
    SELECT @p = 3;
    WHILE(@p < 6)
    BEGIN
		SELECT @Return = @Return + ':' + Substring(@strTime,@p,2);
    
    	SELECT @p = @p + 2;
    END

	RETURN @Return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertInt_TimeString', DEFAULT
GO
