IF ( object_id('dbo.Dinner_Employee_ListPager','P') IS NULL ) 
    EXEC sp_executesql N'CREATE PROCEDURE dbo.Dinner_Employee_ListPager AS BEGIN RETURN END' ;
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2011-04-11
-- 描述: 员工列表分页
-- 示例:
DECLARE @Pager_RowCount bigint
EXEC dbo.Dinner_Employee_ListPager 0, 5, @Pager_RowCount out
SELECT @Pager_RowCount;
-- =============================================
*/
ALTER PROC dbo.Dinner_Employee_ListPager
    @Pager_Page		int = 0				-- 页码(从0开始)
   ,@Pager_PageSize	int = 20			-- 每页行数
   ,@Pager_RowCount	bigint OUT			-- 输出总行数
AS 
SET NOCOUNT ON ;

DECLARE @nTop0 int;
SELECT @nTop0 = @Pager_PageSize * @Pager_Page;

SELECT @Pager_RowCount = Count(1) FROM dbo.Employee e;

SELECT TOP(@Pager_PageSize) e.LoginID, LoginName, LoginPwd, PwdExpire, LoginStatus, RegTime, e.EmID, e.IsAdmin, e.EmName, e.EmStatus
  FROM dbo.Logins l RIGHT JOIN dbo.Employee e ON l.LoginID = e.LoginID
 WHERE NOT EXISTS(SELECT TOP 1 1 FROM (SELECT TOP(@nTop0) EmID FROM dbo.Employee e0(NOLOCK) ORDER BY e.EmID) Apq_Pager0 WHERE Apq_Pager0.EmID = e.EmID)
 ORDER BY e.EmID;

/*
DECLARE @nTop0 int;
SELECT @nTop0 = @pNumber * @pSize;

SELECT TOP(@nTop0) ID, PayWay, uid, PayToday, _Time
  INTO #UserChannelPay_Today_ListPager
  FROM UserChannelPay_Today(NOLOCK)
 ORDER BY PayToday DESC;

SELECT TOP(@pSize) ID, PayWay, uid, PayToday, _Time
  FROM #UserChannelPay_Today_ListPager Apq_Pager0
 WHERE NOT EXISTS(SELECT TOP 1 1 FROM #UserChannelPay_Today_ListPager Apq_Pager1 WHERE Apq_Pager0.ID = Apq_Pager1.ID)
 ORDER BY PayToday DESC;

TRUNCATE TABLE #UserChannelPay_Today_ListPager;
DROP TABLE #UserChannelPay_Today_ListPager;
*/

RETURN 1;
GO
