/* 
生成脚本:
选择特定数据库对象
全选

重新生成脚本以后,只更新此注释以下全部内容即可.
*/

USE [Apq_DBA]
GO
/****** Object:  User [BcpIn]    Script Date: 11/09/2010 11:49:49 ******/
CREATE USER [BcpIn] FOR LOGIN [BcpIn] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [LinkIn]    Script Date: 11/09/2010 11:49:49 ******/
CREATE USER [LinkIn] FOR LOGIN [LinkIn] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Performance_Log_User]    Script Date: 11/09/2010 11:49:49 ******/
CREATE USER [Performance_Log_User] FOR LOGIN [BUILTIN\Performance Log Users]
GO
/****** Object:  User [Web]    Script Date: 11/09/2010 11:49:49 ******/
CREATE USER [Web] FOR LOGIN [Web] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Web_Bg]    Script Date: 11/09/2010 11:49:49 ******/
CREATE USER [Web_Bg] FOR LOGIN [Web_Bg] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Web_WS]    Script Date: 11/09/2010 11:49:49 ******/
CREATE USER [Web_WS] FOR LOGIN [Web_WS] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Schema [bak]    Script Date: 11/09/2010 11:49:49 ******/
CREATE SCHEMA [bak] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [bcp]    Script Date: 11/09/2010 11:49:49 ******/
CREATE SCHEMA [bcp] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [dic]    Script Date: 11/09/2010 11:49:49 ******/
CREATE SCHEMA [dic] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [etl]    Script Date: 11/09/2010 11:49:49 ******/
CREATE SCHEMA [etl] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [mgr]    Script Date: 11/09/2010 11:49:49 ******/
CREATE SCHEMA [mgr] AUTHORIZATION [dbo]
GO
/****** Object:  StoredProcedure [dbo].[p_who_lock]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_who_lock]
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
/********************************************************
//　创建 : fengyu　邮件 : maggiefengyu@tom.com
//　日期 :2004-04-30
//　修改 : 从http://www.csdn.net/develop/Read_Article.asp?id=26566
//　学习到并改写
//　说明 : 查看数据库里阻塞和死锁情况
********************************************************/
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'p_who_lock'
GO
/****** Object:  StoredProcedure [dbo].[p_lockinfo]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_lockinfo]
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
/*--处理BLOCK
 查看当前进程,或BLOCK进程,并能自动杀掉死进程
 因为是针对block的,所以如果有block进程,只能查看block进程
 当然,你可以通过参数控制,不管有没有block,都只查看block进程
 感谢: caiyunxia,jiangopen 两位提供的参考信息
--邹建 2004.4(引用请保留此信息)--*/
/*--调用示例
EXEC dbo.p_lockinfo 0, 1
--*/
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'p_lockinfo'
GO
/****** Object:  Table [etl].[Log_Stat]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [etl].[Log_Stat](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatName] [nvarchar](256) NOT NULL,
	[STMT] [nvarchar](4000) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[_LogTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Log_Stat] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计命名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'Log_Stat', @level2type=N'COLUMN',@level2name=N'StatName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计存储过程或统计语句,参数记录具体值' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'Log_Stat', @level2type=N'COLUMN',@level2name=N'STMT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BcpIn到的表名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'Log_Stat', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
/****** Object:  Table [dbo].[Log_SSIS]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log_SSIS](
	[c_Date] [datetime] NULL,
	[c_Time] [varchar](10) NULL,
	[c_Ip] [varchar](20) NULL,
	[cs_Username] [varchar](20) NULL,
	[s_Ip] [varchar](20) NULL,
	[s_ComputerName] [varchar](30) NULL,
	[s_Port] [varchar](10) NULL,
	[cs_Method] [varchar](10) NULL,
	[cs_Uri_Stem] [varchar](500) NULL,
	[cs_Uri_Query] [varchar](500) NULL,
	[sc_Status] [varchar](20) NULL,
	[sc_SubStatus] [varchar](20) NULL,
	[sc_Win32_Status] [varchar](20) NULL,
	[sc_Bytes] [int] NULL,
	[cs_Bytes] [int] NULL,
	[time_Taken] [varchar](10) NULL,
	[cs_Version] [varchar](20) NULL,
	[cs_Host] [varchar](20) NULL,
	[cs_User_Agent] [varchar](500) NULL,
	[cs_Refere] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log_DTS_LocalPick]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_DTS_LocalPick](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CfgName] [nvarchar](256) NOT NULL,
	[PickTime] [datetime] NOT NULL,
	[HasContent] [tinyint] NOT NULL,
	[FileFolder] [nvarchar](3000) NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[FileEX] [nvarchar](50) NOT NULL,
	[t] [nvarchar](50) NOT NULL,
	[r] [nvarchar](50) NOT NULL,
	[TransTime] [datetime] NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_Log_DTS_LocalPick] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Log_DTS_LocalPick:CfgName] ON [dbo].[Log_DTS_LocalPick] 
(
	[CfgName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送配置名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_DTS_LocalPick', @level2type=N'COLUMN',@level2name=N'CfgName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采集时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_DTS_LocalPick', @level2type=N'COLUMN',@level2name=N'PickTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否收集到数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_DTS_LocalPick', @level2type=N'COLUMN',@level2name=N'HasContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件夹' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_DTS_LocalPick', @level2type=N'COLUMN',@level2name=N'FileFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_DTS_LocalPick', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_DTS_LocalPick', @level2type=N'COLUMN',@level2name=N'FileEX'
GO
/****** Object:  Table [dbo].[Log_Apq_Alarm]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_Apq_Alarm](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
	[Severity] [int] NOT NULL,
	[Titile] [nvarchar](50) NOT NULL,
	[Msg] [nvarchar](max) NULL,
 CONSTRAINT [PK_Log_Apq_Alarm] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Log_Apq_Alarm:_InTime] ON [dbo].[Log_Apq_Alarm] 
(
	[_InTime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型{0:未分类}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_Apq_Alarm', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'严重度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_Apq_Alarm', @level2type=N'COLUMN',@level2name=N'Severity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_Apq_Alarm', @level2type=N'COLUMN',@level2name=N'Titile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Log_Apq_Alarm', @level2type=N'COLUMN',@level2name=N'Msg'
GO
/****** Object:  Table [etl].[LoadCfg]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [etl].[LoadCfg](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EtlName] [nvarchar](256) NOT NULL,
	[SrcFullTableName] [nvarchar](512) NOT NULL,
	[DstFullTableName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Cycle] [int] NOT NULL,
	[LTime] [smalldatetime] NOT NULL,
	[PreLTime] [datetime] NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_LoadCfg] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LoadCfg:EtlName] ON [etl].[LoadCfg] 
(
	[EtlName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ETL配置名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'LoadCfg', @level2type=N'COLUMN',@level2name=N'EtlName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BcpIn到的表名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'LoadCfg', @level2type=N'COLUMN',@level2name=N'DstFullTableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'LoadCfg', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加载周期(分钟)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'LoadCfg', @level2type=N'COLUMN',@level2name=N'Cycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'首次加载时间' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'LoadCfg', @level2type=N'COLUMN',@level2name=N'LTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一次加载时间' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'LoadCfg', @level2type=N'COLUMN',@level2name=N'PreLTime'
GO
/****** Object:  Table [dbo].[LinkLogic]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkLogic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Src] [nvarchar](200) NULL,
	[Dst] [nvarchar](200) NULL,
	[SrcDep] [nvarchar](200) NULL,
	[DstDep] [nvarchar](200) NULL,
	[Approach] [nvarchar](200) NULL,
	[DstPort] [nvarchar](200) NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发起方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LinkLogic', @level2type=N'COLUMN',@level2name=N'Src'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LinkLogic', @level2type=N'COLUMN',@level2name=N'Dst'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发起方相关部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LinkLogic', @level2type=N'COLUMN',@level2name=N'SrcDep'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收方相关部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LinkLogic', @level2type=N'COLUMN',@level2name=N'DstDep'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用途/途径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LinkLogic', @level2type=N'COLUMN',@level2name=N'Approach'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收方端口(描述)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LinkLogic', @level2type=N'COLUMN',@level2name=N'DstPort'
GO
/****** Object:  StoredProcedure [dbo].[Job_updatestats]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-01
-- 描述: 更新所有数据库的统计信息
-- 示例:
EXEC dbo.Job_updatestats
-- =============================================
*/
CREATE PROC [dbo].[Job_updatestats]
AS 
SET NOCOUNT ON ;

DECLARE @ExMsg nvarchar(max), @Now1 datetime, @sql nvarchar(max), @sqlDB nvarchar(max);
SELECT @Now1 = getdate();

DECLARE @DBName sysname, @DB_objID int;
DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT name
  FROM master.sys.databases
 WHERE is_read_only = 0 AND database_id > 4
	AND state = 0;
	
OPEN @csr;
FETCH NEXT FROM @csr INTO @DBName;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @sqlDB = 'sp_updatestats';
	SELECT @sql = 'EXEC [' + @DBName + ']..sp_executesql @sqlDB'
	EXEC sp_executesql @sql, N'@sqlDB nvarchar(max)', @sqlDB = @sqlDB;

	FETCH NEXT FROM @csr INTO @DBName;
END

Quit:
CLOSE @csr;

DECLARE @Now2 datetime;
SELECT @Now2 = getdate();
GO
/****** Object:  StoredProcedure [dbo].[Pr_RebuildIdx]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-05
-- 功能: 索引重命名,重建/整理本库索引
-- 描述:
	重命名规则:
		主键:	PK_架构名_表名
		非主键:	IX_架构名_表名:列名{,...}
		
	如果碎片率低于30％用 选项REORGANIZE / INDEXDEFRAG,如果高于30％用 选项REBUILD / DBREINDEX
-- 示例:
EXEC dbo.Pr_RebuildIdx
-- =============================================
*/
CREATE PROC [dbo].[Pr_RebuildIdx]
AS 
SET NOCOUNT ON ;

DECLARE @objectid int,@indexid int,@partitioncount bigint,@schemaname sysname,@objectname sysname,@indexname sysname,@partitionnum bigint,@partitions bigint
	,@frag float,@is_unique tinyint,@is_primary_key tinyint
	,@idxNameOld nvarchar(776),@idxNameNew nvarchar(776)-- 新旧索引全名(架构.表名.索引名)
	;

DECLARE @sql nvarchar(max), @ColNames_idx nvarchar(max);
SELECT t.object_id,t.index_id,partition_number,avg_fragmentation_in_percent,ColNames=Convert(nvarchar(max),N'')
  INTO #Tmp_Idx_Rebuild
  FROM sys.dm_db_index_physical_stats(DB_ID(),NULL,NULL,NULL,'LIMITED') t
 WHERE t.index_id > 0 ;

UPDATE t
   SET t.ColNames = s.ColNames
  FROM #Tmp_Idx_Rebuild t INNER JOIN (
	SELECT * FROM (SELECT DISTINCT object_id,index_id FROM sys.index_columns) A OUTER APPLY( 
		SELECT ColNames= STUFF(REPLACE(REPLACE( 
			(SELECT c.name FROM sys.index_columns ic INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
			  WHERE ic.object_id = A.object_id AND ic.index_id = A.index_id
			  ORDER BY c.object_id,ic.index_id,ic.index_column_id
				FOR XML AUTO 
			), '<c name="', ','), '"/>', ''), 1, 1, '') 
		) r
	) s ON t.object_id = s.object_id AND t.index_id = s.index_id;

-- Declare the cursor for the list of partitions to be processed.
DECLARE @csr_partitions CURSOR
SET @csr_partitions = CURSOR FOR
SELECT object_id,index_id,partition_number,avg_fragmentation_in_percent,ColNames
  FROM #Tmp_Idx_Rebuild ;

-- Open the cursor.
OPEN @csr_partitions ;

-- Loop through the @csr_partitions.
FETCH NEXT FROM @csr_partitions INTO @objectid, @indexid, @partitionnum, @frag,@ColNames_idx;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @objectname = o.name,@schemaname = s.name
	  FROM sys.objects AS o JOIN sys.schemas AS s ON s.schema_id = o.schema_id
	 WHERE o.object_id = @objectid ;

	SELECT @indexname = name, @is_unique = is_unique,@is_primary_key = is_primary_key
		,@idxNameOld = '[' + @schemaname + '].[' + @objectname + '].[' + name + ']'
	  FROM sys.indexes
	 WHERE object_id = @objectid AND index_id = @indexid ;

	SELECT @partitioncount = count(*)
	  FROM sys.partitions
	 WHERE object_id = @objectid AND index_id = @indexid ;

	BEGIN TRY
		-- 重命名
		SELECT @idxNameNew = 'IX_' + @schemaname + '_' + @objectname + ':' + @ColNames_idx;
		IF(@is_primary_key = 1)
			SELECT @idxNameNew = 'PK_' + @schemaname + '_' + @objectname;
		EXEC sp_rename @idxNameOld,@idxNameNew,'INDEX';
		
		-- (10,30)	整理索引
		IF(@frag > 10.0 AND @frag < 30.0)
		BEGIN
			SELECT  @sql = 'ALTER INDEX [' + @idxNameNew + '] ON [' + @schemaname + '].[' + @objectname + '] REORGANIZE';
			IF(@partitioncount > 1)
				SELECT  @sql = @sql + ' PARTITION=' + CONVERT(nvarchar(10),@partitionnum) ;
		END
		-- [30,]	重建索引
		IF(@frag >= 30.0)
		BEGIN
			SELECT @sql = 'ALTER INDEX [' + @idxNameNew + '] ON [' + @schemaname + '].[' + @objectname + '] REBUILD';
			IF(@partitioncount)> 1 
				SELECT  @sql = @sql + ' PARTITION=' + CONVERT (nvarchar(10),@partitionnum) ;

			/*
			填充因子(百分比)
			1.低更改的表(读写比率为100:1以上)	100
			2.高更改的表(写超过读)				50-70
			3.读写各一半						80-90
			*/
			IF(@is_unique = 0)
				SELECT @sql = @sql + ' WITH(PAD_INDEX = ON, FILLFACTOR = 75)';
		END
		
		EXEC sp_executesql @sql ;
		PRINT 'Executed ' + @sql ;
	END TRY
	BEGIN CATCH
		PRINT '处理失败'
	END CATCH

	FETCH NEXT FROM @csr_partitions INTO @objectid, @indexid, @partitionnum, @frag,@ColNames_idx;
END
-- Close and deallocate the cursor.
CLOSE @csr_partitions

-- drop the temporary table
DROP TABLE #Tmp_Idx_Rebuild
GO
/****** Object:  Table [dbo].[StatConfig_Day]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatConfig_Day](
	[StatName] [nvarchar](50) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Detect] [nvarchar](4000) NULL,
	[STMT] [nvarchar](4000) NOT NULL,
	[StatTime] [datetime] NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[LastStatDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StatConfig_Day] PRIMARY KEY NONCLUSTERED 
(
	[StatName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day', @level2type=N'COLUMN',@level2name=N'StatName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计前检测,可使用以下预定义变量@StatDate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day', @level2type=N'COLUMN',@level2name=N'Detect'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计语句,可使用以下预定义变量@StatDate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day', @level2type=N'COLUMN',@level2name=N'STMT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'每日统计时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day', @level2type=N'COLUMN',@level2name=N'StatTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次统计日期(初始)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day', @level2type=N'COLUMN',@level2name=N'LastStatDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据统计配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatConfig_Day'
GO
/****** Object:  Table [etl].[StatCfg]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [etl].[StatCfg](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatName] [nvarchar](256) NOT NULL,
	[Detect] [nvarchar](4000) NULL,
	[STMT] [nvarchar](3500) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[FirstStartTime] [datetime] NOT NULL,
	[Cycle] [int] NOT NULL,
	[RTime] [smalldatetime] NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[PreRTime] [datetime] NULL,
 CONSTRAINT [PK_StatCfg] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计命名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'StatName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测数据到位情况,通过时返回1(参数:@StatName,@StartTime,@EndTime)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'Detect'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计存储过程或统计语句(参数:@StartTime,@EndTime)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'STMT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计参数:开始时间初始值' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'FirstStartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计周期(分钟)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'Cycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'首次统计执行时间' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'RTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一次统计时间' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg', @level2type=N'COLUMN',@level2name=N'PreRTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'不能保证准时执行,只能用于数据仓库' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'StatCfg'
GO
/****** Object:  Table [mgr].[SrvPwd]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mgr].[SrvPwd](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SrvID] [int] NOT NULL,
	[PwdType] [int] NOT NULL,
	[LoginName] [nvarchar](256) NOT NULL,
	[LoginPwd] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[SID] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_SrvPwd] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'SrvPwd', @level2type=N'COLUMN',@level2name=N'SrvID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码类型{1:OS,2:DB,3:FTP,4:Serv-U管理密码}' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'SrvPwd', @level2type=N'COLUMN',@level2name=N'PwdType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'SrvPwd', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录密码' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'SrvPwd', @level2type=N'COLUMN',@level2name=N'LoginPwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'SrvPwd', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  StoredProcedure [dbo].[Apq_WH_Init]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 维护作业初始化
-- 步骤: 1.禁用相关作业 2.启用维护作业
-- 示例:
EXEC dbo.Apq_WH_Init
-- =============================================
*/
CREATE PROC [dbo].[Apq_WH_Init]
AS
SET NOCOUNT ON ;

-- 1.禁用相关作业
--EXEC msdb..sp_update_job @job_name = '日志转移',@enabled = 0
DECLARE @job_name sysname
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT name FROM msdb..sysjobs WHERE name LIKE '%日志切换转移'

OPEN @csr;
FETCH NEXT FROM @csr INTO @job_name
WHILE(@@FETCH_STATUS = 0)
BEGIN
	EXEC msdb..sp_update_job @job_name = @job_name,@enabled = 0

	FETCH NEXT FROM @csr INTO @job_name
END
CLOSE @csr;

-- 2.启用维护作业
EXEC msdb..sp_update_job @job_name = '例行维护',@enabled = 1
GO
/****** Object:  StoredProcedure [dbo].[Apq_WH_End]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 结束维护作业
-- 步骤: 1.启用相关作业 2.禁用维护作业
-- 示例:
EXEC dbo.Apq_WH_End
-- =============================================
*/
CREATE PROC [dbo].[Apq_WH_End]
AS
SET NOCOUNT ON ;

-- 1.启用相关作业
--EXEC msdb..sp_update_job @job_name = '日志转移',@enabled = 1
DECLARE @job_name sysname
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT name FROM msdb..sysjobs WHERE name LIKE '%日志切换转移'

OPEN @csr;
FETCH NEXT FROM @csr INTO @job_name
WHILE(@@FETCH_STATUS = 0)
BEGIN
	EXEC msdb..sp_update_job @job_name = @job_name,@enabled = 1

	FETCH NEXT FROM @csr INTO @job_name
END
CLOSE @csr;

-- 2.禁用维护作业
EXEC msdb..sp_update_job @job_name = '例行维护',@enabled = 0
GO
/****** Object:  Table [mgr].[Server]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mgr].[Server](
	[SrvID] [int] NOT NULL,
	[SrvName] [nvarchar](256) NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[Usage] [nvarchar](500) NOT NULL,
	[OS] [nvarchar](500) NOT NULL,
	[IPWan1] [nvarchar](500) NOT NULL,
	[IPLan1] [nvarchar](500) NOT NULL,
	[RdpPort] [int] NULL,
	[SqlPort] [int] NULL,
	[FTPPort] [int] NULL,
	[IPWan2] [nvarchar](500) NOT NULL,
	[IPLan2] [nvarchar](500) NOT NULL,
	[IPWan3] [nvarchar](500) NOT NULL,
	[IPLan3] [nvarchar](500) NOT NULL,
	[IPWan4] [nvarchar](500) NOT NULL,
	[IPLan4] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Server] PRIMARY KEY NONCLUSTERED 
(
	[SrvID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器命名/编号' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'SrvName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置(机房,机柜等)' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'Location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主要用途' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'Usage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作系统(版本号)' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'OS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外网IP' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'IPWan1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内网IP' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'IPLan1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'远程端口' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'RdpPort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SQL Server端口' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'SqlPort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP端口' , @level0type=N'SCHEMA',@level0name=N'mgr', @level1type=N'TABLE',@level1name=N'Server', @level2type=N'COLUMN',@level2name=N'FTPPort'
GO
/****** Object:  Table [dbo].[RSrvConfig]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RSrvConfig](
	[ID] [int] NOT NULL,
	[ParentID] [int] NULL,
	[Name] [nvarchar](256) NOT NULL,
	[LSName] [nvarchar](512) NOT NULL,
	[UID] [nvarchar](256) NULL,
	[PwdC] [nvarchar](max) NULL,
	[Type] [int] NULL,
	[LSMaxTimes] [int] NOT NULL,
	[LSErrTimes] [int] NOT NULL,
	[LSState] [int] NOT NULL,
	[IPLan] [nvarchar](500) NULL,
	[IPWan1] [nvarchar](500) NOT NULL,
	[IPWan2] [nvarchar](500) NULL,
	[FTPPort] [int] NOT NULL,
	[FTPU] [nvarchar](50) NULL,
	[FTPPC] [nvarchar](max) NULL,
	[SqlPort] [int] NOT NULL,
	[Enabled] [tinyint] NOT NULL,
 CONSTRAINT [PK_RSrvConfig] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器命名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LinkServerName(别名)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'LSName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码(已加密)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'PwdC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器分类{1:区库,2:游戏库}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许该LinkServer断开的最大次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'LSMaxTimes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已经断开的次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'LSErrTimes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LinkServer状态{0:断开(连续失败N次),1:正常}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RSrvConfig', @level2type=N'COLUMN',@level2name=N'LSState'
GO
/****** Object:  Table [bak].[RestoreFromFolder]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bak].[RestoreFromFolder](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[LastFileName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[BakFolder] [nvarchar](4000) NOT NULL,
	[RestoreType] [int] NOT NULL,
	[RestoreFolder] [nvarchar](4000) NULL,
	[DB_HisNum] [int] NOT NULL,
	[Num_Full] [int] NOT NULL,
	[RunnerIDCfg] [int] NOT NULL,
	[RunnerIDRun] [int] NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_RestoreFromFolder] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库名' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'DBName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次还原使用的文件名' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'LastFileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份文件目录' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'BakFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还原类型{0x1:历史,0x2:备用}' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'RestoreType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还原目录' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'RestoreFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'历史库保留个数' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'DB_HisNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完整备份文件保留个数(同时保留具有基础完整备份文件的日志备份文件)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'RestoreFromFolder', @level2type=N'COLUMN',@level2name=N'Num_Full'
GO
/****** Object:  Table [dbo].[RDBUser]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RDBUser](
	[RDBUserID] [bigint] NOT NULL,
	[RDBID] [bigint] NULL,
	[DBUserName] [nvarchar](256) NOT NULL,
	[DBUserDesc] [nvarchar](max) NULL,
	[RDBLoginID] [bigint] NOT NULL,
 CONSTRAINT [PK_RDBUser] PRIMARY KEY NONCLUSTERED 
(
	[RDBUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名编号(ALL,可采用分级分段分配)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBUser', @level2type=N'COLUMN',@level2name=N'RDBUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'空表示通用用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBUser', @level2type=N'COLUMN',@level2name=N'RDBID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBUser', @level2type=N'COLUMN',@level2name=N'DBUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述(用户用途等)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBUser', @level2type=N'COLUMN',@level2name=N'DBUserDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应的登录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBUser', @level2type=N'COLUMN',@level2name=N'RDBLoginID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'远程数据库列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBUser'
GO
/****** Object:  Table [dbo].[RDBLogin]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RDBLogin](
	[RDBLoginID] [bigint] NOT NULL,
	[RSrvID] [int] NULL,
	[DBLoginName] [nvarchar](256) NOT NULL,
	[DBLoginDesc] [nvarchar](max) NULL,
	[SID] [varbinary](85) NULL,
	[LoginPwdC] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RDBLogin] PRIMARY KEY NONCLUSTERED 
(
	[RDBLoginID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名编号(ALL,可采用分级分段分配)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin', @level2type=N'COLUMN',@level2name=N'RDBLoginID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'空表示通用登录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin', @level2type=N'COLUMN',@level2name=N'RSrvID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin', @level2type=N'COLUMN',@level2name=N'DBLoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述(登录用途,使用者等)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin', @level2type=N'COLUMN',@level2name=N'DBLoginDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录SID(NULL表示windows登录)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin', @level2type=N'COLUMN',@level2name=N'SID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin', @level2type=N'COLUMN',@level2name=N'LoginPwdC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'远程数据库列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBLogin'
GO
/****** Object:  Table [dbo].[RDBConfig]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RDBConfig](
	[RDBID] [bigint] NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[RDBDesc] [nvarchar](max) NULL,
	[RDBType] [int] NOT NULL,
	[PLevel] [int] NOT NULL,
	[GLevel] [int] NOT NULL,
	[SrvID] [int] NOT NULL,
	[GameID] [int] NOT NULL,
	[Enabled] [tinyint] NOT NULL,
 CONSTRAINT [PK_RDBConfig] PRIMARY KEY NONCLUSTERED 
(
	[RDBID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'远程数据库编号(ALL,可采用分级分段分配)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'RDBID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'DBName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库主要功能描述(主要功能名称,如''1-2-5世界名称'',''1-0-中心库''等)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'RDBDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库类型{0:未分类,1:通行证,2:游戏用户中心,3:游戏数据中心,4:游戏日志中心,5:游戏库,6:游戏库日志}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'RDBType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库层级(ALL)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'PLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库层级(游戏内)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'GLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库服务器编号(ALL)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'SrvID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'游戏编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig', @level2type=N'COLUMN',@level2name=N'GameID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'远程数据库列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RDBConfig'
GO
/****** Object:  Table [dic].[PwdType]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dic].[PwdType](
	[PwdType] [int] NOT NULL,
	[Description] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertIP4_VarBinary]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-28
-- 描述: 将IP4串转化为 varbinary(max)
-- 示例:
SELECT dbo.Apq_ConvertIP4_VarBinary('255.255.255.255');
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertIP4_VarBinary](
	@IP	varchar(max)
)RETURNS varbinary(max)
AS
BEGIN
	SELECT @IP = LTRIM(RTRIM(@IP));

	DECLARE @Return varbinary(Max)
		,@Len int		-- 字符数
		,@ib int		-- 当前解析起始位置
		,@ie int		-- 当前解析结束位置
		,@i int
		;
	SELECT @Return = 0x
		,@Len = LEN(@IP)
		,@ie = 0
		,@i = 1
		;

	IF(@Len < 7) RETURN @Return;

	WHILE(@i <= 4)
	BEGIN
		SELECT	@ib = @ie + 1;
		SELECT	@ie = CHARINDEX('.', @IP, @ib);
		IF(@ie = 0)
		BEGIN
			SELECT	@ie = @Len + 1;
		END
		
		IF(@ib >= @ie) BREAK;

		SELECT	@Return = ISNULL(@Return, 0x) + Convert(binary(1), Convert(int, SUBSTRING(@IP, @ib, @ie - @ib)));

		SELECT	@i = @i + 1;
	END

	RETURN @Return;
END
GO
/****** Object:  StoredProcedure [dbo].[Apq_RandomString]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-03-31
-- 描述: 生成随机字符串
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max), @SimpleChars nvarchar(max);
SELECT @SimpleChars = 'ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy0123456789';
SELECT @SimpleChars = 'ABCDEFGHJLMNRTWY0123456789';
EXEC @rtn = dbo.Apq_RandomString @ExMsg out, @SimpleChars, 50000, 8, 1, 1;
SELECT @rtn, @ExMsg;
-- =============================================
*/
CREATE PROC [dbo].[Apq_RandomString]
	 @ExMsg nvarchar(max) out
	 
	,@Chars		nvarchar(max)='ABCDEFGHJLMNRTWYacdefhijkmnprtuvwxy0123456789'	-- 字符集(一般不重复)
	,@Count		int	= 10		-- 个数
	,@Length	int = 16		-- 长度
	,@Repeat	tinyint = 1		-- 每个串中是否允许重复
	,@Distinct	tinyint	= 0		-- 相互之间是否唯一(为1时需要保证 排列数 >= @Count)
AS
SET NOCOUNT ON;

DECLARE @Now datetime, @i int, @l int, @chrs nvarchar(max)
	,@s nvarchar(max)	-- 当前已生成的串
	,@p int			-- 当前选中的位置(@chrs中)
	--,@pl int			-- 上次选中的位置(@chrs中)
	,@c nvarchar(1)	-- 上次选中的字符
	;
SELECT @Now = getdate();

CREATE TABLE #t(
	s	nvarchar(max)
);
IF(@Distinct = 1)
BEGIN
	CREATE INDEX [IX_#t:s] ON #t(s)
END

SELECT @i = 0;
WHILE(@i < @Count)
BEGIN
	SELECT @l = 0, @p = 0, @chrs = @Chars, @s = '', @c='';
	WHILE(@l < @Length)
	BEGIN
		IF(@Repeat = 0 AND LEN(@c) > 0)
		BEGIN
			-- 去除上次选中的字符
			SELECT @chrs = REPLACE(@chrs, @c,'');
		END
		
		SELECT @p = Convert(int,RAND()*LEN(@chrs)) + 1;
		SELECT @c = SUBSTRING(@chrs, @p,1);
		SELECT @s = @s + @c;
	
		SELECT @l = @l + 1;
	END
	
	IF(@Distinct = 1 AND EXISTS(SELECT 1 FROM #t WHERE s = @s))
	BEGIN
		-- 跳过重复
		CONTINUE;
	END
	
	INSERT #t(s) SELECT @s;
	SELECT @i = @i + 1;
END

SELECT * FROM #t;
 
TRUNCATE TABLE #t;
DROP TABLE #t;

SELECT @ExMsg = '生成成功';
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_Process_KillDead]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_Process_KillDead]
	 @PName	nvarchar(256) = 'ftp.exe'	-- 进程名
	,@MaxRunMinutes int = 120
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-20
-- 描述: 结束死掉的进程(运行时间超过指定时长(分钟),默认120)
-- 示例:
EXEC dbo.Apq_Process_KillDead 'cmd.exe';
-- =============================================
*/
SET NOCOUNT ON;

SELECT @PName = ISNULL(@PName,'ftp.exe')
	,@MaxRunMinutes = ISNULL(@MaxRunMinutes,120)
	;

DECLARE @rtn int, @SPBeginTime datetime;
SELECT @SPBeginTime=GetDate();

DECLARE @cmd nvarchar(4000)
	;
CREATE TABLE #cmd(
	s	nvarchar(4000)
);

SELECT @cmd = 'wmic process where (name="' + @PName + '") get CreationDate,ProcessId';
EXEC @rtn = xp_cmdshell @cmd;	-- 先运行一次防止未安装
INSERT #cmd EXEC @rtn = xp_cmdshell @cmd;

DECLARE @s nvarchar(4000),@PID int,@StartTime datetime;
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT s FROM #cmd WHERE len(s) > 27 AND Left(s,12) <> 'CreationDate'

OPEN @csr;
FETCH NEXT FROM @csr INTO @s;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @PID = LEFT(Right(@s,12),10),@StartTime = LEFT(@s,8) + ' ' + Substring(@s,9,2)+':' + Substring(@s,11,2)+':'+Substring(@s,13,2);
	IF(datediff(n,@StartTime,@SPBeginTime) > @MaxRunMinutes)
	BEGIN
		SELECT @cmd = 'wmic process where (ProcessId=' + Convert(nvarchar,@PID) + ') delete';
		EXEC xp_cmdshell @cmd;
	END

	FETCH NEXT FROM @csr INTO @s;
END
CLOSE @csr;

DROP TABLE #cmd;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_RenameDefault]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-06
-- 功能: 重建本库默认值(命名规范化)
-- 描述:
	重命名规则: DF_表名_列名
-- 示例:
EXEC dbo.Apq_RenameDefault
-- =============================================
*/
CREATE PROC [dbo].[Apq_RenameDefault]
AS 
DECLARE @SchemaName nvarchar(128)
DECLARE @TableName nvarchar(128)
DECLARE @ColumnName nvarchar(128)
DECLARE @Defname nvarchar(128)
DECLARE @Definition nvarchar(128)
DECLARE @sql nvarchar(max)

DECLARE @csr_Constraints CURSOR ;
SET @csr_Constraints = CURSOR STATIC FOR 
SELECT  SCHEMA_NAME(o.schema_id),o.Name,c.Name,so.name,so.definition 
  FROM  sys.default_constraints so INNER JOIN sys.objects o  ON o.object_id = so.parent_object_id
								   INNER JOIN sys.columns c  ON C.DEFAULT_OBJECT_ID =so.object_id;

OPEN @csr_Constraints ;

FETCH NEXT FROM @csr_Constraints INTO @SchemaName,@TableName,@ColumnName,@Defname,@Definition ;
WHILE ( @@FETCH_STATUS = 0 ) 
BEGIN
    SELECT  @sql = 'ALTER TABLE [' + @SchemaName + '].[' + @TableName + '] DROP CONSTRAINT [' + @Defname + ']'
    EXEC sp_executesql @sql

    SELECT  @sql = 'ALTER TABLE [' + @SchemaName + '].[' + @TableName + '] ADD CONSTRAINT DF_' + @TableName + '_' + @ColumnName + ' DEFAULT '
            + @Definition + ' FOR [' + @ColumnName + ']'
    EXEC sp_executesql @sql

    FETCH NEXT FROM @csr_Constraints INTO @SchemaName,@TableName,@ColumnName,@Defname,@Definition ;
END

CLOSE @csr_Constraints
RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[Apq_RebuildIdx]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-05
-- 功能: 索引重命名,重建/整理本库索引
-- 描述:
	重命名规则:
		主键:	PK_表名
		非主键:	IX_表名:列名{,...}
		
	如果碎片率低于30％用 选项REORGANIZE / INDEXDEFRAG,如果高于30％用 选项REBUILD / DBREINDEX
-- 示例:
EXEC dbo.Apq_RebuildIdx
-- =============================================
*/
CREATE PROC [dbo].[Apq_RebuildIdx]
AS 
SET NOCOUNT ON ;

DECLARE @objectid int,@indexid int,@partitioncount bigint,@schemaname sysname,@objectname sysname,@indexname sysname,@partitionnum bigint,@partitions bigint
	,@frag float,@is_unique tinyint,@is_primary_key tinyint
	,@idxNameOld nvarchar(776),@idxNameNew nvarchar(776)-- 新旧索引全名(架构.表名.索引名)
	;

DECLARE @sql nvarchar(max), @ColNames_idx nvarchar(max);
SELECT t.object_id,t.index_id,partition_number,avg_fragmentation_in_percent,ColNames=Convert(nvarchar(max),N'')
  INTO #Apq_RebuildIdx
  FROM sys.dm_db_index_physical_stats(DB_ID(),NULL,NULL,NULL,'LIMITED') t
 WHERE t.index_id > 0 ;

UPDATE t
   SET t.ColNames = s.ColNames
  FROM #Apq_RebuildIdx t INNER JOIN (
	SELECT * FROM (SELECT DISTINCT object_id,index_id FROM sys.index_columns) A OUTER APPLY( 
		SELECT ColNames= STUFF(REPLACE(REPLACE( 
			(SELECT c.name FROM sys.index_columns ic INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
			  WHERE ic.object_id = A.object_id AND ic.index_id = A.index_id
			  ORDER BY c.object_id,ic.index_id,ic.index_column_id
				FOR XML AUTO 
			), '<c name="', ','), '"/>', ''), 1, 1, '') 
		) r
	) s ON t.object_id = s.object_id AND t.index_id = s.index_id;

-- Declare the cursor for the list of partitions to be processed.
DECLARE @csr_partitions CURSOR
SET @csr_partitions = CURSOR FOR
SELECT object_id,index_id,partition_number,avg_fragmentation_in_percent,ColNames
  FROM #Apq_RebuildIdx ;

-- Open the cursor.
OPEN @csr_partitions ;

-- Loop through the @csr_partitions.
FETCH NEXT FROM @csr_partitions INTO @objectid, @indexid, @partitionnum, @frag,@ColNames_idx;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @objectname = o.name,@schemaname = s.name
	  FROM sys.objects AS o JOIN sys.schemas AS s ON s.schema_id = o.schema_id
	 WHERE o.object_id = @objectid ;

	SELECT @indexname = name, @is_unique = is_unique,@is_primary_key = is_primary_key
		,@idxNameOld = '[' + @schemaname + '].[' + @objectname + '].[' + name + ']'
	  FROM sys.indexes
	 WHERE object_id = @objectid AND index_id = @indexid ;

	SELECT @partitioncount = count(*)
	  FROM sys.partitions
	 WHERE object_id = @objectid AND index_id = @indexid ;

	BEGIN TRY
		-- 重命名
		SELECT @idxNameNew = 'IX_' + @objectname + ':' + @ColNames_idx;
		IF(@is_primary_key = 1)
			SELECT @idxNameNew = 'PK_' + @objectname;
		EXEC sp_rename @idxNameOld,@idxNameNew,'INDEX';
		
		-- (10,30)	整理索引
		IF(@frag > 10.0 AND @frag < 30.0)
		BEGIN
			SELECT  @sql = 'ALTER INDEX [' + @idxNameNew + '] ON [' + @schemaname + '].[' + @objectname + '] REORGANIZE';
			IF(@partitioncount > 1)
				SELECT  @sql = @sql + ' PARTITION=' + CONVERT(nvarchar(10),@partitionnum) ;
		END
		-- [30,]	重建索引
		IF(@frag >= 30.0)
		BEGIN
			SELECT @sql = 'ALTER INDEX [' + @idxNameNew + '] ON [' + @schemaname + '].[' + @objectname + '] REBUILD';
			IF(@partitioncount)> 1 
				SELECT  @sql = @sql + ' PARTITION=' + CONVERT (nvarchar(10),@partitionnum) ;

			/*
			填充因子(百分比)
			1.低更改的表(读写比率为100:1以上)	100
			2.高更改的表(写超过读)				50-70
			3.读写各一半						80-90
			*/
			IF(@is_unique = 0)
				SELECT @sql = @sql + ' WITH(PAD_INDEX = ON, FILLFACTOR = 75)';
		END
		
		EXEC sp_executesql @sql ;
		PRINT 'Executed ' + @sql ;
	END TRY
	BEGIN CATCH
		PRINT '处理失败'
	END CATCH

	FETCH NEXT FROM @csr_partitions INTO @objectid, @indexid, @partitionnum, @frag,@ColNames_idx;
END
-- Close and deallocate the cursor.
CLOSE @csr_partitions

-- drop the temporary table
DROP TABLE #Apq_RebuildIdx
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_NewID]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Apq_NewID]()RETURNS int AS BEGIN RETURN 0 END
GO
/****** Object:  StoredProcedure [dbo].[Apq_Login_StatCount]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-14
-- 描述: 统计指定登录名的连接数(未指定时为所有登录名)
-- 示例:
EXEC dbo.Apq_Login_StatCount N'LoginName'
-- =============================================
*/
CREATE PROC [dbo].[Apq_Login_StatCount]
    @LoginName nvarchar(256)
AS 
SET NOCOUNT ON ;

IF ( Len(@LoginName) < 1 ) 
    SELECT  @LoginName = NULL ;

DECLARE @stmt nvarchar(max)
   ,@pSession CURSOR ;

CREATE TABLE #t_who (
     spid smallint
    ,ecid smallint
    ,status nvarchar(30)
    ,loginame nvarchar(128)
    ,hostname nvarchar(128)
    ,blk nvarchar(5)
    ,DBName nvarchar(128)
    ,cmd nvarchar(16)
    ,request_id int
    ) ;

INSERT  #t_who
        EXEC sp_who @LoginName
IF(@@ROWCOUNT = 0) RETURN;

SELECT loginame,count(spid)
  FROM #t_who
 GROUP BY loginame
GO
/****** Object:  StoredProcedure [dbo].[Apq_KILL_Login]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 断开某用户数据库的所有连接
-- 示例:
EXEC dbo.Apq_KILL_Login N'LoginName'
-- =============================================
*/
CREATE PROC [dbo].[Apq_KILL_Login]
    @LoginName nvarchar(256)
AS 
SET NOCOUNT ON ;

DECLARE @LoginID int

IF ( Len(@LoginName) < 1 ) 
    SELECT  @LoginName = NULL ;

SELECT  @LoginID = principal_id
FROM    master.sys.login_token
WHERE   name = @LoginName ;
IF ( @LoginID IS NULL
     OR @LoginID < 4
   ) 
    RETURN -2 ;

DECLARE @stmt nvarchar(max)
   ,@pSession CURSOR ;

CREATE TABLE #t_who (
     spid smallint
    ,ecid smallint
    ,status nvarchar(30)
    ,loginame nvarchar(128)
    ,hostname nvarchar(128)
    ,blk nvarchar(5)
    ,DBName nvarchar(128)
    ,cmd nvarchar(16)
    ,request_id int
    ) ;

INSERT  #t_who
        EXEC sp_who @LoginName

SET @pSession = CURSOR FOR
SELECT N'KILL ' + CAST(spid AS nvarchar) FROM #t_who;

OPEN @pSession ;
FETCH NEXT FROM @pSession INTO @stmt ;
WHILE ( @@FETCH_STATUS = 0 ) 
    BEGIN
        EXEC sp_executesql @stmt ;

        FETCH NEXT FROM @pSession INTO @stmt ;
    END
CLOSE @pSession ;
GO
/****** Object:  StoredProcedure [dbo].[Apq_KILL_DB]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-20
-- 描述: 断开某用户数据库的所有连接
-- 示例:
EXEC dbo.Apq_KILL_DB N'DBName'
-- =============================================
*/
CREATE PROC [dbo].[Apq_KILL_DB]
	@DBName	nvarchar(256)
AS
SET NOCOUNT ON;

DECLARE	@stmt nvarchar(max), @pSession cursor;

CREATE TABLE #sp_who(
	spid	smallint,
	ecid	smallint,
	status	nvarchar(30),
	loginame	nvarchar(128),
	hostname	nvarchar(128),
	blk		int,
	dbname	nvarchar(128),
	cmd		nvarchar(16),
	request_id	int
);

INSERT #sp_who EXEC sp_who;
SELECT * FROM #sp_who
 WHERE dbname = @DBName;

SET	@pSession = CURSOR FOR
SELECT DISTINCT N'KILL ' + CAST(spid AS nvarchar)
  FROM #sp_who
 WHERE dbname = @DBName;

OPEN @pSession;
FETCH NEXT FROM @pSession INTO @stmt;
WHILE( @@FETCH_STATUS = 0 )
BEGIN
	EXEC sp_executesql @stmt;

	FETCH NEXT FROM @pSession INTO @stmt;
END
CLOSE @pSession;

DROP TABLE #sp_who;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_IsNumeric]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-18
-- 描述: 判断字符串是否全为数字
-- 示例:
SELECT dbo.Apq_IsNumeric('30000229');
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_IsNumeric](
	@s	nvarchar(50)
)RETURNS tinyint
AS
BEGIN
	DECLARE @rtn tinyint, @i int, @c nvarchar(1);

	SELECT @i = 1;
	WHILE(@i <= LEN(@s))
	BEGIN
		SELECT @c = SubString(@s, @i, 1);
		IF(@c < '0' OR '9' < @c)
		BEGIN
			RETURN 0;
		END

		SELECT @i = @i + 1;
	END

	RETURN 1;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_IP_GetCityID]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-11-19
-- 描述: 根据IP获取所在城市ID
-- 输入: 二进制IP
-- 返回: 城市ID,未知时返回-10
-- 示例:
SELECT dbo.Apq_IP_GetCityID(0x0000000000000000000000000029F4475B);
-- =============================================
-10: 未知
*/
CREATE FUNCTION [dbo].[Apq_IP_GetCityID](
	@binIP	binary(16)
)RETURNS bigint
AS
BEGIN
	DECLARE @CityID bigint
	SELECT @CityID = -10;	-- 未知
	SELECT TOP 1 @CityID = CityID FROM Apq_IP WHERE @binIP BETWEEN IPBegin AND IPEnd ORDER BY _Time DESC;
	RETURN @CityID;
END
GO
/****** Object:  Table [dbo].[Apq_Ext]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apq_Ext](
	[ID1] [bigint] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](256) NOT NULL,
	[ID] [bigint] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Apq_Ext] PRIMARY KEY NONCLUSTERED 
(
	[ID1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Ext', @level2type=N'COLUMN',@level2name=N'TableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Ext', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Ext', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Ext', @level2type=N'COLUMN',@level2name=N'Value'
GO
/****** Object:  StoredProcedure [dbo].[Apq_DropIndex]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-04-29
-- 描述: 删除某个表的全部索引并输出每个索引的创建语句
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max), @sql_Create nvarchar(max), @sql_Drop nvarchar(max);
EXEC @rtn = dbo.Apq_DropIndex @ExMsg out, 'dtxc', 'TaskVote_Log', @sql_Create out, @sql_Drop out, 0;
SELECT @rtn, @ExMsg, @sql_Create, @sql_Drop;
-- =============================================
*/
CREATE PROC [dbo].[Apq_DropIndex]
	@ExMsg nvarchar(max) out
	
	,@Schema_Name	nvarchar(512)
	,@Table_Name	nvarchar(512)
	,@sql_Create	nvarchar(max) = '' out
	,@sql_Drop		nvarchar(max) = '' out
	,@DoDrop		tinyint = 1
AS
SET NOCOUNT ON;

SELECT @sql_Create = '', @sql_Drop = '';

DECLARE @sql nvarchar(max), @FullTableName nvarchar(640);
SELECT @sql = '', @FullTableName = @Schema_Name + '.' + @Table_Name;

CREATE TABLE #Apq_DropIndex_t_helpindex(
	index_name	nvarchar(512),
	index_description	varchar(210),
	index_keys	nvarchar(2078),
	IsPS tinyint,
	PSorFG nvarchar(512),
	ignore_dup_key tinyint,
	fill_factor	tinyint,
	IsPrimaryKey	int,
	IsClustered	int,
	IsUnique	int,
	Sql_CREATE	nvarchar(max),
	Sql_DROP	nvarchar(max)
);

INSERT #Apq_DropIndex_t_helpindex(index_name,index_description,index_keys) EXEC sp_helpindex @FullTableName;
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = '[' + REPLACE(index_keys,',','],[');	-- (,)前后加括号,最前面加([)
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = REPLACE(index_keys,'[ ','[');			-- 去掉([)后面的空格
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = REPLACE(index_keys,'(-)','] DESC') + ']';	-- 装(-)替换为排序方式,最后加(])
UPDATE #Apq_DropIndex_t_helpindex SET index_keys = REPLACE(index_keys,'] DESC]','] DESC');	-- 去掉排序方式后多余的(])

UPDATE #Apq_DropIndex_t_helpindex
   SET IsPrimaryKey = OBJECTPROPERTY(OBJECT_ID(@Schema_Name+'.['+index_name+']'),'IsPrimaryKey')
	,IsClustered = INDEXPROPERTY(OBJECT_ID(@FullTableName), index_name, 'IsClustered')
	,IsUnique = INDEXPROPERTY(OBJECT_ID(@FullTableName), index_name, 'IsUnique');

-- 查找索引属性
UPDATE t
   SET t.IsPS = CASE dsi.type WHEN 'FG' THEN 0 WHEN 'PS' THEN 1 ELSE 2 END, t.PSorFG = dsi.name, t.ignore_dup_key = i.ignore_dup_key, t.fill_factor = i.fill_factor
  FROM #Apq_DropIndex_t_helpindex t INNER JOIN sys.indexes i ON t.index_name = i.name
	LEFT JOIN sys.data_spaces AS dsi ON dsi.data_space_id = i.data_space_id
 WHERE i.index_id > 0 and i.is_hypothetical = 0
	AND i.object_id=object_id(@FullTableName);

-- 查找传入分区方案的列
CREATE TABLE #Apq_DropIndex_t_index_PSColumn(
	index_name	nvarchar(512),
	partition_ordinal tinyint,
	name nvarchar(512)
);
INSERT #Apq_DropIndex_t_index_PSColumn ( index_name,partition_ordinal,name )
SELECT index_name = i.name,ic.partition_ordinal,c.name
  FROM sys.indexes AS i
	INNER JOIN sys.index_columns ic ON (ic.partition_ordinal > 0) AND (ic.index_id=CAST(i.index_id AS int) AND ic.object_id=CAST(i.object_id AS int))
	INNER JOIN sys.columns c ON c.object_id = ic.object_id and c.column_id = ic.column_id
 WHERE i.index_id > 0 and i.is_hypothetical = 0
	AND (i.object_id=object_id(@FullTableName))

UPDATE t
   SET t.Sql_DROP = 'ALTER TABLE ' + @FullTableName + ' DROP CONSTRAINT [' + index_name + ']'
	,t.Sql_CREATE = 'ALTER TABLE ' + @FullTableName + ' ADD CONSTRAINT [' + index_name + '] PRIMARY KEY ' + CASE IsClustered WHEN 1 THEN '' ELSE 'NON' END + 'CLUSTERED(' + index_keys + ') ON '
		+ ISNULL(t.PSorFG,'PRIMARY') + CASE t.IsPS WHEN 1 THEN '('+ll.PSCols+')' ELSE '' END
  FROM #Apq_DropIndex_t_helpindex t OUTER APPLY (
	SELECT PSCols = STUFF(REPLACE(REPLACE(
		(SELECT name FROM #Apq_DropIndex_t_index_PSColumn ld WHERE ld.index_name = t.index_name FOR XML AUTO),
		'<ld name="', ','), '"/>', ''), 1,1,''
	)) ll
 WHERE IsPrimaryKey = 1;

UPDATE t
   SET t.Sql_DROP = 'DROP INDEX ' + @FullTableName + '.[' + index_name + ']'
	,t.Sql_CREATE = 'CREATE ' + CASE IsClustered WHEN 1 THEN '' ELSE 'NON' END + 'CLUSTERED INDEX [' + index_name + '] ON ' + @FullTableName + '(' + index_keys + ') ON '
		+ ISNULL(t.PSorFG,'PRIMARY') + CASE t.IsPS WHEN 1 THEN '('+ll.PSCols+')' ELSE '' END
  FROM #Apq_DropIndex_t_helpindex t OUTER APPLY (
	SELECT PSCols = STUFF(REPLACE(REPLACE(
		(SELECT name FROM #Apq_DropIndex_t_index_PSColumn ld WHERE ld.index_name = t.index_name FOR XML AUTO),
		'<ld name="', ','), '"/>', ''), 1,1,''
	)) ll
 WHERE Sql_DROP IS NULL;

SELECT @sql_Drop = @sql_Drop + Sql_DROP + '; '
  FROM #Apq_DropIndex_t_helpindex
 ORDER BY IsClustered;

SELECT @sql_Create = @sql_Create + Sql_CREATE + '; '
  FROM #Apq_DropIndex_t_helpindex
 ORDER BY IsClustered DESC;

TRUNCATE TABLE #Apq_DropIndex_t_helpindex;
TRUNCATE TABLE #Apq_DropIndex_t_index_PSColumn;
DROP TABLE #Apq_DropIndex_t_helpindex;
DROP TABLE #Apq_DropIndex_t_index_PSColumn;

IF(@DoDrop = 1)
BEGIN
	EXEC sp_executesql @sql_Drop;
END

SELECT @ExMsg = '操作成功';
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_Dependent_List]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_Dependent_List]
   @object_id int
AS 
SET NOCOUNT ON ;

DECLARE @SchemaName sysname, @ObjName sysname;
SELECT @SchemaName = schema_name(uid), @ObjName = object_name(id)
  FROM sys.sysobjects
 WHERE id = @object_id;
IF(@@ROWCOUNT = 0) RETURN -1;

CREATE TABLE #Apq_Dependent_List1(
	obj_id	int
);

INSERT #Apq_Dependent_List1
SELECT object_id(ROUTINE_SCHEMA+'.'+ROUTINE_NAME)
  FROM INFORMATION_SCHEMA.ROUTINES
 WHERE Charindex(@ObjName, ROUTINE_DEFINITION) > 0
 
INSERT #Apq_Dependent_List1
SELECT o.id
  FROM sys.syscomments cmt INNER JOIN sys.sysobjects o ON cmt.id = o.id
 WHERE Charindex(@ObjName, cmt.text) > 0

SELECT DISTINCT SchemaName=schema_name(uid),name,xtype
  FROM #Apq_Dependent_List1 t INNER JOIN sys.sysobjects o ON t.obj_id = o.id

TRUNCATE TABLE #Apq_Dependent_List1;
DROP TABLE #Apq_Dependent_List1;
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 显示(可能)依赖于指定对象的对象
-- 作者: 黄宗银
-- 日期: 2010-05-11
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max), @object_id int;
SELECT @object_id = object_id(''dbo.aa'')
EXEC @rtn = dbo.Apq_Dependent_List @object_id;
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'Apq_Dependent_List'
GO
/****** Object:  Table [dbo].[Apq_ID]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apq_ID](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Crt] [bigint] NOT NULL,
	[Limit] [bigint] NOT NULL,
	[Init] [bigint] NOT NULL,
	[Inc] [bigint] NOT NULL,
	[State] [int] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[_InTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Apq_ID] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = ON, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'说明2', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'Crt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大值(Bigint最大值-Int最大值)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'Limit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'初始值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'Init'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'增量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'Inc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态{0:正常,1:已使用}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自定义ID自增控制表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'说明2', @value=N'随便写写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_ID'
GO
/****** Object:  StoredProcedure [dbo].[Apq_Gen_Ins]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-15
-- 描述: 计算INSERT语句
-- 示例:
EXEC dbo.Apq_Gen_Ins 'dbo.Apq_Ext',1
-- =============================================
*/
CREATE PROC [dbo].[Apq_Gen_Ins]
   @TableName nvarchar(512) = NULL	-- 表全名(架构.表名)
  ,@HasID tinyint = 0	-- 是否包含自增列
  ,@sqlWhere nvarchar(max) = NULL
AS 
SET NOCOUNT ON ;

IF(@HasID IS NULL) SELECT @HasID = 0;
IF(Charindex('.',@TableName) < 2) SELECT @TableName = '[dbo].' + @TableName;
IF(LEN(@sqlWhere) < 2) SELECT @sqlWhere = NULL;

DECLARE @objID int, @FullName nvarchar(512);
SELECT @objID = object_id(@TableName);

DECLARE @sql nvarchar(max),@sqlValues nvarchar(max);
DECLARE @DBName sysname;
DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT object_id, '[' + schema_name(schema_id) + '].[' + name + ']'
  FROM sys.objects
 WHERE @objID IS NULL OR object_id = @objID

CREATE TABLE #sql(
	ID bigint IDENTITY(1,1),
	sql nvarchar(max)
);
CREATE TABLE #sqlT(
	ID bigint IDENTITY(1,1),
	sql nvarchar(max)
);

OPEN @csr;
FETCH NEXT FROM @csr INTO @objID,@TableName;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	TRUNCATE TABLE #sqlT
	IF(@HasID = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.columns WHERE object_id = @objID AND is_identity = 1))
	BEGIN
		INSERT #sqlT ( sql )
		SELECT 'SET IDENTITY_INSERT ' + @TableName + ' ON'
	END
	
	SELECT @sql ='('
	SELECT @sqlValues = 'VALUES (''+'
	SELECT @sqlValues = @sqlValues + cols + ' + '','' + ' ,@sql = @sql + '[' + name + '],'
	  FROM (SELECT name,Cols = CASE
				WHEN system_type_id IN (48,52,56,59,60,62,104,106,108,122,127)  --如果是数值型或MOENY型     
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE ' + 'Convert(nvarchar,['+ name + '])'+' END'
				WHEN system_type_id IN (165, 173) -- binary varbinary
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE ''0x'' + ' +  'dbo.Apq_ConvertVarBinary_HexStr([' + name + '])' + ' END'
				WHEN system_type_id IN (58,61) --如果是datetime或smalldatetime类型
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+''''''''' + ' + 'Convert(varchar,['+ name +'],121)'+ '+'''''''''+' END'
				WHEN system_type_id IN (167,175) --如果是varchar类型
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+''''''''' + ' + 'REPLACE(['+ name+'],'''''''','''''''''''')' + '+'''''''''+' END'
				WHEN system_type_id IN (231,239) --如果是nvarchar类型
					 THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+'''N'''''' + ' + 'REPLACE(['+ name+'],'''''''','''''''''''')' + '+'''''''''+' END'
                /*
                WHEN system_type_id IN (175) --如果是CHAR类型
                     THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+''''''''' + ' + 'CAST(REPLACE('+ name+','''''''','''''''''''') AS char(' + cast(max_length as varchar)  + '))+'''''''''+' END'
                WHEN system_type_id IN (239) --如果是NCHAR类型
                     THEN 'CASE WHEN '+ name +' IS NULL THEN ''NULL'' ELSE '+'''N'''''' + ' + 'CAST(REPLACE('+ name+','''''''','''''''''''') AS char(' + cast(max_length as varchar)  + '))+'''''''''+' END'
				*/
				ELSE '''NULL'''
			END
	  FROM sys.columns
	 WHERE object_id = object_id(@TableName) AND (@HasID <> 0 OR is_identity = 0)
	) T
	SELECT @sql ='SELECT ''INSERT INTO '+ @TableName + left(@sql,len(@sql)-1)+') ' + left(@sqlValues,len(@sqlValues)-4) + ')'' FROM '+@TableName + '(NOLOCK)'
	IF(LEN(@sqlWhere) > 1)
		SELECT @sql = @sql + ' WHERE (' + @sqlWhere + ')';
	INSERT #sqlT ( sql )
	EXEC sp_executesql @sql

	IF(@HasID = 1 AND EXISTS(SELECT TOP 1 1 FROM sys.columns WHERE object_id = @objID AND is_identity = 1))
	BEGIN
		INSERT #sqlT ( sql )
		SELECT 'SET IDENTITY_INSERT ' + @TableName + ' OFF'
	END
	
	INSERT #sql ( sql )
	SELECT sql FROM #sqlT ORDER BY ID;

	FETCH NEXT FROM @csr INTO @objID,@TableName;
END
CLOSE @csr;

SELECT sql FROM #sql ORDER BY ID;

TRUNCATE TABLE #sqlT
DROP TABLE #sqlT
TRUNCATE TABLE #sql
DROP TABLE #sql
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertInt_TimeString]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 将整数表示的时间 转换为 24小时时间字符串,格式[hh:mi:ss]
-- 示例:
SELECT dbo.Apq_ConvertInt_TimeString('310')
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertInt_TimeString]
(
	@nTime	int
)
RETURNS nvarchar(8)
AS
BEGIN
	DECLARE @Return nvarchar(8), @strTime nvarchar(8), @p int;
    SELECT @strTime = RIGHT('000000'+Convert(nvarchar(6),@nTime),6);
	SELECT @Return = LEFT(@strTime,2);
    SELECT @p = 3;
    WHILE(@p < 6)
    BEGIN
		SELECT @Return = @Return + ':' + Substring(@strTime,@p,2);
    
    	SELECT @p = @p + 2;
    END

	RETURN @Return;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertInt_Time]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 将整数表示的时间 转换为 24小时时间字符串,格式[hh:mi:ss]
-- 示例:
SELECT dbo.Apq_ConvertInt_Time('310'/*所支持的最大值*/)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertInt_Time]
(
	@nTime	int
)
RETURNS nvarchar(8)
AS
BEGIN
	DECLARE @Return nvarchar(8), @strTime nvarchar(8), @p int;
    SELECT @strTime = RIGHT('000000'+Convert(nvarchar(6),@nTime),6);
	SELECT @Return = LEFT(@strTime,2);
    SELECT @p = 3;
    WHILE(@p < 6)
    BEGIN
		SELECT @Return = @Return + ':' + Substring(@strTime,@p,2);
    
    	SELECT @p = @p + 2;
    END

	RETURN @Return;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertHexStr_VarBinary]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-04-30
-- 描述: [未检测溢出]将一串16进制的字符串 @str 转换为 VarBinary
-- 示例:
SELECT dbo.Apq_ConvertHexStr_VarBinary('7FFFFFFFFFFFFFFF'/*所支持的最大值*/)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertHexStr_VarBinary]
(
	@hexstr	varchar(max)
)
RETURNS varbinary(max)
AS
BEGIN
	DECLARE @Return varbinary(max), @ind int, @byte1 int, @byte2 int;
	SELECT @Return = 0x;
	IF(lower(substring(@hexstr, 1, 2)) = '0x') SET @ind = 3;
	ELSE SET @ind = 1;
	
    WHILE ( @ind <= len(@hexstr) )
    BEGIN
        SET @byte1 = ascii(substring(@hexstr, @ind, 1))
        SET @byte2 = ascii(substring(@hexstr, @ind + 1, 1))
        SET @Return = @Return + convert(binary(1),
                  case
                        when @byte1 between 48 and 57 then @byte1 - 48
                        when @byte1 between 65 and 70 then @byte1 - 55
                        when @byte1 between 97 and 102 then @byte1 - 87
                        else null end * 16 +
                  case
                        when @byte2 between 48 and 57 then @byte2 - 48
                        when @byte2 between 65 and 70 then @byte2 - 55
                        when @byte2 between 97 and 122 then @byte2 - 87
                        else null end)
        SET @ind = @ind + 2
    END

	RETURN @Return;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertBinary4_IP4]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-10-30
-- 描述: 将 binary(4) 转化为 IP4串
-- 示例:
SELECT dbo.Apq_ConvertBinary4_IP4(0xFFFFFFFFFFFFFFFF);
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertBinary4_IP4](
	@binIP	binary(4)
)RETURNS varchar(max)
AS
BEGIN
	DECLARE	 @Return varchar(max)
			,@nIP int
			,@i int
			;
	SELECT	 @nIP = 0
			,@i = 1
			;

	WHILE(@i <= 4)
	BEGIN
		SELECT	@nIP = Convert(int, SUBSTRING(@binIP, @i, 1));
		SELECT	@Return = ISNULL(@Return, '') + '.' + Convert(varchar, @nIP);

		SELECT	@i = @i + 1;
	END

	RETURN SUBSTRING(@Return, 2, LEN(@Return)-1);
END
GO
/****** Object:  Table [dbo].[Apq_Config]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apq_Config](
	[ID] [bigint] NOT NULL,
	[ParentID] [bigint] NULL,
	[Class] [nvarchar](512) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Apq_Config] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Config', @level2type=N'COLUMN',@level2name=N'Class'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Config', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Apq_Config', @level2type=N'COLUMN',@level2name=N'Value'
GO
/****** Object:  UserDefinedFunction [bak].[Apq_Compute_DBName_Restore]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [bak].[Apq_Compute_DBName_Restore](
	@DBName	nvarchar(256),
	@BakTime	datetime
)RETURNS nvarchar(256) AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-06-16
-- 描述: 计算还原时使用的数据库名(可自行修改)
-- 示例:
SELECT bak.Apq_Compute_DBName_Restore('Apq', getdate())
-- =============================================
*/
BEGIN
	DECLARE	@rtn nvarchar(256);
	SELECT @rtn = @DBName + '_DW' + Convert(nvarchar,DatePart(dw,@BakTime));
	--SELECT @rtn = @DBName + FGameDB.dbo.Apq_Ext_Get('',0,'ServerID') + '_Bak' + Convert(nvarchar,DatePart(dw,@BakTime));
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_CharIndexR]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 描述: 自右向左的CharIndex
-- 示例:
SELECT dbo.Apq_CharIndexR('D:\Apq_DBA\UpFile\D\Bak\[Log]Apq_Bak.txt','\',1);
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_CharIndexR] (
     @str nvarchar(max)
    ,@str_find nvarchar(max)
    ,@Num bigint = 1
    )
RETURNS bigint
AS 
BEGIN
    DECLARE @t TABLE (
         ID [bigint] IDENTITY(1,1)
                     NOT NULL
        ,Pos bigint
        )
    DECLARE @Return bigint
       ,@Len bigint	-- 长度
       ,@ib int		-- 当前解析起始位置
       ,@ie int		-- 当前解析结束位置
       ,@i int ;
       
    SELECT  @Len = Len(@str) ;
    SELECT  @ib = 1 ;
    WHILE ( @ib <= @Len ) 
        BEGIN
            SELECT  @ie = Charindex(@str_find,@str,@ib) ;
            IF ( @ie > 0 ) 
                INSERT  @t ( Pos )
                        SELECT  @ie ;
            ELSE 
                BREAK ;
                
            SELECT  @ib = @ie + 1 ;
        END
    
    SELECT TOP ( @Num )
            @Return = Pos
    FROM    @t
    ORDER BY ID DESC
    
    RETURN @Return ;
END
GO
/****** Object:  StoredProcedure [bcp].[Apq_BcpInFromFolder_ga]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bcp].[Apq_BcpInFromFolder_ga]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-09-23
-- 描述: 从文件夹导入GameActor数据
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bcp.Apq_BcpInFromFolder_ga;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @FullTableName nvarchar(256),	-- 完整表名
	@BcpFolder nvarchar(4000),--数据目录
	@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@FileName nvarchar(4000)		-- 临时变量:数据文件名
	;

SELECT @BcpFolder = 'D:\FTP\GameActor\'
	,@FullTableName = 'Stat_QQHX.bcp.GameActor_Bcp';
-- 接收cmd返回结果
CREATE TABLE #t(s nvarchar(4000));

-- 读取文件
SELECT @cmd = 'dir /a:-d/b/o:n "' + @BcpFolder + 'ga_*.txt"';
INSERT #t(s) EXEC master..xp_cmdshell @cmd;

DECLARE @FileCount int;
SELECT @FileCount = COUNT(*) FROM #t WHERE s IS NOT NULL;
WHILE(@FileCount > 0 )
BEGIN
	SELECT TOP 1 @FileName = s FROM #t;
	DELETE #t WHERE s = @FileName;
	SELECT @FileCount = COUNT(*) FROM #t WHERE s IS NOT NULL;
	
	-- Bcp in
	SELECT @cmd = 'bcp "' + @FullTableName + '" in "' + @BcpFolder + @FileName + '" -c -r~*$';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		CONTINUE;
	END
	SELECT @cmd = 'del /F /Q "' + @BcpFolder + @FileName + '"'
	EXEC master..xp_cmdshell @cmd;
END

DROP TABLE #t;
RETURN 1;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertPBigintTo]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-29
-- 描述: 将一正整数 @value 转换为 @to 进制表示的字符串
-- 示例:
SELECT dbo.Apq_ConvertPBigintTo(16, 9223372036854775807/*Bigint正数最大值*/)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertPBigintTo]
(
	@to		int,
	@value	bigint
)
RETURNS varchar(max)
AS
BEGIN
	IF(@to < 2 OR @to > 36)
	BEGIN
		RETURN '';
	END

	DECLARE @Return varchar(max);

	DECLARE @q bigint, @r int;
	SELECT @q = @value, @Return = '';
	WHILE( @q <> 0 )
	BEGIN
		SELECT @r = @q % @to;
		IF( @r < 10 )
		BEGIN
			SELECT @Return = CHAR(@r + ASCII('0') - 0) + @Return;
		END
		ELSE
		BEGIN
			SELECT @Return = CHAR(@r + ASCII('A') - 10) + @Return;
		END

		SELECT @q = @q / @to;
	END

	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertPBigintFrom]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-29
-- 描述: [未检测溢出]将一串 @from 进制的字符串 @str 转换为Bigint正数
-- 示例:
SELECT dbo.Apq_ConvertPBigintFrom(16, '7FFFFFFFFFFFFFFF'/*所支持的最大值*/)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertPBigintFrom]
(
	@from	bigint,
	@str	varchar(max)
)
RETURNS bigint
AS
BEGIN
	DECLARE @Return bigint;

	DECLARE @i int, @Length int, @c char(1);
	SELECT @Return = 0, @i = 0, @Length = LEN(@str);
	WHILE( @i < @Length )
	BEGIN
		SELECT @c = SUBSTRING( @str, @Length - @i, 1 );
		IF( ASCII(@c) <= ASCII('9') )
		BEGIN
			SELECT @Return = POWER( @from, @i ) * (ASCII(@c) - ASCII('0') + 0) + @Return;
		END
		ELSE
		BEGIN
			IF( ASCII(@c) > ASCII('Z') )
			BEGIN
				SELECT @c = CHAR(ASCII(@c) - 32);	-- 转换为大写
			END
			SELECT @Return = POWER( @from, @i ) * (ASCII(@c) - ASCII('A') + 10) + @Return;
		END

		SELECT @i = @i + 1;
	END

	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertVarBinary_HexStr]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-15
-- 描述: 将VarBinary转换为16进制字符串表示(不含0x)
-- 示例:
SELECT dbo.Apq_ConvertVarBinary_HexStr(0x0123456789ABCDEF)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertVarBinary_HexStr]
(
	@bin	varbinary(max)
)
RETURNS varchar(max)
AS
BEGIN
	DECLARE @Return varchar(max), @ind int, @byte binary(1),@byte1 int, @byte2 int;
	SELECT @Return = '',@ind = 1;
	
    WHILE ( @ind <= datalength(@bin) )
    BEGIN
		SELECT @byte = substring(@bin, @ind, 1);
        SET @byte1 = @byte / 16
        IF(@byte1 >= 10)
			SELECT @Return = @Return + 
				CASE @byte1
					WHEN 10 THEN 'A'
					WHEN 11 THEN 'B'
					WHEN 12 THEN 'C'
					WHEN 13 THEN 'D'
					WHEN 14 THEN 'E'
					WHEN 15 THEN 'F'
				END
		ELSE
			SELECT @Return = @Return + convert(char(1),@byte1)

        SET @byte2 = @byte % 16
        IF(@byte2 >= 10)
			SELECT @Return = @Return + 
				CASE @byte2
					WHEN 10 THEN 'A'
					WHEN 11 THEN 'B'
					WHEN 12 THEN 'C'
					WHEN 13 THEN 'D'
					WHEN 14 THEN 'E'
					WHEN 15 THEN 'F'
				END
		ELSE
			SELECT @Return = @Return + convert(char(1),@byte2)

		SELECT @ind = @ind + 1;
    END

	RETURN @Return;
END
GO
/****** Object:  Table [dbo].[DTSConfig]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DTSConfig](
	[TransName] [nvarchar](50) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[TransMethod] [tinyint] NOT NULL,
	[STMT] [nvarchar](4000) NOT NULL,
	[TransCycle] [int] NOT NULL,
	[TransTime] [datetime] NOT NULL,
	[SrvName] [nvarchar](256) NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[SchemaName] [nvarchar](256) NOT NULL,
	[SPTName] [nvarchar](256) NOT NULL,
	[U] [nvarchar](256) NOT NULL,
	[P] [nvarchar](256) NOT NULL,
	[LastID] [bigint] NULL,
	[LastTime] [datetime] NULL,
	[STMTMax] [nvarchar](4000) NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[LastTransTime] [datetime] NULL,
	[NeedTrans] [tinyint] NOT NULL,
	[Detect] [nvarchar](4000) NULL,
	[TodayBeginTime] [datetime] NULL,
	[KillFtpTime] [datetime] NOT NULL,
 CONSTRAINT [PK_DTSConfig] PRIMARY KEY NONCLUSTERED 
(
	[TransName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传送名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'TransName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传送方法{1:BCP queryout,2:BCP out,3:远程SP,4:LinkServer}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'TransMethod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用LinkServer时,可使用以下预定义变量@LastID,@LastTime,@MaxID,@MaxTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'STMT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传送周期(天)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'TransCycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'TransTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用于计算本次传送最大值的临时存储过程,参数:@MaxID bigint out,@MaxTime datetime out' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'STMTMax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次传送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig', @level2type=N'COLUMN',@level2name=N'LastTransTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据传送配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTSConfig'
GO
/****** Object:  Table [dbo].[DTS_Send]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DTS_Send](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CfgName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[RunnerIDCfg] [int] NOT NULL,
	[TransMethod] [tinyint] NOT NULL,
	[SrvID] [int] NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[SchemaName] [nvarchar](256) NOT NULL,
	[SPTName] [nvarchar](256) NOT NULL,
	[U] [nvarchar](256) NOT NULL,
	[P] [nvarchar](256) NOT NULL,
	[FTPIP] [tinyint] NOT NULL,
	[FTPFolderTmp] [nvarchar](512) NOT NULL,
	[FTPFolder] [nvarchar](512) NOT NULL,
	[RunnerIDRun] [int] NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[PickLastID] [bigint] NOT NULL,
	[PickLastTime] [datetime] NOT NULL,
 CONSTRAINT [PK_DTS_Send] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_DTS_Send:CfgName] ON [dbo].[DTS_Send] 
(
	[CfgName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送配置名(唯一)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'CfgName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预订执行者编号(0即为任意执行者)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'RunnerIDCfg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传送方法{1:BCP in,3:远程SP,4:LinkServer,5:FTP}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'TransMethod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标服务器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'SrvID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'U'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'P'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP传送时选择IP{0:Lan,1:Wan1,2:Wan2}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'FTPIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP临时目录,上传完成后移动到正式目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'FTPFolderTmp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'FTPFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前执行者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'RunnerIDRun'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[仅用于收集作业]已收集数据的最后ID(数据来源)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'PickLastID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[仅用于收集作业]已收集数据的最后时间(数据来源)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send', @level2type=N'COLUMN',@level2name=N'PickLastTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据传送配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DTS_Send'
GO
/****** Object:  Table [dbo].[DisplayToID]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DisplayToID](
	[GUID] [uniqueidentifier] NOT NULL,
	[RunID] [int] NULL,
	[DisplayString] [varchar](1024) NOT NULL,
	[LogStartTime] [char](24) NULL,
	[LogStopTime] [char](24) NULL,
	[NumberOfRecords] [int] NULL,
	[MinutesToUTC] [int] NULL,
	[TimeZoneName] [char](32) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[DisplayString] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CounterDetails]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CounterDetails](
	[CounterID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [varchar](1024) NOT NULL,
	[ObjectName] [varchar](1024) NOT NULL,
	[CounterName] [varchar](1024) NOT NULL,
	[CounterType] [int] NOT NULL,
	[DefaultScale] [int] NOT NULL,
	[InstanceName] [varchar](1024) NULL,
	[InstanceIndex] [int] NULL,
	[ParentName] [varchar](1024) NULL,
	[ParentObjectID] [int] NULL,
	[TimeBaseA] [int] NULL,
	[TimeBaseB] [int] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[CounterID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CounterData]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CounterData](
	[GUID] [uniqueidentifier] NOT NULL,
	[CounterID] [int] NOT NULL,
	[RecordIndex] [int] NOT NULL,
	[CounterDateTime] [char](24) NOT NULL,
	[CounterValue] [float] NOT NULL,
	[FirstValueA] [int] NULL,
	[FirstValueB] [int] NULL,
	[SecondValueA] [int] NULL,
	[SecondValueB] [int] NULL,
	[MultiCount] [int] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[GUID] ASC,
	[CounterID] ASC,
	[RecordIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cfg_WH]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cfg_WH](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DBName] [nvarchar](128) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
 CONSTRAINT [PK_Cfg_WH] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [etl].[BcpSTableCfg]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [etl].[BcpSTableCfg](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EtlName] [nvarchar](50) NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[SchemaName] [nvarchar](256) NOT NULL,
	[TName] [nvarchar](256) NOT NULL,
	[STName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Cycle] [int] NOT NULL,
	[STime] [smalldatetime] NOT NULL,
	[PreSTime] [datetime] NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_BcpSTableCfg] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BcpSTableCfg:EtlName] ON [etl].[BcpSTableCfg] 
(
	[EtlName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BcpIn到的表名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpSTableCfg', @level2type=N'COLUMN',@level2name=N'TName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpSTableCfg', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'切换周期(分钟)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpSTableCfg', @level2type=N'COLUMN',@level2name=N'Cycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'切换时间' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpSTableCfg', @level2type=N'COLUMN',@level2name=N'STime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一次切换时间' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpSTableCfg', @level2type=N'COLUMN',@level2name=N'PreSTime'
GO
/****** Object:  Table [etl].[BcpInQueue]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [etl].[BcpInQueue](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EtlName] [nvarchar](256) NULL,
	[Folder] [nvarchar](512) NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[SchemaName] [nvarchar](256) NOT NULL,
	[TName] [nvarchar](256) NOT NULL,
	[t] [nvarchar](10) NOT NULL,
	[r] [nvarchar](10) NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[IsFinished] [tinyint] NOT NULL,
	[DBID] [int] NULL,
 CONSTRAINT [PK_BcpInQueue] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ETL配置名(为空时不作为加载队列判断依据,即视为手工操作)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'EtlName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件目录(含时期)(必须以\结尾)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'Folder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名(格式:FileName[DBID].txt)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BcpIn到的表名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'TName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'-t' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N't'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'-r' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'r'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否完成' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'BcpInQueue', @level2type=N'COLUMN',@level2name=N'IsFinished'
GO
/****** Object:  Table [bak].[BakCfg]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bak].[BakCfg](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[FTPFolder] [nvarchar](4000) NOT NULL,
	[BakFolder] [nvarchar](4000) NOT NULL,
	[FullTime] [smalldatetime] NOT NULL,
	[FullCycle] [int] NOT NULL,
	[TrnCycle] [int] NOT NULL,
	[NeedTruncate] [tinyint] NOT NULL,
	[PreFullTime] [datetime] NULL,
	[PreBakTime] [datetime] NULL,
	[ReadyAction] [tinyint] NOT NULL,
	[NeedRestore] [tinyint] NOT NULL,
	[RestoreFolder] [nvarchar](4000) NULL,
	[State] [tinyint] NOT NULL,
	[DB_HisNum] [int] NOT NULL,
	[FTPFolderT] [nvarchar](4000) NULL,
	[Num_Full] [int] NOT NULL,
 CONSTRAINT [PK_BakCfg] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库名' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'DBName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份文件最终转入该目录以便FTP选择传送' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'FTPFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份目录(高性能,可为共享目录)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'BakFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完整备份时间(5分钟的倍数时间点)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'FullTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完整备份周期(天)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'FullCycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志备份周期(分钟,5的倍数)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'TrnCycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要截断日志' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'NeedTruncate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一次完整备份时间' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'PreFullTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一次备份时间' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'PreBakTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下一次备份作业启动后执行什么操作{0:跳过,1:完整备份,2:日志备份}' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'ReadyAction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要还原出历史库' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'NeedRestore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还原目录' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'RestoreFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态{0:空闲,1:完整备份中,2:日志备份中}' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'历史库保留个数' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'DB_HisNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中转文件夹(性能不定)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'FTPFolderT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完整备份文件保留个数(同时保留具有基础完整备份文件的日志备份文件)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'BakCfg', @level2type=N'COLUMN',@level2name=N'Num_Full'
GO
/****** Object:  Table [dbo].[ArpCfg]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArpCfg](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[GateWay] [nvarchar](50) NULL,
	[Mac] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArpCfg] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_XOr]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-08-21
-- 描述: 二进制位异或
-- 示例:
SELECT dbo.Apq_VarBinary_XOr(0x0040, 0x0141)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_XOr](
	@vb1	varbinary(max),
	@vb2	varbinary(max)
)RETURNS varbinary(max) AS
BEGIN
	DECLARE	@rtn varbinary(max)
		,@l1 int, @l2 int
		,@lmin int, @lmax int, @i int
		,@t1 tinyint, @b2 binary(1)
		,@a1 tinyint, @o1 tinyint
		;
	SELECT @rtn = 0x, @l1 = DataLength(@vb1), @l2 = DataLength(@vb2);
	SELECT @i = 1, @lmin = CASE WHEN @l1 < @l2 THEN @l1 ELSE @l2 END, @lmax = CASE WHEN @l1 > @l2 THEN @l1 ELSE @l2 END;

	WHILE(@i <= @lmin)
	BEGIN
		SELECT @t1 = SubString(@vb1, @i, 1);
		SELECT @b2 = SubString(@vb2, @i, 1);

		SELECT @o1 = @t1 | @b2;
		SELECT @a1 = @t1 & @b2;
		SELECT @rtn = @rtn + Convert(binary(1),(@o1-@a1));

		SELECT @i = @i + 1;
	END

	IF(@lmax > @lmin)
	BEGIN
		SELECT @rtn = @rtn + 
			CASE 
				WHEN @l1 = @lmax THEN SubString(@vb1, @i, @lmax - @lmin)
				ELSE SubString(@vb2, @i, @lmax - @lmin) 
			END;
	END
	
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_Reverse]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-12-19
-- 描述: 逆转二进制串
-- 示例:
SELECT dbo.Apq_VarBinary_Reverse(0x00000000FFFFFFFF)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_Reverse](
	@Input	varbinary(max)
)RETURNS varbinary(max)AS
BEGIN
	DECLARE	@Byte binary(1), @rtn varbinary(max), @i int;
	SELECT	@i = 0, @rtn = 0x;
	WHILE(@i < DATALENGTH(@Input))
	BEGIN
		SELECT	@i = @i + 1;
		SELECT	@Byte = Substring(@Input, @i, 1);
		SELECT	@rtn = @Byte + @rtn;
	END
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_Or]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-25
-- 描述: 二进制位或
-- 示例:
SELECT dbo.Apq_VarBinary_Or(0x000100, 0x010001)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_Or](
	@vb1	varbinary(max),
	@vb2	varbinary(max)
)RETURNS varbinary(max) AS
BEGIN
	DECLARE	@rtn varbinary(max)
		,@l1 int, @l2 int
		,@lmin int, @lmax int, @i int
		,@t1 tinyint, @b2 binary(1)
		;
	SELECT @rtn = 0x, @l1 = DataLength(@vb1), @l2 = DataLength(@vb2);
	SELECT @i = 1, @lmin = CASE WHEN @l1 < @l2 THEN @l1 ELSE @l2 END, @lmax = CASE WHEN @l1 > @l2 THEN @l1 ELSE @l2 END;

	WHILE(@i <= @lmin)
	BEGIN
		SELECT @t1 = SubString(@vb1, @i, 1);
		SELECT @b2 = SubString(@vb2, @i, 1);

		SELECT @rtn = @rtn + Convert(binary(1),(@t1 | @b2));

		SELECT @i = @i + 1;
	END

	IF(@lmax > @lmin)
	BEGIN
		SELECT @rtn = @rtn + 
			CASE 
				WHEN @l1 = @lmax THEN SubString(@vb1, @i, @lmax - @lmin)
				ELSE SubString(@vb2, @i, @lmax - @lmin) 
			END;
	END
	
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_InsertAt]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-16
-- 描述: 在二进制串中插入二进制
-- 示例:
SELECT dbo.Apq_VarBinary_InsertAt(0x00000000FFFFFFFF, 2, 0x11);
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_InsertAt](
	@vb1	varbinary(max),
	@index	int,
	@vb2	varbinary(max)
)RETURNS varbinary(max)AS
BEGIN
	IF(@index IS NULL OR @vb2 IS NULL OR @index < 0)
	BEGIN
		RETURN @vb1;
	END
	
	IF(@index = 0)
	BEGIN
		RETURN @vb2 + @vb1;
	END
	
	IF(@index > DATALENGTH(@vb1))
	BEGIN
		RETURN @vb1 + @vb2;
	END

	DECLARE @vb1b varbinary(max), @vb1e varbinary(max);
	SELECT @vb1b = SubString(@vb1, 1, @index);
	SELECT @vb1e = SubString(@vb1, @index, DATALENGTH(@vb1)-@index);
	RETURN @vb1b + @vb2 + @vb1e;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_InitFromBitIndex]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-25
-- 描述: 以指定索引位为1来初始化二进制串
-- 示例:
SELECT dbo.Apq_VarBinary_InitFromBitIndex(159)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_InitFromBitIndex](
	@idx	int
)RETURNS varbinary(max) AS
BEGIN
	DECLARE @rtn varbinary(max), @i tinyint
		,@q int, @r int;
	SELECT @rtn = 0x, @q = @idx / 8, @r = @idx % 8;
	IF(@q > 0 AND @r = 0)
	BEGIN
		SELECT @q = @q - 1;
	END

	SELECT @i = 0;
	WHILE(@i < @q)
	BEGIN
		SELECT @rtn = @rtn + 0x00;

		SELECT @i = @i + 1;
	END

	SELECT @rtn = @rtn + CASE @r WHEN 0 THEN 0x01 ELSE Convert(binary(1), Power(2,8-@r)) END;
	
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_ComputeBitIndex]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-07
-- 描述: 计算二进串中为1的位索引位置
-- 示例:
SELECT dbo.Apq_VarBinary_ComputeBitIndex(0x000000000000000000000000000000000000000000800000000000)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_ComputeBitIndex](
	@vb	varbinary(max)
)RETURNS varchar(max) AS
BEGIN
	DECLARE @return varchar(max), @i int, @p int, @b tinyint;
	SELECT @return = '', @i = 1;
	WHILE(@i <= DATALENGTH(@vb))
	BEGIN
		SELECT @b = SubString(@vb, @i, 1);
		
		SELECT @p = 0;
		WHILE(@p < 8)
		BEGIN
			IF((@b & 128 / Convert(tinyint,POWER(2,@p))) > 0)
			BEGIN
				SELECT @return = @return + ',' + Convert(varchar,(@i-1)*8+1+@p);
			END
			
			SELECT @p = @p + 1;
		END

		SELECT @i = @i + 1;
	END

	RETURN @return;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_And_2k]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-25
-- 描述: 二进制位与
-- 示例:
SELECT dbo.Apq_VarBinary_And(0x010101, 0xFF00FF11FF)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_And_2k](
	@vb1	varbinary(8000),
	@vb2	varbinary(8000)
)RETURNS varbinary(8000) AS
BEGIN
	DECLARE	@rtn varbinary(8000)
		,@l1 int, @l2 int
		,@lmin int, @lmax int, @i int
		,@t1 tinyint, @b2 binary(1)
		;
	SELECT @rtn = 0x, @l1 = DataLength(@vb1), @l2 = DataLength(@vb2);
	SELECT @i = 1, @lmin = CASE WHEN @l1 < @l2 THEN @l1 ELSE @l2 END, @lmax = CASE WHEN @l1 > @l2 THEN @l1 ELSE @l2 END;

	WHILE(@i <= @lmin)
	BEGIN
		SELECT @t1 = SubString(@vb1, @i, 1);
		SELECT @b2 = SubString(@vb2, @i, 1);

		SELECT @rtn = @rtn + Convert(binary(1),(@t1 & @b2));

		SELECT @i = @i + 1;
	END

	WHILE(@i <= @lmax)
	BEGIN
		SELECT @rtn = @rtn + 0x00;

		SELECT @i = @i + 1;
	END
	
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_VarBinary_And]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-06-25
-- 描述: 二进制位与
-- 示例:
SELECT dbo.Apq_VarBinary_And(0x010101, 0xFF00FF11FF)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_VarBinary_And](
	@vb1	varbinary(max),
	@vb2	varbinary(max)
)RETURNS varbinary(max) AS
BEGIN
	DECLARE	@rtn varbinary(max)
		,@l1 int, @l2 int
		,@lmin int, @lmax int, @i int
		,@t1 tinyint, @b2 binary(1)
		;
	SELECT @rtn = 0x, @l1 = DataLength(@vb1), @l2 = DataLength(@vb2);
	SELECT @i = 1, @lmin = CASE WHEN @l1 < @l2 THEN @l1 ELSE @l2 END, @lmax = CASE WHEN @l1 > @l2 THEN @l1 ELSE @l2 END;

	WHILE(@i <= @lmin)
	BEGIN
		SELECT @t1 = SubString(@vb1, @i, 1);
		SELECT @b2 = SubString(@vb2, @i, 1);

		SELECT @rtn = @rtn + Convert(binary(1),(@t1 & @b2));

		SELECT @i = @i + 1;
	END

	WHILE(@i <= @lmax)
	BEGIN
		SELECT @rtn = @rtn + 0x00;

		SELECT @i = @i + 1;
	END
	
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_Tree_City_List]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Apq_Tree_City_List]()RETURNS TABLE AS RETURN SELECT ID=1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_SwithPatition]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_SwithPatition]
	 @DBName	nvarchar(256)
	,@SrcTName	nvarchar(256)
	,@DstTName	nvarchar(256)
	,@Month		datetime
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-10-09
-- 描述: 切换表分区
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_SwithPatition 'Stat_QQHX', 'log.Consume','his.Consume','20090901';
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @sql nvarchar(4000), @PatitionNo nvarchar(50), @sqlDB nvarchar(4000);
SELECT @SPBeginTime=GetDate();

-- 计算 @PatitionNo
SELECT @PatitionNo = DATEDIFF(MM,'20070101',@Month) + 2;
--                                ↑这里是第一个分区的月数

SELECT @sql = 'ALTER TABLE ' + @SrcTName + ' SWITCH PARTITION ' + @PatitionNo
	+ ' TO ' + @DstTName + ' PARTITION ' + @PatitionNo;
SELECT @sqlDB = 'EXEC ' + @DBName + '.dbo.sp_executesql @sql';
--SELECT @sql,@sqlDB;
EXEC sp_executesql @sqlDB, N'@sql nvarchar(4000)',@sql=@sql;

RETURN 1;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_SwitchBinary8]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-12-18
-- 描述: 交换高低位,便于C++处理
-- 示例:
SELECT dbo.Apq_SwitchBinary8(0x00000000FFFFFFFF)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_SwitchBinary8](
	@Input	binary(8)
)RETURNS binary(8)AS
BEGIN
	DECLARE	@bin41 binary(4), @bin42 binary(4);
	SELECT	 @bin41 = @Input					-- 高位
			,@bin42 = Substring(@Input, 5, 4)	-- 低位
	RETURN @bin42 + @bin41;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_String_Get_tvp]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Apq_String_Get_tvp]() RETURNS @t table([ID] int) AS BEGIN RETURN; END
GO
/****** Object:  Table [dbo].[IPConfig]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPConfig](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[nicName] [nvarchar](50) NULL,
	[MAC] [nvarchar](50) NULL,
	[IP] [nvarchar](500) NULL,
	[Gateway] [nvarchar](500) NULL,
	[DNS] [nvarchar](500) NULL,
 CONSTRAINT [PK_IPConfig] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FTP_SendQueue]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FTP_SendQueue](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Folder] [nvarchar](512) NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[FTPSrv] [nvarchar](256) NOT NULL,
	[U] [nvarchar](256) NOT NULL,
	[P] [nvarchar](256) NOT NULL,
	[FTPFolder] [nvarchar](512) NOT NULL,
	[FTPFolderTmp] [nvarchar](512) NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[LSize] [bigint] NOT NULL,
	[RSize] [bigint] NOT NULL,
	[IsSuccess] [tinyint] NOT NULL,
 CONSTRAINT [PK_FTP_SendQueue] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'Folder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP服务器(IP 端口)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'FTPSrv'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'U'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'P'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'FTPFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP临时目录,上传完成后移动到正式目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'FTPFolderTmp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'LSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'远程文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'RSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成功(失败重传)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_SendQueue', @level2type=N'COLUMN',@level2name=N'IsSuccess'
GO
/****** Object:  Table [bak].[FTP_PutBak]    Script Date: 11/09/2010 11:49:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bak].[FTP_PutBak](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[LastFileName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Folder] [nvarchar](512) NOT NULL,
	[FTPSrv] [nvarchar](256) NOT NULL,
	[U] [nvarchar](256) NOT NULL,
	[P] [nvarchar](256) NOT NULL,
	[FTPFolder] [nvarchar](512) NOT NULL,
	[FTPFolderTmp] [nvarchar](512) NOT NULL,
	[Num_Full] [int] NOT NULL,
	[TransferIDCfg] [int] NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
	[TransferIDRun] [int] NOT NULL,
 CONSTRAINT [PK_FTP_PutBak] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库名' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'DBName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件目录' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'Folder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP服务器(IP:端口)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'FTPSrv'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'U'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'P'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP目录' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'FTPFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP临时目录,上传完成后移动到正式目录' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'FTPFolderTmp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完整备份文件保留个数(同时保留具有基础完整备份文件的日志备份文件)' , @level0type=N'SCHEMA',@level0name=N'bak', @level1type=N'TABLE',@level1name=N'FTP_PutBak', @level2type=N'COLUMN',@level2name=N'Num_Full'
GO
/****** Object:  Table [dbo].[FTP_GetBak]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FTP_GetBak](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[LastFileName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Folder] [nvarchar](512) NOT NULL,
	[FTPSrv] [nvarchar](256) NOT NULL,
	[U] [nvarchar](256) NOT NULL,
	[P] [nvarchar](256) NOT NULL,
	[FTPFolder] [nvarchar](512) NOT NULL,
	[Num_Full] [int] NOT NULL,
	[Num_Trn] [int] NOT NULL,
	[NeedRestore] [tinyint] NOT NULL,
	[RestoreFolder] [nvarchar](4000) NULL,
	[State] [tinyint] NOT NULL,
	[DB_HisNum] [int] NOT NULL,
 CONSTRAINT [PK_FTP_GetBak] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'DBName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'Folder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP服务器(IP:端口)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'FTPSrv'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'U'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'P'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'FTPFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完整备份文件保留个数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'Num_Full'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志备份文件保留个数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'Num_Trn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要还原{1:历史,2:备用}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'NeedRestore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还原目录(备份机目录)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'RestoreFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态{0:空闲,1:下载中,2:恢复中}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'历史库保留个数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FTP_GetBak', @level2type=N'COLUMN',@level2name=N'DB_HisNum'
GO
/****** Object:  Table [dbo].[FileTrans]    Script Date: 11/09/2010 11:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileTrans](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](500) NOT NULL,
	[DBFolder] [nvarchar](500) NOT NULL,
	[CFolder] [nvarchar](500) NULL,
	[FileStream] [varbinary](max) NOT NULL,
	[_InTime] [datetime] NOT NULL,
 CONSTRAINT [PK_FileTrans] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名(短)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileTrans', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DB文件夹' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileTrans', @level2type=N'COLUMN',@level2name=N'DBFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端文件夹(简单相对路径)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileTrans', @level2type=N'COLUMN',@level2name=N'CFolder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件流' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileTrans', @level2type=N'COLUMN',@level2name=N'FileStream'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileTrans', @level2type=N'COLUMN',@level2name=N'_InTime'
GO
/****** Object:  Table [etl].[EtlCfg]    Script Date: 11/09/2010 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [etl].[EtlCfg](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EtlName] [nvarchar](256) NOT NULL,
	[Folder] [nvarchar](512) NOT NULL,
	[PeriodType] [int] NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[DBName] [nvarchar](256) NOT NULL,
	[SchemaName] [nvarchar](256) NOT NULL,
	[TName] [nvarchar](256) NOT NULL,
	[t] [nvarchar](10) NOT NULL,
	[r] [nvarchar](10) NOT NULL,
	[_InTime] [datetime] NOT NULL,
	[_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_EtlCfg] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EtlCfg:EtlName] ON [etl].[EtlCfg] 
(
	[EtlName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = ON, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ETL配置名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'EtlName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件目录(不含时期)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'Folder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时期类型{1:年,2:半年,3:季度,4:月,5:周,6:日,7:时,8:分}' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'PeriodType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名(前缀)(格式:FileName[SrvID].txt)' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用与否' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BcpIn到的表名' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'TName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'-t' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N't'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'-r' , @level0type=N'SCHEMA',@level0name=N'etl', @level1type=N'TABLE',@level1name=N'EtlCfg', @level2type=N'COLUMN',@level2name=N'r'
GO
/****** Object:  StoredProcedure [etl].[Etl_SwitchBcpTable]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Etl_SwitchBcpTable]
	@EtlName	nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 切换BCP接收表
-- 功能: 按预定时间切换BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Etl_SwitchBcpTable 'ImeiLog';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@STName nvarchar(256),
	@Cycle int,			-- 切换周期(分钟)
	@STime smalldatetime,-- 切换时间
	@PreSTime datetime	-- 上一次切换时间
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

SELECT @ID=ID,@DBName=DBName,@SchemaName=SchemaName,@TName=TName,@STName=STName
  FROM etl.BcpSTableCfg
 WHERE EtlName = @EtlName;
IF(@@ROWCOUNT > 0)
BEGIN
	-- 切换表
	SELECT @sqlDB = 'EXEC sp_rename ''' + @SchemaName + '.' + @STName + ''', ''' + @STName + '_SwithTmp'';
EXEC sp_rename ''' + @SchemaName + '.' + @TName + ''', ''' + @STName + ''';
EXEC sp_rename ''' + @SchemaName + '.' + @STName + '_SwithTmp'', ''' + @TName + ''';
';
	SELECT @sql = 'EXEC [' + @DBName + ']..sp_executesql @sqlDB';
	EXEC sp_executesql @sql, N'@sqlDB nvarchar(4000)', @sqlDB = @sqlDB;
	UPDATE etl.BcpSTableCfg SET _Time = getdate() WHERE ID = @ID;
END

RETURN 1;
GO
/****** Object:  StoredProcedure [etl].[Etl_Load]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Etl_Load]
	@EtlName	nvarchar(50)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 加载到正式表
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Etl_Load 'ImeiLog';
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@SrcFullTableName nvarchar(256),
	@DstFullTableName nvarchar(256)
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;

SELECT @ID = ID,@SrcFullTableName=SrcFullTableName,@DstFullTableName=DstFullTableName
  FROM etl.LoadCfg
 WHERE EtlName = @EtlName;
IF(@@ROWCOUNT > 0)
BEGIN
	-- 加载表
	SELECT @sql = '
INSERT ' + @DstFullTableName + '
SELECT *
FROM ' + @SrcFullTableName + ';

TRUNCATE TABLE ' + @SrcFullTableName + ';
';
	EXEC sp_executesql @sql;
	
	UPDATE etl.LoadCfg SET _Time = getdate() WHERE ID = @ID;
END

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_Stat]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_Stat]
	 @StatName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-11-05
-- 描述: 数据统计
-- 参数:
@StatName: 统计名
@STMT: 统计语句
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_Stat 'GameActor';
SELECT @rtn;
-- =============================================
-1:未找到配置
-2:检测未通过
-3:时间未到
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime;
SELECT @SPBeginTime=GetDate();

DECLARE @Detect nvarchar(4000)
	,@STMT nvarchar(4000)
	,@LastStatDate datetime
	
	,@StatDate datetime
	;
SELECT @Detect = Detect, @STMT = STMT, @LastStatDate = DateAdd(dd,0,DateDiff(dd,0,LastStatDate))
  FROM dbo.StatConfig_Day
 WHERE StatName = @StatName;
IF(@@ROWCOUNT = 0)
BEGIN
	RETURN -1;
END

-- 检测数据(通过才统计)
IF(LEN(@Detect)>1)
BEGIN
	EXEC @rtn = sp_executesql @Detect;
	IF(@@ERROR <> 0 OR @rtn <> 1)
	BEGIN
		RETURN -2;
	END
END

SELECT @StatDate = DATEADD(DD,1,@LastStatDate);
IF(DATEADD(DD,1,@StatDate) > @SPBeginTime)
BEGIN
	RETURN -3;
END

-- 先修改已统计日期
UPDATE dbo.StatConfig_Day
   SET _Time = GETDATE(), LastStatDate = @StatDate
 WHERE StatName = @StatName;

EXEC @rtn = sp_executesql @STMT, N'@StatDate datetime', @StatDate = @StatDate;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Apq_Restore]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Apq_Restore]
	 @Action	tinyint	-- 动作{0x01:是否执行,0x02:是否打印语句,0x04:是否以结果集显示语句}
	,@DBName		nvarchar(128)	-- 从完整备份恢复时,物理文件名为 数据库名[文件组名].(mdf/ldf)
	,@FileFullName	nvarchar(4000)	-- 备份文件全名(含路径)
	,@RType			tinyint = 1	-- 还原类型{1:完整,2:日志}
	,@RFolder		nvarchar(4000) = ''	-- MOVE 目标目录
	,@NORECOVERY	tinyint = 0	-- 完整还原时 NORECOVERY 选项选择(日志还原一定使用STANDBY){0:无(即RECOVERY),1:NORECOVERY,2:STANDBY}
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-19
-- 描述: 还原数据库(完整/日志)
-- 参数:
@DBName: 还原使用的数据库名
@FileFullName: 备份文件全名(含路径)
@RType: 还原类型{1:完整,2:日志}
@RFolder: 还原使用的目录
@NORECOVERY: 完整还原时 NORECOVERY 选项选择(日志还原一定使用STANDBY){0:无(即RECOVERY),1:NORECOVERY,2:STANDBY}
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_Restore;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@RType IS NULL) SELECT @RType = 1;
IF(@RFolder IS NULL) SELECT @RFolder = '';

--定义变量
DECLARE @Restore_MOVE nvarchar(max)--Restore语句的MOVE子句

	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(max)
	,@SPBeginTime datetime
	,@LogicalName nvarchar(128)
	,@Type char(1)
	,@StdbFileFullName nvarchar(4000)	-- 临时变量:StandBy文件全名
	;
SELECT @SPBeginTime=GetDate();

CREATE TABLE #FileList(
	LogicalName nvarchar(128),
	Physicalname nvarchar(260),
	Type char(1),
	FileGroupName nvarchar(128),
	Size numeric(20,0),
	MaxSize numeric(20),
	FileID bigint,
	CreateLSN numeric(25,0),
	DropLSN numeric(25,0) NULL,
	UniqueID uniqueidentifier,
	ReadOnlyLSN numeric(25,0) NULL,
	ReadWriteLSN numeric(25,0) NULL,
	BackupSizeInBytes bigint,
	SourceBlockSize int,
	FileGroupID int ,
	LogGroupGUID uniqueidentifier NULL,
	DifferentialBaseLSN numeric(25,0) NULL,
	DifferentialBaseGUID uniqueidentifier,
	IsReadOnly bit,
	IsPresent bit,
	TDEThumbprint varbinary(32)
);

--定义游标
DECLARE @csrF CURSOR
SET @csrF=CURSOR FOR
SELECT LogicalName,Type FROM #FileList WHERE Type IN ('D','L');

-- 创建还原目录
IF(Len(@RFolder)>3)
BEGIN
	IF(Right(@RFolder,1) <> '\') SELECT @RFolder = @RFolder + '\';
	SELECT @cmd = 'md ' + LEFT(@RFolder, LEN(@RFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 踢数据库用户
EXEC dbo.Apq_KILL_DB @DBName = @DBName;

SELECT @StdbFileFullName = @RFolder + @DBName + '.lsf'

IF(@RType = 1)	-- 完整还原
BEGIN
	SELECT @sql = 'RESTORE FILELISTONLY FROM DISK=@BakFile',@Restore_MOVE = '';
	INSERT #FileList(LogicalName,Physicalname,Type,FileGroupName,Size,MaxSize,FileID,CreateLSN,DropLSN
		,UniqueID,ReadOnlyLSN,ReadWriteLSN,BackupSizeInBytes,SourceBlockSize,FileGroupID,LogGroupGUID
		,DifferentialBaseLSN,DifferentialBaseGUID,IsReadOnly,IsPresent,TDEThumbprint)
	EXEC sp_executesql @sql,N'@BakFile nvarchar(512)', @BakFile=@FileFullName;

	OPEN @csrF;
	FETCH NEXT FROM @csrF INTO @LogicalName,@Type;
	WHILE(@@FETCH_STATUS=0)
	BEGIN
		SELECT @Restore_MOVE = @Restore_MOVE + ',
	MOVE ''' + @LogicalName + ''' TO ''' + @RFolder + @DBName + '[' + @LogicalName
			+ '].' + CASE @Type WHEN 'D' THEN 'mdf' ELSE 'ldf' END + '''';

		NEXT_File:
		FETCH NEXT FROM @csrF INTO @LogicalName,@Type;
	END
	CLOSE @csrF;
	
	SELECT @sql = 'DBCC TRACEON(1807);
RESTORE DATABASE @DBName FROM DISK=@BakFile WITH REPLACE' + CASE @NORECOVERY
			WHEN 1 THEN ', NORECOVERY'
			WHEN 2 THEN ', STANDBY=@StdbFileFullName'
			ELSE ''
		END + @Restore_MOVE;
	IF(@Action & 4 > 0) SELECT sql = @sql;
	IF(@Action & 2 > 0) PRINT @sql;
	IF(@Action & 1 > 0)
		EXEC sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000),@StdbFileFullName nvarchar(4000)'
			,@DBName = @DBName, @BakFile = @FileFullName, @StdbFileFullName = @StdbFileFullName;
END

IF(@RType = 2)	-- 日志还原
BEGIN
	SELECT @sql = 'RESTORE LOG @DBName FROM DISK=@BakFile WITH ' + CASE @NORECOVERY
			WHEN 1 THEN 'NORECOVERY'
			WHEN 2 THEN 'STANDBY=@StdbFileFullName'
			ELSE ''
		END;
	IF(@Action & 4 > 0) SELECT sql = @sql;
	IF(@Action & 2 > 0) PRINT @sql;
	IF(@Action & 1 > 0)
		EXEC sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000),@StdbFileFullName nvarchar(4000)'
			,@DBName = @DBName, @BakFile = @FileFullName, @StdbFileFullName = @StdbFileFullName;
END

DROP TABLE #FileList;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_Servers_Save]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_Servers_Save]
    @ID int
   ,@ParentID int
   ,@Name nvarchar(256)
   ,@SrvName nvarchar(256)
   ,@UID nvarchar(256)
   ,@PwdC nvarchar(max)
   ,@Type int
AS 
SET NOCOUNT ON ;

IF ( @ID IS NULL ) 
    RETURN -1 ;

UPDATE  dbo.RSrvConfig
SET     ParentID = @ParentID,Name = @Name,LSName = @SrvName,UID = @UID,PwdC = @PwdC,Type = @Type
WHERE   ID = @ID ;
IF ( @@ROWCOUNT = 0 ) 
    INSERT  dbo.RSrvConfig ( ID,ParentID,Name,LSName,UID,PwdC,Type )
    VALUES  ( @ID,@ParentID,@Name,@SrvName,@UID,@PwdC,@Type )
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存服务器
-- 作者: 黄宗银
-- 日期: 2010-03-10
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Servers_Save @ExMsg out, 1, DEFAULT, ''127019'',''User1'',''User1'';
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_Servers_Save'
GO
/****** Object:  UserDefinedFunction [dbo].[ApqDBMgr_Servers_GetAll]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT * FROM dbo.ApqDBMgr_Servers_GetAll();
-- =============================================
*/
CREATE FUNCTION [dbo].[ApqDBMgr_Servers_GetAll] ( )
RETURNS TABLE
    AS RETURN
    SELECT  *
    FROM    dbo.RSrvConfig
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_RSrv_Save_20100505]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_RSrv_Save_20100505]
    @ID int
   ,@ParentID int
   ,@Name nvarchar(256)
   ,@UID nvarchar(256)
   ,@PwdC nvarchar(max)
   ,@Type int
   ,@IPLan nvarchar(500)
   ,@IPWan1 nvarchar(500)
   ,@IPWan2 nvarchar(500)
   ,@FTPPort int
   ,@FTPU nvarchar(50)
   ,@FTPPC nvarchar(500)
   ,@SqlPort int
AS 
SET NOCOUNT ON ;

IF ( @ID IS NULL ) 
    RETURN -1 ;

UPDATE  dbo.RSrvConfig
SET     ParentID = @ParentID,Name = ISNULL(@Name,Name),UID = @UID,PwdC = @PwdC,Type = @Type,IPLan = @IPLan,IPWan1 = @IPWan1,IPWan2 = @IPWan2,
        FTPPort = ISNULL(@FTPPort,21),FTPU = @FTPU,FTPPC = @FTPPC,SqlPort = ISNULL(@SqlPort,1433)
WHERE   ID = @ID ;
IF ( @@ROWCOUNT = 0 ) 
    INSERT  dbo.RSrvConfig ( ID,ParentID,Name,UID,PwdC,Type,IPLan,IPWan1,IPWan2,FTPPort,FTPU,FTPPC,SqlPort )
    VALUES  ( @ID,@ParentID,@Name,@UID,@PwdC,@Type,@IPLan,@IPWan1,@IPWan2,@FTPPort,@FTPU,@FTPPC,@SqlPort )
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存服务器
-- 作者: 黄宗银
-- 日期: 2010-03-27
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_RSrv_Save_20100505 @ExMsg out, 1, DEFAULT, ''127019'',''User1'',''User1'';
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_RSrv_Save_20100505'
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_RSrv_Save]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_RSrv_Save]
    @ID int
   ,@ParentID int
   ,@Name nvarchar(256)
   ,@UID nvarchar(256)
   ,@PwdC nvarchar(max)
   ,@Type int
   ,@IPLan nvarchar(500)
   ,@IPWan1 nvarchar(500)
   ,@IPWan2 nvarchar(500)
   ,@FTPPort int
   ,@FTPU nvarchar(50)
   ,@FTPPC nvarchar(max)
   ,@SqlPort int
AS 
SET NOCOUNT ON ;

IF ( @ID IS NULL ) 
    RETURN -1 ;

UPDATE  dbo.RSrvConfig
SET     ParentID = @ParentID,Name = ISNULL(@Name,Name),LSName = ISNULL(@Name,Name),UID = @UID,PwdC = @PwdC,Type = @Type,IPLan = @IPLan,IPWan1 = @IPWan1,IPWan2 = @IPWan2,
        FTPPort = ISNULL(@FTPPort,21),FTPU = @FTPU,FTPPC = @FTPPC,SqlPort = ISNULL(@SqlPort,1433)
WHERE   ID = @ID ;
IF ( @@ROWCOUNT = 0 ) 
    INSERT  dbo.RSrvConfig ( ID,ParentID,Name,LSName,UID,PwdC,Type,IPLan,IPWan1,IPWan2,FTPPort,FTPU,FTPPC,SqlPort )
    VALUES  ( @ID,@ParentID,@Name,@Name,@UID,@PwdC,@Type,@IPLan,@IPWan1,@IPWan2,@FTPPort,@FTPU,@FTPPC,@SqlPort )
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存服务器
-- 作者: 黄宗银
-- 日期: 2010-03-27
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_RSrv_Save @ExMsg out, 1, DEFAULT, ''127019'',''User1'',''User1'';
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_RSrv_Save'
GO
/****** Object:  UserDefinedFunction [dbo].[ApqDBMgr_RSrv_List]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT * FROM dbo.ApqDBMgr_RSrv_List();
-- =============================================
*/
CREATE FUNCTION [dbo].[ApqDBMgr_RSrv_List] ( )
RETURNS TABLE
    AS RETURN
    SELECT  ID, ParentID, Name, UID, PwdC, Type, LSMaxTimes, LSErrTimes, LSState, IPLan, IPWan1, IPWan2, FTPPort, FTPU, FTPPC, SqlPort
    FROM    dbo.RSrvConfig
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_Route_BatArchive]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_Route_BatArchive]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 路由存档(bat文件)
-- 作者: 黄宗银
-- 日期: 2010-04-11
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Route_BatArchive NULL;
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_Route_BatArchive'
GO
/****** Object:  UserDefinedFunction [dbo].[ApqDBMgr_RDBUser_List]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT dbo.ApqDBMgr_RDBUser_List('FFFF::FFFF', 8);
-- =============================================
*/
CREATE FUNCTION [dbo].[ApqDBMgr_RDBUser_List] ( )
RETURNS TABLE
    AS RETURN
    SELECT  [RDBUserID],RDBID,DBUserName,DBUserDesc,RDBLoginID
    FROM    dbo.RDBUser
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_RDB_Save]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_RDB_Save]
    @ID int
   ,@ParentID int
   ,@Name nvarchar(256)
   ,@SrvName nvarchar(256)
   ,@UID nvarchar(256)
   ,@PwdC nvarchar(max)
   ,@Type int
AS 
SET NOCOUNT ON ;

IF ( @ID IS NULL ) 
    RETURN -1 ;

UPDATE  dbo.RSrvConfig
SET     ParentID = @ParentID,Name = @Name,LSName=@Name,UID = @UID,PwdC = @PwdC,Type = @Type
WHERE   ID = @ID ;
IF ( @@ROWCOUNT = 0 ) 
    INSERT  dbo.RSrvConfig ( ID,ParentID,Name,LSName,UID,PwdC,Type )
    VALUES  ( @ID,@ParentID,@Name,@Name,@UID,@PwdC,@Type )
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存数据库
-- 作者: 黄宗银
-- 日期: 2010-03-10
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_RDB_Save @ExMsg out, 1, DEFAULT, ''127019'',''User1'',''User1'';
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_RDB_Save'
GO
/****** Object:  UserDefinedFunction [dbo].[ApqDBMgr_RDB_List]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT dbo.ApqDBMgr_RDB_List('FFFF::FFFF', 8);
-- =============================================
*/
CREATE FUNCTION [dbo].[ApqDBMgr_RDB_List] ( )
RETURNS TABLE
    AS RETURN
    SELECT  RDBID,DBName,RDBDesc,RDBType,PLevel,GLevel,SrvID,GameID
    FROM    dbo.RDBConfig
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_Arp_BatArchive]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_Arp_BatArchive]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- ARP(静态)存档(bat文件)
-- 作者: 黄宗银
-- 日期: 2010-04-12
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Arp_BatArchive NULL;
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_Arp_BatArchive'
GO
/****** Object:  StoredProcedure [dbo].[ApqDBMgr_Alias_RegArchive]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ApqDBMgr_Alias_RegArchive]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 别名存档(注册表文件)
-- 作者: 黄宗银
-- 日期: 2010-04-11
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.ApqDBMgr_Alias_RegArchive NULL;
SELECT @rtn,@ExMsg;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'ApqDBMgr_Alias_RegArchive'
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertScale]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-28
-- 描述: 将一 @from 进制的串 @value 转换为 @to 进制表示的字符串
-- 示例:
SELECT dbo.Apq_ConvertScale(16, 10, '7FFFFFFFFFFFFFFF'/*所支持的最大值*/)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertScale]
(
	@from	int,
	@to		int,
	@value	varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	RETURN dbo.Apq_ConvertPBigintTo( @to, dbo.Apq_ConvertPBigintFrom( @from, @value ) );
END
GO
/****** Object:  StoredProcedure [bak].[Apq_Bak_Trn]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Apq_Bak_Trn]
	@DBName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 日志备份(仅本地)
-- 参数:
@DBName: 数据库名
@BakFolder: 备份路径
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_Bak_Trn 'dtxc';
SELECT @rtn;
-- =============================================
1: 备份成功
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @BakFileName nvarchar(256), @BakFileFullName nvarchar(4000)
	,@cmd nvarchar(4000)
	,@sql nvarchar(4000)
	,@ID bigint
	,@BakFolder nvarchar(4000)
	,@FTPFolder nvarchar(4000)
	,@FTPFolderT nvarchar(4000)
	,@FTPFileFullName nvarchar(4000);
SELECT @SPBeginTime=GetDate();
SELECT @BakFolder = '';
SELECT @BakFolder = BakFolder,@FTPFolder = FTPFolder,@FTPFolderT = FTPFolderT
  FROM bak.BakCfg
 WHERE DBName = @DBName;

IF(Len(@BakFolder)>3)
BEGIN
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@BakFolder, 1, LEN(@BakFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolder)>3)
BEGIN
	IF(RIGHT(@FTPFolder,1)<>'\') SELECT @FTPFolder = @FTPFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolderT)>3)
BEGIN
	IF(RIGHT(@FTPFolderT,1)<>'\') SELECT @FTPFolderT = @FTPFolderT+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 记录备份日志
SELECT @BakFileName = @DBName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].trn';
SELECT @BakFileFullName = @BakFolder + @BakFileName;
--SELECT @cmd = '@echo [' + convert(nvarchar,@SPBeginTime,120) + ']' + @BakFileName + '>>' + @BakFolder + '[Log]Apq_Bak.txt'
--EXEC @rtn = xp_cmdshell @cmd;

-- 备份
SELECT @sql = 'BACKUP LOG @DBName TO DISK=@BakFile';
EXEC @rtn = sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000)', @DBName = @DBName, @BakFile = @BakFileFullName;
IF(@@ERROR <> 0 OR @rtn<>0)
BEGIN
	RETURN -1;
END

-- 移动到FTP目录 -----------------------------------------------------------------------------------
IF(Len(@FTPFolderT)>0)
BEGIN
	SELECT @FTPFileFullName = @BakFolder + @BakFileName;
	SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1) + '"';
	EXEC master..xp_cmdshell @cmd;
END
ELSE
BEGIN
	SELECT @FTPFolderT = @BakFolder;
END
SELECT @FTPFileFullName = @FTPFolderT + @BakFileName;
SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1) + '"';
EXEC master..xp_cmdshell @cmd;
-- =================================================================================================

SELECT BakFileName = @BakFileName;
RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Apq_Bak_Full]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Apq_Bak_Full]
	 @DBName		nvarchar(256)
	,@BakFileName	nvarchar(256) OUT	-- 备份文件名(不含路径)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 完整备份(仅本地)
-- 参数:
@DBName: 数据库名
@BakFolder: 备份路径
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_Bak_Full 'Apq_DBA';
SELECT @rtn;
-- =============================================
-2: 空间不足
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @BakFileFullName nvarchar(4000)
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	,@ID bigint
	,@BakFolder nvarchar(4000)
	,@FTPFolder nvarchar(4000)
	,@FTPFolderT nvarchar(4000)
	,@FTPFileFullName nvarchar(4000)
	,@NeedTruncate tinyint;
SELECT @SPBeginTime=GetDate();
SELECT @BakFolder = '', @NeedTruncate = 0;
SELECT @BakFolder = BakFolder, @FTPFolder = FTPFolder,@FTPFolderT = FTPFolderT, @NeedTruncate = NeedTruncate
  FROM bak.BakCfg
 WHERE DBName = @DBName;

IF(Len(@BakFolder)>3)
BEGIN
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@BakFolder, 1, LEN(@BakFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolder)>3)
BEGIN
	IF(RIGHT(@FTPFolder,1)<>'\') SELECT @FTPFolder = @FTPFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolderT)>3)
BEGIN
	IF(RIGHT(@FTPFolderT,1)<>'\') SELECT @FTPFolderT = @FTPFolderT+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 记录备份日志
SELECT @BakFileName = @DBName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].bak';
SELECT @BakFileFullName = @BakFolder + @BakFileName;
SELECT @cmd = '@echo [' + convert(nvarchar,@SPBeginTime,120) + ']' + @BakFileName + '>>' + @BakFolder + '[Log]Apq_Bak.txt'
EXEC @rtn = master..xp_cmdshell @cmd;

-- 检测剩余空间 ------------------------------------------------------------------------------------
DECLARE @spused float, @disksp float;
SELECT @sql = '
CREATE TABLE #spused(
	name		nvarchar(256),
	rows		varchar(11),
	reserved	varchar(18),
	data		varchar(18),
	index_size	varchar(18),
	unused		varchar(18)
);
EXEC ' + @DBName + '..sp_MSforeachtable "INSERT #spused EXEC sp_spaceused ''?'', ''true''";
SELECT @spused = 0;
SELECT @spused = @spused + LEFT(reserved,LEN(reserved)-3) FROM #spused;
SELECT @spused = @spused / 1024;
DROP TABLE #spused;
';
EXEC @rtn = sp_executesql @sql, N'@spused float out', @spused = @spused out;
CREATE TABLE #drives(
	drive	varchar(5),
	MB		float
);
INSERT #drives
EXEC master..xp_fixeddrives;

IF(EXISTS(SELECT TOP 1 1 FROM #drives WHERE MB < @spused * 0.7 AND drive IN(LEFT(@BakFolder,1),LEFT(@FTPFolder,1)))) -- 暂取0.7
RETURN -2;
-- =================================================================================================

--截断日志(仅限2000使用)
IF(@NeedTruncate=1)
BEGIN
	SELECT @sql='BACKUP LOG '+@DBName+' WITH NO_LOG';
	EXEC sp_executesql @sql;
END

SELECT @sql = 'BACKUP DATABASE @DBName TO DISK=@BakFile WITH INIT';
EXEC @rtn = sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000)', @DBName = @DBName, @BakFile = @BakFileFullName;
IF(@@ERROR <> 0 OR @rtn<>0)
BEGIN
	RETURN -1;
END

-- 移动到FTP目录 -----------------------------------------------------------------------------------
IF(Len(@FTPFolderT)>0)
BEGIN
	SELECT @FTPFileFullName = @BakFolder + @BakFileName;
	SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1) + '"';
	EXEC master..xp_cmdshell @cmd;
END
ELSE
BEGIN
	SELECT @FTPFolderT = @BakFolder;
END
SELECT @FTPFileFullName = @FTPFolderT + @BakFileName;
SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1) + '"';
EXEC master..xp_cmdshell @cmd;
-- =================================================================================================

SELECT BakFileName = @BakFileName;
RETURN 1;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertMac_VarBinary]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-28
-- 描述: 将Mac串转化为 varbinary(max)
-- 示例:
SELECT dbo.Apq_ConvertMac_VarBinary('00-50-8D-9E-EB-70');
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertMac_VarBinary](
	@Mac	varchar(max)
)RETURNS varbinary(max)
AS
BEGIN
	SELECT @Mac = LTRIM(RTRIM(@Mac));

	DECLARE @Return varbinary(max)
		,@Len int		-- 字符数
		,@ib int		-- 当前解析起始位置
		,@ie int		-- 当前解析结束位置
		,@i int
		;
	SELECT @Return = 0x
		,@Len = LEN(@Mac)
		,@ie = 0
		,@i = 1
		;

	IF(@Len < 16) RETURN @Return;

	WHILE(@i <= 6)
	BEGIN
		SELECT	@ib = @ie + 1;
		--SELECT	@ie = CHARINDEX(':', @Mac, @ib);
		SELECT	@ie = CHARINDEX('-', @Mac, @ib);
		IF(@ie = 0)
		BEGIN
			SELECT	@ie = @Len + 1;
		END
		
		IF(@ib >= @ie) BREAK;

		SELECT	@Return = ISNULL(@Return, 0x) + dbo.Apq_ConvertHexStr_VarBinary(SUBSTRING(@Mac, @ib, @ie - @ib));

		SELECT	@i = @i + 1;
	END

	RETURN @Return;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertIP6_VarBinary]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-19-25
-- 描述: 将标准的IP6串转为 varbinary(max)
-- 示例:
SELECT dbo.Apq_ConvertIP6_VarBinary('FFFF::FFFF', 8);
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertIP6_VarBinary](
	@IP		varchar(max),
	@Seg	tinyint	-- 段数
)RETURNS varbinary(max)
AS
BEGIN
	SELECT	 @IP = LTRIM(RTRIM(@IP))
			,@Seg = ISNULL(@Seg, 8)
			;

	IF(CHARINDEX('::', @IP) = LEN(@IP) - 1)
	BEGIN
		SELECT	@IP = @IP + '0000';
	END

	DECLARE	 @Return varbinary(max)
			,@Len int		-- 字符数
			,@ib int		-- 当前解析起始位置
			,@ie int		-- 当前解析结束位置
			,@Subs int		-- 分隔符(:)数量
			,@i int
			,@j int
			,@SLength int	-- (如果存在缩写符)缩写符实际表示的 双字节0(0x0000) 的数量(段数)
			;
	SELECT	 @Len = LEN(@IP)
			,@ie = 0
			,@i = 1
			,@j = 1
			;
	SELECT	@Subs = LEN(REPLACE(@IP, ':', 'zz')) - @Len;
	SELECT	@SLength = CASE CHARINDEX('::', @IP) WHEN 0 THEN 0 ELSE @Seg - @Subs END;

	WHILE(@i <= @Seg)
	BEGIN
		SELECT	@ib = @ie + 1;
		IF(@ib > @Len)
		BEGIN
			SELECT	@j = 1;
			WHILE(@j <= @Seg - @i)
			BEGIN
				SELECT	@Return = ISNULL(@Return, 0x) + 0x0000;
				
				SELECT	@j = @j + 1;
			END
			BREAK;
		END
		SELECT	@ie = CHARINDEX(':', @IP, @ib);
		IF(@ie = 0)
		BEGIN
			SELECT	@ie = @Len + 1;
		END

		IF(@ie = @ib)
		BEGIN	-- 遇到缩写符
			IF(@ib = 1)
			BEGIN
				SELECT	@SLength = @SLength + 1;
			END
			SELECT	@i = @i + @SLength - 1;

			SELECT	@j = 1;
			WHILE(@j <= @SLength)
			BEGIN
				SELECT	@Return = ISNULL(@Return, 0x) + 0x0000;
				
				SELECT	@j = @j + 1;
			END

			IF(@ib = 1)
			BEGIN
				SELECT	@ie = @ie + 1;
			END
		END
		ELSE
		BEGIN
			SELECT	@Return = ISNULL(@Return, 0x) + Convert(binary(2), dbo.Apq_ConvertPBigintFrom(16, SUBSTRING(@IP, @ib, @ie - @ib)));
		END

		SELECT	@i = @i + 1;
	END

	RETURN @Return;
END
GO
/****** Object:  StoredProcedure [bak].[Apq_BakCfg_Trn]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Apq_BakCfg_Trn]
	@DBName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 日志备份(仅本地)
-- 参数:
@DBName: 数据库名
@BakFolder: 备份路径
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_BakCfg_Trn 'dtxc';
SELECT @rtn;
-- =============================================
1: 备份成功
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @BakFileName nvarchar(256), @BakFileFullName nvarchar(4000)
	,@cmd nvarchar(4000)
	,@sql nvarchar(4000)
	,@ID bigint
	,@BakFolder nvarchar(4000)
	,@FTPFolder nvarchar(4000)
	,@FTPFolderT nvarchar(4000)
	,@FTPFileFullName nvarchar(4000);
SELECT @SPBeginTime=GetDate();
SELECT @BakFolder = '';
SELECT @BakFolder = BakFolder,@FTPFolder = FTPFolder,@FTPFolderT = FTPFolderT
  FROM bak.BakCfg
 WHERE DBName = @DBName;

IF(Len(@BakFolder)>3)
BEGIN
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@BakFolder, 1, LEN(@BakFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolder)>3)
BEGIN
	IF(RIGHT(@FTPFolder,1)<>'\') SELECT @FTPFolder = @FTPFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolderT)>3)
BEGIN
	IF(RIGHT(@FTPFolderT,1)<>'\') SELECT @FTPFolderT = @FTPFolderT+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 记录备份日志
SELECT @BakFileName = @DBName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].trn';
SELECT @BakFileFullName = @BakFolder + @BakFileName;
--SELECT @cmd = '@echo [' + convert(nvarchar,@SPBeginTime,120) + ']' + @BakFileName + '>>' + @BakFolder + '[Log]Apq_Bak.txt'
--EXEC @rtn = xp_cmdshell @cmd;

-- 备份
SELECT @sql = 'BACKUP LOG @DBName TO DISK=@BakFile';
EXEC @rtn = sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000)', @DBName = @DBName, @BakFile = @BakFileFullName;
IF(@@ERROR <> 0 OR @rtn<>0)
BEGIN
	RETURN -1;
END

-- 移动到FTP目录 -----------------------------------------------------------------------------------
IF(Len(@FTPFolderT)>0)
BEGIN
	SELECT @FTPFileFullName = @BakFolder + @BakFileName;
	SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1) + '"';
	EXEC master..xp_cmdshell @cmd;
END
ELSE
BEGIN
	SELECT @FTPFolderT = @BakFolder;
END
SELECT @FTPFileFullName = @FTPFolderT + @BakFileName;
SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1) + '"';
EXEC master..xp_cmdshell @cmd;
-- =================================================================================================

SELECT BakFileName = @BakFileName;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_FileTrans_WriteToHD_ADO]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_FileTrans_WriteToHD_ADO]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存文件
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_FileTrans_WriteToHD_ADO 2, 1;
SELECT @rtn;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'Apq_FileTrans_WriteToHD_ADO'
GO
/****** Object:  StoredProcedure [dbo].[Apq_FileTrans_List]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_FileTrans_List] @ID bigint
AS 
SET NOCOUNT ON ;

SELECT  FileName,FileStream,DBFolder,CFolder
FROM    dbo.FileTrans
WHERE   ID = @ID ;
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 列表"缓存文件"(用于下载)
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 示例:
EXEC dbo.Apq_FileTrans_List 2;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'Apq_FileTrans_List'
GO
/****** Object:  StoredProcedure [dbo].[Apq_FileTrans_Insert_ADO]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_FileTrans_Insert_ADO]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存文件(从本地)
-- 作者: 黄宗银
-- 日期: 2010-03-31
-- 示例:
DECLARE @rtn int, @ID bigint;
EXEC @rtn = dbo.Apq_FileTrans_Insert_ADO @ID out, ''D:\Bak\Wallow[20100331_1657].bak'','''';
SELECT @rtn;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'Apq_FileTrans_Insert_ADO'
GO
/****** Object:  StoredProcedure [dbo].[Apq_FileTrans_Insert]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_FileTrans_Insert]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 保存文件到数据库
-- 作者: 黄宗银
-- 日期: 2010-03-29
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'Apq_FileTrans_Insert'
GO
/****** Object:  StoredProcedure [dbo].[Apq_FileTrans_Delete]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_FileTrans_Delete] @ID bigint
AS 
SET NOCOUNT ON ;

DELETE  dbo.FileTrans
WHERE   ID = @ID ;
RETURN 1 ;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'
-- 删除"缓存文件"
-- 作者: 黄宗银
-- 日期: 2010-04-07
-- 示例:
EXEC dbo.Apq_FileTrans_Delete 2;
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'Apq_FileTrans_Delete'
GO
/****** Object:  StoredProcedure [dbo].[Apq_Ext_Set]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-05
-- 描述: 设置扩展属性值(添加或修改)
-- 示例:
DECLARE @err int, @rtn int, @Msg nvarchar(MAX);
EXEC @rtn = dbo.Apq_Ext_Set @Msg out, '', 0, 'pName', 'zh-cn', '属性值';
SELECT @err = @@ERROR;
SELECT @err, @rtn, @Msg;
-- =============================================
*/
CREATE PROC [dbo].[Apq_Ext_Set]
	 @TableName	nvarchar(256) = ''
	,@ID		bigint = 0
	,@Name		nvarchar(256)

	,@Value	nvarchar(MAX)
AS
SET NOCOUNT ON;

DECLARE @err int, @rtn int, @strID bigint;

UPDATE Apq_Ext
   SET Value = @Value
 WHERE TableName = @TableName AND ID = @ID AND Name = @Name;
IF( @@ROWCOUNT = 0 )
BEGIN
	INSERT Apq_Ext(TableName, ID, Name, Value)
	VALUES(@TableName, @ID, @Name, @Value);
END

RETURN 1;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_Ext_Get]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-01-14
-- 描述: 逆置类型转换,符号扩展(最多支持8字节,bigint类型)
-- 示例:
SELECT dbo.Apq_Ext_Get('',0,'ServerID')
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_Ext_Get](
	 @TableName	nvarchar(256) = N''
	,@ID		bigint = 0
	,@Name		nvarchar(256) = N''
)  
RETURNS nvarchar(max) AS
BEGIN
	DECLARE	@Return nvarchar(max);
	SELECT @Return = Value FROM Apq_Ext	WHERE TableName = @TableName AND ID = @ID AND Name = @Name;
	RETURN @Return;
END
GO
/****** Object:  StoredProcedure [dbo].[Apq_Def_EveryDB]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-01
-- 描述: 将对象定义到所有数据库
-- 功能: 
	1.存储过程	删除后重建
	2.函数		删除后重建
	3.表		删除后重建,导入原数据
-- 示例:
EXEC dbo.Apq_Def_EveryDB DEFAULT, 'Apq_Ext', DEFAULT
-- =============================================
*/
CREATE PROC [dbo].[Apq_Def_EveryDB]
    @SchemaName sysname = 'dbo'
   ,@objName sysname
   ,@ToMsDB tinyint = 0
AS 
SET NOCOUNT ON ;

DECLARE @ExMsg nvarchar(max);
IF(@SchemaName IS NULL) SELECT @SchemaName = 'dbo';
IF(@ToMsDB IS NULL) SELECT @ToMsDB = 0;

DECLARE @objID int, @FullName nvarchar(512);
SELECT @FullName = '[' + @SchemaName + '].[' + @objName + ']';
SELECT @objID = object_id(@FullName);
IF(@objName IS NULL OR @objID IS NULL) RETURN;

DECLARE @sql nvarchar(max),@sqlDB nvarchar(max)
	,@sqlDetect nvarchar(max)	-- 检测目标库是否存在
	,@sqlDrop nvarchar(max)		-- 删除目标库已存在的对象(含保存数据到临时表)
	,@sqlDef nvarchar(max)		-- 对象定义[1.存储过程,2.函数]
	,@sqlGrant nvarchar(max)	-- 目标库授权
	
	-- 3.表
	,@sqlDef_Table nvarchar(max)-- 表定义
	,@sqlDef_TCols nvarchar(max)-- 列定义
	,@sql_Cols nvarchar(max)	-- 来源和目标均具有的列名
	,@sql_SI nvarchar(max)		-- 导入原数据
	,@sqlDef_Index nvarchar(max)-- 索引定义
	,@sqlDrop_Index nvarchar(max)-- [实际不会使用]索引删除语句
	,@sqlDef_PX nvarchar(max)	-- 扩展属性添加
	;

-- 字典表定义 ------------------------------------------------------------------------------
DECLARE @DicFTX TABLE(
	[XName] [sysname],
	[value] nvarchar(max),
	def_exec nvarchar(max)
);
DECLARE @DicFC TABLE(
	[column_id] [int] NOT NULL,
	[name] [sysname],
	user_type_id int NOT NULL,
	[max_length] nvarchar(10) NOT NULL,
	[precision] nvarchar(10) NOT NULL,
	[scale] nvarchar(10) NOT NULL,
	[is_nullable] tinyint,
	[is_identity] tinyint NOT NULL,
	[is_computed] tinyint NOT NULL,
	[typename] [sysname] NOT NULL,
	def_default nvarchar(max),
	def nvarchar(max)
);
DECLARE @DicFCX TABLE(
	[column_id] [int] NOT NULL,
	[XName] [sysname],
	[value] nvarchar(max),
	def_exec nvarchar(max)
);
DECLARE @DicTC TABLE(
	[column_id] [int] NOT NULL,
	[name] [sysname]
);
-- =========================================================================================

-- 1/2.存储过程/函数
IF((OBJECTPROPERTY(@objID,'IsProcedure') = 1 AND OBJECTPROPERTY(@objID,'IsExtendedProc') = 0 AND OBJECTPROPERTY(@objID,'IsReplProc') = 0)
	OR (OBJECTPROPERTY(@objID,'IsInlineFunction') = 1 OR OBJECTPROPERTY(@objID,'IsScalarFunction') = 1 OR OBJECTPROPERTY(@objID,'IsTableFunction') = 1)
)
BEGIN
	SELECT @sqlDef = OBJECT_DEFINITION(@objID);
	IF(@sqlDef IS NULL) RETURN;
END

-- 3.表
ELSE IF(OBJECTPROPERTY(@objID,'IsUserTable') = 1)
BEGIN
	/*
	处理方法:重建表,导入数据,重建索引(键)
	其它:{
		说明: 添加/修改
	}
	type:{
		1: 列
		2: 索引(键)
	}
	op:{
		1: 添加
		2: 修改
		3: 删除
	}
	*/
	
	-- 来源表字典建立 --------------------------------------------------------------------------
	INSERT @DicFTX ( XName,value )
	SELECT name,CONVERT(nvarchar(max),value)
	  FROM sys.fn_listextendedproperty(DEFAULT,'SCHEMA', @SchemaName, 'TABLE', @objName, DEFAULT, DEFAULT);
	
	INSERT @DicFC ( column_id,name,user_type_id,max_length,precision,scale,is_nullable,is_identity,is_computed,typename,def_default )
	SELECT c.column_id,c.name,c.user_type_id,c.max_length,c.precision,c.scale,c.is_nullable,is_identity,is_computed,typename=t.name,d.definition
	  FROM sys.columns c
		INNER JOIN sys.objects o ON c.object_id = o.object_id
		INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
		LEFT JOIN sys.default_constraints d ON d.parent_object_id = o.object_id AND d.parent_column_id = c.column_id
	 WHERE o.object_id = object_id(@FullName);
	
	INSERT @DicFCX ( column_id,XName,value )
	SELECT c.column_id,XName=cx.name,CONVERT(nvarchar(max),value)
	  FROM sys.columns c CROSS APPLY sys.fn_listextendedproperty(DEFAULT,'SCHEMA', @SchemaName, 'TABLE', @objName, 'COLUMN', c.name) cx
	 WHERE c.object_id = object_id(@FullName)
	-- =========================================================================================
	
	-- 计算列定义语句 --------------------------------------------------------------------------
	UPDATE @DicFC
	   SET def = '[' + name + '] [' + typename + ']' + CASE
			WHEN user_type_id BETWEEN 34 AND 61 THEN ''
			WHEN user_type_id IN(98,99,104,122,127,189,241,256) THEN ''
			ELSE '(' + CASE
					WHEN user_type_id = 62 THEN '53'	-- 固定:float(53)
					WHEN user_type_id IN(106,108) THEN precision + ',' + scale
					WHEN max_length = -1 THEN 'max'
					WHEN user_type_id IN(231,239) THEN Convert(nvarchar(max),max_length / 2)
					ELSE max_length
				END
			+ ')'
		END + CASE is_identity WHEN 1 THEN ' IDENTITY' ELSE	-- 固定:IDENTITY(1,1)
			CASE is_nullable WHEN 1 THEN '' ELSE ' NOT NULL' END
			+ CASE WHEN def_default IS NULL THEN '' ELSE ' DEFAULT' + def_default END
		END
	 WHERE is_computed = 0;
	 
	IF (EXISTS(SELECT TOP 1 1 FROM @DicFC WHERE def IS NULL)) RETURN;
	
	SELECT @sqlDef_TCols = '';
	SELECT @sqlDef_TCols = @sqlDef_TCols + def + ','
	  FROM @DicFC
	SELECT @sqlDef_TCols = Left(@sqlDef_TCols,Len(@sqlDef_TCols)-1);
	-- 建表语句
	SELECT @sqlDef_Table = 'CREATE TABLE ' + @FullName + '(' + @sqlDef_TCols + ');';
	-- 加入扩展属性
	UPDATE @DicFTX
	   SET def_exec = 'EXEC sp_addextendedproperty @name = N''' + XName + ''', @value = ''' + value + ''',
@level0type = N''SCHEMA'', @level0name = '''+ @SchemaName + ''',
@level1type = N''TABLE'',  @level1name = '''+ @objName + ''';
';
	UPDATE cx
	   SET cx.def_exec = 'EXEC sp_addextendedproperty @name = N''' + XName + ''', @value = ''' + value + ''',
@level0type = N''SCHEMA'', @level0name = '''+ @SchemaName + ''',
@level1type = N''TABLE'',  @level1name = '''+ @objName + ''',
@level2type = N''COLUMN'', @level2name = '''+ c.name + ''';
'
	  FROM @DicFCX cx INNER JOIN @DicFC c ON cx.column_id = c.column_id;
	SELECT @sqlDef_Table = @sqlDef_Table + def_exec
	  FROM @DicFTX;
	SELECT @sqlDef_Table = @sqlDef_Table + def_exec
	  FROM @DicFCX;
	
	EXEC dbo.Apq_DropIndex @ExMsg = @ExMsg out,
		@Schema_Name = @SchemaName,
		@Table_Name = @objName,
		@sql_Create = @sqlDef_Index out,
		@sql_Drop = @sqlDrop_Index out,
		@DoDrop = 0
	    
	--PRINT @sqlDef_Table; RETURN;
	-- =========================================================================================
END
ELSE RETURN;

DECLARE @DBName sysname, @DB_objID int;
DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT name
  FROM master.sys.databases
 WHERE is_read_only = 0
	--AND database_id = db_id('Agiltron')	-- 只进入测试库
	AND database_id <> db_id() AND database_id <> db_id('tempdb') AND is_read_only = 0 AND state = 0
	AND (@ToMsDB <> 0 OR database_id > 4);
	
OPEN @csr;
FETCH NEXT FROM @csr INTO @DBName;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	-- 1.存储过程
	IF(OBJECTPROPERTY(@objID,'IsProcedure') = 1 AND OBJECTPROPERTY(@objID,'IsExtendedProc') = 0 AND OBJECTPROPERTY(@objID,'IsReplProc') = 0)
	BEGIN
		SELECT @sqlDef = OBJECT_DEFINITION(@objID);
		IF(@sqlDef IS NULL) BREAK;
		SELECT @DB_objID = NULL,@sqlGrant = '';
		SELECT @sqlDetect = 'SELECT @DB_objID = object_id(@FullName)';
		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDetect, N''@DB_objID int out,@FullName nvarchar(512)'',@DB_objID = @DB_objID out,@FullName = @FullName';
		EXEC sp_executesql @sqlDB,N'@sqlDetect nvarchar(max),@DB_objID int out,@FullName nvarchar(512)',@sqlDetect = @sqlDetect, @DB_objID = @DB_objID OUT,@FullName = @FullName
		IF(@DB_objID IS NOT NULL)
		BEGIN
			-- 计算授权语句
			SELECT @sqlDetect = '
SELECT @sqlGrant = @sqlGrant + ''
GRANT EXEC ON '+ @FullName + ' TO ['' + user_name(grantee_principal_id) + '']'' + CASE state WHEN ''W'' THEN '' WITH GRANT OPTION'' ELSE '''' END
  FROM sys.database_permissions
 WHERE type = ''EX'' AND permission_name = ''EXECUTE'' AND state IN (''G'',''W'') AND major_id = object_id(@FullName)';
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDetect, N''@sqlDetect nvarchar(max),@sqlGrant nvarchar(max) out, @FullName nvarchar(512)''
				,@sqlDetect = @sqlDetect,@sqlGrant = @sqlGrant out,@FullName = @FullName';
			EXEC sp_executesql @sqlDB,N'@sqlDetect nvarchar(max),@sqlGrant nvarchar(max) out,@FullName nvarchar(512)',@sqlDetect = @sqlDetect,@sqlGrant = @sqlGrant OUT,@FullName = @FullName

			SELECT @sqlDrop = 'DROP PROC ' + @FullName;
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDrop';
			EXEC sp_executesql @sqlDB,N'@sqlDrop nvarchar(max)',@sqlDrop = @sqlDrop
		END

		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDef';
		EXEC sp_executesql @sqlDB,N'@sqlDef nvarchar(max)',@sqlDef = @sqlDef

		-- 授权
		IF(Len(@sqlGrant)>1)
		BEGIN
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlGrant';
			EXEC sp_executesql @sqlDB,N'@sqlGrant nvarchar(max)',@sqlGrant = @sqlGrant
		END
	END
	
	-- 2.函数
	IF(OBJECTPROPERTY(@objID,'IsInlineFunction') = 1 OR OBJECTPROPERTY(@objID,'IsScalarFunction') = 1 OR OBJECTPROPERTY(@objID,'IsTableFunction') = 1)
	BEGIN
		SELECT @sqlDef = OBJECT_DEFINITION(@objID);
		IF(@sqlDef IS NULL) BREAK;
		SELECT @DB_objID = NULL,@sqlGrant = '';
		SELECT @sqlDetect = 'SELECT @DB_objID = object_id(@FullName)';
		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDetect, N''@DB_objID int out,@FullName nvarchar(512)'',@DB_objID = @DB_objID out,@FullName = @FullName';
		EXEC sp_executesql @sqlDB,N'@sqlDetect nvarchar(max),@DB_objID int out,@FullName nvarchar(512)',@sqlDetect = @sqlDetect, @DB_objID = @DB_objID OUT,@FullName = @FullName
		IF(@DB_objID IS NOT NULL)
		BEGIN
			-- 计算授权语句
			SELECT @sqlDetect = '
SELECT @sqlGrant = @sqlGrant + ''
GRANT EXEC ON '+ @FullName + ' TO ['' + user_name(grantee_principal_id) + '']'' + CASE state WHEN ''W'' THEN '' WITH GRANT OPTION'' ELSE '''' END
  FROM sys.database_permissions
 WHERE type = ''EX'' AND permission_name = ''EXECUTE'' AND state IN (''G'',''W'') AND major_id = object_id(@FullName)';
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDetect, N''@sqlDetect nvarchar(max),@sqlGrant nvarchar(max) out,@FullName nvarchar(512)''
				,@sqlDetect = @sqlDetect,@sqlGrant = @sqlGrant out,@FullName = @FullName';
			EXEC sp_executesql @sqlDB,N'@sqlDetect nvarchar(max),@sqlGrant nvarchar(max) out,@FullName nvarchar(512)',@sqlDetect = @sqlDetect,@sqlGrant = @sqlGrant OUT,@FullName = @FullName

			SELECT @sqlDrop = 'DROP FUNCTION ' + @FullName;
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDrop';
			EXEC sp_executesql @sqlDB,N'@sqlDrop nvarchar(max)',@sqlDrop = @sqlDrop
		END

		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDef';
		EXEC sp_executesql @sqlDB,N'@sqlDef nvarchar(max)',@sqlDef = @sqlDef

		-- 授权
		IF(Len(@sqlGrant)>1)
		BEGIN
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlGrant';
			EXEC sp_executesql @sqlDB,N'@sqlGrant nvarchar(max)',@sqlGrant = @sqlGrant
		END
	END

	-- 3.表
	IF(OBJECTPROPERTY(@objID,'IsUserTable') = 1)
	BEGIN
		DELETE @DicTC;
		SELECT @DB_objID = NULL,@sqlGrant = '';
		SELECT @sqlDetect = 'SELECT @DB_objID = object_id(@FullName)';
		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDetect, N''@DB_objID int out,@FullName nvarchar(512)'',@DB_objID = @DB_objID out,@FullName = @FullName';
		EXEC sp_executesql @sqlDB,N'@sqlDetect nvarchar(max),@DB_objID int out,@FullName nvarchar(512)',@sqlDetect = @sqlDetect, @DB_objID = @DB_objID OUT,@FullName = @FullName
		IF(@DB_objID IS NOT NULL)
		BEGIN
			-- 目标表字典建立 --------------------------------------------------------------------------
			SELECT @sql = '
SELECT c.column_id,c.name
  FROM .sys.columns c
 WHERE c.object_id = object_id(@FullName);
';
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sql, N''@FullName nvarchar(512)'',@FullName = @FullName';
			INSERT @DicTC ( column_id,name )
			EXEC sp_executesql @sqlDB,N'@sql nvarchar(max),@FullName nvarchar(512)',@sql = @sql, @FullName = @FullName
			-- =========================================================================================
			
			-- 目标表数据存入临时表后删除表
			SELECT @sqlDrop = '
--TRUNCATE TABLE [' + @SchemaName + '].' + @objName +'_apqtmpt;
--DROP TABLE [' + @SchemaName + '].' + @objName +'_apqtmpt;
SELECT * INTO [' + @SchemaName + '].' + @objName +'_apqtmpt FROM ' + @FullName + ';
TRUNCATE TABLE ' + @FullName + ';
DROP TABLE ' + @FullName;
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDrop';
			EXEC sp_executesql @sqlDB,N'@sqlDrop nvarchar(max)',@sqlDrop = @sqlDrop
		END
		
		-- 目标库建表(不含索引/键) -----------------------------------------------------------------
		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDef_Table';
		EXEC sp_executesql @sqlDB,N'@sqlDef_Table nvarchar(max)',@sqlDef_Table = @sqlDef_Table
		-- =========================================================================================
		
		-- 导入原数据(按列名保留),删除临时表 -------------------------------------------------------
		IF(@DB_objID IS NOT NULL)
		BEGIN
			SELECT @sql_Cols = '';
			SELECT @sql_Cols = @sql_Cols + '[' + t.name + '],'
			  FROM @DicFC f INNER JOIN @DicTC t ON f.name = t.name;
			SELECT @sql_Cols = Left(@sql_Cols,Len(@sql_Cols)-1);
			IF(EXISTS(SELECT TOP 1 1 FROM @DicFC WHERE is_identity = 1)
				AND EXISTS(SELECT TOP 1 1 FROM @DicFC f INNER JOIN @DicTC t ON f.name = t.NAME WHERE f.is_identity = 1))
			BEGIN
				SELECT @sql_SI = 'SET IDENTITY_INSERT ' + @FullName + ' ON';
			END
			ELSE
			BEGIN
				SELECT @sql_SI = '';
			END
			SELECT @sql_SI = @sql_SI + '
INSERT ' + @FullName + '(' + @sql_Cols + ')
SELECT ' + @sql_Cols + '
  FROM [' + @SchemaName + '].' + @objName +'_apqtmpt;
TRUNCATE TABLE [' + @SchemaName + '].' + @objName +'_apqtmpt;
DROP TABLE [' + @SchemaName + '].' + @objName +'_apqtmpt;
';
			IF(EXISTS(SELECT TOP 1 1 FROM @DicFC WHERE is_identity = 1)
				AND EXISTS(SELECT TOP 1 1 FROM @DicFC f INNER JOIN @DicTC t ON f.name = t.NAME WHERE f.is_identity = 1))
			BEGIN
				SELECT @sql_SI = @sql_SI + 'SET IDENTITY_INSERT ' + @FullName + ' OFF';
			END
			SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sql_SI';
			EXEC sp_executesql @sqlDB,N'@sql_SI nvarchar(max)',@sql_SI = @sql_SI
		END
		-- =========================================================================================
		
		-- 创建索引(键) ----------------------------------------------------------------------------
		SELECT @sqlDB = 'EXEC [' + @DBName + ']..sp_executesql @sqlDef_Index';
		EXEC sp_executesql @sqlDB,N'@sqlDef_Index nvarchar(max)',@sqlDef_Index = @sqlDef_Index
		-- =========================================================================================
	END

	FETCH NEXT FROM @csr INTO @DBName;
END

Quit:
CLOSE @csr;
GO
/****** Object:  StoredProcedure [dbo].[Apq_DataTrans]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_DataTrans]
	 @TransName	nvarchar(256)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-08-11
-- 描述: 数据传送(条件为OR)
-- 替换: 对STMT进行
^LastID$: 上一次传送的最大编号
^LastTime$: 上一次传送数据的最后时间
^MaxID$: 本次传送最大编号
^MaxTime$: 本次传送最后时间
-- 参数:
@TransName: 传送名
@TransMethod: 传送方法{1:BCP queryout & in,2:BCP out & in,3:远程SP,4:LinkServer,5:BCP queryout & FTP,6:BCP out & FTP}
@STMT: 获取源数据语句(3时为实参列表)
@SrvName: 目标服名(可为IP,端口)
@DBName: 目标数据库名
@SchemaName: 架构名
@SPTName: 存储过程名 或 表名(4时可含列名)
@LastID: 上一次传送的最大编号
@LastTime: 上一次传送数据的最后时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_DataTrans 'GameActor';
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

DECLARE @Return int, @rtn int, @SPBeginTime datetime, @cmd nvarchar(4000), @sql nvarchar(4000), @strSPBeginTimeTrim nvarchar(20);
SELECT @SPBeginTime=GetDate();
SELECT @strSPBeginTimeTrim = REPLACE(REPLACE(REPLACE(CONVERT(nvarchar(19),@SPBeginTime,120),'-',''),':',''),' ','_');

DECLARE @TransMethod tinyint
	,@Detect nvarchar(4000)
	,@STMT nvarchar(4000)
	,@SrvName nvarchar(256)
	,@DBName nvarchar(256)
	,@SchemaName nvarchar(256)
	,@SPTName nvarchar(256)
	,@LastID bigint
	,@LastTime datetime
	,@STMTMax nvarchar(4000)
	,@U nvarchar(256)
	,@P nvarchar(256)
	
	,@sqlSTMT nvarchar(4000)
	,@MaxID bigint
	,@MaxTime datetime
	;
SELECT @TransMethod = TransMethod, @Detect = Detect, @STMT = STMT, @SrvName = SrvName, @DBName = DBName, @SchemaName = SchemaName, @SPTName = SPTName
	,@LastID = LastID, @LastTime = LastTime, @STMTMax = STMTMax, @U = U, @P = P
  FROM dbo.DTSConfig
 WHERE TransName = @TransName;
IF(@@ROWCOUNT = 0)
BEGIN
	RETURN -1;
END

-- 检测数据(通过才发送)
IF(LEN(@Detect)>1)
BEGIN
	EXEC @rtn = sp_executesql @Detect;
	IF(@@ERROR <> 0 OR @rtn <> 1)
	BEGIN
		RETURN -2;
	END
END

-- 计算中间文件名,传送数据ID和时间范围
DECLARE @BcpFile nvarchar(4000), @strLastID nvarchar(50), @strMaxID nvarchar(50), @qLastTime nvarchar(21), @qMaxTime nvarchar(21);
SELECT @BcpFile = 'D:\' + @TransName + '[' + @strSPBeginTimeTrim + '].txt'
	,@qLastTime = '''' + CONVERT(nvarchar(19),@LastTime,120) + ''''
	,@strLastID = @LastID;
IF(LEN(@STMTMax) > 1)
BEGIN
	EXEC sp_executesql @STMTMax, N'@MaxID bigint out, @MaxTime datetime out'
		,@MaxID = @MaxID out, @MaxTime = @MaxTime out;

	SELECT @strMaxID = @MaxID, @qMaxTime = '''' + CONVERT(nvarchar(19),@MaxTime,120) + '''';
END
-- 防止替换结果为NULL
IF(@strLastID IS NULL) SELECT @strLastID = '';
IF(@strMaxID IS NULL) SELECT @strMaxID = '';
IF(@qLastTime IS NULL) SELECT @qLastTime = '';
IF(@qMaxTime IS NULL) SELECT @qMaxTime = '';

SELECT @sqlSTMT = REPLACE(REPLACE(REPLACE(REPLACE(@STMT,'^LastID$',@strLastID),'^MaxID$',@strMaxID),'^LastTime$',@qLastTime),'^MaxTime$',@qMaxTime);

-- BCP queryout & in
IF(@TransMethod = 1)
BEGIN
	-- BCP queryout
	SELECT @cmd = 'bcp "' + @sqlSTMT + '" queryout "' + @BcpFile + N'" -c -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- BCP in
	SELECT @cmd = 'bcp "' + @DBName + '.' + @SchemaName + '.' + @SPTName + '" in "' + @BcpFile + '" -c -r~*$ -S' + @SrvName + ' -U' + @U + ' -P' + @P;
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END
END

-- BCP out & in
IF(@TransMethod = 2)
BEGIN
	-- BCP out
	SELECT @cmd = 'bcp "' + @STMT + '" out "' + @BcpFile + N'" -c -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- BCP in
	SELECT @cmd = 'bcp "' + @DBName + '.' + @SchemaName + '.' + @SPTName + '" in "' + @BcpFile + '" -c -r~*$ -S' + @SrvName + ' -U' + @U + ' -P' + @P;
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END
END

-- 远程SP
IF(@TransMethod = 3)
BEGIN
	SELECT @sql = @SrvName + '.' + @DBName + '.' + @SchemaName + '.' + @SPTName + ' ' + @STMT;
	EXEC sp_executesql @sql, N'@LastID bigint, @LastTime datetime, @MaxID bigint, @MaxTime datetime'
		,@LastID = @LastID, @LastTime = @LastTime, @MaxID = @MaxID, @MaxTime = @MaxTime;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		RETURN -1;
	END
END

-- LinkServer
IF(@TransMethod = 4)
BEGIN
	SELECT @sql = 'INSERT ' + @SrvName + '.' + @DBName + '.' + @SchemaName + '.' + @SPTName + ' ' + @STMT;
	EXEC sp_executesql @sql, N'@LastID bigint, @LastTime datetime, @MaxID bigint, @MaxTime datetime'
		,@LastID = @LastID, @LastTime = @LastTime, @MaxID = @MaxID, @MaxTime = @MaxTime;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		RETURN -1;
	END
END

-- BCP queryout & FTP
IF(@TransMethod = 5)
BEGIN
	-- BCP queryout
	SELECT @cmd = 'bcp "' + @sqlSTMT + '" queryout "' + @BcpFile + N'" -c -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- 记录FTP开始时间
	UPDATE dbo.DTSConfig
	   SET TodayBeginTime = GETDATE()
	 WHERE TransName = @TransName;

	-- FTP
	SELECT @cmd = 'echo open ' + @SrvName + '>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo user ' + @U + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo ' + @P + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;
	--SELECT @cmd = 'echo cd DownLoad\QQHX\UserActor>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--EXEC master..xp_cmdshell @cmd;
	IF(Len(@SchemaName) > 1)
	BEGIN
		SELECT @cmd = 'echo cd ' + @SchemaName + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
		EXEC master..xp_cmdshell @cmd;
	END
	IF(Len(@SPTName) > 1)
	BEGIN
		DECLARE @ftpFileName1 nvarchar(256);
		EXEC sp_executesql @SPTName, N'@ftpFileName nvarchar(256) out', @ftpFileName = @ftpFileName1 out;
		SELECT @cmd = 'echo put "'+ @BcpFile +'" "' + @ftpFileName1 + '">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	ELSE
	BEGIN
		SELECT @cmd = 'echo put "'+ @BcpFile +'">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo bye>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;

	CREATE TABLE #t1(s nvarchar(4000));
	SELECT @cmd = 'ftp -i -n -s:D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--SELECT @cmd;
	INSERT #t1 EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		DROP TABLE #t1;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	SELECT * FROM #t1;
	IF(EXISTS(SELECT TOP 1 1 FROM #t1 WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%'))
	BEGIN
		DROP TABLE #t1;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	DROP TABLE #t1;
	-- 删除FTP命令文件
	SELECT @cmd = 'del "D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp" /q';
	EXEC master..xp_cmdshell @cmd;
END

-- BCP out & FTP
IF(@TransMethod = 6)
BEGIN
	-- BCP out
	SELECT @cmd = 'bcp "' + @sqlSTMT + '" queryout "' + @BcpFile + N'" -c -r~*$ -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		SELECT @Return = -1;
		GOTO DelFile;
	END

	-- 记录FTP开始时间
	UPDATE dbo.DTSConfig
	   SET TodayBeginTime = GETDATE()
	 WHERE TransName = @TransName;

	-- FTP
	SELECT @cmd = 'echo open ' + @SrvName + '>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo user ' + @U + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo ' + @P + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;
	--SELECT @cmd = 'echo cd DownLoad\QQHX\UserActor>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--EXEC master..xp_cmdshell @cmd;
	IF(Len(@SchemaName) > 1)
	BEGIN
		SELECT @cmd = 'echo cd ' + @SchemaName + '>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
		EXEC master..xp_cmdshell @cmd;
	END
	IF(Len(@SPTName) > 1)
	BEGIN
		DECLARE @ftpFileName2 nvarchar(256);
		EXEC sp_executesql @SPTName, N'@ftpFileName nvarchar(256) out', @ftpFileName = @ftpFileName2 out;
		SELECT @cmd = 'echo put "'+ @BcpFile +'" "' + @ftpFileName2 + '">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	ELSE
	BEGIN
		SELECT @cmd = 'echo put "'+ @BcpFile +'">>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	END
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo bye>>D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	EXEC master..xp_cmdshell @cmd;

	CREATE TABLE #t2(s nvarchar(4000));
	SELECT @cmd = 'ftp -i -n -s:D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp';
	--SELECT @cmd;
	INSERT #t2 EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		DROP TABLE #t2;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	SELECT * FROM #t2;
	IF(EXISTS(SELECT TOP 1 1 FROM #t2 WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%'))
	BEGIN
		DROP TABLE #t2;
		SELECT @Return = -1;
		GOTO DelFile;
	END
	DROP TABLE #t2;
	-- 删除FTP命令文件
	SELECT @cmd = 'del "D:\ftp_' + @TransName + '[' + @strSPBeginTimeTrim + '].scp" /q';
	EXEC master..xp_cmdshell @cmd;
END

-- 记录已成功上传日期
UPDATE dbo.DTSConfig
   SET _Time = GETDATE(), LastTransTime = @SPBeginTime, LastID = @MaxID, LastTime = @MaxTime
 WHERE TransName = @TransName;
SELECT @Return = 1;

-- 删除中间文件
DelFile:
SELECT @cmd = 'del "' + @BcpFile + '" /q';
EXEC master..xp_cmdshell @cmd;

RETURN @Return;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_CreateSqlON]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-05-17
-- 描述: 创建对象的SQL表示串
-- 示例:
SELECT dbo.Apq_CreateSqlON(16.3)
SELECT dbo.Apq_CreateSqlON('16.3')
SELECT dbo.Apq_CreateSqlON(0x16)
DECLARE @V_dt datetime
SELECT @V_dt = getdate();
SELECT dbo.Apq_CreateSqlON(@V_dt)
DECLARE @V_un uniqueidentifier
SELECT @V_un = newid();
SELECT dbo.Apq_CreateSqlON(@V_un)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_CreateSqlON](
	@Value sql_variant
)
RETURNS nvarchar(max)
AS
BEGIN
	IF(@Value IS NULL) RETURN 'NULL';
	
	DECLARE @Vr nvarchar(max), @BaseType sysname, @Precision int, @Scale int, @MaxLength int;
	SELECT @BaseType = Convert(sysname,SQL_VARIANT_PROPERTY(@Value,'BaseType'))
		,@Precision = Convert(int,SQL_VARIANT_PROPERTY(@Value,'Precision'))
		,@Scale = Convert(int,SQL_VARIANT_PROPERTY(@Value,'Scale'))
		,@MaxLength = Convert(int,SQL_VARIANT_PROPERTY(@Value,'MaxLength'))
	
	IF(Charindex('binary',@BaseType) > 0)
	BEGIN
		DECLARE @V_Binary varbinary(max);
		SELECT @V_Binary = CONVERT(varbinary(max),@Value);
		SELECT @Vr = '0x' + dbo.Apq_ConvertVarBinary_HexStr(@V_Binary);
	END
	ELSE IF(Charindex('char',@BaseType) > 0)
	BEGIN
		DECLARE @V_Char nvarchar(max);
		SELECT @V_Char = CONVERT(nvarchar(max),@Value);
		SELECT @Vr = '''' + REPLACE(@V_Char,'''','''''') + '''';
	END
	ELSE IF(Len(@BaseType)>=8 AND Right(@BaseType,8)='datetime')
	BEGIN
		SELECT @Vr = '''' + Convert(nvarchar(50),@Value,121) + '''';
	END
	ELSE IF(@BaseType = 'uniqueidentifier')
	BEGIN
		SELECT @Vr = '''' + Convert(nvarchar(max),@Value) + '''';
	END
	ELSE
		SELECT @Vr = Convert(nvarchar(max),@Value);
	
	Quit:
	RETURN @Vr;
END
GO
/****** Object:  StoredProcedure [dbo].[Apq_Identifier]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_Identifier]
	 @ExMsg nvarchar(MAX) out

	,@Name	nvarchar(256)	-- 自增名
	,@Count	bigint = 1		-- 增长次数

	,@Next	bigint = 1 out	-- 可以使用的首个值
AS
SET NOCOUNT ON;
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-02-26
-- 描述: 自增, 范围:(Init, Limit]
-- 示例:
DECLARE	@err int, @rtn int, @Next bigint, @ExMsg nvarchar(MAX);
EXEC @rtn = dbo.Apq_Identifier @ExMsg out, N'Apq_String', 1, @Next out;
SELECT @err = @@ERROR;
SELECT @err, @rtn, @Next, @ExMsg;
-- =============================================
10: 无可分配值
11: 超出当前自增范围
*/

SELECT @Next = ISNULL(@Next,1);

DECLARE	@ID bigint, @Limit bigint, @Inc bigint, @End bigint;
SELECT @Limit = 9223372034707292160;

-- 查找第一条用于分配ID的行
SELECT TOP 1 @ID = ID
  FROM Apq_ID
 WHERE Name = @Name AND State = 0;
IF(@@ROWCOUNT = 0)
BEGIN
	IF(EXISTS(SELECT TOP 1 1 FROM Apq_ID WHERE Name = @Name AND State = 1))
	BEGIN
		SELECT @ExMsg = '自增名"' + @Name + '"已无可分配值!';
		RETURN -1;
	END
	ELSE
	BEGIN
		-- 提取自动初始化行,失败则不能分配
		UPDATE Apq_ID SET @ID = ID, State = 0 WHERE Name = @Name AND State = 2;
		IF(@@ROWCOUNT = 0)
		BEGIN
			SELECT @ExMsg = '自增名"' + @Name + '"未配置!';
			RETURN -1;
		END
	END
END

-- 尝试分配ID
UPDATE Apq_ID
   SET _Time = getdate(), @Limit = Limit, @Inc = Inc, @Next = Inc + Crt, @End = Crt = Inc * @Count + Crt
 WHERE ID = @ID;

IF((@Inc > 0 AND @End > @Limit) OR (@Inc < 0 AND @End < @Limit))
BEGIN
	UPDATE Apq_ID
	   SET State = 1
	 WHERE ID = @ID;

	SELECT @ExMsg = '超出当前自增范围,请重试!';
	RETURN -1;
END

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_ID_Reset]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_ID_Reset]
	@Name	nvarchar(256)
AS
SET NOCOUNT ON;
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-05-25
-- 描述: 操作 Apq_ID,将指定 Name 的正常行的当前值重置为初始值
-- 示例:
INSERT Apq_ID(Name, Init, Limit, Inc) VALUES('GameUser', 40000000, 60000000, 1);
DECLARE	@rtn int;
EXEC @rtn = dbo.Apq_ID_Reset 'GameUser';
SELECT	@rtn;
-- =============================================
*/

UPDATE Apq_ID
   SET Crt = Init
 WHERE State = 0
	AND (@Name = '' OR Name = @Name);
GO
/****** Object:  StoredProcedure [dbo].[Apq_ID_Recofig]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Apq_ID_Recofig]
AS
SET NOCOUNT ON;
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-02-26
-- 描述: 操作 Apq_ID, 将正常行的当前值正确配置, 请确保在 配置启用前 或 连接少的时候 调用
-- 示例:
INSERT Apq_ID(Name, Init, Limit, Inc) VALUES('GameUser', 40000000, 60000000, 1);
DECLARE	@rtn int;
EXEC @rtn = dbo.Apq_ID_Recofig;
SELECT	@rtn;
-- =============================================
*/

UPDATE Apq_ID
   SET Crt = Init
 WHERE State = 0
	AND NOT (Inc > 0 AND Init <= Crt AND Crt < Limit)
	AND NOT (Inc < 0 AND Limit < Crt AND Crt <= Init);
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_RConvertVarBinary8X_BigInt]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-02-19
-- 描述: 逆置类型转换,符号扩展(最多支持8字节,bigint类型)
-- 示例:
SELECT dbo.Apq_RConvertVarBinary8X_BigInt(0x01F1)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_RConvertVarBinary8X_BigInt](
	@Input	varbinary(8)
)RETURNS bigint AS
BEGIN
	IF(DATALENGTH(@Input) < 8)
	BEGIN
		IF((128 & SubString(@Input,DataLength(@Input),1)) > 0)
		BEGIN
			-- 符号扩展
			SELECT @Input = @Input + 0xFFFFFFFFFFFFFF/*7字节*/
		END
	END
	
	RETURN dbo.Apq_VarBinary_Reverse(@Input);
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_RConvertVarBinary8_BigInt]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-05-07
-- 描述: 逆置类型转换(最多支持8字节,bigint类型)
-- 示例:
SELECT dbo.Apq_RConvertVarBinary8_BigInt(0x0100)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_RConvertVarBinary8_BigInt](
	@Input	varbinary(8)
)RETURNS bigint AS
BEGIN
	DECLARE	@bin varbinary(8), @rtn bigint;
	SELECT @bin = dbo.Apq_VarBinary_Reverse(@Input);
	SELECT @rtn = Convert(bigint, @bin);
	RETURN @rtn;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_RConvertBigInt_VarBinary8]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-05-07
-- 描述: 逆置类型转换(最多支持8字节,bigint类型)
-- 示例:
SELECT dbo.Apq_RConvertBigInt_VarBinary8(0x0100)
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_RConvertBigInt_VarBinary8](
	@Input	bigint,
	@Length	tinyint	-- [1,8]
)RETURNS varbinary(8) AS
BEGIN
	DECLARE	@bin varbinary(8), @rtn varbinary(8);
	SELECT @bin = Convert(varbinary, @Input);
	SELECT @bin = dbo.Apq_VarBinary_Reverse(@bin);
	SELECT @rtn = SubString(@bin,1, @Length);
	RETURN @rtn;
END
GO
/****** Object:  StoredProcedure [dbo].[Prws_Alarm]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-03
-- 描述: 读取报警列表信息
-- 示例:
EXEC dbo.Prws_Alarm DEFAULT, 'Apq_ID', DEFAULT
-- =============================================
*/
CREATE PROC [dbo].[Prws_Alarm]
    @BTime datetime	-- 开始时间
   ,@ETime datetime OUT	-- 结束时间
AS 
SET NOCOUNT ON ;

DECLARE @Now datetime;
SELECT @Now = getdate();

IF(@BTime IS NULL) SELECT @BTime = Dateadd(dd,-1,@Now);

SELECT @ETime = max(_InTime) FROM dbo.Log_Apq_Alarm WHERE [_InTime] >= @BTime;
IF(@ETime IS NULL) SELECT @ETime = @Now

SELECT ID, _InTime, Type, Severity, Msg
  FROM dbo.Log_Apq_Alarm(NOLOCK)
 WHERE [_InTime] > @BTime AND [_InTime] <= @ETime;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_BakCfg_Init]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_BakCfg_Init]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 备份作业(仅本地)的初始化
-- 功能: 计算下一次备份作业启动后对"哪些"数据库执行"何种"备份
-- 参数:
@JobTime: 作业启动时间
@DBName: 数据库名
@FullCycle: 完整备份周期(天)
@FullTime: 完整备份时间(每次日期与周期相关)
@TrnCycle: 日志备份周期(分钟,请尽量作业周期的倍数)
@PreFullTime: 上一次完整备份时间
@PreBakTime: 上一次备份时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Job_Apq_BakCfg_Init;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@FullCycle int,			-- 完整备份周期(天)
	@FullTime smalldatetime,-- 完整备份时间(每次日期与周期相关)
	@TrnCycle smallint,		-- 日志备份周期(分钟,请尽量作业周期的倍数)
	@PreFullTime datetime,	-- 上一次完整备份时间
	@PreBakTime datetime	-- 上一次备份时间
	
	,@rtn int, @sql nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@ProductVersion	decimal	-- 服务器版本(取前两字符)
	,@ForceFull tinyint
	;
SELECT @JobTime=GetDate(), @ForceFull = 0
	,@ProductVersion = CONVERT(decimal,LEFT(Convert(nvarchar,SERVERPROPERTY('ProductVersion')),2));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,DBName,FullCycle,FullTime,TrnCycle,PreFullTime,PreBakTime
  FROM bak.BakCfg
 WHERE Enabled = 1 AND (State & 1) = 0;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@FullCycle,@FullTime,@TrnCycle,@PreFullTime,@PreBakTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 2000以上版本判断是否已完整备份过,没有则强制完整备份
	SELECT @ForceFull = 0;
	IF(@ProductVersion > 8 AND DATABASEPROPERTYEX(@DBName,'Recovery')<>'SIMPLE')
	BEGIN
		SELECT @sql = 'SELECT @ForceFull = 0;
IF(EXISTS(SELECT * FROM sys.database_recovery_status WHERE last_log_backup_lsn IS NULL AND database_id = DB_ID(@DBName)))
SELECT @ForceFull = 1;';
		EXEC sp_executesql @sql,N'@ForceFull int out, @DBName nvarchar(256)', @ForceFull = @ForceFull out, @DBName = @DBName;
	END
	
	IF(@ForceFull = 1 OR @PreFullTime IS NULL
		OR DATEDIFF(n, @PreFullTime, @JobTime) >= 1440 * @FullCycle
	)BEGIN
		UPDATE [bak].BakCfg SET ReadyAction = ReadyAction | 1 WHERE ID = @ID;
	END
	ELSE IF(DATABASEPROPERTYEX(@DBName,'Recovery')<>'SIMPLE' AND DateDiff(N,@PreBakTime,@JobTime) >= @TrnCycle - 1)
	BEGIN
		UPDATE [bak].BakCfg SET ReadyAction = ReadyAction | 2 WHERE ID = @ID;
	END

	FETCH NEXT FROM @csr INTO @ID,@DBName,@FullCycle,@FullTime,@TrnCycle,@PreFullTime,@PreBakTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_Bak_Raiserror]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_Bak_Raiserror]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-05-18
-- 描述: 备份系列作业状态检查,出现异常状况则抛出异常
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Job_Apq_Bak_Raiserror;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

DECLARE @Now datetime;
SELECT @Now = getdate();

IF(EXISTS(SELECT TOP 1 1 FROM bak.BakCfg WHERE Enabled = 1 AND ReadyAction > 0 AND PreBakTime < Dateadd(n,-TrnCycle*8,@Now)))
	RAISERROR('本地备份 出现异常',16,1);

IF(EXISTS(SELECT TOP 1 1 FROM bak.FTP_PutBak WHERE Enabled = 1 AND Len(LastFileName) > 1 AND DATABASEPROPERTYEX(DBName,'Recovery')<>'SIMPLE'
	AND Convert(datetime,Substring(LastFileName,Len(LastFileName)-17,8) + ' ' + Substring(LastFileName,Len(LastFileName)-8,2) + ':' + Substring(LastFileName,Len(LastFileName)-6,2)) < Dateadd(hh,-2,@Now)))
	RAISERROR('向外传送 出现异常',16,1);

IF(EXISTS(SELECT TOP 1 1 FROM bak.RestoreFromFolder WHERE Enabled = 1 AND Len(LastFileName) > 1 AND DATABASEPROPERTYEX(DBName,'Recovery')<>'SIMPLE'
	AND Convert(datetime,Substring(LastFileName,Len(LastFileName)-17,8) + ' ' + Substring(LastFileName,Len(LastFileName)-8,2) + ':' + Substring(LastFileName,Len(LastFileName)-6,2)) < Dateadd(hh,-2,@Now)))
	RAISERROR('还原或删除 出现异常',16,1);
RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_Bak_Init]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_Bak_Init]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 备份作业(仅本地)的初始化
-- 功能: 计算下一次备份作业启动后对"哪些"数据库执行"何种"备份
-- 参数:
@JobTime: 作业启动时间
@DBName: 数据库名
@FullCycle: 完整备份周期(天)
@FullTime: 完整备份时间(每次日期与周期相关)
@TrnCycle: 日志备份周期(分钟,请尽量作业周期的倍数)
@PreFullTime: 上一次完整备份时间
@PreBakTime: 上一次备份时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Job_Apq_Bak_Init;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@FullCycle int,			-- 完整备份周期(天)
	@FullTime smalldatetime,-- 完整备份时间(每次日期与周期相关)
	@TrnCycle smallint,		-- 日志备份周期(分钟,请尽量作业周期的倍数)
	@PreFullTime datetime,	-- 上一次完整备份时间
	@PreBakTime datetime	-- 上一次备份时间
	
	,@rtn int, @sql nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@ProductVersion	decimal	-- 服务器版本(取前两字符)
	,@ForceFull tinyint
	;
SELECT @JobTime=GetDate(), @ForceFull = 0
	,@ProductVersion = CONVERT(decimal,LEFT(Convert(nvarchar,SERVERPROPERTY('ProductVersion')),2));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,DBName,FullCycle,FullTime,TrnCycle,PreFullTime,PreBakTime
  FROM bak.BakCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@FullCycle,@FullTime,@TrnCycle,@PreFullTime,@PreBakTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 2000以上版本判断是否已完整备份过,没有则强制完整备份
	SELECT @ForceFull = 0;
	IF(@ProductVersion > 8 AND DATABASEPROPERTYEX(@DBName,'Recovery')<>'SIMPLE')
	BEGIN
		SELECT @sql = 'SELECT @ForceFull = 0;
IF(EXISTS(SELECT * FROM sys.database_recovery_status WHERE last_log_backup_lsn IS NULL AND database_id = DB_ID(@DBName)))
SELECT @ForceFull = 1;';
		EXEC sp_executesql @sql,N'@ForceFull int out, @DBName nvarchar(256)', @ForceFull = @ForceFull out, @DBName = @DBName;
	END
	
	IF(@ForceFull = 1 OR @PreFullTime IS NULL
		OR DATEDIFF(n, @PreFullTime, @JobTime) >= 1440 * @FullCycle - 2
	)BEGIN
		UPDATE [bak].BakCfg SET ReadyAction = ReadyAction | 1 WHERE ID = @ID;
	END
	ELSE IF(DATABASEPROPERTYEX(@DBName,'Recovery')<>'SIMPLE' AND DateDiff(N,@PreBakTime,@JobTime) >= @TrnCycle - 1)
	BEGIN
		UPDATE [bak].BakCfg SET ReadyAction = ReadyAction | 2 WHERE ID = @ID;
	END

	FETCH NEXT FROM @csr INTO @ID,@DBName,@FullCycle,@FullTime,@TrnCycle,@PreFullTime,@PreBakTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_Bak_FTP_Enqueue]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_Bak_FTP_Enqueue]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-20
-- 描述: 通过FTP队列上传备份文件
-- 示例:
EXEC bak.Job_Apq_Bak_FTP_Enqueue;
-- =============================================
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @cmd nvarchar(4000);
SELECT @SPBeginTime=GetDate();

DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT ID,DBName,LastFileName,FTPSrv,Folder,U,P,FTPFolder,FTPFolderTmp,Num_Full
  FROM bak.FTP_PutBak
 WHERE Enabled = 1;

DECLARE @csrFile CURSOR;
DECLARE @FTPFileName nvarchar(512);
CREATE TABLE #t1(s nvarchar(4000));
CREATE TABLE #t2(s nvarchar(4000));

DECLARE @ID bigint,@DBName nvarchar(256),@LastFileName nvarchar(256),@FTPSrv nvarchar(256),@Folder nvarchar(512)
	,@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512),@Num_Full int;
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	IF(RIGHT(@FTPFolder,1)<>'/') SELECT @FTPFolder = @FTPFolder+'/';
	IF(RIGHT(@FTPFolderTmp,1)<>'/') SELECT @FTPFolderTmp = @FTPFolderTmp+'/';
	SELECT @cmd = 'md ' + LEFT(@Folder,LEN(@Folder)-1);
	EXEC master..xp_cmdshell @cmd;

	-- 传送入队 -----------------------------------------------------------------------------------
	TRUNCATE TABLE #t1;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @Folder + @DBName + '[*].*"';
	INSERT #t1(s) EXEC master..xp_cmdshell @cmd;
	
	TRUNCATE TABLE #t2;
	INSERT #t2(s)
	SELECT s FROM #t1
	 WHERE s > @LastFileName AND Len(s) > Len(@DBName) + 6
		AND Left(s,Len(@DBName)+1) = @DBName+'['
		AND SubString(s,Len(@DBName)+ 15,5) IN ('].bak','].trn')
	 ORDER BY s;
	SET @csrFile = CURSOR FOR
	SELECT s FROM #t2;
	
	INSERT dbo.FTP_SendQueue ( Folder,FileName,Enabled,FTPSrv,U,P,FTPFolder,FTPFolderTmp )
	SELECT @Folder,s,1,@FTPSrv,@U,@P,@FTPFolder,@FTPFolderTmp
	  FROM #t2
	
	SELECT @FTPFileName = NULL;
	SELECT @FTPFileName = Max(s) FROM #t2;
	-- 更新最后文件名
	IF(@FTPFileName IS NOT NULL) UPDATE bak.FTP_PutBak SET _Time = getdate(), LastFileName = @FTPFileName WHERE ID = @ID;
	-- =============================================================================================
	
	NEXT_DB:
	FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
END
CLOSE @csr;

Quit:
DROP TABLE #t1;
DROP TABLE #t2;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_WH]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 维护作业:1.默认值重命名,2.整理索引(如果碎片率低于30％用INDEXDEFRAG，如果高于30％用DBREINDEX)
-- 示例:
EXEC dbo.Apq_WH
-- =============================================
*/
CREATE PROC [dbo].[Apq_WH]
AS
SET NOCOUNT ON ;

DECLARE @sql nvarchar(max),@DBName nvarchar(128)
DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT DBName
  FROM dbo.Cfg_WH
 WHERE Enabled = 1

OPEN @csr
FETCH NEXT FROM @csr INTO @DBName
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @sql = 'EXEC [' + @DBName + ']..Apq_RenameDefault';
	EXEC sp_executesql @sql;
	SELECT @sql = 'EXEC [' + @DBName + ']..Apq_RebuildIdx';
	EXEC sp_executesql @sql;
	SELECT @sql = 'EXEC [' + @DBName + ']..sp_updatestats';
	EXEC sp_executesql @sql;
	
	FETCH NEXT FROM @csr INTO @DBName
END
CLOSE @csr
GO
/****** Object:  StoredProcedure [dbo].[Job_Apq_DataTrans_Loader]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_Apq_DataTrans_Loader]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-08-11
-- 描述: 数据传送作业
-- 参数:
@TransName: 传送名
@TransCycle: 传送周期(天)
@TransTime: 传送时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_DataTrans_Loader;
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

--定义变量
DECLARE @rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@SPBeginTime datetime		-- 存储过程启动时间
	,@Today datetime
	;
SELECT @SPBeginTime=GetDate();
SELECT @Today = DATEADD(DD,0,DATEDIFF(dd,0,@SPBeginTime));

UPDATE DTSConfig
   SET NeedTrans = 1
 WHERE Enabled = 1
	AND (DATEDIFF(MI, LastTransTime, @SPBeginTime) >= 1440 * TransCycle
		OR DateDiff(ss,@SPBeginTime,CONVERT(nvarchar(11),@SPBeginTime,120) + Right(Convert(nvarchar,TransTime,120),8)) BETWEEN -90 AND 209);

-- 检测作业是否卡死在ftp进程上,是则终止ftp进程
UPDATE DTSConfig
   SET KillFtpTime = @SPBeginTime
 WHERE Enabled = 1 AND TransMethod IN (5,6) AND NeedTrans = 1
	AND DATEDIFF(N,TodayBeginTime,@SPBeginTime) > 30
	AND DATEDIFF(N,KillFtpTime,@SPBeginTime) > 60;
IF(@@ROWCOUNT > 0)
BEGIN
	CREATE TABLE #cmd(s nvarchar(4000))
	SELECT @cmd = 'TaskList /nh /fi "Imagename eq ftp.exe"';
	INSERT #cmd
	EXEC @rtn = master..xp_cmdshell @cmd;
	DECLARE @i int, @n int;
	SELECT @i = 0;
	SELECT @n = COUNT(*) FROM #cmd WHERE s IS NOT NULL;
	WHILE(@i < @n)
	BEGIN
		SELECT @cmd = 'TaskKill /im "ftp.exe" /f';
		EXEC master..xp_cmdshell @cmd;
		SELECT @i = @i + 1;
	END
	DROP TABLE #cmd;
END

RETURN 1;
GO
/****** Object:  StoredProcedure [etl].[Job_Etl_BcpIn_Init]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Job_Etl_BcpIn_Init]
	@CfgRowCount	int = 10000	-- 从配置中最多读取的行数
	,@BcpPeriod		datetime = NULL
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-27
-- 描述: 根据ETL配置检查已有文件,并将需要BcpIn的文件插入BcpIn队列
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn_Init;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @CfgRowCount = ISNULL(@CfgRowCount,10000);
SELECT @BcpPeriod = ISNULL(@BcpPeriod,getdate());

DECLARE @rtn int, @SPBeginTime datetime, @BCPDay datetime
	,@yyyy int, @mm int, @dd int, @hh int, @mi int
	,@ww int
	,@yyyyStr nvarchar(50), @mmStr nvarchar(50), @ddStr nvarchar(50), @hhStr nvarchar(50), @miStr nvarchar(50)
	,@wwStr nvarchar(50)
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	;

SELECT @SPBeginTime = getdate();
SELECT @BCPDay = dateadd(dd,0,datediff(dd,0,@BcpPeriod));	-- 数据传送时间(一般当天传送上一周期的全部数据)

DECLARE @ID bigint,
	@EtlName nvarchar(256),	-- 配置名
	@Folder nvarchar(512),	-- 本地文件目录(不含时期)
	@PeriodType int,		-- 时期类型{1:年,2:半年,3:季度,4:月,5:周,6:日,7:时,8:分}
	@FileName nvarchar(256),-- 文件名(前缀)(格式:FileName_SrvID.txt)
	@DBName nvarchar(256),
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@r nvarchar(10),
	@t nvarchar(10)
	
	,@BTime datetime, @ETime datetime, @CTime datetime
	,@FullFolder nvarchar(512)-- 目录(含时期)
	--,@BcpInFullTableName nvarchar(512)-- BcpIn到的完整表名(数据库名.架构名.表名)
	;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@CfgRowCount) ID, EtlName, Folder, PeriodType, FileName, DBName, SchemaName, TName, r, t
  FROM etl.EtlCfg
 WHERE Enabled = 1

DECLARE @DataPeriod datetime;
CREATE TABLE #t(s nvarchar(4000));

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@PeriodType,@FileName,@DBName,@SchemaName,@TName,@r,@t;
WHILE(@@FETCH_STATUS=0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	SELECT @FullFolder = @Folder;
	
	-- 从当前时间计算应该获取的数据时间(往前推一周期,最小为日) -------------------------------------
	SELECT @DataPeriod = dateadd(dd,-1,@BCPDay); -- 默认取前1天
	IF(@PeriodType = 1) SELECT @DataPeriod = dateadd(yyyy,-1,@BCPDay);
	IF(@PeriodType = 2) SELECT @DataPeriod = dateadd(mm,-6,@BCPDay);
	IF(@PeriodType = 3) SELECT @DataPeriod = dateadd(mm,-3,@BCPDay);
	IF(@PeriodType = 4) SELECT @DataPeriod = dateadd(mm,-1,@BCPDay);
	-- =============================================================================================
	
	-- 循环检查数据目录 ----------------------------------------------------------------------------
	SELECT @BTime = @DataPeriod
	SELECT @ETime = @BCPDay
	SELECT @CTime = @BTime;
	
	WHILE(@CTime < @ETime)
	BEGIN
		IF(@PeriodType > 0)
		BEGIN
			SELECT @yyyy = datepart(yyyy,@CTime)
				,@mm = datepart(mm,@CTime)
				,@dd = datepart(dd,@CTime)
				,@hh = datepart(hh,@CTime)
				,@mi = datepart(n,@CTime)
				;
				
			SELECT @yyyyStr = Convert(nvarchar(50),@yyyy)
				,@mmStr = CASE WHEN @mm < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@mm)
				,@ddStr = CASE WHEN @dd < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@dd)
				,@hhStr = CASE WHEN @hh < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@hh)
				,@miStr = CASE WHEN @mi < 10 THEN '0' ELSE '' END + Convert(nvarchar(50),@mi)
				;
				
			IF(@PeriodType = 1) SELECT @FullFolder = @Folder + @yyyyStr;
			IF(@PeriodType IN (2,3,4)) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr;
			IF(@PeriodType = 5) SELECT @FullFolder = @Folder + @yyyyStr + '_' + @wwStr;
			IF(@PeriodType = 6) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr + @ddStr;
			IF(@PeriodType = 7) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr + @ddStr + '_' + @hhStr;
			IF(@PeriodType = 8) SELECT @FullFolder = @Folder + @yyyyStr + @mmStr + @ddStr + '_' + @hhStr + @miStr;
		END
		
		IF(RIGHT(@FullFolder,1)<>'\') SELECT @FullFolder = @FullFolder+'\';
		
		-- 入队 --------------------------------------------------------------------------------
		TRUNCATE TABLE #t;
		SELECT @cmd = 'dir /a:-d/b/o:n "' + @FullFolder + @FileName + '*.txt"';
		--SELECT @cmd;
		INSERT #t(s) EXEC master..xp_cmdshell @cmd;
		
		INSERT etl.BcpInQueue ( EtlName, Folder, FileName, DBName, SchemaName, TName, t, r )
		SELECT @EtlName,@FullFolder,s,@DBName,@SchemaName,@TName,@t,@r
		  FROM #t
		 WHERE Left(s,Len(@FileName)) = @FileName
			AND (@PeriodType = 0 OR NOT EXISTS(SELECT TOP 1 * FROM etl.BcpInQueue t WHERE EtlName = @EtlName AND Folder = @FullFolder AND Left(s,Len(FileName)) = FileName))
		-- =====================================================================================
		
		IF(@PeriodType = 0) SELECT @CTime = @ETime;
		IF(@PeriodType = 1) SELECT @CTime = dateadd(yyyy,1,@CTime);
		IF(@PeriodType = 2) SELECT @CTime = dateadd(yyyy,6,@CTime);
		IF(@PeriodType = 3) SELECT @CTime = dateadd(mm,3,@CTime);
		IF(@PeriodType = 4) SELECT @CTime = dateadd(mm,1,@CTime);
		IF(@PeriodType = 5) SELECT @CTime = dateadd(dd,7,@CTime);
		IF(@PeriodType = 6) SELECT @CTime = dateadd(dd,1,@CTime);
		IF(@PeriodType = 7) SELECT @CTime = dateadd(hh,1,@CTime);
		IF(@PeriodType = 8) SELECT @CTime = dateadd(n,1,@CTime);
	END
	-- ================================================================================================

	FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@PeriodType,@FileName,@DBName,@SchemaName,@TName,@r,@t;
END
CLOSE @csr;

Quit:
DROP TABLE #t;

RETURN 1;
GO
/****** Object:  StoredProcedure [etl].[Job_Etl_BcpIn]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Job_Etl_BcpIn]
	@TransRowCount	int = 10000	-- 从队列中最多读取的行数
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-27
-- 描述: 读取BcpIn队列,执行BcpIn
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_BcpIn_Init 10000;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT @TransRowCount = ISNULL(@TransRowCount,10000);

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	;
	
SELECT @SPBeginTime = getdate();

DECLARE @ID bigint,
	@EtlName nvarchar(256),	-- 配置名
	@Folder nvarchar(512),	-- 本地文件目录(含时期)
	@FileName nvarchar(256),-- 文件名(前缀)(格式:FileName_SrvID.txt)
	@DBName nvarchar(256),
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@r nvarchar(10),
	@t nvarchar(10),
	@DBID int
	
	,@FullTableName nvarchar(512)-- 完整表名(数据库名.架构名.表名)
	;

DECLARE @csr CURSOR
SET @csr = CURSOR FOR
SELECT TOP(@TransRowCount) ID, EtlName, Folder, FileName, DBName, SchemaName, TName, t, r
  FROM etl.BcpInQueue
 WHERE Enabled = 1 AND IsFinished = 0;

DECLARE @sidx int;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@FileName,@DBName,@SchemaName,@TName, @t, @r;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 解析出DBID
	SELECT @sidx = Charindex('[',@FileName);
	IF( @sidx > 0)
	BEGIN
		SELECT @DBID = substring(@FileName,@sidx+1,Charindex(']',@FileName,@sidx)-@sidx-1);
		UPDATE etl.BcpInQueue SET DBID = @DBID WHERE ID = @ID;
	END

	SELECT @FullTableName = '[' + @DBName + '].[' + @SchemaName + '].[' + @TName + ']';

	SELECT @cmd = 'bcp "' + @FullTableName + '" in "' + @Folder + @FileName + '" -c -t' + @t + ' -r' + @r + ' -T';
	--SELECT @cmd;
	EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		GOTO NEXT_File;
	END
	
	Success:
	UPDATE etl.BcpInQueue SET [_Time] = getdate(), IsFinished = 1 WHERE ID = @ID;
	SELECT @cmd = 'del "' + @Folder + @FileName + '" /f /q';
	EXEC xp_cmdshell @cmd;
	
	NEXT_File:
	FETCH NEXT FROM @csr INTO @ID,@EtlName,@Folder,@FileName,@DBName,@SchemaName,@TName, @t, @r;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_FTP_PutBak]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_FTP_PutBak]
	 @TransferID	int	-- 传送者ID
	,@TransRowCount	int	-- 传送行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-05-12
-- 描述: 通过FTP上传备份文件
-- 原因: FTP下载时是先写入临时文件,可能引起空间不足
-- 示例:
EXEC bak.Job_Apq_FTP_PutBak 1,100;
-- =============================================
*/
SET NOCOUNT ON;

IF(@TransferID IS NULL) SELECT @TransferID = 1;
IF(@TransRowCount IS NULL) SELECT @TransRowCount = 100;

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	,@ScpFolder nvarchar(4000)
	,@ScpFileName nvarchar(128)
	,@ScpFullName nvarchar(4000)
	,@LFullName nvarchar(4000)	 -- 临时变量:本地文件全名
	,@FullFileToDel nvarchar(4000)--在此文件之前的文件将被删除
	;
SELECT @SPBeginTime=GetDate(),@ScpFolder = 'D:\Apq_DBA\Scp\'
SELECT @ScpFileName = 'Job_Apq_FTP_PutBak['+LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].scp';
SELECT @ScpFullName = @ScpFolder + @ScpFileName;
IF(RIGHT(@ScpFolder,1)<>'\') SELECT @ScpFolder = @ScpFolder+'\';
SELECT @cmd = 'md ' + LEFT(@ScpFolder,LEN(@ScpFolder)-1);
EXEC master..xp_cmdshell @cmd;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@TransRowCount) ID,DBName,LastFileName,FTPSrv,Folder,U,P,FTPFolder,FTPFolderTmp,Num_Full
  FROM bak.FTP_PutBak
 WHERE Enabled = 1
	AND ((TransferIDCfg = 0 AND TransferIDRun = 0) OR TransferIDCfg = @TransferID)

DECLARE @csrFile CURSOR;
-- 游标内临时变量
DECLARE @FTPFileName nvarchar(512)
	,@DBName_R nvarchar(256)
	;
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));
CREATE TABLE #t2(s nvarchar(4000));

DECLARE @ID bigint,@DBName nvarchar(256),@LastFileName nvarchar(256),@FTPSrv nvarchar(256),@Folder nvarchar(512)
	,@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512),@Num_Full int;
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	UPDATE bak.FTP_PutBak SET [_Time] = getdate(),TransferIDRun = @TransferID WHERE ID = @ID;
	
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	IF(RIGHT(@FTPFolder,1)<>'/') SELECT @FTPFolder = @FTPFolder+'/';
	IF(RIGHT(@FTPFolderTmp,1)<>'/') SELECT @FTPFolderTmp = @FTPFolderTmp+'/';
	SELECT @cmd = 'md ' + LEFT(@Folder,LEN(@Folder)-1);
	EXEC master..xp_cmdshell @cmd;

	-- 传送文件 -----------------------------------------------------------------------------------
	TRUNCATE TABLE #t1;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @Folder + @DBName + '[*].*"';
	INSERT #t1(s) EXEC master..xp_cmdshell @cmd;
	
	TRUNCATE TABLE #t2;
	INSERT #t2(s)
	SELECT s FROM #t1
	 WHERE s > @LastFileName AND Len(s) > Len(@DBName) + 6
		AND Left(s,Len(@DBName)+1) = @DBName+'['
		AND SubString(s,Len(@DBName)+ 15,5) IN ('].bak','].trn')
	 ORDER BY s;
	SET @csrFile = CURSOR FOR
	SELECT s FROM #t2;
	
	SELECT @FTPFileName = NULL;
	OPEN @csrFile;
	FETCH NEXT FROM @csrFile INTO @FTPFileName
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		SELECT @cmd = 'echo open ' + @FTPSrv + '>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo user ' + @U + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo ' + @P + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo cd "' + @FTPFolderTmp + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo lcd "' + LEFT(@Folder,LEN(@Folder)-1) + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
	
		SELECT @cmd = 'echo binary>>"' + @ScpFullName + '"';-- 将文件传输类型设置为二进制
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo put "' + @FTPFileName + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo rename "' + @FTPFileName + '" "' + @FTPFolder + @FTPFileName + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo bye>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'ftp -i -n -s:"' + @ScpFullName + '"';
		--SELECT @cmd;
		TRUNCATE TABLE #t;
		INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
		IF(@@ERROR <> 0 OR @rtn <> 0)
		BEGIN
			CLOSE @csrFile;
			GOTO NEXT_DB;
		END
		SELECT * FROM #t;
		IF(EXISTS(SELECT TOP 1 1 FROM #t WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%' OR s LIKE 'Permission denied%'
				OR s LIKE '%Unable to rename%'))
		BEGIN
			CLOSE @csrFile;
			GOTO NEXT_DB;
		END
		
		-- 更新最后文件名
		IF(@FTPFileName IS NOT NULL) UPDATE bak.FTP_PutBak SET _Time = getdate(), LastFileName = @FTPFileName WHERE ID = @ID;

		NEXT_File:
		FETCH NEXT FROM @csrFile INTO @FTPFileName
	END
	CLOSE @csrFile;
	-- =============================================================================================
	
	-- 删除本地历史文件 ----------------------------------------------------------------------------
	-- 删除保留个数以外的备份文件
	TRUNCATE TABLE #t;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @Folder + @DBName + '[*].*"';
	INSERT #t(s) EXEC master..xp_cmdshell @cmd;
	
	SELECT TOP(@Num_Full) @FullFileToDel = s
	  FROM #t
	 WHERE Len(s) > Len(@DBName) + 6
		AND Left(s,Len(@DBName)+1) = @DBName+'['
		AND SubString(s,Len(@DBName)+ 15,5) = '].bak'
	 ORDER BY s DESC;
	IF(@@ROWCOUNT >= @Num_Full)
	BEGIN
		SELECT @cmd = '';
		SELECT @cmd = @cmd + '&&del /F /Q "' + @Folder + s + '"'
		  FROM #t
		 WHERE s < @FullFileToDel;

		IF(Len(@cmd) > 2)
		BEGIN
			SELECT @cmd = Right(@cmd,Len(@cmd)-2);
			SELECT @cmd;
			EXEC master..xp_cmdshell @cmd;
		END
	END
	-- =============================================================================================
	
	NEXT_DB:
	-- 删除FTP命令文件
	SELECT @cmd = 'del ""' + @ScpFullName + '"" /q';
	EXEC master..xp_cmdshell @cmd;
	-- 恢复空闲状态
	UPDATE bak.FTP_PutBak SET _Time = getdate(), TransferIDRun = 0 WHERE ID = @ID;

	FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@FTPSrv,@Folder,@U,@P,@FTPFolder,@FTPFolderTmp,@Num_Full;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
DROP TABLE #t2;
RETURN 1;
GO
/****** Object:  StoredProcedure [etl].[Job_Etl_Stat]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Job_Etl_Stat]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-29
-- 描述: 统计作业
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_Stat;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@StatName nvarchar(256),
	@Detect nvarchar(4000),	-- 检测数据到位情况,通过时返回1(参数:@StatName,@StartTime,@EndTime)
	@STMT nvarchar(3500),	-- 统计存储过程或统计语句(参数:@StartTime,@EndTime)[可接受重复统计请求,时间段参数值内部可进行调整]
	@FirstStartTime datetime, -- 统计参数:开始时间初始值
	@Cycle int,			-- 统计周期(分钟)
	@RTime smalldatetime,-- 首次统计执行时间
	@PreRTime datetime	-- 上一次统计时间
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定统计时间
	,@PreStartTime datetime	-- 上一次成功的统计开始时间
	,@PreEndTime datetime	-- 上一次成功的统计结束时间
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID, StatName, Detect, STMT, FirstStartTime, Cycle, RTime, PreRTime
  FROM etl.StatCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@StatName,@Detect,@STMT,@FirstStartTime,@Cycle,@RTime,@PreRTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 单次循环内的变量初始清空
	SELECT @PreStartTime = NULL, @PreEndTime = NULL;
	
	SELECT @PreRTime = ISNULL(@PreRTime,dateadd(n,4-@Cycle,@RTime))
	SELECT @TodaySTime = Convert(nvarchar(11),@Today,120) + Left(Convert(nvarchar(50),@RTime,108),6)+'00';
	
	-- 取上次统计结束时间为下次统计开始时间,直到结束时间超过当前作业执行时间,无执行记录时取初始时间
	SELECT TOP 1 @PreStartTime = StartTime, @PreEndTime = EndTime
	  FROM etl.Log_Stat
	 WHERE StatName = @StatName
	 ORDER BY StartTime DESC;
	SELECT @PreStartTime = ISNULL(@PreStartTime,Dateadd(n,-@Cycle,@FirstStartTime));
	SELECT @PreEndTime = ISNULL(@PreEndTime,Dateadd(n,@Cycle,@PreStartTime));
	
	WHILE(DATEDIFF(n, @PreRTime, @JobTime) >= @Cycle	-- 执行周期已到
		AND @JobTime >= @RTime	-- 首次执行时间已到
		AND Dateadd(n,@Cycle,@PreEndTime) <= @JobTime	-- 即将统计的结束时间未超过当前作业时间,即要求数据在一个执行周期内到位
	)BEGIN
		-- 即将统计的时间段
		SELECT @PreStartTime = @PreEndTime;
		SELECT @PreEndTime = Dateadd(n,@Cycle,@PreStartTime);
		
		-- 检测数据到位情况(通过才统计)
		IF(LEN(@Detect)>1)
		BEGIN
			EXEC @rtn = sp_executesql @Detect, N'@StatName nvarchar(256),@StartTime datetime,@EndTime datetime'
				, @StatName = @StatName, @StartTime = @PreStartTime,@EndTime = @PreEndTime;
			IF(@@ERROR <> 0 OR @rtn <> 1)
			BEGIN
				GOTO NEXT_Stat;
			END
		END
		
		EXEC @rtn = sp_executesql @STMT, N'@StartTime datetime,@EndTime datetime', @StartTime = @PreStartTime,@EndTime = @PreEndTime;
		IF(@@ERROR <> 0 OR @rtn <> 1)
		BEGIN
			GOTO NEXT_Stat;
		END
		
		-- 记录本次统计日志
		INSERT etl.Log_Stat ( StatName,STMT,StartTime,EndTime )
		VALUES  (@StatName,@STMT,@PreStartTime,@PreEndTime)
		
		-- 更新统计配置表记录的最后执行时间
		UPDATE etl.StatCfg SET _Time = getdate(), PreRTime = @JobTime WHERE ID = @ID;
	END
	
	NEXT_Stat:
	FETCH NEXT FROM @csr INTO @ID,@StatName,@Detect,@STMT,@FirstStartTime,@Cycle,@RTime,@PreRTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Job_Apq_Arp]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_Apq_Arp]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-07-01
-- 描述: Arp绑定
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_Arp;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,@GateWay nvarchar(50), @Mac nvarchar(50),
	
	@rtn int, @cmd nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	;
SELECT @JobTime=GetDate();

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,GateWay,Mac
  FROM dbo.ArpCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@GateWay,@Mac;
WHILE(@@FETCH_STATUS=0)
BEGIN
	SELECT @cmd = 'arp -s ' + @GateWay + ' ' + @Mac;
	EXEC master..xp_cmdshell @cmd;

	FETCH NEXT FROM @csr INTO @ID,@GateWay,@Mac;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Job_FTP_Send]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_FTP_Send]
	@TransRowCount	int = 100	-- 传送行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-19
-- 描述: 通过FTP上传文件
-- 原因: FTP下载时是先写入临时文件,可能引起空间不足
-- 示例:
EXEC dbo.Job_FTP_Send 100;
-- =============================================
*/
SET NOCOUNT ON;

IF(@TransRowCount IS NULL) SELECT @TransRowCount = 100;

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	,@ScpFolder nvarchar(4000)
	,@ScpFileName nvarchar(128)
	,@ScpFullName nvarchar(4000)
	,@LFullName nvarchar(4000)	 -- 临时变量:本地文件全名
	,@FullFileToDel nvarchar(4000)--在此文件之前的文件将被删除
	;
SELECT @SPBeginTime=GetDate(),@ScpFolder = 'D:\Apq_DBA\Scp\'
SELECT @ScpFileName = 'FTP_Send['+LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].scp';
SELECT @ScpFullName = @ScpFolder + @ScpFileName;
IF(RIGHT(@ScpFolder,1)<>'\') SELECT @ScpFolder = @ScpFolder+'\';
SELECT @cmd = 'md ' + LEFT(@ScpFolder,LEN(@ScpFolder)-1);
EXEC master..xp_cmdshell @cmd;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@TransRowCount) ID,Folder,FileName,FTPSrv,U,P,FTPFolder,FTPFolderTmp,LSize,RSize
  FROM dbo.FTP_SendQueue
 WHERE Enabled = 1 AND IsSuccess = 0
 ORDER BY FileName

-- 游标内临时变量
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));

DECLARE @ID bigint,@Folder nvarchar(512),@FileName nvarchar(256),@LSize bigint,@RSize bigint,
	@FTPSrv nvarchar(256),@U nvarchar(256),@P nvarchar(256),@FTPFolder nvarchar(512),@FTPFolderTmp nvarchar(512);
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@Folder,@FileName,@FTPSrv,@U,@P,@FTPFolder,@FTPFolderTmp,@LSize,@RSize;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	IF(RIGHT(@Folder,1)<>'\') SELECT @Folder = @Folder+'\';
	IF(RIGHT(@FTPFolder,1)<>'/') SELECT @FTPFolder = @FTPFolder+'/';
	IF(RIGHT(@FTPFolderTmp,1)<>'/') SELECT @FTPFolderTmp = @FTPFolderTmp+'/';
	SELECT @cmd = 'md ' + LEFT(@Folder,LEN(@Folder)-1);
	EXEC master..xp_cmdshell @cmd;
	
	-- 获取本地文件大小 ----------------------------------------------------------------------------
	SELECT @cmd = 'dir "' + @Folder + @FileName + '"';
	TRUNCATE TABLE #t1;
	INSERT #t1 EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		GOTO NEXT_Row;
	END
	
	DECLARE @sLDir nvarchar(4000);
	SELECT TOP 1 @sLDir = s FROM #t1 WHERE RIGHT(s,Len(@FileName)) = @FileName;
	--SELECT @sLDir;
	IF(@sLDir IS NOT NULL) SELECT @LSize = Replace(LTRIM(RTRIM(Substring(@sLDir,18,Len(@sLDir)-18-Len(@FileName)))),',','');
	IF(@LSize>0)
	BEGIN
		UPDATE dbo.FTP_SendQueue SET [_Time] = getdate(),LSize = @LSize WHERE ID = @ID;
	END

	-- 传送文件 ------------------------------------------------------------------------------------
	SELECT @cmd = 'echo open ' + @FTPSrv + '>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo user ' + @U + '>>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell  @cmd;
	SELECT @cmd = 'echo ' + @P + '>>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo cd "' + @FTPFolderTmp + '">>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo lcd "' + LEFT(@Folder,LEN(@Folder)-1) + '">>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;

	SELECT @cmd = 'echo binary>>"' + @ScpFullName + '"';-- 将文件传输类型设置为二进制
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo put "' + @FileName + '">>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo rename "' + @FileName + '" "' + @FTPFolder + @FileName + '">>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo dir "' + @FTPFolder + @FileName + '">>"' + @ScpFullName + '"';	-- 获取已上传文件大小
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'echo bye>>"' + @ScpFullName + '"';
	EXEC master..xp_cmdshell @cmd;
	SELECT @cmd = 'ftp -i -n -s:"' + @ScpFullName + '"';
	--SELECT @cmd;
	TRUNCATE TABLE #t;
	INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		GOTO NEXT_Row;
	END
	SELECT * FROM #t;
	IF(EXISTS(SELECT TOP 1 1 FROM #t WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%' OR s LIKE 'Permission denied%'
			OR s LIKE '%Unable to rename%'))
	BEGIN
		GOTO NEXT_Row;
	END
	-- 获取远程文件大小 ------------------------------------------------------------------------
	DECLARE @sRDir nvarchar(4000);
	SELECT TOP 1 @sRDir = s FROM #t WHERE Left(RIGHT(s,Len(@FileName)+1),Len(@FileName)) = @FileName;
	--SELECT @sRDir;
	/*
-rw-rw-rw-   1 user     group      118272 Oct 25 14:52 BaseBusinessDb[20101022_2150].trn 
-rw-rw-rw-   1 user     group    42460672 Oct 25 14:18 PayCenter[20101022_2150].trn 
-rw-rw-rw-   1 user     group    101897728 Oct 22 20:04 StreamMedia[20101022_1951].bak 
规律:从34列开始为文件大小,最少占8字符,不足8位数字前补0,因此,从42列开始查找下一个空格,以此作为结束点.
	*/
	DECLARE @sidx int;
	SELECT @sidx = charindex(' ',@sRDir,42);
	SELECT @RSize = Replace(LTRIM(RTRIM(Substring(@sRDir,34,@sidx-34))),',','');
	--SELECT @RSize;
	IF(@RSize>0)
	BEGIN
		UPDATE dbo.FTP_SendQueue SET [_Time] = getdate(),RSize = @RSize WHERE ID = @ID;
	END
	
	-- 上传成功
	IF(@LSize = @RSize) UPDATE dbo.FTP_SendQueue SET _Time = getdate(), IsSuccess = 1 WHERE ID = @ID;
	-- =============================================================================================
	
	NEXT_Row:
	-- 删除FTP命令文件
	SELECT @cmd = 'del ""' + @ScpFullName + '"" /q';
	EXEC master..xp_cmdshell @cmd;

	FETCH NEXT FROM @csr INTO @ID,@Folder,@FileName,@FTPSrv,@U,@P,@FTPFolder,@FTPFolderTmp,@LSize,@RSize;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Pick_JobHis]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Pick_JobHis]
    @PickLastID bigint
   ,@PickMaxID bigint
   ,@PickLastTime datetime
   ,@PickMaxTime datetime
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 作业日志收集,供BCP使用
-- 示例:
DECLARE @rtn int,@PickLastID bigint,@PickMaxID bigint,@PickLastTime datetime,@PickMaxTime datetime;
SELECT @PickLastID = -1
	,@PickMaxID = 500
	,@PickLastTime = DateAdd(dd,-1,getdate())
	,@PickMaxTime = getdate()
EXEC @rtn = dbo.Pick_JobHis @PickLastID, @PickMaxID, @PickLastTime,@PickMaxTime
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON ;

--定义变量
DECLARE @LocalSrvID nvarchar(32)
   ,@SPBeginTime datetime		-- 存储过程启动时间
	;
SELECT  @SPBeginTime = GetDate() ;
SELECT  @LocalSrvID = dbo.Apq_Ext_Get('',0,'LocalSrvID') ;

SELECT  @LocalSrvID,j.name,h.step_id,h.step_name,h.sql_message_id,h.sql_severity,h.message,h.run_status
	,CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )),h.run_duration
FROM    msdb.dbo.sysjobhistory h
        INNER JOIN msdb.dbo.sysjobs j
            ON h.job_id = j.job_id
WHERE   step_id > 0
		AND name NOT IN ('Apq_Bak','Apq_Bak_Init','Apq_Bak_Raiserror','Apq_FTP_PutBak[1]','Apq_RestoreFromFolder[1]','Apq_杀掉死进程')
        AND run_status = 0
        AND ( /*( h.instance_id > @PickLastID
                AND h.instance_id <= @PickMaxID
              )
              OR */( CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) > @PickLastTime
                   AND CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) <= @PickMaxTime
                 )
            )
UNION ALL
SELECT  @LocalSrvID,j.name,h.step_id,h.step_name,h.sql_message_id,h.sql_severity,h.message,h.run_status
	,CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )),h.run_duration
FROM    msdb.dbo.sysjobhistory h
        INNER JOIN msdb.dbo.sysjobs j
            ON h.job_id = j.job_id
WHERE   step_id > 0
		AND name IN ('Apq_Bak','Apq_Bak_Init','Apq_Bak_Raiserror','Apq_FTP_PutBak[1]','Apq_RestoreFromFolder[1]','Apq_杀掉死进程')
        AND run_status = 0
        AND ( CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) > Dateadd(n,-30,@SPBeginTime)
               AND CONVERT(datetime,( CONVERT(nvarchar(20),run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time) )) <= @PickMaxTime
             )
GO
/****** Object:  StoredProcedure [etl].[Job_Etl_SwitchBcpTable]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Job_Etl_SwitchBcpTable]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 切换BCP接收表
-- 功能: 按预定时间切换BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_SwitchBcpTable;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@EtlName nvarchar(50),
	@DBName nvarchar(256),	-- 数据库名
	@SchemaName nvarchar(256),
	@TName nvarchar(256),
	@STName nvarchar(256),
	@Cycle int,			-- 切换周期(分钟)
	@STime smalldatetime,-- 切换时间
	@PreSTime datetime	-- 上一次切换时间
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID, EtlName, DBName, SchemaName, TName, STName, Cycle, STime, PreSTime
  FROM etl.BcpSTableCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@DBName,@SchemaName,@TName,@STName,@Cycle,@STime,@PreSTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	SELECT @PreSTime = ISNULL(@PreSTime,dateadd(n,4-@Cycle,@STime))
	SELECT @TodaySTime = Convert(nvarchar(11),@Today,120) + Left(Convert(nvarchar(50),@STime,108),6)+'00';

	IF(DATEDIFF(n, @PreSTime, @JobTime) >= @Cycle - 1	-- 执行周期已到
		--AND datediff(n,@JobTime,@TodaySTime) BETWEEN 0 AND 5	-- 今天执行时间已到
	)BEGIN
		-- 切换表
		EXEC @rtn = etl.Etl_SwitchBcpTable @EtlName = @EtlName;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			UPDATE etl.BcpSTableCfg SET _Time = getdate(), PreSTime = @JobTime WHERE ID = @ID;
		END
	END

	FETCH NEXT FROM @csr INTO @ID,@EtlName,@DBName,@SchemaName,@TName,@STName,@Cycle,@STime,@PreSTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Job_Pick_JobHis]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_Pick_JobHis]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-11
-- 描述: 作业日志本地收集
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Pick_JobHis
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @LocalSrvID nvarchar(32),
	@CfgName nvarchar(256),		-- 配置名(唯一)
	@PickLastID bigint,
	@PickLastTime datetime,
	@HasContent tinyint,
	@FileFolder nvarchar(3000),	-- 文件夹
	@FileName nvarchar(256),	-- 文件名
	@FileEX nvarchar(50),		-- 扩展名(.txt)
	@SrvID int,					-- 服务器编号
	@DBName nvarchar(256),		-- 数据库名
	@t nvarchar(50),
	@r nvarchar(50),
	
	@LSName nvarchar(512),		-- 目标服务器别名
	@PickMaxID bigint,
	@PickMaxTime datetime,
	
	@rtn int, @cmd nvarchar(4000), @sql nvarchar(max)
	,@SPBeginTime datetime		-- 存储过程启动时间
	;
SELECT @SPBeginTime=GetDate();
SELECT @CfgName = 'JobHis'	-- 指定配置名
	,@FileFolder = 'D:\'
	,@FileEX = '.txt'
	,@t = '~*'	-- 列分隔符 推荐使用"~*"
	,@r = '~*$'	-- 行分隔符 推荐使用"~*$"
SELECT @LocalSrvID = dbo.Apq_Ext_Get('',0,'LocalSrvID');
SELECT @FileName = @CfgName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+']';
SELECT @HasContent = 0;

IF(Len(@FileFolder)>3)
BEGIN
	IF(RIGHT(@FileFolder,1)<>'\') SELECT @FileFolder = @FileFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FileFolder, 1, LEN(@FileFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 取出上次收集的最后ID和最后时间
SELECT @PickLastID = PickLastID, @PickLastTime = PickLastTime
  FROM dbo.DTS_Send
 WHERE CfgName = @CfgName;

-- 计算本次收集的最后ID或最后时间
SELECT @PickMaxID = ISNULL(max(instance_id),-1), @PickMaxTime = ISNULL(max(Convert(datetime,(convert(nvarchar(20), run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time)))),getdate())
  FROM msdb.dbo.sysjobhistory

-- 有数据时导出
IF(EXISTS(SELECT TOP 1 1 FROM msdb.dbo.sysjobhistory WHERE run_status = 0
	AND ((instance_id > @PickLastID AND instance_id <= @PickMaxID)
	OR (Convert(datetime,(convert(nvarchar(20), run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time))) > @PickLastTime
	AND Convert(datetime,(convert(nvarchar(20), run_date) + ' ' + dbo.Apq_ConvertInt_TimeString(run_time))) <= @PickMaxTime))
	)
)
BEGIN
	SELECT @HasContent = 1;
	SELECT @sql = 'EXEC Apq_DBA.dbo.Pick_JobHis ' + dbo.Apq_CreateSqlON(@PickLastID);
	SELECT @sql = @sql + ',' + dbo.Apq_CreateSqlON(@PickMaxID);
	SELECT @sql = @sql + ',' + dbo.Apq_CreateSqlON(@PickLastTime);
	SELECT @sql = @sql + ',' + dbo.Apq_CreateSqlON(@PickMaxTime);
	-- BCP queryout
	SELECT @cmd = 'bcp "' + @sql + '" queryout "' + @FileFolder + @FileName + @FileEX
			+ '" -c -t' + CASE WHEN LEN(@t) > 0 THEN @t ELSE '\t' END
			+ ' -r' + CASE WHEN LEN(@r) > 0 THEN @r ELSE '\n' END
			+ ' -T'
	--Print @sql; Print @cmd; RETURN;
	EXEC @rtn = master..xp_cmdshell @cmd;
END

-- 更新上次收集的最后ID和最后时间
UPDATE dbo.DTS_Send
   SET _Time = GETDATE(), PickLastID = @PickMaxID, PickLastTime = @PickMaxTime
 WHERE CfgName = @CfgName;

-- 记录收集日志
INSERT dbo.Log_DTS_LocalPick ( CfgName,PickTime,HasContent,FileFolder,FileName,FileEX,t,r )
VALUES  (@CfgName,@SPBeginTime,@HasContent,@FileFolder,@FileName,@FileEX,@t,@r)

RETURN 1;
GO
/****** Object:  StoredProcedure [etl].[Job_Etl_Load]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [etl].[Job_Etl_Load]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-28
-- 描述: 加载到正式表
-- 功能: 按预定时间加载BCP接收表
-- 示例:
DECLARE @rtn int;
EXEC @rtn = etl.Job_Etl_Load;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@EtlName nvarchar(256),
	@SrcFullTableName nvarchar(256),
	@DstFullTableName nvarchar(256),
	@Cycle int,			-- 切换周期(分钟)
	@LTime smalldatetime,-- 切换时间
	@PreLTime datetime	-- 上一次切换时间
	
	,@rtn int, @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@Today smalldatetime
	,@TodaySTime smalldatetime -- 今天预定切换时间
	;
SELECT @JobTime=GetDate();
SELECT @Today = dateadd(dd,0,datediff(dd,0,@JobTime));

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID, EtlName, SrcFullTableName, DstFullTableName, Cycle, LTime, PreLTime
  FROM etl.LoadCfg
 WHERE Enabled = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@EtlName,@SrcFullTableName,@DstFullTableName,@Cycle,@LTime,@PreLTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	SELECT @PreLTime = ISNULL(@PreLTime,dateadd(n,4-@Cycle,@LTime))
	SELECT @TodaySTime = Convert(nvarchar(11),@Today,120) + Left(Convert(nvarchar(50),@LTime,108),6)+'00';

	IF(DATEDIFF(n, @PreLTime, @JobTime) >= @Cycle - 1	-- 执行周期已到
		--AND datediff(n,@JobTime,@TodaySTime) BETWEEN 0 AND 5	-- 今天执行时间已到
	)BEGIN
		-- 切换表
		EXEC @rtn = etl.Etl_Load @EtlName;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			UPDATE etl.LoadCfg SET _Time = getdate(), PreLTime = @JobTime WHERE ID = @ID;
		END
	END

	NEXT_Load:
	FETCH NEXT FROM @csr INTO @ID,@EtlName,@SrcFullTableName,@DstFullTableName,@Cycle,@LTime,@PreLTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Job_Apq_DTS_Send]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_Apq_DTS_Send]
	 @RunnerID	int	-- 传送者ID
	,@RunRowCount	int	-- 传送行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-08
-- 描述: 基于收集历史的数据传送作业
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_DTS_Send 1, 100;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

IF(@RunnerID IS NULL) SELECT @RunnerID = 1;
IF(@RunRowCount IS NULL) SELECT @RunRowCount = 100;

--定义变量
DECLARE @LocalSrvID nvarchar(32),
	@ID bigint,	-- 配置ID
	@CfgName nvarchar(256),		-- 配置名(唯一)
	@PickTime datetime,			-- 采集时间
	@HasContent tinyint,		-- 是否收集到数据
	@FileFolder nvarchar(3000),	-- 文件夹
	@FileName nvarchar(256),	-- 文件名
	@FileEX nvarchar(50),		-- 扩展名(.txt)
	@RunnerIDCfg int,			-- 指定执行者(0:未指定)
	@TransMethod tinyint,		-- 传送方式{1:BCP in,5:FTP}
	@SrvID int,					-- 服务器编号
	@DBName nvarchar(256),		-- 数据库名
	@SchemaName nvarchar(256),	-- 架构名
	@SPTName nvarchar(256),		-- 存储过程名
	@U nvarchar(256),
	@P nvarchar(256),
	@FTPIP tinyint,				-- FTP传送时选择IP{0:Lan,1:Wan1,2:Wan2}
	@FTPFolderTmp nvarchar(512),-- FTP 临时目录
	@FTPFolder nvarchar(512),	-- FTP 目录
	@LogID bigint,
	@t nvarchar(50),
	@r nvarchar(50),
	
	@LSName nvarchar(512),		-- 目标服务器别名
	@strFTPIP nvarchar(500),	-- 目标服务器FTPIP
	@FTPPort nvarchar(32),		-- 目标服务器FTP端口
	@ScpFullName nvarchar(3000),
	
	@rtn int, @cmd nvarchar(4000), @sql nvarchar(max)
	,@SPBeginTime datetime		-- 存储过程启动时间
	,@TodayTransTime datetime	-- 当天理论传送时间
	;
SELECT @SPBeginTime=GetDate();
SELECT @LocalSrvID = dbo.Apq_Ext_Get('',0,'LocalSrvID');

--定义游标
DECLARE @csr_DTS CURSOR;
SET @csr_DTS=CURSOR FOR
SELECT TOP(@RunRowCount) ds.ID, ds.CfgName, ds.TransMethod, ds.SrvID, rs.LSName, ds.DBName, ds.SchemaName, ds.SPTName, ds.U, ds.P, ds.FTPFolderTmp, ds.FTPFolder
	, LogID=l.ID,l.PickTime,l.HasContent,l.FileFolder + CASE WHEN RIGHT(l.FileFolder,1)<>'\' THEN '\' ELSE '' END,l.FileName, l.FileEX,t,r
  FROM dbo.DTS_Send ds INNER JOIN dbo.Log_DTS_LocalPick l ON l.CfgName = ds.CfgName INNER JOIN dbo.RSrvConfig rs ON ds.SrvID = rs.ID
 WHERE ds.Enabled = 1
	AND ((ds.RunnerIDCfg = 0 AND ds.RunnerIDRun = 0) OR ds.RunnerIDCfg = @RunnerID)
	AND l.TransTime IS NULL;

CREATE TABLE #t(s nvarchar(4000));

OPEN @csr_DTS;
FETCH NEXT FROM @csr_DTS INTO @ID,@CfgName,@TransMethod,@SrvID,@LSName,@DBName,@SchemaName,@SPTName,@U,@P,@FTPFolderTmp,@FTPFolder
	,@LogID,@PickTime,@HasContent,@FileFolder,@FileName,@FileEX,@t,@r;
WHILE(@@FETCH_STATUS=0)
BEGIN
	UPDATE dbo.DTS_Send SET [_Time] = getdate(),RunnerIDRun = @RunnerID WHERE ID = @ID;
	IF(@HasContent = 0) GOTO Success;	-- 无数据时直接认为成功

	-- 开始传送文件 ------------------------------------------------------------------------
	-- 1.BCP in
	IF(@TransMethod = 1)
	BEGIN
		SELECT @cmd = 'bcp "' + @DBName + '.' + @SchemaName + '.' + @SPTName + '" in "' + @FileFolder + @FileName + @FileEX
			+ '" -c -t' + CASE WHEN LEN(@t) > 0 THEN @t ELSE '\t' END	-- 列分隔符 推荐使用"~*"
			+ ' -r' + CASE WHEN LEN(@r) > 0 THEN @r ELSE '\n' END			-- 行分隔符 推荐使用"~*$"
			+ ' -U' + @U + ' -P' + @P + ' -S' + @LSName;
		--PRINT @cmd; GOTO NEXT_Trans;
		EXEC @rtn = master..xp_cmdshell @cmd;
		--PRINT @@ERROR; PRINT @rtn; GOTO NEXT_Trans;
		IF(@@ERROR <> 0 OR @rtn <> 0)
		BEGIN
			-- 失败
			GOTO NEXT_Trans;
		END
	END
	
	-- 5.FTP
	IF(@TransMethod = 5)
	BEGIN
		SELECT @ScpFullName = @FileFolder + @CfgName + '.scp';
		SELECT @strFTPIP = CASE @FTPIP WHEN 1 THEN IPWan1 WHEN 2 THEN IPWan2 ELSE IPLan END,@FTPPort = FTPPort
		  FROM dbo.RSrvConfig
		 WHERE ID = @SrvID;
	
		SELECT @cmd = 'echo open ' + @strFTPIP + ' ' + @FTPPort + '>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo user ' + @U + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell  @cmd;
		SELECT @cmd = 'echo ' + @P + '>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo cd "' + @FTPFolderTmp + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo lcd "' + LEFT(@FileFolder,LEN(@FileFolder)-1) + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
	
		SELECT @cmd = 'echo binary>>"' + @ScpFullName + '"';-- 将文件传输类型设置为二进制
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo put "' + @FileName + @FileEX + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo rename "' + @FileName + @FileEX + '" "' + @FTPFolder + @FileName + @FileEX + '">>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'echo bye>>"' + @ScpFullName + '"';
		EXEC master..xp_cmdshell @cmd;
		SELECT @cmd = 'ftp -i -n -s:"' + @ScpFullName + '"';
		--SELECT @cmd;
		TRUNCATE TABLE #t;
		INSERT #t EXEC @rtn = master..xp_cmdshell @cmd;
		IF(@@ERROR <> 0 OR @rtn <> 0)
		BEGIN
			-- 失败
			GOTO NEXT_Trans;
		END
		SELECT * FROM #t;
		IF(EXISTS(SELECT TOP 1 1 FROM #t WHERE s LIKE 'Not connected%' OR s LIKE 'Connection closed%'))
		BEGIN
			-- 失败
			GOTO NEXT_Trans;
		END
	END
	-- =====================================================================================
	
	Success:
	SELECT @sql = '
INSERT [' + @LSName + '].[' + @DBName + '].dbo.Log_DTS_Receive(CfgName, PickTime, RSrvID, SendTime)
VALUES(@CfgName,@PickTime,@RSrvID,@SendTime);
';
	EXEC @rtn = sp_executesql @sql,N'@CfgName nvarchar(256), @PickTime datetime, @RSrvID int, @SendTime datetime'
		,@CfgName = @CfgName,@PickTime = @PickTime, @RSrvID = @LocalSrvID, @SendTime = @SPBeginTime;
	IF(@@ERROR <> 0 OR @rtn <> 0)
	BEGIN
		-- 失败
		GOTO NEXT_Trans;
	END
	
	UPDATE dbo.Log_DTS_LocalPick
	   SET [_Time] = getdate(),TransTime = getdate()
	 WHERE ID = @LogID;
	
	SELECT @cmd = 'del "' + @FileFolder + @FileName + @FileEX + '" /f /q';
	EXEC master..xp_cmdshell @cmd;
	
	NEXT_Trans:
	UPDATE dbo.DTS_Send SET [_Time] = getdate(),RunnerIDRun = 0 WHERE ID = @ID;
	FETCH NEXT FROM @csr_DTS INTO @ID,@CfgName,@TransMethod,@SrvID,@LSName,@DBName,@SchemaName,@SPTName,@U,@P,@FTPFolderTmp,@FTPFolder
		,@LogID,@PickTime,@HasContent,@FileFolder,@FileName,@FileEX,@t,@r;
END
CLOSE @csr_DTS;

TRUNCATE TABLE #t;
DROP TABLE #t;

RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Job_Apq_Stat]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_Apq_Stat]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-11-05
-- 描述: 数据统计作业
-- 参数:
@StatName: 统计名
@StatTime: 统计时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_Stat;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @StatName nvarchar(50), -- 统计名
	@StatTime datetime,-- 统计时间
	@LastStatDate datetime -- 上次统计时间
	
	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@SPBeginTime datetime		-- 存储过程启动时间
	;
SELECT @SPBeginTime=GetDate();

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR FOR
SELECT StatName,StatTime
  FROM dbo.StatConfig_Day
 WHERE Enabled = 1 AND DateAdd(dd,2,LastStatDate) < @SPBeginTime -- 至少今天尚未统计
	AND Convert(datetime,CONVERT(nvarchar(11),@SPBeginTime,120) + Right(Convert(nvarchar,StatTime,120),8)) < @SPBeginTime; -- 时间已到

OPEN @csr;
FETCH NEXT FROM @csr INTO @StatName,@StatTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 统计数据
	EXEC @rtn = dbo.Apq_Stat @StatName;

	NEXT_Stat:
	FETCH NEXT FROM @csr INTO @StatName,@StatTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_RestoreFromFolder]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_RestoreFromFolder]
	 @RunnerID		int	-- 执行者ID
	,@RunRowCount	int	-- 执行行数(最多读取的行数)
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-05-12
-- 描述: 从指定文件夹寻找最新的备份文件按设置还原数据库
-- 示例:
EXEC bak.Job_Apq_RestoreFromFolder 1,100;
-- =============================================
*/
SET NOCOUNT ON;

IF(@RunnerID IS NULL) SELECT @RunnerID = 1;
IF(@RunRowCount IS NULL) SELECT @RunRowCount = 100;

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@LFullName nvarchar(4000)	 -- 临时变量:本地文件全名
	,@Num_HisDB int	-- 临时变量:已有历史库个数
	,@FullFileToDel nvarchar(4000)--在此文件之前的文件将被删除
	,@ProductVersion	decimal
	;

DECLARE @csr CURSOR
SET @csr = CURSOR STATIC FOR
SELECT TOP(@RunRowCount) ID, DBName, LastFileName, BakFolder, Num_Full, RestoreType, RestoreFolder, DB_HisNum
  FROM bak.RestoreFromFolder
 WHERE Enabled = 1
	AND ((RunnerIDCfg = 0 AND RunnerIDRun = 0) OR RunnerIDCfg = @RunnerID)

DECLARE @csrFile CURSOR;
-- 游标内临时变量
DECLARE @FTPFileName nvarchar(512)
	,@DBName_R nvarchar(256)
	;
CREATE TABLE #t(s nvarchar(4000));
CREATE TABLE #t1(s nvarchar(4000));
CREATE TABLE #t2(s nvarchar(4000));

DECLARE @ID bigint,@DBName nvarchar(256),@LastFileName nvarchar(256),@BakFolder nvarchar(4000),@Num_Full int
	,@RestoreType int,@RestoreFolder nvarchar(4000),@DB_HisNum int;
OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@BakFolder,@Num_Full,@RestoreType,@RestoreFolder,@DB_HisNum;
WHILE(@@FETCH_STATUS = 0)
BEGIN
	UPDATE bak.RestoreFromFolder SET [_Time] = getdate(),RunnerIDRun = @RunnerID WHERE ID = @ID;
	
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	
	-- 还原数据库 ----------------------------------------------------------------------------------
	TRUNCATE TABLE #t1;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @BakFolder + @DBName + '[*].*"';
	INSERT #t1(s) EXEC master..xp_cmdshell @cmd;
	
	TRUNCATE TABLE #t2;
	INSERT #t2(s)
	SELECT s FROM #t1
	 WHERE s > @LastFileName AND Len(s) > Len(@DBName) + 6
		AND Left(s,Len(@DBName)+1) = @DBName+'['
		AND SubString(s,Len(@DBName)+ 15,5) IN ('].bak','].trn')
	 ORDER BY s;
	
	SET @csrFile = CURSOR FOR
	SELECT s FROM #t2;
	
	SELECT @FTPFileName = NULL;
	OPEN @csrFile;
	FETCH NEXT FROM @csrFile INTO @FTPFileName
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		SELECT @DBName_R = @DBName + '_' + LEFT(REPLACE(CONVERT(nvarchar,Dateadd(dd,-1,Substring(@FTPFileName,Len(@DBName)+2,8)),120),'-',''),8)
		SELECT @LFullName = @BakFolder + @FTPFileName;
		-- 恢复完整备份到历史数据库
		IF(SubString(@FTPFileName,Len(@DBName)+ 15,5) = '].bak' AND @RestoreType & 1 <> 0 AND Len(@RestoreFolder) > 1)
		BEGIN
			-- 删除历史库
			SELECT @Num_HisDB = Count(name)
			  FROM sys.databases
			 WHERE Len(name) > Len(@DBName) AND Left(name,Len(@DBName)+1) = @DBName + '_';
			IF(@Num_HisDB >= @DB_HisNum)
			BEGIN
				SELECT @sql = '';
				SELECT TOP(@Num_HisDB-@DB_HisNum+1) @sql = @sql + 'EXEC dbo.Apq_KILL_DB ''' + name + ''';DROP DATABASE [' + name + '];'
				  FROM sys.databases
				 WHERE Len(name) > Len(@DBName) AND Left(name,Len(@DBName)+1) = @DBName + '_'
				 ORDER BY name;
				EXEC sp_executesql @sql;
			END
			
			EXEC @rtn = bak.Apq_Restore 1,@DBName_R,@LFullName,1,@RestoreFolder,2
			IF(@@ERROR <> 0 OR @rtn <> 1)
			BEGIN
				CLOSE @csrFile;
				GOTO NEXT_DB;
			END
			
			SELECT @ProductVersion = CONVERT(decimal,LEFT(Convert(nvarchar,SERVERPROPERTY('ProductVersion')),2));
			IF(@ProductVersion<10)
			BEGIN
				SELECT @sql='BACKUP LOG '+@DBName_R+' WITH NO_LOG';
				EXEC sp_executesql @sql;
				SELECT @sqlDB = 'DBCC SHRINKFILE(''' + @DBName + '_Log'',10)'
				SELECT @sql = 'EXEC [' + @DBName_R + ']..sp_executesql @sqlDB';
				EXEC sp_executesql @sql,N'@sqlDB nvarchar(4000)',@sqlDB=@sqlDB;
			END
		END
		
		-- 恢复日志备份到备用数据库
		ELSE IF(@RestoreType & 2 <> 0)
		BEGIN
			IF(SubString(@FTPFileName,Len(@DBName)+ 15,5) = '].bak')
			BEGIN
				--EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,1,@RestoreFolder,2;
				EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,1,@RestoreFolder,1;
				IF(@@ERROR <> 0 OR @rtn <> 1)
				BEGIN
					CLOSE @csrFile;
					GOTO NEXT_DB;
				END
			END
			IF(SubString(@FTPFileName,Len(@DBName)+ 15,5) = '].trn')
			BEGIN
				--EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,2,@RestoreFolder,2
				EXEC @rtn = bak.Apq_Restore 1,@DBName,@LFullName,2,@RestoreFolder,1
				IF(@@ERROR <> 0 OR @rtn <> 1)
				BEGIN
					CLOSE @csrFile;
					GOTO NEXT_DB;
				END;
			END
		END
	
		-- 更新最后文件名
		IF(@FTPFileName IS NOT NULL) UPDATE bak.RestoreFromFolder SET [_Time] = getdate(),LastFileName = @FTPFileName WHERE ID = @ID;

		NEXT_File:
		FETCH NEXT FROM @csrFile INTO @FTPFileName
	END
	CLOSE @csrFile;
	-- =============================================================================================
	
	-- 删除本地历史文件 ----------------------------------------------------------------------------
	-- 删除保留个数以外的备份文件
	TRUNCATE TABLE #t;
	SELECT @cmd = 'dir /a:-d/b/o:n "' + @BakFolder + @DBName + '[*].*"';
	INSERT #t(s) EXEC master..xp_cmdshell @cmd;
	
	SELECT TOP(@Num_Full) @FullFileToDel = s
	  FROM #t
	 WHERE Len(s) > Len(@DBName) + 6
		AND Left(s,Len(@DBName)+1) = @DBName+'['
		AND SubString(s,Len(@DBName)+ 15,5) = '].bak'
	 ORDER BY s DESC;
	IF(@@ROWCOUNT >= @Num_Full)
	BEGIN
		SELECT @cmd = '';
		SELECT @cmd = @cmd + '&&del /F /Q "' + @BakFolder + s + '"'
		  FROM #t
		 WHERE s < @FullFileToDel;

		IF(Len(@cmd) > 2)
		BEGIN
			SELECT @cmd = Right(@cmd,Len(@cmd)-2);
			SELECT @cmd;
			EXEC master..xp_cmdshell @cmd;
		END
	END
	-- =============================================================================================
	
	NEXT_DB:
	-- 恢复空闲状态
	UPDATE bak.RestoreFromFolder SET [_Time] = getdate(),RunnerIDRun = 0 WHERE ID = @ID;

	FETCH NEXT FROM @csr INTO @ID,@DBName,@LastFileName,@BakFolder,@Num_Full,@RestoreType,@RestoreFolder,@DB_HisNum;
END
CLOSE @csr;

Quit:
DROP TABLE #t;
DROP TABLE #t1;
DROP TABLE #t2;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Job_Apq_DataTrans]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Job_Apq_DataTrans]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-08-11
-- 描述: 数据传送作业
-- 参数:
@TransName: 传送名
@TransCycle: 传送周期(天)
@TransTime: 传送时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_DataTrans;
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

--定义变量
DECLARE @TransName nvarchar(50), -- 传送名
	@TransCycle int,-- 传送周期(天)
	@TransTime datetime,-- 传送时间
	@LastTransTime datetime -- 上次传送时间
	
	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@SPBeginTime datetime		-- 存储过程启动时间
	,@TodayTransTime datetime	-- 当天理论传送时间
	;
SELECT @SPBeginTime=GetDate();

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR FOR
SELECT TransName,TransCycle,TransTime,LastTransTime
  FROM dbo.DTSConfig
 WHERE Enabled = 1 AND NeedTrans = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @TransName,@TransCycle,@TransTime,@LastTransTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 传送数据
	EXEC @rtn = dbo.Apq_DataTrans @TransName;
	
	IF(@@ERROR = 0 AND @rtn = 1)
	BEGIN
		UPDATE DTSConfig
		   SET NeedTrans = 0
		 WHERE TransName = @TransName;
	END

	NEXT_Trans:
	FETCH NEXT FROM @csr INTO @TransName,@TransCycle,@TransTime,@LastTransTime;
END
CLOSE @csr;

RETURN 1;
GO
/****** Object:  StoredProcedure [bak].[Job_Apq_Bak]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Job_Apq_Bak]
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 备份作业(仅本地)
-- 参数:
@JobTime: 作业启动时间
@DBName: 数据库名
@BakFolder: 备份路径
@FullCycle: 完整备份周期(天)
@FullTime: 完整备份时间(每次日期与周期相关)
@TrnCycle: 日志备份周期(分钟,请尽量作业周期的倍数)
@PreFullTime: 上一次完整备份时间
@PreBakTime: 上一次备份时间
@Num_Full: 备份文件保留个数
@NeedRestore: 是否需要还原
@RestoreFolder: 还原历史库的目录(本机)
@NeedTruncate: 是否需要截断日志(兼容2000)
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Job_Apq_Bak;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

--定义变量
DECLARE @ID bigint,
	@DBName nvarchar(256),	-- 数据库名
	@BakFolder nvarchar(4000),--备份目录
	@FTPFolder nvarchar(4000),--FTP目录
	@ReadyAction tinyint,	-- 备份操作
	@FullTime smalldatetime, -- 完整备份时间
	@PreFullTime datetime,	-- 上一次完整备份时间
	@PreBakTime datetime,	-- 上一次备份时间
	@Num_Full int,		-- 完整备份文件保留个数
	@FullFileToDel nvarchar(4000),--在此文件之前的文件将被删除
	@NeedRestore tinyint,
	@RestoreFolder nvarchar(4000),
	@DB_HisNum int
	,@Num_HisDB int	-- 临时变量:已有历史库个数
	
	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@JobTime datetime	-- 作业启动时间
	,@BakFile nvarchar(4000)		-- 临时变量:备份文件
	,@FullBakFile nvarchar(4000)	-- 完整备份文件
	,@TodayFullTime	datetime	-- 当天完整备份的时间
	,@BakFileName	nvarchar(512)	-- 完整备份文件名(不含目录)
	,@ProductVersion	decimal
	;
SELECT @JobTime=Convert(nvarchar(16),GetDate(),120) + ':00'	--只精确到分

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR STATIC FOR
SELECT ID,DBName,BakFolder+CASE RIGHT(BakFolder,1) WHEN '\' THEN '' ELSE '\' END,FTPFolder+CASE RIGHT(FTPFolder,1) WHEN '\' THEN '' ELSE '\' END
	,FullTime,PreFullTime,PreBakTime,Num_Full,ReadyAction,NeedRestore,RestoreFolder,DB_HisNum
  FROM bak.BakCfg
 WHERE Enabled = 1 AND ReadyAction > 0;

-- 接收cmd返回结果
CREATE TABLE #t(s nvarchar(4000));

OPEN @csr;
FETCH NEXT FROM @csr INTO @ID,@DBName,@BakFolder,@FTPFolder,@FullTime,@PreFullTime,@PreBakTime,@Num_Full,@ReadyAction,@NeedRestore,@RestoreFolder,@DB_HisNum;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 计算当天完整备份的时间
	IF((@ReadyAction & 1) <> 0)
	BEGIN
		UPDATE bak.BakCfg SET State = State | 1 WHERE ID = @ID;
	
		-- 先删除历史备份文件 ----------------------------------------------------------------------
		-- 删除保留个数以外的备份文件
		TRUNCATE TABLE #t;
		SELECT @cmd = 'dir /a:-d/b/o:n "' + @FTPFolder + @DBName + '[*].*"';
		INSERT #t(s) EXEC master..xp_cmdshell @cmd;
		
		SELECT TOP(@Num_Full) @FullFileToDel = s
		  FROM #t
		 WHERE Len(s) > Len(@DBName) + 6
			AND Left(s,Len(@DBName)+1) = @DBName+'['
			AND SubString(s,Len(@DBName)+ 15,5) = '].bak'
		 ORDER BY s DESC;
		IF(@@ROWCOUNT >= @Num_Full)
		BEGIN
			SELECT @cmd = '';
			SELECT @cmd = @cmd + '&&del /F /Q "' + @FTPFolder + s + '"'
			  FROM #t
			 WHERE s <= @FullFileToDel;

			IF(Len(@cmd) > 2)
			BEGIN
				SELECT @cmd = Right(@cmd,Len(@cmd)-2);
				SELECT @cmd;
				EXEC master..xp_cmdshell @cmd;
			END
		END
		-- =========================================================================================

		--完整备份
		EXEC @rtn = bak.Apq_Bak_Full @DBName, @BakFileName OUT;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			SELECT @TodayFullTime = CONVERT(nvarchar(11),@JobTime,120) + Right(Convert(nvarchar,@FullTime,120),8);
			UPDATE bak.BakCfg
			   SET PreBakTime = @JobTime, PreFullTime = @TodayFullTime, ReadyAction = 0
			 WHERE ID = @ID;

			-- 本地恢复历史库
			IF(@NeedRestore & 1 <> 0)
			BEGIN
				-- 删除历史库
				SELECT @Num_HisDB = Count(name)
				  FROM sys.databases
				 WHERE Len(name) > Len(@DBName) AND Left(name,Len(@DBName)+1) = @DBName + '_';
				IF(@Num_HisDB >= @DB_HisNum)
				BEGIN
					SELECT @sql = '';
					SELECT TOP(@Num_HisDB-@DB_HisNum+1) @sql = @sql + 'EXEC dbo.Apq_KILL_DB ''' + name + ''';DROP DATABASE [' + name + '];'
					  FROM sys.databases
					 WHERE Len(name) > Len(@DBName) AND Left(name,Len(@DBName)+1) = @DBName + '_'
					 ORDER BY name;
					EXEC sp_executesql @sql;
				END
				
				DECLARE @DBName_R nvarchar(256);
				SELECT @DBName_R = @DBName + '_' + LEFT(REPLACE(CONVERT(nvarchar,Dateadd(dd,-1,Substring(@BakFileName,Len(@DBName)+2,8)),120),'-',''),8)
					,@FullBakFile = @FTPFolder + @BakFileName;
				EXEC bak.Apq_Restore 1,@DBName_R,@FullBakFile,1,@RestoreFolder
				
				SELECT @ProductVersion = CONVERT(decimal,LEFT(Convert(nvarchar,SERVERPROPERTY('ProductVersion')),2));
				IF(@ProductVersion<10)
				BEGIN
					SELECT @sql='BACKUP LOG '+@DBName_R+' WITH NO_LOG';
					EXEC sp_executesql @sql;
					SELECT @sqlDB = 'DBCC SHRINKFILE(''' + @DBName + '_Log'',10)'
					SELECT @sql = 'EXEC [' + @DBName_R + ']..sp_executesql @sqlDB';
					EXEC sp_executesql @sql,N'@sqlDB nvarchar(4000)',@sqlDB=@sqlDB;
				END
			END
		END
		
		UPDATE bak.BakCfg SET State = State & (~1) WHERE ID = @ID;
	END
	ELSE IF((@ReadyAction & 2) <> 0)
	BEGIN
		UPDATE bak.BakCfg SET State = State | 2 WHERE ID = @ID;
		
		--日志备份
		EXEC @rtn = bak.Apq_Bak_Trn @DBName;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			UPDATE bak.BakCfg SET PreBakTime=@JobTime,ReadyAction = ReadyAction & (~2) WHERE ID = @ID;
		END
		
		UPDATE bak.BakCfg SET State = State & (~2) WHERE ID = @ID;
	END

	NEXT_DB:
	FETCH NEXT FROM @csr INTO @ID,@DBName,@BakFolder,@FTPFolder,@FullTime,@PreFullTime,@PreBakTime,@Num_Full,@ReadyAction,@NeedRestore,@RestoreFolder,@DB_HisNum;
END
CLOSE @csr;

DROP TABLE #t;
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[Apq_Pager]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-14
-- 描述: 分页
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(max);
EXEC @rtn = dbo.Apq_Pager @ExMsg out, 'ID, Name, Crt, Limit, Init, Inc, State, _Time, _InTime'
	, 'dbo.Apq_ID'
	, 'ID','_Time','', 20, 0;
SELECT @rtn, @ExMsg;
-- =============================================
*/
CREATE PROC [dbo].[Apq_Pager]
	@ExMsg nvarchar(max) out,
	@sCols		nvarchar(max),		-- 输出列(不支持*)
    @fTable		nvarchar(max),		-- 表/子查询(子查询时必须带括号传入且里面用到的表须加NOLOCK)
    @pKeys		nvarchar(max),		-- 主键(NOT EXISTS里使用)
    @oCols		nvarchar(max),		-- 排序列(分隔符[,])
	@oBy		nvarchar(max) = '',	-- 排序方式(分隔符[,],与列对应)
    @pSize		int = 20,			-- 每页行数
    @pNumber	int = 0				-- 页码(从0开始)
AS
SET NOCOUNT ON;

IF(@pKeys IS NULL OR Len(@pKeys) < 1) SELECT @pKeys = @sCols;

DECLARE @rtn int, @sql nvarchar(max), @sqlTable1 nvarchar(max), @sqlTable2 nvarchar(max), @sqlNE nvarchar(max), @sqlIdx nvarchar(max), @sqlOrder nvarchar(max)
	, @PagerNo nvarchar(21),@Apq_Pager1Name nvarchar(max);

-- [Build]ORDER BY 后的片段
IF(LEN(@oBy) > 1)
BEGIN
	SELECT @sqlOrder = '';
	DECLARE @p1 int, @p2 int, @q1 int, @q2 int;
	SELECT @p1 = 1, @p2 = LEN(@oCols), @q1 = 1, @q2 = LEN(@oBy);
	WHILE(@p2 > 0)
	BEGIN
		SELECT @p2 = CHARINDEX(',', @oCols, @p1);
		IF(@p2 = 0)
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ',' + SubString(@oCols, @p1, LEN(@oCols)+1-@p1);
		END
		ELSE
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ',' + SubString(@oCols, @p1, @p2-@p1);
		END

		SELECT @q2 = CHARINDEX(',', @oBy, @q1);
		IF(@q2 = 0)
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ' ' + SubString(@oBy, @q1, LEN(@oBy)+1-@q1);
		END
		ELSE
		BEGIN
			SELECT @sqlOrder = @sqlOrder + ' ' + SubString(@oBy, @q1, @q2-@q1);
		END

		SELECT @p1 = @p2 + 1;
		SELECT @q1 = @q2 + 1;
	END
	SELECT @sqlOrder = RIGHT(@sqlOrder,Len(@sqlOrder)-1);
END
ELSE
BEGIN
	SELECT @sqlOrder = @oCols + ' ' + @oBy;
END
--PRINT @sqlOrder;RETURN;

-- [Build]NOT EXISTS 里的关联条件,同时算出创建索引的语句
SELECT @sqlNE = '', @sqlIdx = '';
SELECT @p1 = 1, @p2 = LEN(@pKeys);
WHILE(@p2 > 0)
BEGIN
	SELECT @p2 = CHARINDEX(',', @pKeys, @p1);
	IF(@p2 = 0)
	BEGIN
		SELECT @sqlNE = @sqlNE + ' AND Apq_Pager1.' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1) + ' = Apq_Pager0.' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1);
		SELECT @sqlIdx = @sqlIdx + '
CREATE INDEX [IX_^Apq_Pager1$:' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1) + '] ON ^Apq_Pager1$(' + SubString(@pKeys, @p1, LEN(@pKeys)+1-@p1) + ');'
	END
	ELSE
	BEGIN
		SELECT @sqlNE = @sqlNE + ' AND Apq_Pager1.' + SubString(@pKeys, @p1, @p2-@p1) + ' = Apq_Pager0.' + SubString(@pKeys, @p1, @p2-@p1);
		SELECT @sqlIdx = @sqlIdx + '
CREATE INDEX [IX_^Apq_Pager1$:' + SubString(@pKeys, @p1, @p2-@p1) + '] ON ^Apq_Pager1$(' + SubString(@pKeys, @p1, @p2-@p1) + ');'
	END

	SELECT @p1 = @p2 + 1;
END
SELECT @sqlNE = '(' + RIGHT(@sqlNE,Len(@sqlNE)-5) + ')';
--PRINT @sqlNE;RETURN;

EXEC @rtn = dbo.Apq_Identifier @ExMsg OUT, @Name = 'Apq_Pager', @Count = 1, @Next = @PagerNo OUT
IF(@@ERROR <> 0 OR @rtn <> 1)
BEGIN
	RETURN -1;
END
SELECT @Apq_Pager1Name = 'dbo.Apq_Pager1_' + @PagerNo;

-- 取前(@pNumber)页的主键并建索引
SELECT @sqlTable1 = '
SELECT TOP ^TOP$ ^pKeys$ INTO ^Apq_Pager1$ FROM ^fTable$ Apq_Pager0 ORDER BY ^sqlOrder$;'
	+ @sqlIdx;
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^TOP$', @pNumber * @pSize);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^pKeys$', @pKeys);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^fTable$', @fTable);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^sqlOrder$', @sqlOrder);
SELECT @sqlTable1 = REPLACE(@sqlTable1,'^Apq_Pager1$', @Apq_Pager1Name);
--PRINT @sqlTable1;RETURN;
EXEC sp_executesql @sqlTable1;
SELECT @sqlTable1 = @Apq_Pager1Name;

-- 输出
SELECT @sqlTable2 = '
SELECT TOP ^pSize$ ^sCols$ FROM ^fTable$ Apq_Pager0
 WHERE NOT EXISTS(SELECT TOP 1 1 FROM ^sqlTable1$ Apq_Pager1 WHERE ^sqlNE$)
 ORDER BY ^sqlOrder$;';
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^fTable$', @fTable);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sqlTable1$', @sqlTable1);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^pSize$', @pSize);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sCols$', @sCols);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sqlNE$', @sqlNE);
SELECT @sqlTable2 = REPLACE(@sqlTable2,'^sqlOrder$', @sqlOrder);
--PRINT @sqlTable2; RETURN;
EXEC sp_executesql @sqlTable2;

-- 删除分页表
SELECT @sql ='
TRUNCATE TABLE ^sqlTable1$;
DROP TABLE ^sqlTable1$;';
SELECT @sql = REPLACE(@sql,'^sqlTable1$', @sqlTable1);
--PRINT @sql; RETURN;
EXEC sp_executesql @sql;

RETURN 1;
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertIP_Binary16]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-28
-- 描述: 将IP串转为 binary(16)
-- 示例:
SELECT dbo.Apq_ConvertIP_Binary16('FFFF:FFFF:FFFF:FFFF:FFFF::255.255.255.255');
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertIP_Binary16](
	@IP	varchar(max)
)RETURNS binary(16)
AS
BEGIN
	SELECT	@IP = LTRIM(RTRIM(@IP));

	DECLARE	@Return varbinary(max), @IsIP6 int
			,@Len int		-- 字符数
			,@Subs int		-- 分隔符(:)数量
			,@i int
			;
	SELECT	 @IsIP6 = CHARINDEX(':', @IP)
			,@Len = LEN(@IP)
			,@i = 0
			;
	SELECT	@Subs = LEN(REPLACE(@IP, ':', 'zz')) - @Len;

	IF(@IsIP6 > 0)
	BEGIN	-- IP6:(FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF)(FFFF::FFFF:FFFF)
			-- (FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:255.255.255.255)(FFFF::255.255.255.255)
		IF(CHARINDEX('.', @IP) > 0)
		BEGIN	-- 混合IP4
			DECLARE	@IP6End int;	-- IP6 的结束位置
			SELECT	@IP6End = 0;
			WHILE(@i < @Subs)
			BEGIN
				SELECT	@IP6End = CHARINDEX(':', @IP, @IP6End+1);

				SELECT	@i = @i + 1;
			END

			SELECT	@Return = dbo.Apq_ConvertIP6_VarBinary(SUBSTRING(@IP, 1, @IP6End), 6) + 
						dbo.Apq_ConvertIP4_VarBinary(SUBSTRING(@IP, @IP6End+1, @Len - @IP6End));
		END
		ELSE
		BEGIN	-- IP6标准地址
			SELECT	@Return = dbo.Apq_ConvertIP6_VarBinary(@IP, 8);
		END
	END
	ELSE
	BEGIN
		SELECT	@Return = 0x000000000000000000000000 + dbo.Apq_ConvertIP4_VarBinary(@IP);
	END

	RETURN @Return;
END
GO
/****** Object:  UserDefinedFunction [dbo].[Apq_ConvertBinary16_IP6]    Script Date: 11/09/2010 11:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-10-30
-- 描述: 将 binary(16) 转化为 IP6串
-- 示例:
SELECT dbo.Apq_ConvertBinary16_IP6(0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF);
-- =============================================
*/
CREATE FUNCTION [dbo].[Apq_ConvertBinary16_IP6](
	@binIP	binary(16)
)RETURNS varchar(max)
AS
BEGIN
	DECLARE	 @Return varchar(max)
			,@nIP int
			,@i int
			;
	SELECT	 @nIP = 0
			,@i = 1
			;

	WHILE(@i <= 16)
	BEGIN
		SELECT	@nIP = Convert(int, SUBSTRING(@binIP, @i, 2));
		SELECT	@Return = ISNULL(@Return, '') + ':' + dbo.Apq_ConvertScale(10, 16, Convert(varchar, @nIP));

		SELECT	@i = @i + 2;
	END

	RETURN SUBSTRING(@Return, 2, LEN(@Return)-1);
END
GO
/****** Object:  StoredProcedure [bak].[Apq_BakCfg_Full]    Script Date: 11/09/2010 11:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [bak].[Apq_BakCfg_Full]
	 @DBName		nvarchar(256)
	,@NeedRestore	tinyint = 0
	,@RestoreFolder	nvarchar(4000) = ''
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-17
-- 描述: 完整备份(仅本地)
-- 参数:
@DBName: 数据库名
@BakFolder: 备份路径
-- 示例:
DECLARE @rtn int;
EXEC @rtn = bak.Apq_BakCfg_Full 'Apq_DBA';
SELECT @rtn;
-- =============================================
-2: 空间不足
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @BakFileName nvarchar(256), @BakFileFullName nvarchar(4000)
	,@cmd nvarchar(4000), @sql nvarchar(4000)
	,@ID bigint
	,@BakFolder nvarchar(4000)
	,@FTPFolder nvarchar(4000)
	,@FTPFolderT nvarchar(4000)
	,@FTPFileFullName nvarchar(4000)
	,@NeedTruncate tinyint;
SELECT @SPBeginTime=GetDate();
SELECT @BakFolder = '', @NeedTruncate = 0;
SELECT @BakFolder = BakFolder, @FTPFolder = FTPFolder,@FTPFolderT = FTPFolderT, @NeedTruncate = NeedTruncate
  FROM bak.BakCfg
 WHERE DBName = @DBName;

IF(Len(@BakFolder)>3)
BEGIN
	IF(RIGHT(@BakFolder,1)<>'\') SELECT @BakFolder = @BakFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@BakFolder, 1, LEN(@BakFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolder)>3)
BEGIN
	IF(RIGHT(@FTPFolder,1)<>'\') SELECT @FTPFolder = @FTPFolder+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1);
	EXEC master..xp_cmdshell @cmd;
END
IF(Len(@FTPFolderT)>3)
BEGIN
	IF(RIGHT(@FTPFolderT,1)<>'\') SELECT @FTPFolderT = @FTPFolderT+'\';
	SELECT @cmd = 'md ' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1);
	EXEC master..xp_cmdshell @cmd;
END

-- 记录备份日志
SELECT @BakFileName = @DBName + '[' + LEFT(REPLACE(REPLACE(REPLACE(CONVERT(nvarchar,@SPBeginTime,120),'-',''),':',''),' ','_'),13)+'].bak';
SELECT @BakFileFullName = @BakFolder + @BakFileName;
SELECT @cmd = '@echo [' + convert(nvarchar,@SPBeginTime,120) + ']' + @BakFileName + '>>' + @BakFolder + '[Log]Apq_Bak.txt'
EXEC @rtn = master..xp_cmdshell @cmd;

-- 检测剩余空间 ------------------------------------------------------------------------------------
DECLARE @spused float, @disksp float;
SELECT @sql = '
CREATE TABLE #spused(
	name		nvarchar(256),
	rows		varchar(11),
	reserved	varchar(18),
	data		varchar(18),
	index_size	varchar(18),
	unused		varchar(18)
);
EXEC ' + @DBName + '..sp_MSforeachtable "INSERT #spused EXEC sp_spaceused ''?'', ''true''";
SELECT @spused = 0;
SELECT @spused = @spused + LEFT(reserved,LEN(reserved)-3) FROM #spused;
SELECT @spused = @spused / 1024;
DROP TABLE #spused;
';
EXEC @rtn = sp_executesql @sql, N'@spused float out', @spused = @spused out;
CREATE TABLE #drives(
	drive	varchar(5),
	MB		float
);
INSERT #drives
EXEC master..xp_fixeddrives;
SELECT @disksp = MB FROM #drives WHERE drive = LEFT(@BakFolder,1);
DROP TABLE #drives;

IF(@disksp < @spused * 0.7) RETURN -2; -- 暂取0.7
-- =================================================================================================

--截断日志(仅限2000使用)
IF(@NeedTruncate=1)
BEGIN
	SELECT @sql='BACKUP LOG '+@DBName+' WITH NO_LOG';
	EXEC sp_executesql @sql;
END

SELECT @sql = 'BACKUP DATABASE @DBName TO DISK=@BakFile WITH INIT';
EXEC @rtn = sp_executesql @sql, N'@DBName nvarchar(256), @BakFile nvarchar(4000)', @DBName = @DBName, @BakFile = @BakFileFullName;
IF(@@ERROR <> 0 OR @rtn<>0)
BEGIN
	RETURN -1;
END

-- 移动到FTP目录 -----------------------------------------------------------------------------------
IF(Len(@FTPFolderT)>0)
BEGIN
	SELECT @FTPFileFullName = @BakFolder + @BakFileName;
	SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolderT, 1, LEN(@FTPFolderT)-1) + '"';
	EXEC master..xp_cmdshell @cmd;
END
ELSE
BEGIN
	SELECT @FTPFolderT = @BakFolder;
END
SELECT @FTPFileFullName = @FTPFolderT + @BakFileName;
SELECT @cmd = 'move /y "' + @FTPFileFullName + '" "' + SUBSTRING(@FTPFolder, 1, LEN(@FTPFolder)-1) + '"';
EXEC master..xp_cmdshell @cmd;
-- =================================================================================================

-- 本地恢复历史库
IF(@NeedRestore & 1 <> 0)
BEGIN
	DECLARE @DBName_R nvarchar(256);
	SELECT @DBName_R = @DBName + '_' + LEFT(REPLACE(CONVERT(nvarchar,Dateadd(dd,-1,Substring(@BakFileName,Len(@DBName)+2,8)),120),'-',''),8)
		,@BakFileFullName = @FTPFolder + @BakFileName;
	EXEC bak.Apq_Restore 1,@DBName_R,@BakFileFullName,1,@RestoreFolder
END

SELECT BakFileName = @BakFileName;
RETURN 1;
GO
/****** Object:  Default [DF_BakCfg_Enabled]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_BakCfg_FTPFolder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_FTPFolder]  DEFAULT (N'D:\Bak2FTP') FOR [FTPFolder]
GO
/****** Object:  Default [DF_BakCfg_BakFolder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_BakFolder]  DEFAULT (N'D:\Bak') FOR [BakFolder]
GO
/****** Object:  Default [DF_BakCfg_FullTime]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_FullTime]  DEFAULT (dateadd(hour,(4),dateadd(day,(0),datediff(day,(0),getdate())))) FOR [FullTime]
GO
/****** Object:  Default [DF_BakCfg_FullCycle]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_FullCycle]  DEFAULT ((1)) FOR [FullCycle]
GO
/****** Object:  Default [DF_BakCfg_TrnCycle]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_TrnCycle]  DEFAULT ((30)) FOR [TrnCycle]
GO
/****** Object:  Default [DF_BakCfg_NeedTruncate]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_NeedTruncate]  DEFAULT ((0)) FOR [NeedTruncate]
GO
/****** Object:  Default [DF_BakCfg_ReadyAction]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_ReadyAction]  DEFAULT ((0)) FOR [ReadyAction]
GO
/****** Object:  Default [DF_BakCfg_NeedRestore]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_NeedRestore]  DEFAULT ((0)) FOR [NeedRestore]
GO
/****** Object:  Default [DF_BakCfg_RestoreFolder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_RestoreFolder]  DEFAULT (N'D:\DB_His') FOR [RestoreFolder]
GO
/****** Object:  Default [DF_BakCfg_State]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_State]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF_BakCfg_DB_HisNum]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_DB_HisNum]  DEFAULT ((15)) FOR [DB_HisNum]
GO
/****** Object:  Default [DF_BakCfg_Num_Full]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[BakCfg] ADD  CONSTRAINT [DF_BakCfg_Num_Full]  DEFAULT ((3)) FOR [Num_Full]
GO
/****** Object:  Default [DF_FTP_PutBak_LastFileName]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_LastFileName]  DEFAULT ('') FOR [LastFileName]
GO
/****** Object:  Default [DF_FTP_PutBak_Enabled]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_FTP_PutBak_Folder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_Folder]  DEFAULT (N'D:\Bak2FTP\') FOR [Folder]
GO
/****** Object:  Default [DF_FTP_PutBak_FTPFolder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_FTPFolder]  DEFAULT (N'/D/BakFromFTP/') FOR [FTPFolder]
GO
/****** Object:  Default [DF_FTP_PutBak_FTPFolderTmp]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_FTPFolderTmp]  DEFAULT (N'/') FOR [FTPFolderTmp]
GO
/****** Object:  Default [DF_FTP_PutBak_Num_Full]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_Num_Full]  DEFAULT ((3)) FOR [Num_Full]
GO
/****** Object:  Default [DF_FTP_PutBak_TransferIDCfg]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_TransferIDCfg]  DEFAULT ((0)) FOR [TransferIDCfg]
GO
/****** Object:  Default [DF_FTP_PutBak__InTime]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_FTP_PutBak__Time]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_FTP_PutBak_TransferIDRun]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[FTP_PutBak] ADD  CONSTRAINT [DF_FTP_PutBak_TransferIDRun]  DEFAULT ((0)) FOR [TransferIDRun]
GO
/****** Object:  Default [DF_RestoreFromFolder_LastFileName]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_LastFileName]  DEFAULT ('') FOR [LastFileName]
GO
/****** Object:  Default [DF_RestoreFromFolder_Enabled]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_RestoreFromFolder_BakFolder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_BakFolder]  DEFAULT (N'D:\BakFromFTP') FOR [BakFolder]
GO
/****** Object:  Default [DF_RestoreFromFolder_RestoreType]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_RestoreType]  DEFAULT ((0)) FOR [RestoreType]
GO
/****** Object:  Default [DF_RestoreFromFolder_RestoreFolder]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_RestoreFolder]  DEFAULT (N'D:\DB_His') FOR [RestoreFolder]
GO
/****** Object:  Default [DF_RestoreFromFolder_DB_HisNum]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_DB_HisNum]  DEFAULT ((15)) FOR [DB_HisNum]
GO
/****** Object:  Default [DF_RestoreFromFolder_Num_Full]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_Num_Full]  DEFAULT ((3)) FOR [Num_Full]
GO
/****** Object:  Default [DF_RestoreFromFolder_RunnerIDCfg]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_RunnerIDCfg]  DEFAULT ((0)) FOR [RunnerIDCfg]
GO
/****** Object:  Default [DF_RestoreFromFolder_RunnerIDRun]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder_RunnerIDRun]  DEFAULT ((0)) FOR [RunnerIDRun]
GO
/****** Object:  Default [DF_RestoreFromFolder__InTime]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_RestoreFromFolder__Time]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [bak].[RestoreFromFolder] ADD  CONSTRAINT [DF_RestoreFromFolder__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_Apq_Config_Class]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_Config] ADD  CONSTRAINT [DF_Apq_Config_Class]  DEFAULT ('') FOR [Class]
GO
/****** Object:  Default [DF_Apq_Ext_TableName]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_Ext] ADD  CONSTRAINT [DF_Apq_Ext_TableName]  DEFAULT ('') FOR [TableName]
GO
/****** Object:  Default [DF_Apq_Ext_ID]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_Ext] ADD  CONSTRAINT [DF_Apq_Ext_ID]  DEFAULT ((0)) FOR [ID]
GO
/****** Object:  Default [DF_Apq_ID_Crt]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID_Crt]  DEFAULT ((0)) FOR [Crt]
GO
/****** Object:  Default [DF_Apq_ID_Limit]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID_Limit]  DEFAULT ((9223372034707292159.)) FOR [Limit]
GO
/****** Object:  Default [DF_Apq_ID_Init]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID_Init]  DEFAULT ((0)) FOR [Init]
GO
/****** Object:  Default [DF_Apq_ID_Inc]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID_Inc]  DEFAULT ((1)) FOR [Inc]
GO
/****** Object:  Default [DF_Apq_ID_State]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID_State]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF_Apq_ID__Time]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_Apq_ID__InTime]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Apq_ID] ADD  CONSTRAINT [DF_Apq_ID__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_ArpCfg_Enabled]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[ArpCfg] ADD  CONSTRAINT [DF_ArpCfg_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_Cfg_WH_Enabled]    Script Date: 11/09/2010 11:49:45 ******/
ALTER TABLE [dbo].[Cfg_WH] ADD  CONSTRAINT [DF_Cfg_WH_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_DTS_Send_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_DTS_Send_RunnerIDCfg]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_RunnerIDCfg]  DEFAULT ((0)) FOR [RunnerIDCfg]
GO
/****** Object:  Default [DF_DTS_Send_TransMethod]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_TransMethod]  DEFAULT ((1)) FOR [TransMethod]
GO
/****** Object:  Default [DF_DTS_Send_SchemaName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_SchemaName]  DEFAULT (N'bcp') FOR [SchemaName]
GO
/****** Object:  Default [DF_DTS_Send_FTPIP]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_FTPIP]  DEFAULT ((0)) FOR [FTPIP]
GO
/****** Object:  Default [DF_DTS_Send_FTPFolderTmp]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_FTPFolderTmp]  DEFAULT (N'/') FOR [FTPFolderTmp]
GO
/****** Object:  Default [DF_DTS_Send_FTPFolder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_FTPFolder]  DEFAULT (N'/D/BakFromFTP/') FOR [FTPFolder]
GO
/****** Object:  Default [DF_DTS_Send_RunnerIDRun]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_RunnerIDRun]  DEFAULT ((0)) FOR [RunnerIDRun]
GO
/****** Object:  Default [DF_DTS_Send__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_DTS_Send__Time]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_DTS_Send_PickLastID]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_PickLastID]  DEFAULT ((-1)) FOR [PickLastID]
GO
/****** Object:  Default [DF_DTS_Send_PickLastTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTS_Send] ADD  CONSTRAINT [DF_DTS_Send_PickLastTime]  DEFAULT (getdate()) FOR [PickLastTime]
GO
/****** Object:  Default [DF_DTSConfig_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_DTSConfig_TransMethod]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_TransMethod]  DEFAULT ((1)) FOR [TransMethod]
GO
/****** Object:  Default [DF_DTSConfig_TransCycle]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_TransCycle]  DEFAULT ((1)) FOR [TransCycle]
GO
/****** Object:  Default [DF_DTSConfig_TransTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_TransTime]  DEFAULT (dateadd(hour,(4),dateadd(day,(0),datediff(day,(0),getdate())))) FOR [TransTime]
GO
/****** Object:  Default [DF_DTSConfig_SchemaName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_SchemaName]  DEFAULT (N'bcp') FOR [SchemaName]
GO
/****** Object:  Default [DF_DTSConfig__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_DTSConfig__Time]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_DTSConfig_NeedTrans]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_NeedTrans]  DEFAULT ((0)) FOR [NeedTrans]
GO
/****** Object:  Default [DF_DTSConfig_KillFtpTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[DTSConfig] ADD  CONSTRAINT [DF_DTSConfig_KillFtpTime]  DEFAULT (dateadd(month,(-1),getdate())) FOR [KillFtpTime]
GO
/****** Object:  Default [DF_FileTrans__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FileTrans] ADD  CONSTRAINT [DF_FileTrans__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_FTP_GetBak_LastFileName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_GetBak] ADD  CONSTRAINT [DF_FTP_GetBak_LastFileName]  DEFAULT ('') FOR [LastFileName]
GO
/****** Object:  Default [DF_FTP_GetBak_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_GetBak] ADD  CONSTRAINT [DF_FTP_GetBak_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_FTP_GetBak_Folder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_GetBak] ADD  CONSTRAINT [DF_FTP_GetBak_Folder]  DEFAULT (N'D:\Apq_DBA\FTP_GetBak\') FOR [Folder]
GO
/****** Object:  Default [DF_FTP_GetBak_FTPFolder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_GetBak] ADD  CONSTRAINT [DF_FTP_GetBak_FTPFolder]  DEFAULT (N'/') FOR [FTPFolder]
GO
/****** Object:  Default [DF_FTP_GetBak_State]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_GetBak] ADD  CONSTRAINT [DF_FTP_GetBak_State]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF_FTP_GetBak_DB_HisNum]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_GetBak] ADD  CONSTRAINT [DF_FTP_GetBak_DB_HisNum]  DEFAULT ((15)) FOR [DB_HisNum]
GO
/****** Object:  Default [DF_FTP_SendQueue_Folder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_Folder]  DEFAULT (N'D:\Bak2FTP\') FOR [Folder]
GO
/****** Object:  Default [DF_FTP_SendQueue_FileName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_FileName]  DEFAULT ('') FOR [FileName]
GO
/****** Object:  Default [DF_FTP_SendQueue_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_FTP_SendQueue_FTPFolder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_FTPFolder]  DEFAULT (N'/D/BakFromFTP/') FOR [FTPFolder]
GO
/****** Object:  Default [DF_FTP_SendQueue_FTPFolderTmp]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_FTPFolderTmp]  DEFAULT (N'/') FOR [FTPFolderTmp]
GO
/****** Object:  Default [DF_FTP_SendQueue__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_FTP_SendQueue__Time]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_FTP_SendQueue_LSize]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_LSize]  DEFAULT ((0)) FOR [LSize]
GO
/****** Object:  Default [DF_FTP_SendQueue_RSize]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_RSize]  DEFAULT ((0)) FOR [RSize]
GO
/****** Object:  Default [DF_FTP_SendQueue_IsSuccess]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[FTP_SendQueue] ADD  CONSTRAINT [DF_FTP_SendQueue_IsSuccess]  DEFAULT ((0)) FOR [IsSuccess]
GO
/****** Object:  Default [DF_Log_Apq_Alarm__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_Apq_Alarm] ADD  CONSTRAINT [DF_Log_Apq_Alarm__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_Log_Apq_Alarm_Type]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_Apq_Alarm] ADD  CONSTRAINT [DF_Log_Apq_Alarm_Type]  DEFAULT ((0)) FOR [Type]
GO
/****** Object:  Default [DF_Log_Apq_Alarm_Severity]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_Apq_Alarm] ADD  CONSTRAINT [DF_Log_Apq_Alarm_Severity]  DEFAULT ((16)) FOR [Severity]
GO
/****** Object:  Default [DF_Log_Apq_Alarm_Titile]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_Apq_Alarm] ADD  CONSTRAINT [DF_Log_Apq_Alarm_Titile]  DEFAULT ('') FOR [Titile]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick_PickTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick_PickTime]  DEFAULT (getdate()) FOR [PickTime]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick_HasContent]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick_HasContent]  DEFAULT ((1)) FOR [HasContent]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick_FileFolder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick_FileFolder]  DEFAULT (N'D:\DTS') FOR [FileFolder]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick_FileEX]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick_FileEX]  DEFAULT (N'.txt') FOR [FileEX]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick_t]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick_t]  DEFAULT (N'\t') FOR [t]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick_r]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick_r]  DEFAULT (N'\n') FOR [r]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_Log_DTS_LocalPick__Time]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[Log_DTS_LocalPick] ADD  CONSTRAINT [DF_Log_DTS_LocalPick__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_RDBConfig_RDBType]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RDBConfig] ADD  CONSTRAINT [DF_RDBConfig_RDBType]  DEFAULT ((0)) FOR [RDBType]
GO
/****** Object:  Default [DF_RDBConfig_PLevel]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RDBConfig] ADD  CONSTRAINT [DF_RDBConfig_PLevel]  DEFAULT ((1)) FOR [PLevel]
GO
/****** Object:  Default [DF_RDBConfig_GLevel]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RDBConfig] ADD  CONSTRAINT [DF_RDBConfig_GLevel]  DEFAULT ((2)) FOR [GLevel]
GO
/****** Object:  Default [DF_RDBConfig_GameID]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RDBConfig] ADD  CONSTRAINT [DF_RDBConfig_GameID]  DEFAULT ((0)) FOR [GameID]
GO
/****** Object:  Default [DF_RDBConfig_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RDBConfig] ADD  CONSTRAINT [DF_RDBConfig_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_RDBLogin_LoginPwdC]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RDBLogin] ADD  CONSTRAINT [DF_RDBLogin_LoginPwdC]  DEFAULT ('') FOR [LoginPwdC]
GO
/****** Object:  Default [DF_RSrvConfig_LSName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_LSName]  DEFAULT ('') FOR [LSName]
GO
/****** Object:  Default [DF_RSrvConfig_UID]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_UID]  DEFAULT ('') FOR [UID]
GO
/****** Object:  Default [DF_RSrvConfig_LSMaxTimes]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_LSMaxTimes]  DEFAULT ((10)) FOR [LSMaxTimes]
GO
/****** Object:  Default [DF_RSrvConfig_LSErrTimes]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_LSErrTimes]  DEFAULT ((0)) FOR [LSErrTimes]
GO
/****** Object:  Default [DF_RSrvConfig_LSState]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_LSState]  DEFAULT ((1)) FOR [LSState]
GO
/****** Object:  Default [DF_RSrvConfig_FTPPort]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_FTPPort]  DEFAULT ((21)) FOR [FTPPort]
GO
/****** Object:  Default [DF_RSrvConfig_SqlPort]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_SqlPort]  DEFAULT ((1433)) FOR [SqlPort]
GO
/****** Object:  Default [DF_RSrvConfig_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [dbo].[RSrvConfig] ADD  CONSTRAINT [DF_RSrvConfig_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_BcpInQueue_Folder]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_Folder]  DEFAULT (N'D:\BcpIn\XXX\20101029\') FOR [Folder]
GO
/****** Object:  Default [DF_BcpInQueue_FileName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_FileName]  DEFAULT ('') FOR [FileName]
GO
/****** Object:  Default [DF_BcpInQueue_Enabled]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_BcpInQueue_SchemaName]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_SchemaName]  DEFAULT (N'dbo') FOR [SchemaName]
GO
/****** Object:  Default [DF_BcpInQueue_t]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_t]  DEFAULT (N'\t') FOR [t]
GO
/****** Object:  Default [DF_BcpInQueue_r]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_r]  DEFAULT (N'\n') FOR [r]
GO
/****** Object:  Default [DF_BcpInQueue__InTime]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_BcpInQueue__Time]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_BcpInQueue_IsFinished]    Script Date: 11/09/2010 11:49:46 ******/
ALTER TABLE [etl].[BcpInQueue] ADD  CONSTRAINT [DF_BcpInQueue_IsFinished]  DEFAULT ((0)) FOR [IsFinished]
GO
/****** Object:  Default [DF_BcpSTableCfg_EtlName]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg_EtlName]  DEFAULT ('') FOR [EtlName]
GO
/****** Object:  Default [DF_BcpSTableCfg_SchemaName]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg_SchemaName]  DEFAULT (N'dbo') FOR [SchemaName]
GO
/****** Object:  Default [DF_BcpSTableCfg_Enabled]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_BcpSTableCfg_Cycle]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg_Cycle]  DEFAULT ((1440)) FOR [Cycle]
GO
/****** Object:  Default [DF_BcpSTableCfg_STime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg_STime]  DEFAULT (dateadd(hour,(1),dateadd(day,(0),datediff(day,(0),getdate())))) FOR [STime]
GO
/****** Object:  Default [DF_BcpSTableCfg__InTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_BcpSTableCfg__Time]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[BcpSTableCfg] ADD  CONSTRAINT [DF_BcpSTableCfg__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_EtlCfg_Folder]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_Folder]  DEFAULT (N'D:\BcpIn\XXX\20101029\') FOR [Folder]
GO
/****** Object:  Default [DF_EtlCfg_PeriodType]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_PeriodType]  DEFAULT ((6)) FOR [PeriodType]
GO
/****** Object:  Default [DF_EtlCfg_FileName]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_FileName]  DEFAULT ('') FOR [FileName]
GO
/****** Object:  Default [DF_EtlCfg_Enabled]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_EtlCfg_SchemaName]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_SchemaName]  DEFAULT (N'dbo') FOR [SchemaName]
GO
/****** Object:  Default [DF_EtlCfg_t]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_t]  DEFAULT (N'\t') FOR [t]
GO
/****** Object:  Default [DF_EtlCfg_r]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg_r]  DEFAULT (N'\n') FOR [r]
GO
/****** Object:  Default [DF_EtlCfg__InTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_EtlCfg__Time]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[EtlCfg] ADD  CONSTRAINT [DF_EtlCfg__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_LoadCfg_Enabled]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[LoadCfg] ADD  CONSTRAINT [DF_LoadCfg_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_LoadCfg_Cycle]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[LoadCfg] ADD  CONSTRAINT [DF_LoadCfg_Cycle]  DEFAULT ((1440)) FOR [Cycle]
GO
/****** Object:  Default [DF_LoadCfg_LTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[LoadCfg] ADD  CONSTRAINT [DF_LoadCfg_LTime]  DEFAULT (dateadd(hour,(1),dateadd(day,(0),datediff(day,(0),getdate())))) FOR [LTime]
GO
/****** Object:  Default [DF_LoadCfg__InTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[LoadCfg] ADD  CONSTRAINT [DF_LoadCfg__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_LoadCfg__Time]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[LoadCfg] ADD  CONSTRAINT [DF_LoadCfg__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_Log_Stat_StartTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[Log_Stat] ADD  CONSTRAINT [DF_Log_Stat_StartTime]  DEFAULT (dateadd(day,(-1),dateadd(day,(0),datediff(day,(0),getdate())))) FOR [StartTime]
GO
/****** Object:  Default [DF_Log_Stat_EndTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[Log_Stat] ADD  CONSTRAINT [DF_Log_Stat_EndTime]  DEFAULT (dateadd(day,(0),datediff(day,(0),getdate()))) FOR [EndTime]
GO
/****** Object:  Default [DF_Log_Stat__LogTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[Log_Stat] ADD  CONSTRAINT [DF_Log_Stat__LogTime]  DEFAULT (getdate()) FOR [_LogTime]
GO
/****** Object:  Default [DF_StatCfg_Enabled]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[StatCfg] ADD  CONSTRAINT [DF_StatCfg_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_StatCfg_Cycle]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[StatCfg] ADD  CONSTRAINT [DF_StatCfg_Cycle]  DEFAULT ((1440)) FOR [Cycle]
GO
/****** Object:  Default [DF_StatCfg_RTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[StatCfg] ADD  CONSTRAINT [DF_StatCfg_RTime]  DEFAULT (dateadd(hour,(1),dateadd(day,(0),datediff(day,(0),getdate())))) FOR [RTime]
GO
/****** Object:  Default [DF_StatCfg__InTime]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[StatCfg] ADD  CONSTRAINT [DF_StatCfg__InTime]  DEFAULT (getdate()) FOR [_InTime]
GO
/****** Object:  Default [DF_StatCfg__Time]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [etl].[StatCfg] ADD  CONSTRAINT [DF_StatCfg__Time]  DEFAULT (getdate()) FOR [_Time]
GO
/****** Object:  Default [DF_Server_Location]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_Location]  DEFAULT ('') FOR [Location]
GO
/****** Object:  Default [DF_Server_Usage]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_Usage]  DEFAULT ('') FOR [Usage]
GO
/****** Object:  Default [DF_Server_IPWan1]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPWan1]  DEFAULT ('') FOR [IPWan1]
GO
/****** Object:  Default [DF_Server_IPLan1]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPLan1]  DEFAULT ('') FOR [IPLan1]
GO
/****** Object:  Default [DF_Server_RdpPort]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_RdpPort]  DEFAULT ((3389)) FOR [RdpPort]
GO
/****** Object:  Default [DF_Server_SqlPort]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_SqlPort]  DEFAULT ((1433)) FOR [SqlPort]
GO
/****** Object:  Default [DF_Server_FTPPort]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_FTPPort]  DEFAULT ((21)) FOR [FTPPort]
GO
/****** Object:  Default [DF_Server_IPWan2]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPWan2]  DEFAULT ('') FOR [IPWan2]
GO
/****** Object:  Default [DF_Server_IPLan2]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPLan2]  DEFAULT ('') FOR [IPLan2]
GO
/****** Object:  Default [DF_Server_IPWan3]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPWan3]  DEFAULT ('') FOR [IPWan3]
GO
/****** Object:  Default [DF_Server_IPLan3]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPLan3]  DEFAULT ('') FOR [IPLan3]
GO
/****** Object:  Default [DF_Server_IPWan4]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPWan4]  DEFAULT ('') FOR [IPWan4]
GO
/****** Object:  Default [DF_Server_IPLan4]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[Server] ADD  CONSTRAINT [DF_Server_IPLan4]  DEFAULT ('') FOR [IPLan4]
GO
/****** Object:  Default [DF_SrvPwd_PwdType]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[SrvPwd] ADD  CONSTRAINT [DF_SrvPwd_PwdType]  DEFAULT ((2)) FOR [PwdType]
GO
/****** Object:  Default [DF_SrvPwd_Enabled]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[SrvPwd] ADD  CONSTRAINT [DF_SrvPwd_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF_SrvPwd_SID]    Script Date: 11/09/2010 11:49:47 ******/
ALTER TABLE [mgr].[SrvPwd] ADD  CONSTRAINT [DF_SrvPwd_SID]  DEFAULT ('0x01') FOR [SID]
GO
GRANT DELETE ON [dbo].[CounterData] TO [Performance_Log_User] AS [dbo]
GO
GRANT INSERT ON [dbo].[CounterData] TO [Performance_Log_User] AS [dbo]
GO
GRANT SELECT ON [dbo].[CounterData] TO [Performance_Log_User] AS [dbo]
GO
GRANT UPDATE ON [dbo].[CounterData] TO [Performance_Log_User] AS [dbo]
GO
GRANT DELETE ON [dbo].[CounterDetails] TO [Performance_Log_User] AS [dbo]
GO
GRANT INSERT ON [dbo].[CounterDetails] TO [Performance_Log_User] AS [dbo]
GO
GRANT SELECT ON [dbo].[CounterDetails] TO [Performance_Log_User] AS [dbo]
GO
GRANT UPDATE ON [dbo].[CounterDetails] TO [Performance_Log_User] AS [dbo]
GO
GRANT DELETE ON [dbo].[DisplayToID] TO [Performance_Log_User] AS [dbo]
GO
GRANT INSERT ON [dbo].[DisplayToID] TO [Performance_Log_User] AS [dbo]
GO
GRANT SELECT ON [dbo].[DisplayToID] TO [Performance_Log_User] AS [dbo]
GO
GRANT UPDATE ON [dbo].[DisplayToID] TO [Performance_Log_User] AS [dbo]
GO
GRANT SELECT ON [dbo].[RSrvConfig] TO [LinkIn] AS [dbo]
GO
GRANT EXECUTE ON [dbo].[Prws_Alarm] TO [Web_Bg] AS [dbo]
GO
