IF ( OBJECT_ID('dbo.p_who_lock','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.p_who_lock AS BEGIN RETURN END' ;
GO
IF ( NOT EXISTS ( SELECT    *
                  FROM      fn_listextendedproperty(N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'p_who_lock',DEFAULT,DEFAULT) )
   ) 
    EXEC sys.sp_addextendedproperty @name = N'MS_Description',@value = N'',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',
        @level1name = N'p_who_lock'
EXEC sp_updateextendedproperty @name = N'MS_Description',@value = N'
/********************************************************
//　创建 : fengyu　邮件 : maggiefengyu@tom.com
//　日期 :2004-04-30
//　修改 : 从http://www.csdn.net/develop/Read_Article.asp?id=26566
//　学习到并改写
//　说明 : 查看数据库里阻塞和死锁情况
********************************************************/
',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'PROCEDURE',@level1name = N'p_who_lock' ;
GO
ALTER PROC p_who_lock
AS 
SET NOCOUNT ON
 
DECLARE @spid int
   ,@bl int
   ,@intTransactionCountOnEntry int
   ,@intRowcount int
   ,@intCountProperties int
   ,@intCounter int
CREATE TABLE #tmp_lock_who (
     id int IDENTITY(1,1)
    ,spid smallint
    ,bl smallint
    )
IF @@ERROR <> 0 
    RETURN @@ERROR
INSERT  INTO #tmp_lock_who ( spid,bl )
        SELECT  0,blocking_session_id
        FROM    ( SELECT * FROM master.sys.dm_exec_requests WHERE blocking_session_id> 0
                ) a
        WHERE   NOT EXISTS ( SELECT *
                             FROM   ( SELECT * FROM master.sys.dm_exec_requests WHERE blocking_session_id> 0
                                    ) b
                             WHERE  a.blocking_session_id = b.session_id )
        UNION
        SELECT  session_id,blocking_session_id
        FROM    master.sys.dm_exec_requests
        WHERE   blocking_session_id > 0
IF @@ERROR <> 0 
    RETURN @@ERROR
-- 找到临时表的记录数
SELECT  @intCountProperties = Count(*),@intCounter = 1
FROM    #tmp_lock_who
IF @@ERROR <> 0 
    RETURN @@ERROR
IF @intCountProperties = 0 
    SELECT  '现在没有阻塞和死锁信息' AS message
-- 循环开始
WHILE @intCounter <= @intCountProperties 
    BEGIN
		-- 取第一条记录
        SELECT  @spid = spid,@bl = bl
        FROM    #tmp_lock_who
        WHERE   Id = @intCounter
        BEGIN
            IF @spid = 0 
                SELECT  '引起数据库死锁的是: ' + CAST(@bl AS varchar(10)) + '进程号,其执行的SQL语法如下'
            ELSE 
                SELECT  '进程号SPID：' + CAST(@spid AS varchar(10)) + '被' + '进程号SPID：' + CAST(@bl AS varchar(10)) + '阻塞,其当前进程执行的SQL语法如下'
            DBCC INPUTBUFFER (@bl )
        END
		-- 循环指针下移
        SET @intCounter = @intCounter + 1
    END
DROP TABLE #tmp_lock_who
RETURN 0
