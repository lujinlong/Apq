IF ( OBJECT_ID('dbo.Apq_Login_StatCount','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Login_StatCount AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-04-14
-- 描述: 统计指定登录名的连接数(未指定时为所有登录名)
-- 示例:
EXEC dbo.Apq_Login_StatCount N'LoginName'
-- =============================================
*/
ALTER PROC dbo.Apq_Login_StatCount
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
