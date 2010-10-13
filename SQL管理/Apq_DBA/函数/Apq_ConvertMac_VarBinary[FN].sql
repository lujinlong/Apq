IF( OBJECT_ID('dbo.Apq_ConvertMac_VarBinary', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertMac_VarBinary()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-28
-- 描述: 将Mac串转化为 varbinary(max)
-- 示例:
SELECT dbo.Apq_ConvertMac_VarBinary('00-50-8D-9E-EB-70');
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertMac_VarBinary(
	@Mac	varchar(max)
)RETURNS varbinary(max)
AS
BEGIN
	SELECT @Mac = LTRIM(RTRIM(@Mac));

	DECLARE @Return varbinary(max)
		,@Len int		-- 字符数
		,@ib int		-- 当前解析起始位置
		,@ie int		-- 当前解析结束位置
		,@i int
		;
	SELECT @Return = 0x
		,@Len = LEN(@Mac)
		,@ie = 0
		,@i = 1
		;

	IF(@Len < 16) RETURN @Return;

	WHILE(@i <= 6)
	BEGIN
		SELECT	@ib = @ie + 1;
		--SELECT	@ie = CHARINDEX(':', @Mac, @ib);
		SELECT	@ie = CHARINDEX('-', @Mac, @ib);
		IF(@ie = 0)
		BEGIN
			SELECT	@ie = @Len + 1;
		END
		
		IF(@ib >= @ie) BREAK;

		SELECT	@Return = ISNULL(@Return, 0x) + dbo.Apq_ConvertHexStr_VarBinary(SUBSTRING(@Mac, @ib, @ie - @ib));

		SELECT	@i = @i + 1;
	END

	RETURN @Return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertMac_VarBinary', DEFAULT
