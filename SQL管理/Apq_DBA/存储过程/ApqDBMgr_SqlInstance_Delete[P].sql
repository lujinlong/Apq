IF ( object_id('dbo.ApqDBMgr_SqlInstance_Delete','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.ApqDBMgr_SqlInstance_Delete AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-01
-- 描述: 删除Sql实例
-- 示例:
EXEC dbo.ApqDBMgr_SqlInstance_Delete 1
	,-1,-1,'测试数据',0,-1,'172.16.0.20',1433,'apq', 'f'
-- =============================================
*/
ALTER PROC dbo.ApqDBMgr_SqlInstance_Delete
	@SqlID			int
AS
SET NOCOUNT ON ;

DELETE dbo.SqlInstance WHERE SqlID = @SqlID;
GO
