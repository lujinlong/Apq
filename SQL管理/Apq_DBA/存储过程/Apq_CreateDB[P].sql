IF( OBJECT_ID('dbo.Apq_CreateDB', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_CreateDB AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_CreateDB
	 @DBName		nvarchar(256)
	,@RootFolder	nvarchar(3000) = ''
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-01-04
-- 描述: 创建数据库
	目录: 未指定根目录时使用默认根目录,再以数据库建立该库文件目录
	设置: 数据文件增长100M,日志文件增长100M
		打开自动异步更新统计信息
		打开行版本控制
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_CreateDB 'Apq_DBA3';
SELECT @rtn;
-- =============================================
-1:数据库名必须指定!
-2:已存在的数据库名!
-3:其余异常
*/
SET NOCOUNT ON;

IF(@DBName IS NULL OR Len(@DBName) < 1)
BEGIN
	PRINT '数据库名必须指定!';
	RETURN -1;
END

SELECT @RootFolder = isnull(@RootFolder,'')
	,@DBName = LTRIM(RTRIM(@DBName))
	;

IF(EXISTS(SELECT TOP 1 1 FROM master..sysdatabases WHERE name = @DBName))
BEGIN
	PRINT '已存在的数据库名!';
	RETURN -2;
END

SELECT @RootFolder = @RootFolder + CASE WHEN LEN(@RootFolder) > 1 AND RIGHT(@RootFolder,1) <> '\' THEN '\' ELSE '' END;

DECLARE @rtn int, @SPBeginTime datetime
	,@cmd nvarchar(4000), @sql nvarchar(4000), @sqlDB nvarchar(4000)
	,@DBFolder nvarchar(4000)
	,@DBOwner nvarchar(256)
	;
SELECT @SPBeginTime=GetDate();

IF(Len(@RootFolder) < 1)
BEGIN
	SELECT @RootFolder = dbo.Apq_Ext_Get('dbo.Apq_CreateDB',0,'RootFolder');
	IF(@RootFolder IS NULL OR Len(@RootFolder) < 1) SELECT @RootFolder = 'D:\DB\';
END

SELECT @DBOwner = 'sa';
SELECT @DBOwner = name
  FROM master..syslogins
 WHERE sid = 0x01;

SELECT @DBFolder = @RootFolder + @DBName;
SELECT @cmd = 'md "' + @DBFolder + '"';
EXEC master..xp_cmdshell @cmd;
SELECT @DBFolder = @DBFolder + '\';

SELECT @sql = '
CREATE DATABASE [' + @DBName + '] ON  PRIMARY 
( NAME = N''' + @DBName + ''', FILENAME = N''' + @DBFolder + @DBName + '.mdf'' , SIZE = 3072KB , FILEGROWTH = 102400KB )
 LOG ON 
( NAME = N''' + @DBName + '_log'', FILENAME = N''' + @DBFolder + @DBName + '_log.ldf'' , SIZE = 1024KB , MAXSIZE = 102400000KB , FILEGROWTH = 102400KB );

ALTER DATABASE [' + @DBName + '] SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE;
ALTER DATABASE [' + @DBName + '] SET AUTO_UPDATE_STATISTICS_ASYNC ON;
ALTER AUTHORIZATION ON DATABASE::' + @DBName + ' TO ' + @DBOwner + ';
';

EXEC @rtn = sp_executesql @sql;
IF(@@ERROR = 0 AND @rtn = 0) RETURN 1;
RETURN -3;
GO
