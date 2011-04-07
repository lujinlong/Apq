IF ( object_id('dbo.ApqDBMgr_DBC_List','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_DBC_List AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: 获取Sql实例列表
-- 示例:
EXEC dbo.ApqDBMgr_DBC_List DEFAULT, 'Apq_Ext', DEFAULT
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_DBC_List
AS 
SET NOCOUNT ON ;

SELECT s.ComputerID, d.SqlID, [DBID], DBCType, UseTrusted, DBName, d.UserId, d.PwdC, Mirror, [Option]
  FROM dbo.DBC d LEFT JOIN dbo.SqlInstance s ON d.SqlID = s.SqlID
GO
