IF ( object_id('dbo.Dinner_Restaurant_Save','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Restaurant_Save AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-13
-- 描述: 餐馆列表
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Restaurant_Save 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Restaurant_Save
	 @ExMsg nvarchar(max) OUT

	,@RestID		bigint out
	
	,@RestName	nvarchar(50)
	,@RestAddr	nvarchar(200)
AS
SET NOCOUNT ON ;

DECLARE @rtn int, @ExMsg1 nvarchar(max);
DECLARE @RestIDDB bigint;

IF(@RestID >= 0)
BEGIN
	UPDATE dbo.Restaurant
	   SET RestName = @RestName, RestAddr = @RestAddr
	 WHERE RestID = @RestID;
	IF(@@ROWCOUNT = 0)
	BEGIN
		EXEC @rtn = dbo.Apq_Identifier @ExMsg1 OUT,'dbo.Restaurant',1,@RestIDDB OUT;
		IF(@@ERROR = 0 AND @rtn = 1)
		BEGIN
			INSERT dbo.Restaurant ( RestID,RestName,RestAddr )
			VALUES ( @RestIDDB,@RestName,@RestAddr);
			SELECT @RestID = @RestIDDB;
		END
	END
END
ELSE
BEGIN
	SELECT @ExMsg = '餐馆编号异常';
	RETURN -1;
END

RETURN 1;
GO
