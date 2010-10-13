
IF( OBJECT_ID('dbo.Apq_ConvertBinary4_IP4', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertBinary4_IP4()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-10-30
-- 描述: 将 binary(4) 转化为 IP4串
-- 示例:
SELECT dbo.Apq_ConvertBinary4_IP4(0xFFFFFFFFFFFFFFFF);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertBinary4_IP4(
	@binIP	binary(4)
)RETURNS varchar(max)
AS
BEGIN
	DECLARE	 @Return varchar(max)
			,@nIP int
			,@i int
			;
	SELECT	 @nIP = 0
			,@i = 1
			;

	WHILE(@i <= 4)
	BEGIN
		SELECT	@nIP = Convert(int, SUBSTRING(@binIP, @i, 1));
		SELECT	@Return = ISNULL(@Return, '') + '.' + Convert(varchar, @nIP);

		SELECT	@i = @i + 1;
	END

	RETURN SUBSTRING(@Return, 2, LEN(@Return)-1);
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertBinary4_IP4', DEFAULT
