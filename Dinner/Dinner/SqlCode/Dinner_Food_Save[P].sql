IF ( object_id('dbo.Dinner_Food_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Food_Save AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-13
-- 描述: 菜品保存
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Food_Save 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Food_Save
	 @ExMsg nvarchar(max) OUT

	,@FoodID		bigint out
	
	,@RestID		bigint
	,@FoodName		nvarchar(50)
	,@FoodPrice		money
AS
SET NOCOUNT ON ;

DECLARE @rtn int, @ExMsg1 nvarchar(max);
DECLARE @FoodIDDB bigint;

IF(@RestID >= 0)
BEGIN
	UPDATE dbo.Food
	   SET RestID = @RestID, FoodName = @FoodName, FoodPrice = @FoodPrice
	 WHERE FoodID = @FoodID;
	IF(@@ROWCOUNT = 0)
	BEGIN
		EXEC @rtn = dbo.Apq_Identifier @ExMsg1 OUT,'dbo.Food',1,@FoodIDDB OUT;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			INSERT dbo.Food ( FoodID,RestID,FoodName,FoodPrice )
			VALUES ( @FoodIDDB,@RestID,@FoodName,@FoodPrice);
			SELECT @FoodID = @FoodIDDB;
		END
	END
END
ELSE
BEGIN
	SELECT @ExMsg = '菜品编号异常';
	RETURN -1;
END

RETURN 1;
GO
