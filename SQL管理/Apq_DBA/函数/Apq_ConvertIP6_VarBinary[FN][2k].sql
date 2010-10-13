IF( OBJECT_ID('dbo.Apq_ConvertIP6_VarBinary', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertIP6_VarBinary()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-19-25
-- 描述: 将标准的IP6串转为 varbinary(8000)
-- 示例:
SELECT dbo.Apq_ConvertIP6_VarBinary('FFFF::FFFF', 8);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertIP6_VarBinary(
	@IP		varchar(8000),
	@Seg	tinyint	-- 段数
)RETURNS varbinary(8000)
AS
BEGIN
	SELECT	 @IP = LTRIM(RTRIM(@IP))
			,@Seg = ISNULL(@Seg, 8)
			;

	IF(CHARINDEX('::', @IP) = LEN(@IP) - 1)
	BEGIN
		SELECT	@IP = @IP + '0000';
	END

	DECLARE	 @Return varbinary(8000)
			,@Len int		-- 字符数
			,@ib int		-- 当前解析起始位置
			,@ie int		-- 当前解析结束位置
			,@Subs int		-- 分隔符(:)数量
			,@i int
			,@j int
			,@SLength int	-- (如果存在缩写符)缩写符实际表示的 双字节0(0x0000) 的数量(段数)
			;
	SELECT	 @Len = LEN(@IP)
			,@ie = 0
			,@i = 1
			,@j = 1
			;
	SELECT	@Subs = LEN(REPLACE(@IP, ':', 'zz')) - @Len;
	SELECT	@SLength = CASE CHARINDEX('::', @IP) WHEN 0 THEN 0 ELSE @Seg - @Subs END;

	WHILE(@i <= @Seg)
	BEGIN
		SELECT	@ib = @ie + 1;
		IF(@ib > @Len)
		BEGIN
			SELECT	@j = 1;
			WHILE(@j <= @Seg - @i)
			BEGIN
				SELECT	@Return = ISNULL(@Return, 0x) + 0x0000;
				
				SELECT	@j = @j + 1;
			END
			BREAK;
		END
		SELECT	@ie = CHARINDEX(':', @IP, @ib);
		IF(@ie = 0)
		BEGIN
			SELECT	@ie = @Len + 1;
		END

		IF(@ie = @ib)
		BEGIN	-- 遇到缩写符
			IF(@ib = 1)
			BEGIN
				SELECT	@SLength = @SLength + 1;
			END
			SELECT	@i = @i + @SLength - 1;

			SELECT	@j = 1;
			WHILE(@j <= @SLength)
			BEGIN
				SELECT	@Return = ISNULL(@Return, 0x) + 0x0000;
				
				SELECT	@j = @j + 1;
			END

			IF(@ib = 1)
			BEGIN
				SELECT	@ie = @ie + 1;
			END
		END
		ELSE
		BEGIN
			SELECT	@Return = ISNULL(@Return, 0x) + Convert(binary(2), dbo.Apq_ConvertPBigintFrom(16, SUBSTRING(@IP, @ib, @ie - @ib)));
		END

		SELECT	@i = @i + 1;
	END

	RETURN @Return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertIP6_VarBinary', DEFAULT
