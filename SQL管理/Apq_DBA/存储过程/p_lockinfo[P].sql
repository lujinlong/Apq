IF ( OBJECT_ID('dbo.p_lockinfo','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.p_lockinfo AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'p_lockinfo',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'p_lockinfo'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
/*--处理BLOCK
 查看当前进程,或BLOCK进程,并能自动杀掉死进程
 因为是针对block的,所以如果有block进程,只能查看block进程
 当然,你可以通过参数控制,不管有没有block,都只查看block进程
 感谢: caiyunxia,jiangopen 两位提供的参考信息
--邹建 2004.4(引用请保留此信息)--*/
/*--调用示例
EXEC dbo.p_lockinfo 0, 1
--*/
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'p_lockinfo' ;
GO
ALTER PROC p_lockinfo
    @kill_lock_spid tinyint = 0  --是否杀掉block的进程,1 杀掉, 0 仅显示
   ,@show_spid_if_nolock tinyint = 1 --如果没有block的进程,是否显示正常进程信息,1 显示,0 不显示
AS 
SET NOCOUNT ON
DECLARE @count int
   ,@s nvarchar(4000)
   ,@i int
SELECT  id = IDENTITY( bigint,1,1),标志,进程ID = spid,线程ID = kpid,块进程ID = blocked,数据库ID = dbid,数据库名 = db_name(dbid),用户ID = uid,用户名 = CONVERT(nvarchar(128),loginame),
        累计CPU时间 = cpu,登陆时间 = login_time,打开事务数 = open_tran,进程状态 = CONVERT(nvarchar(30),status),工作站名 = CONVERT(nvarchar(128),hostname),
        应用程序名 = CONVERT(nvarchar(128),program_name),工作站进程ID = CONVERT(nvarchar(10),hostprocess),域名 = CONVERT(nvarchar(128),nt_domain),
        网卡地址 = CONVERT(nvarchar(12),net_address)
INTO    #t
FROM    ( SELECT    标志 = '|_牺牲品(被BLOCK)_>',spid,kpid,blocked,dbid,uid,loginame,cpu,login_time,open_tran,status,hostname,program_name,hostprocess,nt_domain,
                    net_address,s1 = blocked,s2 = 1
          FROM      master..sysprocesses a
          WHERE     blocked <> 0
          UNION ALL
          SELECT    'BLOCK的进程',spid,kpid,a.blocked,dbid,uid,loginame,cpu,login_time,open_tran,status,hostname,program_name,hostprocess,nt_domain,
                    net_address,s1 = a.spid,s2 = 0
          FROM      master..sysprocesses a
                    JOIN ( SELECT   blocked
                           FROM     master..sysprocesses
                           GROUP BY blocked
                         ) b
                        ON a.spid = b.blocked
          WHERE     a.blocked = 0
        ) a
ORDER BY s1,s2
SELECT  @count = @@rowcount,@i = 1
IF @count = 0
    AND @show_spid_if_nolock = 1 
    BEGIN
        INSERT  #t
                SELECT  标志 = '正常的进程',spid,kpid,blocked,dbid,db_name(dbid),uid,loginame,cpu,login_time,open_tran,status,hostname,program_name,hostprocess,
                        nt_domain,net_address
                FROM    master..sysprocesses
        SET @count = @@rowcount
    END
IF @count > 0 
    BEGIN
        CREATE TABLE #t1 (
             id bigint IDENTITY(1,1)
            ,a nvarchar(50)
            ,b bigint
            ,EventInfo nvarchar(max)
            )
        IF @kill_lock_spid = 1 
            BEGIN
                DECLARE @spid varchar(10)
                   ,@标志 varchar(10)
                WHILE @i <= @count 
                    BEGIN
                        SELECT  @spid = 进程ID,@标志 = 标志
                        FROM    #t
                        WHERE   id = @i
                        INSERT  #t1
                                EXEC ( 'DBCC INPUTBUFFER(' + @spid + ')'
                                    )
                        IF @@rowcount = 0 
                            INSERT  #t1 ( a )
                            VALUES  ( NULL )
                        IF @标志 = 'BLOCK的进程' 
                            EXEC('kill '+@spid)
                        SET @i = @i + 1
                    END
            END
        ELSE 
            WHILE @i <= @count 
                BEGIN
                    SELECT  @s = 'DBCC INPUTBUFFER(' + cast(进程ID AS varchar) + ')'
                    FROM    #t
                    WHERE   id = @i
                    INSERT  #t1
                            EXEC ( @s
                                )
                    IF @@rowcount = 0 
                        INSERT  #t1 ( a )
                        VALUES  ( NULL )
                    SET @i = @i + 1
                END
        SELECT  a.*,进程的SQL语句 = b.EventInfo
        FROM    #t a
                JOIN #t1 b
                    ON a.id = b.id
        ORDER BY 进程ID
    END

