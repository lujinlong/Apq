IF ( object_id('dbo.Dinner_Stat_EmDinner_Food','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Stat_EmDinner_Food AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-16
-- 描述: 后台统计点餐历史
-- 示例:
EXEC dbo.Dinner_Stat_EmDinner_Food '2011-04-11', '2011-04-18'
-- =============================================
*/
ALTER PROC dbo.Dinner_Stat_EmDinner_Food
    @BTime	datetime
   ,@ETime	datetime
   ,@State	int
AS
SET NOCOUNT ON ;

SELECT DinnerTime=DateAdd(dd,0,datediff(dd,0,DinnerTime)), FoodID, FoodMoney=Sum(FoodPrice), FoodName, RestID, RestName, FoodCount=Count(1)
  FROM dbo.EmDinner l
 WHERE DinnerTime >= @BTime AND DinnerTime < @ETime AND State = @State
 GROUP BY DateAdd(dd,0,datediff(dd,0,DinnerTime)), FoodID, FoodName, RestID, RestName

RETURN 1;
GO
