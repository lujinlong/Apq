IF ( OBJECT_ID('dbo.Apq_FileTrans_Insert','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_FileTrans_Insert AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'Apq_FileTrans_Insert',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'Apq_FileTrans_Insert'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 保存文件到数据库
-- 作者: 黄宗银
-- 日期: 2010-03-29
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'Apq_FileTrans_Insert' ;
GO
ALTER PROC dbo.Apq_FileTrans_Insert
    @ID bigint OUT
   ,@FileName nvarchar(500)
   ,@DBFolder nvarchar(500)
   ,@CFolder nvarchar(500)
   ,@FileStream varbinary(max)
AS 
SET NOCOUNT ON ;

INSERT  dbo.FileTrans ( FileName,DBFolder,CFolder,FileStream )
VALUES  ( @FileName,@DBFolder,@CFolder,@FileStream ) ;
SELECT  @ID = Scope_identity() ;
RETURN 1 ;
GO
