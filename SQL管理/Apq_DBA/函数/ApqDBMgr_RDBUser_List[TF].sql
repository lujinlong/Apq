IF ( OBJECT_ID('dbo.ApqDBMgr_RDBUser_List') IS NULL ) 
    EXEC sp_executesql N'CREATE FUNCTION dbo.ApqDBMgr_RDBUser_List() RETURNS TABLE AS RETURN SELECT 1 ID' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT dbo.ApqDBMgr_RDBUser_List('FFFF::FFFF', 8);
-- =============================================
*/
ALTER FUNCTION dbo.ApqDBMgr_RDBUser_List ( )
RETURNS TABLE
    AS RETURN
    SELECT  [RDBUserID],RDBID,DBUserName,DBUserDesc,RDBLoginID
    FROM    dbo.RDBUser
GO
