IF( OBJECT_ID('dbo.Apq_ConvertBinary16_IP6', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertBinary16_IP6()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-10-30
-- 描述: 将 binary(16) 转化为 IP6串
-- 示例:
SELECT dbo.Apq_ConvertBinary16_IP6(0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertBinary16_IP6(
	@binIP	binary(16)
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

	WHILE(@i <= 16)
	BEGIN
		SELECT	@nIP = Convert(int, SUBSTRING(@binIP, @i, 2));
		SELECT	@Return = ISNULL(@Return, '') + ':' + dbo.Apq_ConvertScale(10, 16, Convert(varchar, @nIP));

		SELECT	@i = @i + 2;
	END

	RETURN SUBSTRING(@Return, 2, LEN(@Return)-1);
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertBinary16_IP6', DEFAULT
