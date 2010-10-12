IF ( OBJECT_ID('dbo.ApqDBMgr_Servers_GetAll') IS NULL ) 
    EXEC sp_executesql N'CREATE FUNCTION dbo.ApqDBMgr_Servers_GetAll() RETURNS TABLE AS RETURN SELECT 1 ID' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-03-24
-- 描述: 获取服务器列表
-- 示例:
SELECT * FROM dbo.ApqDBMgr_Servers_GetAll();
-- =============================================
*/
ALTER FUNCTION dbo.ApqDBMgr_Servers_GetAll ( )
RETURNS TABLE
    AS RETURN
    SELECT  *
    FROM    dbo.RSrvConfig
GO
