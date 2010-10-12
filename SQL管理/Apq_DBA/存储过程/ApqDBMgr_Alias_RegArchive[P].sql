IF ( OBJECT_ID('dbo.ApqDBMgr_Alias_RegArchive','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_Alias_RegArchive AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'ApqDBMgr_Alias_RegArchive',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'ApqDBMgr_Alias_RegArchive'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 别名存档(注册表文件)
-- 作者: 黄宗银
-- 日期: 2010-04-11
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Alias_RegArchive NULL;
SELECT @rtn,@ExMsg;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'ApqDBMgr_Alias_RegArchive' ;
GO
ALTER PROC dbo.ApqDBMgr_Alias_RegArchive
    @FullName nvarchar(500)
AS 
SET NOCOUNT ON ;

IF ( @FullName IS NULL ) 
    SELECT  @FullName = 'D:\Apq_DBA\FileOut\ConnectTo.reg' ;

DECLARE @DBFolder nvarchar(500)
   ,@cmd nvarchar(4000)
   ,@rtn int
   ,@s nvarchar(max)
-- 创建目录
SELECT  @DBFolder = Substring(@FullName,1,dbo.Apq_CharIndexR(@FullName,'\',1)) ;
SELECT  @cmd = 'md "' + @DBFolder + '"' ;
EXEC sys.xp_cmdshell @cmd ;

DECLARE @t TABLE (
     Value nvarchar(500)
    ,Data nvarchar(500)
    ) ;

INSERT  @t
        EXEC master..xp_regenumvalues @Root = 'HKEY_LOCAL_MACHINE',@Key = 'SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo' ;

DECLARE @Value nvarchar(500)
   ,@Data nvarchar(500) ;
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT Value,Data FROM @t;

SELECT  @cmd = 'echo Windows Registry Editor Version 5.00>' + @FullName ;
EXEC xp_cmdshell @cmd ;
SELECT  @cmd = 'echo.>>' + @FullName ;
EXEC xp_cmdshell @cmd ;
SELECT  @cmd = 'echo [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo]>>' + @FullName ;
EXEC xp_cmdshell @cmd ;
	
OPEN @csr ;
FETCH NEXT FROM @csr INTO @Value,@Data ;
WHILE ( @@FETCH_STATUS = 0 ) 
    BEGIN
        SELECT  @cmd = 'echo "' + @Value + '"="' + @Data + '">>' + @FullName ;
        EXEC xp_cmdshell @cmd ;
	
        FETCH NEXT FROM @csr INTO @Value,@Data ;
    END
CLOSE @csr ;

RETURN 1 ;
GO
