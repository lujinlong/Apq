IF ( object_id('dbo.Job_updatestats','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_updatestats AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-01
-- 描述: 更新所有数据库的统计信息
-- 示例:
EXEC dbo.Job_updatestats
-- =============================================
*/
ALTER PROC dbo.Job_updatestats
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
