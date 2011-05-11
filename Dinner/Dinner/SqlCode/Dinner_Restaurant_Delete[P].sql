IF ( object_id('dbo.Dinner_Restaurant_Delete','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Restaurant_Delete AS BEGIN RETURN END' ;
GO
/* =============================================
-- ◊˜’ﬂ: ª∆◊⁄“¯
-- »’∆⁄: 2011-04-13
-- √Ë ˆ: ≤Õπ›…æ≥˝
--  æ¿˝:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Restaurant_Delete 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Restaurant_Delete
	 @ExMsg nvarchar(max) OUT

	,@RestID bigint
AS
SET NOCOUNT ON ;

DECLARE @rtn int, @ExMsg1 nvarchar(max);

IF(@RestID >= 0)
BEGIN
	DELETE dbo.Restaurant WHERE RestID = @RestID;
END
ELSE
BEGIN
	SELECT @ExMsg = '≤Õπ›±‡∫≈“Ï≥£';
	RETURN -1;
END

RETURN 1;
GO
