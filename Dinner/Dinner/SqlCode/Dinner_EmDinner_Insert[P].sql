IF ( object_id('dbo.Dinner_EmDinner_Insert','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_EmDinner_Insert AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-16
-- 描述: 员工点餐
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_EmDinner_Insert 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_EmDinner_Insert
	 @ExMsg nvarchar(max) OUT

	,@EmID		bigint
	,@FoodID	bigint
AS
SET NOCOUNT ON ;

DECLARE @rtn int, @ExMsg1 nvarchar(max);

INSERT dbo.EmDinner ( EmID,FoodID,FoodPrice,FoodName,RestID,RestName )
SELECT @EmID, @FoodID, FoodPrice, FoodName,f.RestID,RestName
  FROM dbo.Food f INNER JOIN dbo.Restaurant r ON f.RestID = r.RestID
 WHERE FoodID = @FoodID;

RETURN 1;
GO
