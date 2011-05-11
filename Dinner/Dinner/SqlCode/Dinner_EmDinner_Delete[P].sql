IF ( object_id('dbo.Dinner_EmDinner_Delete','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_EmDinner_Delete AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-16
-- 描述: 取消点餐
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_EmDinner_Delete 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_EmDinner_Delete
	 @ExMsg nvarchar(max) OUT

	,@ID	bigint
AS
SET NOCOUNT ON ;

DECLARE @rtn int, @ExMsg1 nvarchar(max);

DELETE dbo.EmDinner WHERE ID = @ID AND State = 0;
IF(@@ROWCOUNT = 0)
BEGIN
	IF(EXISTS(SELECT TOP 1 1 FROM dbo.EmDinner WHERE ID = @ID))
	BEGIN
		SELECT @ExMsg = '对不起,该餐已订购,不能取消.';
		RETURN -1;
	END
	ELSE
	BEGIN
		SELECT @ExMsg = '对不起,没有此订餐信息.';
		RETURN -1;
	END
END

RETURN 1;
GO
