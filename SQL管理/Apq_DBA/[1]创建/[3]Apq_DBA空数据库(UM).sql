 USE [master]
GO

DECLARE @cmd nvarchar(4000), @sql nvarchar(4000)
	,@DBFolder nvarchar(4000);
SELECT @DBFolder = 'D:\DB\Apq_DBA';
SELECT @cmd = 'md "' + @DBFolder + '"';
EXEC xp_cmdshell @cmd;

SELECT @sql = '
CREATE DATABASE Apq_DBA ON  PRIMARY 
( NAME = N''Apq_DBA'', FILENAME = N''' + @DBFolder + '\Apq_DBA.mdf'' , SIZE = 3968KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N''Apq_DBA_log'', FILENAME = N''' + @DBFolder + '\Apq_DBA_log.ldf'' , SIZE = 1024KB , FILEGROWTH = 1024KB)
';
EXEC sp_executesql @sql;

-- 数据库设置 --------------------------------------------------------------------------------------
USE [Apq_DBA]
GO
ALTER DATABASE Apq_DBA SET DB_CHAINING ON;
ALTER DATABASE Apq_DBA SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE; -- 需要单连接,因此在迁移/维护时才能做
ALTER DATABASE Apq_DBA SET AUTO_UPDATE_STATISTICS_ASYNC ON WITH ROLLBACK IMMEDIATE; -- 自动更新为异步方式(要求打开上一个选项)
ALTER DATABASE Apq_DBA SET AUTO_SHRINK ON WITH ROLLBACK IMMEDIATE;	-- 一般不设置该项
GO
-- =================================================================================================
