IF ( OBJECT_ID('dbo.ApqDBMgr_Route_BatArchive','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_Route_BatArchive AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'ApqDBMgr_Route_BatArchive',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'ApqDBMgr_Route_BatArchive'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
-- 路由存档(bat文件)
-- 作者: 黄宗银
-- 日期: 2010-04-11
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Route_BatArchive NULL;
SELECT @rtn,@ExMsg;
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'ApqDBMgr_Route_BatArchive' ;
GO
ALTER PROC dbo.ApqDBMgr_Route_BatArchive
    @FullName nvarchar(500)
AS 
SET NOCOUNT ON ;

IF ( @FullName IS NULL ) 
    SELECT  @FullName = 'D:\Apq_DBA\FileOut\ipr_all.bat' ;

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
SELECT  @cmd = 'route print' ;
INSERT  @t
        EXEC @rtn = xp_cmdshell @cmd ;

DECLARE @i bigint ;
SELECT  @i = ID
FROM    @t
WHERE   s = 'Persistent Routes:' ;
IF ( @@ROWCOUNT > 0 ) 
    BEGIN
        SELECT  @i = @i + 1 ;	-- 跳过列头
        WHILE ( 1 = 1 ) 
            BEGIN
                SELECT  @i = @i + 1 ;
                SELECT  @s = s
                FROM    @t
                WHERE   ID = @i ;
                IF ( @@ROWCOUNT = 0
                     OR @s IS NULL
                   ) 
                    BREAK ;
		
                IF ( Len(@s) > 58 ) 
                    BEGIN
                        SELECT  @cmd = 'echo route add -p' + Substring(@s,2,17) + 'mask' + substring(@s,19,33) + '>>' + @FullName ;
                        EXEC @rtn = xp_cmdshell @cmd ;
                    END
            END
    END

RETURN 1 ;
GO
