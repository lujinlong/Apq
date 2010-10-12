IF( OBJECT_ID('dbo.GameWS_UserChannelPayLimit_List', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.GameWS_UserChannelPayLimit_List AS BEGIN RETURN END';
GO
ALTER PROC dbo.GameWS_UserChannelPayLimit_List
	 @ExMsg	nvarchar(max) OUT
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-10-09
-- 描述: 获取配置列表
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.GameWS_UserChannelPayLimit_List;
SELECT @rtn;
-- =============================================
*/
SET NOCOUNT ON;

SELECT ID, LimitType, PayWay, Limit, _InTime, _Time
  FROM dbo.UserChannelPayLimit

SELECT @ExMsg = '列表成功！';
RETURN 1;
GO
GRANT EXECUTE ON [dbo].[GameWS_UserChannelPayLimit_List] TO GameW
GO
