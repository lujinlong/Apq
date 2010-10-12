IF ( OBJECT_ID('dbo.Apq_FileTrans_List','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_FileTrans_List AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'Apq_FileTrans_List',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'Apq_FileTrans_List'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 列表"缓存文件"(用于下载)
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 示例:
EXEC dbo.Apq_FileTrans_List 2;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'Apq_FileTrans_List' ;
GO
ALTER PROC dbo.Apq_FileTrans_List @ID bigint
AS 
SET NOCOUNT ON ;

SELECT  FileName,FileStream,DBFolder,CFolder
FROM    dbo.FileTrans
WHERE   ID = @ID ;
RETURN 1 ;
GO
