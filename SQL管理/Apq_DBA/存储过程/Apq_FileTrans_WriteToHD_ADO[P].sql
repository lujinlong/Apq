IF ( OBJECT_ID('dbo.Apq_FileTrans_WriteToHD_ADO','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_FileTrans_WriteToHD_ADO AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'Apq_FileTrans_WriteToHD_ADO',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'Apq_FileTrans_WriteToHD_ADO'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 保存文件
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_FileTrans_WriteToHD_ADO 2, 1;
SELECT @rtn;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'Apq_FileTrans_WriteToHD_ADO' ;
GO
ALTER PROC dbo.Apq_FileTrans_WriteToHD_ADO
    @ID bigint
   ,@KeepInDB tinyint
AS 
SET NOCOUNT ON ;

DECLARE @rtn int
   ,@rs int
   ,@Stream int
   ,@Len int
   ,@i int
   ,@value varbinary(8000)
   ,@constr nvarchar(200)
   ,@sql nvarchar(4000)
   ,@FileName nvarchar(500)
   ,@DBFolder nvarchar(500)
   ,@FullName nvarchar(500)
   ,@cmd nvarchar(4000)
   
DECLARE @source nvarchar(4000)
DECLARE @description nvarchar(4000)
/*
--查看错误信息
EXEC sp_OAGetErrorInfo @rs, @source OUT, @description OUT
SELECT @source, @description
*/

SET @constr = 'Provider=SQLOLEDB;Data Source=(local);Initial Catalog=' + db_name() + ';Integrated Security=SSPI;'
SET @sql = 'SELECT FileName, FileStream, DBFolder,CFolder FROM dbo.FileTrans WHERE ID = ' + CONVERT(nvarchar,@ID) ;
EXEC sp_OACreate 'ADODB.Recordset',@rs OUT
EXEC sp_OAMethod @rs,'open',NULL,@sql,@constr
EXEC sp_OAGetProperty @rs,'Fields.item(0).Value',@FileName OUT
EXEC sp_OAGetProperty @rs,'Fields.item(2).Value',@DBFolder OUT
EXEC sp_OAGetProperty @rs,'Fields.item(1).ActualSize',@len OUT
EXEC sp_OACreate 'ADODB.Stream',@Stream OUT
EXEC sp_OASetProperty @Stream,'type',1--1是二进制 2是文本
EXEC sp_OASetProperty @Stream,'mode',3--读/写状态
EXEC sp_OAMethod @Stream,'open'--打开流
SET @i = 0
WHILE ( @Len > @i ) 
    BEGIN
        EXEC sp_OAGetProperty @rs,'Fields.item(1).GetChunk',@Value OUT,8000
        EXEC sp_OAMethod @Stream,'write',NULL,@Value
        SET @i = @i + 8000
    END
--EXEC sp_OASetProperty @Stream,'Position',@Len
--EXEC sp_OAMethod @Stream,'SetEos'

-- 创建目录
SELECT  @cmd = 'md "' + @DBFolder + '"' ;
EXEC sys.xp_cmdshell @cmd ;

IF ( RIGHT(@DBFolder,1) <> '\' ) SELECT @DBFolder = @DBFolder + '\' ;
-- 写入文件
SELECT  @FullName = @DBFolder + @FileName ;
EXEC sp_OAMethod @Stream,'SaveToFile',NULL,@FullName,2

-- 清理COM组件
EXEC sp_OADestroy @rs
EXEC sp_OADestroy @Stream

-- 删除数据库内的记录
IF ( @KeepInDB IS NULL
     OR @KeepInDB <> 1
   ) 
    DELETE  FileTrans
    WHERE   ID = @ID ;

RETURN 1 ;
GO
