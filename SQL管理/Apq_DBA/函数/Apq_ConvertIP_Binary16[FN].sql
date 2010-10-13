IF( OBJECT_ID('dbo.Apq_ConvertIP_Binary16', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_ConvertIP_Binary16()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-28
-- 描述: 将IP串转为 binary(16)
-- 示例:
SELECT dbo.Apq_ConvertIP_Binary16('FFFF:FFFF:FFFF:FFFF:FFFF::255.255.255.255');
-- =============================================
*/
ALTER FUNCTION dbo.Apq_ConvertIP_Binary16(
	@IP	varchar(max)
)RETURNS binary(16)
AS
BEGIN
	SELECT	@IP = LTRIM(RTRIM(@IP));

	DECLARE	@Return varbinary(max), @IsIP6 int
			,@Len int		-- 字符数
			,@Subs int		-- 分隔符(:)数量
			,@i int
			;
	SELECT	 @IsIP6 = CHARINDEX(':', @IP)
			,@Len = LEN(@IP)
			,@i = 0
			;
	SELECT	@Subs = LEN(REPLACE(@IP, ':', 'zz')) - @Len;

	IF(@IsIP6 > 0)
	BEGIN	-- IP6:(FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF)(FFFF::FFFF:FFFF)
			-- (FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:255.255.255.255)(FFFF::255.255.255.255)
		IF(CHARINDEX('.', @IP) > 0)
		BEGIN	-- 混合IP4
			DECLARE	@IP6End int;	-- IP6 的结束位置
			SELECT	@IP6End = 0;
			WHILE(@i < @Subs)
			BEGIN
				SELECT	@IP6End = CHARINDEX(':', @IP, @IP6End+1);

				SELECT	@i = @i + 1;
			END

			SELECT	@Return = dbo.Apq_ConvertIP6_VarBinary(SUBSTRING(@IP, 1, @IP6End), 6) + 
						dbo.Apq_ConvertIP4_VarBinary(SUBSTRING(@IP, @IP6End+1, @Len - @IP6End));
		END
		ELSE
		BEGIN	-- IP6标准地址
			SELECT	@Return = dbo.Apq_ConvertIP6_VarBinary(@IP, 8);
		END
	END
	ELSE
	BEGIN
		SELECT	@Return = 0x000000000000000000000000 + dbo.Apq_ConvertIP4_VarBinary(@IP);
	END

	RETURN @Return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ConvertIP_Binary16', DEFAULT
