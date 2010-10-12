IF ( OBJECT_ID('dbo.Apq_WH','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_WH AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 维护作业:1.默认值重命名,2.整理索引(如果碎片率低于30％用INDEXDEFRAG，如果高于30％用DBREINDEX)
-- 示例:
EXEC dbo.Apq_WH
-- =============================================
*/
ALTER PROC dbo.Apq_WH
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
