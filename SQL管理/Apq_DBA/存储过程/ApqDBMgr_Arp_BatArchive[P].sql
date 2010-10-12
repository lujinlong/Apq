IF ( OBJECT_ID('dbo.ApqDBMgr_Arp_BatArchive','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_Arp_BatArchive AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'ApqDBMgr_Arp_BatArchive',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'ApqDBMgr_Arp_BatArchive'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- ARP(静态)存档(bat文件)
-- 作者: 黄宗银
-- 日期: 2010-04-12
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Arp_BatArchive NULL;
SELECT @rtn,@ExMsg;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'ApqDBMgr_Arp_BatArchive' ;
GO
ALTER PROC dbo.ApqDBMgr_Arp_BatArchive
    @FullName nvarchar(500)
AS 
SET NOCOUNT ON ;

IF ( @FullName IS NULL ) 
    SELECT  @FullName = 'D:\Apq_DBA\FileOut\arp_all.bat' ;

DECLARE @DBFolder nvarchar(500)
   ,@cmd nvarchar(4000)
   ,@rtn int
   ,@s nvarchar(max)
-- 创建目录
SELECT  @DBFolder = Substring(@FullName,1,dbo.Apq_CharIndexR(@FullName,'\',1)) ;
SELECT  @cmd = 'md "' + @DBFolder + '"' ;
EXEC sys.xp_cmdshell @cmd ;

DECLARE @t TABLE (
     ID bigint IDENTITY(1,1)
    ,s nvarchar(max)
    ) ;

SELECT  @cmd = 'echo.>' + @FullName ;
EXEC xp_cmdshell @cmd ;
SELECT  @cmd = 'arp -a' ;
INSERT  @t
        EXEC @rtn = xp_cmdshell @cmd ;

DECLARE @i bigint ;
SELECT TOP 1
        @i = ID
FROM    @t
WHERE   s LIKE '%static%' ;
IF ( @@ROWCOUNT > 0 ) 
    BEGIN
        WHILE ( 1 = 1 ) 
            BEGIN
                SELECT  @s = s
                FROM    @t
                WHERE   ID = @i ;
                IF ( @@ROWCOUNT = 0
                     OR @s IS NULL
                   ) 
                    BREAK ;
		
                IF ( @s LIKE '%static%' ) 
                    BEGIN
                        SELECT  @cmd = 'echo arp -s' + Substring(@s,2,40) + '>>' + @FullName ;
                        EXEC @rtn = xp_cmdshell @cmd ;
                    END

                SELECT  @i = @i + 1 ;
            END
    END

RETURN 1 ;
GO
