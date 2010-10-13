IF( OBJECT_ID('dbo.Apq_NewID', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_NewID()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-01-13
-- 描述: 生成新的唯一ID
-- 示例:
SELECT dbo.Apq_NewID(0xFFFFFFFFFFFFFFFF);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_NewID(
)RETURNS uniqueidentifier
AS
BEGIN
	DECLARE  @rtn varchar(16)
			,@nIP int
			,@i int
			;
	SELECT	 @rtn = 0x0000
			,@nIP = 0
			,@i = 1
			;

	WHILE(@i <= 4)
	BEGIN
		SELECT	@nIP = Convert(int, SUBSTRING(@binIP, @i, 1));
		SELECT	@rtn = ISNULL(@rtn, '') + '.' + Convert(varchar, @nIP);

		SELECT	@i = @i + 1;
	END

	RETURN SUBSTRING(@rtn, 2, LEN(@rtn)-1);
END
GO
