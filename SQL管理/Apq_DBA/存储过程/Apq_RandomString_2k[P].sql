IF( OBJECT_ID('dbo.Apq_RandomString_2k', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_RandomString_2k AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-03-31
-- 描述: 生成随机字符串
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(4000), @SimpleChars nvarchar(4000);
SELECT @SimpleChars = 'ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy0123456789';
EXEC @rtn = dbo.Apq_RandomString_2k @ExMsg out, @SimpleChars, 50000, 8, 1, 1;
SELECT @rtn, @ExMsg;
-- =============================================
*/
ALTER PROC dbo.Apq_RandomString_2k
	 @ExMsg nvarchar(4000) out
	 
	,@Chars		nvarchar(4000)='ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy0123456789'	-- 字符集(一般不重复)
	,@Count		int	= 10		-- 个数
	,@Length	int = 16		-- 长度
	,@Repeat	tinyint = 1		-- 每个串中是否允许重复
	,@Distinct	tinyint	= 0		-- 相互之间是否唯一(为1时需要保证 排列数 >= @Count)
AS
SET NOCOUNT ON;

DECLARE @Now datetime, @i int, @l int, @chrs nvarchar(4000)
	,@s nvarchar(4000)	-- 当前已生成的串
	,@p int			-- 当前选中的位置(@chrs中)
	--,@pl int			-- 上次选中的位置(@chrs中)
	,@c nvarchar(1)	-- 上次选中的字符
	;
SELECT @Now = getdate();

CREATE TABLE #t(
	s	nvarchar(4000)
);
IF(@Distinct = 1)
BEGIN
	CREATE INDEX [IX_#t:s] ON #t(s)
END

SELECT @i = 0;
WHILE(@i < @Count)
BEGIN
	SELECT @l = 0, @p = 0, @chrs = @Chars, @s = '', @c='';
	WHILE(@l < @Length)
	BEGIN
		IF(@Repeat = 0 AND LEN(@c) > 0)
		BEGIN
			-- 去除上次选中的字符
			SELECT @chrs = REPLACE(@chrs, @c,'');
		END
		
		SELECT @p = Convert(int,RAND()*LEN(@chrs)) + 1;
		SELECT @c = SUBSTRING(@chrs, @p,1);
		SELECT @s = @s + @c;
	
		SELECT @l = @l + 1;
	END
	
	IF(@Distinct = 1 AND EXISTS(SELECT 1 FROM #t WHERE s = @s))
	BEGIN
		-- 跳过重复
		CONTINUE;
	END
	
	INSERT #t(s) SELECT @s;
	SELECT @i = @i + 1;
END

SELECT * FROM #t;
 
TRUNCATE TABLE #t;
DROP TABLE #t;

SELECT @ExMsg = '生成成功';
RETURN 1;
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_RandomString_2k', DEFAULT
