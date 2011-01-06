IF( OBJECT_ID('dbo.Apq_Combine', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Combine AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_Combine
     @str		nvarchar(max)		-- 最多63列
    ,@n			int = NULL			-- 单行列数({<=0:任意个数,>0:确定个数})
    ,@tsplit	nvarchar(1) = NULL	-- 列分隔符
    ,@rsplit	nvarchar(1) = NULL	-- 行分隔符
	,@strOut	nvarchar(max) = NULL OUT	-- 输出组合
	,@nCombine	int = NULL OUT				-- 组合个数
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-01-06
-- 描述: 返回组合
-- 示例:
DECLARE @rtn int, @strOut nvarchar(max);
EXEC @rtn = dbo.Apq_Combine '1,2,3,',NULL,NULL,NULL,@strOut out;
SELECT @rtn,@strOut;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @str = isnull(@str,'')
	,@n = ISnull(@n,0)
	,@tsplit = isnull(@tsplit,',')
	,@rsplit = isnull(@rsplit,'
')
	,@strOut = ''
	,@nCombine = 1
	;
	
IF(charindex(@tsplit,@str) <= 0)
BEGIN
	SELECT @nCombine = 1, @strOut = @str;
	RETURN 1;
END

SELECT @n = CASE WHEN @n < 0 THEN 0 ELSE @n END;
SELECT @str = @str + CASE WHEN RIGHT(@str,1) = @tsplit THEN '' ELSE @tsplit END;	-- 保证以列分隔符结尾

DECLARE @t TABLE (
     ID [bigint] NOT NULL
    ,s nvarchar(4000)
);

DECLARE @MaxID bigint, @c nvarchar(4000), @pB int, @pE int, @i int;
SELECT @pB = 0, @pE = 0, @i = -1;
WHILE(@pE<Len(@str))
BEGIN
	SELECT @pB = @pE+1,@i = @i + 1;
	SELECT @pE = charindex(@tsplit,@str,@pB);
	IF(@pE <= 0) SELECT @pE = Len(@str);
	SELECT @c = substring(@str,@pB,@pE-@pB)
	INSERT @t(ID,s) VALUES(Power(2,@i),@c);
END

SELECT @MaxID = Power(2,@i+1);

DECLARE @bi binary(8), @ni bigint, @idxstr nvarchar(4000), @strC nvarchar(4000);

SELECT @ni = 1;
WHILE(@ni < @MaxID)
BEGIN
	SELECT @bi = @ni;
	
	SELECT @idxstr = dbo.Apq_VarBinary_ComputeBitIndex(@bi);
	SELECT @pB = 1, @pE = 1, @i = 0, @strC = '';
	WHILE(@pE<Len(@idxstr))
	BEGIN
		SELECT @pB = @pE+1,@i = @i + 1;
		SELECT @pE = charindex(',',@idxstr,@pB);
		IF(@pE <= 0) SELECT @pE = Len(@idxstr) + 1;
		SELECT @c = substring(@idxstr,@pB,@pE-@pB)
		SELECT @strC = @strC + s + @tsplit
		  FROM @t
		 WHERE ID = Power(2,64-Convert(int,@c));
	END
	IF(@i = @n OR @n = 0)
	BEGIN
		SELECT @nCombine = @nCombine + 1
			,@strOut = @strOut + @strC + @rsplit;
	END
	
	SELECT @ni = @ni + 1;
END

PRINT @strOut;
RETURN 1;
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_Combine', DEFAULT
