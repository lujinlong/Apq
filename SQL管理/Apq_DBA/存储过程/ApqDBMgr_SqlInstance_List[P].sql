IF ( object_id('dbo.ApqDBMgr_SqlInstance_List','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_SqlInstance_List AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: 获取Sql实例列表
-- 示例:
EXEC dbo.ApqDBMgr_SqlInstance_List DEFAULT, 'Apq_Ext', DEFAULT
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_SqlInstance_List
AS 
SET NOCOUNT ON ;

SELECT ComputerID = ISNULL(i.ComputerID,-1), ComputerName = ISNULL(ComputerName,''), SqlID, SqlName, ParentID, SqlType, IP, SqlPort, UserId, PwdC
  FROM dbo.SqlInstance i LEFT JOIN dbo.Computer c ON i.ComputerID = c.ComputerID;
GO
