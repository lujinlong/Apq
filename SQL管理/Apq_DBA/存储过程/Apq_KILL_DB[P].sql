IF( OBJECT_ID('dbo.Apq_KILL_DB', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_KILL_DB AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-09-20
-- 描述: 断开某用户数据库的所有连接
-- 示例:
EXEC dbo.Apq_KILL_DB N'DBName'
-- =============================================
*/
ALTER PROC dbo.Apq_KILL_DB
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
