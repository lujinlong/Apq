
IF( OBJECT_ID('dbo.Apq_Tree_City_Save_PName', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Tree_City_Save_PName AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-11-19
-- 描述: 保存城市名
-- 输入: 城市名及上级名
-- 输出: 城市ID
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(4000), @CityID bigint;
EXEC @rtn = dbo.Apq_Tree_City_Save_PName @ExMsg out, '诸城', '山东', @CityID out;
SELECT @rtn, @ExMsg, @CityID;
-- =============================================
*/
ALTER PROC dbo.Apq_Tree_City_Save_PName
	@ExMsg	nvarchar(4000) out,

	@CityName	nvarchar(450),	-- 地名

	@ParentName	nvarchar(450)	-- 上级名

	,@CityID	bigint out
AS
SET NOCOUNT ON;

DECLARE @rtn int, @ParentID bigint;
-- 获取上级ID
SELECT TOP 1 @ParentID = ID FROM Apq_Tree_City WHERE Name = @ParentName ORDER BY _Time DESC;

EXEC dbo.Apq_Tree_City_Save_PID @ExMsg out, @CityName, @ParentID, @CityID out;
RETURN 1;
GO
