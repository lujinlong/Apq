IF ( OBJECT_ID('dbo.Apq_CharIndexR','FN') IS NULL ) 
    EXEC sp_executesql N'CREATE FUNCTION dbo.Apq_CharIndexR()RETURNS int AS BEGIN RETURN 0 END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 描述: 自右向左的CharIndex
-- 示例:
SELECT dbo.Apq_CharIndexR('D:\Apq_DBA\UpFile\D\Bak\[Log]Apq_Bak.txt','\',1);
-- =============================================
*/
ALTER FUNCTION dbo.Apq_CharIndexR (
     @str nvarchar(max)
    ,@str_find nvarchar(max)
    ,@Num bigint = 1
    )
RETURNS bigint
AS 
BEGIN
    DECLARE @t TABLE (
         ID [bigint] IDENTITY(1,1)
                     NOT NULL
        ,Pos bigint
        )
    DECLARE @Return bigint
       ,@Len bigint	-- 长度
       ,@ib int		-- 当前解析起始位置
       ,@ie int		-- 当前解析结束位置
       ,@i int ;
       
    SELECT  @Len = Len(@str) ;
    SELECT  @ib = 1 ;
    WHILE ( @ib <= @Len ) 
        BEGIN
            SELECT  @ie = Charindex(@str_find,@str,@ib) ;
            IF ( @ie > 0 ) 
                INSERT  @t ( Pos )
                        SELECT  @ie ;
            ELSE 
                BREAK ;
                
            SELECT  @ib = @ie + 1 ;
        END
    
    SELECT TOP ( @Num )
            @Return = Pos
    FROM    @t
    ORDER BY ID DESC
    
    RETURN @Return ;
END
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_CharIndexR', DEFAULT
