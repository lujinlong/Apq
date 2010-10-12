
--ALTER PROC tmp_Debug AS
DECLARE @t TABLE (
     ID bigint IDENTITY(1,1)
    ,s nvarchar(max)
    ) ;
DECLARE @tIpConfig TABLE (
     ID bigint IDENTITY(1,1)
    ,nicName nvarchar(50)
    ,MAC nvarchar(50)
    ,IP nvarchar(500)
    ,Gateway nvarchar(500)
    ,DNS nvarchar(500)
    ) ;
DECLARE @rtn int
   ,@Count bigint
   ,@i bigint
   ,@s nvarchar(max)
   ,@nicName nvarchar(50)
   ,@MAC nvarchar(50)
   ,@ip nvarchar(500)
   ,@gw nvarchar(500)
   ,@DNS nvarchar(500)
   ,@cmd nvarchar(4000)
   ,@sql nvarchar(4000) ;
SELECT  @nicName = '',@IP = '',@MAC = '',@Gw = '',@DNS = '' ;

INSERT  @t
EXEC xp_cmdshell 'ipconfig /all' ;

-- 获取最大行数
SELECT  @Count = Max(ID)
FROM    @t ;

SELECT TOP 1
        @i = ID - 1
FROM    @t
WHERE   LTRIM(s) LIKE 'Ethernet adapter%'
WHILE ( @i <= @Count ) 
BEGIN
    SELECT TOP 1
            @i = ID,@s = s
    FROM    @t
    WHERE   ID = @i + 1 ;
    IF ( @@ROWCOUNT = 0 ) 
    BEGIN
        IF ( Len(@nicName) > 0 ) 
        BEGIN
            IF ( Len(@IP) > 1 ) 
                SELECT  @IP = Substring(@IP,2,Len(@IP) - 1) ;
            IF ( Len(@gw) > 1 ) 
                SELECT  @gw = Substring(@gw,2,Len(@gw) - 1) ;
            IF ( Len(@DNS) > 1 ) 
                SELECT  @DNS = Substring(@DNS,2,Len(@DNS) - 1) ;
            INSERT  @tIpConfig ( nicName,MAC,IP,Gateway,DNS )
			SELECT  @nicName,@MAC,@IP,@gw,@DNS ;
        END
        BREAK ;
	END
    
    IF ( @s LIKE 'Ethernet adapter%' ) 
    BEGIN
        IF ( Len(@nicName) > 0 ) 
        BEGIN
            IF ( Len(@IP) > 1 ) 
                SELECT  @IP = Substring(@IP,2,Len(@IP) - 1) ;
            IF ( Len(@gw) > 1 ) 
                SELECT  @gw = Substring(@gw,2,Len(@gw) - 1) ;
            IF ( Len(@DNS) > 1 ) 
                SELECT  @DNS = Substring(@DNS,2,Len(@DNS) - 1) ;
            INSERT  @tIpConfig ( nicName,MAC,IP,Gateway,DNS )
			SELECT  @nicName,@MAC,@IP,@gw,@DNS ;
        END

        SELECT  @nicName = substring(@s,18,Len(@s) - 19),@IP = '',@MAC = '',@Gw = '',@DNS = '' ;
    END
    ELSE IF ( @s LIKE '%Physical ADDRESS%' ) 
    BEGIN
		IF ( LEN(@s) > 45 ) 
			SELECT  @MAC = substring(@s,40,17) ;
	END
    ELSE IF ( @s LIKE '%IP Address%' ) 
    BEGIN
        IF ( LEN(@s) > 45 ) 
        BEGIN
            SELECT  @ip = @ip + ';' + substring(@s,40,Len(@s) - 40) ;
            SELECT TOP 1
                    @i = ID,@s = s
            FROM    @t
            WHERE   ID = @i + 1 ;--IP的下一行即为掩码
            SELECT  @ip = @ip + '/' + substring(@s,40,Len(@s) - 40) ;
        END
	END
    ELSE IF ( @s LIKE '%Default Gateway%' ) 
    BEGIN
        IF ( LEN(@s) > 45 ) 
            WHILE ( 1 = 1 ) 
            BEGIN
                IF ( @s IS NULL
                     OR @s LIKE '%DNS Servers%'
                   ) 
                BEGIN
                    SELECT  @i = @i - 1 ;--退回一行
                    BREAK ;
                END
                SELECT  @gw = @gw + ';' + substring(@s,40,Len(@s) - 40) ;
                SELECT TOP 1
                        @i = ID,@s = s
                FROM    @t
                WHERE   ID = @i + 1 ;--下一行
            END
	END
    ELSE IF ( @s LIKE '%DNS Servers%' ) 
	BEGIN
        IF ( LEN(@s) > 45 ) 
            WHILE ( 1 = 1 ) 
            BEGIN
                IF ( @s IS NULL
                     OR @s LIKE 'Ethernet adapter%'
                     OR LEN(@s) < 45
                   ) 
                BEGIN
                    SELECT  @i = @i - 1 ;--退回一行
                    BREAK ;
                END
                SELECT  @DNS = @DNS + ';' + substring(@s,40,Len(@s) - 40) ;
                SELECT TOP 1
                        @i = ID,@s = s
                FROM    @t
                WHERE   ID = @i + 1 ;--下一行
            END
	END
END

SELECT  *
FROM    @tIpConfig ;

/*
-- 注意:添加路由需要管理员权限,故只生成批处理文档
-- 路由
-- 添加冰川公司路由
SELECT  @cmd = 'echo route -p add 219.134.65.144 mask 255.255.255.255 ' + @gw_wan + '>D:\Apq_DBA\FileOut\ipr_com.bat' ;
EXEC @rtn = xp_cmdshell @cmd ;
*/
SELECT  @rtn,@@ERROR
ENDALL:
--SELECT  @ip_wan,@mask_wan,@gw_wan ;
