
IF( OBJECT_ID('dbo.Apq_Tree_City_Save_PID', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_Tree_City_Save_PID AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-11-14
-- 描述: 保存城市名
-- 输入: 城市名及上级ID
-- 输出: 城市ID
-- 示例:
DECLARE @rtn int, @ExMsg nvarchar(4000), @CityID bigint;
EXEC @rtn = dbo.Apq_Tree_City_Save_PID @ExMsg out, '诸城', 1, @CityID out;
SELECT @rtn, @ExMsg, @CityID;
-- =============================================
*/
ALTER PROC dbo.Apq_Tree_City_Save_PID
	@ExMsg	nvarchar(4000) out,

	@CityName	nvarchar(450),	-- 地名

	@ParentID	bigint	-- 上级ID

	,@CityID	bigint out
AS
SET NOCOUNT ON;

DECLARE @rtn int;

-- 设置城市ID
IF(@CityID IS NULL OR @CityID = 0)
BEGIN
	EXEC @rtn = dbo.Apq_Identifier @ExMsg out, N'Apq_Tree_City', 1, @CityID out;
	IF(@@ERROR <> 0 OR @rtn <> 1)
	BEGIN
		RETURN -1;
	END
END

-- 更新信息
UPDATE Apq_Tree_City SET _Time = getdate(), ParentID = @ParentID, @CityID = ID WHERE Name = @CityName;
IF(@@ROWCOUNT = 0)
BEGIN
	INSERT Apq_Tree_City(ID,ParentID,Name) VALUES(@CityID,@ParentID,@CityName);
END
RETURN 1;
GO
