IF ( object_id('dbo.Prws_Alarm','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Prws_Alarm AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2010-06-03
-- 描述: 读取报警列表信息
-- 示例:
EXEC dbo.Prws_Alarm DEFAULT, 'Apq_ID', DEFAULT
-- =============================================
*/
ALTER PROC dbo.Prws_Alarm
    @BTime datetime	-- 开始时间
   ,@ETime datetime OUT	-- 结束时间
AS
SET NOCOUNT ON ;

DECLARE @Now datetime;
SELECT @Now = getdate();

IF(@BTime IS NULL) SELECT @BTime = Dateadd(dd,-1,@Now);

SELECT @ETime = max(_InTime) FROM dbo.Log_Apq_Alarm WHERE [_InTime] >= @BTime;
IF(@ETime IS NULL) SELECT @ETime = @Now

SELECT ID, _InTime, Type, Severity, Msg
  FROM dbo.Log_Apq_Alarm(NOLOCK)
 WHERE [_InTime] > @BTime AND [_InTime] <= @ETime;

RETURN 1;
GO
GRANT EXEC ON dbo.Prws_Alarm TO Web_Bg
GO
