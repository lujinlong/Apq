IF ( object_id('dbo.Dinner_Restaurant_List','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Restaurant_List AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-13
-- 描述: 餐馆列表
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Restaurant_List 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Restaurant_List
AS
SET NOCOUNT ON ;

SELECT RestID, RestName, RestAddr
  FROM dbo.Restaurant

RETURN 1;
GO
