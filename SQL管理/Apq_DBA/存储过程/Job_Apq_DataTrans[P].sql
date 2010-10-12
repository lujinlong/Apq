IF( OBJECT_ID('dbo.Job_Apq_DataTrans', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Job_Apq_DataTrans AS BEGIN RETURN END';
GO
ALTER PROC dbo.Job_Apq_DataTrans
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-08-11
-- 描述: 数据传送作业
-- 参数:
@TransName: 传送名
@TransCycle: 传送周期(天)
@TransTime: 传送时间
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Job_Apq_DataTrans;
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

--定义变量
DECLARE @TransName nvarchar(50), -- 传送名
	@TransCycle int,-- 传送周期(天)
	@TransTime datetime,-- 传送时间
	@LastTransTime datetime -- 上次传送时间
	
	,@rtn int, @cmd nvarchar(4000), @sql nvarchar(4000)
	,@SPBeginTime datetime		-- 存储过程启动时间
	,@TodayTransTime datetime	-- 当天理论传送时间
	;
SELECT @SPBeginTime=GetDate();

--定义游标
DECLARE @csr CURSOR;
SET @csr=CURSOR FOR
SELECT TransName,TransCycle,TransTime,LastTransTime
  FROM dbo.DTSConfig
 WHERE Enabled = 1 AND NeedTrans = 1;

OPEN @csr;
FETCH NEXT FROM @csr INTO @TransName,@TransCycle,@TransTime,@LastTransTime;
WHILE(@@FETCH_STATUS=0)
BEGIN
	-- 传送数据
	EXEC @rtn = dbo.Apq_DataTrans @TransName;
	
	IF(@@ERROR = 0 AND @rtn = 1)
	BEGIN
		UPDATE DTSConfig
		   SET NeedTrans = 0
		 WHERE TransName = @TransName;
	END

	NEXT_Trans:
	FETCH NEXT FROM @csr INTO @TransName,@TransCycle,@TransTime,@LastTransTime;
END
CLOSE @csr;

RETURN 1;
GO
