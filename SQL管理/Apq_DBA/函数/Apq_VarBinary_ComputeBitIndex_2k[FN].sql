IF( OBJECT_ID('dbo.Apq_VarBinary_ComputeBitIndex_2k', 'FN') IS NULL )
	EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_VarBinary_ComputeBitIndex_2k()RETURNS int AS BEGIN RETURN 0 END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-07
-- 描述: 计算二进串中为1的位索引位置
-- 示例:
SELECT dbo.Apq_VarBinary_ComputeBitIndex_2k(0x000000000000000000000000000000000000000000800000000000)
-- =============================================
*/
ALTER FUNCTION dbo.Apq_VarBinary_ComputeBitIndex_2k(
	@vb	varbinary(8000)
)RETURNS varchar(8000) AS
BEGIN
	DECLARE @return varchar(8000), @i int, @p int, @b tinyint;
	SELECT @return = '', @i = 1;
	WHILE(@i <= DATALENGTH(@vb))
	BEGIN
		SELECT @b = SubString(@vb, @i, 1);
		
		SELECT @p = 0;
		WHILE(@p < 8)
		BEGIN
			IF((@b & 128 / Convert(tinyint,POWER(2,@p))) > 0)
			BEGIN
				SELECT @return = @return + ',' + Convert(varchar,(@i-1)*8+1+@p);
			END
			
			SELECT @p = @p + 1;
		END

		SELECT @i = @i + 1;
	END

	RETURN @return;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_VarBinary_ComputeBitIndex_2k', DEFAULT
