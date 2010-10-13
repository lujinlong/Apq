IF( OBJECT_ID('dbo.Apq_ID_Recofig', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_ID_Recofig AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_ID_Recofig
AS
SET NOCOUNT ON;
/* =============================================
-- 作者: 黄宗银
-- 日期: 2008-02-26
-- 描述: 操作 Apq_ID, 将正常行的当前值正确配置, 请确保在 配置启用前 或 连接少的时候 调用
-- 示例:
INSERT Apq_ID(Name, Init, Limit, Inc) VALUES('GameUser', 40000000, 60000000, 1);
DECLARE	@rtn int;
EXEC @rtn = dbo.Apq_ID_Recofig;
SELECT	@rtn;
-- =============================================
*/

UPDATE Apq_ID
   SET Crt = Init
 WHERE State = 0
	AND NOT (Inc > 0 AND Init <= Crt AND Crt < Limit)
	AND NOT (Inc < 0 AND Limit < Crt AND Crt <= Init);
GO
EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_ID_Recofig', DEFAULT
GO
