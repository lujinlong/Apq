IF ( OBJECT_ID('dbo.Apq_FileTrans_Insert_ADO','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_FileTrans_Insert_ADO AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'Apq_FileTrans_Insert_ADO',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'Apq_FileTrans_Insert_ADO'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 保存文件(从本地)
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 示例:
DECLARE @rtn int, @ID bigint;
EXEC @rtn = dbo.Apq_FileTrans_Insert_ADO @ID out, ''D:\Bak\Wallow[20100331_1657].bak'','''';
SELECT @rtn;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'Apq_FileTrans_Insert_ADO' ;
GO
ALTER PROC dbo.Apq_FileTrans_Insert_ADO
    @ID bigint OUT
   ,@FullName nvarchar(500)
   ,@CFolder nvarchar(500)
AS 
SET NOCOUNT ON ;

DECLARE @rtn int
   ,@Stream int
   ,@Len int
   ,@i int
   ,@value varbinary(8000)
   ,@constr nvarchar(200)
   ,@sql nvarchar(4000)
   ,@FileName nvarchar(500)
   ,@FileStream varbinary(max)
   ,@DBFolder nvarchar(500)
   ,@cmd nvarchar(4000)
   
DECLARE @source nvarchar(4000)
DECLARE @description nvarchar(4000)
/*
--查看错误信息
EXEC sp_OAGetErrorInfo @Stream, @source OUT, @description OUT
SELECT @source, @description
*/

EXEC sp_OACreate 'ADODB.Stream',@Stream OUT
EXEC sp_OASetProperty @Stream,'type',1--1是二进制 2是文本
EXEC sp_OASetProperty @Stream,'mode',3--读写
EXEC sp_OAMethod @Stream,'Open'-- 打开流
EXEC sp_OAMethod @Stream,'LoadFromFile',NULL,@FullName--打开文件
EXEC sp_OAGetProperty @Stream,'Size',@Len OUT--取长度
SELECT  @FileStream = 0x,@i = 0
WHILE ( @Len > @i ) 
    BEGIN
        EXEC sp_OAMethod @Stream,'Read',@Value OUT,8000
        SELECT  @FileStream = @FileStream + @Value ;
        SET @i = @i + 8000
    END
--EXEC sp_OASetProperty @Stream,'Position',@Len
--EXEC sp_OAMethod @Stream,'SetEos'

-- 清理COM组件
EXEC sp_OADestroy @Stream

-- 计算本地目录
SELECT  @DBFolder = Substring(@FullName,1,dbo.Apq_CharIndexR(@FullName,'\',1)) ;
SELECT  @FileName = Substring(@FullName,dbo.Apq_CharIndexR(@FullName,'\',1) + 1,Len(@FullName) - Len(@DBFolder)) ;

-- 写入数据库
INSERT  dbo.FileTrans ( FileName,FileStream,DBFolder,CFolder )
        SELECT  @FileName,@FileStream,@DBFolder,@CFolder
SELECT  @ID = Scope_identity() ;

RETURN 1 ;
GO
