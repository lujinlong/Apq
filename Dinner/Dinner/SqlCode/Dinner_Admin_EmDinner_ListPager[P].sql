IF ( object_id('dbo.Dinner_Admin_EmDinner_ListPager','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Admin_EmDinner_ListPager AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-16
-- 描述: 后台统计点餐历史
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Admin_EmDinner_ListPager 1, 22, @Pager_RowCount out, '2011-04-11', '2011-04-18', 0
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Admin_EmDinner_ListPager
    @Pager_Page		int = 0				-- 页码(从0开始)
   ,@Pager_PageSize	int = 20			-- 每页行数
   ,@Pager_RowCount	bigint OUT			-- 输出总行数
   
   ,@BTime	datetime
   ,@ETime	datetime
   ,@RestID	bigint
   ,@State	int = 0
AS
SET NOCOUNT ON ;

DECLARE @nTop0 int;
SELECT @nTop0 = @Pager_PageSize * @Pager_Page;

SELECT @Pager_RowCount = Count(1) FROM dbo.EmDinner e WHERE State = @State AND DinnerTime >= @BTime AND DinnerTime < @ETime AND (@RestID = 0 OR RestID = @RestID);

SELECT TOP(@Pager_PageSize) ID, DinnerTime, l.EmID, FoodID, FoodPrice, FoodName, RestID, RestName, EmName,State
  FROM dbo.EmDinner l INNER JOIN dbo.Employee e ON l.EmID = e.EmID
 WHERE NOT EXISTS(SELECT TOP 1 1 FROM (SELECT TOP(@nTop0) ID FROM dbo.EmDinner l0(NOLOCK) WHERE State = @State AND DinnerTime >= @BTime AND DinnerTime < @ETime AND (@RestID = 0 OR RestID = @RestID) ORDER BY l0.ID DESC) Apq_Pager0 WHERE l.ID = Apq_Pager0.ID)
	AND State = @State
	AND DinnerTime >= @BTime AND DinnerTime < @ETime
	AND (@RestID = 0 OR RestID = @RestID)
 ORDER BY l.ID DESC;

RETURN 1;
GO
