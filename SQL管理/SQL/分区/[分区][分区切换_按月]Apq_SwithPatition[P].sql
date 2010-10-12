IF( OBJECT_ID('dbo.Apq_SwithPatition', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_SwithPatition AS BEGIN RETURN END';
GO
ALTER PROC dbo.Apq_SwithPatition
	 @DBName	nvarchar(256)
	,@SrcTName	nvarchar(256)
	,@DstTName	nvarchar(256)
	,@Month		datetime
AS
/* =============================================
-- 作者: 黄宗银
-- 日期: 2009-10-09
-- 描述: 切换表分区
-- 参数:
-- 示例:
DECLARE @rtn int;
EXEC @rtn = dbo.Apq_SwithPatition 'Stat_QQHX', 'log.Consume','his.Consume','20090901';
SELECT @rtn;
-- =============================================
1: 首选备份成功
2: 备用备份成功
*/
SET NOCOUNT ON;

DECLARE @rtn int, @SPBeginTime datetime, @sql nvarchar(4000), @PatitionNo nvarchar(50), @sqlDB nvarchar(4000);
SELECT @SPBeginTime=GetDate();

-- 计算 @PatitionNo
SELECT @PatitionNo = DATEDIFF(MM,'20070101',@Month) + 2;
--                                ↑这里是第一个分区的月数

SELECT @sql = 'ALTER TABLE ' + @SrcTName + ' SWITCH PARTITION ' + @PatitionNo
	+ ' TO ' + @DstTName + ' PARTITION ' + @PatitionNo;
SELECT @sqlDB = 'EXEC ' + @DBName + '.dbo.sp_executesql @sql';
--SELECT @sql,@sqlDB;
EXEC sp_executesql @sqlDB, N'@sql nvarchar(4000)',@sql=@sql;

RETURN 1;
GO
--EXEC dbo.Apq_Def_EveryDB 'dbo', 'Apq_SwithPatition', DEFAULT
GO
