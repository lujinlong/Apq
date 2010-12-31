IF ( OBJECT_ID('dbo.Apq_KILL_Login','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_KILL_Login AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 断开某登录的所有连接
-- 示例:
EXEC dbo.Apq_KILL_Login N'LoginName'
-- =============================================
-1:不存在的登录名!
*/
ALTER PROC dbo.Apq_KILL_Login
    @LoginName nvarchar(256)
AS 
SET NOCOUNT ON ;

IF(Len(@LoginName) < 1) SELECT @LoginName = NULL ;

SELECT TOP(1) 1
  FROM master.sys.syslogins
 WHERE name = @LoginName;
IF(@@ROWCOUNT = 0)
BEGIN
	PRINT '不存在的登录名!';
	RETURN -1 ;
END

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

INSERT #sp_who EXEC sp_who @LoginName;
SELECT * FROM #sp_who
 WHERE spid > 50 AND spid <> @@spid;

SET	@pSession = CURSOR FOR
SELECT DISTINCT N'KILL ' + CAST(spid AS nvarchar)
  FROM #sp_who
 WHERE spid > 50 AND spid <> @@spid;

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
