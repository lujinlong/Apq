IF ( object_id('dbo.Dinner_Admin_EmDinner_DoDinner','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Admin_EmDinner_DoDinner AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-16
-- 描述: 确认订餐
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Admin_EmDinner_DoDinner 1, 22, @Pager_RowCount out, '2011-04-11', '2011-04-18', 0
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Admin_EmDinner_DoDinner
    @BTime	datetime
   ,@ETime	datetime
AS
SET NOCOUNT ON ;

UPDATE dbo.EmDinner
   SET State = 1
 WHERE State = 0 AND DinnerTime >= @BTime AND DinnerTime < @ETime;

RETURN 1;
GO
