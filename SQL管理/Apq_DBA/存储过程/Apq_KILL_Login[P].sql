IF ( OBJECT_ID('dbo.Apq_KILL_Login','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_KILL_Login AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-13
-- 描述: 断开某用户数据库的所有连接
-- 示例:
EXEC dbo.Apq_KILL_Login N'LoginName'
-- =============================================
*/
ALTER PROC dbo.Apq_KILL_Login
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
