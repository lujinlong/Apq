IF ( OBJECT_ID('dbo.Apq_FileTrans_Delete','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_FileTrans_Delete AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'Apq_FileTrans_Delete',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'Apq_FileTrans_Delete'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 删除"缓存文件"
-- 作者: 黄宗银
-- 日期: 2010-04-07
-- 示例:
EXEC dbo.Apq_FileTrans_Delete 2;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'Apq_FileTrans_Delete' ;
GO
ALTER PROC dbo.Apq_FileTrans_Delete @ID bigint
AS 
SET NOCOUNT ON ;

DELETE  dbo.FileTrans
WHERE   ID = @ID ;
RETURN 1 ;
GO
