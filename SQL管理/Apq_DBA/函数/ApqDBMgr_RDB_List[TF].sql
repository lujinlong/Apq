IF ( OBJECT_ID('dbo.ApqDBMgr_RDB_List') IS NULL ) 
    EXEC sp_executesql N'CREATE FUNCTION dbo.ApqDBMgr_RDB_List() RETURNS TABLE AS RETURN SELECT 1 ID' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT dbo.ApqDBMgr_RDB_List('FFFF::FFFF', 8);
-- =============================================
*/
ALTER FUNCTION dbo.ApqDBMgr_RDB_List ( )
RETURNS TABLE
    AS RETURN
    SELECT  RDBID,DBName,RDBDesc,RDBType,PLevel,GLevel,SrvID,GameID
    FROM    dbo.RDBConfig
GO
